using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Orden;
using System.Net;



namespace ALOG.APILogistico.Controllers.CtrllLogistico
{
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    [Route("[controller]")]
    [ApiController]
    public class SLOSolicitudesController : Controller
    {
        private readonly ISLOSolicitudesRepositorio _ctRepoSolicitudes;        
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();        

        public SLOSolicitudesController(ISLOSolicitudesRepositorio ctRepoSolicitudes)
        {                        
            _ctRepoSolicitudes = ctRepoSolicitudes;
        }

        #region SOLICITUDES_SERVICIOS
        #region SLO_SOLICITUDES
        //Función para obtener las solicitudes con filtro
        [HttpPost("SLOSolicitudesObtener")]
        public async Task<IActionResult> SLOSolicitudesObtener(FiltroSLOSolicitudes pFiltro)
        {
            ICollection<SLOSolicitudes> listado_solicitudes = await _ctRepoSolicitudes.SLOSolicitudesObtener(pFiltro);
            return Ok(listado_solicitudes);

        }

        [HttpGet("SLOSolicitudesListar")]
        public async Task<IActionResult> SLOSolicitudesListar()
        {
            ICollection<SLOSolicitudes> lista = await _ctRepoSolicitudes.obtenerListaTodosGenerico();
            return Ok(lista);
        }

        [HttpPost("SLOSolicitudesCrear")]
        public async Task<IActionResult> SLOSolicitudesCrear([FromBody] SLOSolicitudes entidad)
        {
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
                var response = await _ctRepoSolicitudes.SLOSolicitudesCrear(entidad);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("SLOSolicitudesEditar/{idSolicitud}")]
        public async Task<IActionResult> SLOSolicitudesEditar(int idSolicitud)
        {
            try
            {
                var respuestaDTO = await _ctRepoSolicitudes.SLOSolicitudesEditar(idSolicitud);

                if (!respuestaDTO.IsSuccess || respuestaDTO.Entidad is null)
                {
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

        [HttpPut("SLOSolicitudesActualizar/{id}")]
        public async Task<IActionResult> SLOSolicitudesActualizar([FromBody] int id,SLOSolicitudes solicitud)
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
                var entidadExistente = await _ctRepoSolicitudes.obtenerPorIdGenerico(solicitud.IdSLOSolicitud);
                if (entidadExistente == null)
                {
                    respuesta.StatusCode = HttpStatusCode.NotFound;
                    respuesta.lstrErrorMessages = new List<string> { $"Entidad con ID {solicitud.IdSLOSolicitud} no encontrada." };
                    return NotFound(respuesta);
                }

                var actualizada = await _ctRepoSolicitudes.SLOSolicitudesActualizar(id,solicitud);
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
                respuesta.lstrErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, respuesta);
            }
        }
        #endregion SLO_SOLICITUDES
        #endregion SOLICITUDES_SERVICIOS        
    }
}
