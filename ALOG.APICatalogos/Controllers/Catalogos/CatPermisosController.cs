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
    public class CatPermisosController : GenericoController<CatPermisos>
    {
        private readonly IGenericoRepositorio<CatPermisos> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatPermisosController(ApplicationDbContext db, IGenericoRepositorio<CatPermisos> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }

        // CatPermisosCoincidencia
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatPermisosCoincidencia(string pCoincidencia)
        {
            var obj = await _db.catPermisos
                .Where(x => x.Activo == true && x.Nombre.Contains(pCoincidencia))
                .ToListAsync();

            return Ok(obj);
        }
    }
}
