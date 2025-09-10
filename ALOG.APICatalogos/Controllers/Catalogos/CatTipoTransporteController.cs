using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatTipoTransporteController : GenericoController<CatTipoTransporte>
    {
        private readonly IGenericoRepositorio<CatTipoTransporte> _repositorio;
        private readonly ApplicationDbContext _context;

        public CatTipoTransporteController(ApplicationDbContext context, IGenericoRepositorio<CatTipoTransporte> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _context = context;
        }
        
    }
}
