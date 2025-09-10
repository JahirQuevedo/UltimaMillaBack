using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using System.Net;

namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatTipoEventosCronController : GenericoController<CatTipoEventosCron>
    {
        private readonly IGenericoRepositorio<CatTipoEventosCron> _repositorio;
        private readonly ApplicationDbContext _context;
        //private ICatTipoEventosCronRepositorio _iCatTipoEventosCronRepositorio;

        public CatTipoEventosCronController(ApplicationDbContext context,
            //ICatTipoEventosCronRepositorio catTipoEventosCronRepositorio,
            IGenericoRepositorio<CatTipoEventosCron> repositorio) : base(repositorio)
        {
            _context = context;
            //_iCatTipoEventosCronRepositorio = catTipoEventosCronRepositorio;
            _repositorio = repositorio;

        }

        //[Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]

        //[HttpGet("ListarCoincidencia")]
        //public async Task<IActionResult> CatTipoEventosCronRepositorio()
        //{
        //    var resp = new RespuestaGenericaDTO
        //    {
        //        StatusCode = HttpStatusCode.BadRequest,
        //        IsSuccess = false,
        //        Entidades = new List<object>()
        //    };

        //    var objRespuesta = await _iCatTipoEventosCronRepositorio.obtenerTipoEventosCronCoincidencia();
        //    if (objRespuesta != null)
        //    {
        //        resp.Entidades.AddRange(objRespuesta);

        //        resp.StatusCode = HttpStatusCode.OK;
        //        resp.IsSuccess = true;

        //        return Ok(resp);
        //    }

        //    resp.strMensaje = "NO SE ENCONTRO CATALOGO";
        //    return BadRequest(resp);
        //}
    }

}
