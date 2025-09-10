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
    public class CatPatiosConfigController : GenericoController<CatPatiosConfig>
    {
        private readonly IGenericoRepositorio<CatPatiosConfig> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatPatiosConfigController(ApplicationDbContext db, IGenericoRepositorio<CatPatiosConfig> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }
    }
}
