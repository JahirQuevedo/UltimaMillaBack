using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Control;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.DTO.Utilerias;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Control;
using ALOG.Repositorios.Repositorio.Control.IControl;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Net;
using System.Text.Json;

namespace ALOG.APIControl.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class ControlController : Controller
    {
        private readonly ICatSistemasRepositorio _ctCatSistemas;
        private readonly ICatUsuariosRepositorio _ctCatUsuarios;
        private readonly IOrdenesRepositorio _ctRepoOrdenes;
        private readonly IControlAccesoRepo _ctControlAccesoRepo;


        public ControlController(ICatSistemasRepositorio ctCatSistemas, ICatUsuariosRepositorio ctCatUsuarios, IOrdenesRepositorio ctRepoOrdenes, IControlAccesoRepo controlAccesoRepo)
        {

            _ctCatSistemas = ctCatSistemas;
            _ctCatUsuarios = ctCatUsuarios;
            _ctRepoOrdenes = ctRepoOrdenes;
            _ctControlAccesoRepo = controlAccesoRepo;



        }

        [AllowAnonymous]
        [HttpPost("login", Name = "ObtenerTokenSistemas")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerTokenSistemas([FromBody] SistemaLoginDTO usuarioLoginDTO)
        {
            #region Variables

            #endregion Variables

            #region Operaciones

            var usuarioLogin = _ctCatSistemas.Login(usuarioLoginDTO);
            if (usuarioLogin == null)
            {
                return NotFound(usuarioLogin);
            }
            return Ok(usuarioLogin);


            #endregion Operaciones

        }

        [AllowAnonymous]
        [HttpPost("loginUser", Name = "ObtenerTokenUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerTokenUser(SistemaLoginDTO usuarioLoginDTO)
        {
            #region Variables

            #endregion Variables

            #region Operaciones

            var usuarioLogin = await _ctCatUsuarios.LoginUser(usuarioLoginDTO);
            if (usuarioLogin == null)
            {
                return NotFound(usuarioLogin);
            }

            return Ok(usuarioLogin);


            #endregion Operaciones

        }

        [HttpPost("ObtenerCatRolesPermisos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerCatRolesPermisos([FromBody] SolDatosUsuariosDTO filtros)
        {
            // Implementar lógica para obtener CatRolesPermisos
            CatRolesPermisos result = await _ctControlAccesoRepo.ObtenerCatRolesPermisos(filtros.IdCatRoles, filtros.Activo);
            return Ok(result);
        }

        [HttpPost("ObtenerCatUsuarios")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerCatUsuarios([FromBody] SolDatosUsuariosDTO filtros)
        {
            // Implementar lógica para obtener CatUsuarios
            CatUsuarios result = await _ctControlAccesoRepo.ObtenerCatUsuarios(filtros.IdCatUsuario, filtros.Activo);
            return Ok(result);
        }

        [HttpPost("ObtenerCatUsuariosPermisos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerCatUsuariosPermisos([FromBody] SolDatosUsuariosDTO filtros)
        {
            // Implementar lógica para obtener CatUsuariosPermisos
            CatUsuariosPermisos result = await _ctControlAccesoRepo.ObtenerCatUsuariosPermisos(filtros.IdCatUsuario, filtros.Activo);
            return Ok(result);
        }

        [HttpPost("ObtenerCatUsuariosRoles")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerCatUsuariosRoles([FromBody] SolDatosUsuariosDTO filtros)
        {
            // Implementar lógica para obtener CatUsuariosRoles
            CatUsuarioRoles result = await _ctControlAccesoRepo.ObtenerCatUsuariosRoles(filtros.IdCatUsuario, filtros.Activo);
            return Ok(result);
        }

        [HttpPost("EncriptarFiltro")]
        [ProducesResponseType(201, Type = typeof(FiltroBaseDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EncriptarFiltro([FromBody] JsonElement json) {

            string filtroEncriptado = "";


            try {
                if (json.TryGetProperty("IdOrden", out _)) {
                    var modelo = JsonSerializer.Deserialize<FiltrosCancelacionElementoDTO>(json.GetRawText());
                    filtroEncriptado = AesEncryptionHelper.Encrypt(FiltroMapperHelper.ConvertirFiltroAString(modelo));
                    return Ok(filtroEncriptado);
                }

                if (json.TryGetProperty("ProveedorInfo", out _)) {
                    var modelo = JsonSerializer.Deserialize<FiltroPatioDTO>(json.GetRawText());
                    filtroEncriptado = AesEncryptionHelper.Encrypt(FiltroMapperHelper.ConvertirFiltroAString(modelo));
                    return Ok(filtroEncriptado);
                }

                if (json.TryGetProperty("IdCatEmpresa", out _)) {
                    var modelo = JsonSerializer.Deserialize<FiltroTarifarioServicioDTO>(json.GetRawText());
                    filtroEncriptado = AesEncryptionHelper.Encrypt(FiltroMapperHelper.ConvertirFiltroAString(modelo));
                    return Ok(filtroEncriptado);
                }

                return BadRequest("No se reconoce el tipo de filtro enviado.");
            } catch (Exception ex) {
                return BadRequest($"Error en la deserialización: {ex.Message}");
            }

        }


    }
}
