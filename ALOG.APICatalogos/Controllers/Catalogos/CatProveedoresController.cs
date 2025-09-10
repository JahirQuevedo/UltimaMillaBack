using ALOG.Modelos.Modelos.Catalogos;
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
    public class CatProveedoresController : GenericoController<CatProveedores>
    {
        private readonly IGenericoRepositorio<CatProveedores> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatProveedoresController(ApplicationDbContext db, IGenericoRepositorio<CatProveedores> repositorio) : base(repositorio)
        {
            _db = db;
            _repositorio = repositorio;
        }



        // CatProveedoresListar
        [HttpGet("ListarDetalle")]
        public async Task<IActionResult> CatProveedoresListar()
        {
            var objcatProveedores = await _db.catProveedores
                .Include(c => c.CatPaises)
                .Include(c => c.catEstados)
                .ToListAsync();

            return Ok(objcatProveedores);
        }

        // CatClientesListar
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatProveedoresCoincidencia(string pCoincidencia)
        {
            var obj = await _db.catProveedores
                .Include(c => c.CatPaises)
                .Include(c => c.catEstados).Where(x => x.Activo == true && x.RazonSocial.Contains(pCoincidencia))
                .Select(c => new
                {
                    Id = c.IdCatProveedor,
                    c.RazonSocial,
                    Pais = c.CatPaises.Nombre,
                    Estado = c.catEstados.Nombre,
                    c.RFC,
                    c.Activo,
                    c.Acronimo
                })
                .ToListAsync();

            return Ok(obj);
        }

        [HttpGet("ObtenerPorRFC")]
        public async Task<IActionResult> CatProveedoresPorRFC(string pProveedorRFC)
        {
            var proveedorRFC = Regex.Replace(pProveedorRFC, @"[^A-Za-z0-9]", "");

            var catProveedores = await _db.catProveedores.Where(c => c.RFC.Equals(proveedorRFC.ToUpper().Trim())).FirstOrDefaultAsync();

            if (catProveedores == null)
                return NotFound($"No se encontró el proveedor con el RFC '{pProveedorRFC}'.");

            return Ok(catProveedores);
        }

    }
}
