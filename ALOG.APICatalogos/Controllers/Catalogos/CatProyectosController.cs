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
    public class CatProyectosController : GenericoController<CatProyectos>
    {
        private readonly IGenericoRepositorio<CatProyectos> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatProyectosController(ApplicationDbContext db, IGenericoRepositorio<CatProyectos> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }

    }
}
