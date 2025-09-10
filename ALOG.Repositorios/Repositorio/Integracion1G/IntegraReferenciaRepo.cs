using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G;
using Microsoft.EntityFrameworkCore;
using ALOG.Repositorios.Repositorio.Logistica;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using Microsoft.AspNetCore.Http;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Repositorio.Control;
using System;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Modelos.Modelos.Catalogos;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using ALOG.Repositorios.Repositorio.SPFunciones;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.Repositorios.Repositorio.Integración1G
{
    public class IntegraReferenciaRepo : GenericoRepositorio<IntegraReferencia>, IIntegraReferenciaRepo
    {
        private readonly ApplicationDbContext _db;
        private IVaciosRepositorio _vaciosRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenericoRepositorio<CatClientes> _iGenericoRepositorio;
        private readonly IGenericoRepositorio<CatAduana> _catAduanaRepositorio;
        private readonly IConfiguration _configuration;
        private readonly IOrdenesRepositorio _ordenesRepositorio;
        private DbSPFuncionesRepositorio _dbSPFuncionesRepositorio;
        public IntegraReferenciaRepo(ApplicationDbContext db,
                                     IGenericoRepositorio<CatClientes> iGenericoRepositorio,
                                     IGenericoRepositorio<CatAduana> catAduanaRepositorio,
                                     IHttpContextAccessor httpContextAccessor,
                                     IOrdenesRepositorio ordenesRepositorio,
                                     IVaciosRepositorio vaciosRepository,
                                     IConfiguration configuration                                     
            ) : base(db)
        {
            _db = db;
            _iGenericoRepositorio = iGenericoRepositorio;
            _catAduanaRepositorio = catAduanaRepositorio;
            _httpContextAccessor = httpContextAccessor;
            _ordenesRepositorio = ordenesRepositorio;
            _vaciosRepository = vaciosRepository;
            _configuration = configuration;
        }

        public async Task<ICollection<IntegraReferencia>> obtenerReferencias1G(FiltroIntegraReferencias1GDTO pFiltro)
        {
            try
            {


                pFiltro.IdIntReferencia = pFiltro.IdIntReferencia == null ? 0 : pFiltro.IdIntReferencia;
                pFiltro.IdOrden = pFiltro.IdOrden == null ? 0 : pFiltro.IdOrden;


                var objlst = _db.integracionReferencia
                    .Where(a => (pFiltro.IdIntReferencia <= 0 || a.IdIntReferencia == pFiltro.IdIntReferencia) &&
                                (pFiltro.IdOrden <= 0 || a.IdOrden == pFiltro.IdOrden) &&
                                (pFiltro.Activo == a.Activo) &&
                                (string.IsNullOrEmpty(pFiltro.ReferenciaALO) || a.ReferenciaALO == pFiltro.ReferenciaALO)
                                &&
                                 (string.IsNullOrEmpty(pFiltro.IdCompaniaExterna) || a.IdCompaniaExterna == pFiltro.IdCompaniaExterna) &&
                                 (string.IsNullOrEmpty(pFiltro.ClaveClienteExterno) || a.ClaveClienteExterno == pFiltro.ClaveClienteExterno) &&
                                 (string.IsNullOrEmpty(pFiltro.ReferenciaClienteExterno) || a.ReferenciaClienteExterno == pFiltro.ReferenciaClienteExterno) &&
                                 (string.IsNullOrEmpty(pFiltro.Estado1G) || a.Estado1G == pFiltro.Estado1G) &&
                                 (string.IsNullOrEmpty(pFiltro.RespuestaWS1G) || a.RespuestaWS1G.Contains(pFiltro.RespuestaWS1G))

                                 )

                    .ToList();
                return objlst;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IntegraReferencia obtenerreferencias1GPorId(int IdIntegraReferencia)
        {
            try
            {
                return _db.integracionReferencia.Include(a => a.ordenes).FirstOrDefault(x => x.IdIntReferencia == IdIntegraReferencia);
            }
            catch (Exception)
            {

                return null;
            }
        }

        #region Métodos para enviar solicitudes de facturación y referencias alo a 1G
        public bool ExisteOrdenPendiente(int idOrden) {
            var usuarioToken = GetUsuarioToken();

            return _db.ordenes.Any(o =>
                                      o.IdOrden.Equals(idOrden)
                                      && o.IdEstadoOrden.Equals(5)
                                      && (usuarioToken.IdCatCliente == null || o.IdCatCliente == usuarioToken.IdCatCliente)
                                  );
        }

        public async Task<Ordenes> GetOrdenPorId(int idOrden) {
            var usuarioToken = GetUsuarioToken();

            return await _db.ordenes
                                .Include(o => o.peticionesReferencias)
                                    .ThenInclude(r => r.Contenedores)
                                        .ThenInclude(c => c.Servicios)
                                .FirstOrDefaultAsync(o => o.IdOrden == idOrden
                                    && (usuarioToken.IdCatCliente == null || o.IdCatCliente.Equals(usuarioToken.IdCatCliente))
                                );
        }

        public RespuestaTokenDTO GetUsuarioToken() {
            // 1) Obtener el HttpContext de la petición
            var context = _httpContextAccessor.HttpContext;

            // 2) Llamar el método de extensión y retornar el objeto RespuestaTokenDTO
            return context.ObtenerUsuarioTokenUnificado();
        }

        public async Task<RespuestaGenericaDTO> CrearReferenciasFacturacion(List<int> ordenes) {
            var lstErrores = new List<string>();
            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();


            if (ordenes != null) {
                foreach (var idOrden in ordenes) {
                    var objOrden = await GetOrdenPorId(idOrden);
                    if (objOrden != null) {
                        objOrden.catAduana = await _catAduanaRepositorio.obtenerPorIdGenerico((int) objOrden.IdCatAduana);
                        respuesta = await GenerarFacturacionDesdeOrdenesAsync(objOrden);
                    }
                }
            }
            return respuesta;
        }

        #endregion Métodos para enviar solicitudes de facturación y referencias alo a 1G

        public async Task<RespuestaGenericaDTO> GenerarFacturacionDesdeOrdenesAsync(Ordenes orden) {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();
            _dbSPFuncionesRepositorio = new DbSPFuncionesRepositorio(_db);
            List<string> erroes = new List<string>();

            try {
                int consecutivo = 0;

                var contenedores = orden.peticionesReferencias
                    .SelectMany(r => r.Contenedores)
                    .ToList();

                // Agrupar por ReferenciaClienteFacturar
                var agrupadoPorReferenciaFacturar = contenedores
                    .SelectMany(c => c.Servicios.Select(s => new { Contenedor = c, Servicio = s }))
                    .GroupBy(x => x.Servicio.ReferenciaClienteFacturar);

                foreach (var grupo in agrupadoPorReferenciaFacturar) {
                    var ejemplo = grupo.First();
                    consecutivo++;

                    CatClientes clienteFacturar = await _iGenericoRepositorio.obtenerPorIdGenerico((int)ejemplo.Servicio.IdClienteFacturarA);
                    var referenciaAlo = await _dbSPFuncionesRepositorio.ObtenerReferenciaALO(orden, 0, 0, (int)ejemplo.Servicio.IdClienteFacturarA);
                    // 1. Crear IntegraReferencia
                    var integraReferencia = new IntegraReferencia {
                        IdOrden = orden.IdOrden,
                        IdCompaniaExterna = "3",
                        ReferenciaALO = referenciaAlo,
                        ReferenciaClienteExterno = grupo.Key,
                        ClaveClienteExterno = ejemplo.Servicio.IdClienteFacturarA.ToString(),
                        Aduana = orden.catAduana?.Aduana.ToString() ?? "00",
                        Enviado = false
                        //FechaEnvio = DateTime.Now
                    };

                    var respuestaIntReferencia = await _dbSPFuncionesRepositorio.GuardarIntegracionReferenciaAsync(integraReferencia);

                    if (respuestaIntReferencia.lstrErrorMessages.Count() > 0) {
                        erroes.AddRange(respuestaIntReferencia.lstrErrorMessages);
                        continue;
                    }

                    var integracionReferencia = (IntegraReferencia)respuestaIntReferencia.Entidad;

                    var integraFacturaEnc = new IntegraFacturaEnc {
                        IdOrden = orden.IdOrden,
                        IdPeticionesReferencia = ejemplo.Contenedor.IdReferencia,
                        IdSolicitudFacturacion = orden.IdOrden.ToString(),
                        IdCompaniaExterna = "3",
                        ClaveClienteExterno = ejemplo.Servicio.IdClienteFacturarA.ToString(),
                        Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                        ConceptoFacturacion = "3",
                        Comentario = "",
                        Nota = "",
                        Referencia = integraReferencia.ReferenciaALO,
                        ClaveSATMoneda = "MXN",
                        ClaveSATUsoCFDI = clienteFacturar.UsoCFDISAT,
                        RFC = clienteFacturar.Rfc1G,
                        FacturacionAutomatica = false,
                        CierreReferencia = false,
                        Activo = true,
                        FechaRegistro = DateTime.Now,
                        //FechaEnvio = DateTime.Now,
                        Enviado = false,
                        IdSucursalExterna = orden.catAduana.IdCatAduana,
                        CodigoPostal = clienteFacturar.CodigoPostal,
                        IdIntReferencia = integracionReferencia.IdIntReferencia
                    };

                    var integracionFacturaEnc = await _dbSPFuncionesRepositorio.GuardarIntegracionFacturaEncAsync(integraFacturaEnc);
                    var intFacturaEnc = (IntegraFacturaEnc) integracionFacturaEnc.Entidad;

                    if (integracionFacturaEnc.lstrErrorMessages.Count() > 0) {
                        erroes.AddRange(integracionFacturaEnc.lstrErrorMessages);
                        continue;
                    }

                    foreach (var item in grupo) {
                        var detalle = new IntegraFacturaDet {
                            IddIntFacturaEnc = intFacturaEnc.IdIntFacturaEnc,
                            ClaveServicio = item.Servicio.IdTipoServicio.ToString(),
                            Cantidad = "1",
                            Precio = item.Servicio.IdTipoServicio == 3 ? "500" : "5670", // Lógica ejemplo
                            IdPeticionesReferencia = item.Contenedor.IdReferencia,
                            IdPeticionesContenedor = item.Contenedor.IdContenedor,
                            Contenedor = item.Contenedor.Contenedor,
                            CentroCostos = orden.catAduana.IdCatAduana.ToString(),
                            EIR = "",
                            Comentario = "",
                            Nota = "",
                            IdCatServicio = item.Servicio.IdTipoServicio
                        };

                        var respuestaIntFacturaDet = await _dbSPFuncionesRepositorio.GuardarIntegracionFacturaDetAsync(detalle);
                        var intFacturaDet = (IntegraFacturaDet) respuestaIntFacturaDet.Entidad;

                        if (respuestaIntFacturaDet.lstrErrorMessages.Count() > 0) {
                            erroes.AddRange(respuestaIntFacturaDet.lstrErrorMessages);
                            continue;
                        }
                    }

                }
            } catch (Exception ex) {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            respuesta.lstrErrorMessages = erroes;

            respuesta.IsSuccess = respuesta.lstrErrorMessages.Count() == 0;
            respuesta.StatusCode = respuesta.lstrErrorMessages.Count() == 0 ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;

            return respuesta;
        }
    }
}
