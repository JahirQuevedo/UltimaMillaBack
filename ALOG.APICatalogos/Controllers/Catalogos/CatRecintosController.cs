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
    public class CatRecintosController : GenericoController<CatRecintos>
    {
        private readonly IGenericoRepositorio<CatRecintos> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatRecintosController(ApplicationDbContext db, IGenericoRepositorio<CatRecintos> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }


        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
        // CatClientesListar
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatRecintosCoincidencia(string pCoincidencia)
        {
            var objRespuesta = await _db.catRecintos
                .Where(x => x.Activo == true && x.Nombre.Contains(pCoincidencia))
                .Select(c => new
                {
                    Id = c.IdCatPais,
                    RazonSocial = c.Nombre,
                    Pais = "",
                    Estado = "",
                    RFC = "",
                    c.Activo,
                    Acronimo = ""
                })
                .ToListAsync();

            return Ok(objRespuesta);
        }
    }
}
