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
    public class CatLineaNegocioController : GenericoController<CatLineaNegocio>
    {
        private readonly IGenericoRepositorio<CatLineaNegocio> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatLineaNegocioController(ApplicationDbContext db, IGenericoRepositorio<CatLineaNegocio> repositorio) : base(repositorio)
        {
            _db = db;
        }

        #region CatLineaNegocios



        // GET: api/CatLineaNegocio/Filtrar
        [HttpGet("Filtrar")]
        public async Task<ActionResult<ICollection<CatLineaNegocio>>> Filtrar([FromQuery] string pNombreLinea, [FromQuery] string pAcronimo)
        {
            var lista = await _repositorio.obtenerListaTodosGenerico();
            var filtrada = lista.Where(t => (string.IsNullOrEmpty(pNombreLinea) || t.Nombre.Contains(pNombreLinea)) &&
                                             (string.IsNullOrEmpty(pAcronimo) || t.Acronimo.Contains(pAcronimo))).ToList();
            return Ok(filtrada);
        }

        // CatClientesLeer
        [HttpGet("TarifasPorLinea/{pIdLineaNegocios}/{pIdAduana}/{pIdServicio}")]
        public async Task<ActionResult<ICollection<CatLineaNegocio>>> TarifasPorLinea(int pIdLineaNegocios, int pIdAduana, int pIdServicio)
        {
            //return _db.peticionesReferencias.Include(c => c.Contenedores).ThenInclude(s => s.Servicios).ThenInclude(x => x.Documentos).Where(c => c.Contenedores.Any(d => d.RefenciaCliente.Equals(referenciaNAD))).ToList();
            var catCliente = await _db.catLineaNegocio
                .Include(c => c.GetCatLineaNegocioTarifas)
                .ThenInclude(x => x.GetCatLineaNegocioTariPrecios)
                .Where(c => c.IdCatLineaNegocio == pIdLineaNegocios && c.GetCatLineaNegocioTarifas.Any(x => x.IdCatLineaNegocio == pIdLineaNegocios && x.GetCatLineaNegocioTariPrecios.Any(d => d.IdCatAduana == pIdAduana))).ToListAsync();
            //.Where(c => c.IdCatLineaNegocio == pIdLineaNegocios && c.GetCatLineaNegocioTarifas.Any(x => x.IdCatLineaNegocio == pIdLineaNegocios)).ToListAsync();

            if (catCliente == null)
                return NotFound();

            return Ok(catCliente);
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatLineasCoincidencia(string pCoincidencia)
        {
            var objRespuesta = await _db.catLineaNegocio
                .Where(x => x.Activo == true &&
                            (x.Nombre).Contains(pCoincidencia)).Select(c => new
                            {
                                Id = c.IdCatLineaNegocio,
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

        #endregion CatLineaNegocios

    }
}
