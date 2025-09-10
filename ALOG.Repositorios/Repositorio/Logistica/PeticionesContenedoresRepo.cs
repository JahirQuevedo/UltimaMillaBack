using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Utilerias;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.Logistica
{

    public class PeticionesContenedoresRepo : GenericoRepositorio<PeticionesContenedores>, IPeticionesContenedoresRepo
    {
        private readonly ApplicationDbContext _db;
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();
        private readonly UtileriasRespuestas objUtileriasRespuestas = new UtileriasRespuestas();


        public PeticionesContenedoresRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;


            #region RESPUESTA_GENERICA
            _respuestaGenericaDTO = new RespuestaGenericaDTO();
            _respuestaGenericaDTO.lstrErrorMessages = new List<string>();
            _respuestaGenericaDTO.strMensaje = null;
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.Entidades = null;
            _respuestaGenericaDTO.Entidad = null;
            _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
            #endregion RESPUESTA_GENERICA
        }


        public Task<bool> actualizarContenedor(PeticionesContenedores pContenedor)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaGenericaDTO> actualizarNavieraContenedor(PeticionesContenedores pContenedor)
        {
            #region VARIABLES
            RespuestaGenericaDTO objRespuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuestaIncorrecta();
            #endregion VARIABLES

            try
            {
                //Existe Naviera
                var existeNaviera = await _db.catNavieras.AnyAsync(x => x.IdCatNaviera == pContenedor.Naviera_Id);
                if (existeNaviera)
                {
                    var objNaviera = await _db.catNavieras.Where(x => x.IdCatNaviera == pContenedor.Naviera_Id).FirstOrDefaultAsync();
                    var objContenedor = await _db.peticionesContenedores.Where(x => x.IdContenedor == pContenedor.IdContenedor
                    && x.IdReferencia == pContenedor.IdReferencia
                    && x.Activo == true).FirstOrDefaultAsync();
                    objContenedor.Naviera_Id = objNaviera.IdCatNaviera;
                    objContenedor.Naviera_RFC = objNaviera.RFC;
                    objContenedor.Naviera_RazonSocial = objNaviera.RazonSocial;
                    _db.Update(objContenedor);
                    _db.SaveChanges();

                    objRespuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuestaCorrecta();
                    return objRespuestaGenericaDTO;
                }
                objRespuestaGenericaDTO.strMensaje = "No existe la naviera";
                return objRespuestaGenericaDTO;
            }
            catch (Exception ex)
            {
                objRespuestaGenericaDTO.strMensaje = ex.Message;
                return objRespuestaGenericaDTO;
            }
        }

        //public async Task<bool> actualizarPatioContenedor(PeticionesContenedores pContenedor)
        //{
        //    try
        //    {

        //        var existe = await _db.catPatios.AnyAsync(x => x.IdCatPatios == pContenedor.PatioId);
        //        if (existe)
        //        {
        //            var objCatPatios = await _db.catPatios
        //                                            .Include(x => x.catProveedoresPatios)
        //                                                .ThenInclude(obj => obj.catProveedores)
        //                                            .Where(x => x.IdCatPatios == pContenedor.PatioId).FirstOrDefaultAsync();


        //            //var objContenedor = await _db.peticionesContenedores.Where(x => x.IdContenedor == pContenedor.IdContenedor && x.IdReferencia == pContenedor.IdReferencia).FirstOrDefaultAsync();
        //            var objContenedor = await _db.peticionesContenedores.Where(x => x.IdContenedor == pContenedor.IdContenedor && x.IdReferencia == pContenedor.IdReferencia).FirstOrDefaultAsync();

        //           var objContenedorInfo = await _db.peticionesContenedores
        //                                                .Include(c => c.PeticionesReferencias)
        //                                                    .ThenInclude(r => r.ordenes)
        //                                                .Where(c => c.IdContenedor == pContenedor.IdContenedor).FirstOrDefaultAsync();
        //            if (objContenedorInfo.PeticionesReferencias.ordenes.IdCatAduana != objCatPatios.catProveedoresPatios.IdCatAduana)
        //                return false;

        //            objContenedor.PatioId = objCatPatios.IdCatPatios;
        //            objContenedor.Patio_RazonSocial = objCatPatios.RazonSocial;
        //            objContenedor.Patio_RFC = objCatPatios.catProveedoresPatios.catProveedores.RFC;

        //            _db.Update(objContenedor);
        //            _db.SaveChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public async Task<bool> ActualizarPatioContenedor(PeticionesContenedores pContenedor)
        //{
        //    RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();
        //    List<string> errores = new List<string>();

        //    if (pContenedor == null) {
        //        return false;
        //    }

        //    // 1. Cargar el contenedor con su referencia y orden para conocer la aduana
        //    var objContenedor = await _db.peticionesContenedores
        //                                .Include(c => c.PeticionesReferencias)
        //                                    .ThenInclude(r => r.ordenes)
        //                                .FirstOrDefaultAsync(c =>
        //                                                         c.IdContenedor == pContenedor.IdContenedor &&
        //                                                         c.IdReferencia == pContenedor.IdReferencia
        //                                );

        //    if (objContenedor == null) {
        //        errores.Add($"El contenedor {objContenedor.Contenedor} no está registrado en el sistema.");
        //        return false;
        //    }

        //    // Extraer la aduana de la orden
        //    var aduanaOrden = objContenedor.PeticionesReferencias.ordenes.IdCatAduana;

        //    // 2. Traer el registro de CatProveedoresPatios que liga ese patio con la misma aduana
        //    var proveedorPatio = await _db.catProveedoresPatios
        //                                    .Include(pp => pp.catPatios)
        //                                    .Include(pp => pp.catProveedores)
        //                                    .FirstOrDefaultAsync(pp =>
        //                                                              pp.IdCatPatio == pContenedor.PatioId &&
        //                                                              pp.IdCatAduana == aduanaOrden
        //                                    );

        //    if (proveedorPatio == null) {
        //        errores.Add($"El patio {objContenedor.Patio_RazonSocial} no está registrado en el sistema.");
        //        return false; // No existe ese patio en esa aduana
        //    }

        //    // 3. Actualizar solo los tres campos necesarios
        //    objContenedor.PatioId = proveedorPatio.IdCatPatio;
        //    objContenedor.Patio_RazonSocial = proveedorPatio.catPatios.RazonSocial;
        //    objContenedor.Patio_RFC = proveedorPatio.catProveedores.RFC;

        //    // Marcar únicamente esas propiedades como modificadas
        //    var entry = _db.Entry(objContenedor);
        //    entry.Property(c => c.PatioId).IsModified = true;
        //    entry.Property(c => c.Patio_RazonSocial).IsModified = true;
        //    entry.Property(c => c.Patio_RFC).IsModified = true;

        //    // 4. Guardar cambios
        //    await _db.SaveChangesAsync();
        //    return true;
        //}

        public async Task<RespuestaGenericaDTO> AsignarPatioContenedor(PeticionesContenedores pContenedor)
        {

            var respuesta = new RespuestaGenericaDTO
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                IsSuccess = false,
                lstrErrorMessages = new List<string>()
            };

            // Validaciones iniciales
            if (pContenedor == null)
            {
                respuesta.lstrErrorMessages.Add("Ocurrió un problema al momento de actualizar uno o más contenedores. Por favor, intenta nuevamente.");
                return respuesta;
            }

            if (pContenedor.IdContenedor <= 0 || pContenedor.IdReferencia <= 0 || pContenedor.PatioId <= 0)
            {
                respuesta.lstrErrorMessages.Add($"No se encontró información sobre el contenedor {pContenedor.Contenedor}");
                return respuesta;
            }

            // 1. Cargar el contenedor con su referencia y orden para conocer la aduana
            var objContenedor = await _db.peticionesContenedores
                                          .Include(c => c.PeticionesReferencias)
                                              .ThenInclude(r => r.ordenes)
                                          .FirstOrDefaultAsync(c =>
                                              c.IdContenedor == pContenedor.IdContenedor &&
                                              c.IdReferencia == pContenedor.IdReferencia);

            if (objContenedor == null || objContenedor.PeticionesReferencias == null || objContenedor.PeticionesReferencias.ordenes == null)
            {
                respuesta.lstrErrorMessages.Add($"No se encontró el contenedor {pContenedor.Contenedor} o falta información de referencia u orden.");
                return respuesta;
            }

            int idCatAduana = (int)objContenedor.PeticionesReferencias.ordenes.IdCatAduana;

            // 2. Traer el registro de CatProveedoresPatios que liga ese patio con la misma aduana
            var proveedorPatio = await _db.catProveedoresPatios
                                          .Include(pp => pp.catPatios)
                                          .Include(pp => pp.catProveedores)
                                          .FirstOrDefaultAsync(pp =>
                                              pp.IdCatPatio == pContenedor.PatioId &&
                                              pp.IdCatAduana == idCatAduana &&
                                              pp.Activo == true);

            if (proveedorPatio == null)
            {
                respuesta.lstrErrorMessages.Add($"El patio {pContenedor.Patio_RazonSocial} no está registrado en la aduana indicada.");
                return respuesta;
            }

            // 3. Actualizar solo los tres campos necesarios si cambiaron
            bool cambios = false;

            if (objContenedor.PatioId != proveedorPatio.IdCatPatio)
            {
                objContenedor.PatioId = proveedorPatio.IdCatPatio;
                _db.Entry(objContenedor).Property(c => c.PatioId).IsModified = true;
                cambios = true;
            }

            if (objContenedor.Patio_RazonSocial != proveedorPatio.catPatios?.RazonSocial)
            {
                objContenedor.Patio_RazonSocial = proveedorPatio.catPatios?.RazonSocial;
                _db.Entry(objContenedor).Property(c => c.Patio_RazonSocial).IsModified = true;
                cambios = true;
            }

            if (objContenedor.Patio_RFC != proveedorPatio.catProveedores?.RFC)
            {
                objContenedor.Patio_RFC = proveedorPatio.catProveedores?.RFC;
                _db.Entry(objContenedor).Property(c => c.Patio_RFC).IsModified = true;
                cambios = true;
            }

            if (!cambios)
            {
                respuesta.StatusCode = System.Net.HttpStatusCode.OK;
                respuesta.IsSuccess = true;
                return respuesta;
            }

            // 4. Guardar cambios con control de errores
            try
            {
                await _db.SaveChangesAsync();
                respuesta.StatusCode = System.Net.HttpStatusCode.OK;
                respuesta.IsSuccess = true;
            }
            catch (Exception ex)
            {
                respuesta.lstrErrorMessages.Add($"Error al guardar los cambios: {ex.Message}");
                respuesta.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return respuesta;
        }


        public async Task<bool> actualizarTransportistaContenedor(PeticionesContenedores pContenedor)
        {
            try
            {
                //Existe Naviera
                var existe = await _db.catTransportistas.AnyAsync(x => x.IdCatTransportista == pContenedor.IdCatTransportista);
                if (existe)
                {
                    var obj = await _db.catTransportistas
                        .Where(x => x.IdCatTransportista == pContenedor.IdCatTransportista).FirstOrDefaultAsync();
                    var objContenedor = await _db.peticionesContenedores.Where(x => x.IdContenedor == pContenedor.IdContenedor && x.IdReferencia == pContenedor.IdReferencia).FirstOrDefaultAsync();
                    objContenedor.IdCatTransportista = obj.IdCatTransportista;
                    _db.Update(objContenedor);
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool contenedorActivo(int IdContenedor)
        {
            throw new NotImplementedException();
        }

        public bool contenedorActivo(string sContenedor)
        {
            throw new NotImplementedException();
        }

        public async Task<PeticionesContenedores> obtenerContenedor(int pIdcontenedor)
        {
            #region Variables
            _respuestaGenericaDTO = new RespuestaGenericaDTO();
            _respuestaGenericaDTO.lstrErrorMessages = new List<string>();
            _respuestaGenericaDTO.strMensaje = null;
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _respuestaGenericaDTO.Entidad = null;
            _respuestaGenericaDTO.Entidades = null;
            #endregion Variables

            try
            {
                var objContenedor = await _db.peticionesContenedores.Include(x => x.Servicios).ThenInclude(x => x.Documentos)
                                    .Include(x => x.catReferenciaEstado)
                                    .Include(x => x.catPatios)
                                    .Include(x => x.catAduana)
                                    .Include(x => x.catNavieras)
                                    .Include(x => x.catClientes)
                                    .Include(x => x.catClientesFacturarA).Where(s => s.IdContenedor == pIdcontenedor).FirstOrDefaultAsync();

                //_respuestaGenericaDTO.IsSuccess = true;
                //_respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                //_respuestaGenericaDTO.Entidades=new List<object>();
                //_respuestaGenericaDTO.Entidades.AddRange(objContenedor);

                return objContenedor;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<ICollection<PeticionesContenedores>> obtenerContenedores(FiltroOrdenesReferenciasDTO pFiltro)
        {
            pFiltro.IdCliente = pFiltro.IdCliente == null ? 0 : pFiltro.IdCliente;
            pFiltro.IdAduana = pFiltro.IdAduana == null ? 0 : pFiltro.IdAduana;
            pFiltro.IdEmpresa = pFiltro.IdEmpresa == null ? new List<string>() : pFiltro.IdEmpresa;
            pFiltro.IdLNegocio = pFiltro.IdLNegocio == null ? 0 : pFiltro.IdLNegocio;
            pFiltro.IdOrden = pFiltro.IdOrden == null ? 0 : pFiltro.IdOrden;
            pFiltro.IdServicio = pFiltro.IdServicio == null ? 0 : pFiltro.IdServicio;
            pFiltro.Ticket = pFiltro.Ticket == null ? 0 : pFiltro.Ticket;
            pFiltro.IdPatio = pFiltro.IdPatio == null ? 0 : pFiltro.IdPatio;
            pFiltro.FechaSolicitudIni = pFiltro.FechaSolicitudIni == null ? DateTime.Now.AddDays(-7) : pFiltro.FechaSolicitudIni;
            pFiltro.FechaSolicitudFin = pFiltro.FechaSolicitudFin == null ? DateTime.Now : pFiltro.FechaSolicitudFin;


            var lstPeticionesContenedores = await _db.peticionesContenedores.Include(a => a.PeticionesReferencias).ThenInclude(d => d.ordenes)
                .Include(d => d.catReferenciaEstado)
                .Include(b => b.Servicios).ThenInclude(s => s.catServicios)
                .Include(b => b.Servicios).ThenInclude(cs => cs.catReferenciaEstado)
                .Include(b => b.Servicios).ThenInclude(pd => pd.Documentos).ThenInclude(td => td.CatDocumento)
                .Include(b => b.catPatios)
                .Where(a => (pFiltro.IdCliente <= 0 || a.PeticionesReferencias.ordenes.IdCatCliente == pFiltro.IdCliente) &&
                             (pFiltro.IdOrden <= 0 || a.PeticionesReferencias.ordenes.IdOrden == pFiltro.IdOrden) &&
                            (pFiltro.IdAduana <= 0 || a.PeticionesReferencias.ordenes.IdCatAduana == pFiltro.IdAduana) &&
                            //(pFiltro.IdEmpresa <= 0 || a.ordenes.IdCatEmpresa == pFiltro.IdEmpresa) &&
                            (pFiltro.IdEmpresa.Count <= 0 || pFiltro.IdEmpresa.Contains(a.PeticionesReferencias.ordenes.IdCatEmpresa.ToString())) &&
                            (pFiltro.IdLNegocio <= 0 || a.PeticionesReferencias.ordenes.IdCatLineaNegocio == pFiltro.IdLNegocio) &&
                            (pFiltro.IdPatio <= 0 || a.PatioId == pFiltro.IdPatio) &&
                            (pFiltro.Ticket <= 0 || a.PeticionesReferencias.Ticket == pFiltro.Ticket) &&
                            (pFiltro.IdServicio <= 0 || a.Servicios.Any(c => c.IdTipoServicio == pFiltro.IdServicio))
                            &&
                            (a.FechaRegistro >= pFiltro.FechaSolicitudIni && a.FechaRegistro <= pFiltro.FechaSolicitudFin) &&

                            (string.IsNullOrEmpty(pFiltro.Contenedor) || a.Contenedor == pFiltro.Contenedor) &&
                             (string.IsNullOrEmpty(pFiltro.Buque) || a.Buque.Contains(pFiltro.Buque)) &&
                             (string.IsNullOrEmpty(pFiltro.Ejecutivo) || a.Cliente_Solicitante.Contains(pFiltro.Ejecutivo)) &&
                             (string.IsNullOrEmpty(pFiltro.ReferenciaCliente) || a.RefenciaCliente == pFiltro.ReferenciaCliente) &&
                             (string.IsNullOrEmpty(pFiltro.ReferenciaALO) || a.PeticionesReferencias.ordenes.ReferenciaALO.Equals(pFiltro.ReferenciaALO))
                             )

               .ToListAsync();




            return lstPeticionesContenedores;

        }

        Task<bool> IPeticionesContenedoresRepo.ActualizarPatioContenedor(PeticionesContenedores pContenedor)
        {
            throw new NotImplementedException();
        }
    }
}
