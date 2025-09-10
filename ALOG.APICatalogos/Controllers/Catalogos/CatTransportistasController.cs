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
    public class CatTransportistasController : GenericoController<CatTransportistas>
    {
        private readonly IGenericoRepositorio<CatTransportistas> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatTransportistasController(ApplicationDbContext db, IGenericoRepositorio<CatTransportistas> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }



        // GET: api/CatTransportistas/Filtrar
        [HttpGet("Filtrar")]
        public async Task<ActionResult<ICollection<CatTransportistas>>> Filtrar([FromQuery] string razonSocial, [FromQuery] string ciudad)
        {
            var lista = await _repositorio.obtenerListaTodosGenerico();
            var filtrada = lista.Where(t => (string.IsNullOrEmpty(razonSocial) || t.RazonSocial.Contains(razonSocial)) &&
                                             (string.IsNullOrEmpty(ciudad) || t.Ciudad.Contains(ciudad))).ToList();
            return Ok(filtrada);
        }
    }
}
