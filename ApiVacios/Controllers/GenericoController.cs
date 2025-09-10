using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiVacios.Controllers
{

    //[AllowAnonymous]
    [Authorize(Roles = "ADMIN,SISTEMA")]
    //[Route("hidden-path/[controller]")]
    //[Route("generico/[controller]")]
    [ApiController]
    public class GenericoController<T> : ControllerBase where T : class
    {
        private readonly IGenericoRepositorio<T> _repositorio;
        private RespuestaGenericaDTO _respuestaGenericaDTO;
        private List<string> _lstErrores;

        public GenericoController(IGenericoRepositorio<T> repositorio)
        {
            _repositorio = repositorio;
        }

        // Función para agregar entidad: Modelo + Crear
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] T entidad)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (entidad == null)
                {
                    return BadRequest("La entidad no puede ser nula");
                }

                _repositorio.AgregarGenerico(entidad);
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Función para guardar cambios: Modelo + Guardar
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] T entidad)
        {
            var entidadExistente = await _repositorio.obtenerPorIdGenerico(id);

            if (entidadExistente == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            _repositorio.ActualizarGenerico(id, entidad);
            return Ok($"La entidad {typeof(T).Name} fue actualizada exitosamente.");
        }

        // Función para eliminar entidad: Modelo + Borrar
        [HttpDelete("Borrar/{id}")]
        public async Task<IActionResult> Borrar(int id)
        {
            var entidad = await _repositorio.obtenerPorIdGenerico(id);

            if (entidad == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            _repositorio.BorrarGenerico(entidad);
            return Ok($"La entidad {typeof(T).Name} fue eliminada exitosamente.");
        }

        // Función para obtener todas las entidades: Modelo + Listar
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var lista = await _repositorio.obtenerListaTodosGenerico();
            return Ok(lista);
        }

        // Función para obtener entidad por ID: Modelo + Obtener
        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var entidad = await _repositorio.obtenerPorIdGenerico(id);

            if (entidad == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            return Ok(entidad);
        }

        // Función para activar la entidad (Alta)
        [HttpPatch("Alta/{id}")]
        public async Task<IActionResult> Alta(int id)
        {
            bool resultado = _repositorio.AltaGenerico(id, x => x.GetType().GetProperty("Activo"), true);

            if (resultado)
            {
                return Ok($"La entidad {typeof(T).Name} fue activada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo activar la entidad.");
            }
        }

        // Función para desactivar la entidad (Baja)
        [HttpPatch("Baja/{id}")]
        public async Task<IActionResult> Baja(int id)
        {
            bool resultado = _repositorio.BajaGenerico(id, x => x.GetType().GetProperty("Activo"), false);

            if (resultado)
            {
                return Ok($"La entidad {typeof(T).Name} fue desactivada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo desactivar la entidad.");
            }
        }


        #region Utilerias
        private RespuestaGenericaDTO InicializaRespuesta()
        {
            RespuestaGenericaDTO objRespuestaGenericaDTO = new RespuestaGenericaDTO();
            objRespuestaGenericaDTO.IsSuccess = false;
            objRespuestaGenericaDTO.strMensaje = string.Empty;
            objRespuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
            objRespuestaGenericaDTO.lstrErrorMessages = new List<string>();
            objRespuestaGenericaDTO.Entidad = null;
            return objRespuestaGenericaDTO;
        }

        //private RespuestaTokenDTO ObtenerSistemaToken()
        //{
        //    #region Variables
        //    RespuestaTokenDTO respuestaTokenDTO = new RespuestaTokenDTO();
        //    respuestaTokenDTO.RolesUsuario = new List<string>();
        //    #endregion Variables
        //    #region Operaciones
        //    try
        //    {

        //        var idCatClienteClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //        if (!string.IsNullOrEmpty(idCatClienteClaim) && int.TryParse(idCatClienteClaim, out int idCatCliente))
        //        {
        //            respuestaTokenDTO.IdCatCliente = idCatCliente;
        //        }

        //        // Get "IdCatSistema" from ClaimTypes.NameIdentifier
        //        var idCatSistemaClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //        if (!string.IsNullOrEmpty(idCatSistemaClaim) && int.TryParse(idCatSistemaClaim, out int idCatSistema))
        //        {
        //            respuestaTokenDTO.IdCatSistema = idCatSistema;
        //        }

        //        // Get "IdCatSistema" from ClaimTypes.NameIdentifier
        //        var idCatEmpresaClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
        //        if (!string.IsNullOrEmpty(idCatEmpresaClaim) && int.TryParse(idCatEmpresaClaim, out int idCatEmpresa))
        //        {
        //            respuestaTokenDTO.IdCatEmpresa = idCatEmpresa;
        //        }

        //        // Get "Role" from ClaimTypes.Role
        //        var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        //        if (!string.IsNullOrEmpty(roleClaim))
        //        {
        //            respuestaTokenDTO.RolesUsuario.Add(roleClaim);
        //        }

        //        return respuestaTokenDTO;
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //    #endregion Operaciones

        //}

        #endregion Utilerias
    }
}
