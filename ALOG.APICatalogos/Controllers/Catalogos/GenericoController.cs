using ALOG.Modelos.Modelos.DTO;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiVacios.Controllers.Catalogos
{
    //[Authorize]
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class GenericoController<T> : ControllerBase where T : class, IActivable
    {
        private readonly IGenericoRepositorio<T> _repositorio;
        private RespuestaGenericaDTO _respuestaGenericaDTO;
        private List<string> _lstErrores;

        public GenericoController(IGenericoRepositorio<T> repositorio)
        {
            _repositorio = repositorio;
            _lstErrores = new List<string>();
            _respuestaGenericaDTO = InicializaRespuesta();

        }

        [Authorize(Roles = "ADMIN,ADMINUSER")]
        // Función para agregar entidad: Modelo + Crear
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] T entidad)
        {
            #region Variables
            _respuestaGenericaDTO = InicializaRespuesta();

            #endregion Variables
            #region Operaciones
            try
            {

                if (!ModelState.IsValid)
                {
                    _respuestaGenericaDTO.strMensaje = ModelState.ToString();
                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_respuestaGenericaDTO);
                }
                if (entidad == null)
                {
                    _respuestaGenericaDTO.strMensaje = "La entidad no puede ser nula";
                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                    _lstErrores.Add("La entidad no puede ser nula");
                    _respuestaGenericaDTO.lstrErrorMessages = _lstErrores;

                    return BadRequest(_respuestaGenericaDTO);
                }

                _repositorio.AgregarGenerico(entidad);

                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.Entidad = entidad;
                return Ok(_respuestaGenericaDTO);
            }
            catch (Exception ex)
            {

                _respuestaGenericaDTO.strMensaje = "Error interno del servidor";
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.InternalServerError;
                _lstErrores.Add($"Error interno del servidor: {ex.Message}");
                return StatusCode(500, _respuestaGenericaDTO);
            }
            #endregion Operaciones
        }



        // Función para guardar cambios: Modelo + Guardar
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] T entidad)
        {
            #region Variables
            _respuestaGenericaDTO = InicializaRespuesta();
            #endregion Variables
            try
            {

                if (ModelState.IsValid)
                {
                    var entidadExistente = _repositorio.obtenerPorIdGenerico(id);

                    if (entidadExistente == null)

                        _respuestaGenericaDTO.lstrErrorMessages.Add("$\"La entidad con ID {id} no fue encontrada.\"");
                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
                    _lstErrores.Add($"La entidad con ID {id} no fue encontrada.");
                    return NotFound(_respuestaGenericaDTO);
                }


                if (_repositorio.ActualizarGenerico(id, entidad) == null)
                {



                    _respuestaGenericaDTO.lstrErrorMessages.Add($"La entidad {typeof(T).Name} fue actualizada exitosamente.");
                    return NotFound(_respuestaGenericaDTO);
                }

                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                _respuestaGenericaDTO.strMensaje = $"La entidad {typeof(T).Name} fue actualizada exitosamente.";
                _respuestaGenericaDTO.Entidad = entidad;
                //return Ok($"La entidad {typeof(T).Name} fue actualizada exitosamente.");
                return Ok(_respuestaGenericaDTO);

            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add(ex.Message);
                return BadRequest(_respuestaGenericaDTO);
            }
        }


        // Función para eliminar entidad: Modelo + Borrar
        [Authorize(Roles = "ADMIN,ADMINUSER")]
        [HttpDelete("Borrar/{id}")]
        public async Task<IActionResult> Borrar(int id)
        {
            #region Variables
            _respuestaGenericaDTO = InicializaRespuesta();
            #endregion Variables
            try
            {


                var entidad = await _repositorio.obtenerPorIdGenerico(id);

                if (entidad == null)
                {
                    _respuestaGenericaDTO.strMensaje = $"La entidad con ID {id} no fue encontrada.";
                    return NotFound(_respuestaGenericaDTO);
                }

                if (_repositorio.BorrarGenerico(entidad) == null)
                {
                    _respuestaGenericaDTO.strMensaje = "No se pudo eliminar la entidad.";
                    return NotFound(_respuestaGenericaDTO);
                }
                else
                {
                    _respuestaGenericaDTO.IsSuccess = true;
                    _respuestaGenericaDTO.strMensaje = "No se pudo eliminar la entidad.";
                    _respuestaGenericaDTO.Entidad = entidad;
                    return Ok(_respuestaGenericaDTO);
                }
            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.strMensaje = ex.Message;
                return StatusCode(500, _respuestaGenericaDTO);

            }
        }


        // Función para obtener todas las entidades: Modelo + Listar
        [Authorize(Roles = "ADMIN,ADMINUSER,SISTEMA,CLIENTE")]
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {

            var lista = await _repositorio.obtenerListaTodosGenerico();
            return Ok(lista);
        }

        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> Obtener(int id) {
            _respuestaGenericaDTO = InicializaRespuesta();
            try {
                var entidad = await _repositorio.obtenerPorIdGenerico(id);

                if (entidad == null) {
                    _respuestaGenericaDTO.strMensaje = $"La entidad con ID {id} no fue encontrada.";
                    return NotFound(_respuestaGenericaDTO);
                }

                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.Entidad = entidad;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                return Ok(_respuestaGenericaDTO);
            } catch (Exception ex) {
                _respuestaGenericaDTO.strMensaje = ex.Message;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _respuestaGenericaDTO);
            }
        }

        // Función para obtener entidad por ID: Modelo + Obtener
        //[HttpGet("Obtener/{id}")]
        //public async Task<IActionResult> Obtener(int id)
        //{
        //    #region Variables
        //    _respuestaGenericaDTO = InicializaRespuesta();
        //    #endregion Variables
        //    try
        //    {


        //        var entidad = _repositorio.obtenerPorIdGenerico(id);

        //        if (entidad == null)
        //        {
        //            _respuestaGenericaDTO.strMensaje = $"La entidad con ID {id} no fue encontrada.";
        //            return NotFound(_respuestaGenericaDTO);
        //        }


        //        _respuestaGenericaDTO.IsSuccess = true;
        //        _respuestaGenericaDTO.Entidad = entidad;
        //        _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
        //        return Ok(_respuestaGenericaDTO);
        //    }
        //    catch (Exception ex)
        //    {

        //        _respuestaGenericaDTO.strMensaje = ex.Message;
        //        _respuestaGenericaDTO.StatusCode = HttpStatusCode.InternalServerError;
        //        return StatusCode(500, _respuestaGenericaDTO);
        //    }
        //}

        // Función para activar la entidad (Alta)
        [HttpPatch("Alta/{id}")]
        public async Task<IActionResult> Alta(int id)
        {
            #region Variables
            _respuestaGenericaDTO = InicializaRespuesta();
            #endregion Variables
            try
            {


                bool resultado = _repositorio.AltaGenerico(id, x => x.Activo, true);

                if (resultado)
                {
                    _respuestaGenericaDTO.IsSuccess = true;
                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                    _respuestaGenericaDTO.strMensaje = $"La entidad {typeof(T).Name} fue activada exitosamente.";
                    return Ok(_respuestaGenericaDTO);
                }
                else
                {
                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaGenericaDTO.strMensaje = "No se pudo activar la entidad.";
                    return BadRequest(_respuestaGenericaDTO);
                }
            }
            catch (Exception ex)
            {

                _respuestaGenericaDTO.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaGenericaDTO.strMensaje = ex.Message;
                return StatusCode(500, _respuestaGenericaDTO);
            }
        }

        // Función para desactivar la entidad (Baja)
        [HttpPatch("Baja/{id}")]
        public async Task<IActionResult> Baja(int id)
        {
            #region Variables
            _respuestaGenericaDTO = InicializaRespuesta();
            #endregion Variables

            try
            {


                bool resultado = _repositorio.BajaGenerico(id, x => x.Activo, false);

                if (resultado)
                {
                    _respuestaGenericaDTO.IsSuccess = true;
                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                    _respuestaGenericaDTO.strMensaje = $"La entidad {typeof(T).Name} fue desactivada exitosamente.";
                    return Ok(_respuestaGenericaDTO);
                }
                else
                {
                    _respuestaGenericaDTO.strMensaje = "No se pudo desactivar la entidad.";
                    return BadRequest(_respuestaGenericaDTO);
                }
            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.strMensaje = ex.Message;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _respuestaGenericaDTO);

            }
        }

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


    }
}
