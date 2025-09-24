using ALOG.Modelos.Modelos.DTO.Respuestas;
using System.Net;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Repositorios.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Repositorios.Repositorio.Logistica;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;

namespace ALOG.APILogistico.Controllers.CtrlDTLogistico
{
    //[AllowAnonymous]
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    [Route("[controller]")]
    [ApiController]
    public class SLOTorreControlController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISLOSolicitudesRepositorio _solicitudesRepositorio;

        public SLOTorreControlController(ApplicationDbContext context, ISLOSolicitudesRepositorio solicitudesRepositorio)
        {
            _context = context;
            _solicitudesRepositorio = solicitudesRepositorio;
        }

        #region SOLICITUDES_SERVICIOS        
        #region SLO_SOLICITUDES
        //Función para obtener las solicitudes con filtro
        [HttpPost("SolicitudesFiltrar")]
        public async Task<IActionResult> SLOSolicitudesObtener(FiltroSLOSolicitudes pFiltro)
        {
            ICollection<SLOSolicitudes> listado_solicitudes = await _solicitudesRepositorio.SLOSolicitudesObtener(pFiltro);
            return Ok(listado_solicitudes);

        }

        [HttpGet("SolicitudesListar")]
        public async Task<IActionResult> SLOSolicitudesListar()
        {
            ICollection<SLOSolicitudes> lista = await _solicitudesRepositorio.obtenerListaTodosGenerico();
            return Ok(lista);
        }

        [HttpPost("SolicitudesCrear")]
        public async Task<IActionResult> SLOSolicitudesCrear([FromBody] SLOSolicitudes entidad)
        {

            entidad.IdCatTipoEstado = _context.catUsuariosEmpresa
    .Any(u => u.IdCatUsuarios == entidad.IdCatUsuario && (u.idCatEmpresa == 1 || u.idCatEmpresa == 2)) ? 5 : 1;


            if (!ModelState.IsValid)
            {
                // Aquí devolvemos todos los errores de validación
                var errores = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { mensaje = "Datos inválidos", errores });
            }
            

