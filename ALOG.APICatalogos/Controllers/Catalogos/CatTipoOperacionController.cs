using Microsoft.AspNetCore.Components;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using ALOG.Modelos.Modelos.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatTipoOperacionController : GenericoController<CatTipoOperacion>
    {
        private readonly IGenericoRepositorio<CatTipoOperacion> _repositorio;
        private readonly ApplicationDbContext _context;

        public CatTipoOperacionController(ApplicationDbContext context, IGenericoRepositorio<CatTipoOperacion> repositorio) : base(repositorio)
        { 
            _repositorio = repositorio;
            _context = context;
        }

    }
}
