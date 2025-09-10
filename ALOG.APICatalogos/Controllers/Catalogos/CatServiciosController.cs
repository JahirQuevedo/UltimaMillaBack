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
    public class CatServiciosController : GenericoController<CatServicios>
    {
        private readonly ApplicationDbContext _db;

        public CatServiciosController(ApplicationDbContext db, IGenericoRepositorio<CatServicios> repositorio) : base(repositorio)
        {
            _db = db;
        }



        // CatServiciosListar
        [HttpGet("ListarDetalle")]
        public async Task<IActionResult> CatServiciosListar()
        {
            var objcat = await _db.catServicios
                .Include(c => c.catEmpresas)
                .Include(c => c.catUsuarios)
                .ToListAsync();

            return Ok(objcat);
        }

        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatServiciosCoincidencia(string pCoincidencia)
        {
            var obj = await _db.catServicios
                .Include(c => c.catEmpresas)
                .Include(c => c.catUsuarios).Where(x => x.Activo == true && x.Nombre.Contains(pCoincidencia))
                .ToListAsync();

            return Ok(obj);
        }

    }
}
