using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatTiposContenedorController : GenericoController<CatTipoContenedor>
    {
        private readonly ApplicationDbContext _context;

        public CatTiposContenedorController(ApplicationDbContext context, IGenericoRepositorio<CatTipoContenedor> repositorio) : base(repositorio)
        {
            _context = context;
        }
    }
}
