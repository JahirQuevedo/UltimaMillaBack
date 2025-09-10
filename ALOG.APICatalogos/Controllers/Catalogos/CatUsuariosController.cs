using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Utilerias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVacios.Controllers.Catalogos
{
    //[Authorize(Roles = "ADMIN,ADMINUSER")]
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatUsuariosController : Controller
    {
        private ICatUsuariosRepositorio _icatUsuariosRepositorio;
        private readonly ApplicationDbContext _db;
        private RespuestaGenericaDTO respuestaGenericaDTO;
        private UtileriasRespuestas objUtileriasRespuestas = new UtileriasRespuestas();

        public CatUsuariosController(ApplicationDbContext db, ICatUsuariosRepositorio catUsuariosRepositorio)
        {

            _db = db;
            _icatUsuariosRepositorio = catUsuariosRepositorio;

            #region InitRespuestaGen
            respuestaGenericaDTO = new RespuestaGenericaDTO();
            respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.NotFound;
            respuestaGenericaDTO.IsSuccess = false;
            respuestaGenericaDTO.strMensaje = "No se pudo crear el usuario";
            respuestaGenericaDTO.lstrErrorMessages = null;
            respuestaGenericaDTO.Entidad = null;
            respuestaGenericaDTO.Entidades = null;
            #endregion InitRespuestaGen
        }

        [HttpPost("CrearUsuario/")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CrearUsuario([FromBody] CatUsuarios pCatUsuario)
        {
            #region Variables
            respuestaGenericaDTO = new RespuestaGenericaDTO();
            respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();

            #endregion Variables

            #region Operaciones
            if (ModelState.IsValid)
            {
                if (pCatUsuario == null)
                {
                    respuestaGenericaDTO.strMensaje = "Objeto nulo";
                    return BadRequest(respuestaGenericaDTO);
                }

                if (_db.catUsuarios.Any(u => u.Usuario.Trim().ToUpper() == pCatUsuario.Usuario.Trim().ToUpper()))
                {
                    respuestaGenericaDTO.strMensaje = "El usuario ya existe";
                    return BadRequest(respuestaGenericaDTO);
                }
                if (pCatUsuario.passSistema.Trim().Length <= 8)
                {
                    respuestaGenericaDTO.strMensaje = "Contraseña menor a 8 caracteres ";
                    return BadRequest(respuestaGenericaDTO);
                }

                // Decodificar el parámetro
                //string decodedParameter = HttpUtility.UrlDecode(referenciaNAD);
                var objCatUsuario = await _icatUsuariosRepositorio.CrearUsuario(pCatUsuario);

                if (objCatUsuario != null)
                {

                    respuestaGenericaDTO.Entidad = objCatUsuario;
                    respuestaGenericaDTO.IsSuccess = true;
                    respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(respuestaGenericaDTO);
                }
                else
                {
                    return BadRequest(respuestaGenericaDTO);
                }
            }
            else
            {
                respuestaGenericaDTO.strMensaje = ModelState.ToString();
                return BadRequest(respuestaGenericaDTO);
            }

            #endregion Operaciones
        }


        [HttpPost("BajaUsuario/")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BajaUsuario([FromBody] CatUsuarios pCatUsuario)
        {
            #region Variables            
            respuestaGenericaDTO = new RespuestaGenericaDTO();
            respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            #endregion Variable

            #region Operaciones
            if (ModelState.IsValid)
            {
                if (pCatUsuario == null)
                {
                    respuestaGenericaDTO.strMensaje = "Objeto nulo";
                    return BadRequest(respuestaGenericaDTO);
                }

                if (!_db.catUsuarios.Any(u => u.Usuario.Trim().ToUpper() == pCatUsuario.Usuario.Trim().ToUpper()))
                {
                    respuestaGenericaDTO.strMensaje = "El usuario no existe";
                    return BadRequest(respuestaGenericaDTO);
                }


                // Decodificar el parámetro
                //string decodedParameter = HttpUtility.UrlDecode(referenciaNAD);
                var objCatUsuario = await _icatUsuariosRepositorio.CrearUsuario(pCatUsuario);

                if (objCatUsuario != null)
                {

                    respuestaGenericaDTO.Entidad = objCatUsuario;
                    respuestaGenericaDTO.IsSuccess = true;
                    respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(respuestaGenericaDTO);
                }
                else
                {
                    return BadRequest(respuestaGenericaDTO);
                }
            }
            else
            {
                respuestaGenericaDTO.strMensaje = ModelState.ToString();
                return BadRequest(respuestaGenericaDTO);
            }

            #endregion Operaciones
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatUsuariosCoincidencia(string pCoincidencia)
        {
            var objRespuesta = await _db.catUsuarios
                .Where(x => x.Activo == true &&
                            (x.Nombre + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno).Contains(pCoincidencia))
                .ToListAsync();

            return Ok(objRespuesta);
        }

    }
}
