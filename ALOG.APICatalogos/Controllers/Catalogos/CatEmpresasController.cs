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
    public class CatEmpresasController : GenericoController<CatEmpresas>
    {
        private readonly IGenericoRepositorio<CatEmpresas> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatEmpresasController(ApplicationDbContext db, IGenericoRepositorio<CatEmpresas> repositorio) : base(repositorio)
        {
            _db = db;
        }


        [HttpGet("Filtrar")]
        public async Task<ActionResult<ICollection<CatEmpresas>>> Filtrar([FromQuery] string prazonSocial, [FromQuery] string prfc)
        {
            var lista = await _repositorio.obtenerListaTodosGenerico();
            var filtrada = lista.Where(t => (string.IsNullOrEmpty(prazonSocial) || t.RazonSocial.Contains(prazonSocial)) &&
                                             (string.IsNullOrEmpty(prfc) || t.RFC.Contains(prfc))).ToList();
            return Ok(filtrada);
        }
    }
}
