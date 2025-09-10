using ALOG.Modelos.Modelos.DTO.Control;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ALOG.APIFacturacion.Controllers
{
    [Authorize(Roles = "ADMIN,SISTEMA,ADMINUSER,USUARIO")]
    [Route("api/[controller]")]
    [ApiController]
    public class ControlController : Controller
    {
        private readonly ICatSistemasRepositorio _ctCatSistemas;
        private readonly ICatUsuariosRepositorio _ctCatUsuarios;

        private readonly IMapper _mapper;
        public ControlController(ICatSistemasRepositorio ctCatSistemas, IMapper mapper, ICatUsuariosRepositorio ctCatUsuarios, IOrdenesRepositorio ctRepoOrdenes)
        {

            _ctCatSistemas = ctCatSistemas;
            _ctCatUsuarios = ctCatUsuarios;

            _mapper = mapper;

        }

        [AllowAnonymous]
        [HttpPost("login")]
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
    }
}
