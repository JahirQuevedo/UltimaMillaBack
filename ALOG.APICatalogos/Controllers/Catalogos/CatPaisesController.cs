using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVacios.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatPaisesController : GenericoController<CatPaises>
    {
        private readonly IGenericoRepositorio<CatPaises> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatPaisesController(ApplicationDbContext db, IGenericoRepositorio<CatPaises> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }

        // CatPermisosCoincidencia
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatPaisesCoincidencia(string pCoincidencia)
        {
            var obj = await _db.catPaises
                .Where(x => x.Activo == true && x.Nombre.Contains(pCoincidencia))
                .ToListAsync();

            return Ok(obj);
        }
    }
}
