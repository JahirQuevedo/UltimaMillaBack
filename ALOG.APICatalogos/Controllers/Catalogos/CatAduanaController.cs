using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVacios.Controllers.Catalogos
{
    //[Authorize(Roles = "ADMIN,ADMINUSER")]
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatAduanaController : GenericoController<CatAduana>
    {
        private readonly IGenericoRepositorio<CatAduana> _repositorio;
        private readonly ApplicationDbContext _context;
        public CatAduanaController(ApplicationDbContext context, IGenericoRepositorio<CatAduana> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _context = context;

        }
        //// Función para filtrar por múltiples parámetros
        //[Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE")]
        //[HttpPost("Filtrar")]
        //public async Task<IActionResult> Filtrar([FromBody] Dictionary<string, object> filtros)
        //{
        //    // Implementar lógica de filtrado aquí usando los parámetros recibidos en `filtros`
        //    // Ejemplo: filtros["RazonSocial"], filtros["Activo"]
        //    // Tendrías que aplicar linq dinámico para manejar los filtros.

        //    // TODO: Implementar lógica de filtrado.
        //    return Ok();
        //}

        // CatClientesListar
        [HttpGet("ListarDetalle")]
        public async Task<IActionResult> CatAduanasListar()
        {
            var catClientes = await _context.catAduana
                .Include(c => c.catPaises)
                .Include(c => c.catEstados).Where(x => x.Activo == true)
                .ToListAsync();

            return Ok(catClientes);
        }


        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
        // CatClientesListar
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatAduanasCoincidencia(string pCoincidencia)
        {
            var objRespuesta = await _context.catAduana
                .Include(c => c.catPaises)
                .Include(c => c.catEstados).Where(x => x.Activo == true && x.Nombre.Contains(pCoincidencia))
                .Select(c => new
                {
                    Id = c.IdCatAduana,
                    RazonSocial = c.Nombre,
                    Pais = c.catPaises.Nombre,
                    Estado = c.catEstados.Nombre,
                    RFC = "",
                    c.Activo,
                    c.Acronimo
                })
                .ToListAsync();

            return Ok(objRespuesta);
        }


    }
}
