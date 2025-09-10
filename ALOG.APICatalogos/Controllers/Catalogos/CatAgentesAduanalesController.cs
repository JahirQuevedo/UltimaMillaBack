using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatAgentesAduanalesController : GenericoController<CatAgentesAduanales>
    {
        private readonly ApplicationDbContext _context;


        public CatAgentesAduanalesController(ApplicationDbContext context, IGenericoRepositorio<CatAgentesAduanales> repositorio) : base(repositorio)
        {
            _context = context;
        }

        #region CatAgentesAduanales

        [HttpGet("ListarDetalle")]
        public async Task<IActionResult> CatAgentesAduanalesListarDetalle()
        {
            var objCatAgentesAduanales = await _context.catAgentesAduanales
                .ToListAsync();

            return Ok(objCatAgentesAduanales);
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
        [HttpGet("ListarCatalogoAgentesAduanales")]
        public async Task<IActionResult> CatAgentesAduanalesListar(bool pActivo)
        {
            var objCatAgentesAduanales = await _context.catAgentesAduanales
                .Where(x => x.Activo == pActivo)
                .ToListAsync();

            return Ok(objCatAgentesAduanales);
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTES,SISTEMA")]
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatAgentesAduanalesCoincidencia(string pCoincidencia)
        {
            var objCatAgentesAduanales = await _context.catAgentesAduanales
                .Where(x => x.Activo == true && x.Nombre.Contains(pCoincidencia))
                .ToListAsync();

            return Ok(objCatAgentesAduanales);
        }

        [HttpGet("ObtenerPorRFC")]
        public async Task<IActionResult> CatAgentesAduanalesPorRFC(string pAgenteAduanalRFC)
        {
            var varRFC = Regex.Replace(pAgenteAduanalRFC, @"[^A-Za-z0-9]", "");
            var objCatAgentesAduanales = await _context.catAgentesAduanales.Where(c => c.RFC.Equals(varRFC.ToUpper().Trim())).FirstOrDefaultAsync();

            if (objCatAgentesAduanales == null)
                return NotFound($"No se encontró un cliente con el RFC '{pAgenteAduanalRFC}'.");

            return Ok(objCatAgentesAduanales);
        }

        #endregion CatAgentesAduanales


    }
}
