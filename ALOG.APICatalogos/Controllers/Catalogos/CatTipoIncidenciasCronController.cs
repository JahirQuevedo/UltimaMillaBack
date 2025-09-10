using System.Net;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatTipoIncidenciasCronController : GenericoController<CatTipoIncidenciaCron>
    {
        private readonly IGenericoRepositorio<CatTipoIncidenciaCron> _repositorio;
        private readonly ApplicationDbContext _context;
        private ICatTipoIncidenciasCronRepositorio _iCatTipoIncidenciasCronRepositorio;
        public CatTipoIncidenciasCronController(ApplicationDbContext context,
            ICatTipoIncidenciasCronRepositorio CatTipoIncidenciasCronRepositorio, 
            IGenericoRepositorio<CatTipoIncidenciaCron> repositorio) : base(repositorio)
        {
            _context = context;
            _iCatTipoIncidenciasCronRepositorio = CatTipoIncidenciasCronRepositorio;
        }


        [HttpGet("ObtenerRelacionIncidenciaEvento")]
        public async Task<IActionResult> CatTipoIncidenciasCronRepositorio()
        {
            var objRespuesta = await _iCatTipoIncidenciasCronRepositorio.obtenerCatTipoIncidenciaEvento();
            if (objRespuesta.Count() > 0)
                return Ok(objRespuesta);

            return BadRequest(objRespuesta);
        }
    }
}
