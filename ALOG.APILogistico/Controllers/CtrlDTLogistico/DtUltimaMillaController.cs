//using ApiVacios.Controllers.Catalogos;
using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ApiVacios.Controllers.CtrlDTLogistico
{
    //[AllowAnonymous]
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    [Route("DTLogistica/[controller]")]
    [ApiController]
    public class DtUltimaMillaController : Controller
    {

        private readonly IDtUltimaMillaEncRepositorio _ctRepoUMillaEnc;
        private readonly IDtUltimaMillaDetRepositorio _ctRepoUMillaDet;
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();
        private List<string> _lstErrores = new List<string>();

        //private readonly IOrdenesRepositorio _ctRepoOrdenes;
        //private readonly ApplicationDbContext _db;
        public DtUltimaMillaController(IDtUltimaMillaEncRepositorio ctRepoUMillaEnc, IDtUltimaMillaDetRepositorio ctRepoUMillaDet)
        {


            _ctRepoUMillaEnc = ctRepoUMillaEnc;
            _ctRepoUMillaDet = ctRepoUMillaDet;
        }


        #region UltMillaEnc

        // Función para desactivar la entidad (Baja)
        [HttpPatch("UltMillaEncBaja/{id}")]
        public async Task<IActionResult> UltMillaEncBaja(int id)
        {

            bool resultado = _ctRepoUMillaEnc.BajaGenerico(id, x => x.GetType().GetProperty("Activo"), false);

            if (resultado)
            {
                return Ok($"La entidad {id} fue desactivada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo desactivar la entidad.");
            }
        }


        // Función para agregar entidad: Modelo + Crear
        [HttpPost("UltMillaEncCrear")]
        public async Task<IActionResult> UltMillaEncCrear([FromBody] DtUltimaMillaEnc entidad)
        {
            try
            {
                if (entidad == null)
                {
                    return BadRequest("La entidad no puede ser nula");
                }

                _ctRepoUMillaEnc.AgregarGenerico(entidad);

                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        // Función para guardar cambios: Modelo + Guardar
        [HttpPut("UltMillaEncActualizar")]
        //public async Task<IActionResult> UltMillaEncActualizar([FromBody] DtUltimaMillaEnc entidad)
        //{

        //    if(_ctRepoUMillaEnc.ActualizarGenerico(entidad.IdDtUltMillaEnc,entidad)==null)
        //        return NotFound("fal");
        //    return Ok($"La entidad {entidad.IdDtUltMillaEnc} fue actualizada exitosamente.");
        //}
        public async Task<IActionResult> UltMillaEncActualizar([FromBody] DtUltimaMillaEnc entidad)
        {
            #region Variables
            _respuestaGenericaDTO = new RespuestaGenericaDTO();
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
            _respuestaGenericaDTO.Entidad = null;
            #endregion Variables
            try
            {

                if (!ModelState.IsValid)
                {

                    _respuestaGenericaDTO.lstrErrorMessages.Add(ModelState.ValidationState.ToString());
                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
                    //_lstErrores.Add($"La entidad con ID {entidad.IdDtUltMillaEnc} no fue encontrada.");
                    return NotFound(_respuestaGenericaDTO);
                }


                var entidadExistente = _ctRepoUMillaEnc.obtenerPorIdGenerico(entidad.IdDtUltMillaEnc);

                if (entidadExistente == null)
                {
                    _respuestaGenericaDTO.lstrErrorMessages.Add("$\"La entidad con ID {id} no fue encontrada.\"");
                    _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
                    //_lstErrores.Add($"La entidad con ID {entidad.IdDtUltMillaEnc} no fue encontrada.");
                    return NotFound(_respuestaGenericaDTO);
                }



                if (_ctRepoUMillaEnc.ActualizarGenerico(entidad.IdDtUltMillaEnc, entidad) == null)
                {
                    _respuestaGenericaDTO.lstrErrorMessages.Add($"La entidad {entidad.IdDtUltMillaEnc} fue actualizada exitosamente.");
                    return NotFound(_respuestaGenericaDTO);
                }

                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                _respuestaGenericaDTO.strMensaje = $"La entidad {entidad.IdDtUltMillaEnc} fue actualizada exitosamente.";
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




        // Función para obtener todas las entidades: Modelo + Listar
        [HttpGet("UltMillaEncListar")]
        public async Task<IActionResult> UltMillaEncListar()
        {
            var lista = await _ctRepoUMillaEnc.obtenerListaTodosGenerico();
            return Ok(lista);
        }

        // Función para obtener entidad por ID: Modelo + Obtener
        [HttpGet("UltMillaEncObtener/{id}")]
        public async Task<IActionResult> UltMillaEncObtener(int id)
        {
            var entidad = await _ctRepoUMillaEnc.obtenerUltimaMillaEnc(id);

            if (entidad == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            return Ok(entidad);
        }

        // Función para activar la entidad (Alta)
        [HttpPatch("UltMillaEncAlta/{id}")]
        public async Task<IActionResult> UltMillaEncAlta(int id)
        {
            bool resultado = _ctRepoUMillaEnc.AltaGenerico(id, x => x.GetType().GetProperty("Activo"), true);

            if (resultado)
            {
                return Ok($"La entidad {id} fue activada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo activar la entidad.");
            }
        }


        // Función para obtener entidad por ID: Modelo + Obtener
        [HttpPost("UltMillaEncObtenerSolicitudes")]
        public async Task<IActionResult> UltMillaEncObtenerSolicitudes(FiltroDtUltimaMillaDTO pFiltro)
        {
            var entidad = await _ctRepoUMillaEnc.obtenerUltimaMillaEncs(pFiltro);

            return Ok(entidad);
        }


        [HttpGet("UltMillaEncObtenerDetalles/{idEncabezado}")]
        public async Task<IActionResult> UltMillaEncObtenerDetalles(int idEncabezado)
        {
            var lista = await _ctRepoUMillaDet.ObtenerDetalles(idEncabezado);
            return Ok(lista);
        }

        [HttpPut("UltMillaEncCambiarEstado/{idEncabezado}/{idTipoEstado}")]
        public async Task<IActionResult> UltMillaEncCambiarEstado(int idEncabezado, int idTipoEstado)
        {

            if (idEncabezado <= 0 && idTipoEstado <= 0)
            {
                return NotFound("Error interno");
            }

            var encabezado = await _ctRepoUMillaEnc.obtenerPorIdGenerico(idEncabezado);

            if (encabezado == null)
            {
                return NotFound("Entidad no encontrada");
            }
            //             Terminado             Cancelado
            if (encabezado.IdTipoEstado == 3 || encabezado.IdTipoEstado == 4)
            {
                return BadRequest("No se puede cambiar el estado de la entidad " + idEncabezado);
            }

            bool respuesta = await _ctRepoUMillaEnc.CambioEstado(idEncabezado, idTipoEstado);

            if (respuesta)
            {
                return Ok($"La entidad {idEncabezado} fue actualizada exitosamente.");
            }

            return NotFound("fal");
        }

        #endregion UltMillaEnc

        #region UMillaDet
        // Función para agregar entidad: Modelo + Crear
        [HttpPost("UMillaDetCrear")]
        public async Task<IActionResult> UMillaDetCrear([FromBody] DtUltimaMillaDet entidad)
        {
            try
            {
                if (entidad == null)
                {
                    return BadRequest("La entidad no puede ser nula");
                }

                _ctRepoUMillaDet.AgregarGenerico(entidad);
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Función para guardar cambios: Modelo + Guardar
        [HttpPut("UMillaDetActualizar")]
        public async Task<IActionResult> UMillaDetActualizar([FromBody] DtUltimaMillaDet entidad)
        {
            #region Variables
            _respuestaGenericaDTO = new RespuestaGenericaDTO();
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
            _respuestaGenericaDTO.Entidad = null;
            #endregion Variables
            if (!ModelState.IsValid)
            {

                _respuestaGenericaDTO.lstrErrorMessages.Add(ModelState.ValidationState.ToString());
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
                //_lstErrores.Add($"La entidad con ID {entidad.IdDtUltMillaEnc} no fue encontrada.");
                return NotFound(_respuestaGenericaDTO);
            }

            var entidadExistente = _ctRepoUMillaDet.obtenerPorIdGenerico(entidad.IdDtUltimaMillaDet);

            if (entidadExistente == null)
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add("$\"La entidad con ID {id} no fue encontrada.\"");
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
                //_lstErrores.Add($"La entidad con ID {entidad.IdDtUltMillaEnc} no fue encontrada.");
                return NotFound(_respuestaGenericaDTO);
            }

            if (_ctRepoUMillaDet.ActualizarGenerico(entidad.IdDtUltimaMillaDet, entidad) == null)
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add($"La entidad {entidad.IdDtUltimaMillaDet} fue actualizada exitosamente.");
                return NotFound($"La entidad con ID {entidad.IdDtUltimaMillaDet} no fue encontrada.");
            }

            _respuestaGenericaDTO.IsSuccess = true;
            _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
            _respuestaGenericaDTO.strMensaje = $"La entidad {entidad.IdDtUltimaMillaDet} fue actualizada exitosamente.";
            _respuestaGenericaDTO.Entidad = entidad;

            return Ok(_respuestaGenericaDTO);
        }



        // Función para obtener todas las entidades: Modelo + Listar
        [HttpGet("UMillaDetListar")]
        public async Task<IActionResult> UMillaDetListar()
        {
            var lista = await _ctRepoUMillaDet.obtenerListaTodosGenerico();
            return Ok(lista);
        }

        // Función para obtener entidad por ID: Modelo + Obtener
        [HttpGet("UMillaDetObtener/{id}")]
        public async Task<IActionResult> UMillaDetObtener(int id)
        {
            var entidad = _ctRepoUMillaDet.obtenerPorIdGenerico(id);

            if (entidad == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            return Ok(entidad);
        }

        // Función para activar la entidad (Alta)
        [HttpPatch("UMillaDetAltaDet/{id}")]
        public async Task<IActionResult> UMillaDetAlta(int id)
        {
            bool resultado = _ctRepoUMillaDet.AltaGenerico(id, x => x.GetType().GetProperty("Activo"), true);

            if (resultado)
            {
                return Ok($"La entidad {id} fue activada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo activar la entidad.");
            }
        }

        // Función para desactivar la entidad (Baja)
        [HttpPatch("UMillaDetBajaDet/{id}")]
        public async Task<IActionResult> UMillaDetBaja(int id)
        {
            bool resultado = _ctRepoUMillaDet.BajaGenerico(id, x => x.Activo, false);

            if (resultado)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest("No se pudo desactivar la entidad.");
            }
        }

        #endregion UMillaDet

        #region Utilerias
        //private RespuestaTokenDTO ObtenerSistemaToken()
        //{
        //    #region Variables
        //    RespuestaTokenDTO respuestaTokenDTO = new RespuestaTokenDTO();
        //    respuestaTokenDTO.RolesUsuario = new List<string>();
        //    #endregion Variables
        //    #region Operaciones
        //    try
        //    {

        //        // Get "IdCatCliente" from ClaimTypes.Name
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
        #endregion Utilerias
    }
}