            try
            {
                var response = await _solicitudesRepositorio.SLOSolicitudesCrear(entidad);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("SolicitudesObtener/{idSolicitud}")]
        //public async Task<IActionResult> SLOSolicitudesObtener(int idSolicitud)
        //{
        //    try
        //    {
        //        var respuestaDTO = await _solicitudesRepositorio.SLOSolicitudesObtener()
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        [HttpGet("SolicitudesEditar/{idSolicitud}")]
        public async Task<IActionResult> SLOSolicitudesEditar(int idSolicitud)
        {
            try
            {
                var respuestaDTO = await _solicitudesRepositorio.SLOSolicitudesEditar(idSolicitud);

                if (!respuestaDTO.IsSuccess || respuestaDTO.Entidad is null)
                {
                    //return StatusCode((int)respuestaDTO.StatusCode, 
                    return NotFound(new
                    {
                        mensaje = "No se encontró la solicitud",
                        errores = respuestaDTO.lstrErrorMessages
                    });
                }

                var solicitud = (SLOSolicitudes)respuestaDTO.Entidad;
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        //[HttpPut("SolicitudesActualizar")]
        //public async Task<IActionResult> SLOSolicitudesActualizar(int id,SLOSolicitudes solicitud)
        //{
        //    var respuesta = new RespuestaGenericaDTO();

        //    if (!ModelState.IsValid)
        //    {
        //        respuesta.StatusCode = HttpStatusCode.BadRequest;
        //        respuesta.lstrErrorMessages = new List<string> { "Modelo inválido." };
        //        return BadRequest(respuesta);
        //    }

        //    try
        //    {
        //        var entidadExistente = await _solicitudesRepositorio.obtenerPorIdGenerico(solicitud.IdSLOSolicitud);
        //        if (entidadExistente == null)
        //        {
        //            respuesta.StatusCode = HttpStatusCode.NotFound;
        //            respuesta.lstrErrorMessages = new List<string> { $"Entidad con ID {solicitud.IdSLOSolicitud} no encontrada." };
        //            return NotFound(respuesta);
        //        }

        //        var actualizada = await _solicitudesRepositorio.SLOSolicitudesActualizar(id, solicitud);
        //        if (actualizada == null)
        //        {
        //            respuesta.StatusCode = HttpStatusCode.InternalServerError;
        //            respuesta.lstrErrorMessages = new List<string> { "Error al actualizar la entidad." };
        //            return StatusCode(500, respuesta);
        //        }

        //        respuesta.IsSuccess = true;
        //        respuesta.StatusCode = HttpStatusCode.OK;
        //        respuesta.Entidad = actualizada;
        //        respuesta.strMensaje = "Actualización exitosa.";
        //        return Ok(respuesta);
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.StatusCode = HttpStatusCode.InternalServerError;
        //        respuesta.lstrErrorMessages = new List<string> { ex.Message };
        //        return StatusCode(500, respuesta);
        //    }
        //}

        [HttpPut("SolicitudesActualizarSolicitud/{id}")]
        public async Task<IActionResult> SLOSolicitudesActualizar(int id,[FromBody] SLOSolicitudes solicitud)
        {
            var respuesta = new RespuestaGenericaDTO();
            

            if (!ModelState.IsValid)
            {
                respuesta.StatusCode = HttpStatusCode.BadRequest;
                respuesta.lstrErrorMessages = new List<string> { "Modelo inválido." };
                return BadRequest(respuesta);
            }

            try
            {
                // validar que el id de la ruta y el del body coincidan
                if (id != solicitud.IdSLOSolicitud)
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuesta.lstrErrorMessages = new List<string> { "El ID de la ruta no coincide con el del objeto." };
                    return BadRequest(respuesta);
                }

                var entidadExistente = await _solicitudesRepositorio.obtenerPorIdGenerico(id);
                if (entidadExistente == null)
                {
                    respuesta.StatusCode = HttpStatusCode.NotFound;
                    respuesta.lstrErrorMessages = new List<string> { $"Entidad con ID {id} no encontrada." };
                    return NotFound(respuesta);
                }

                var actualizada = await _solicitudesRepositorio.SLOSolicitudesActualizar(id, solicitud);
                if (actualizada == null)
                {
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                    respuesta.lstrErrorMessages = new List<string> { "Error al actualizar la entidad." };
                    return StatusCode(500, respuesta);
                }

                respuesta.IsSuccess = true;
                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.Entidad = actualizada;
                respuesta.strMensaje = "Actualización exitosa.";
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.lstrErrorMessages = new List<string> { ex.InnerException?.Message ?? ex.Message };
                return StatusCode(500, respuesta);
            }
        }


        [HttpPut("SolicitudesAlta/{id}")]
        public async Task<IActionResult> SLOSolicitudesActivar(int id)
        {
            var respuesta = await _solicitudesRepositorio.SLOSolicitudesAlta(id);

            if (!respuesta.IsSuccess)
                return StatusCode((int)respuesta.StatusCode, respuesta);

            return Ok(respuesta);
        }

        [HttpPut("SLOSolicitudesBaja/{id}")]
        public async Task<IActionResult> SLOSolicitudesDesactivar(int id)
        {
            var respuesta = await _solicitudesRepositorio.SLOSolicitudesBaja(id);

            if (!respuesta.IsSuccess)
                return StatusCode((int)respuesta.StatusCode, respuesta);

            return Ok(respuesta);
        }


        #endregion SLO_SOLICITUDES

        #region SLO_SOLICITUDES_DETALLE
        [HttpGet("SLOSolicitudesDetalleObtener/{id}")]
        public async Task<IActionResult> SLOSolicitudesDetalleObtener(int id)
        {
            RespuestaGenericaDTO respuesta = await _solicitudesRepositorio.SLOSolicitudesDetalleObtener(id);

            if(respuesta.IsSuccess)
                return Ok(respuesta);
            else
                return StatusCode((int)respuesta.StatusCode, respuesta);
        }
        #endregion SLO_SOLICITUDES_DETALLE

        #endregion SOLICITUDES_SERVICIOS

        #region SOLICITUDES TRANSPORTES

        #region TRANSPORTES_DETALLE
        [HttpPost("CrearTransporteDetalle")]
        public async Task<IActionResult> CrearTransporteDetalle([FromBody] SLOTransporteDetalle model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            model.FechaRegistro = DateTime.Now;
            model.Activo = true;            

            _context.SLOTransporteDetalles.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerTransporteDetalle), new { id = model.IdSLOTransporteDetalle }, model);
        }

        [HttpGet("ObtenerTransporteDetalle/{id}")]
        public async Task<IActionResult> ObtenerTransporteDetalle(int id)
        {
            var entity = await _context.SLOTransporteDetalles.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();
            return Ok(entity);
        }

        [HttpGet("ListarTransporteDetalle/{id}")]
        public async Task<IActionResult> ListarTransporteDetalle(int id)
        {
            var entity = await _context.SLOTransporteDetalles
                                        
                .Where(x => x.IdSLOTransporteSolicitud == id).ToListAsync();

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }
        

        [HttpPut("ActualizarTransporteDetalle")]
        public async Task<IActionResult> ActualizarTransporteDetalle([FromBody] SLOTransporteDetalle model)
        {
            var entity = await _context.SLOTransporteDetalles.FindAsync(model.IdSLOTransporteDetalle);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.IdCatTipoEstados = model.IdCatTipoEstados;
            entity.IdCatTipoOperTransportes = model.IdCatTipoOperTransportes;
            entity.IdSLOTransporteSolicitud = model.IdSLOTransporteSolicitud;
            entity.FolioUUID = model.FolioUUID;
            entity.IdCatUsuarios = model.IdCatUsuarios;
            entity.IdSLOSolicitudDet = model.IdSLOSolicitudDet;
            entity.IdSLOSolicitud = model.IdSLOSolicitud;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("BajaTransporteDetalle/{id}")]
        public async Task<IActionResult> BajaTransporteDetalle(int id)
        {
            var entity = await _context.SLOTransporteDetalles.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPut("AltaTransporteDetalle/{id}")]
        public async Task<IActionResult> AltaTransporteDetalle(int id)
        {
            var entity = await _context.SLOTransporteDetalles.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion TRANSPORTE_DETALLE

        #region TRANSPORTES_SOLICITUD
        [HttpPost("CrearTransporteSolicitud")]
        public async Task<IActionResult> CrearTransporteSolicitud([FromBody] SLOTransporteSolicitud pSLOTransporteSolicitud)
        {
            var respuesta = new RespuestaGenericaDTO
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            var enProceso = await _context.catTipoEstados.Where(e => e.TipoEstado == "P").Select(e => e.IdCatTipoEstados).FirstOrDefaultAsync();

            if (!ModelState.IsValid)
            {
                respuesta.Entidad = ModelState;
                return StatusCode((int)respuesta.StatusCode, respuesta);
            }

            try
            {
                pSLOTransporteSolicitud.FechaRegistro = DateTime.Now;
                pSLOTransporteSolicitud.Activo = true;
                pSLOTransporteSolicitud.IdCatTipoEstados = enProceso;
                var solicitud = new SLOSolicitudes { IdSLOSolicitud = pSLOTransporteSolicitud.IdSLOSolicitud, IdCatTipoEstado = enProceso };
                _context.sloSolicitudes.Attach(solicitud);
                _context.Entry(solicitud).Property(e => e.IdCatTipoEstado).IsModified = true;
                _context.SLOTransporteSolicitudes.Add(pSLOTransporteSolicitud);
                var result = await _context.SaveChangesAsync();

                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.IsSuccess = true;
                respuesta.strMensaje = "Solicitud de transporte creada correctamente";
                respuesta.Entidad = pSLOTransporteSolicitud;

                return StatusCode((int)respuesta.StatusCode, respuesta);
                //return Ok(new { message = "Solicitud de transporte creada correctamente", id = pSLOTransporteSolicitud.IdSLOTransporteSolicitud });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Ocurrió un error al crear la solicitud de transporte.", details = ex.InnerException.Message });
            }
        }


        [HttpGet("ObtenerTransporteSolicitud/{id}")]
        public async Task<IActionResult> ObtenerTransporteSolicitud(int id)
        {
            var entity = await _context.SLOTransporteSolicitudes
                .Include(st => st.catTipoOperacionesTransportes).ThenInclude(st => st.catTipoOperacionesSLO)
                .Include(st => st.catTipoOperacionesTransportes).ThenInclude(st => st.catTipoTransporte).Where(st => st.IdSLOTransporteSolicitud == id)
                .FirstOrDefaultAsync();

            if (entity == null || !entity.Activo)
                return NotFound();
            return Ok(entity);
        }

        [HttpPut("ActualizarTransporteSolicitud/{id}")]
        public async Task<IActionResult> ActualizarTransporteSolicitud(int id, [FromBody] SLOTransporteSolicitud pSLOTransporteSolicitud)
        {
            var entity = await _context.SLOTransporteSolicitudes.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.IdCatTransportista = pSLOTransporteSolicitud.IdCatTransportista;
            entity.IdCatTipoEstados = pSLOTransporteSolicitud.IdCatTipoEstados;
            entity.IdCatUsuarios = pSLOTransporteSolicitud.IdCatUsuarios;
            entity.CAAT = pSLOTransporteSolicitud.CAAT;
            entity.IdCatTipoOperTransportes = pSLOTransporteSolicitud.IdCatTipoOperTransportes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("BajaTransporteSolicitud/{id}")]
        public async Task<IActionResult> BajaTransporteSolicitud(int id)
        {
            var entity = await _context.SLOTransporteSolicitudes.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("AltaTransporteSolicitud/{id}")]
        public async Task<IActionResult> AltaTransporteSolicitud(int id)
        {
            var entity = await _context.SLOTransporteSolicitudes.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("FinalizarTransporteSolicitud/{id}")]
        public async Task<IActionResult> FinalizarTransporteSolicitud(int id)
        {
            // Buscar solicitud de transporte
            var entity = await _context.SLOTransporteSolicitudes.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            // Id del estado "Terminado"
            int idEstadoTerminado = await _context.catTipoEstados
                .Where(e => e.TipoEstado == "T")
                .Select(e => e.IdCatTipoEstados)
                .FirstOrDefaultAsync();

            // Finalizamos la solicitud de transporte actual
            entity.IdCatTipoEstados = idEstadoTerminado;

            // Buscar la solicitud de servicio relacionada
            var solicitudServicio = await _context.sloSolicitudes.FindAsync(entity.IdSLOSolicitud);
            if (solicitudServicio == null)
                return NotFound();

            //Buscamos la orden relacionada
            var orden = await _context.ordenes.FindAsync(solicitudServicio.IdOrden);
            if (orden == null)
                return NotFound();

            // Traer todas las solicitudes de transporte activas de este servicio
            var solicitudesTransporte = await _context.SLOTransporteSolicitudes
                .Where(st => st.IdSLOSolicitud == solicitudServicio.IdSLOSolicitud && st.Activo)
                .ToListAsync();

            // Verificar si TODAS las solicitudes de transporte están terminadas
            bool todasTerminadas = solicitudesTransporte.All(st => st.IdCatTipoEstados == idEstadoTerminado);

            if (todasTerminadas)
            {
                solicitudServicio.IdCatTipoEstado = idEstadoTerminado;
                orden.IdEstadoOrden = idEstadoTerminado;
            }
            
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = todasTerminadas
                    ? "Solicitud de servicio finalizada correctamente"
                    : "Solicitud de transporte finalizada correctamente"
            });
        }

        #endregion TRANSPORTES_SOLICITUD

        #region TRANSPORTE_ASIGNADO
        [HttpPost("CrearTransporteAsignado")]
        public async Task<IActionResult> CrearTransporteAsignado([FromBody] SLOTransporteAsignado pSLOTransporteAsignado)
        {
            ////FALTAN VALIDACIONES DE CREAR.
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            try
            {
                pSLOTransporteAsignado.FechaRegistro = DateTime.Now;
                pSLOTransporteAsignado.Activo = true;

                _context.sLOTransporteAsignados.Add(pSLOTransporteAsignado);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Solicitud de transporte creada correctamente", id = pSLOTransporteAsignado.IdSLOTransporteAsignado, placas = pSLOTransporteAsignado.Placas });
            } catch(Exception ex)
            {
                return BadRequest(new { error = "Ocurrió un error al crear la asignación de transporte.", details = ex.InnerException.Message });
            }
            

            return CreatedAtAction(nameof(ObtenerTransporteAsignado), new { id = pSLOTransporteAsignado.IdSLOTransporteAsignado }, pSLOTransporteAsignado);
        }

        [HttpGet("ObtenerTransporteAsignado/{id}")]
        public async Task<IActionResult> ObtenerTransporteAsignado(int id)
        {
            var entity = await _context.sLOTransporteAsignados.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();
            return Ok(entity);
        }

        [HttpPut("ActualizarTransporteAsignado/{id}")]
        public async Task<IActionResult> ActualizarTransporteAsignado(int id, [FromBody] SLOTransporteAsignado pSLOTransporteAsignado)
        {
            //FALTAN VALIDACIONES DE ACTUALIZAR.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _context.sLOTransporteAsignados.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.IdSLOTransporteDetalle = pSLOTransporteAsignado.IdSLOTransporteDetalle;
            entity.IdCatTipoOperTransportes = pSLOTransporteAsignado.IdCatTipoOperTransportes;
            entity.Placas = pSLOTransporteAsignado.Placas;
            entity.Economico = pSLOTransporteAsignado.Economico;
            entity.Operador = pSLOTransporteAsignado.Operador;
            entity.Color = pSLOTransporteAsignado.Color;
            entity.Marca = pSLOTransporteAsignado.Marca;
            entity.IdCatUsuarios = pSLOTransporteAsignado.IdCatUsuarios;
            entity.IdSLOTransporteSolicitud = pSLOTransporteAsignado.IdSLOTransporteSolicitud;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("BajaTransporteAsignado/{id}")]
        public async Task<IActionResult> BajaTransporteAsignado(int id)
        {
            //FALTAN VALIDACIONES DE BORRADO.

            var entity = await _context.sLOTransporteAsignados.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("AltaTransporteAsignado/{id}")]
        public async Task<IActionResult> AltaTransporteAsignado(int id)
        {
            //FALTAN VALIDACIONES DE BORRADO.

            var entity = await _context.sLOTransporteAsignados.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion TRANSPORTE_ASIGNADO

        #region TRANSPORTE_EVENTOS
        [HttpPost("CrearTransporteCron")]
        public async Task<IActionResult> CrearTransporteCron([FromBody] SLOTransportesCron model)
        {
            RespuestaGenericaDTO respuestaGenericaDTO = new();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.FechaRegistro = DateTime.Now;
            model.Activo = true;

            try
            {
                _context.SLOTransportesCron.Add(model);
                var response = await _context.SaveChangesAsync();

                if(response > 0)
                {
                    respuestaGenericaDTO.IsSuccess = true;
                    respuestaGenericaDTO.strMensaje = "Eventualidad creada correctamente";
                    respuestaGenericaDTO.Entidad = model;

                    return Ok(respuestaGenericaDTO);
                }
                else
                {
                    respuestaGenericaDTO.IsSuccess = false;
                    respuestaGenericaDTO.strMensaje = "No se creó la eventualidad debido a un error inesperado";

                    return BadRequest(respuestaGenericaDTO);
                }
            } catch (Exception ex)
            {
                respuestaGenericaDTO.lstrErrorMessages = new List<string>
                {
                    $"Error al crear la solicitud: {ex.InnerException}"
                };
                respuestaGenericaDTO.IsSuccess = false;

                return BadRequest(respuestaGenericaDTO);
            }                      
        }

        [HttpGet("ObtenerPorIDTransporteCron/{id}")]
        public async Task<IActionResult> ObtenerPorIDTransporteCron(int id)
        {
            var entity = await _context.SLOTransportesCron
                .Include(x => x.catTipoEventosCron)
                .Include(x => x.catUsuarios)
                .Include(x => x.sloTransporteAsignado)
                .FirstOrDefaultAsync(x => x.IdSLOTransporteCron == id && x.Activo);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpGet("ListarTransporteCron/{id}")]
        public async Task<IActionResult> ListarTransporteCron(int id)
        {
            var entity = await _context.SLOTransportesCron
                .Include(x => x.catTipoEventosCron)
                .Where(x => x.IdSLOTransporteAsignado == id).ToListAsync();
            if (!entity.Any())
                return NotFound();

            return Ok(entity);
        }

        [HttpPost("ActualizarTransporteCron")]
        public async Task<IActionResult> ActualizarTransporteCron([FromBody] SLOTransportesCron model)
        {
            var entity = await _context.SLOTransportesCron.FindAsync(model.IdSLOTransporteCron);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.IdCatTipoEventoCron = model.IdCatTipoEventoCron;
            entity.Comentarios = model.Comentarios;
            entity.FechaEvento = model.FechaEvento;
            entity.URLMaps = model.URLMaps;
            //entity.IdCatUsuarios = model.IdCatUsuarios;
            entity.IdSLOTransporteAsignado = model.IdSLOTransporteAsignado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("BajaTransporteCron/{id}")]
        public async Task<IActionResult> BajaTransporteCron(int id)
        {
            var entity = await _context.SLOTransportesCron.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("AltaTransporteCron/{id}")]
        public async Task<IActionResult> AltaTransporteCron(int id)
        {
            var entity = await _context.SLOTransportesCron.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("CrearTransporteCronDocumentos")]
        public async Task<IActionResult> CrearTransporteCronDocumentos([FromBody] List<SLOTransporteCronDocumentos> lstsloCronDocumentos)
        {
            var respuestaGenericaDTO = new RespuestaGenericaDTO();

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            try
            {
                // Configurar valores por defecto en todos los registros
                //foreach (var doc in lstsloCronDocumentos)
                //{
                //    doc.FechaRegistro = DateTime.Now;
                //    doc.Activo = true;
                //}

                // Guardar en lote
                await _context.sloTransporteCronDocumentos.AddRangeAsync(lstsloCronDocumentos);
                var response = await _context.SaveChangesAsync();

                if (response > 0) // ✅ SaveChangesAsync devuelve la cantidad de filas afectadas
                {
                    respuestaGenericaDTO.IsSuccess = true;
                    respuestaGenericaDTO.strMensaje = "Documentos de eventualidad asignados correctamente";
                    respuestaGenericaDTO.Entidades = lstsloCronDocumentos.Cast<object>().ToList(); // como tu DTO usa object
                    return Ok(respuestaGenericaDTO);
                }
                else
                {
                    respuestaGenericaDTO.IsSuccess = false;
                    respuestaGenericaDTO.strMensaje = "No se creó la asignación de documentos de eventualidad debido a un error inesperado";
                    return BadRequest(respuestaGenericaDTO);
                }
            }
            catch (Exception ex)
            {
                respuestaGenericaDTO.lstrErrorMessages = new List<string>
        {
            $"Error al crear la asignación de documentos de eventualidad: {ex.InnerException?.Message ?? ex.Message}"
        };
                respuestaGenericaDTO.IsSuccess = false;

                return BadRequest(respuestaGenericaDTO);
            }
        }

        [HttpGet("ObtenerTransporteCronDocumentos/{idTransporteCron}")]
        public async Task<IActionResult> ObtenerTransporteCronDocumentos(int idTransporteCron)
        {
            RespuestaGenericaDTO respuestaGenericaDTO = new RespuestaGenericaDTO();
            try
            {
                var lstTransporteCronDocumentos = await _context.sloTransporteCronDocumentos           
                   .Where(dc => dc.IdSLOTransporteCron == idTransporteCron).ToListAsync();

                respuestaGenericaDTO.IsSuccess = true;
                respuestaGenericaDTO.Entidades = lstTransporteCronDocumentos.Cast<object>().ToList();
                return Ok(respuestaGenericaDTO);

            } catch (Exception ex)            
            {
                respuestaGenericaDTO.IsSuccess  = false;
                respuestaGenericaDTO.lstrErrorMessages.Add(ex.Message);
                return BadRequest(respuestaGenericaDTO);
            }
        }
        #endregion TRANSPORTE_EVENTOS


        #endregion SOLICITUDES TRANSPORTES

        #region TORRES_CONTROL

        #region TORRE_CONTROL_TERRESTRE
        [HttpPost("CrearTControlTerrestre")]
        public async Task<IActionResult> CrearTControlTerrestre([FromBody] SLOTControlTerrestre model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.FechaRegistro = DateTime.Now;
            model.Activo = true;

            _context.sloTControlTerrestres.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorIdTControlTerrestre), new { id = model.IdTControlTerrestre }, model);
        }

        [HttpGet("ObtenerPorIdTControlTerrestre/{id}")]
        public async Task<IActionResult> ObtenerPorIdTControlTerrestre(int id)
        {
            //var getTransporteAsignado = _context.sloTControlTerrestre
            //    .Include(x => x.catUsuarios)
            //    .Include(x => x.sloSolicitudes)
            //    .Include(x => x.sloSolicitudesDetalles)
            //    .Include(x => x.sloTransporteAsignado)
            //    .FirstOrDefaultAsync(ct => ct.IdSLOSolicitud == id && ct.Activo);
            try
            {
                var entity = await _context.sloTControlTerrestre
                    .Include(x => x.catUsuarios)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(s => s.sloSOlicitudesDetalle)
                    .ThenInclude(x => x.catTipoMercancia)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(s => s.sloSOlicitudesDetalle)
                    .ThenInclude(x => x.catTipoIMO)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(o => o.Orden)
                    .Include(x => x.sloTransporteAsignado)
                    .ThenInclude(d => d.sloTransporteSolicitud)
                    .ThenInclude(x => x.sloTransporteDetalle)
                    .Include(x => x.sloTransporteAsignado)
                    .ThenInclude(x => x.sloTransporteSolicitud.catTransportistas)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(x => x.catClienteUbicacionOrigen)
                    .ThenInclude(cuo => cuo.catPaises)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(x => x.catClienteUbicacionOrigen)
                    .ThenInclude(cuo => cuo.catPaisEstados)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(x => x.catClienteUbicacionOrigen)
                    .ThenInclude(cuo => cuo.catPaisMunicipios)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(x => x.catClienteUbicacionDestino)
                    .ThenInclude(cud => cud.catPaises)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(x => x.catClienteUbicacionDestino)
                    .ThenInclude(cud => cud.catPaisEstados)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(x => x.catClienteUbicacionDestino)
                    .ThenInclude(cud => cud.catPaisMunicipios)
                    .Include(x => x.sloSolicitudes)
                    .ThenInclude(x => x.catTipoOperComercio)
                    .Where(x => x.IdSLOSolicitud == id && x.Activo)
                    .ToListAsync();


                return Ok(entity);
            } catch (Exception ex)
            {
                return NotFound();
            }
            
        }

        [HttpPost("ActualizarTControlTerrestre")]
        public async Task<IActionResult> ActualizarTControlTerrestre([FromBody] SLOTControlTerrestre model)
        {
            try
            {
                // Validaciones iniciales
                if (model == null)
                    return BadRequest("El modelo no puede ser nulo.");

                if (model.IdTControlTerrestre <= 0)
                    return BadRequest("IdTControlTerrestre inválido.");

                // Buscar registro activo
                var entity = await _context.sloTControlTerrestres
                    .FirstOrDefaultAsync(x => x.IdTControlTerrestre == model.IdTControlTerrestre && x.Activo);

                if (entity == null)
                    return NotFound("No se encontró el registro o no está activo.");

                // Actualización condicional de campos (evita sobreescribir con null)
                entity.FConfirmaBooking = model.FConfirmaBooking ?? entity.FConfirmaBooking;
                entity.FUnidensitiocarga = model.FUnidensitiocarga ?? entity.FUnidensitiocarga;
                entity.FUnidadencarga = model.FUnidadencarga ?? entity.FUnidadencarga;
                entity.FFinalizaCarga = model.FFinalizaCarga ?? entity.FFinalizaCarga;
                entity.FIniciaTransito = model.FIniciaTransito ?? entity.FIniciaTransito;
                entity.FEnFrontera = model.FEnFrontera ?? entity.FEnFrontera;
                entity.FInicioDespachoAA = model.FInicioDespachoAA ?? entity.FInicioDespachoAA;
                entity.FFinDespachoAA = model.FFinDespachoAA ?? entity.FFinDespachoAA;
                entity.FIniciaTransitoEXPO = model.FIniciaTransitoEXPO ?? entity.FIniciaTransitoEXPO;
                entity.FPuntoDescarga = model.FPuntoDescarga ?? entity.FPuntoDescarga;
                entity.FEnProcesoDescarga = model.FEnProcesoDescarga ?? entity.FEnProcesoDescarga;
                entity.FFinDescarga = model.FFinDescarga ?? entity.FFinDescarga;
                entity.FRecepcionPOD = model.FRecepcionPOD ?? entity.FRecepcionPOD;
                entity.FFinOperacion = model.FFinOperacion ?? entity.FFinOperacion;

                if (model.IdCatUsuarios > 0)
                    entity.IdCatUsuarios = model.IdCatUsuarios;

                if (model.IdSLOSolicitud > 0)
                    entity.IdSLOSolicitud = model.IdSLOSolicitud;

                if (model.IdSLOTransporteAsignado > 0)
                    entity.IdSLOTransporteAsignado = model.IdSLOTransporteAsignado;

                if (model.IdSLOSolicitudDet > 0)
                    entity.IdSLOSolicitudDet = model.IdSLOSolicitudDet;

                // Guardar cambios
                await _context.SaveChangesAsync();

                return Ok("Registro actualizado correctamente.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict($"Error de concurrencia: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error de base de datos: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        [Authorize(Roles = "ADMIN,ADMINUSER")]
        [HttpDelete("BajaTControlTerrestre/{id}")]
        public async Task<IActionResult> BajaTControlTerrestre(int id)
        {
            var entity = await _context.sloTControlTerrestres.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("AltaTControlTerrestre/{id}")]
        public async Task<IActionResult> AltaTControlTerrestre(int id)
        {
            var entity = await _context.sloTControlTerrestres.FindAsync(id);
            if (entity == null || !entity.Activo)
                return NotFound();

            entity.Activo = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion TORRE_CONTROL_TERRESTRE


        #endregion TORRES_CONTROL
    }
}
