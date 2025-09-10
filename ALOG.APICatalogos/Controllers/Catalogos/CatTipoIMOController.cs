using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatTipoIMOController : GenericoController<CatTipoIMO>
    {
        private readonly IGenericoRepositorio<CatTipoIMO> _repositorio;
        private readonly ApplicationDbContext _context;

        public CatTipoIMOController(IGenericoRepositorio<CatTipoIMO> repositorio, ApplicationDbContext context) : base(repositorio)
        {
            _repositorio = repositorio;
            _context = context;
        }
    }
}
