using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiVacios.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatPaisEstadosController : GenericoController<CatPaisEstados>
    {
        private readonly IGenericoRepositorio<CatPaisEstados> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatPaisEstadosController(ApplicationDbContext db, IGenericoRepositorio<CatPaisEstados> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }
    }
}
