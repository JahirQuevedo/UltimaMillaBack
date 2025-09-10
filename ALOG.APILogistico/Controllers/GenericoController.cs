using ALOG.Modelos.Modelos.DTO;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Repositorio.Control;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiVacios.Controllers
{

    //[AllowAnonymous]
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER,SISTEMA")]
    //[Authorize(Roles = "ADMIN")]
    [Route("hidden-path/[controller]")]
    //[Route("generico/[controller]")]
    [ApiController]
    public class GenericoController<T> : ControllerBase where T : class, IActivable
    {

        private RespuestaGenericaDTO _respuestaGenericaDTO;
        private List<string> _lstErrores;
        private RespuestaTokenDTO respuestaToken = new RespuestaTokenDTO();
        private readonly IGenericoRepositorio<T> _repositorio;
        public GenericoController(IGenericoRepositorio<T> repositorio)
        {

            _lstErrores = new List<string>();
            _respuestaGenericaDTO = InicializaRespuesta();
            //respuestaToken = ObtenerSistemaToken();
            _repositorio = repositorio;
        }

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

                #region ObtenerToken
                respuestaToken = HttpContext.ObtenerUsuarioTokenUnificado();
                #endregion ObtenerToken

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
            #region Variables
            _respuestaGenericaDTO = InicializaRespuesta();
            #endregion Variables
            try
            {


                var entidad = _repositorio.obtenerPorIdGenerico(id);

                if (entidad == null)
                {
                    _respuestaGenericaDTO.strMensaje = $"La entidad con ID {id} no fue encontrada.";
                    return NotFound(_respuestaGenericaDTO);
                }


                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.Entidad = entidad;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                return Ok(_respuestaGenericaDTO);
            }
            catch (Exception ex)
            {

                _respuestaGenericaDTO.strMensaje = ex.Message;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _respuestaGenericaDTO);
            }
        }

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

        //private RespuestaTokenDTO ObtenerUsuarioToken()
        //{
        //    #region Variables
        //    RespuestaTokenDTO respuestaTokenDTO = new RespuestaTokenDTO();
        //    respuestaTokenDTO.RolesUsuario = new List<string>();
        //    #endregion Variables
        //    #region Operaciones
        //    try
        //    {
        //        //NOMBRE USUARIO
        //        var nombreUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //        if (!string.IsNullOrEmpty(nombreUsuarioClaim) && int.TryParse(nombreUsuarioClaim, out int nombreUsuario))
        //        {
        //            respuestaTokenDTO.IdCatCliente = nombreUsuario;
        //        }

        //        //EMAIL
        //        var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //        if (!string.IsNullOrEmpty(emailClaim) && int.TryParse(emailClaim, out int email))
        //        {
        //            respuestaTokenDTO.IdCatCliente = email;
        //        }

        //        // Get "IdCatUsuario" from ClaimTypes.NameIdentifier
        //        var idCatUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //        if (!string.IsNullOrEmpty(idCatUsuarioClaim) && int.TryParse(idCatUsuarioClaim, out int idCatUsuario))
        //        {
        //            respuestaTokenDTO.IdCatSistema = idCatUsuario;
        //        }

        //        // Get "IdCatEmpresa" from ClaimTypes.NameIdentifier
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
    //public class GenericoController<T> : ControllerBase where T : class, IActivable
    //{
    //    private readonly IGenericoRepositorio<T> _repositorio;
    //    private RespuestaGenericaDTO _respuestaGenericaDTO;
    //    private List<string> _lstErrores;

    //    public GenericoController(IGenericoRepositorio<T> repositorio)
    //    {
    //        _repositorio = repositorio;
    //    }

    //    // Función para agregar entidad: Modelo + Crear
    //    [HttpPost("Crear")]
    //    public async Task<IActionResult> Crear([FromBody] T entidad)
    //    {
    //        try
    //        {

    //            if (!ModelState.IsValid)
    //            {
    //                return BadRequest(ModelState);
    //            }
    //            if (entidad == null)
    //            {
    //                return BadRequest("La entidad no puede ser nula");
    //            }

    //            _repositorio.AgregarGenerico(entidad);
    //            return Ok(entidad);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    //        }
    //    }

    //    // Función para guardar cambios: Modelo + Guardar
    //    //[HttpPut("Actualizar/{id}")]
    //    //public async Task<IActionResult> Actualizar(int id, [FromBody] T entidad)
    //    //{
    //    //    var entidadExistente =  _repositorio.obtenerPorIdGenerico(id);

    //    //    if (entidadExistente == null)
    //    //    {
    //    //        return NotFound($"La entidad con ID {id} no fue encontrada.");
    //    //    }

    //    //    if (_repositorio.ActualizarGenerico(id, entidad) == null)
    //    //        return NotFound("False");
    //    //    return Ok($"La entidad {typeof(T).Name} fue actualizada exitosamente.");
    //    //}

    //    // Función para guardar cambios: Modelo + Guardar
    //    [HttpPut("Actualizar/{id}")]
    //    public async Task<IActionResult> Actualizar(int id, [FromBody] T entidad)
    //    {
    //        #region Variables
    //        _respuestaGenericaDTO = new RespuestaGenericaDTO();
    //        _respuestaGenericaDTO.IsSuccess = false;
    //        _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
    //        _respuestaGenericaDTO.Entidad = null;
    //        #endregion Variables
    //        try
    //        {

    //            if (!ModelState.IsValid)
    //            {

    //                    _respuestaGenericaDTO.lstrErrorMessages.Add(ModelState.ToString());
    //                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
    //                    //_lstErrores.Add($"La entidad con ID {id} no fue encontrada.");
    //                    return NotFound(_respuestaGenericaDTO);
    //            }


    //                var entidadExistente = _repositorio.obtenerPorIdGenerico(id);

    //            if (entidadExistente == null)
    //            {
    //                _respuestaGenericaDTO.lstrErrorMessages.Add("$\"La entidad con ID {id} no fue encontrada.\"");
    //                _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
    //                _lstErrores.Add($"La entidad con ID {id} no fue encontrada.");
    //                return NotFound(_respuestaGenericaDTO);
    //            }


    //            if (_repositorio.ActualizarGenerico(id, entidad) == null)
    //            {
    //                _respuestaGenericaDTO.lstrErrorMessages.Add($"La entidad {typeof(T).Name} fue actualizada exitosamente.");
    //                return NotFound(_respuestaGenericaDTO);
    //            }

    //            _respuestaGenericaDTO.IsSuccess = true;
    //            _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
    //            _respuestaGenericaDTO.strMensaje = $"La entidad {typeof(T).Name} fue actualizada exitosamente.";
    //            _respuestaGenericaDTO.Entidad = entidad;
    //            //return Ok($"La entidad {typeof(T).Name} fue actualizada exitosamente.");
    //            return Ok(_respuestaGenericaDTO);

    //        }
    //        catch (Exception ex)
    //        {
    //            _respuestaGenericaDTO.lstrErrorMessages.Add(ex.Message);
    //            return BadRequest(_respuestaGenericaDTO);
    //        }
    //    }

    //    // Función para eliminar entidad: Modelo + Borrar
    //    [HttpDelete("Borrar/{id}")]
    //    public async Task<IActionResult> Borrar(int id)
    //    {
    //        var entidad =  _repositorio.obtenerPorIdGenerico(id);

    //        if (entidad == null)
    //        {
    //            return NotFound($"La entidad con ID {id} no fue encontrada.");
    //        }

    //         _repositorio.BorrarGenerico(entidad);
    //        return Ok($"La entidad {typeof(T).Name} fue eliminada exitosamente.");
    //    }

    //    // Función para obtener todas las entidades: Modelo + Listar
    //    [HttpGet("Listar")]
    //    public async Task<IActionResult> Listar()
    //    {
    //        var lista = await _repositorio.obtenerListaTodosGenerico();
    //        return Ok(lista);
    //    }

    //    // Función para obtener entidad por ID: Modelo + Obtener
    //    [HttpGet("Obtener/{id}")]
    //    public async Task<IActionResult> Obtener(int id)
    //    {
    //        var entidad =  _repositorio.obtenerPorIdGenerico(id);

    //        if (entidad == null)
    //        {
    //            return NotFound($"La entidad con ID {id} no fue encontrada.");
    //        }

    //        return Ok(entidad);
    //    }

    //    // Función para activar la entidad (Alta)
    //    [HttpPatch("Alta/{id}")]
    //    public async Task<IActionResult> Alta(int id)
    //    {
    //        bool resultado =  _repositorio.AltaGenerico(id, x => x.Activo, true);

    //        if (resultado)
    //        {
    //            return Ok($"La entidad {typeof(T).Name} fue activada exitosamente.");
    //        }
    //        else
    //        {
    //            return BadRequest("No se pudo activar la entidad.");
    //        }
    //    }

    //    // Función para desactivar la entidad (Baja)
    //    [HttpPatch("Baja/{id}")]
    //    public async Task<IActionResult> Baja(int id)
    //    {
    //        bool resultado =  _repositorio.BajaGenerico(id, x => x.Activo, false);

    //        if (resultado)
    //        {
    //            return Ok($"La entidad {typeof(T).Name} fue desactivada exitosamente.");
    //        }
    //        else
    //        {
    //            return BadRequest("No se pudo desactivar la entidad.");
    //        }
    //    }


    //}
}
