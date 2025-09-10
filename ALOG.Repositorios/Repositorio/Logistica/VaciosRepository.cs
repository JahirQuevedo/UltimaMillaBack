using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Catalogos;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Repositorio.RutasArchivos;
using ALOG.Repositorios.Repositorio.SPFunciones;
using ALOG.Repositorios.Utilerias;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using ALOG.Repositorios.Repositorio.Control;
using ALOG.Modelos.Modelos.DTO.Utilerias;
using System.Net;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using ALOG.Modelos.Modelos.Facturacion;
using ALOG.Repositorios.Repositorio.Integracion1G.IIntegracion1G;
using ALOG.Modelos.Modelos.Peticiones;
using ALOG.Modelos;
using ALOG.Modelos.Modelos;

namespace ALOG.Repositorios.Repositorio.Logistica
{
    public class VaciosRepository : IVaciosRepositorio
    {
        #region Variables Globales
        private readonly ApplicationDbContext _db;
        private readonly string _pathBaseExpediente;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrdenesRepositorio _ctRepoOrdenes;

        private CatTransportitasRepositorio ClsCatTransportitasRepositorio;
        private CatProveedoresRepositorio ClsCatProveedoresRepositorio;
        private CatNavierasRepositorio ClsCatNavierasRepositorio;
        private CatClientesRepositorio ClsCatClientesRepositorio;
        private CatPatiosRepositorio ClsCatPatiosRespositorio;
        private DbSPFuncionesRepositorio _spfuncionRepo;
        private IIntegracion1GRepo _integracion1GRepo;
        private IPeticionesContenedorCronRepo _ctRepoContenedoresCron;

        #endregion Variables Globales

        public VaciosRepository(ApplicationDbContext db, IOptions<RutasArchivosRepositorio> opciones, IHttpContextAccessor httpContextAccessor, IOrdenesRepositorio ctRepoOrdenes, IIntegracion1GRepo integracion1GRepo, IPeticionesContenedorCronRepo ctRepoContenedoresCron)
        {
            _db = db;
            _pathBaseExpediente = opciones.Value.PathBaseExpediente;
            _httpContextAccessor = httpContextAccessor;
            _ctRepoOrdenes = ctRepoOrdenes;
            _integracion1GRepo = integracion1GRepo;
            _ctRepoContenedoresCron = ctRepoContenedoresCron;
            //estadosProceso = new[]
            //{
            //    EnumEstados.EstadosReferencias.EnProceso,
            //        EnumEstados.EstadosReferencias.Abierta
            // };
        }

