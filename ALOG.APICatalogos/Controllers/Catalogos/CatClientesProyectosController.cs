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
    public class CatClientesProyectosController : GenericoController<CatClientesProyectos>
    {
        private readonly IGenericoRepositorio<CatNavieras> _repositorio;
        private readonly ApplicationDbContext _db;
        public CatClientesProyectosController(ApplicationDbContext db, IGenericoRepositorio<CatClientesProyectos> repositorio) : base(repositorio)
        {
            _db = db;
        }
    }
}
