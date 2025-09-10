using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;

namespace ALOG.APICatalogos.Controllers.Catalogos
{
    public class CatTipoCargaController : GenericoController<CatTipoCarga>
    {
        private readonly IGenericoRepositorio<CatTipoCarga> _repositorio;
        private readonly ApplicationDbContext _context;

        public CatTipoCargaController(IGenericoRepositorio<CatTipoCarga> repositorio, ApplicationDbContext context) : base(repositorio)
        {
            _repositorio = repositorio;
            _context = context;
        }
    }
}
