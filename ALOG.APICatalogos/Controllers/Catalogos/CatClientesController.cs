using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace ApiVacios.Controllers.Catalogos
{

    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatClientesController : GenericoController<CatClientes>
    {
        private readonly ApplicationDbContext _context;

        public CatClientesController(ApplicationDbContext context, IGenericoRepositorio<CatClientes> repositorio) : base(repositorio)
        {
            _context = context;
        }


        #region CatClientes

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
        [HttpGet("ListarDetalle")]
        public async Task<IActionResult> CatClientesListarDetalle()
        {
            var catobj = await _context.catClientes
                .ToListAsync();

            return Ok(catobj);
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE,SISTEMA")]
        [HttpGet("CatClientesListar")]
        public async Task<IActionResult> CatClientesListar(bool pActivo)
        {
            var catClientes = await _context.catClientes
                .Where(x => x.Activo == pActivo)
                .ToListAsync();

            return Ok(catClientes);
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTES,SISTEMA")]
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatClientesCoincidencia(string pCoincidencia) 
        {
            var objRespuesta = new List<RespListarCoincidenciasDTO>();

            objRespuesta = await _context.catClientes.Where(x => x.Activo == true && x.RazonSocial.Contains(pCoincidencia))
                .Select(c => new RespListarCoincidenciasDTO
                {
                    Id = c.IdCatCliente,
                    RazonSocial = c.RazonSocial,
                    RFC = c.RFC
                })
                .ToListAsync();

            if (objRespuesta.Any())
                return Ok(objRespuesta);
            else
                return NotFound(objRespuesta);
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTES,SISTEMA")]
        [HttpGet("ListarCoincidenciaWMS/{pCoincidencia}")]
        public async Task<IActionResult> CatClientesCoincidenciaWMS(string pCoincidencia)
        {
            var objRespuesta = await _context.catClientes
              .Include(c => c.CatPaises)
              .Include(c => c.catEstados).Where(x => x.Activo == true && x.RazonSocial.Contains(pCoincidencia))
              .Select(c => new
              {
                  Id = c.IdCatCliente,
                  RazonSocial = c.RazonSocial,
                  Pais = c.CatPaises.Nombre,
                  Estado = c.catEstados.Nombre,
                  RFC = "",
                  c.Activo,
                  c.Acronimo
              })
              .ToListAsync();
            return Ok(objRespuesta);
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTES,SISTEMA")]
        [HttpGet("ObtenerPorRFC")]
        public async Task<IActionResult> CatclientesPorRFC(string pClienteRFC)
        {
            var varRFC = Regex.Replace(pClienteRFC, @"[^A-Za-z0-9]", "");
            var objCatClientes = await _context.catClientes.Where(c => c.RFC.Equals(varRFC.ToUpper().Trim())).FirstOrDefaultAsync();

            if (objCatClientes == null)
                return NotFound($"No se encontró un cliente con el RFC '{pClienteRFC}'.");

            return Ok(objCatClientes);
        }

        #endregion CatClientes
    }
}
