using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APICatalogos.Controllers.Catalogos
{

    //[Authorize(Roles = "ADMIN,ADMINUSER")]
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatTipoMonedaController : GenericoController<CatTipoMoneda>
    {

        private readonly ApplicationDbContext _context;
        private readonly ICatTipoMonedaRepositorio _ctRepoTipoMoneda;
        IGenericoRepositorio<CatTipoMoneda> _repositorio;

        public CatTipoMonedaController(ApplicationDbContext context, IGenericoRepositorio<CatTipoMoneda> repositorio) : base(repositorio)
        {
            _context = context;
            _repositorio = repositorio;
        }
    }
}
