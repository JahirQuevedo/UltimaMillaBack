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
    public class CatNavierasController : GenericoController<CatNavieras>
    {
        private readonly IGenericoRepositorio<CatNavieras> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatNavierasController(ApplicationDbContext db, IGenericoRepositorio<CatNavieras> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }



        // GET: api/CatNavieras/Filtrar
        [HttpGet("Filtrar")]
        public async Task<ActionResult<ICollection<CatNavieras>>> Filtrar([FromQuery] string prazonSocial, [FromQuery] string prfc)
        {
            var lista = await _repositorio.obtenerListaTodosGenerico();
            var filtrada = lista.Where(t => (string.IsNullOrEmpty(prazonSocial) || t.RazonSocial.Contains(prazonSocial)) &&
                                             (string.IsNullOrEmpty(prfc) || t.RFC.Contains(prfc))).ToList();
            return Ok(filtrada);
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
        // CatClientesListar
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatNavierasCoincidencia(string pCoincidencia)
        {
            var objRespuesta = await _db.catNavieras
                .Where(x => x.Activo == true && x.RazonSocial.Contains(pCoincidencia))
                .Select(c => new
                {
                    Id = c.IdCatNaviera,
                    Nombre = c.RazonSocial,
                    Pais = "",
                    Estado = "",
                    RFC = "",
                    c.Activo,
                    c.Acronimo
                })
                .ToListAsync();

            return Ok(objRespuesta);
        }
    }
}
