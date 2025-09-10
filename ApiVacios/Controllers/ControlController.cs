
using ALOG.Modelos.Modelos.DTO.Control;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiVacios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ControlController : Controller
    {
        private readonly ICatSistemasRepositorio _ctCatSistemas;
        //private readonly ICatUsuariosRepositorio _ctCatUsuarios;
        //private readonly IOrdenesRepositorio _ctRepoOrdenes;

        private readonly IMapper _mapper;
        public ControlController(ICatSistemasRepositorio ctCatSistemas, IMapper mapper, ICatUsuariosRepositorio ctCatUsuarios, IOrdenesRepositorio ctRepoOrdenes)
        {

            _ctCatSistemas = ctCatSistemas;
            //_ctCatUsuarios = ctCatUsuarios;
            //_ctRepoOrdenes = ctRepoOrdenes;

            _mapper = mapper;

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

        //[AllowAnonymous]
        //[HttpPost("loginUser", Name = "ObtenerTokenUser")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult ObtenerTokenUser([FromBody] SistemaLoginDTO usuarioLoginDTO)
        //{
        //    #region Variables

        //    #endregion Variables

        //    #region Operaciones

        //    var usuarioLogin = _ctCatUsuarios.LoginUser(usuarioLoginDTO);
        //    if (usuarioLogin == null)
        //    {
        //        return NotFound(usuarioLogin);
        //    }
        //    return Ok(usuarioLogin);


        //    #endregion Operaciones

        //}


    }
}
