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
    public class CatUsuariosAduanasController : GenericoController<CatUsuariosAduanas>
    {
        private readonly IGenericoRepositorio<CatUsuariosAduanas> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatUsuariosAduanasController(ApplicationDbContext db, IGenericoRepositorio<CatUsuariosAduanas> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }
    }
}
