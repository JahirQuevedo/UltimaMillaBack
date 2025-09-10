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
    public class CatProveedoresTarifaPatioController : GenericoController<CatProveedoresTarifaPatio>
    {
        private readonly IGenericoRepositorio<CatProveedoresTarifaPatio> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatProveedoresTarifaPatioController(ApplicationDbContext db, IGenericoRepositorio<CatProveedoresTarifaPatio> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }

    }
}