        #region METODOS PUBLICOS
        #region CREAR
        public async Task<PeticionesRespuestaDTO> CrearOrden(PeticionesReferenciasClienteExternoDTO peticionesReferenciasClienteExternoDTO)
        {
            #region VARIABLES
            var objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.ErrorMessages = new List<string>();
            var objOrden = new Ordenes();
            objOrden.peticionesReferencias = new List<PeticionesReferencias>();
            var usuarioToken = GetUsuarioToken();
            #endregion VARIABLES

            #region INICIALIZACIÓN DE VARIABLES
            objRespuesta.IsSuccess = false;
            objRespuesta.StatusCode = HttpStatusCode.BadRequest;
            #endregion INICIALIZACIÓN DE VARIABLES

            if (usuarioToken != null)
            {
                //1).- VALIDAMOS QUE LA ADUANA EXISTA EN NUESTRO CATALOGO
                var objCatAduana = GetAduana(idCatAduana: peticionesReferenciasClienteExternoDTO.IdCatAduana);
                if (objCatAduana == null)
                {
                    objRespuesta.ErrorMessages.Add("La aduana que indicó no se encuentra dentro de nuestro catálogo, favor de revisar su solicitud.");
                    return objRespuesta;
                }

                #region ASIGNACIÓN DE VALORES POR DEFAULT AL OBJETO Ordenes
                objOrden.IdUsuario = usuarioToken.IdCatUsuario == 0 ? null : usuarioToken.IdCatUsuario;
                objOrden.IdCatSistema = usuarioToken.IdCatSistema == 0 ? null : usuarioToken.IdCatSistema;
                objOrden.IdCatEmpresa = 2;
                objOrden.IdCatSucursal = 1; //Veracruz.
                objOrden.IdCatLineaNegocio = 1; //Vacios
                objOrden.IdCatAduana = objCatAduana.IdCatAduana;
                #endregion ASIGNACIÓN DE VALORES POR DEFAULT AL OBJETO Ordenes

                //2).- PROCEDEMOS A VALIDAR QUE LOS CAMPOS DE LA REFERENCIA NO VENGAN VACÍOS SEGÚN LOS CRITERIOS A TOMAR
                #region VALIDAMOS LA INFORMACIÓN DE LA REFERENCIA
                if (ValidarCamposObjetoReferenciaClienteExterno(peticionesReferenciasClienteExternoDTO, objOrden, out var lstStrErroresReferenciaOut))
                {
                    //2.1).- POSTERIORMENTE A LA VALIDACIÓN, PROCEDEMOS A LLENAR EL OBJETO PeticionesReferencias PARA SU POSTERIOR GUARDADO
                    var objPeticionesReferencias = LlenarObjetoReferencia(peticionesReferenciasClienteExternoDTO, out var lstStrLlenarReferenciaOut);
                    if (lstStrLlenarReferenciaOut.Count == 0)
                    {
                        // 3).- UNA VEZ QUE SE VALIDORON LOS CAMPOS DE LA REFERENCIA, CONTENEDORES Y SERVICIOS Y NO EXISTIÓ ERROR ALGUNO, PROCEDEMOS A CREAR LA ORDEN Y LA REFERENCIA.
                        //3.1).- GENERAMOS LA REFERENCIA ALO
                        var _dbSpFunciones = new DbSPFuncionesRepositorio(_db);
                        var vStrReferenciaALO = await _dbSpFunciones.ObtenerReferenciaALO(objOrden, 0, 0, 1);

                        if (!string.IsNullOrEmpty(vStrReferenciaALO))
                        {

                            objPeticionesReferencias.Contenedores
                                                                 .ToList()
                                                                 .ForEach(c =>
                                                                 {

                                                                     if (string.IsNullOrWhiteSpace(c.RefenciaCliente))
                                                                         c.RefenciaCliente = vStrReferenciaALO;
                                                                     if (string.IsNullOrWhiteSpace(c.ReferenciaFacturacion))
                                                                         c.ReferenciaFacturacion = vStrReferenciaALO;

                                                                     c.Servicios?
                                                                      .ToList()
                                                                      .ForEach(s =>
                                                                      {
                                                                          if (string.IsNullOrWhiteSpace(s.ReferenciaClienteFacturar))
                                                                              s.ReferenciaClienteFacturar = vStrReferenciaALO;
                                                                      });
                                                                 });

                            //VALIDAMOS QUE EL OBJETO PETICIONES REFERENCIAS ESTE CORRECTO ANTES DE MANDARLO A GUARDAR.
                            if (ValidarObjetoPeticionesReferencia(objPeticionesReferencias, objOrden.IdCatCliente, out var lstStrErroresValidaReferenciaOut))
                            {
                                int idEstado;
                                string estado;

                                if (EsUsuarioInterno())
                                {
                                    objOrden.IdEstadoOrden = 2;
                                    idEstado = 4;
                                    estado = "P";
                                }
                                else
                                {
                                    objOrden.IdEstadoOrden = 5;
                                    idEstado = 7;
                                    estado = "PE";
                                }

                                objOrden.ReferenciaALO = vStrReferenciaALO;

                                objPeticionesReferencias.IdCatReferenciaEstado = idEstado;
                                objPeticionesReferencias.EstadoReferencia = estado;
                                objPeticionesReferencias.Contenedores
                                                                 .ToList()
                                                                 .ForEach(c =>
                                                                 {
                                                                     c.IdEstadoContenedor = idEstado;
                                                                     c.EstadoContenedor = estado;

                                                                     c.Servicios?
                                                                      .ToList()
                                                                      .ForEach(s =>
                                                                      {
                                                                          s.IdEstadoServicio = idEstado;
                                                                          s.EstadoServicio = estado;
                                                                      });
                                                                 });

                                using (var transaction = _db.Database.BeginTransaction())
                                {
                                    //CREAMOS LA ORDEN
                                    var ordenInsertada = await _ctRepoOrdenes.CrearOrden(objOrden);
                                    if (ordenInsertada != null)
                                    {
                                        //AGREGO EL IDENTIFICADOR DE LA ORDEN A MI OBJETO PETICIONES REFERENCIAS PARA SU RELACIÓN
                                        objPeticionesReferencias.IdOrden = ordenInsertada.IdOrden;

                                        //SE CREA LA REFERENCIA JUNTO CON TODAS SUS RELACIONES
                                        if (CrearReferencia(objPeticionesReferencias, out var lstStrErroresCrearReferenciaOut))
                                        {
                                            //SI SE CREA CORRECTAMENTE, CONFIRMAMOS LA TRANSACCIÓN.
                                            transaction.Commit();

                                            if (EsUsuarioInterno())
                                            {
                                                var lstPeticionesContenedores = objPeticionesReferencias.Contenedores;

                                                foreach (var pCont in lstPeticionesContenedores)
                                                {
                                                    if (pCont.EstadoContenedor == "P" && pCont.IdEstadoContenedor == 4)
                                                    {
                                                        foreach (var servicio in pCont.Servicios)
                                                        {
                                                            if (servicio.EstadoServicio == "P" && servicio.IdEstadoServicio == 4)
                                                            {
                                                                var objPeticionesContenedorCron = new PeticionesContenedoresCron
                                                                {
                                                                    IdCatTipoIncidenciaEvento = 1,
                                                                    IdRegistroUsuario = 1,
                                                                    IdContenedor = pCont.IdContenedor,
                                                                    Comentarios = "Inicio de operación.",
                                                                    FechaEvento = DateTime.Now,
                                                                    FechaRegistro = DateTime.Now,
                                                                    IdServicio = servicio.IdServicio
                                                                };

                                                                await _ctRepoContenedoresCron.agregarCronologiaporContenedor(objPeticionesContenedorCron);
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            objRespuesta.IsSuccess = true;
                                            objRespuesta.StatusCode = HttpStatusCode.OK;
                                            objRespuesta.IdReferenciaALO = objPeticionesReferencias.IdReferencia;
                                            objRespuesta.IdOrdenServicio = objPeticionesReferencias.IdOrden;

                                            var lstErroresOutDocumentos = await CrearDocumentos(objPeticionesReferencias, peticionesReferenciasClienteExternoDTO);
                                            if (lstErroresOutDocumentos.Count > 0)
                                            {
                                                objRespuesta.ErrorMessages.Add("Se creo la orden de servicio pero se presentaron problemas al guardar los documentos.");
                                            }
                                        }
                                        else
                                        {
                                            transaction.Rollback();
                                            objRespuesta.ErrorMessages.Add("Se presento un problema al guardar la referencia.");
                                            objRespuesta.ErrorMessages.AddRange(lstStrErroresCrearReferenciaOut);
                                        }
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        objRespuesta.ErrorMessages.Add("Se presento un problema al crear la orden");
                                    }
                                }
                            }
                            else
                                objRespuesta.ErrorMessages.AddRange(lstStrErroresValidaReferenciaOut);
                        }
                    }
                    else
                        objRespuesta.ErrorMessages.AddRange(lstStrLlenarReferenciaOut);
                }
                else
                    objRespuesta.ErrorMessages.AddRange(lstStrErroresReferenciaOut);

                #endregion VALIDAMOS LA INFORMACIÓN DE LA REFERENCIA
            }
            else
                objRespuesta.ErrorMessages.Add("No se pudo obtener la información del usuario. Favor de intentar nuevamente.");

            return objRespuesta;
        }
        public async Task<PeticionesRespuestaDTO> AgregarContenedorAReferencia(int idReferencia, PeticionesContenedoresClienteExternoDTO peticionesContenedoresClienteExternoDTO)
        {
            #region Variables
            var objContenedor = new PeticionesContenedores();
            var objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.ErrorMessages = new List<string>();
            #endregion Variables

            #region Asignación de valores a variables
            objRespuesta.IsSuccess = false;
            objRespuesta.StatusCode = HttpStatusCode.BadRequest;
            #endregion

            //1).- VALIDAMOS SI LA REFERENCIA SE ENCUENTRA ACTIVA Y EN CASO DE SER UN CLIENTE QUIEN CONSULTA, VALIDAMOS QUE LA REFERENCIA QUE CONSULTA LE PERTENEZCA.
            if (ExisteReferenciaActiva(idReferencia))
            {
                //2).- OBTENEMOS LA INFORMACIÓN DE LA ORDEN RELACIONADA A LA REFERENCIA
                var objOrden = await GetOrdenActiva(idReferencia: idReferencia);
                if (objOrden != null)
                {
                    //3).- VALIDAMOS QUE LOS DATOS DE LOS CAMPOS DEL OBJETO PeticionesContenedoresClienteExternoDTO SEAN CORRECTOS
                    if (ValidarCamposObjetoContenedorClienteExterno(peticionesContenedoresClienteExternoDTO, objOrden, out var lstStrErroresOut))
                    {
                        //4).- OBTENEMOS LA INFORMACIÓN DE LA ADUANA QUE SE ENCUENTRA EN LA ORDEN
                        var objCatAduana = GetAduana(idCatAduana: (int)objOrden.IdCatAduana);
                        if (objCatAduana != null)
                        {
                            //4).- ASIGNAMOS LOS VALORES DE LA ADUANA AL CONTENEDOR
                            objContenedor.AduanaId = objCatAduana.Aduana;
                            objContenedor.Aduana = objCatAduana.Nombre;
                            if (EsClienteNad(idCatCliente: objOrden.IdCatCliente))
                            {
                                objContenedor.RefenciaCliente = peticionesContenedoresClienteExternoDTO.ReferenciaCliente;
                                objContenedor.ReferenciaFacturacion = peticionesContenedoresClienteExternoDTO.Servicios?.LastOrDefault()?.ReferenciaClienteFacturar;
                            }
                            else
                            {
                                objContenedor.RefenciaCliente = objOrden.ReferenciaALO;
                                objContenedor.ReferenciaFacturacion = objOrden.ReferenciaALO;
                            }

                            objContenedor.IdReferencia = idReferencia;
                            objContenedor.IdCatTransportista = null;

                            //PROCEDEMOS A LLENAR EL OBJETO PETICIONESCONTENEDORES CON LOS DATOS DE LA SOLIITUD
                            var lstStrErrores = LlenarObjetoContenedor(peticionesContenedoresClienteExternoDTO, objContenedor);
                            if (lstStrErrores.Count == 0)
                            {
                                //5).- VALIDAMOS EL OBJETO PETICIONESCONTENEDORES
                                if (ValidarObjetoPeticionesContenedores(objContenedor, objOrden.IdCatCliente, out var lstStrErroresPCOut))
                                {
                                    using (var transaction = _db.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            //6).- CREAMOS EL CONTENEDOR JUNTO CON SUS SERVICIOS
                                            var lstStrErroresCrearContenedor = await CrearContenedor(idReferencia, objContenedor);
                                            if (lstStrErroresCrearContenedor.Count == 0)
                                            {
                                                objRespuesta.IsSuccess = true;
                                                objRespuesta.StatusCode = HttpStatusCode.OK;
                                                objRespuesta.IdReferenciaALO = idReferencia;
                                                objRespuesta.IdOrdenServicio = objOrden.IdOrden;
                                                // Si todo sale bien, se confirma la transacción
                                                transaction.Commit();

                                                if (EsUsuarioInterno())
                                                {
                                                    if (objContenedor.EstadoContenedor == "P" && objContenedor.IdEstadoContenedor == 4)
                                                    {
                                                        foreach (var servicio in objContenedor.Servicios)
                                                        {
                                                            if (servicio.EstadoServicio == "P" && servicio.IdEstadoServicio == 4)
                                                            {
                                                                var objPeticionesContenedorCron = new PeticionesContenedoresCron
                                                                {
                                                                    IdCatTipoIncidenciaEvento = 1,
                                                                    IdRegistroUsuario = 1,
                                                                    IdContenedor = objContenedor.IdContenedor,
                                                                    Comentarios = "Inicio de operación.",
                                                                    FechaEvento = DateTime.Now,
                                                                    FechaRegistro = DateTime.Now,
                                                                    IdServicio = servicio.IdServicio
                                                                };

                                                                await _ctRepoContenedoresCron.agregarCronologiaporContenedor(objPeticionesContenedorCron);
                                                            }
                                                        }
                                                    }
                                                }

                                                //7).- OBTENEMOS LA INFORMACIÓN DE LA ORDEN Y FILTRAMOS LA REFERENCIA POR EL 
                                                var peticionesReferencias = await GetReferencia(idReferencia);
                                                var peticionesReferenciasClienteExternoDTO = new PeticionesReferenciasClienteExternoDTO
                                                {
                                                    Contenedores = new List<PeticionesContenedoresClienteExternoDTO>
                                                    {
                                                        peticionesContenedoresClienteExternoDTO
                                                    }
                                                };

                                                var lstErroresOutDocumentos = await CrearDocumentos(peticionesReferencias, peticionesReferenciasClienteExternoDTO);
                                                if (lstErroresOutDocumentos.Count > 0)
                                                {
                                                    objRespuesta.ErrorMessages.Add("El contenedor se creo correctamente, pero se presentaron problemas al guardar los documentos.");
                                                }
                                            }
                                            else
                                            {
                                                // En caso de error, se revierte la transacción.
                                                transaction.Rollback();

                                                objRespuesta.ErrorMessages = lstStrErrores;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // En caso de error, se revierte la transacción.
                                            transaction.Rollback();

                                            objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                                            objRespuesta.ErrorMessages.Add($"Error al guardar el contenedor.");
                                        }
                                    }
                                }
                                else
                                    objRespuesta.ErrorMessages.AddRange(lstStrErroresPCOut);
                            }
                            else
                                objRespuesta.ErrorMessages.AddRange(lstStrErrores);
                        }
                    }
                    else
                        objRespuesta.ErrorMessages.AddRange(lstStrErroresOut);
                }
                else
                    objRespuesta.ErrorMessages.Add($"Error al guardar el contenedor {peticionesContenedoresClienteExternoDTO.Contenedor}.");
            }
            else
                objRespuesta.ErrorMessages.Add("No fue posible agregar el contenedor a la referencia, ya que la referencia no existe o no se encuentra activa.");

            return objRespuesta;
        }
        public async Task<PeticionesRespuestaDTO> AgregarServicioAContenedor(int idReferencia, int idContenedor, PeticionesServiciosClienteExternoDTO pServClienteExterno)
        {
            #region Variables
            var objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.ErrorMessages = new List<string>();
            #endregion Variables

            #region Asignación de valores a variables
            objRespuesta.IsSuccess = false;
            objRespuesta.StatusCode = HttpStatusCode.BadRequest;
            #endregion Asignación de valores a variables


            //var objPeticionesContenedores = GetContenedor(idReferencia, idContenedor);
            var objOrden = await GetOrdenActiva(idReferencia: idReferencia);
            if (objOrden != null)
            {
                if (ValidarCamposObjetoServicioClienteExterno(pServClienteExterno, objOrden, out var lstStrErroresServicioClienteExterno))
                {
                    var objContenedor = objOrden.peticionesReferencias
                                                        .SelectMany(pr => pr.Contenedores)
                                                        .FirstOrDefault(c => c.IdContenedor == idContenedor);


                    var objPeticionesServicios = LlenarObjetoServicio(objContenedor, pServClienteExterno, out var lstStrErroresLlenarPeticionesServicios);
                    if (lstStrErroresLlenarPeticionesServicios.Count == 0)
                    {
                        if (ValidarObjetoPeticionesServicios(objPeticionesServicios, out var lstStrErroresPeticionServicio))
                        {
                            if (objPeticionesServicios != null)
                            {
                                if (CrearServicio(idReferencia, idContenedor, objPeticionesServicios))
                                {
                                    objRespuesta.StatusCode = HttpStatusCode.OK;
                                    objRespuesta.IsSuccess = true;

                                    if (EsUsuarioInterno())
                                    {
                                        if (objPeticionesServicios.EstadoServicio == "P" && objPeticionesServicios.IdEstadoServicio == 4)
                                        {
                                            var objPeticionesContenedorCron = new PeticionesContenedoresCron
                                            {
                                                IdCatTipoIncidenciaEvento = 1,
                                                IdRegistroUsuario = 1,
                                                IdContenedor = objPeticionesServicios.IdContenedor,
                                                Comentarios = "Inicio de operación.",
                                                FechaEvento = DateTime.Now,
                                                FechaRegistro = DateTime.Now,
                                                IdServicio = objPeticionesServicios.IdServicio
                                            };

                                            await _ctRepoContenedoresCron.agregarCronologiaporContenedor(objPeticionesContenedorCron);
                                        }
                                    }
                                }
                                else
                                {
                                    objRespuesta.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                                    objRespuesta.ErrorMessages.Add($"Algo salió mal guardando el registro del servicio: {objPeticionesServicios.IdTipoServicio}");
                                }
                            }
                        }
                        else
                            objRespuesta.ErrorMessages.AddRange(lstStrErroresPeticionServicio);
                    }
                    else
                        objRespuesta.ErrorMessages.AddRange(lstStrErroresLlenarPeticionesServicios);
                }
                else
                    objRespuesta.ErrorMessages.AddRange(lstStrErroresServicioClienteExterno);
            }
            else
                objRespuesta.ErrorMessages.Add("No se encontro la orden relacionada a la referencia indicada.");

            return objRespuesta;
        }
        public bool CrearServicio(int IdReferencia, int IdContenedor, PeticionesServicios servicio)
        {
            string strError = "";
            if (servicio.IdTipoServicio > 0)
            {
                if (ExisteReferenciaActiva(IdReferencia))
                {
                    if (ExisteContenedorActivo(idReferencia: IdReferencia, idContenedor: IdContenedor))
                    {
                        servicio.FechaRegistro = DateTime.Now;
                        servicio.FechaCierre = null;
                        servicio.IdContenedor = IdContenedor;
                        servicio.IdEstadoServicio = servicio.IdEstadoServicio > 0 ? servicio.IdEstadoServicio : 7;
                        servicio.EstadoServicio = !string.IsNullOrWhiteSpace(servicio.EstadoServicio) ? servicio.EstadoServicio : "PE";
                        servicio.Activo = true;
                        _db.peticionesServicios.Add(servicio);
                        return Guardar(out strError);
                    }
                }
            }
            return false;
        }
        #endregion CREAR

        #region GET
        public async Task<ICollection<RespObtenerReferenciasDTO>> GetReferencias(FiltroOrdenesReferenciasDTO pFiltro)
        {
            var usuarioToken = GetUsuarioToken();

            // Determinar cliente según token
            if (usuarioToken.IdCatCliente == null &&
                (usuarioToken.RolesUsuario.Contains("ADMIN") ||
                 usuarioToken.RolesUsuario.Contains("ADMINUSER") ||
                 "ALogistics".Equals(usuarioToken.User, StringComparison.OrdinalIgnoreCase)))
            {
                pFiltro.IdCliente ??= 0;
            }
            else
            {
                pFiltro.IdCliente = usuarioToken.IdCatCliente;
            }

            // Valores por defecto
            pFiltro.IdAduana ??= 0;
            pFiltro.IdEmpresa ??= new List<string>();
            pFiltro.IdLNegocio ??= 0;
            pFiltro.IdOrden ??= 0;
            pFiltro.IdServicio ??= 0;
            pFiltro.Ticket ??= 0;
            pFiltro.IdPatio ??= 0;
            pFiltro.IdCatEstadoOrden ??= 0;
            pFiltro.IdCatEstadoReferencia ??= 0;
            pFiltro.IdCatEstadoContenedor ??= 0;
            pFiltro.FechaSolicitudIni ??= DateTime.Now.AddDays(-20);
            pFiltro.FechaSolicitudFin ??= DateTime.Now;

            // Base de la consulta
            var query = _db.peticionesReferencias.AsQueryable();

            // Filtros de cabecera
            query = query.Where(r =>
                (pFiltro.IdCliente <= 0 || r.ordenes.IdCatCliente == pFiltro.IdCliente) &&
                (pFiltro.IdOrden <= 0 || r.IdOrden == pFiltro.IdOrden) &&
                (pFiltro.IdAduana <= 0 || r.ordenes.IdCatAduana == pFiltro.IdAduana) &&
                (pFiltro.IdEmpresa.Count == 0 || pFiltro.IdEmpresa.Contains(r.ordenes.IdCatEmpresa.ToString())) &&
                (pFiltro.IdLNegocio <= 0 || r.ordenes.IdCatLineaNegocio == pFiltro.IdLNegocio) &&
                (pFiltro.Ticket <= 0 || r.Ticket == pFiltro.Ticket) &&
                (pFiltro.IdCatEstadoOrden <= 0 || r.ordenes.IdEstadoOrden == pFiltro.IdCatEstadoOrden) &&
                (pFiltro.IdCatEstadoReferencia <= 0 || r.IdCatReferenciaEstado == pFiltro.IdCatEstadoReferencia) &&
                (r.FechaSolicitud >= pFiltro.FechaSolicitudIni && r.FechaSolicitud <= pFiltro.FechaSolicitudFin) &&
                (string.IsNullOrEmpty(pFiltro.ReferenciaALO) || r.ordenes.ReferenciaALO == pFiltro.ReferenciaALO)
            );

            // Filtros de contenedores (simulan LEFT JOIN)
            if (pFiltro.IdCatEstadoContenedor > 0)
                query = query.Where(r => r.Contenedores.Any(c => c.IdEstadoContenedor == pFiltro.IdCatEstadoContenedor));
            if (pFiltro.IdPatio > 0)
                query = query.Where(r => r.Contenedores.Any(c => c.PatioId == pFiltro.IdPatio));
            if (!string.IsNullOrEmpty(pFiltro.Contenedor))
                query = query.Where(r => r.Contenedores.Any(c => c.Contenedor == pFiltro.Contenedor));
            if (!string.IsNullOrEmpty(pFiltro.Buque))
                query = query.Where(r => r.Contenedores.Any(c => c.Buque.Contains(pFiltro.Buque)));
            if (!string.IsNullOrEmpty(pFiltro.Ejecutivo))
                query = query.Where(r => r.Contenedores.Any(c => c.Cliente_Solicitante.Contains(pFiltro.Ejecutivo)));
            if (!string.IsNullOrEmpty(pFiltro.ReferenciaCliente))
                query = query.Where(r => r.Contenedores.Any(c => c.RefenciaCliente == pFiltro.ReferenciaCliente));
            if (pFiltro.IdServicio > 0)
                query = query.Where(r => r.Contenedores.Any(c => c.Servicios.Any(s => s.IdTipoServicio == pFiltro.IdServicio)));

            // Proyección final con filtrado en colecciones
            var resultado = await query.Select(r => new RespObtenerReferenciasDTO
            {
                IdReferencia = r.IdReferencia,
                Ticket = r.Ticket ?? 0,
                Transporte_RFC = r.Transporte_RFC,
                Transporte_RazonSocial = r.Transporte_RazonSocial,
                TrasporteId = r.TrasporteId ?? 0,
                Transporte_Usuario = r.Transporte_Usuario,
                Transporte_UsuarioEmail = r.Transporte_UsuarioEmail,
                Comentarios = r.Comentarios,
                TipoReferencia = r.TipoReferencia,
                Procesado = r.Procesado,
                FechaProcesado = r.FechaProcesado,
                FechaSolicitud = r.FechaSolicitud,
                EstadoReferencia = r.EstadoReferencia,
                IdCatReferenciaEstado = r.IdCatReferenciaEstado,
                Activo = r.Activo,
                IdOrden = r.IdOrden,
                IdCatcliente = r.ordenes.IdCatCliente,
                RazonSocialCliente = r.ordenes.catClientes.RazonSocial,
                IdCatProveedor = r.ordenes.IdCatProveedor ?? 0,
                RazonSocialProveedor = r.ordenes.catProveedores.RazonSocial,
                IdCatAduana = r.ordenes.IdCatAduana ?? 0,
                Aduana = r.ordenes.catAduana.Acronimo,
                IdCatSistema = r.ordenes.IdCatSistema ?? 0,
                Sistema = r.ordenes.catSistemas.nombre,
                IdCatEmpresa = r.ordenes.IdCatEmpresa,
                RazonSocialEmpresa = r.ordenes.CatEmpresas.RazonSocial,
                IdCatsucursal = r.ordenes.IdCatSucursal,
                Sucursal = r.ordenes.CatSucursales.Nombre,
                IdLNegocio = r.ordenes.IdCatLineaNegocio,
                LineaNegocio = r.ordenes.CatLineaNegocio.Acronimo,
                IdUsuario = r.ordenes.IdUsuario ?? 0,
                Usuario = r.ordenes.catUsuario.Usuario,
                ActivoOrden = r.ordenes.Activo,
                FechaRegistroOrden = r.ordenes.FechaRegistro,
                FechaCierreOrden = r.ordenes.FechaCierre,
                FechaRegistroReferencia = r.FechaRegistro,
                FechaCierreReferencia = r.FechaCierre,
                ReferenciaALO = r.ordenes.ReferenciaALO ?? string.Empty,
                peticionesContenedores = r.Contenedores
                    .Where(c =>
                        (pFiltro.IdCatEstadoContenedor <= 0 || c.IdEstadoContenedor == pFiltro.IdCatEstadoContenedor) &&
                        (pFiltro.IdPatio <= 0 || c.PatioId == pFiltro.IdPatio) &&
                        (string.IsNullOrEmpty(pFiltro.Contenedor) || c.Contenedor == pFiltro.Contenedor) &&
                        (string.IsNullOrEmpty(pFiltro.Buque) || c.Buque.Contains(pFiltro.Buque)) &&
                        (string.IsNullOrEmpty(pFiltro.Ejecutivo) || c.Cliente_Solicitante.Contains(pFiltro.Ejecutivo)) &&
                        (string.IsNullOrEmpty(pFiltro.ReferenciaCliente) || c.RefenciaCliente == pFiltro.ReferenciaCliente)
                    )
                    .Select(c => new PeticionesContenedores
                    {
                        IdContenedor = c.IdContenedor,
                        Contenedor = c.Contenedor,
                        FolioManiobra = c.FolioManiobra,
                        IdCatTipoContenedor = c.IdCatTipoContenedor,
                        ClaveTipoContenedor = c.ClaveTipoContenedor,
                        catTipoContenedor = c.catTipoContenedor,
                        IdEstadoContenedor = c.IdEstadoContenedor,
                        EstadoContenedor = c.EstadoContenedor,
                        RefenciaCliente = c.RefenciaCliente,
                        FechaRegistro = c.FechaRegistro,
                        FechaCierre = c.FechaCierre,
                        Aduana = c.Aduana,
                        AduanaId = c.AduanaId,
                        PatioId = c.PatioId,
                        Patio_RFC = c.Patio_RFC,
                        Patio_RazonSocial = c.Patio_RazonSocial,
                        catPatios = c.catPatios,
                        Naviera_Id = c.Naviera_Id,
                        Naviera_RFC = c.Naviera_RFC,
                        Naviera_RazonSocial = c.Naviera_RazonSocial,
                        catNavieras = c.catNavieras,
                        BL = c.BL,
                        ClienteId = c.ClienteId,
                        Cliente_RFC = c.Cliente_RFC,
                        Cliente_RazonSocial = c.Cliente_RazonSocial,
                        Consignado_RazonSocial = c.Consignado_RazonSocial,
                        IdReferencia = c.IdReferencia,
                        catClientes = c.catClientes,
                        Servicios = c.Servicios
                            .Where(s => pFiltro.IdServicio <= 0 || s.IdTipoServicio == pFiltro.IdServicio)
                            .Select(s => new PeticionesServicios
                            {
                                IdServicio = s.IdServicio,
                                IdContenedor = s.IdContenedor,
                                IdTipoServicio = s.IdTipoServicio,
                                Documentos = s.Documentos
                                    .Select(d => new PeticionesDocumentos
                                    {
                                        IdDocumento = d.IdDocumento,
                                        IdTipoDocumento = d.IdTipoDocumento,
                                        NombreDocumento = d.NombreDocumento,
                                        DocumentoUUID = d.DocumentoUUID,
                                        CatDocumento = d.CatDocumento

                                    }).ToList(),
                                catServicios = s.catServicios,
                                ReferenciaClienteFacturar = s.ReferenciaClienteFacturar,
                                FechaRegistro = s.FechaRegistro,
                                FechaCierre = s.FechaCierre,
                                IdEstadoServicio = s.IdEstadoServicio,
                                catReferenciaEstado = s.catReferenciaEstado
                            }).ToList()
                    }).ToList()
            }).ToListAsync();

            return resultado;
        }
        public async Task<PeticionesReferencias> GetReferencia(int IdReferencia)
        {
            var usuarioToket = GetUsuarioToken();
            int? idCatCliente = usuarioToket.IdCatCliente;

            var obj = await _db.peticionesReferencias
                .Include(x => x.catReferenciaEstado)
                .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(d => d.Documentos).ThenInclude(e => e.CatDocumento)
                .Include(cc => cc.Contenedores).ThenInclude(cp => cp.catPatios)
                 .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(cr => cr.catReferenciaEstado)
                 .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(cs => cs.catServicios)
                 .Include(r => r.ordenes)
                 .Where(r => (idCatCliente == null || r.ordenes.IdCatCliente == idCatCliente)
                             && r.IdReferencia == IdReferencia)
                 .FirstOrDefaultAsync();
            return obj;
        }

        ///// <summary>
        ///// Obtiene una referencia junto con sus relaciones (contenedores, servicios, documentos, patios, estado, orden)
        ///// siempre y cuando la referencia pertenezca al cliente autorizado (si corresponde).
        ///// </summary>
        ///// <param name="IdReferencia">Id de la referencia que se desea obtener.</param>
        ///// <returns>La entidad PeticionesReferencias con todo su árbol cargado, o null si no existe o no pertenece al cliente.</returns>
        //public async Task<PeticionesReferencias?> GetReferencia(int IdReferencia)
        //{
        //    // Recuperar token y cliente (si está autenticado como cliente específico)
        //    var usuarioToken = GetUsuarioToken();
        //    int? idCatCliente = usuarioToken.IdCatCliente;

        //    // Construir la consulta base
        //    var query = _db.peticionesReferencias
        //        .Include(x => x.catReferenciaEstado)
        //        .Include(x => x.ordenes)
        //        .Include(x => x.Contenedores)
        //            .ThenInclude(c => c.Servicios)
        //                .ThenInclude(s => s.Documentos)
        //                    .ThenInclude(d => d.CatDocumento)
        //        .Include(x => x.Contenedores)
        //            .ThenInclude(c => c.catPatios)
        //        .Include(x => x.Contenedores)
        //            .ThenInclude(c => c.Servicios)
        //                .ThenInclude(s => s.catReferenciaEstado)
        //        .Include(x => x.Contenedores)
        //            .ThenInclude(c => c.Servicios)
        //                .ThenInclude(s => s.catServicios)
        //        .AsQueryable();

        //    // Si el token corresponde a un cliente específico, agrego el filtro
        //    if (idCatCliente.HasValue)
        //    {
        //        query = query.Where(o => o.ordenes.IdCatCliente == idCatCliente.Value);
        //    }

        //    // Finalmente obtengo la referencia cuyo Id coincida
        //    var resultado = await query
        //        .FirstOrDefaultAsync(o => o.IdReferencia == IdReferencia);

        //    return resultado;
        //}


        #endregion GET

        #region EXISTE
        public bool ExisteReferenciaNAD(string referenciaNAD)
        {
            bool valor = _db.peticionesReferencias.Any(c => c.Contenedores.Any(x => x.RefenciaCliente == referenciaNAD));
            return valor;
        }
        public bool ExisteContenedorRefCliente(string pRefCliente, string contenedor)
        {
            bool respuesta = _db.peticionesReferencias.Include(c => c.Contenedores).Any(c => c.Contenedores.Any(x => x.Contenedor.Equals(contenedor.Trim()) && x.RefenciaCliente.Equals(pRefCliente)));
            return respuesta;
            //throw new NotImplementedException();
        }
        public bool ExisteContenedorOperando(string pReferenciaCliente, string pContenedor)
        {
            //bool respuesta = _db.peticionesReferencias
            //.Include(pr => pr.Contenedores)
            //.Any(pr =>
            //     // Verificamos que pr.IdCatReferenciaEstado NO sea 5 ni 6
            //     pr.IdCatReferenciaEstado != (int)EnumEstados.EstadosReferencias.Terminada &&
            //     pr.IdCatReferenciaEstado != (int)EnumEstados.EstadosReferencias.Cancelada &&

            //     // Entre los contenedores de esa referencia, buscamos alguno que:
            //     pr.Contenedores.Any(pc =>
            //         pc.Contenedor == pContenedor.Trim() &&
            //         // También pc.IdEstadoContenedor NO sea 5 ni 6
            //         pc.IdEstadoContenedor != (int)EnumEstados.EstadosReferencias.Terminada &&
            //         pc.IdEstadoContenedor != (int)EnumEstados.EstadosReferencias.Cancelada
            //     )
            //);

            bool respuesta =
                _db.peticionesReferencias.Include(c => c.Contenedores)
                                         .Any(c => c.Contenedores
                                         .Any(x => x.Contenedor.Equals(pContenedor.Trim())
                                          && x.RefenciaCliente.Equals(pReferenciaCliente)
                                          && (x.IdEstadoContenedor != (int)EnumEstados.EstadosReferencias.Terminada &&
                                              x.IdEstadoContenedor != (int)EnumEstados.EstadosReferencias.Cancelada
                                          )));

            return respuesta;
        }
        public bool ExisteTicket(int IdTicket)
        {

            //FALTA IDENTIFICAR DE QUE CLIENTE ES EL TICKET
            if (_db.peticionesReferencias.Where(c => c.Ticket.Equals(IdTicket)).ToList().Count > 0)
            { return true; }

            return false;
        }
        #endregion EXISTE

        #region ACTUALIZAR
        public List<string> ActualizarFolioContenedor(SolActualizarFolioManiobraDTO pSolActualizarFolioManiobraDTO)
        {
            #region Variables
            List<string> lstErrores = new List<string>();
            #endregion Variables

            #region Operacion
            string strError = "";
            //Obtenemos contenedor.

            if (ExisteContenedorActivo(idReferencia: pSolActualizarFolioManiobraDTO.IdReferencia, idContenedor: pSolActualizarFolioManiobraDTO.IdContenedor))
            {
                var contenedor = _db.peticionesContenedores.FirstOrDefault(x => x.IdReferencia == pSolActualizarFolioManiobraDTO.IdReferencia
                                       && x.IdContenedor == pSolActualizarFolioManiobraDTO.IdContenedor);
                if (contenedor.Activo)
                {


                    //if (objContenedor.IdEstadoContenedor!=5 && objContenedor.IdEstadoContenedor!=6)
                    //{
                    contenedor.FolioManiobra = pSolActualizarFolioManiobraDTO.FolioManiobra;
                    _db.peticionesContenedores.Update(contenedor);
                    if (Guardar(out strError))
                        return lstErrores;
                    else
                    {
                        lstErrores.Add($"Error al guardar el contenedor {contenedor.Contenedor}");
                        lstErrores.Add(strError);
                        return lstErrores;
                    }
                    //}
                    //else
                    //{
                    //    lstErrores.Add("El contenedor " + objContenedor.Contenedor + " ya se encuentra en operación");
                    //}
                }
                else
                {
                    lstErrores.Add("El contenedor está dado de baja");
                }
            }
            else
                lstErrores.Add("El contenedor " + pSolActualizarFolioManiobraDTO.IdContenedor + " No existe");
            return lstErrores;
            #endregion Operacion


        }
        public List<string> ActualizarContenedor(PeticionesContenedores pCont)
        {
            var lstErrores = new List<string>();
            string strError = "";

            try
            {
                //_db.Entry(pCont).State = EntityState.Modified;
                _db.Update(pCont);
                if (Guardar(out strError))
                    return lstErrores;
                else
                {
                    lstErrores.Add($"Error al actualizar el contenedor {pCont.Contenedor}");
                    lstErrores.Add(strError);
                    return lstErrores;
                }
            }
            catch (Exception ex)
            {
                lstErrores.Add($"Error al guardar el actualizar {pCont.Contenedor}");
                lstErrores.Add(ex.Message);
                return lstErrores;
            }
        }
        public async Task<RespuestaGenericaDTO> CambiarEstadoServicio(SolCambioEstadoDTO pSolCambioEstadoDTO)
        {
            var resp = new RespuestaGenericaDTO
            {
                IsSuccess = false,
                lstrErrorMessages = new List<string>()
            };

            try
            {
                //1).- VALIDAMOS QUE EXISTA EL ESTADO DENTRO DE NUESTRO CATÁLOGO CATREFERENCIAESTADO
                if (_db.catReferenciaEstado.Any(e => e.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado))
                {
                    // 2).- OBTENEMOS EL CONTENEDOR, SERVICIO, DOCUMENTOS
                    var cont = await _db.peticionesContenedores
                        //.Include(c => c.PeticionesReferencias).ThenInclude(r => r.ordenes)
                        .Include(c => c.Servicios).ThenInclude(s => s.Documentos)
                        //.Include(c => c.Servicios).ThenInclude(s => s.catServicios)
                        .FirstOrDefaultAsync(c => c.IdContenedor == pSolCambioEstadoDTO.IdContenedor && c.Activo);

                    if (cont != null)
                    {
                        //3).- VALIDAMOS QUE EL CONTENEDOR SEA APTO PARA EL CABIO DE ESTATUS
                        if (cont.IdEstadoContenedor != 5 || cont.IdEstadoContenedor != 6)
                        {
                            if (cont.IdEstadoContenedor != pSolCambioEstadoDTO.IdCatReferenciaEstado)
                            {
                                //4).- VERIFICAMOS QUE EL SERVICIO EXISTA DENTRO DEL OBJETO CONTENEDOR
                                if (cont.Servicios.Any(s => s.IdServicio == pSolCambioEstadoDTO.IdServicio))
                                {
                                    //5).- VALIDAMOS SI EL CAMBIO DE ESTATUS ES TERMINADO
                                    if (pSolCambioEstadoDTO.IdCatReferenciaEstado == 5)
                                    {
                                        //6).- EN CASO DE SER TERMINADO, VERIFICAMOS QUE EL SERVICIO CUENTE CON DOCUMENTOS, SALVO EN EL SERVICIO 3 (recuperación de garantías).
                                        if (!cont.Servicios.Any(s => s.IdServicio == pSolCambioEstadoDTO.IdServicio && s.IdTipoServicio == 3))
                                        {
                                            if (!cont.Servicios.Any(s => s.Documentos.Any(d => d.IdServicio == pSolCambioEstadoDTO.IdServicio)))
                                                resp.lstrErrorMessages.Add("No se puede cambiar el estado del servicio ya que no cuenta con un documento cargado.");
                                        }
                                    }
                                }
                                else
                                {
                                    resp.lstrErrorMessages.Add($"No existe el servicio para el contenedor {cont.Contenedor}.");
                                    resp.StatusCode = HttpStatusCode.NotFound;
                                }
                            }
                        }
                        else
                        {
                            resp.lstrErrorMessages.Add($"No se puede cambiar el estado del servicio debido a que el contenedor se encuentra 'TERMINADO' o 'CANCELADO'.");
                            resp.StatusCode = HttpStatusCode.Conflict;
                        }

                        //6).- VERIFICAMOS QUE NO EXISTAN ERRORES PARA PROCEDER CON EL CAMBIO DE ESTADO
                        if (resp.lstrErrorMessages.Count == 0)
                        {
                            // 7).- OBTENEMOS EL OBJETO DE SERVICIOS QUE VAMOS A ACTUALIZAR
                            var objServicio = _db.peticionesServicios.Where(x => x.IdServicio == pSolCambioEstadoDTO.IdServicio).FirstOrDefault();

                            //8).- REALIZAMOS EL CAMBIO DE ESTATUS PARA EL OBJETO SERVICIO
                            objServicio.IdEstadoServicio = pSolCambioEstadoDTO.IdCatReferenciaEstado;
                            objServicio.EstadoServicio = _db.catReferenciaEstado.FirstOrDefault(x => x.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado).Clave;
                            //_db.peticionesServicios.Update(objServicio);
                            _db.Entry(objServicio).Property(s => s.IdEstadoServicio).IsModified = true;
                            _db.Entry(objServicio).Property(s => s.EstadoServicio).IsModified = true;

                            //9).- VERIFICAMOS SI ES POSIBLE UN CIERRE DE CONTENEDOR-REFERENCIA-ORDEN
                            PropagarCierre(objServicio);

                            // 10).- GUARDAR TODO EN UN ÚNICO SAVECHANGES
                            await _db.SaveChangesAsync();

                            var objPeticionesContenedoresCron = new PeticionesContenedoresCron
                            {
                                IdCatTipoIncidenciaEvento = 2,
                                IdRegistroUsuario = 1,
                                FechaEvento = DateTime.Now,
                                FechaRegistro = DateTime.Now,
                                Comentarios = "Servicio terminado.",
                                IdContenedor = objServicio.IdContenedor,
                                IdServicio = objServicio.IdServicio
                            };

                            await _ctRepoContenedoresCron.agregarCronologiaporContenedor(objPeticionesContenedoresCron);

                            resp.StatusCode = HttpStatusCode.OK;
                            resp.IsSuccess = true;
                        }
                    }
                    else
                    {
                        resp.lstrErrorMessages.Add("No se encontró el contenedor indicado.");
                        resp.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    resp.lstrErrorMessages.Add("No se encontró el estado dentro de nuestro catálogo.");
                    resp.StatusCode = HttpStatusCode.NotFound;
                }
                return resp;
            }
            catch (Exception ex)
            {
                resp.lstrErrorMessages.Add("Surgio un error inesperado al realizar el cambio de estado.");
                return resp;
            }
        }
        #endregion ACTUALIZAR

        #region BORRAR
        public bool BorrarActivarReferencia(int idReferencia, bool status)
        {
            try
            {
                // Obtener la entidad existente
                var parentEntity = _db.peticionesReferencias.Find(idReferencia);

                if (parentEntity == null)
                {
                    throw new Exception("Entity not found");
                }

                // Actualizar solo el campo específico
                parentEntity.Activo = status;

                // Marcar solo el campo específico como modificado
                _db.Entry(parentEntity).Property(p => p.Activo).IsModified = true;

                // Guardar los cambios
                _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw new Exception("BorrarReferenciaTask Error en el proceso");

            }

        }
        public bool BorrarActivarContenedor(int idReferencia, int IdContenedor, bool status)
        {
            try
            {
                // Obtener la entidad existente
                if (ExisteContenedorActivo(idReferencia: idReferencia, idContenedor: IdContenedor))
                {
                    var parentEntity = _db.peticionesContenedores.Find(IdContenedor);

                    if (parentEntity == null)
                    {
                        return false;
                        //throw new Exception("Entity not found");
                    }

                    // Actualizar solo el campo específico
                    parentEntity.Activo = status;

                    // Marcar solo el campo específico como modificado
                    _db.Entry(parentEntity).Property(p => p.Activo).IsModified = true;

                    // Guardar los cambios
                    _db.SaveChangesAsync();
                    return true;

                }
                else { return false; }
            }
            catch (Exception)
            {
                throw new Exception("BorrarContenedorTask Entity not found");

                //return false;

            }

        }
        public bool BorrarActivarServicioContenedor(int idReferencia, int IdContenedor, int IdServicio, bool status)
        {
            try
            {
                // Obtener la entidad existente
                var parentEntity = _db.peticionesServicios.Find(IdServicio);

                if (parentEntity == null)
                {
                    throw new Exception("Entity not found");
                }

                // Actualizar solo el campo específico
                parentEntity.Activo = status;

                // Marcar solo el campo específico como modificado
                _db.Entry(parentEntity).Property(p => p.Activo).IsModified = true;

                // Guardar los cambios
                _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("BorrarServicioTask Entity not found");

                //return false;

            }

        }
        #endregion BORRAR

        public async Task<List<string>> IniciarProcesoOrden(List<int> ordenes)
        {
            var lstErrores = new List<string>();

            if (ordenes != null)
            {
                foreach (var idOrden in ordenes)
                {
                    if (ValidarOrdenDiferenteCanceladaTerminada(idOrden))
                    {
                        var objOrden = await GetOrdenActiva(idOrden: idOrden);
                        if (objOrden != null)
                        {
                            var respuestaIntegracion = await _integracion1GRepo.GenerarFacturacionDesdeOrdenesAsync(objOrden);
                            var respuestaAnticipo = await GenerarAnticipo(objOrden);
                            if (respuestaIntegracion.IsSuccess && respuestaIntegracion.StatusCode == HttpStatusCode.OK && respuestaIntegracion.lstrErrorMessages.Count() == 0)
                            {
                                if (!IniciarProcesoJerarquico(objOrden))
                                    lstErrores.Add($"No se pudo actualizar el estatus de la orden {objOrden.IdOrden.ToString()}.");
                                else
                                {
                                    var lstPeticionesContenedores = objOrden.peticionesReferencias.SelectMany(pr => pr.Contenedores).ToList();

                                    foreach (var cont in lstPeticionesContenedores)
                                    {
                                        foreach (var serv in cont.Servicios)
                                        {
                                            var objPeticionesContenedorCron = new PeticionesContenedoresCron
                                            {
                                                IdCatTipoIncidenciaEvento = 1,
                                                IdRegistroUsuario = 1,
                                                IdContenedor = cont.IdContenedor,
                                                Comentarios = "Inicio de operación.",
                                                FechaEvento = DateTime.Now,
                                                FechaRegistro = DateTime.Now,
                                                IdServicio = serv.IdServicio
                                            };

                                            await _ctRepoContenedoresCron.agregarCronologiaporContenedor(objPeticionesContenedorCron);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lstErrores.AddRange(respuestaIntegracion.lstrErrorMessages);
                            }
                        }
                    }
                    else
                        lstErrores.Add($"El número {idOrden} de orden no se encontró o ya se encuentra en proceso.");
                }
            }
            else
                lstErrores.Add("Debe seleccionar al menos una orden para iniciar su proceso.");

            return lstErrores;
        }

        public async Task<List<string>> CancelarOrden(List<int> ordenes)
        {
            var errores = new List<string>();

            foreach (var id in ordenes)
            {
                if (!ValidarOrdenDiferenteCanceladaTerminada(id))
                {
                    errores.Add($"No se puede cancelar la orden {id} ya que no se encuentra en estado pendiente.");
                    continue;
                }

                var orden = await GetOrdenActiva(idOrden: id);
                if (ExistenElementosNoPendientesEnOrden(orden))
                {
                    errores.Add($"No se puede cancelar la orden {id} por que tiene elementos en estatus distinto a Pendiente.");
                    continue;
                }

                if (!CancelacionJerarquica(orden))
                    errores.Add($"Error al cancelar la orden {id}.");
            }

            return errores;
        }
        public async Task<string?> CancelarElemento(string parametrosEncriptados)
        {
            if (string.IsNullOrWhiteSpace(parametrosEncriptados))
                return "No se proporcionaron parametros de entrada.";

            string textoPlano;
            try
            {
                textoPlano = AesEncryptionHelper.Decrypt(parametrosEncriptados);
            }
            catch
            {
                return "Error al descencriptar los parametros.";
            }

            if (string.IsNullOrWhiteSpace(textoPlano))
                return "No se proporcionaron parametros de entrada.";

            var parametros = textoPlano.Split('&', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(p => p.Split('=', 2))
                                       .Where(p => p.Length == 2)
                                       .ToDictionary(p => p[0], p => p[1]);

            if (!FiltroMapperHelper.TryObtenerFiltros(parametros, out FiltrosCancelacionElementoDTO filtrosCancelacion))
                return "Error al obtener los parametros proporcionados.";

            var orden = await GetOrdenActiva(idOrden: filtrosCancelacion.IdOrden);
            if (orden == null)
                return $"Orden {filtrosCancelacion.IdOrden} no encontrada.";

            var contenedor = orden.peticionesReferencias
                .SelectMany(r => r.Contenedores)
                .FirstOrDefault(c => c.IdContenedor == filtrosCancelacion.IdContenedor);

            if (contenedor == null)
                return $"Contenedor {filtrosCancelacion.IdContenedor} no pertenece a la orden.";

            if (filtrosCancelacion.IdServicio.HasValue)
            {
                // Lógica para cancelar servicio
                var servicio = contenedor.Servicios.FirstOrDefault(s => s.IdServicio == filtrosCancelacion.IdServicio.Value);
                if (servicio == null)
                    return $"Servicio no encontrado en contenedor {contenedor.Contenedor}.";

                if (!EsTodoPendiente(orden, contenedor, servicio))
                    return $"No se puede cancelar el número de servicio {filtrosCancelacion.IdServicio.Value} debido a que su estatus es distinto a Pendiente.";

                return CancelacionJerarquica(orden, filtrosCancelacion.IdContenedor, filtrosCancelacion.IdServicio)
                    ? null
                    : $"Error al cancelar el servicio {filtrosCancelacion.IdServicio.Value}.";
            }
            else
            {
                // Lógica para cancelar contenedor
                if (!EsTodoPendiente(orden, contenedor))
                    return $"Contenedor {filtrosCancelacion.IdContenedor} o su jerarquía no está completamente en estado Pendiente.";

                return CancelacionJerarquica(orden, filtrosCancelacion.IdContenedor)
                    ? null
                    : $"El contenedor {filtrosCancelacion.IdContenedor} no puede ser cancelado, favor de verificar o intente nuevamente.";
            }
        }

        #endregion METODOS PUBLICOS


        #region METODOS PRIVADOS
        #region CREAR
        /// <summary>
        /// Llena el objeto peticiones.
        /// Añade mensajes de error a la colección interna <c>lstStrErroresOut</c>:
        /// <list type="bullet">
        ///   <item>Si <paramref name="ticket"/> es <c>null</c>, añade un error indicando que falta el ticket.</item>
        ///   <item>Si el ticket ya existe en la base de datos, añade un error indicando duplicado.</item>
        /// </list>
        /// </summary>
        /// <param name="ticket">Número de ticket a validar.</param>
        private PeticionesReferencias LlenarObjetoReferencia(PeticionesReferenciasClienteExternoDTO peticionesReferenciasClienteExterno, out List<string> lstStrErroresOut)
        {
            //UtileriasCifrados clsCifrados = new UtileriasCifrados();
            //string strSalt = "";
            //string pass = clsCifrados.ComputeSha256HashWithSalt("Prueba1", out strSalt);
            //string salt = strSalt;
            #region VARIABLES
            var referencia = new PeticionesReferencias();
            referencia.Contenedores = new List<PeticionesContenedores>();
            ClsCatTransportitasRepositorio = new CatTransportitasRepositorio(_db);
            lstStrErroresOut = new List<string>();
            //string strError = string.Empty;
            var usuarioToken = GetUsuarioToken();
            #endregion VARIABLES

            #region INICIALIZACIÓN DE VARIABLES
            referencia.Ticket = peticionesReferenciasClienteExterno.Ticket == null ? null : (int)peticionesReferenciasClienteExterno.Ticket;
            referencia.Comentarios = peticionesReferenciasClienteExterno.Comentarios;
            referencia.TipoReferencia = 1;
            referencia.Procesado = "N";
            referencia.FechaCierre = null;
            referencia.Activo = true;
            //=========================================================================================================================
            // ################### VALIDAR LOS CAMPOS Y SU ESTATUS DEPENDIENDO DEL USUARIO QUE LO ESTE AGREGANDO ######################
            //referencia.EstadoReferencia = "PE";
            //referencia.IdCatReferenciaEstado = 7;
            //=========================================================================================================================
            #endregion INICIALIZACIÓN DE VARIABLES

            try
            {
                ////1).- OBTENEMOS LOS IdCatServicios QUE VENGAN DENTRO DE NUESTRA PETICIÓN
                //var distinctIdCatServicio = peticionesReferenciasClienteExterno.Contenedores
                //                            .Where(c => c.Servicios != null)
                //                            .SelectMany(c => c.Servicios)
                //                            .Select(s => s.IdCatServicio)
                //                            .Distinct()
                //                            .ToList();

                ////2).- PREGUNTAMOS SI DENTRO DE LA LISTA QUE NOS REGRESO, CONTIENE EL SERVICIO 1
                //if (distinctIdCatServicio.Contains(1))
                //{
                //    //2.1).- SE OBTIENE EL RFC DEL TRANSPORTISTA PARA INGRESARLO A NIVEL DE LA REFERENCIA
                //    var infoServicio = peticionesReferenciasClienteExterno.Contenedores
                //                       .Where(c => c.Servicios != null && c.Servicios.Any(s => s.IdCatServicio == 1))
                //                       .Select(c => new
                //                       {
                //                           TransporteRFC = c.Servicios.FirstOrDefault(s => s.IdCatServicio == 1)?.TransporteRFC,
                //                           TransporteNombreContacto = c.Servicios.FirstOrDefault(s => s.IdCatServicio == 1)?.TransporteNombreContacto,
                //                           TransporteEmailContacto = c.Servicios.FirstOrDefault(s => s.IdCatServicio == 1)?.TransporteEmailContacto
                //                       })
                //                       .FirstOrDefault();

                //    if (infoServicio != null)
                //    {
                //        //2.2).- OBTENEMOS LA INFORMACIÓN DEL TRANSPORTISTA Y LA INGRESAMOS EN LA REFERENCIA
                //        var objCatTransportistas = ClsCatTransportitasRepositorio.ObtenerPorRFC(infoServicio.TransporteRFC.Trim());
                //        if (objCatTransportistas != null)
                //        {
                //            referencia.TrasporteId = objCatTransportistas.IdCatTransportista;
                //            referencia.Transporte_RFC = objCatTransportistas.RFC;
                //            referencia.Transporte_RazonSocial = objCatTransportistas.RazonSocial;
                //            referencia.Transporte_Usuario = infoServicio.TransporteNombreContacto;
                //            referencia.Transporte_UsuarioEmail = infoServicio.TransporteEmailContacto;
                //        }
                //    }
                //}
                //else if (distinctIdCatServicio.Contains(2))
                //    referencia.TrasporteId = null;

                // 1).- Obtenemos el primer servicio con IdCatServicio == 1 (o null si no existe)
                var servicio = peticionesReferenciasClienteExterno
                    .Contenedores
                    .Where(c => c.Servicios != null)
                    .SelectMany(c => c.Servicios!)
                    .FirstOrDefault(s => s.IdCatServicio == 1);

                if (servicio != null && !string.IsNullOrWhiteSpace(servicio.TransporteRFC))
                {
                    var rfc = servicio.TransporteRFC.Trim();
                    var objCatTransportistas = ClsCatTransportitasRepositorio.ObtenerPorRFC(rfc);

                    if (objCatTransportistas != null)
                    {
                        referencia.TrasporteId = objCatTransportistas.IdCatTransportista;
                        referencia.Transporte_RFC = objCatTransportistas.RFC;
                        referencia.Transporte_RazonSocial = objCatTransportistas.RazonSocial;
                        referencia.Transporte_Usuario = servicio.TransporteNombreContacto;
                        referencia.Transporte_UsuarioEmail = servicio.TransporteEmailContacto;
                    }
                }
                else
                    referencia.TrasporteId = null;

                //3).- VALIDAMOS SI EL CONTENIDO DE LA LISTA DE CONTENEDORES ES MAYOR A 0
                if (peticionesReferenciasClienteExterno.Contenedores.Count > 0)
                {
                    //3.1).- OBTENEMOS LA INFORMACIÓN DE LA ADUANA
                    var objCatAduana = GetAduana(idCatAduana: peticionesReferenciasClienteExterno.IdCatAduana);
                    if (objCatAduana != null)
                    {
                        //3.2).- PROCEDEMOS A LLENAR EL OBJETO CONTENEDORES PARA AGREGARLO A LA LISTA DE CONTENEDORES DE LA REFERENCIA
                        foreach (var contenedor in peticionesReferenciasClienteExterno.Contenedores)
                        {
                            var objContenedor = new PeticionesContenedores();
                            objContenedor.AduanaId = objCatAduana.Aduana;
                            objContenedor.Aduana = objCatAduana.Nombre;

                            var lstStrErroresContenedorOut = LlenarObjetoContenedor(contenedor, objContenedor);

                            if (lstStrErroresContenedorOut.Count == 0)
                                referencia.Contenedores.Add(objContenedor);
                            else
                            {
                                lstStrErroresOut.AddRange(lstStrErroresContenedorOut);
                                referencia = null;
                            }
                        }
                    }
                    else
                        lstStrErroresOut.Add("La aduana que indico en su solicitud no se encuentra dentro de nuestro catálogo. Favor de revisar su solicitud.");
                }
                else
                    lstStrErroresOut.Add("Debe agregar al menos un contenedor");

                //if (lstStrErroresOut.Count() == 0)
                //{
                //    // 1. Se comprueba que TODOS los contenedores tengan información del patio
                //    bool todosConPatio = referencia.Contenedores
                //        .All(c =>
                //            c.PatioId.HasValue &&
                //            !string.IsNullOrWhiteSpace(c.Patio_RazonSocial) &&
                //            !string.IsNullOrWhiteSpace(c.Patio_RFC)
                //        );

                //    if (todosConPatio)
                //    {
                //        // 1) Cambiamos el estado de la referencia
                //        referencia.EstadoReferencia = "P";
                //        referencia.IdCatReferenciaEstado = 4;

                //        // 2) Cambiamos el estado para cada contenedor
                //        foreach (var cont in referencia.Contenedores)
                //        {
                //            cont.EstadoContenedor = "P";
                //            cont.IdEstadoContenedor = 4;

                //            // 3) Cambiamos el estado para cada servicio dentro del contenedor
                //            foreach (var serv in cont.Servicios)
                //            {
                //                serv.EstadoServicio = "P";
                //                serv.IdEstadoServicio = 4;
                //            }
                //        }
                //    }
                //}

                return referencia;
            }
            catch (Exception ex)
            {
                lstStrErroresOut.Add(ex.Message);
                return referencia = null;
            }
        }
        private List<string> LlenarObjetoContenedor(PeticionesContenedoresClienteExternoDTO pCont, PeticionesContenedores objContenedor)
        {
            #region VARIABLES
            ClsCatClientesRepositorio = new CatClientesRepositorio(_db);
            objContenedor.Servicios = new List<PeticionesServicios>();
            var lstStrErroresOut = new List<string>();
            #endregion VARIABLES

            #region SE INICIALIZAN LAS PROPIEDADES DEL OBJETO CONTENEDOR
            objContenedor.FechaRegistro = DateTime.Now;
            objContenedor.FechaCierre = null;
            objContenedor.IdCatTransportista = null;
            //objContenedor.EstadoContenedor = "PE";
            //objContenedor.IdEstadoContenedor = 7;
            objContenedor.Activo = true;
            #endregion SE INICIALIZAN LAS PROPIEDADES DEL OBJETO CONTENEDOR

            if (!string.IsNullOrWhiteSpace(pCont.Contenedor))
            {
                objContenedor.Contenedor = pCont.Contenedor;

                //1).- OBTENEMOS LOS DATOS DEL TIPO DE CONTENEDOR DE NUESTRO CATÁLOGO
                var objCatTipoContenedor = GetCatTipoContenedor(pCont.IdCatTipoContenedor, null);
                if (objCatTipoContenedor != null)
                {
                    objContenedor.IdCatTipoContenedor = objCatTipoContenedor.IdCatTipoContenedor;
                    objContenedor.ClaveTipoContenedor = objCatTipoContenedor.Nomenclatura;
                }

                //2).- VALIDAMOS SI EL CLIENTE EXISTE DENTRO DE NUESTRO CATALOGO DE CLIENTES.
                if (pCont.ClienteRFC.Trim().Length > 0 || pCont.ClienteRFC != null)
                {
                    //2.1).- OBTENEMOS LA INFORMACIÓN DEL CLIENTE
                    var objCatClientes = ClsCatClientesRepositorio.obtenerClienteRFC(pCont.ClienteRFC.Trim());
                    if (objCatClientes != null)
                    {
                        objContenedor.ClienteId = objContenedor.ClienteId == null || objContenedor.ClienteId == 0 ? objCatClientes.IdCatCliente : objContenedor.ClienteId;
                        objContenedor.Cliente_RFC = string.IsNullOrEmpty(objContenedor.Cliente_RFC) ? objCatClientes.RFC : objContenedor.Cliente_RFC;
                        objContenedor.Cliente_RazonSocial = string.IsNullOrEmpty(objContenedor.Cliente_RazonSocial) ? objCatClientes.RazonSocial : objContenedor.Cliente_RazonSocial;
                    }
                    else
                        lstStrErroresOut.Add($"El RFC {pCont.ClienteRFC} del Cliente no está registrado en nuestro catálogo.");
                }
                else
                    lstStrErroresOut.Add("Favor de proporcionar el RFC del cliente.");

                //3).- VALIDAMOS QUE VENGA EL NOMBRE DE EJECUTIVO SOLICITANTE
                if (string.IsNullOrWhiteSpace(pCont.NombreEjecutivoSolicitante))
                    objContenedor.Cliente_Solicitante = "";
                else
                    objContenedor.Cliente_Solicitante = pCont.NombreEjecutivoSolicitante;

                //4).- VALIDAMOS SI EL CONTENEDOR CUENTA CON REFERENCIA CLIENTE.
                if (!string.IsNullOrWhiteSpace(pCont.ReferenciaCliente))
                    objContenedor.RefenciaCliente = pCont.ReferenciaCliente;

                if (string.IsNullOrWhiteSpace(objContenedor.ReferenciaFacturacion))
                {
                    if (EsClienteNad(rfc: objContenedor.Cliente_RFC))
                    {
                        if (string.IsNullOrWhiteSpace(pCont.Servicios?.LastOrDefault()?.ReferenciaClienteFacturar))
                            lstStrErroresOut.Add("No se proporcionó la referencia cliente a facturar.");
                        else
                            objContenedor.ReferenciaFacturacion = pCont.Servicios?.LastOrDefault()?.ReferenciaClienteFacturar;
                    }
                    else
                        objContenedor.ReferenciaFacturacion = objContenedor.RefenciaCliente;
                }

                foreach (var servicio in pCont.Servicios.OrderBy(s => s.IdCatServicio))
                {
                    var objServicios = LlenarObjetoServicio(objContenedor, servicio, out var lstStrErroresServiciosOut);

                    if (lstStrErroresServiciosOut.Count > 0)
                        lstStrErroresOut.AddRange(lstStrErroresServiciosOut);
                    else
                        objContenedor.Servicios.Add(objServicios);
                }
            }
            else
                lstStrErroresOut.Add("No se proporciono un contenedor. Favor de revisar su solicitud.");

            return lstStrErroresOut;
        }
        private PeticionesServicios LlenarObjetoServicio(PeticionesContenedores objContenedor, PeticionesServiciosClienteExternoDTO servicio, out List<string> lstStrErrores)
        {
            var objServicio = new PeticionesServicios();
            ClsCatNavierasRepositorio = new CatNavierasRepositorio(_db);
            ClsCatClientesRepositorio = new CatClientesRepositorio(_db);
            ClsCatTransportitasRepositorio = new CatTransportitasRepositorio(_db);
            lstStrErrores = new List<string>();
            string strError = null;
            //objContenedor.IdCatTransportista = null;


            //lstStrErrores.AddRange(ValidacionDeCamposPorServicio(servicio));
            //if (lstStrErrores.Count > 0)
            //    return objServicio;

            #region VALIDACIONES PARA UN CONTENEDOR ACTIVO
            //1).- VALIDAMOS SI EL OBJETO CONTENEDOR CUENTA CON IDCONTENEDOR > 0 Y IDREFERENCIA > 0
            if (objContenedor.IdContenedor > 0 && objContenedor.IdReferencia > 0)
            {
                //2).- VALIDAMOS QUE LA REFERENCIA A LA QUE PERTENECE EL CONTENEDOR SE ENCUENTRE ACTIVA
                if (ExisteReferenciaActiva(objContenedor.IdReferencia))
                {
                    //3).- VALIDAMOS SI EL CONTENEDOR AL QUE LE QUEREMOS AGREGAR EL SERVICIO SE ENCUENTRA REGISTRADO EN NUESTRA BASE DE DATOS Y ESTA ACTIVO
                    if (ExisteContenedorActivo(idReferencia: objContenedor.IdReferencia, contenedor: objContenedor.Contenedor))
                    {

                        //4).- VALIDAMOS QUE NO SE AGREGUE NINGUN SERVICIO DUPLICADO AL CONTENEDOR.
                        // SERVICIOS > IDCATSERVICIO 4 PUEDEN REPETIRSE SIEMPRE Y CUANDO ESTOS SE ENCUENTREN EN UN ESTADO CANCELADO O TERMINADO.
                        if (ValidaAsignacionServicioContenedor(objContenedor.IdReferencia, objContenedor.IdContenedor, servicio.IdCatServicio))
                        {
                            //4.1).- SI TODO OK, ASIGNAMOS LOS VALORES AL OBJETO DE SERVICIO QUE RETORNAREMOS
                            objServicio.IdTipoServicio = servicio.IdCatServicio;
                            objServicio.IdContenedor = objContenedor.IdContenedor;
                            if (EsUsuarioInterno())
                            {
                                objServicio.EstadoServicio = objContenedor.EstadoContenedor;
                                objServicio.IdEstadoServicio = objContenedor.IdEstadoContenedor;
                            }
                            else
                            {
                                objServicio.EstadoServicio = "PE";
                                objServicio.IdEstadoServicio = 7;
                            }
                            objServicio.ReferenciaClienteFacturar = string.IsNullOrEmpty(servicio.ReferenciaClienteFacturar) ? objContenedor.RefenciaCliente : servicio.ReferenciaClienteFacturar;
                            objServicio.Activo = true;

                            var objCatCliente = GetDatosCliente(rfc: servicio.RFCFacturar);
                            if (objCatCliente != null)
                                objServicio.IdClienteFacturarA = objCatCliente.IdCatCliente;
                        }
                        else
                            lstStrErrores.Add($"Ya se encuentra asignado el servicio {servicio.IdCatServicio} en el contenedor {objContenedor.Contenedor} o es incompatible con otro servicio que ya se encuentra asignado en el contenedor.");
                    }
                    else
                        lstStrErrores.Add($"El contenedor {objContenedor.Contenedor} no corresponde al número de referencia {objContenedor.IdReferencia} o no existe. Revisa los datos e inténtalo de nuevo.");
                }
                else
                    lstStrErrores.Add($"El número de referencia {objContenedor.IdReferencia} no se encuentra activa. Verifica que la referencia sea correcta.");

                if (lstStrErrores.Count == 0)
                    return objServicio;

                return null;
            }
            #endregion VALIDACIONES PARA UN CONTENEDOR ACTIVO

            #region VALIDACIONES PARA AGREGAR UN SERVICIO A UN CONTENEDOR NUEVO
            switch (servicio.IdCatServicio)
            {
                //SERVICIO DE MANIOBRA
                case 1:

                    //Valida si el RFC del transportista existe.
                    if (!string.IsNullOrWhiteSpace(servicio.TransporteRFC))
                    {
                        var objCatTransportistas = ClsCatTransportitasRepositorio.ObtenerPorRFC(servicio.TransporteRFC.Trim());
                        if (objCatTransportistas != null)
                            objContenedor.IdCatTransportista = objCatTransportistas.IdCatTransportista;
                    }

                    //5.1).- VALIDAMOS LA RAZON SOCIAL DE LA NAVIERA
                    if (!string.IsNullOrWhiteSpace(servicio.NavieraRazonSocial))
                    {
                        var lstCatNavieras = ClsCatNavierasRepositorio.ObtenerPorRazonSocial(servicio.NavieraRazonSocial);
                        if (lstCatNavieras != null && lstCatNavieras.Count > 0)
                        {
                            //5.1.1).- LLENAMOS EL OBJETO CONTENEDOR CON LOS DATOS REGRESADOS DE LA BASE DE DATOS.
                            var objCatNaviera = lstCatNavieras.FirstOrDefault();
                            objContenedor.Naviera_Id = objCatNaviera.IdCatNaviera;
                            objContenedor.Naviera_RazonSocial = objCatNaviera.RazonSocial;
                        }
                        else
                            lstStrErrores.Add($"La razón social {servicio.NavieraRazonSocial} de la naviera, no se encuentra dentro de nuestro catálogo.");

                        objContenedor.BL = servicio.NumeroBL;
                        objContenedor.Buque = servicio.NombreBuque;
                        objContenedor.BuqueViaje = servicio.NumeroViaje;
                    }
                    else
                        lstStrErrores.Add($"No se proporcionó razón social de la naviera en el contenedor {objContenedor.Contenedor}.");

                    if (EsUsuarioInterno())
                    {
                        if (servicio.IdCatPatio > 0)
                        {
                            var objCatProveedorPatio = GetInformacionProveedorPatio(idCatPatio: servicio.IdCatPatio, aduanaId: objContenedor.AduanaId);
                            if (objCatProveedorPatio != null)
                            {
                                //6.1).- SE LLENAN LOS DATOS DEL PATIO EN EL CONTENEDOR
                                objContenedor.PatioId = objCatProveedorPatio.IdCatPatio;
                                objContenedor.Patio_RFC = objCatProveedorPatio.RFC;
                                objContenedor.Patio_RazonSocial = objCatProveedorPatio.RazonSocial;
                                objContenedor.FolioManiobra = servicio.FolioManiobra;
                            }
                            else
                                lstStrErrores.Add("El patio que indico en la solicitud no corresponde a la aduana indicada en la orden.");
                        }
                    }

                    break;
                //SERVICIO DE GESTIÓN DE EIR
                case 2:

                    if (servicio.IdCatPatio == 0)
                        lstStrErrores.Add($"No se asignó el patio al contenedor {objContenedor.Contenedor} para el servicio de recuperación de EIR.");
                    else
                    {
                        var objCatProveedorPatio = GetInformacionProveedorPatio(idCatPatio: servicio.IdCatPatio, aduanaId: objContenedor.AduanaId);
                        if (objCatProveedorPatio != null)
                        {
                            //6.1).- SE LLENAN LOS DATOS DEL PATIO EN EL CONTENEDOR
                            objContenedor.PatioId = objCatProveedorPatio.IdCatPatio;
                            objContenedor.Patio_RFC = objCatProveedorPatio.RFC;
                            objContenedor.Patio_RazonSocial = objCatProveedorPatio.RazonSocial;
                            objContenedor.FolioManiobra = servicio.FolioManiobra;
                        }
                        else
                            lstStrErrores.Add("El patio que indico en la solicitud no corresponde a la aduana indicada en la orden.");
                    }

                    break;
                    //SERVICIO DE RECUPERACIÓN DE GARANTIAS
                    //case 3:

                    //    break;
            }

            //6).- VALIDAMOS SI LA SOLICITUD YA VIENE CON PATIO INCLUIDO.
            if (!string.IsNullOrWhiteSpace(servicio.NombreConductor))
                objContenedor.NombreConductor = servicio.NombreConductor;

            if (!string.IsNullOrWhiteSpace(servicio.NumLicenciaConductor))
                objContenedor.LicenciaConductor = servicio.NumLicenciaConductor;

            if (!string.IsNullOrWhiteSpace(servicio.NumUnidad))
                objContenedor.NumeroUnidad = servicio.NumUnidad;

            if (!string.IsNullOrWhiteSpace(servicio.PlacaUnidad))
                objContenedor.PlacaUnidad = servicio.PlacaUnidad;

            //7).-VALIDAMOS SI EL CLIENTE EXISTE DENTRO DE NUESTRO CATALOGO DE CLIENTES.
            if (servicio.RFCFacturar.Trim().Length > 0 || servicio.RFCFacturar != null)
            {
                var objCatClientes = ClsCatClientesRepositorio.obtenerClienteRFC(servicio.RFCFacturar.Trim());
                if (objCatClientes != null)
                {
                    objServicio.ReferenciaClienteFacturar = string.IsNullOrWhiteSpace(servicio.ReferenciaClienteFacturar) ? objContenedor.RefenciaCliente : servicio.ReferenciaClienteFacturar;
                    objServicio.IdClienteFacturarA = objCatClientes.IdCatCliente;
                    objServicio.IdTipoServicio = servicio.IdCatServicio;
                    objServicio.DescServicio = null;
                    objServicio.FechaRegistro = DateTime.Now;
                    objServicio.FechaCierre = null;
                    objServicio.EstadoServicio = "PE";
                    objServicio.IdEstadoServicio = 7;
                    objServicio.Activo = true;
                }
                else
                    lstStrErrores.Add($"El RFC {servicio.RFCFacturar} del Cliente al que se va a facturar no está registrado.");
            }

            if (lstStrErrores.Count == 0)
                return objServicio;
            #endregion VALIDACIONES PARA AGREGAR UN SERVICIO A UN CONTENEDOR NUEVO

            return null;
        }

        private bool CrearReferencia(PeticionesReferencias peticionesReferencias, out List<string> lstError)
        {
            lstError = new List<string>();
            string strError = string.Empty;

            #region ASIGNAR VALORES DEFAULT
            peticionesReferencias.TipoReferencia = 1;
            peticionesReferencias.Procesado = "N";
            //peticionesReferencias.EstadoReferencia = "PE";
            //peticionesReferencias.IdCatReferenciaEstado = 7;
            peticionesReferencias.FechaProcesado = DateTime.Now;
            peticionesReferencias.FechaRegistro = DateTime.Now;
            peticionesReferencias.FechaCierre = null;
            peticionesReferencias.Activo = true;
            #endregion ASIGNAR VALORES DEFAULT

            _db.peticionesReferencias.Add(peticionesReferencias);
            if (Guardar(out strError))
                return true;
            else
            {
                lstError.Add(strError);
                return false;
            }
        }
        private async Task<List<string>> CrearContenedor(int IdReferencia, PeticionesContenedores contenedor)
        {
            #region Variables
            List<string> lstErrores = new List<string>();
            string strError = "";
            #endregion Variables

            #region Operacion
            if (ExisteReferenciaActiva(IdReferencia))
            {
                if (ExisteContenedorActivo(contenedor: contenedor.Contenedor))
                {
                    lstErrores.Add($"El contenedor {contenedor.Contenedor} ya se encuentra operando.");
                    return lstErrores;
                }
                else
                {
                    #region ASIGNAR VALORES
                    contenedor.FechaRegistro = DateTime.Now;
                    contenedor.FechaCierre = null;
                    contenedor.Activo = true;

                    if (EsUsuarioInterno())
                    {
                        var objReferencia = await GetReferencia(IdReferencia);

                        contenedor.EstadoContenedor = objReferencia.EstadoReferencia;
                        contenedor.IdEstadoContenedor = objReferencia.IdCatReferenciaEstado;

                        contenedor.Servicios.ToList()
                            .ForEach(s =>
                            {
                                s.EstadoServicio = objReferencia.EstadoReferencia;
                                s.IdEstadoServicio = objReferencia.IdCatReferenciaEstado;
                            });
                    }
                    else
                    {
                        contenedor.EstadoContenedor = "PE";
                        contenedor.IdEstadoContenedor = 7;
                    }
                    #endregion ASIGNAR VALORES

                    _db.peticionesContenedores.Add(contenedor);
                    if (Guardar(out strError))
                        return lstErrores;
                    else
                    {
                        lstErrores.Add($"Error al guardar el contenedor {contenedor.Contenedor}");
                        lstErrores.Add(strError);
                        return lstErrores;
                    }
                }

            }
            else
                lstErrores.Add($"Error al guardar el contenedor {contenedor.Contenedor} debido a que no existe el id de la referencia.");

            return lstErrores;
            #endregion Operacion
        }

        private async Task<List<string>> CrearDocumentos(PeticionesReferencias pRef, PeticionesReferenciasClienteExternoDTO pRefExterno)
        {
            _spfuncionRepo = new DbSPFuncionesRepositorio(_db);
            var lstStrErrores = new List<string>();
            var idOrden = pRef.IdOrden;

            foreach (var contDto in pRefExterno.Contenedores)
            {
                // Buscar el contenedor real (EF) usando el identificador
                var contenedorEF = pRef.Contenedores.FirstOrDefault(c => c.Contenedor == contDto.Contenedor);

                if (contenedorEF == null)
                {
                    lstStrErrores.Add($"No se encontró el contenedor '{contDto.Contenedor}' en la referencia.");
                    continue;
                }

                foreach (var servDto in contDto.Servicios)
                {
                    // Buscar el servicio real asociado al contenedor
                    var servicioEF = contenedorEF.Servicios.FirstOrDefault(s => s.IdTipoServicio == servDto.IdCatServicio);

                    if (servicioEF == null)
                    {
                        lstStrErrores.Add($"No se encontró el servicio '{servDto.IdCatServicio}' para el contenedor '{contDto.Contenedor}'.");
                        continue;
                    }

                    if (servicioEF.Documentos == null)
                        servicioEF.Documentos = new List<PeticionesDocumentos>();

                    // Verificamos si hay documentos asociados (como BL, EIR, etc.)
                    if (servDto.BL != null)
                    {
                        if (servDto.BL.IdTipoDocumento > 0)
                        {
                            var pathExpedienteBD = await _spfuncionRepo.ObtenerPathExpedienteAsync(idOrden, contenedorEF.IdContenedor, servicioEF.IdServicio, servDto.BL.IdTipoDocumento, 1);
                            if (pathExpedienteBD != null || !string.IsNullOrWhiteSpace(pathExpedienteBD))
                            {
                                var uuid = Guid.NewGuid().ToString();
                                var fileName = uuid + Path.GetExtension(servDto.BL.Nombre);

                                var docBL = new PeticionesDocumentos
                                {
                                    DocumentoUUID = uuid,
                                    Ubicacion = pathExpedienteBD + "\\" + fileName,
                                    IdTipoDocumento = servDto.BL.IdTipoDocumento,
                                    MimeType = servDto.BL.MimeType,
                                    NombreDocumento = fileName,
                                    IdContenedor = contenedorEF.IdContenedor,
                                    IdServicio = servicioEF.IdServicio,
                                    IdUsuarioRegistro = 1,
                                    Activo = true,
                                    FechaRegistro = DateTime.Now
                                };

                                _db.peticionesDocumentos.Add(docBL);
                                if (!Guardar(out var strError))
                                {
                                    if (strError != null)
                                        lstStrErrores.Add(strError);
                                }

                                if (!MoverArchivo(docBL, servDto.BL.Base64))
                                    lstStrErrores.Add($"Se presento un error en el guardado del documento BL para el contenedor {contenedorEF.Contenedor}");
                            }
                            else
                                lstStrErrores.Add($"Se presento un error al generar la ruta de guardado del documento BL para el contenedor {contenedorEF.Contenedor}");
                        }
                    }

                    if (servDto.EirDeLleno != null)
                    {
                        if (servDto.EirDeLleno.IdTipoDocumento > 0)
                        {
                            var pathExpedienteBD = await _spfuncionRepo.ObtenerPathExpedienteAsync(idOrden, contenedorEF.IdContenedor, servicioEF.IdServicio, servDto.EirDeLleno.IdTipoDocumento, 1);
                            if (pathExpedienteBD != null || !string.IsNullOrWhiteSpace(pathExpedienteBD))
                            {
                                var uuid = Guid.NewGuid().ToString();
                                var fileName = uuid + Path.GetExtension(servDto.EirDeLleno.Nombre);

                                var docEirDeLleno = new PeticionesDocumentos
                                {
                                    DocumentoUUID = uuid,
                                    Ubicacion = pathExpedienteBD + "\\" + fileName,
                                    IdTipoDocumento = servDto.EirDeLleno.IdTipoDocumento,
                                    MimeType = servDto.EirDeLleno.MimeType,
                                    NombreDocumento = fileName,
                                    IdContenedor = contenedorEF.IdContenedor,
                                    IdServicio = servicioEF.IdServicio,
                                    IdUsuarioRegistro = 1,
                                    Activo = true,
                                    FechaRegistro = DateTime.Now
                                };

                                _db.peticionesDocumentos.Add(docEirDeLleno);
                                if (!Guardar(out var strError))
                                {
                                    if (strError != null)
                                        lstStrErrores.Add(strError);
                                }

                                if (!MoverArchivo(docEirDeLleno, servDto.EirDeLleno.Base64))
                                    lstStrErrores.Add("Error al copiar el archivo EIR de lleno en el expediente.");
                            }
                            else
                                lstStrErrores.Add($"Se presento un error al generar la ruta de guardado del documento EIR de lleno para el contenedor {contenedorEF.Contenedor}");
                        }
                    }

                    if (servDto.SoporteNaviera != null)
                    {
                        if (servDto.SoporteNaviera.IdTipoDocumento > 0)
                        {
                            var pathExpedienteBD = await _spfuncionRepo.ObtenerPathExpedienteAsync(idOrden, contenedorEF.IdContenedor, servicioEF.IdServicio, servDto.SoporteNaviera.IdTipoDocumento, 1);
                            if (pathExpedienteBD != null || !string.IsNullOrWhiteSpace(pathExpedienteBD))
                            {
                                var uuid = Guid.NewGuid().ToString();
                                var fileName = uuid + Path.GetExtension(servDto.SoporteNaviera.Nombre);

                                var docSoporteNaviera = new PeticionesDocumentos
                                {
                                    DocumentoUUID = uuid,
                                    Ubicacion = pathExpedienteBD + "\\" + fileName,
                                    IdTipoDocumento = servDto.SoporteNaviera.IdTipoDocumento,
                                    MimeType = servDto.SoporteNaviera.MimeType,
                                    NombreDocumento = fileName,
                                    IdContenedor = contenedorEF.IdContenedor,
                                    IdServicio = servicioEF.IdServicio,
                                    IdUsuarioRegistro = 1,
                                    Activo = true,
                                    FechaRegistro = DateTime.Now
                                };

                                _db.peticionesDocumentos.Add(docSoporteNaviera);
                                if (!Guardar(out var strError))
                                {
                                    if (strError != null)
                                        lstStrErrores.Add(strError);
                                }

                                if (!MoverArchivo(docSoporteNaviera, servDto.SoporteNaviera.Base64))
                                    lstStrErrores.Add("Error al copiar el archivo correspondiente al soporte de la naviera en el expediente.");
                            }
                            else
                                lstStrErrores.Add($"Se presento un error al generar la ruta de guardado del documento correspondiente al soporte de la naviera para el contenedor {contenedorEF.Contenedor}");
                        }
                    }
                }
            }

            return lstStrErrores;
        }
        private bool MoverArchivo(PeticionesDocumentos pDoc, string base64)
        {
            try
            {
                // Convertir base64 a byte[]
                byte[] archivoBytes = ConvertirBase64ABytes(base64);

                // Construir la ruta completa (base + ubicación relativa)
                string strPathCompleto = Path.Combine(_pathBaseExpediente, pDoc.Ubicacion);

                // Extraer solo la ruta del directorio (sin incluir el archivo)
                string strSoloRuta = Path.GetDirectoryName(strPathCompleto);

                if (string.IsNullOrEmpty(strSoloRuta))
                    return false;

                // Crear el directorio si no existe
                if (!Directory.Exists(strSoloRuta))
                    Directory.CreateDirectory(strSoloRuta);

                // Guardar el archivo
                File.WriteAllBytes(strPathCompleto, archivoBytes);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static byte[] ConvertirBase64ABytes(string base64String)
        {
            var base64Data = base64String.Contains(",")
                ? base64String.Substring(base64String.IndexOf(",") + 1)
                : base64String;

            return Convert.FromBase64String(base64Data);
        }


        #endregion CREAR

        #region VALIDAR
        #region REFERENCIA
        private bool ValidarCamposObjetoReferenciaClienteExterno(PeticionesReferenciasClienteExternoDTO peticionesReferenciasClienteExternoDTO, Ordenes orden, out List<string> lstStrErroresOut)
        {
            #region VARIABLES
            var usuarioToken = GetUsuarioToken();
            lstStrErroresOut = new List<string>();
            #endregion VARIABLES

            //1).- EN CASO DE QUE EL USUARIO SEA INTERNO VALIDAMOS SI LA REFERENCIA CONTIENE EL ID DEL CLIENTE SOLICITANTE
            if (EsUsuarioInterno() && peticionesReferenciasClienteExternoDTO.IdCatClienteSolicitante == null)
            {
                lstStrErroresOut.Add("Por favor, especifique la empresa solicitante a la que se le asociará la orden.");
                return false;
            }

            //2).- OBTENEMOS LA INFORMACIÓN DEL CLIENTE SOLICITANTE DEL TOKEN O DE LA REFERENCIA SEGUN SEA EL CASO
            var idClienteSolicitante = usuarioToken.IdCatCliente == null ? (int)peticionesReferenciasClienteExternoDTO.IdCatClienteSolicitante : (int)usuarioToken.IdCatCliente;
            orden.IdCatCliente = idClienteSolicitante;


            // 3).- VALIDAMOS QUE LA ORDEN CONTENGA UNA ADUANA
            if (peticionesReferenciasClienteExternoDTO.IdCatAduana == 0)
                lstStrErroresOut.Add("Favor de indicar la aduana donde se creara la orden de servicio.");

            // 4).- VALIDAMOS QUE LA LISTA DE CONTENEDORES NO SEA NULL O QUE CONTENGA ALGO
            if (peticionesReferenciasClienteExternoDTO.Contenedores == null && !peticionesReferenciasClienteExternoDTO.Contenedores.Any())
                lstStrErroresOut.Add("Favor de agregar al menos un contenedor a la orden.");
            else
            {
                //4.1).- VALIDAMOS SI LA LISTA DE CONTENEDORES CUENTA CON CONTENEDORES REPETIDOS
                var duplicados = peticionesReferenciasClienteExternoDTO.Contenedores
                                                                                .Select(c => c.Contenedor)
                                                                                .GroupBy(id => id, StringComparer.OrdinalIgnoreCase)
                                                                                .Where(g => g.Count() > 1)
                                                                                .Select(g => g.Key)
                                                                                .ToList();
                if (duplicados.Any())
                    lstStrErroresOut.Add($"Se encontraron contenedores duplicados: {string.Join(", ", duplicados)}");
                else
                {
                    foreach (var pCont in peticionesReferenciasClienteExternoDTO.Contenedores)
                    {
                        //5).- VALIDAMOS SI EL CLIENTE SOLICITANTE ES CLIENTE NAD
                        if (EsClienteNad(idCatCliente: idClienteSolicitante))
                        {
                            if (!EsNestleOMarcasNestle(rfc: pCont.ClienteRFC))
                            {
                                //5.1).- VALIDAMOS SI EL CAMPO TICKET NO TIENE UN VALOR
                                if (!peticionesReferenciasClienteExternoDTO.Ticket.HasValue)
                                    lstStrErroresOut.Add("No se proporciono el número del ticket.");
                                else
                                {
                                    //5.2).- VALIDAMOS SI EL TICKET NO FUE REGISTRADO CON ANTERIORIDAD
                                    if (ExisteTicket((int)peticionesReferenciasClienteExternoDTO.Ticket))
                                        lstStrErroresOut.Add($"El número de ticket {peticionesReferenciasClienteExternoDTO.Ticket} ya se encuentra registrado.");
                                }
                            }
                        }

                        //VALIDAMOS LA INFORMACIÓN DE LOS CAMPOS DEL CONTENEDOR, SI EL METODO REGRESA FALSE, AGREGAMOS LOS ERRORES A LA LISTA DE ERRORES QUE YA TENEMOS
                        if (!ValidarCamposObjetoContenedorClienteExterno(pCont, orden, out var lstStrErroresContenedorOut))
                            lstStrErroresOut.AddRange(lstStrErroresContenedorOut);
                    }
                }
            }

            if (lstStrErroresOut.Count() == 0)
                return true;

            return false;
        }
        private bool ValidarObjetoPeticionesReferencia(PeticionesReferencias pRef, int idClienteSolicitante, out List<string> lstStrErrores)
        {
            lstStrErrores = new List<string>();

            if (EsClienteNad(idCatCliente: idClienteSolicitante))
            {
                var lstCatCliente = GetDatosNestleOMarcasNestle();

                if (pRef.Contenedores != null && pRef.Contenedores.Any())
                {
                    if (!pRef.Contenedores.Any(c => lstCatCliente.Any(n => n.RFC == c.Cliente_RFC)))
                    {
                        if (!pRef.Ticket.HasValue)
                            lstStrErrores.Add("La orden no cuenta con número de ticket.");

                        if (pRef.Contenedores.Any(c => c.Servicios != null && c.Servicios.Any(s => s.IdTipoServicio == 1)))
                        {
                            if (string.IsNullOrWhiteSpace(pRef.Transporte_RFC))
                                lstStrErrores.Add("La referencia no cuenta con el rfc de la linea transportista.");
                            if (string.IsNullOrWhiteSpace(pRef.Transporte_RazonSocial))
                                lstStrErrores.Add("La referencia no cuenta con la razón social de la linea transportista.");
                            if (!pRef.TrasporteId.HasValue)
                                lstStrErrores.Add("No se asignó la linea transportista a la referencia.");
                            if (string.IsNullOrWhiteSpace(pRef.Transporte_Usuario))
                                lstStrErrores.Add("No se proporcionó");
                        }
                    }
                }
            }
            else
            {
                if (pRef.Contenedores != null && pRef.Contenedores.Any(c => c.Servicios != null && c.Servicios.Any(s => s.IdTipoServicio == 1)))
                {
                    if (string.IsNullOrWhiteSpace(pRef.Transporte_RFC))
                        lstStrErrores.Add("La referencia no cuenta con el rfc de la linea transportista.");
                    if (string.IsNullOrWhiteSpace(pRef.Transporte_RazonSocial))
                        lstStrErrores.Add("La referencia no cuenta con la razón social de la linea transportista.");
                    if (!pRef.TrasporteId.HasValue)
                        lstStrErrores.Add("No se asignó la linea transportista a la referencia.");
                    if (string.IsNullOrWhiteSpace(pRef.Transporte_Usuario))
                        lstStrErrores.Add("No se proporcionó");
                }
            }

            if (pRef.Contenedores != null && pRef.Contenedores.Any())
            {
                foreach (var pCont in pRef.Contenedores)
                {
                    if (!ValidarObjetoPeticionesContenedores(pCont, idClienteSolicitante, out var lstStrErroresContOut))
                        lstStrErrores.AddRange(lstStrErroresContOut);
                }
            }

            if (lstStrErrores.Count == 0)
                return true;

            return false;
        }
        #endregion REFERENCIA

        #region CONTENEDOR
        private bool ValidarCamposObjetoContenedorClienteExterno(PeticionesContenedoresClienteExternoDTO pCont, Ordenes orden, out List<string> lstStrErroresOut)
        {
            #region VARIABLES
            var usuarioToken = GetUsuarioToken();
            lstStrErroresOut = new List<string>();
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s\.]+(\.[^@\s\.]+)*$");
            #endregion VARIABLES

            if (pCont.Contenedor.Trim().Length > 0 || pCont.Contenedor != null || !string.IsNullOrWhiteSpace(pCont.Contenedor))
            {
                // 1).- VALIDAMOS EL FORMATO DEL CONTENEDOR
                #region VALIDACIÓN FORMATO CONTENEDOR
                // 1.1).- QUITA TODO LO QUE NO SEA [A-Za-z0-9]
                //    (ESPACIOS, COMAS, GUIONES, ETC.)
                string alfanumerico = Regex.Replace(pCont.Contenedor, @"[^A-Za-z0-9]", "");

                // 1.2).- CONVIERTE EN MAYÚSCULAS
                pCont.Contenedor = alfanumerico.ToUpperInvariant();

                // 1.3).- VERIFICA QUE TENGA EXACTAMENTE 11 CARACTERES
                //    Y CUMPLA CON EL PATRÓN: 4 letras + 7 dígitos
                //    => ^[A-Z]{4}\d{7}$
                if (pCont.Contenedor.Length != 11)
                    lstStrErroresOut.Add("El formato del contenedor " + pCont.Contenedor + " no es valido.");
                else
                {
                    var regex = new Regex(@"^[A-Z]{4}\d{7}$");
                    if (!regex.IsMatch(pCont.Contenedor))
                        lstStrErroresOut.Add("El formato del contenedor " + pCont.Contenedor + " no es valido.");
                    else
                    {
                        // 1.4).- SEPARAMOS LA PARTE ALFA (4 chars) Y LA PARTE NÚMERICA (7 chars)
                        string parteAlfa = pCont.Contenedor.Substring(0, 4);   // Ej: "ABCD"
                        string parteNum = pCont.Contenedor.Substring(4, 7);   // Ej: "1234567"

                        // 1.5).- REGLA: LOS 4 DÍGITOS ALFABÉTICOS NO DEBEN SER TODOS IGUALES (p.e. "AAAA")
                        //    COMPROBAMOS SI TODAS LAS LETRAS SON IGUALES
                        if (parteAlfa.Distinct().Count() <= 2)
                            lstStrErroresOut.Add("El formato del contenedor " + pCont.Contenedor + " no es valido.");
                        else
                        {
                            // 1.6).- Regla: los 7 dígitos numéricos NO deben ser alguno de estos patrones
                            //    (ejemplos proporcionados):
                            //    "1111111", "1111222", "1112223", "1122334"
                            //    (puedes añadir otros si lo deseas)
                            var patronesProhibidos = GetPatronesContenedoresProhibidos();
                            if (patronesProhibidos.Contains(parteNum))
                                lstStrErroresOut.Add("El formato del contenedor " + pCont.Contenedor + " no es valido.");
                            else
                            {
                                //1.7).- VERIFICAMOS QUE EL CONTENEDOR NO SE ENCUENTRE ACTIVO EN OTRA ORDEN
                                if (ExisteContenedorActivo(contenedor: pCont.Contenedor))
                                    lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no se puede registrar debido a que ya se encuentra activo.");
                            }
                        }
                    }
                }
                #endregion VALIDACIÓN FORMATO CONTENEDOR

                //2).- VALIDAR QUE LA CLAVE Y TIPO DEL CONTENEDOR NO VENGAN VACIOS
                if (pCont.IdCatTipoContenedor == 0)
                    lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con el tipo de contenedor. Favor de indicar uno.");
                else
                {
                    //2.1).- VALIDAMOS QUE LA CLAVE Y TIPO DLE CONTENEDOR SE ENCUENTRE DENTRO DE NUESTRO CATALOGO
                    if (!ExisteClaveTipoContenedor(pCont.IdCatTipoContenedor))
                        lstStrErroresOut.Add("El tipo de contenedor proporcionado no se encuentra dentro de nuestro catálogo.");
                }

                //3).- VALIDAMOS QUE EL CONTENEDOR CUENTE CON EL RFC DEL CLIENTE
                if (string.IsNullOrWhiteSpace(pCont.ClienteRFC))
                    lstStrErroresOut.Add("Favor de indicar el RFC del cliente.");
                else
                {
                    //VALIDAMOS SI EL RFC DEL CLIENTE EXISTE
                    if (!ExisteClienteEnCatalogo(null, pCont.ClienteRFC))
                        lstStrErroresOut.Add($"Contenedor {pCont.Contenedor}: El RFC {pCont.ClienteRFC} correspondiente al cliente no se encuentra dentro de nuestro catálogo.");
                }

                //4).- VALIDAMOS SI ES UN USUARIO EXTERNO (CLIENTE O SISTEMA) INDIQUE EL NOMBRE DEL EJECUTIVO SOLICITANTE
                if (!EsUsuarioInterno())
                {
                    //4.1).- VALIDAMOS QUE EL CAMPO NOMBRE EJECUTIVO SOLICITANTE NO SEA NULL
                    if (string.IsNullOrWhiteSpace(pCont.NombreEjecutivoSolicitante))
                        lstStrErroresOut.Add("Favor de proporcionar el nombre del ejecutivo que esta solicitando el servicio.");
                }

                //5).- VALIDAMOS SI EL CLIENTE SOLICITANTE ES NAD
                if (EsClienteNad(idCatCliente: orden.IdCatCliente))
                {
                    //5.1).- VALIDAMOS QUE EL CONTENEDOR CONTENGA LA REFERENCIA CLIENTE (ESTA REFERENCIA SIRVE PARA QUE EL BOT CARGUE LOS DOCUMENTOS A NAD MEDIANTE EL SERVICIO WEB)
                    if (string.IsNullOrWhiteSpace(pCont.ReferenciaCliente))
                        lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con referencia cliente. Favor de agregarla.");
                }

                //6).- VALIDAMOS QUE EL OBJETO SERVICIO NO SEA NULL Y QUE CONTENGA ALGO
                if (pCont.Servicios == null && !pCont.Servicios.Any())
                    lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con servicios asignados.");
                else
                {
                    //6.1).- VALIDAR QUE EL CONTENEDOR NO TENGA SERVICIOS DUPLICADOS EN LA PETICIÓN
                    var lstIntServicios = pCont.Servicios.Select(x => x.IdCatServicio).ToList();
                    if (lstIntServicios.Distinct().Count() != pCont.Servicios.Count())
                        lstStrErroresOut.Add($"Existen servicios repetidos en el contenedor {pCont.Contenedor}.");
                    else
                    {
                        //6.2).- VERIFICAMOS QUE EL CONTENEDOR NO CUENTE CON EL SERVICIO DE MANIOBRA Y GESTIÓN DE EIR DENTRO DE LA MISMA SOLICITUD.
                        if (pCont.Servicios.Any(s => s.IdCatServicio == 1) && pCont.Servicios.Any(s => s.IdCatServicio == 2))
                            lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no puede tener el servicio de Maniobra de vacío y Gestión Electrónica de EIR.");
                        {
                            //VALIDAMOS QUE EL CONTENEDOR CONTENGA ALMENOS UN SERVICIO DE MANIOBRA(1) O UNA GESTIÓN DE EIR(2)
                            if (pCont.Servicios.Any(s => s.IdCatServicio == 1 || s.IdCatServicio == 2))
                            {
                                foreach (var servicio in pCont.Servicios.OrderBy(s => s.IdCatServicio))
                                {
                                    //6.3).- VALIDAMOS SI EL SERVICIO ES UNA MANIABRA DE VACIO
                                    if (servicio.IdCatServicio == 1)
                                    {
                                        //6.4).- VALIDAMOS SI EL CLIENTE SOLICITANTE ES CLIENTE NAD
                                        if (EsClienteNad(idCatCliente: orden.IdCatCliente))
                                        {
                                            //6.5).- VALIDAMOS SI EL CLIENTE ES DIFERENTE DE NESTLE O MARCAS NESTLE
                                            if (!EsNestleOMarcasNestle(rfc: pCont.ClienteRFC))
                                            {
                                                //6.6).- VALIDAMOS LOS SIGUIENTES CAMPOS
                                                if (string.IsNullOrWhiteSpace(servicio.TransporteRFC))
                                                    lstStrErroresOut.Add("Favor de proporcionar el RFC de la linea transportista.");
                                                else
                                                {
                                                    if (string.IsNullOrWhiteSpace(servicio.TransporteRazonSocial))
                                                        lstStrErroresOut.Add("Favor de proporcionar la razón social de la linea transportista.");
                                                    else
                                                    {
                                                        if (string.IsNullOrWhiteSpace(servicio.TransporteNombreContacto))
                                                            lstStrErroresOut.Add("Favor de proporcionar un nombre de contacto de la linea transportista.");
                                                        if (string.IsNullOrWhiteSpace(servicio.TransporteEmailContacto))
                                                            lstStrErroresOut.Add("Favor de agregar un email de contacto con la linea transportista.");
                                                        else if (!emailRegex.IsMatch(servicio.TransporteEmailContacto))
                                                            lstStrErroresOut.Add("El correo de la linea transportista no tiene un formato de correo válido.");
                                                        else
                                                        {
                                                            //6.7).- VALIDAMOS SI EXISTE LA LINEA TRANSPORTISTA. EN CASO DE QUE NO EXISTA, SE AGREGA A NUESTRO CATALOGO
                                                            if (!ExisteLineaTransportista(null, servicio.TransporteRFC))
                                                            {
                                                                var objCatTransportista = new CatTransportistas();

                                                                objCatTransportista.RazonSocial = servicio.TransporteRazonSocial;
                                                                objCatTransportista.RFC = servicio.TransporteRFC;
                                                                objCatTransportista.Correo = servicio.TransporteEmailContacto;
                                                                objCatTransportista.Activo = true;
                                                                objCatTransportista.IdCatPaises = 1;
                                                                objCatTransportista.IdCatPaisEstados = 1;
                                                                objCatTransportista.FechaRegistro = DateTime.Now;
                                                                objCatTransportista.IdUsuarioRegistro = 1;
                                                                objCatTransportista.IdCatEmpresas = 1;

                                                                _db.catTransportistas.Add(objCatTransportista);
                                                                if (!Guardar(out var strError))
                                                                    lstStrErroresOut.Add(strError);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (EsUsuarioInterno())
                                        {
                                            if ((EsClienteNad(idCatCliente: orden.IdCatCliente) && !EsNestleOMarcasNestle(rfc: pCont.ClienteRFC)) || !EsClienteNad(idCatCliente: orden.IdCatCliente))
                                            {
                                                //6.8).- VALIDAMOS CAMPOS PARA UN USUARIO INTERNO
                                                if (string.IsNullOrWhiteSpace(servicio.TransporteRFC))
                                                    lstStrErroresOut.Add("Favor de proporcionar el RFC de la linea transportista.");
                                                else
                                                {
                                                    if (string.IsNullOrWhiteSpace(servicio.TransporteRazonSocial))
                                                        lstStrErroresOut.Add("Favor de proporcionar la razón social de la linea transportista.");
                                                    else
                                                    {
                                                        //if (string.IsNullOrWhiteSpace(servicio.TransporteNombreContacto))
                                                        //    lstStrErroresOut.Add("Favor de proporcionar un nombre de contacto de la linea transportista.");
                                                        if (string.IsNullOrWhiteSpace(servicio.TransporteEmailContacto))
                                                            lstStrErroresOut.Add("Favor de agregar un email de contacto con la linea transportista.");
                                                        else if (!emailRegex.IsMatch(servicio.TransporteEmailContacto))
                                                            lstStrErroresOut.Add("El correo de la linea transportista no tiene un formato de correo válido.");
                                                    }

                                                    //6.9).- VALIDAMOS SI EXISTE LA LINEA TRANSPORTISTA. EN CASO DE QUE NO EXISTA, SE AGREGA A NUESTRO CATALOGO
                                                    if (!ExisteLineaTransportista(null, servicio.TransporteRFC))
                                                    {
                                                        var objCatTransportista = new CatTransportistas();

                                                        objCatTransportista.RazonSocial = servicio.TransporteRazonSocial;
                                                        objCatTransportista.RFC = servicio.TransporteRFC;
                                                        objCatTransportista.Correo = servicio.TransporteEmailContacto;
                                                        objCatTransportista.Activo = true;
                                                        objCatTransportista.IdCatPaises = 1;
                                                        objCatTransportista.IdCatPaisEstados = 1;
                                                        objCatTransportista.FechaRegistro = DateTime.Now;
                                                        objCatTransportista.IdUsuarioRegistro = 1;
                                                        objCatTransportista.IdCatEmpresas = 1;

                                                        _db.catTransportistas.Add(objCatTransportista);
                                                        if (!Guardar(out var strError))
                                                            lstStrErroresOut.Add(strError);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //6.10).- VALIDAMOS LOS SIGUIENTES CAMPOS PARA UN CLIENTE DIFERENTE A NAD
                                            if (string.IsNullOrWhiteSpace(servicio.TransporteRFC))
                                                lstStrErroresOut.Add("Favor de proporcionar el RFC de la linea transportista.");
                                            if (string.IsNullOrWhiteSpace(servicio.TransporteNombreContacto))
                                                lstStrErroresOut.Add("Favor de proporcionar un nombre de contacto de la linea transportista.");
                                            if (string.IsNullOrWhiteSpace(servicio.TransporteEmailContacto))
                                                lstStrErroresOut.Add("Favor de agregar un email de contacto con la linea transportista.");
                                            else if (!emailRegex.IsMatch(servicio.TransporteEmailContacto))
                                                lstStrErroresOut.Add("El correo de la linea transportista no tiene un formato de correo válido.");
                                        }
                                    }

                                    if (!ValidarCamposObjetoServicioClienteExterno(servicio, orden, out var lstStrErroresServicio))
                                        lstStrErroresOut.AddRange(lstStrErroresServicio);
                                }
                            }
                            else
                                lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} debe tener al menos un servicio de maniobra o una gestión de eir.");
                        }

                    }
                }
            }
            else
                lstStrErroresOut.Add("No se proporciono un número de contenedor valido.");

            if (lstStrErroresOut.Count() == 0)
                return true;

            return false;
        }
        private bool ValidarObjetoPeticionesContenedores(PeticionesContenedores pCont, int idCatClienteSolicitante, out List<string> lstStrErroresOut)
        {
            lstStrErroresOut = new List<string>();

            if (string.IsNullOrWhiteSpace(pCont.Contenedor))
                lstStrErroresOut.Add("No se proporciono un contenedor.");
            else
            {
                if (pCont.IdCatTipoContenedor == 0)
                    lstStrErroresOut.Add("No se proporcionó el tipo de contenedor.");
                if (string.IsNullOrWhiteSpace(pCont.ClaveTipoContenedor))
                    lstStrErroresOut.Add("No se proporcionó la nomenclatura del contenedor.");
                if (string.IsNullOrWhiteSpace(pCont.RefenciaCliente))
                    lstStrErroresOut.Add($"No se asignó la referencia cliente o referencia ALO al contenedor {pCont.Contenedor}");
                if (!pCont.ClienteId.HasValue)
                    lstStrErroresOut.Add($"No se asocio el cliente al contenedor {pCont.Contenedor}.");
                if (string.IsNullOrWhiteSpace(pCont.Cliente_RazonSocial))
                    lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con la razón social del cliente.");
                if (string.IsNullOrWhiteSpace(pCont.Cliente_RFC))
                    lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con el rfc del cliente.");
                if (!EsUsuarioInterno())
                {
                    if (string.IsNullOrWhiteSpace(pCont.Cliente_Solicitante))
                        lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con el nombre del ejeutivo solicitante.");
                }
                if (!pCont.AduanaId.HasValue)
                    lstStrErroresOut.Add($"No se asocio la aduana al contenedor {pCont.Contenedor}.");
                if (string.IsNullOrWhiteSpace(pCont.Aduana))
                    lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con la aduana.");
                if (string.IsNullOrWhiteSpace(pCont.ReferenciaFacturacion))
                    lstStrErroresOut.Add($"No se proporciono referencia de facturación para el contenedor {pCont.Contenedor}.");
                if (EsUsuarioInterno())
                {
                    if (!pCont.PatioId.HasValue)
                        lstStrErroresOut.Add($"No se proporcionó el patio para el contenedor {pCont.Contenedor}.");
                    if (string.IsNullOrWhiteSpace(pCont.Patio_RazonSocial))
                        lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con la razón social del patio.");
                    if (string.IsNullOrWhiteSpace(pCont.Patio_RFC))
                        lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con el rfc del patio.");
                }

                if (pCont.Servicios != null && pCont.Servicios.Any())
                {
                    if (pCont.Servicios.Any(s => s.IdTipoServicio == 1))
                    {
                        if (string.IsNullOrWhiteSpace(pCont.BL))
                            lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con número de BL.");
                        if (string.IsNullOrWhiteSpace(pCont.Buque))
                            lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con el número de buque.");
                        if (string.IsNullOrWhiteSpace(pCont.BuqueViaje))
                            lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con número de viaje.");
                        //if (string.IsNullOrWhiteSpace(pCont.Naviera_RFC))
                        //    lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con el rfc de la naviera");
                        if (pCont.Naviera_Id <= 0)
                            lstStrErroresOut.Add($"No se asignó la naviera al contenedor {pCont.Contenedor}.");
                        if (string.IsNullOrWhiteSpace(pCont.Naviera_RazonSocial))
                            lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con la razón social de la naviera");
                        if (!pCont.Naviera_Id.HasValue)
                            lstStrErroresOut.Add($"No se asocio la naviera al contenedor {pCont.Contenedor}.");
                        if (EsClienteNad(idCatCliente: idCatClienteSolicitante))
                        {
                            if (!EsNestleOMarcasNestle(rfc: pCont.Cliente_RFC))
                            {
                                if (!pCont.IdCatTransportista.HasValue)
                                    lstStrErroresOut.Add($"No se asocio el transportista al contenedor {pCont.Contenedor}.");
                            }
                        }
                        else
                        {
                            if (!pCont.IdCatTransportista.HasValue)
                                lstStrErroresOut.Add($"No se asocio el transportista al contenedor {pCont.Contenedor}.");
                        }
                    }

                    if (!EsUsuarioInterno() && pCont.Servicios.Any(s => s.IdTipoServicio == 2))
                    {
                        if (!pCont.PatioId.HasValue)
                            lstStrErroresOut.Add($"No se proporcionó el patio para el contenedor {pCont.Contenedor}.");
                        if (string.IsNullOrWhiteSpace(pCont.Patio_RazonSocial))
                            lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con la razón social del patio.");
                        if (string.IsNullOrWhiteSpace(pCont.Patio_RFC))
                            lstStrErroresOut.Add($"El contenedor {pCont.Contenedor} no cuenta con el rfc del patio.");
                    }

                    foreach (var servicio in pCont.Servicios)
                    {
                        if (!ValidarObjetoPeticionesServicios(servicio, out var lstStrErroresServicioOut))
                            lstStrErroresOut.AddRange(lstStrErroresServicioOut);
                    }
                }
            }

            if (lstStrErroresOut.Count == 0)
                return true;

            return false;
        }
        #endregion CONTENEDOR

        #region SERVICIO
        private bool ValidarCamposObjetoServicioClienteExterno(PeticionesServiciosClienteExternoDTO servicio, Ordenes orden, out List<string> lstStrErroresOut)
        {
            #region VARIABLES
            lstStrErroresOut = new List<string>();

            #endregion VARIABLES

            //1).- VALIDAMOS QUE EL SERVICIO ENVIADO EN LA SOLICITUD SEA MAYOR A 0
            if (servicio.IdCatServicio > 0)
            {
                // 2).- VALIDAMOS QUE EL SERVICIO SE ENCUENTRE DENTRO DE NUESTRO CATÁLOGO
                if (ExisteServicioEnCatalogoServicio(servicio.IdCatServicio))
                {
                    // 3).- VALIDAMOS LA INFORMACIÓN DE LOS CAMPOS SEGÚN EL SERVICIO
                    switch (servicio.IdCatServicio)
                    {
                        case 1:
                            // 3.1).- VALIDAMOS LOS CAMPOS PARA EL SERVICIO DE MANIOBRA
                            if (string.IsNullOrWhiteSpace(servicio.NumeroBL))
                                lstStrErroresOut.Add("Favor de proporcionar el número de bl.");
                            if (string.IsNullOrWhiteSpace(servicio.NavieraRazonSocial))
                                lstStrErroresOut.Add("Favor de proporcionar la razón social de la naviera.");
                            //if (string.IsNullOrWhiteSpace(servicio.NavieraRFC))
                            //    lstStrErroresOut.Add("Favor de proporcionar el rfc de la naviera.");
                            //else
                            //{
                            //    //3.2).- VALIDAMOS SI ES CLIENTE NAD O UN USUARIO INTERNO
                            //    if (EsClienteNad(idCatCliente: orden.IdCatCliente) || EsUsuarioInterno())
                            //    {
                            //        //3.2.1).- VALIDAMOS EL CAMPO DE LA RAZÓN SOCIAL DE LA NAVIERA
                            //        if (string.IsNullOrWhiteSpace(servicio.NavieraRazonSocial))
                            //            lstStrErroresOut.Add("Favor de proporcionar la razón social de la naviera.");
                            //        else
                            //        {
                            //            //3.2.2).- VALIDAMOS SI LA NAVIERA EXISTE DENTRO DE NUESTRO CATÁLOGO, EN CASO DE QUE NO EXISTA LO AGREGAMOS
                            //            if (!ExisteNaviera(null, servicio.NavieraRFC))
                            //            {
                            //                var objCatNaviera = new CatNavieras();

                            //                objCatNaviera.RazonSocial = servicio.NavieraRazonSocial;
                            //                objCatNaviera.RFC = servicio.NavieraRFC;
                            //                objCatNaviera.Activo = true;
                            //                objCatNaviera.FechaRegistro = DateTime.Now;

                            //                _db.catNavieras.Add(objCatNaviera);
                            //                if (!Guardar(out var strError))
                            //                    lstStrErroresOut.Add(strError);
                            //            }
                            //        }
                            //    }

                            //}
                            if (string.IsNullOrWhiteSpace(servicio.NombreBuque))
                                lstStrErroresOut.Add("Favor de proporcionar el nombre del buque.");
                            if (string.IsNullOrEmpty(servicio.NumeroViaje))
                                lstStrErroresOut.Add("Favor de proporcionar el número de viaje.");
                            if (!EsUsuarioInterno() && !EsClienteNad(idCatCliente: orden.IdCatCliente))
                            {
                                if (servicio.BL == null)
                                    lstStrErroresOut.Add("Favor de cargar el documento BL.");
                                else if (servicio.BL.IdTipoDocumento != 9)
                                    lstStrErroresOut.Add("Favor de seleccionar el tipo de documento correcto para el BL.");
                            }

                            if (EsUsuarioInterno())
                            {
                                if (servicio.IdCatPatio == 0)
                                    lstStrErroresOut.Add("Favor de agregar el patio al contenedor.");
                            }
                            break;

                        case 2:
                            // 4).- VALIDAMOS CAMPOS PARA EL SERVICIO DE GESTIÓN DE RECUPERACIÓN DE EIR.
                            if (servicio.IdCatPatio.Equals(0))
                                lstStrErroresOut.Add("Favor de indicar el patio donde se realizará la recuperación del EIR.");
                            else
                            {
                                // 4.1).- VALIDAMOS SI EL PATIO INDICADO CORRESPONDE A LA ADUANA INDICADA EN LA ORDEN
                                if (!_db.catProveedoresPatios.Any(pp => pp.IdCatPatio == servicio.IdCatPatio &&
                                                                   pp.IdCatAduana == orden.IdCatAduana &&
                                                                   pp.Activo
                                    )
                                )
                                {
                                    lstStrErroresOut.Add("Favor de verificar su solicitud ya que el patio no corresponde a la aduana seleccionada en la orden.");
                                }
                            }
                            break;

                        case 3:
                            // 3).- CAMPOS A VALIDAR PARA EL SERVICIO DE RECUPERACIÓN DE GARANTIAS
                            // (EN ESTE MOMENTO NO SE CUENTA CON LOS CAMPOS NECESARIOS POR LO QUE SE PODRÁ AGREGAR EL SERVICIO SIN VALIDACIONES)

                            /*
                            if (string.IsNullOrWhiteSpace(servicio.ConsignatarioRFC))
                                errores.Add("El campo ConsignatarioRFC es obligatorio para el servicio de Recuperación de Garantías.");
                            if (string.IsNullOrWhiteSpace(servicio.ConsignatarioRazonSocial))
                                errores.Add("El campo ConsignatarioRazonSocial es obligatorio para el servicio de Recuperación de Garantías.");
                            if (string.IsNullOrWhiteSpace(servicio.NombreInstitucionBancaria))
                                errores.Add("El campo NombreInstitucionBancaria es obligatorio para el servicio de Recuperación de Garantías.");
                            */
                            break;
                        case 4:
                            //4).- CAMPOS A VALIDAR PARA EL SERVICIO DE CORTE DE DEMORAS
                            // (EN ESTE MOMENTO NO SE CUENTA CON LOS CAMPOS NECESARIOS POR LO QUE SE PODRÁ AGREGAR EL SERVICIO SIN VALIDACIONES)

                            break;
                    }
                }
                else
                    lstStrErroresOut.Add("El servicio ingresado no se encuentra dentro de nuestro catálogo o no cuenta con los permisos para su asignación.");
            }
            else
                lstStrErroresOut.Add("No se recibió ningún servicio para agregar. Por favor envía al menos un servicio válido.");

            if (lstStrErroresOut.Count() == 0)
                return true;

            return false;
        }
        private bool ValidarObjetoPeticionesServicios(PeticionesServicios pServ, out List<string> lstStrErrores)
        {
            lstStrErrores = new List<string>();

            if (pServ.IdTipoServicio == 0)
                lstStrErrores.Add("No se asocio ningun servicio.");
            if (string.IsNullOrWhiteSpace(pServ.ReferenciaClienteFacturar))
                lstStrErrores.Add("No se proporcionó una referencia de facturación para el servicio.");
            if (!pServ.IdClienteFacturarA.HasValue)
                lstStrErrores.Add("No se proporcionó el cliente de facturación para el servicio.");

            if (lstStrErrores.Count == 0)
                return true;

            return false;
        }
        #endregion SERVICIO
        private bool EsClienteNad(int? idCatCliente = null, string? rfc = null)
        {
            var objCatClienteNad = GetDatosClienteNad();
            var objCatClienteSolicitante = GetDatosCliente(idCatCliente, rfc);

            if (objCatClienteNad.RFC.Equals(objCatClienteSolicitante.RFC))
                return true;

            return false;
        }

        private bool EsNestleOMarcasNestle(int? idCatCliente = null, string? rfc = null)
        {
            var lstObjCatClienteNestle = GetDatosNestleOMarcasNestle();
            var objCatCliente = GetDatosCliente(idCatCliente, rfc);

            if (objCatCliente != null)
                return lstObjCatClienteNestle.Any(c => string.Equals(c.RFC.Trim(), objCatCliente.RFC, StringComparison.OrdinalIgnoreCase));
            else
                return false;
        }
        private bool EsUsuarioInterno()
        {
            var usuarioToken = GetUsuarioToken();

            if (!usuarioToken.IdCatCliente.HasValue &&
                 (usuarioToken.RolesUsuario.Any(u =>
                                                   u.Equals("ADMIN", StringComparison.OrdinalIgnoreCase) ||
                                                   u.Equals("ADMINUSER", StringComparison.OrdinalIgnoreCase))
                    || ("ALogistics".Equals(usuarioToken.User, StringComparison.OrdinalIgnoreCase) && usuarioToken.RolesUsuario.Contains("SISTEMA"))
                 )
               )
            {
                return true;
            }
            return false;
        }
        private bool ValidarOrdenDiferenteCanceladaTerminada(int idOrden)
        {
            var usuarioToken = GetUsuarioToken();

            return _db.ordenes.Any(o =>
                                      o.IdOrden.Equals(idOrden)
                                      && o.IdEstadoOrden != 3
                                      && o.IdEstadoOrden != 4
                                      && (usuarioToken.IdCatCliente == null || o.IdCatCliente == usuarioToken.IdCatCliente)
                                  );
        }
        private bool ValidaAsignacionServicioContenedor(int IdReferencia, int IdContenedor, int idCatServicio)
        {
            // 1) Obtener los servicios activos (no cancelados) que ya tiene el contenedor.
            var serviciosActuales = _db.peticionesReferencias
                .Where(pr => pr.IdReferencia == IdReferencia)
                .SelectMany(pr => pr.Contenedores) // Navegación a contenedores
                .Where(pc => pc.IdContenedor == IdContenedor)
                .SelectMany(pc => pc.Servicios)    // Navegación a servicios
                .Where(ps => ps.IdEstadoServicio != 6
                             && ps.EstadoServicio != "C"
                             && ps.Activo
                )
                .Select(ps => ps.IdTipoServicio)       // Obtenemos solo el ID del servicio
                .ToList();

            // 2) Regla: Servicios 1 y 2 no se pueden mezclar.
            //    - Si quiero agregar el servicio 1, pero ya existe el 2 (y viceversa), regresa false.
            if (idCatServicio == 1 && serviciosActuales.Contains(2))
                return false;
            if (idCatServicio == 2 && serviciosActuales.Contains(1))
                return false;

            // 3) Regla: Si el contenedor ya tiene activo el servicio 1, 2, 3 o 4, 
            //    no se puede volver a agregar el mismo (a menos que esté cancelado, 
            //    pero aquí filtramos " != 6 " para no considerarlo).
            //    => si "serviciosActuales" ya contiene idServicio (y este es 1, 2, 3 ó 4), no se permite.
            if ((idCatServicio == 1 || idCatServicio == 2 || idCatServicio == 3 || idCatServicio == 4)
                && serviciosActuales.Contains(idCatServicio))
                return false;

            // 4) Validar que el contenedor tenga previamente cargado el servicio 1 o 2
            // Si no tiene el 1 ni el 2, no se puede agregar ningún servicio
            if (!serviciosActuales.Contains(1) && !serviciosActuales.Contains(2))
            {
                // Solo se permite agregar el 1 o 2 como primer servicio
                if (idCatServicio != 1 && idCatServicio != 2)
                    return false;
            }

            // 5) Obtener los servicios con IdTipoServicio > 4 y 
            //    que el estado sea diferente a cancelado (5) o terminado (6)
            var serviciosActuales2 = _db.peticionesReferencias
                .Where(pr => pr.IdReferencia == IdReferencia)
                .SelectMany(pr => pr.Contenedores)
                .Where(pc => pc.IdContenedor == IdContenedor)
                .SelectMany(pc => pc.Servicios)
                .Where(ps => ps.IdEstadoServicio != 5    // != cancelado
                          && ps.IdEstadoServicio != 6    // != terminado
                          && ps.EstadoServicio != "C"
                          && ps.EstadoServicio != "T"
                          && ps.IdTipoServicio > 4
                          && ps.Activo
                )
                .Select(ps => ps.IdTipoServicio)
                .ToList();

            //6) Validar que los servicios que pueden repetirse no se encuentren activos
            if (serviciosActuales2.Contains(idCatServicio))
                return false;

            // 7) Si no viola ninguna regla, se puede agregar el servicio
            return true;

        }


        #endregion VALIDAR

        #region GET
        private CatClientes GetDatosClienteNad()
        {
            return _db.catClientes.Where(c => c.RFC == "NGL0712111M2" && c.Activo).FirstOrDefault();
        }
        private List<CatClientes> GetDatosNestleOMarcasNestle()
        {
            return _db.catClientes.Where(c => c.RFC == "NME980506LPA" ||
                                              c.RFC == "MNE0409226K9" &&
                                              c.Activo
                                        ).ToList();
        }
        private CatClientes GetDatosCliente(int? idCatCliente = null, string? rfc = null)
        {
            return _db.catClientes
                        .Where(cc =>
                                    (idCatCliente == null || cc.IdCatCliente == (int)idCatCliente) &&
                                    (rfc == null || cc.RFC == rfc) &&
                                    cc.Activo
                        )
                        .FirstOrDefault();
        }
        private CatTipoContenedor GetCatTipoContenedor(int? idCatTipoContenedor, string? nomenclatura)
        {
            return _db.catTiposContenedor.Where(c =>
                                                    (idCatTipoContenedor == null || c.IdCatTipoContenedor == idCatTipoContenedor) &&
                                                    (nomenclatura == null || c.Nomenclatura == nomenclatura) &&
                                                    c.Activo
                                               ).FirstOrDefault();
        }


        private string[] GetPatronesContenedoresProhibidos()
        {
            string[] patronesProhibidos =
            {
                "0123456","1234567","2345678","3456789","4567890","0987654","9876543","8765432","7654321",
                "0000000","1111111","2222222","3333333","4444444","5555555","6666666","7777777","8888888",
                "9999999","1222222","1112223","1122334"
            };

            return patronesProhibidos;
        }

        private CatProveedoresPatiosDTO GetInformacionProveedorPatio(int? idCatPatio = null, string? proveedorRFC = null, int? idCatAduana = null, int? aduanaId = null)
        {
            return _db.catProveedoresPatios
                        .Include(cp => cp.catPatios)
                        .Include(cp => cp.catProveedores)
                        .Include(a => a.catAduana)
                        .Where(cpp =>
                                    (proveedorRFC == null || cpp.catProveedores.RFC.Equals(proveedorRFC)) &&
                                    (idCatPatio == null || cpp.IdCatPatio.Equals(idCatPatio)) &&
                                    (idCatAduana == null || cpp.catAduana.IdCatAduana.Equals(idCatAduana)) &&
                                    (aduanaId == null || cpp.catAduana.Aduana.Equals(aduanaId)) &&
                                    cpp.Activo &&
                                    cpp.catPatios.Activo &&
                                    cpp.catProveedores.Activo &&
                                    cpp.catAduana.Activo
                                   )
                        .Select(prov => new CatProveedoresPatiosDTO
                        {
                            RFC = prov.catProveedores.RFC,
                            RazonSocial = prov.catProveedores.RazonSocial,
                            IdCatPatio = prov.catPatios.IdCatPatios
                        })
                        .FirstOrDefault();
        }
        private CatAduana GetAduana(int? idCatAduana = null, int? aduana = null, string? nombre = null, string? acronimo = null)
        {
            return _db.catAduana.
                Where(a =>
                           (idCatAduana == null || a.IdCatAduana == idCatAduana) &&
                           (aduana == null || a.Aduana == aduana) &&
                           (nombre == null || a.Nombre == nombre) &&
                           (acronimo == null || a.Acronimo == acronimo) &&
                           a.Activo
                )
                .FirstOrDefault();
        }
        /// <summary>
        /// Recupera una orden completa (con referencias, contenedores y servicios) 
        /// filtrando por IdOrden o por IdReferencia, y aplicando el filtro de cliente del token.
        /// </summary>
        /// <param name="idOrden">Id de la orden (opcional).</param>
        /// <param name="idReferencia">Id de la referencia (opcional).</param>
        /// <returns>
        /// La entidad <see cref="Ordenes"/> que cumpla con el filtro, o <c>null</c> si no existe.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Se lanza si ambos parámetros son nulos o vacíos.
        /// </exception>
        private async Task<Ordenes?> GetOrdenActiva(int? idOrden = null, int? idReferencia = null)
        {
            // Debe venir al menos uno de los dos filtros
            if (idOrden.HasValue || idReferencia.HasValue)
            {
                // Obtener contexto de cliente
                var usuarioToken = GetUsuarioToken();

                // Base de la consulta con todos los Include necesarios
                var query = _db.ordenes
                    .Include(o => o.peticionesReferencias)
                        .ThenInclude(r => r.Contenedores)
                            .ThenInclude(c => c.Servicios)
                    .AsQueryable();

                // SI ES UN USUARIO CLIENTE, FILTRAREMOS LAS ORDENES QUE LE CORRESPONDEN
                if (usuarioToken.IdCatCliente.HasValue)
                {
                    query = query.Where(o => o.IdCatCliente == usuarioToken.IdCatCliente.Value);
                }

                // Aplica el filtro según valores en parametros (IdOrden o IdReferencia)
                if (idOrden.HasValue)
                {
                    query = query.Where(o => o.IdOrden == idOrden.Value
                                            && o.IdEstadoOrden != 3
                                            && o.IdEstadoOrden != 4
                                            && o.Activo
                    );
                }

                if (idReferencia.HasValue)
                {
                    query = query.Where(o =>
                        o.peticionesReferencias.Any(pr => pr.IdReferencia == idReferencia.Value
                                                       && pr.EstadoReferencia != "T"
                                                       && pr.EstadoReferencia != "C"
                                                       && pr.IdCatReferenciaEstado != 5
                                                       && pr.IdCatReferenciaEstado != 6
                                                       && pr.Activo
                        ));
                }

                return await query.FirstOrDefaultAsync();
            }

            //Retorna null si ninguno de los parametros cuenta con valor
            return null;
        }

        #endregion GET

        #region Existe

        #region REFERENCIA
        private bool ExisteReferenciaActiva(int IdReferencia)
        {
            var usuarioToken = GetUsuarioToken();

            return _db.peticionesReferencias
                        .Include(pr => pr.ordenes)
                        .Any(pr =>
                            pr.IdReferencia == IdReferencia
                            && pr.IdCatReferenciaEstado != 5
                            && pr.IdCatReferenciaEstado != 6
                            && pr.EstadoReferencia != "C"
                            && pr.EstadoReferencia != "T"
                            && pr.Activo
                            && (usuarioToken.IdCatCliente == null || pr.ordenes.IdCatCliente == usuarioToken.IdCatCliente)
                        );
        }
        #endregion REFERENCIA
        #region CONTENEDOR
        /// <summary>
        /// Verifica si existe un contenedor que cumpla con los criterios opcionales de búsqueda
        /// (IdReferencia, IdContenedor o Nombre de contenedor) y además esté activo y en estado válido.
        /// </summary>
        /// <param name="idReferencia">Filtro opcional por Id de referencia.</param>
        /// <param name="idContenedor">Filtro opcional por Id de contenedor.</param>
        /// <param name="contenedorNombre">Filtro opcional por nombre de contenedor.</param>
        /// <returns>True si existe al menos un contenedor que cumpla con todos los criterios y esté activo.</returns>
        private bool ExisteContenedorActivo(int? idReferencia = null, int? idContenedor = null, string? contenedor = null)
        {
            // 1).- SE CONSTRUYE CONSULTA BASE
            var query = _db.peticionesContenedores
                .Include(c => c.PeticionesReferencias)
                    .ThenInclude(r => r.ordenes)
                .AsQueryable();

            //2).- SE APLICA FILTROS DE BUSQUEDA SOLO SI SE PROPORCIONA DATOS EN LOS PARAMETROS
            if (idReferencia.HasValue)
                query = query.Where(c => c.IdReferencia == idReferencia.Value);
            if (idContenedor.HasValue)
                query = query.Where(c => c.IdContenedor == idContenedor.Value);
            if (!string.IsNullOrWhiteSpace(contenedor))
                query = query.Where(c =>
                    c.Contenedor.Equals(contenedor));

            //3).- SE AGREGA FILTRO DE ACTIVO Y ESTADOS DIFERENTE A CANCELADO O TERMINADO
            query = query.Where(c =>
                c.EstadoContenedor != "T" &&
                c.EstadoContenedor != "C" &&
                c.IdEstadoContenedor != 5 &&
                c.IdEstadoContenedor != 6 &&
                c.PeticionesReferencias.EstadoReferencia != "T" &&
                c.PeticionesReferencias.EstadoReferencia != "C" &&
                c.PeticionesReferencias.IdCatReferenciaEstado != 5 &&
                c.PeticionesReferencias.IdCatReferenciaEstado != 6 &&
                c.Activo &&
                c.PeticionesReferencias.Activo
            );

            // RETORNA TRUE EN CASO DE QUE EXISTA ALGUNO QUE CUMPLA CON LOS CRITERIOS
            // RETORNA FALSE EN CASO DE QUE NO EXISTA NINGUNO QUE CUMPLA CON LOS CRITERIOS
            return query.Any();
        }
        private bool ExisteClaveTipoContenedor(int idCatTipoContenedor)
        {
            return _db.catTiposContenedor.Any(tc => tc.IdCatTipoContenedor == idCatTipoContenedor && tc.Activo);
        }
        #endregion CONTENEDOR
        #region SERVICIO
        /// <summary>
        /// Verifica si un IdCatServicio dado está en el catálogo de servicios activos,
        /// aplicando restricciones adicionales si el usuario es externo.
        /// </summary>
        /// <param name="idCatServicio">El identificador del servicio a validar.</param>
        /// <returns>
        /// True si:
        ///  - El servicio existe en catServicios y está activo,  
        ///  - Y si el usuario es externo, su IdCatServicio está en la lista [1,2,3,4].  
        /// </returns>
        private bool ExisteServicioEnCatalogoServicio(int idCatServicio)
        {
            // PARA CLIENTE EXTERNO SOLO PERMITIMOS ESTOS CUATRO SERVICIOS:
            // 1 = Maniobra de vacío, 2 = Gestión de EIR, 3 = Recuperación de garantías, 4 = Corte de demoras
            IEnumerable<int>? serviciosExternosPermitidos = null;
            if (!EsUsuarioInterno())
                serviciosExternosPermitidos = new[] { 1, 2, 3, 4 };

            // VALIDAMOS QUE EL SERVICIO SE ENCUENTRE DENTRO DE NUESTRO CATALOGO
            return _db.catServicios
                .Where(s => s.Activo
                            && (serviciosExternosPermitidos == null
                                || serviciosExternosPermitidos.Contains(s.IdCatServicio)))
                .Any(s => s.IdCatServicio == idCatServicio);
        }

        private bool ExisteServicioContenedorEnPeticionesServicios(int IdReferencia, int IdContenedor, int IdServicio)
        {
            bool respuesta = _db.peticionesReferencias.Include(c => c.Contenedores).ThenInclude(c => c.Servicios).Any(c => c.IdReferencia == IdReferencia && c.Contenedores.Any(x => x.IdContenedor == IdContenedor && x.Servicios.Any(s => s.IdServicio == IdServicio)));
            return respuesta;
        }


        #endregion SERVICIO


        private bool ExisteClienteEnCatalogo(int? idCatCliente, string? rfc)
        {
            return _db.catClientes
                    .Any(cc =>
                            (idCatCliente == null || cc.IdCatCliente == (int)idCatCliente) &&
                            (rfc == null || cc.RFC == rfc) &&
                            cc.Activo
                    );
        }
        private bool ExisteLineaTransportista(int? idCatTransportista, string? rfc)
        {
            return _db.catTransportistas.Any(t =>
                                               (idCatTransportista == null || t.IdCatTransportista == idCatTransportista) &&
                                               (rfc == null || t.RFC == rfc) &&
                                               t.Activo
                                            );
        }
        private bool ExisteNaviera(int? idCatNaviera, string? rfc)
        {
            return _db.catNavieras.Any(n =>
                                        (idCatNaviera == null || n.IdCatNaviera == idCatNaviera) &&
                                        (rfc == null || n.RFC == rfc) &&
                                        n.Activo
            );
        }
        private bool ExistenElementosNoPendientesEnOrden(Ordenes orden) =>
            orden.peticionesReferencias.Any(r =>
                r.EstadoReferencia != "PE" &&
                r.IdCatReferenciaEstado != 7 ||
                r.Contenedores.Any(c =>
                ((c.EstadoContenedor != "PE" && c.IdEstadoContenedor != 7) && (c.EstadoContenedor != "C" && c.IdEstadoContenedor != 6)) ||
                    c.Servicios.Any(s =>
                                    (s.EstadoServicio != "PE" && s.IdEstadoServicio != 7) &&
                                    (s.EstadoServicio != "C" && s.IdEstadoServicio != 6)
                    )
                )
        );
        #endregion Existe

        #region ACTUALIZAR
        /// <summary>
        /// Si todos los servicios del contenedor están terminados → cierra contenedor;
        /// si todos los contenedores de la referencia están terminados → cierra referencia;
        /// si todas las referencias de la orden están terminadas → cierra orden.
        /// </summary>
        private void PropagarCierre(PeticionesServicios servicio)
        {
            int ID_EST_CANCEL_REF_CONT_SERV = _db.catReferenciaEstado.First(e => e.Nombre == "CANCELADO").IdCatReferenciaEstado;
            int ID_EST_TERM_REF_CONT_SERV = _db.catReferenciaEstado.First(e => e.Nombre == "TERMINADO").IdCatReferenciaEstado;
            string EST_TERM_REF_CONT_SERV = _db.catReferenciaEstado.First(e => e.Nombre == "TERMINADO").Clave;
            int ID_EST_TERM_ORD = _db.catTipoEstados.First(e => e.Nombre == "TERMINADO").IdCatTipoEstados;

            // 1).- CONTENEDOR PADRE
            var cont = _db.peticionesContenedores.Include(c => c.Servicios).First(c => c.IdContenedor == servicio.IdContenedor);
            // 1.1).- VALIDAMOS SI TODOS LOS SERVICIOS DEL CONTENEDOR SE ENCUENTRAN EN ESTADO TERMINADO
            if (cont.Servicios.All(s => s.IdEstadoServicio == ID_EST_TERM_REF_CONT_SERV || s.IdEstadoServicio == ID_EST_CANCEL_REF_CONT_SERV))
            {
                cont.IdEstadoContenedor = ID_EST_TERM_REF_CONT_SERV;
                cont.EstadoContenedor = EST_TERM_REF_CONT_SERV;
                _db.Entry(cont).Property(c => c.IdEstadoContenedor).IsModified = true;
                _db.Entry(cont).Property(c => c.EstadoContenedor).IsModified = true;
            }

            // 2) REFERENCIA PADRE
            // 2.1) RECARGAR la REFERENCIA **con TODOS sus contenedores y servicios**
            var pref = _db.peticionesReferencias.Include(r => r.Contenedores).ThenInclude(c => c.Servicios).First(r => r.IdReferencia == cont.IdReferencia);
            // 2.2).- VALIDAMOS SI TODOS LOS CONTENEDORES DE LA REFERENCIA SE ENCUENTRAN EN ESTADO TERMINADO
            if (pref.Contenedores.All(c => c.IdEstadoContenedor == ID_EST_TERM_REF_CONT_SERV || c.IdEstadoContenedor == ID_EST_CANCEL_REF_CONT_SERV))
            {
                pref.IdCatReferenciaEstado = ID_EST_TERM_REF_CONT_SERV;
                pref.EstadoReferencia = EST_TERM_REF_CONT_SERV;
                _db.Entry(pref).Property(p => p.IdCatReferenciaEstado).IsModified = true;
                _db.Entry(pref).Property(p => p.EstadoReferencia).IsModified = true;
            }

            // 3).- ORDEN PADRE
            var ord = _db.ordenes.Include(o => o.peticionesReferencias).ThenInclude(r => r.Contenedores).ThenInclude(c => c.Servicios).First(o => o.IdOrden == pref.IdOrden);
            // 3.1).- VALIDAMOS SI TODAS LAS REFERENCIAS DE LA ORDEN SE ENCUENTRAN EN ESTADO TERMINADO
            if (ord.peticionesReferencias.All(r => r.IdCatReferenciaEstado == ID_EST_TERM_REF_CONT_SERV || r.IdCatReferenciaEstado == ID_EST_CANCEL_REF_CONT_SERV))
            {
                ord.IdEstadoOrden = ID_EST_TERM_ORD;
                _db.Entry(ord).Property(o => o.IdEstadoOrden).IsModified = true;
            }
        }
        #endregion ACTUALIZAR

        #region VARIOS
        private bool EsTodoPendiente(Ordenes orden, PeticionesContenedores contenedor, PeticionesServicios? servicio = null)
        {
            return orden.IdEstadoOrden == 5 &&
                   contenedor.EstadoContenedor == "PE" &&
                   contenedor.IdEstadoContenedor == 7 &&
                   (servicio == null || servicio.EstadoServicio == "PE") &&
                   (servicio == null || servicio.IdEstadoServicio == 7);
        }
        private bool IniciarProcesoJerarquico(Ordenes orden)
        {
            var estadoOrden = _db.catTipoEstados.FirstOrDefault(e => e.TipoEstado == "P");
            var estadoRefContServ = _db.catReferenciaEstado.FirstOrDefault(e => e.Clave == "P");

            if (estadoOrden == null || estadoRefContServ == null)
                return false;

            // --- Cambiar estado de la orden si está en "PE" ---
            if (orden.IdEstadoOrden == _db.catTipoEstados.FirstOrDefault(e => e.TipoEstado == "PE")?.IdCatTipoEstados)
            {
                orden.IdEstadoOrden = estadoOrden.IdCatTipoEstados;
                _db.Entry(orden).Property(o => o.IdEstadoOrden).IsModified = true;
            }

            foreach (var referencia in orden.peticionesReferencias)
            {
                // --- Cambiar estado de referencia si está en "PE" ---
                if (referencia.EstadoReferencia == "PE")
                {
                    referencia.IdCatReferenciaEstado = estadoRefContServ.IdCatReferenciaEstado;
                    referencia.EstadoReferencia = estadoRefContServ.Clave;
                    _db.Entry(referencia).Property(r => r.IdCatReferenciaEstado).IsModified = true;
                    _db.Entry(referencia).Property(r => r.EstadoReferencia).IsModified = true;
                }

                foreach (var contenedor in referencia.Contenedores)
                {
                    // --- Cambiar estado de contenedor si está en "PE" ---
                    if (contenedor.EstadoContenedor == "PE")
                    {
                        contenedor.IdEstadoContenedor = estadoRefContServ.IdCatReferenciaEstado;
                        contenedor.EstadoContenedor = estadoRefContServ.Clave;
                        _db.Entry(contenedor).Property(c => c.IdEstadoContenedor).IsModified = true;
                        _db.Entry(contenedor).Property(c => c.EstadoContenedor).IsModified = true;
                    }

                    foreach (var servicio in contenedor.Servicios)
                    {
                        // --- Cambiar estado de servicio si está en "PE" ---
                        if (servicio.EstadoServicio == "PE")
                        {
                            servicio.IdEstadoServicio = estadoRefContServ.IdCatReferenciaEstado;
                            servicio.EstadoServicio = estadoRefContServ.Clave;
                            _db.Entry(servicio).Property(s => s.IdEstadoServicio).IsModified = true;
                            _db.Entry(servicio).Property(s => s.EstadoServicio).IsModified = true;
                        }
                    }
                }
            }

            return Guardar(out var _);
        }
        private bool CancelacionJerarquica(Ordenes orden, int? idContenedor = null, int? idServicio = null)
        {
            var estadoOrden = _db.catTipoEstados.FirstOrDefault(e => e.TipoEstado == "C");
            var estadoRefContServ = _db.catReferenciaEstado.FirstOrDefault(e => e.Clave == "C");

            if (estadoOrden == null || estadoRefContServ == null)
                return false;

            bool huboCambios = false;

            foreach (var referencia in orden.peticionesReferencias)
            {
                foreach (var contenedor in referencia.Contenedores)
                {
                    // Solo si nos mandan idContenedor, validar que sea el contenedor correcto
                    if (idContenedor.HasValue && contenedor.IdContenedor != idContenedor.Value)
                        continue;

                    foreach (var servicio in contenedor.Servicios)
                    {
                        if (idServicio.HasValue)
                        {
                            // Cancelar solo un servicio específico
                            if (servicio.IdServicio == idServicio.Value && servicio.EstadoServicio == "PE")
                            {
                                servicio.IdEstadoServicio = estadoRefContServ.IdCatReferenciaEstado;
                                servicio.EstadoServicio = estadoRefContServ.Clave;
                                _db.Entry(servicio).State = EntityState.Modified;
                                huboCambios = true;
                            }
                        }
                        else
                        {
                            // Cancelar todos los servicios "PE" (si idServicio == null)
                            if (servicio.EstadoServicio == "PE")
                            {
                                servicio.IdEstadoServicio = estadoRefContServ.IdCatReferenciaEstado;
                                servicio.EstadoServicio = estadoRefContServ.Clave;
                                _db.Entry(servicio).State = EntityState.Modified;
                                huboCambios = true;
                            }
                        }
                    }

                    // Cancelar contenedor si todos sus servicios están en estado "C"
                    if (contenedor.Servicios.All(s => s.EstadoServicio == "C") && contenedor.EstadoContenedor == "PE")
                    {
                        contenedor.IdEstadoContenedor = estadoRefContServ.IdCatReferenciaEstado;
                        contenedor.EstadoContenedor = estadoRefContServ.Clave;
                        _db.Entry(contenedor).State = EntityState.Modified;
                        huboCambios = true;
                    }
                }

                // Cancelar referencia si todos sus contenedores están en estado "C"
                if (referencia.Contenedores.All(c => c.EstadoContenedor == "C") && referencia.EstadoReferencia == "PE")
                {
                    referencia.IdCatReferenciaEstado = estadoRefContServ.IdCatReferenciaEstado;
                    referencia.EstadoReferencia = estadoRefContServ.Clave;
                    _db.Entry(referencia).State = EntityState.Modified;
                    huboCambios = true;
                }
            }

            // Cancelar orden si todas sus referencias están en estado "C"
            if (orden.peticionesReferencias.All(r => r.EstadoReferencia == "C"))
            {
                if (_db.catTipoEstados.FirstOrDefault(e => e.TipoEstado == "PE")?.IdCatTipoEstados == orden.IdEstadoOrden)
                {
                    orden.IdEstadoOrden = estadoOrden.IdCatTipoEstados;
                    _db.Entry(orden).State = EntityState.Modified;
                    huboCambios = true;
                }
            }

            // Cancelar toda la jerarquía si NO se mandó idContenedor ni idServicio
            if (!idContenedor.HasValue && !idServicio.HasValue)
            {
                // Revalidar si podemos cancelar absolutamente todo
                bool todoEsCancelable = orden.peticionesReferencias
                    .All(r => r.Contenedores
                        .All(c => c.Servicios.All(s => s.EstadoServicio == "PE" || s.EstadoServicio == "C")));

                if (todoEsCancelable)
                {
                    // Cancelar todos los servicios "PE"
                    foreach (var referencia in orden.peticionesReferencias)
                    {
                        foreach (var contenedor in referencia.Contenedores)
                        {
                            foreach (var servicio in contenedor.Servicios)
                            {
                                if (servicio.EstadoServicio == "PE")
                                {
                                    servicio.IdEstadoServicio = estadoRefContServ.IdCatReferenciaEstado;
                                    servicio.EstadoServicio = estadoRefContServ.Clave;
                                    _db.Entry(servicio).State = EntityState.Modified;
                                    huboCambios = true;
                                }
                            }

                            // Cancelar contenedores en PE
                            if (contenedor.EstadoContenedor == "PE")
                            {
                                contenedor.IdEstadoContenedor = estadoRefContServ.IdCatReferenciaEstado;
                                contenedor.EstadoContenedor = estadoRefContServ.Clave;
                                _db.Entry(contenedor).State = EntityState.Modified;
                                huboCambios = true;
                            }
                        }

                        // Cancelar referencias en PE
                        if (referencia.EstadoReferencia == "PE")
                        {
                            referencia.IdCatReferenciaEstado = estadoRefContServ.IdCatReferenciaEstado;
                            referencia.EstadoReferencia = estadoRefContServ.Clave;
                            _db.Entry(referencia).State = EntityState.Modified;
                            huboCambios = true;
                        }
                    }

                    // Cancelar orden si estaba en PE
                    if (orden.IdEstadoOrden == _db.catTipoEstados.FirstOrDefault(e => e.TipoEstado == "PE")?.IdCatTipoEstados)
                    {
                        orden.IdEstadoOrden = estadoOrden.IdCatTipoEstados;
                        _db.Entry(orden).State = EntityState.Modified;
                        huboCambios = true;
                    }
                }
            }

            if (!huboCambios)
                return false;

            return Guardar(out var _);
        }
        private RespuestaTokenDTO GetUsuarioToken()
        {
            // 1) Obtener el HttpContext de la petición
            var context = _httpContextAccessor.HttpContext;

            // 2) Llamar el método de extensión y retornar el objeto RespuestaTokenDTO
            return context.ObtenerUsuarioTokenUnificado();
        }


        #endregion VARIOS

        #endregion METODOS PRIVADOS


        public ICollection<PeticionesReferencias> GetReferenciaNAD(string referenciaNAD)
        {
            // return _db.PeticionesContenedores.Include(c => c.PeticionesReferencias).Include(d => d.Servicios).FirstOrDefault(a => a.RefenciaCliente == referenciaNAD);
            return _db.peticionesReferencias
                 .Include(x => x.catReferenciaEstado)
                .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(d => d.Documentos)
                .ThenInclude(e => e.CatDocumento)
                .Include(cc => cc.Contenedores).ThenInclude(cp => cp.catPatios)
                .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(cr => cr.catReferenciaEstado)
                 .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(cs => cs.catReferenciaEstado)
                .Where(c => c.Contenedores.Any(d => d.RefenciaCliente.Equals(referenciaNAD))).ToList();

        }
        public async Task<ICollection<Ordenes>> GetOrdenes(FiltroOrdenesReferenciasDTO pFiltro)
        {
            pFiltro.IdCliente = pFiltro.IdCliente == null ? 0 : pFiltro.IdCliente;
            pFiltro.IdAduana = pFiltro.IdAduana == null ? 0 : pFiltro.IdAduana;

            pFiltro.IdEmpresa = pFiltro.IdEmpresa == null ? new List<string>() : pFiltro.IdEmpresa;
            pFiltro.IdLNegocio = pFiltro.IdLNegocio == null ? 0 : pFiltro.IdLNegocio;
            pFiltro.IdOrden = pFiltro.IdOrden == null ? 0 : pFiltro.IdOrden;

            var lstOrdenesReferencias = await _db.ordenes.Include(a => a.peticionesReferencias).ThenInclude(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(pd => pd.Documentos)
                .Where(a => (pFiltro.IdCliente <= 0 || a.IdCatCliente == pFiltro.IdCliente) &&
                             (pFiltro.IdOrden <= 0 || a.IdOrden == pFiltro.IdOrden) &&
                            (pFiltro.IdAduana <= 0 || a.IdCatAduana == pFiltro.IdAduana) &&
                            //(pFiltro.IdEmpresa <= 0 || a.IdCatEmpresa == pFiltro.IdEmpresa) &&
                            (pFiltro.IdEmpresa.Count <= 0 || pFiltro.IdEmpresa.Contains(a.IdCatEmpresa.ToString())) &&
                            (pFiltro.IdLNegocio <= 0 || a.IdCatLineaNegocio == pFiltro.IdLNegocio)
                            &&
                            (string.IsNullOrEmpty(pFiltro.Contenedor) || a.peticionesReferencias.Any(b => b.Contenedores.Any(b => b.Contenedor == pFiltro.Contenedor)))
                            &&
                            (string.IsNullOrEmpty(pFiltro.ReferenciaCliente) || a.peticionesReferencias.Any(b => b.Contenedores.Any(b => b.RefenciaCliente == pFiltro.ReferenciaCliente))) &&
                            (string.IsNullOrEmpty(pFiltro.ReferenciaALO) || a.ReferenciaALO.Equals(pFiltro.ReferenciaALO))
                           )

                .ToListAsync();
            return lstOrdenesReferencias;

        }
        public PeticionesContenedores GetContenedor(int IdReferencia, int IdContenedor)
        {
            var usuarioToken = GetUsuarioToken();

            return _db.peticionesContenedores
                    .Include(pc => pc.PeticionesReferencias)
                        .ThenInclude(pr => pr.ordenes)
                .Where(c =>
                    c.PeticionesReferencias.IdReferencia == IdReferencia
                    && c.IdContenedor == IdContenedor
                    && c.PeticionesReferencias.EstadoReferencia != "C"
                    && c.PeticionesReferencias.EstadoReferencia != "T"
                    && c.PeticionesReferencias.IdCatReferenciaEstado != 5
                    && c.PeticionesReferencias.IdCatReferenciaEstado != 6
                    && c.EstadoContenedor != "T"
                    && c.EstadoContenedor != "C"
                    && c.IdEstadoContenedor != 5
                    && c.IdEstadoContenedor != 6
                    && (usuarioToken.IdCatCliente == null || c.PeticionesReferencias.ordenes.IdCatCliente == usuarioToken.IdCatCliente)
                    && c.Activo
                ).FirstOrDefault();
        }
        public PeticionesServicios GetServicioContenedor(int IdReferencia, int IdContenedor, int IdServicio)
        {
            PeticionesServicios objPservicios = _db.peticionesServicios.Where(c => c.PeticionesContenedores.IdReferencia == IdReferencia && c.PeticionesContenedores.IdContenedor == IdContenedor && c.IdServicio == IdServicio).FirstOrDefault();
            return objPservicios;
            //throw new NotImplementedException();
        }
        public int? GetIdAduana(int? pAduanaId)
        {
            return _db.catAduana.Where(a => a.Aduana.Equals(pAduanaId)).Select(a => a.IdCatAduana).FirstOrDefault();
        }

        public async Task<List<CatLineaNegocioTariPrecio>> GetTarifarioServicios(string parametrosEncriptados)
        {
            var resultado = new List<CatLineaNegocioTariPrecio>();

            if (string.IsNullOrWhiteSpace(parametrosEncriptados))
                return resultado;

            string textoPlano;
            try
            {
                textoPlano = AesEncryptionHelper.Decrypt(parametrosEncriptados);
            }
            catch
            {
                return resultado;
            }

            if (string.IsNullOrWhiteSpace(textoPlano))
                return resultado;

            var parametros = textoPlano.Split('&', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(p => p.Split('=', 2))
                                       .Where(p => p.Length == 2)
                                       .ToDictionary(p => p[0], p => p[1]);

            if (!FiltroMapperHelper.TryObtenerFiltros(parametros, out FiltroTarifarioServicioDTO filtros))
                return new List<CatLineaNegocioTariPrecio>();

            return await _db.catLineaNegocioTariPrecios
                                .Include(p => p.catLineaNegocioTarifa)
                                    .ThenInclude(t => t.catServicios)
                                .Where(p =>
                                    p.IdCatEmpresa == filtros.IdCatEmpresa &&
                                    p.catLineaNegocioTarifa.IdCatLineaNegocio == filtros.IdLineaNegocio &&
                                    (filtros.IdCatAduana == null || p.IdCatAduana == filtros.IdCatAduana)
                                ).ToListAsync();
        }

        public bool Guardar(out string strError)
        {
            try
            {
                strError = string.Empty;
                return _db.SaveChanges() > 0 ? true : false;
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo de errores relacionados con la actualización de la base de datos
                //return StatusCode(500, $"Database update error: {dbEx.Message}");
                strError = dbEx.Message;
                return false;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        #region METODOS COMENTADOS
        //public List<string> ValidaPeticionReferenciaNad(PeticionesReferencias peticionesReferencias)
        //{
        //    #region Variables
        //    var lstStrErrores = new List<string>();
        //    ClsCatTransportitasRepositorio = new CatTransportitasRepositorio(_db);
        //    string strError = string.Empty;
        //    #endregion Variables

        //    try
        //    {
        //        if (ExisteTicket((int)peticionesReferencias.Ticket))
        //        {
        //            lstStrErrores.Add("El ticket " + peticionesReferencias.Ticket + " ya existe");
        //        }
        //        if (string.IsNullOrWhiteSpace(peticionesReferencias.Transporte_RFC))
        //            lstStrErrores.Add("El RFC de Transportista es requerido");
        //        else
        //        {
        //            if (string.IsNullOrWhiteSpace(peticionesReferencias.Transporte_RazonSocial))
        //                lstStrErrores.Add("Se require el campo Transporte_RazonSocial");
        //            else
        //            {
        //                if (string.IsNullOrWhiteSpace(peticionesReferencias.Transporte_Usuario))
        //                    lstStrErrores.Add("Se require el campo Transporte_Usuario");
        //                else
        //                {
        //                    if (string.IsNullOrWhiteSpace(peticionesReferencias.Transporte_UsuarioEmail))
        //                        lstStrErrores.Add("Se require el campo Transporte_UsuarioEmail");
        //                    else
        //                    {
        //                        //Valida si el RFC del transportista existe.
        //                        var objCatTransportistas = ClsCatTransportitasRepositorio.ObtenerPorRFC(peticionesReferencias.Transporte_RFC.Trim());
        //                        if (objCatTransportistas != null)
        //                            peticionesReferencias.TrasporteId = objCatTransportistas.IdCatTransportista;
        //                        else
        //                        {
        //                            // Agregar al catalogo de transportistas el RFC de la linea transportista
        //                            var objCatTransportista = new CatTransportistas();

        //                            objCatTransportista.RFC = peticionesReferencias.Transporte_RFC;
        //                            objCatTransportista.RazonSocial = peticionesReferencias.Transporte_RazonSocial;
        //                            objCatTransportista.Correo = peticionesReferencias.Transporte_UsuarioEmail;
        //                            objCatTransportista.Activo = true;
        //                            objCatTransportista.IdCatPaises = 1;
        //                            objCatTransportista.IdCatPaisEstados = 1;
        //                            objCatTransportista.FechaRegistro = DateTime.Now.Date;
        //                            objCatTransportista.IdUsuarioRegistro = 1;
        //                            objCatTransportista.IdCatEmpresas = 2;

        //                            _db.catTransportistas.Add(objCatTransportista);
        //                            Guardar(out strError);

        //                            if (string.IsNullOrEmpty(strError))
        //                            {
        //                                objCatTransportistas = ClsCatTransportitasRepositorio.ObtenerPorRFC(peticionesReferencias.Transporte_RFC.Trim());
        //                                if (objCatTransportistas != null)
        //                                    peticionesReferencias.TrasporteId = objCatTransportistas.IdCatTransportista;
        //                            }
        //                            else
        //                                lstStrErrores.Add(strError);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (peticionesReferencias.Contenedores.Count > 0)
        //        {
        //            var lstcontenedores = peticionesReferencias.Contenedores.Select(x => x.Contenedor).ToList();
        //            var lstcontenedoresDistinct = lstcontenedores.Distinct().Count();
        //            if (lstcontenedoresDistinct != lstcontenedores.Count)
        //            {
        //                lstStrErrores.Add("Existen números de contenedor repetidos en la solicitud");
        //            }
        //            //1.- Validamos que los contenedores de la referencia pertenezcan a la misma aduana
        //            var lstIntAduana = peticionesReferencias.Contenedores.Select(a => a.AduanaId).Distinct().ToList();
        //            // 1.1.- Validar si hay más de un AduanaId en la solicitud
        //            if (lstIntAduana.Count() > 1)
        //                lstStrErrores.Add("Existen aduanas diferentes en la solicitud");
        //            else
        //            {
        //                // Obtener el único AduanaId que existe en los contenedores
        //                int aduanaId = (int)lstIntAduana.FirstOrDefault();

        //                // Obtener el nombre correcto de la aduana desde la base de datos
        //                var nombreAduanaCorrecto = _db.catAduana.Where(a => a.Aduana == aduanaId).Select(a => a.Nombre).FirstOrDefault();

        //                // Verificar si el AduanaId existe en la base de datos
        //                if (string.IsNullOrEmpty(nombreAduanaCorrecto))
        //                {
        //                    lstStrErrores.Add($"El AduanaId {aduanaId} no existe en la base de datos.");
        //                }
        //                else
        //                {
        //                    // 1.2.- Validar si el nombre de la aduana en cada contenedor coincide con la base de datos
        //                    foreach (var cont in peticionesReferencias.Contenedores)
        //                    {
        //                        if (!cont.Aduana.Equals(nombreAduanaCorrecto, StringComparison.OrdinalIgnoreCase))
        //                        {
        //                            lstStrErrores.Add($"El contenedor {cont.Contenedor} tiene una Aduana incorrecta. Se esperaba '{nombreAduanaCorrecto}', pero se encontró '{cont.Aduana}'.");
        //                        }
        //                    }
        //                }
        //            }

        //            //Validar Contenedores
        //            foreach (PeticionesContenedores pCont in peticionesReferencias.Contenedores)
        //            {
        //                //Contenedor repetido en solicitud
        //                var objRespuesta = ValidaPeticionContenedorNad(pCont);
        //                if (objRespuesta.Count() > 0)
        //                {
        //                    lstStrErrores.AddRange(objRespuesta);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            lstStrErrores.Add("Debe agregar al menos un contenedor");

        //        }

        //        return lstStrErrores;
        //    }
        //    catch (Exception ex)
        //    {
        //        lstStrErrores.Add(ex.Message);
        //        return lstStrErrores;
        //    }
        //}
        //public List<string> ValidaPeticionContenedorNad(PeticionesContenedores pCont)
        //{
        //    #region Variables
        //    List<string> lstStrErrores = new List<string>();
        //    ClsCatClientesRepositorio = new CatClientesRepositorio(_db);
        //    ClsCatNavierasRepositorio = new CatNavierasRepositorio(_db);
        //    ClsCatPatiosRespositorio = new CatPatiosRepositorio(_db);
        //    #endregion Variables

        //    #region Inicialización de estatus
        //    pCont.IdEstadoContenedor = 7;
        //    pCont.EstadoContenedor = "PE";
        //    pCont.FechaCierre = null;
        //    #endregion

        //    try
        //    {
        //        //Está activo, no se puede repetir.
        //        //Contenedor Activo
        //        if (pCont.Contenedor.Trim().Length > 0 || pCont.Contenedor != null)
        //        {
        //            // 1) Quita todo lo que NO sea [A-Za-z0-9]
        //            //    (espacios, comas, guiones, etc.)
        //            string alfanumerico = Regex.Replace(pCont.Contenedor, @"[^A-Za-z0-9]", "");

        //            // 2) Convierte a mayúsculas
        //            pCont.Contenedor = alfanumerico.ToUpperInvariant();

        //            // 3) Verifica que tenga EXACTAMENTE 11 caracteres
        //            //    y cumpla con el patrón: 4 letras + 7 dígitos
        //            //    => ^[A-Z]{4}\d{7}$
        //            if (pCont.Contenedor.Length != 11)
        //            {
        //                lstStrErrores.Add("El contenedor " + pCont.Contenedor + " no es un contenedor valido.");
        //            }
        //            else
        //            {
        //                var regex = new Regex(@"^[A-Z]{4}\d{7}$");
        //                if (!regex.IsMatch(pCont.Contenedor))
        //                {
        //                    lstStrErrores.Add("El contenedor " + pCont.Contenedor + " no es un contenedor valido.");
        //                }
        //                else
        //                {
        //                    // 4) Separamos la parte alfa (4 chars) y la parte numérica (7 chars)
        //                    string parteAlfa = pCont.Contenedor.Substring(0, 4);   // Ej: "ABCD"
        //                    string parteNum = pCont.Contenedor.Substring(4, 7);   // Ej: "1234567"

        //                    // 5) Regla: los 4 dígitos alfabéticos NO deben ser todos iguales (p.e. "AAAA")
        //                    //    Comprobamos si todas las letras son la misma
        //                    if (parteAlfa.Distinct().Count() <= 2)
        //                    {
        //                        // Ejemplo: "AAAA", "BBBB", etc.
        //                        lstStrErrores.Add("El contenedor " + pCont.Contenedor + " no es un contenedor valido.");
        //                    }
        //                    else
        //                    {
        //                        // 6) Regla: los 7 dígitos numéricos NO deben ser alguno de estos patrones
        //                        //    (ejemplos proporcionados):
        //                        //    "1111111", "1111222", "1112223", "1122334"
        //                        //    (puedes añadir otros si lo deseas)
        //                        var patronesProhibidos = GetPatronesContenedoresProhibidos();
        //                        if (patronesProhibidos.Contains(parteNum))
        //                        {
        //                            lstStrErrores.Add("El contenedor " + pCont.Contenedor + " no es un contenedor valido.");
        //                        }
        //                    }
        //                }
        //            }

        //            //Está activo, no se puede repetir.
        //            if (ExisteContenedorOperando(pCont.RefenciaCliente.Trim(), pCont.Contenedor.Trim()))
        //            {
        //                lstStrErrores.Add("El contenedor " + pCont.Contenedor + " y referencia  " + pCont.RefenciaCliente.Trim() + " ya existe");
        //                lstStrErrores.Add("El contenedor " + pCont.Contenedor + " está activo");
        //            }
        //            //Validamos servicios.
        //            var objlstServicios = ValidaPeticionServiciosNad(pCont);
        //            if (objlstServicios.Count > 0 || objlstServicios != null)
        //            {
        //                lstStrErrores.AddRange(objlstServicios);
        //            }
        //            //Validamos cliente.
        //            if (pCont.Cliente_RFC.Trim().Length > 0 || pCont.Cliente_RFC != null)
        //            {
        //                var objCatClientes = ClsCatClientesRepositorio.obtenerClienteRFC(pCont.Cliente_RFC.Trim());
        //                pCont.ClienteId = pCont.ClienteId == 0 ? objCatClientes.IdCatCliente : pCont.ClienteId;
        //                pCont.IdClienteFacturarA = (pCont.IdClienteFacturarA ?? 0) == 0 ? objCatClientes.IdCatCliente : pCont.IdClienteFacturarA;
        //                if (objCatClientes == null)
        //                    lstStrErrores.Add($"Contenedor:{pCont.Contenedor}:El RFC {pCont.Cliente_RFC} de Cliente no está registrado");
        //            }
        //            //Validamos Patio
        //            if (pCont.Patio_RFC != null)
        //            {
        //                if (pCont.Patio_RFC.Trim().Length > 0 || pCont.Patio_RFC != null)
        //                {
        //                    var objCatPatios = ClsCatPatiosRespositorio.obtenerPatioRFC(pCont.Patio_RFC.Trim());
        //                    if (objCatPatios != null)
        //                    {
        //                        //var objCont = peticionesReferencias.Contenedores.FirstOrDefault(x => x.Contenedor.Equals(pCont.Contenedor));
        //                        pCont.PatioId = objCatPatios.IdCatPatios;
        //                    }
        //                }
        //            }
        //            //Validamos Naviera RFC
        //            if (pCont.Naviera_RFC.Trim().Length > 0 || pCont.Naviera_RFC != null)
        //            {
        //                var objCatNavieras = ClsCatNavierasRepositorio.obtenerPorRFC(pCont.Naviera_RFC.Trim());
        //                if (objCatNavieras != null)
        //                {
        //                    //var objCont = peticionesReferencias.Contenedores.FirstOrDefault(x => x.Contenedor.Equals(pCont.Contenedor));
        //                    pCont.Naviera_Id = objCatNavieras.IdCatNaviera;
        //                    pCont.Naviera_RazonSocial = objCatNavieras.RazonSocial;
        //                }
        //                else
        //                {
        //                    //SE AGREGA LA NAVIERA EN CASO DE NO CONTAR CON EL REGISTRO EN BASE DE DATOS
        //                    string strError = string.Empty;
        //                    var objCatNavieraGuardar = new CatNavieras();

        //                    objCatNavieraGuardar.RazonSocial = pCont.Naviera_RazonSocial;
        //                    objCatNavieraGuardar.Acronimo = null;
        //                    objCatNavieraGuardar.RFC = pCont.Naviera_RFC.Trim();
        //                    objCatNavieraGuardar.Activo = true;
        //                    objCatNavieraGuardar.FechaRegistro = DateTime.Now;

        //                    _db.catNavieras.Add(objCatNavieraGuardar);
        //                    Guardar(out strError);

        //                    if (string.IsNullOrEmpty(strError))
        //                    {
        //                        objCatNavieras = ClsCatNavierasRepositorio.obtenerPorRFC(pCont.Naviera_RFC.Trim());
        //                        if (objCatNavieras != null)
        //                        {
        //                            pCont.Naviera_Id = objCatNavieras.IdCatNaviera;
        //                            pCont.Naviera_RFC = objCatNavieras.RFC;
        //                            pCont.Naviera_RazonSocial = objCatNavieras.RazonSocial;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lstStrErrores.Add(strError);
        //                    }
        //                }
        //                //Agregar la naviera en caso de que este no exista
        //                //else
        //                //{
        //                //    //    lstError.Add("El RFC de Naviera no está registrado");
        //                //    // Agregar el rfc de la naviera
        //                //}
        //            }
        //            //Se asigna el id del cliente al id cliente facturar a si este es vacio.
        //            foreach (var pServ in pCont.Servicios)
        //            {
        //                pServ.FechaCierre = null;
        //                pServ.IdClienteFacturarA = (pServ.IdClienteFacturarA ?? 0) == 0 ? pCont.IdClienteFacturarA : pServ.IdClienteFacturarA;
        //            }
        //        }
        //        else
        //        {
        //            lstStrErrores.Add("Falta el número de contenedor");

        //        }
        //        return lstStrErrores;
        //    }
        //    catch (Exception ex)
        //    {
        //        lstStrErrores.Add(ex.Message);
        //        return lstStrErrores;
        //    }

        //}
        //public List<string> ValidaPeticionServiciosNad(PeticionesContenedores pCont)
        //{
        //    #region Variables
        //    List<string> lstStrErrores = new List<string>();
        //    #endregion Variables

        //    #region Operaciones
        //    try
        //    {
        //        if (pCont.Servicios.Count == 0)
        //        {
        //            lstStrErrores.Add("El contenedor debe tener al menos un servicio");
        //        }
        //        else
        //        {
        //            //Validar servicios diferentes
        //            var lstIntServicios = pCont.Servicios.Select(x => x.IdTipoServicio).ToList();
        //            if (lstIntServicios.Distinct().Count() != pCont.Servicios.Count)
        //            {
        //                lstStrErrores.Add($"Existen servicios repetidos en el contenedor {pCont.Contenedor} ");
        //            }

        //            // Verificamos si están ambos servicios 1 y 2
        //            if (lstIntServicios.Contains(1) && lstIntServicios.Contains(2))
        //            {
        //                lstStrErrores.Add($"El contenedor {pCont.Contenedor} no puede tener el servicio de Maniobra de vacío y Gestión Electrónica de EIR.");
        //            }

        //            //Validar los servicios por contenedor y los datos requeridos.
        //            foreach (PeticionesServicios pServ in pCont.Servicios)
        //            {
        //                //Validamos requeridos Maniobras.
        //                //if (pServ.IdServicio == 1 || pServ.IdServicio == 2)
        //                pServ.IdEstadoServicio = 7;
        //                pServ.EstadoServicio = "PE";

        //                if (string.IsNullOrWhiteSpace(pCont.Contenedor))
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere número contenedor.");
        //                if (string.IsNullOrWhiteSpace(pCont.RefenciaCliente))
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere REFERENCIA CLIENTE.");
        //                if (string.IsNullOrWhiteSpace(pCont.ClaveTipoContenedor))
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere CLAVE TIPO CONTENEDOR.");
        //                if (string.IsNullOrWhiteSpace(pCont.Cliente_RazonSocial))
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere Cliente_RazonSocial.");
        //                if (string.IsNullOrWhiteSpace(pCont.Cliente_RFC))
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere Cliente_RFC.");
        //                if (string.IsNullOrWhiteSpace(pCont.Cliente_Solicitante))
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere Cliente_Solicitante.");

        //                if (!pCont.AduanaId.HasValue)
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere AduanaId.");
        //                if (pCont.Naviera_RazonSocial is null)
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere Naviera_RazonSocial.");
        //                if (pCont.Naviera_RFC is null)
        //                    lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de maniobra requiere Naviera_RFC.");
        //                //Si es recuperación de garantías.
        //                if (pServ.IdTipoServicio == 3)
        //                {
        //                    if (string.IsNullOrWhiteSpace(pCont.Moneda))
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere Moneda.");

        //                    if (string.IsNullOrWhiteSpace(pCont.BL))
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere BL.");
        //                    if (string.IsNullOrWhiteSpace(pCont.Buque))
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere Buque.");
        //                    if (string.IsNullOrWhiteSpace(pCont.Consignado_RazonSocial))
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere Consignado_RazonSocial.");
        //                    if (string.IsNullOrWhiteSpace(pCont.Consignado_RFC))
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere Consignado_RFC.");
        //                    if (string.IsNullOrWhiteSpace(pCont.FondoFinanciamiento))
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere FondoFinanciamiento.");
        //                    if (pCont.MontoSolicitud == null)
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere MontoSolicitud.");
        //                    if (pCont.MontoTotal == null)
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere MontoTotal.");
        //                    if (pCont.FechaPagoGarantiaNav == null)
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere FechaPagoGarantiaNav.");
        //                    if (pCont.FechaSolDevolucion == null)
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere FechaSolDevolucion.");
        //                    if (pCont.FechaTocaPiso == null)
        //                        lstStrErrores.Add($"Contenedor {pCont.Contenedor} Servicio de receperación Garantía requiere FechaTocaPiso.");
        //                }
        //            }
        //        }
        //        return lstStrErrores;
        //    }
        //    catch (Exception ex)
        //    {

        //        lstStrErrores.Add(ex.Message);
        //        return lstStrErrores;
        //    }
        //    #endregion Operaciones
        //}
        //public bool validarAgregarPeticionReferencia(PeticionesReferencias peticionesReferencias, out List<string> pError)
        //{
        //    pError = new List<string>();

        //    //Validar ticket existente
        //    if (ExisteTicket((int)peticionesReferencias.Ticket))
        //    {
        //        pError.Add("El ticket " + peticionesReferencias.Ticket + " ya existe");
        //    }
        //    //Validar referencia NAD-contenedor-estado contenedor.
        //    foreach (PeticionesContenedores pcont in peticionesReferencias.Contenedores)
        //    {
        //        if (ExisteContenedorActivo(pcont.Contenedor))
        //        {
        //            pError.Add("Ticket:" + peticionesReferencias.Ticket + ". El contenedor en la solicitud:" + pcont.RefenciaCliente + " contenedor: " + pcont.Contenedor + " se encuentra en operación no se puede agregar en una nueva referencia");
        //        }


        //    }
        //    //Validar referencia NAD
        //    //Validar referencia NAD cliente.
        //    //Validar contenedor existente y activo.
        //    //Validar tipo de servicio existente.
        //    //Validar datos por tipo de servicio.
        //    //Validar servicio existente por contenedor.
        //    return false;
        //}
        //private List<string> ValidaCamposReferenciaNad(PeticionesReferenciasDTO peticionesReferencias)
        //{
        //    var lstStrErrores = new List<string>();

        //    if (peticionesReferencias != null)
        //    {
        //        if (peticionesReferencias.Ticket == 0)
        //            lstStrErrores.Add("No se ingresó el número de ticket.");

        //        if (!peticionesReferencias.Contenedores.Any(c => c.Cliente_RFC == "NME980506LPA" || c.Cliente_RFC == "MNE0409226K9"))
        //        {
        //            //############################## SOLICITAR CUANDO EL CLIENTE NO SEA NESTLE ######################################
        //            if (string.IsNullOrWhiteSpace(peticionesReferencias.Transporte_RFC))
        //                lstStrErrores.Add("No se ingresó el RFC de la línea transportista.");
        //            if (string.IsNullOrWhiteSpace(peticionesReferencias.Transporte_RazonSocial))
        //                lstStrErrores.Add("No se ingresó la razón social de la línea transportista.");
        //            if (string.IsNullOrWhiteSpace(peticionesReferencias.Transporte_Usuario))
        //                lstStrErrores.Add("No se proporcionó el usuario correspondiente al transporte.");
        //            if (string.IsNullOrWhiteSpace(peticionesReferencias.Transporte_UsuarioEmail))
        //                lstStrErrores.Add("No se proporcionó el email de la linea transportista.");
        //            //#############################################################################################################
        //        }

        //        if (!peticionesReferencias.Contenedores.Any())
        //            lstStrErrores.Add("No se agregaron contenedores a la solicitud.");
        //        else
        //        {
        //            foreach (var contenedor in peticionesReferencias.Contenedores)
        //            {
        //                if (string.IsNullOrWhiteSpace(contenedor.Contenedor))
        //                    lstStrErrores.Add("Debe agregar al menos un contenedor.");
        //                if (string.IsNullOrWhiteSpace(contenedor.ClaveTipoContenedor))
        //                    lstStrErrores.Add("Debe agregar el tipo del contenedor (Ejemplo: HQ20, HQ40, etc).");
        //                if (string.IsNullOrWhiteSpace(contenedor.Cliente_RFC))
        //                    lstStrErrores.Add("No se proporcionó el RFC del cliente.");
        //                if (string.IsNullOrWhiteSpace(contenedor.Cliente_RazonSocial))
        //                    lstStrErrores.Add("No se proporcionó la razón social del cliente.");
        //                if (string.IsNullOrWhiteSpace(contenedor.Cliente_Solicitante))
        //                    lstStrErrores.Add("No se proporcionó el nombre del ejecutivo nad.");
        //                if (string.IsNullOrWhiteSpace(contenedor.Cliente_Solicitante))
        //                    lstStrErrores.Add("No se proporcionó el nombre del ejecutivo nad.");
        //                if (contenedor.AduanaId == 0)
        //                    lstStrErrores.Add("No se proporciono una aduana.");
        //                if (string.IsNullOrWhiteSpace(contenedor.Naviera_RFC))
        //                    lstStrErrores.Add("No se proporcionó el RFC de la naviera.");
        //                if (string.IsNullOrWhiteSpace(contenedor.Naviera_RazonSocial))
        //                    lstStrErrores.Add("No se proporcionó la razón social de la naviera.");
        //                if (!contenedor.Servicios.Any())
        //                    lstStrErrores.Add($"Debe proporcionar al menos un servicio para el contenedor {contenedor.Contenedor}");
        //                else
        //                {
        //                    foreach (var servicio in contenedor.Servicios)
        //                    {
        //                        if (servicio.IdTipoServicio == 0)
        //                            lstStrErrores.Add("No se proporcionó el tipo de servicio.");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        lstStrErrores.Add("No se proporciono información en la solicitud.");
        //    }

        //    return lstStrErrores;
        //}
        //private List<PeticionesServicios> GetServiciosContenedor(int idContenedor)
        //{
        //    return _db.peticionesServicios.Where(ps => ps.IdContenedor == idContenedor).ToList();
        //}

        //private Ordenes GetOrdenPorIdReferencia(int IdReferencia)
        //{
        //    return _db.ordenes
        //                .Where(o => o.peticionesReferencias
        //                             .Any(pr => pr.IdReferencia == IdReferencia))
        //                .FirstOrDefault();
        //}
        //private async Task<Ordenes> GetOrdenPorId(int? idOrden = null, int? idReferencia = null)
        //{
        //    var usuarioToken = GetUsuarioToken();

        //    return await _db.ordenes
        //                        .Include(o => o.peticionesReferencias)
        //                            .ThenInclude(r => r.Contenedores)
        //                                .ThenInclude(c => c.Servicios)
        //                        .FirstOrDefaultAsync(o => 
        //                                (o.IdOrden == idOrden)
        //                            && (usuarioToken.IdCatCliente == null || o.IdCatCliente.Equals(usuarioToken.IdCatCliente))
        //                        );
        //}
        //public async Task<ICollection<RespObtenerReferenciasDTO>> GetReferencias(FiltroOrdenesReferenciasDTO pFiltro)
        //{
        //    var usuarioToken = GetUsuarioToken();

        //    if ((usuarioToken.IdCatCliente == null) && (usuarioToken.RolesUsuario.Contains("ADMIN") ||
        //                                                usuarioToken.RolesUsuario.Contains("ADMINUSER") ||
        //                                                "ALogistics".Equals(usuarioToken.User, StringComparison.OrdinalIgnoreCase)))
        //    {
        //        pFiltro.IdCliente = pFiltro.IdCliente == null ? 0 : pFiltro.IdCliente;
        //    }
        //    else
        //    {
        //        pFiltro.IdCliente = usuarioToken.IdCatCliente;
        //    }

        //    pFiltro.IdAduana = pFiltro.IdAduana == null ? 0 : pFiltro.IdAduana;
        //    pFiltro.IdEmpresa = pFiltro.IdEmpresa == null ? new List<string>() : pFiltro.IdEmpresa;
        //    pFiltro.IdLNegocio = pFiltro.IdLNegocio == null ? 0 : pFiltro.IdLNegocio;
        //    pFiltro.IdOrden = pFiltro.IdOrden == null ? 0 : pFiltro.IdOrden;
        //    pFiltro.IdServicio = pFiltro.IdServicio == null ? 0 : pFiltro.IdServicio;
        //    pFiltro.Ticket = pFiltro.Ticket == null ? 0 : pFiltro.Ticket;
        //    pFiltro.IdPatio = pFiltro.IdPatio == null ? 0 : pFiltro.IdPatio;
        //    pFiltro.IdCatEstadoOrden = pFiltro.IdCatEstadoOrden == null ? 0 : pFiltro.IdCatEstadoOrden;
        //    pFiltro.IdCatEstadoReferencia = pFiltro.IdCatEstadoReferencia == null ? 0 : pFiltro.IdCatEstadoReferencia;
        //    pFiltro.IdCatEstadoContenedor = pFiltro.IdCatEstadoContenedor == null ? 0 : pFiltro.IdCatEstadoContenedor;
        //    pFiltro.FechaSolicitudIni = pFiltro.FechaSolicitudIni == null ? DateTime.Now.AddDays(-7) : pFiltro.FechaSolicitudIni;
        //    pFiltro.FechaSolicitudFin = pFiltro.FechaSolicitudFin == null ? DateTime.Now : pFiltro.FechaSolicitudFin;

        //    var lstOrdenesReferencias = await _db.peticionesReferencias.Include(a => a.ordenes).ThenInclude(d => d.catClientes)
        //        .Include(d => d.catReferenciaEstado)
        //        .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(s => s.catServicios)
        //        .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(cs => cs.catReferenciaEstado)
        //        .Include(b => b.Contenedores).ThenInclude(c => c.Servicios).ThenInclude(pd => pd.Documentos).ThenInclude(td => td.CatDocumento)
        //        .Include(b => b.Contenedores).ThenInclude(c => c.catPatios)
        //        .Where(a => (pFiltro.IdCliente <= 0 || a.ordenes.IdCatCliente == pFiltro.IdCliente) &&
        //                     (pFiltro.IdOrden <= 0 || a.ordenes.IdOrden == pFiltro.IdOrden) &&
        //                    (pFiltro.IdAduana <= 0 || a.ordenes.IdCatAduana == pFiltro.IdAduana) &&
        //                    //(pFiltro.IdEmpresa <= 0 || a.ordenes.IdCatEmpresa == pFiltro.IdEmpresa) &&
        //                    (pFiltro.IdEmpresa.Count <= 0 || pFiltro.IdEmpresa.Contains(a.ordenes.IdCatEmpresa.ToString())) &&
        //                    (pFiltro.IdLNegocio <= 0 || a.ordenes.IdCatLineaNegocio == pFiltro.IdLNegocio) &&
        //                    (pFiltro.IdPatio <= 0 || a.Contenedores.Any(c => c.PatioId == pFiltro.IdPatio)) &&
        //                    (pFiltro.Ticket <= 0 || a.Ticket == pFiltro.Ticket) &&
        //                    (pFiltro.IdCatEstadoOrden <= 0 || a.ordenes.IdEstadoOrden.Equals(pFiltro.IdCatEstadoOrden)) &&
        //                    (pFiltro.IdCatEstadoReferencia <= 0 || a.IdCatReferenciaEstado.Equals(pFiltro.IdCatEstadoReferencia)) &&
        //                    (pFiltro.IdCatEstadoContenedor <= 0 || a.Contenedores.Any(c => c.IdEstadoContenedor == pFiltro.IdCatEstadoContenedor)) &&
        //                    (pFiltro.IdServicio <= 0 || a.Contenedores.Any(c => c.Servicios.Any(c => c.IdTipoServicio == pFiltro.IdServicio)))
        //                    &&
        //                    //(a.FechaSolicitud >= pFiltro.FechaSolicitudIni && a.FechaSolicitud <= pFiltro.FechaSolicitudFin) &&

        //                    (string.IsNullOrEmpty(pFiltro.Contenedor) || a.Contenedores.Any(b => b.Contenedor == pFiltro.Contenedor)) &&
        //                     (string.IsNullOrEmpty(pFiltro.Buque) || a.Contenedores.Any(b => b.Buque.Contains(pFiltro.Buque))) &&
        //                     (string.IsNullOrEmpty(pFiltro.Ejecutivo) || a.Contenedores.Any(b => b.Cliente_Solicitante.Contains(pFiltro.Ejecutivo))) &&
        //                     (string.IsNullOrEmpty(pFiltro.ReferenciaCliente) || a.Contenedores.Any(b => b.RefenciaCliente == pFiltro.ReferenciaCliente)) &&
        //                     (string.IsNullOrEmpty(pFiltro.ReferenciaALO) || a.ordenes.ReferenciaALO.Equals(pFiltro.ReferenciaALO))
        //                     )

        //       .Select(a => new RespObtenerReferenciasDTO
        //       {
        //           // Map the properties you need

        //           IdReferencia = a.IdReferencia,
        //           Ticket = a.Ticket == null ? 0 : (int)a.Ticket,
        //           Transporte_RFC = a.Transporte_RFC,
        //           Transporte_RazonSocial = a.Transporte_RazonSocial,
        //           TrasporteId = a.TrasporteId == null ? 0 : (int)a.TrasporteId,
        //           Transporte_Usuario = a.Transporte_Usuario,
        //           Transporte_UsuarioEmail = a.Transporte_UsuarioEmail,
        //           Comentarios = a.Comentarios,
        //           TipoReferencia = a.TipoReferencia,
        //           Procesado = a.Procesado,
        //           FechaProcesado = a.FechaProcesado,
        //           FechaSolicitud = a.FechaSolicitud,
        //           EstadoReferencia = a.EstadoReferencia,
        //           IdCatReferenciaEstado = a.IdCatReferenciaEstado,
        //           Activo = a.Activo,
        //           IdOrden = a.IdOrden,
        //           IdCatcliente = a.ordenes.IdCatCliente,
        //           RazonSocialCliente = a.ordenes.catClientes.RazonSocial,
        //           IdCatProveedor = a.ordenes.IdCatProveedor == null ? 0 : (int)a.ordenes.IdCatProveedor,
        //           RazonSocialProveedor = a.ordenes.catProveedores.RazonSocial,
        //           IdCatAduana = a.ordenes.IdCatAduana == null ? 0 : (int)a.ordenes.IdCatAduana,
        //           Aduana = a.ordenes.catAduana.Acronimo,
        //           IdCatSistema = a.ordenes.IdCatSistema == null ? 0 : (int)a.ordenes.IdCatSistema,
        //           Sistema = a.ordenes.catSistemas.nombre,
        //           IdCatEmpresa = a.ordenes.IdCatEmpresa,
        //           RazonSocialEmpresa = a.ordenes.CatEmpresas.RazonSocial,
        //           IdCatsucursal = a.ordenes.IdCatSucursal,
        //           Sucursal = a.ordenes.CatSucursales.Nombre,
        //           //IdCatTransporte=a.ordenes,

        //           IdLNegocio = a.ordenes.IdCatLineaNegocio,
        //           LineaNegocio = a.ordenes.CatLineaNegocio.Acronimo,

        //           IdUsuario = a.ordenes.IdUsuario == null ? 0 : (int)a.ordenes.IdUsuario,
        //           Usuario = a.ordenes.catUsuario.Usuario,
        //           //IdCatEstadoOrden=a.ordenes.,
        //           //EstadoOrden=a.ordenes.catEstadoOrden.Nombre,

        //           IdCatEstadoReferencia = a.IdCatReferenciaEstado,
        //           ActivoOrden = a.ordenes.Activo,
        //           FechaRegistroOrden = a.ordenes.FechaRegistro,
        //           FechaRegistroReferencia = a.FechaRegistro,
        //           ReferenciaALO = string.IsNullOrEmpty(a.ordenes.ReferenciaALO) ? "" : a.ordenes.ReferenciaALO,

        //           peticionesContenedores = a.Contenedores.ToList(),

        //           //FechaCierreOrden
        //           //FechaCierreReferencia,
        //           // Add other properties as needed
        //       }).ToListAsync();

        //    return lstOrdenesReferencias;

        //}
        //private bool ActualizarEstatusJerarquico(Ordenes orden, string claveEstado, int? idContenedor = null, int? idServicio = null)
        //{
        //    var estadoOrden = _db.catTipoEstados.FirstOrDefault(e => e.TipoEstado == claveEstado);
        //    var estadoRefContServ = _db.catReferenciaEstado.FirstOrDefault(e => e.Clave == claveEstado);

        //    if (estadoOrden == null || estadoRefContServ == null)
        //        return false;

        //    bool esCancelacion = claveEstado == "C";

        //    bool cancelarOrdenCompleta =
        //        (!idContenedor.HasValue && !idServicio.HasValue) // Caso 1: cancelar toda la orden
        //        ||
        //        (esCancelacion && idContenedor.HasValue && !idServicio.HasValue &&
        //         orden.peticionesReferencias.SelectMany(r => r.Contenedores).Count() == 1); // Caso 2: único contenedor

        //    if (cancelarOrdenCompleta)
        //    {
        //        orden.IdEstadoOrden = estadoOrden.IdCatTipoEstados;
        //        _db.Entry(orden).Property(o => o.IdEstadoOrden).IsModified = true;

        //        foreach (var referencia in orden.peticionesReferencias)
        //        {
        //            referencia.IdCatReferenciaEstado = estadoRefContServ.IdCatReferenciaEstado;
        //            referencia.EstadoReferencia = estadoRefContServ.Clave;
        //            _db.Entry(referencia).Property(r => r.IdCatReferenciaEstado).IsModified = true;
        //            _db.Entry(referencia).Property(r => r.EstadoReferencia).IsModified = true;

        //            foreach (var contenedor in referencia.Contenedores)
        //            {
        //                contenedor.IdEstadoContenedor = estadoRefContServ.IdCatReferenciaEstado;
        //                contenedor.EstadoContenedor = estadoRefContServ.Clave;
        //                _db.Entry(contenedor).Property(c => c.IdEstadoContenedor).IsModified = true;
        //                _db.Entry(contenedor).Property(c => c.EstadoContenedor).IsModified = true;

        //                foreach (var servicio in contenedor.Servicios)
        //                {
        //                    servicio.IdEstadoServicio = estadoRefContServ.IdCatReferenciaEstado;
        //                    servicio.EstadoServicio = estadoRefContServ.Clave;
        //                    _db.Entry(servicio).Property(s => s.IdEstadoServicio).IsModified = true;
        //                    _db.Entry(servicio).Property(s => s.EstadoServicio).IsModified = true;
        //                }
        //            }
        //        }

        //        return Guardar(out var _);
        //    }

        //    // --- Cancelación específica (servicio o contenedor) ---

        //    foreach (var referencia in orden.peticionesReferencias)
        //    {
        //        foreach (var contenedor in referencia.Contenedores)
        //        {
        //            if (idContenedor.HasValue && contenedor.IdContenedor != idContenedor.Value)
        //                continue;

        //            foreach (var servicio in contenedor.Servicios)
        //            {
        //                if (idServicio.HasValue)
        //                {
        //                    if (servicio.IdServicio == idServicio.Value)
        //                    {
        //                        // Cancelar el servicio específico
        //                        servicio.IdEstadoServicio = estadoRefContServ.IdCatReferenciaEstado;
        //                        servicio.EstadoServicio = estadoRefContServ.Clave;
        //                        _db.Entry(servicio).Property(s => s.IdEstadoServicio).IsModified = true;
        //                        _db.Entry(servicio).Property(s => s.EstadoServicio).IsModified = true;
        //                    }
        //                }
        //            }

        //            // --- Cancelar contenedor automáticamente si todos los servicios están cancelados ---
        //            if (esCancelacion)
        //            {
        //                bool todosServiciosCancelados = contenedor.Servicios.All(s =>
        //                    s.EstadoServicio == "C" || (idServicio.HasValue && s.IdServicio == idServicio.Value));

        //                if (todosServiciosCancelados)
        //                {
        //                    contenedor.IdEstadoContenedor = estadoRefContServ.IdCatReferenciaEstado;
        //                    contenedor.EstadoContenedor = estadoRefContServ.Clave;
        //                    _db.Entry(contenedor).Property(c => c.IdEstadoContenedor).IsModified = true;
        //                    _db.Entry(contenedor).Property(c => c.EstadoContenedor).IsModified = true;
        //                }
        //            }
        //        }
        //    }

        //    return Guardar(out var _);
        //}
        //private bool ExisteServicioEnCatalogo(int idCatServicio)
        //{
        //    // 1) Preparamos la lista de IDs
        //    List<int> listaServicios = null;

        //    //2).- VALIDAMOS SI ES UN CLIENTE EXTERNO. EN CASO DE SER UN CLIENTE EXTERNO SE LE PERMITE UNICAMENTE AGREGAR LOS SIGUIENTES SERVICIOS:
        //    //      * IdCatServicio 1: MANIOBRA DE VACIO
        //    //      * IdCatServicio 2: GESTIÓN DE EIR
        //    //      * IdCatServicio 3: RECUPERACIÓN DE GARANTIAS
        //    //      * IdCatServicio 4: CORTE DE DEMORAS
        //    if (!EsUsuarioInterno())
        //        listaServicios = new List<int> { 1, 2, 3, 4 };

        //    //3).- CONSULTA PARA OBTENER LOS IDS DE LOS SERVICIOS DE ACUERDO AL TIPO DE FILTRADO POR EL TIPO DE USUARIO
        //    var lstIntCatServicios = _db.catServicios
        //        // si listaServicios == null => no filtra, de lo contrario => filtra por los IdCatServicio especificados
        //        .Where(s => (listaServicios == null || listaServicios.Contains(s.IdCatServicio))
        //               && s.Activo
        //        )
        //        .Select(s => s.IdCatServicio)
        //        .ToList();

        //    if (lstIntCatServicios.Contains(idCatServicio))
        //        return true;

        //    return false;
        //}
        //public bool ExisteIdServicioEnPeticionesServicios(int idCatServicio)
        //{
        //    return _db.catServicios.Any(s => s.IdCatServicio.Equals(idCatServicio));
        //}
        // METODOS GENERICOS

        //public bool ExisteContenedor(int IdReferencia, string contenedor)
        //{
        //    bool respuesta = _db.peticionesReferencias.Include(c => c.Contenedores).Any(c => c.IdReferencia == IdReferencia && c.Contenedores.Any(x => x.Contenedor.Equals(contenedor.Trim())));
        //    return respuesta;
        //    //throw new NotImplementedException();
        //}
        //public bool ExisteContenedorId(int IdReferencia, int IdContenedor)
        //{
        //    return _db.peticionesReferencias.Include(c => c.Contenedores).Any(c => c.IdReferencia == IdReferencia && c.Contenedores.Any(x => x.IdContenedor == IdContenedor));
        //}

        //private bool ExisteContenedorActivo(string? contenedor)
        //{
        //    return _db.peticionesContenedores
        //            .Include(c => c.PeticionesReferencias)
        //                .ThenInclude(pr => pr.ordenes)
        //            .Any(c =>
        //               (contenedor == null || c.Contenedor == contenedor)
        //               && c.EstadoContenedor != "T"
        //               && c.EstadoContenedor != "C"
        //               && c.IdEstadoContenedor != 5
        //               && c.IdEstadoContenedor != 6
        //               && c.Activo
        //            );
        //}
        //public bool CargaInicial()
        //{


        //    UtileriasCifrados clsCifrados = new UtileriasCifrados();
        //    string strError = "";

        //    CatTipoPuesto objTipoPuesto = new CatTipoPuesto();
        //    objTipoPuesto.Puesto = "Administrador Sistema";
        //    objTipoPuesto.Descripcion = "Administrador";
        //    objTipoPuesto.Activo = true;


        //    _db.catTipoPuesto.Add(objTipoPuesto);

        //    CatUsuarios objCatUsuarios = new CatUsuarios();
        //    objCatUsuarios.Nombre = "Administrador";
        //    objCatUsuarios.ApellidoPaterno = "Administrador";
        //    objCatUsuarios.ApellidoMaterno = "Administrador";
        //    objCatUsuarios.Usuario = "admin";
        //    string strSalt = "";
        //    objCatUsuarios.passSistema = clsCifrados.ComputeSha256HashWithSalt("AutologVeracruz", out strSalt);
        //    objCatUsuarios.salt = strSalt;
        //    objCatUsuarios.Correo = "gerencia-ti.autolog.com.mx";
        //    objCatUsuarios.Activo = true;
        //    objCatUsuarios.Puesto = "Administrador";
        //    objCatUsuarios.RFC = "ADMIN07102024";
        //    objCatUsuarios.Telefono = "2299098960";
        //    objCatUsuarios.IdCatTipoPuesto = 1;
        //    _db.catUsuarios.Add(objCatUsuarios);

        //    Guardar(out strError);

        //    CatEmpresas objCatEmpresas = new CatEmpresas();
        //    objCatEmpresas.RazonSocial = "ALO";
        //    objCatEmpresas.RFC = "NAD4565341E1";
        //    objCatEmpresas.Correo = "gerencia-ti.autolog.com.mx";
        //    objCatEmpresas.telefono = "2299895060";
        //    objCatEmpresas.Calle = "Av. 5 Mayo";
        //    objCatEmpresas.NumeroExterior = "150";
        //    objCatEmpresas.Colonia = "Centro";
        //    objCatEmpresas.CodigoPostal = "91700";
        //    objCatEmpresas.Estado = "Veracruz";
        //    objCatEmpresas.Ciudad = "Veracruz";
        //    objCatEmpresas.Activo = true;
        //    objCatEmpresas.FechaRegistro = DateTime.Now;
        //    objCatEmpresas.IdUsuarioRegistro = 1;
        //    _db.catEmpresas.Add(objCatEmpresas);
        //    Guardar(out strError);

        //    CatReferenciaEstado objCatRef = new CatReferenciaEstado();
        //    objCatRef.Nombre = "Abierto";
        //    objCatRef.Clave = "A";
        //    objCatRef.Descripcion = "Abierto";
        //    objCatRef.Activo = true;
        //    objCatRef.FechaRegistro = DateTime.Now;
        //    objCatRef.IdUsuarioRegistro = 1;
        //    _db.catReferenciaEstado.Add(objCatRef);

        //    objCatRef = new CatReferenciaEstado();
        //    objCatRef.Nombre = "Proceso";
        //    objCatRef.Descripcion = " En proceso";
        //    objCatRef.Clave = "P";
        //    objCatRef.Activo = true;
        //    objCatRef.FechaRegistro = DateTime.Now;
        //    objCatRef.IdUsuarioRegistro = 1;
        //    _db.catReferenciaEstado.Add(objCatRef);

        //    objCatRef = new CatReferenciaEstado();
        //    objCatRef.Nombre = "Cerrado";
        //    objCatRef.Clave = "C";
        //    objCatRef.Descripcion = "Cerrado";
        //    objCatRef.Activo = true;
        //    objCatRef.FechaRegistro = DateTime.Now;
        //    objCatRef.IdUsuarioRegistro = 1;
        //    _db.catReferenciaEstado.Add(objCatRef);

        //    objCatRef = new CatReferenciaEstado();
        //    objCatRef.Nombre = "Cancelado";
        //    objCatRef.Clave = "X";
        //    objCatRef.Activo = true;
        //    objCatRef.Descripcion = "Cancelado";
        //    objCatRef.FechaRegistro = DateTime.Now;
        //    objCatRef.IdUsuarioRegistro = 1;
        //    _db.catReferenciaEstado.Add(objCatRef);
        //    Guardar(out strError);

        //    CatPaises objPaises = new CatPaises();
        //    objPaises.Nombre = "México";
        //    objPaises.Activo = true;
        //    objPaises.ClavePaisSAT = "MX";
        //    objPaises.FechaRegistro = DateTime.Now;
        //    _db.catPaises.Add(objPaises);
        //    Guardar(out strError);

        //    CatPaisEstados objcatPaisEstados = new CatPaisEstados();
        //    objcatPaisEstados.Nombre = "Veracruz";
        //    objcatPaisEstados.Activo = true;
        //    objcatPaisEstados.CodEstadoSAT = "30";
        //    objcatPaisEstados.FechaRegistro = DateTime.Now;
        //    objcatPaisEstados.IdCatPais = 1;
        //    _db.catPaisEstados.Add(objcatPaisEstados);
        //    Guardar(out strError);

        //    CatAduana objCatAduana = new CatAduana();
        //    objCatAduana.Nombre = "VERACRUZ";
        //    objCatAduana.Aduana = 43;
        //    objCatAduana.Seccion = 0;
        //    objCatAduana.Acronimo = "VCZ";
        //    objCatAduana.Activo = true;
        //    objCatAduana.FechaRegistro = DateTime.Now;
        //    objCatAduana.IdUsuarioRegistro = 1;
        //    objCatAduana.IdCatPais = 1;
        //    objCatAduana.IdCatPaisEstados = 1;
        //    _db.catAduana.Add(objCatAduana);
        //    Guardar(out strError);

        //    CatClientes objCatCliente = new CatClientes();
        //    objCatCliente.Ciudad = "VERACRUZ";
        //    objCatCliente.Estado = "VERACRUZ";
        //    objCatCliente.Calle = "Calle";
        //    objCatCliente.CodigoPostal = "91700";
        //    objCatCliente.Colonia = "CENTRO";
        //    objCatCliente.Correo = "gerencia.ti@autolog.com.mx";
        //    objCatCliente.NumeroExterior = "605";
        //    objCatCliente.NumeroInterior = "A";
        //    objCatCliente.RazonSocial = "Automotive Logistics";
        //    objCatCliente.RFC = "ALO3432231W1";
        //    objCatCliente.telefono = "2299890060";
        //    objCatCliente.Colonia = "Centro";
        //    objCatCliente.Acronimo = "ALO";
        //    objCatCliente.Activo = true;
        //    objCatCliente.FechaRegistro = DateTime.Now;
        //    objCatCliente.IdUsuarioRegistro = 1;
        //    objCatCliente.IdCatPaises = 1;
        //    objCatCliente.IdCatPaisEstados = 1;
        //    _db.catClientes.Add(objCatCliente);

        //    objCatCliente = new CatClientes();
        //    objCatCliente.Ciudad = "VERACRUZ";
        //    objCatCliente.Estado = "VERACRUZ";
        //    objCatCliente.Calle = "Calle";
        //    objCatCliente.CodigoPostal = "91800";
        //    objCatCliente.Colonia = "CENTRO";
        //    objCatCliente.Correo = "gerencia.ti@autolog.com.mx";
        //    objCatCliente.NumeroExterior = "605";
        //    objCatCliente.NumeroInterior = "A";
        //    objCatCliente.RazonSocial = "NAD";
        //    objCatCliente.Acronimo = "NAD";
        //    objCatCliente.RFC = "NAD4565341E1";
        //    objCatCliente.telefono = "3333333333";
        //    objCatCliente.Colonia = "Centro";
        //    objCatCliente.Activo = true;
        //    objCatCliente.FechaRegistro = DateTime.Now;
        //    objCatCliente.IdUsuarioRegistro = 1;
        //    objCatCliente.IdCatPaises = 1;
        //    objCatCliente.IdCatPaisEstados = 1;

        //    _db.catClientes.Add(objCatCliente);
        //    Guardar(out strError);


        //    CatProveedores objProv = new CatProveedores();
        //    objProv.Ciudad = "VERACRUZ";
        //    objProv.Estado = "VERACRUZ";
        //    objProv.Calle = "Calle";
        //    objProv.CodigoPostal = "91700";
        //    objProv.Colonia = "CENTRO";
        //    objProv.Correo = "giovanny.rivera@hhtransportes.com.mx";
        //    objProv.NumeroExterior = "700";
        //    objProv.NumeroInterior = "A";
        //    objProv.RazonSocial = "H.H. TRANSPORTES";
        //    objProv.RFC = "HTR060210FJ9";
        //    objProv.telefono = "2299890060";
        //    objProv.Colonia = "Centro";
        //    objProv.Acronimo = "ALO";
        //    objProv.Activo = true;
        //    objProv.FechaRegistro = DateTime.Now;
        //    objProv.IdUsuarioRegistro = 1;
        //    objProv.IdCatPaises = 1;
        //    objProv.IdCatPaisEstados = 1;
        //    _db.catProveedores.Add(objProv);
        //    Guardar(out strError);

        //    CatTransportistas objProvT = new CatTransportistas();
        //    objProvT.Ciudad = "VERACRUZ";
        //    objProvT.Estado = "VERACRUZ";
        //    objProvT.Calle = "Calle";
        //    objProvT.CodigoPostal = "91700";
        //    objProvT.Colonia = "CENTRO";
        //    objProvT.Correo = "giovanny.rivera@hhtransportes.com.mx";
        //    objProvT.NumeroExterior = "700";
        //    objProvT.NumeroInterior = "A";
        //    objProvT.RazonSocial = "H.H. TRANSPORTES";
        //    objProvT.RFC = "HTR060210FJ9";
        //    objProvT.telefono = "2299890060";
        //    objProvT.Colonia = "Centro";
        //    objProvT.Acronimo = "ALO";
        //    objProvT.Activo = true;
        //    objProvT.FechaRegistro = DateTime.Now;
        //    objProvT.IdUsuarioRegistro = 1;
        //    objProvT.IdCatPaises = 1;
        //    objProvT.IdCatPaisEstados = 1;

        //    _db.catTransportistas.Add(objProvT);
        //    Guardar(out strError);

        //    CatLineaNegocio objCatLineaNegocio = new CatLineaNegocio();
        //    objCatLineaNegocio.Nombre = "VACIOS";
        //    objCatLineaNegocio.Acronimo = "VC";
        //    objCatLineaNegocio.Activo = true;
        //    objCatLineaNegocio.FechaRegistro = DateTime.Now;
        //    objCatLineaNegocio.IdUsuarioRegistro = 1;
        //    _db.catLineaNegocio.Add(objCatLineaNegocio);
        //    Guardar(out strError);

        //    CatSistemas objCatSistemas = new CatSistemas();
        //    objCatSistemas.nombre = "ALO Logística";
        //    objCatSistemas.userSistema = "ALOgistics";
        //    strSalt = "";
        //    objCatSistemas.passSistema = clsCifrados.ComputeSha256HashWithSalt("0riwUGuyU7uxe2rab9ep", out strSalt);
        //    objCatSistemas.salt = strSalt;
        //    objCatSistemas.IdCatCliente = 1;
        //    objCatSistemas.Activo = true;
        //    objCatSistemas.rol = "Admin";

        //    _db.catSistemas.Add(objCatSistemas);
        //    Guardar(out strError);

        //    objCatSistemas = new CatSistemas();
        //    objCatSistemas.nombre = "NAD Logística";
        //    objCatSistemas.userSistema = "NADLogistica";
        //    strSalt = "";
        //    objCatSistemas.passSistema = clsCifrados.ComputeSha256HashWithSalt("k96lsVIqnwUcC0r", out strSalt);
        //    objCatSistemas.salt = strSalt;
        //    objCatSistemas.IdCatCliente = 1;
        //    objCatSistemas.rol = "Externo";
        //    objCatSistemas.Activo = true;

        //    _db.catSistemas.Add(objCatSistemas);
        //    Guardar(out strError);

        //    CatSucursales objCatSucursales = new CatSucursales();
        //    objCatSucursales.Nombre = "Veracruz";
        //    objCatSucursales.RFC = "ALORFC";
        //    objCatSucursales.Activo = true;
        //    objCatSucursales.FechaRegistro = DateTime.Now;
        //    objCatSucursales.IdUsuarioRegistro = 1;
        //    _db.catSucursales.Add(objCatSucursales);
        //    Guardar(out strError);

        //    CatDocumentos objCatDocumentos = new CatDocumentos();
        //    objCatDocumentos.Nombre = "EIR";
        //    objCatDocumentos.IdUsuarioRegistro = 1;
        //    objCatDocumentos.FechaRegistro = DateTime.Now;
        //    objCatDocumentos.Activo = true;
        //    _db.catDocumento.Add(objCatDocumentos);
        //    Guardar(out strError);

        //    CatServicios objCatServicios = new CatServicios();
        //    objCatServicios.Nombre = "Maniobras";
        //    objCatServicios.Descripcion = "Servicio de Maniobra completo";
        //    objCatServicios.Activo = true;
        //    objCatServicios.FechaRegistro = DateTime.Now;
        //    objCatServicios.IdUsuarioRegistro = 1;
        //    _db.catServicios.Add(objCatServicios);
        //    Guardar(out strError);



        //    return Guardar(out strError);

        //}

        //public async Task<RespuestaGenericaDTO> ValidarSolicitudAsync(SolTicketDTO solTicket)
        //{

        //    var respuesta = new RespuestaGenericaDTO();
        //    var errores = new List<string>();

        //    try
        //    {
        //        using (var connection = _db.Database.GetDbConnection() as SqlConnection)
        //        {
        //            await connection.OpenAsync();
        //            using (var command = new SqlCommand("VACIOS.ValidarTicket", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Parámetros de entrada
        //                AgregarParametro(command, "@IdCatEmpresa", solTicket.IdCatEmpresa);
        //                AgregarParametro(command, "@IdCatLineaNegocio", solTicket.IdCatLineaNegocio);
        //                AgregarParametro(command, "@IdCatAduana", solTicket.IdCatAduana);
        //                AgregarParametro(command, "@IdCatClienteSolicitante", solTicket.IdCatClienteSolicitante);
        //                AgregarParametro(command, "@IdCatServicio", solTicket.IdCatServicio);
        //                AgregarParametro(command, "@IdCatUsuario", solTicket.IdCatUsuario);
        //                AgregarParametro(command, "@RfcClienteFacturar", solTicket.RfcCLienteFacturar);
        //                AgregarParametro(command, "@Ticket", solTicket.Ticket);
        //                AgregarParametro(command, "@Moneda", solTicket.Moneda);
        //                AgregarParametro(command, "@RefenciaCliente", solTicket.ReferenciaCliente);
        //                AgregarParametro(command, "@ReferenciaFacturacion", solTicket.ReferenciaClienteFacturar);
        //                AgregarParametro(command, "@Contenedor", solTicket.Contenedor);
        //                AgregarParametro(command, "@ClaveTipoContenedor", solTicket.ClaveTipoContenedor);

        //                // Parámetros de salida
        //                var idClienteFacturarParam = command.Parameters.Add("@IdCatClienteFacturar", SqlDbType.Int);
        //                idClienteFacturarParam.Direction = ParameterDirection.Output;

        //                var clienteRazonSocialParam = command.Parameters.Add("@ClienteRazonSocial", SqlDbType.VarChar, -1);
        //                clienteRazonSocialParam.Direction = ParameterDirection.Output;

        //                // Ejecutar el procedimiento almacenado
        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        var mensajeError = reader[0]?.ToString();
        //                        if (!string.IsNullOrEmpty(mensajeError))
        //                        {
        //                            errores.Add(mensajeError);
        //                        }
        //                    }
        //                }

        //                // Capturar valores de salida
        //                solTicket.IdCatClienteFacturar = idClienteFacturarParam.Value != DBNull.Value ? (int)idClienteFacturarParam.Value : 0;
        //                solTicket.RazonSocialCLienteFacturar = clienteRazonSocialParam.Value != DBNull.Value ? clienteRazonSocialParam.Value.ToString() : "";

        //                // Construcción de la respuesta
        //                respuesta.StatusCode = errores.Any() ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
        //                respuesta.lstrErrorMessages = errores;
        //                respuesta.Entidad = solTicket;
        //                respuesta.IsSuccess = !errores.Any();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.StatusCode = HttpStatusCode.InternalServerError;
        //        respuesta.lstrErrorMessages = new List<string> { ex.Message };
        //        respuesta.IsSuccess = false;
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }

        //    return respuesta;
        //}

        //public async Task<RespuestaGenericaDTO> GuardarSolicitudAsync(SolTicketDTO solTicket)
        //{
        //    var respuesta = new RespuestaGenericaDTO();
        //    var errores = new List<string>();

        //    try
        //    {
        //        using (var connection = _db.Database.GetDbConnection() as SqlConnection)
        //        {
        //            await connection.OpenAsync();
        //            using (var command = new SqlCommand("VACIOS.GenerarSolicitud", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Parámetros de entrada
        //                command.Parameters.AddWithValue("@IdCatEmpresa", (object)solTicket.IdCatEmpresa ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@IdCatLineaNegocio", (object)solTicket.IdCatLineaNegocio ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@IdCatAduana", (object)solTicket.IdCatAduana ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@IdCatProyecto", (object)solTicket.IdCatProyecto ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@IdCatClienteSolicitante", (object)solTicket.IdCatClienteSolicitante ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@IdCatClienteFacturar", (object)solTicket.IdCatClienteFacturar ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@IdCatServicio", (object)solTicket.IdCatServicio ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@IdCatUsuario", (object)solTicket.IdCatUsuario ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@Ticket", (object)solTicket.Ticket ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@Moneda", (object)solTicket.Moneda ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@RefenciaCliente", (object)solTicket.ReferenciaCliente ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@ReferenciaFacturacion", (object)solTicket.ReferenciaClienteFacturar ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@Contenedor", (object)solTicket.Contenedor ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@ClaveTipoContenedor", (object)solTicket.ClaveTipoContenedor ?? DBNull.Value);

        //                // Parámetros de salida
        //                var idOrdenParam = new SqlParameter("@IdOrden", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //                var idPeticionContenedorParam = new SqlParameter("@IdPeticionContenedor", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //                var referenciaAloParam = new SqlParameter("@Referencia", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
        //                var referenciaClienteParam = new SqlParameter("@ReferenciaClienteExt", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
        //                var mensajeParam = new SqlParameter("@Mensage", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };


        //                command.Parameters.Add(idOrdenParam);
        //                command.Parameters.Add(idPeticionContenedorParam);
        //                command.Parameters.Add(referenciaAloParam);
        //                command.Parameters.Add(referenciaClienteParam);
        //                command.Parameters.Add(mensajeParam);

        //                await command.ExecuteNonQueryAsync();

        //                // Capturamos los valores de salida después de la ejecución del procedimiento
        //                int idOrden = idOrdenParam.Value != DBNull.Value ? (int)idOrdenParam.Value : 0;
        //                int idPeticionContenedor = idPeticionContenedorParam.Value != DBNull.Value ? (int)idPeticionContenedorParam.Value : 0;
        //                string referenciaAlo = referenciaAloParam.Value != DBNull.Value ? referenciaAloParam.Value.ToString() : "";
        //                string referenciaCliente = referenciaClienteParam.Value != DBNull.Value ? referenciaClienteParam.Value.ToString() : "";
        //                string respuestaMensaje = mensajeParam.Value != DBNull.Value ? mensajeParam.Value.ToString() : "";

        //                solTicket.ReferenciaAlo = referenciaAlo;
        //                solTicket.ReferenciaCliente = referenciaCliente;
        //                // Construimos la respuesta
        //                respuesta.StatusCode = errores.Any() ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
        //                respuesta.strMensaje = respuestaMensaje;
        //                respuesta.Entidad = solTicket;
        //                respuesta.IsSuccess = !errores.Any();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.StatusCode = HttpStatusCode.InternalServerError;
        //        respuesta.lstrErrorMessages = new List<string> { ex.Message };
        //        respuesta.IsSuccess = false;
        //    }

        //    return respuesta;
        //}

        //private void AgregarParametro(SqlCommand command, string nombre, object valor)
        //{
        //    command.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        //}

        //public bool Guardar(out string strError)
        //{
        //    try
        //    {
        //        strError = "";
        //        return _db.SaveChanges() >= 0 ? true : false;
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        // Manejo de errores relacionados con la actualización de la base de datos
        //        //return StatusCode(500, $"Database update error: {dbEx.Message}");
        //        strError = dbEx.Message;
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return false;
        //    }
        //}
        #endregion METODOS COMENTADOS

        #region Métodos para la generación de anticipos

        public async Task<RespuestaGenericaDTO> GenerarAnticipo(Ordenes orden)
        {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();
            _spfuncionRepo = new DbSPFuncionesRepositorio(_db);
            List<string> errores = new List<string>();
            var usuarioToken = GetUsuarioToken();

            if (orden == null)
            {
                respuesta.lstrErrorMessages.Add($"No se encontró la orden número {orden.IdOrden}");
                return respuesta;
            }

            var detallesAnticipo = new List<AnticiposDet>();

            foreach (var referencia in orden.peticionesReferencias)
            {
                foreach (var contenedor in referencia.Contenedores)
                {
                    foreach (var servicio in contenedor.Servicios)
                    {

                        respuesta = await _spfuncionRepo.ContenedorServicioExisteAnticipoAsync(contenedor.IdContenedor, servicio.IdTipoServicio);

                        if (Convert.ToBoolean(respuesta.Entidad))
                        {
                            errores.Add(respuesta.strMensaje);
                            continue;
                        }

                        var requiereAnticipo = await _spfuncionRepo.ProveedorRequiereAnticipoAsync(
                            orden.IdCatAduana ?? 0,
                            contenedor.PatioId ?? 0,
                            servicio.IdTipoServicio
                        );

                        if (!requiereAnticipo) continue;

                        var respuestaTarifa = await _spfuncionRepo.GetTarifaProveedorAsync(
                            orden.IdCatAduana ?? 0,
                            contenedor.PatioId ?? 0,
                            servicio.IdTipoServicio
                        );

                        double tarifa = respuestaTarifa.Entidad == null ? 0 : double.Parse(respuestaTarifa.Entidad.ToString());

                        if (tarifa <= 0)
                        {
                            errores.Add(respuestaTarifa.strMensaje);
                            continue;
                        }


                        var respuestaCuentaBancaria = await _spfuncionRepo.GetDatosBancariosProveedorAsync(
                            contenedor.PatioId ?? 0,
                            orden.IdCatAduana ?? 0
                        );
                        var cuentaBancaria = (CuentaBancariaProveedorDTO)respuestaCuentaBancaria.Entidad;
                        if (respuestaCuentaBancaria.lstrErrorMessages.Count() > 0)
                        {
                            errores.AddRange(respuestaCuentaBancaria.lstrErrorMessages);
                            continue;
                        }

                        var respuestaReferenciaAlo = await _spfuncionRepo.GetReferenciaAloPorReferenciaClienteAsync(servicio.ReferenciaClienteFacturar);

                        if (string.IsNullOrEmpty(respuestaReferenciaAlo.Entidad.ToString()))
                        {
                            errores.Add(respuestaReferenciaAlo.strMensaje);
                            continue;
                        }

                        string referenciaNumerica = GenerarReferenciaNumerica(respuestaReferenciaAlo.Entidad.ToString());

                        detallesAnticipo.Add(new AnticiposDet
                        {
                            IdLineaNegocio = orden.IdCatLineaNegocio,
                            IdOrden = orden.IdOrden,
                            IdProveedor = cuentaBancaria.IdCatProveedor,
                            RFC = "___",
                            CuentaClabe = cuentaBancaria.Clabe,
                            RefProveedor = contenedor.Contenedor,
                            IdCatServicios = servicio.IdTipoServicio,
                            CveServicio = servicio.IdTipoServicio.ToString(),
                            IdCarga = contenedor.IdContenedor,
                            Importe = tarifa,
                            RefTransferencia = "123",
                            FechaAplicacion = DateTime.Now,
                            TipoProv = 1,
                            //refNumero = cuentaBancaria.ReferenciaNumerica,
                            refNumero = referenciaNumerica,
                            CuentaBancaria = 0,
                            SaldoAplicado = 0,
                            txt_app_monex_descargado = "",
                            Clave1G = 0,
                            Estado1G = ""
                        });
                    }
                }
            }

            if (!detallesAnticipo.Any())
            {
                respuesta.lstrErrorMessages = errores;
                return respuesta;
            }

            var anticiposEnc = new AnticiposEnc
            {
                IdUsuarioSolicita = usuarioToken.IdCatUsuario,
                IdUsuarioAutoriza = usuarioToken.IdCatUsuario,
                FechaAutorizacion = DateTime.Now,
                //FechaEnvio = DateTime.Now,
                Enviado = false,
                Autorizado = false,
                IdTipoAnticipo = 1,
                EstadoAnticipo = 1,
                IdTipoMoneda = 1,
                TipoCambio = 1,
                Clave1G = 0,
                Estado1G = ""
            };

            anticiposEnc = await _spfuncionRepo.GuardarAnticipoEncabezadoAsync(anticiposEnc);

            foreach (var detalle in detallesAnticipo)
            {
                detalle.IdAnticiposEnc = anticiposEnc.IdAnticiposEnc;
                await _spfuncionRepo.GuardarAnticipoDetalleAsync(detalle);
            }

            respuesta.lstrErrorMessages = errores;
            return respuesta;
        }

        public static string GenerarReferenciaNumerica(string referencia)
        {

            if (string.IsNullOrWhiteSpace(referencia)) return string.Empty;

            try
            {
                var ultimaParte = referencia.Split('/').LastOrDefault();

                if (string.IsNullOrWhiteSpace(ultimaParte)) return string.Empty;

                var referenciaNumerica = ultimaParte.Replace("-", "");

                return referenciaNumerica;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GenerarReferenciaNumerica: {ex.Message}");
                return string.Empty;
            }
        }




        #endregion Métodos para la generación de anticipos

    }
}