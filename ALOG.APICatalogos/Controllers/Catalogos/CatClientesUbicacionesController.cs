using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatClientesUbicacionesController : GenericoController<CatClientesUbicaciones>
    {
        private readonly IGenericoRepositorio<CatClientesUbicaciones> _repositorio;
        private readonly ApplicationDbContext _context;

        public CatClientesUbicacionesController(ApplicationDbContext context, IGenericoRepositorio<CatClientesUbicaciones> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("CrearClientesUbicaciones")]
        public async Task<IActionResult> CrearClientesUbicaciones(CatClientesUbicaciones nuevaUbicacion)
        {
            try
            {
                await _context.catClientesUbicaciones.AddAsync(nuevaUbicacion);
                var response = await _context.SaveChangesAsync();
                return Ok(response); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("ListarClientesUbicaciones")]
        public async Task<IActionResult> ListarClientesUbicaciones()
        {
            try
            {
                var clientesUbicaciones = await _context.catClientesUbicaciones
                    .Include(c => c.catPaises)
                    .Include(c => c.catPaisEstados)
                    .Include(c => c.catPaisMunicipios)
                    .Where(x => x.Activo == true)
                    .ToListAsync();

                return Ok(clientesUbicaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
