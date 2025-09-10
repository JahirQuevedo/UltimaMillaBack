using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ALOG.Modelos.Modelos.DTO.Utilerias;
using ALOG.Modelos.Modelos.DTO.Catalogos;


namespace ApiVacios.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatPatiosController : GenericoController<CatPatios>
    {
        private readonly IGenericoRepositorio<CatPatios> _repositorio;
        private readonly ApplicationDbContext _db;
        public CatPatiosController(ApplicationDbContext db, IGenericoRepositorio<CatPatios> repositorio) : base(repositorio)
        {
            _db = db;
            _repositorio = repositorio;
        }



        // GET: api/CatPatios/Filtrar
        [HttpGet("Filtrar/{pRazonSocial}/{pAduana:int}")]
        public async Task<ActionResult<ICollection<RespuestaGenericaCatalogosDTO>>> Filtrar(string pRazonSocial, int? pAduana)
        {
            pAduana = pAduana == null ? 0 : pAduana;
            RespuestaGenericaCatalogosDTO objRespuestaGenericaCatalogosDTO = new RespuestaGenericaCatalogosDTO();
            var lista = await _repositorio.obtenerListaTodosGenerico();
            var filtrada = lista.Where(t => (string.IsNullOrEmpty(pRazonSocial) || t.RazonSocial.Contains(pRazonSocial))
                            && t.Activo == true
                            //&& (pAduana <= 0 || t.catProveedoresPatios.Any(p => p.IdCatAduana == pAduana))
                            )
                .Select(x => new RespuestaGenericaCatalogosDTO
                {
                    Id = x.IdCatPatios,
                    //CAMBIO RAZE
                    IdCatAduana = 0, //x.catProveedoresPatios.FirstOrDefault(p => p.IdCatPatio == x.IdCatPatios && p.catAduana.IdCatAduana == pAduana).IdCatAduana,
                    Nombre = x.RazonSocial,


                }).Distinct().ToList();

            //CAMBIO RAZE
            return Ok(filtrada);
        }

        // CatPatiosListar
        [HttpGet("ListarCoincidencia/{pCoincidencia}")]
        public async Task<IActionResult> CatPatiosCoincidencia(string pCoincidencia)
        {
            var lstresultado = await _db.catPatios
                .Include(c => c.catPatiosNavieras)
                .Include(c => c.catPatiosConfigs)
                .Include(cp => cp.catProveedoresPatios)
                .Where(x => x.Activo == true && x.RazonSocial.Contains(pCoincidencia))
                .Select(c => new
                {
                    Id = c.IdCatPatios,
                    c.RazonSocial,
                    IdCatProveedor = c.catProveedoresPatios.IdCatProveedor,
                    IdAduana = c.catProveedoresPatios.IdCatAduana,
                    c.Activo,
                    c.Acronimo
                })
                .ToListAsync();

            return Ok(lstresultado);
        }


        // Lista los patios de acuerdo al acronimo, razon social o rfc del proveedor o razon social del patio
        // y puede filtrar por aduana u obtener todos los patios de una aduana
        [HttpGet("ObtenerListaPatiosFiltrados/{parametroEncriptado}")]
        public async Task<IActionResult> ObtenerListaPatiosFiltrados(string parametroEncriptado)
        {
            var lstCatPatios = new List<CatPatios>();
            var filtroPatio = new FiltroPatioDTO();

            if (string.IsNullOrWhiteSpace(parametroEncriptado))
                return BadRequest("El parámetro encriptado no puede estar vacío.");

            string textoPlano;
            try
            {
                textoPlano = AesEncryptionHelper.Decrypt(parametroEncriptado);
            }
            catch
            {
                return BadRequest("No se pudo desencriptar el parámetro.");
            }

            if (!string.IsNullOrWhiteSpace(textoPlano))
            {
                var parametrosDiccionario = textoPlano.Split('&', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Split('=', 2))
                .Where(p => p.Length == 2)
                .ToDictionary(p => p[0], p => p[1]);

                if (!FiltroMapperHelper.TryObtenerFiltros(parametrosDiccionario, out filtroPatio))
                    return BadRequest("No se pudieron interpretar los filtros enviados.");
            }

            lstCatPatios = await _db.catProveedoresPatios
                .Include(pp => pp.catProveedores)
                .Include(pp => pp.catPatios)
                .Where(pp =>
                    (string.IsNullOrEmpty(filtroPatio.ProveedorInfo) ||
                        pp.catProveedores.Acronimo.Contains(filtroPatio.ProveedorInfo) ||
                        pp.catProveedores.RazonSocial.Contains(filtroPatio.ProveedorInfo) ||
                        pp.catProveedores.RFC.Contains(filtroPatio.ProveedorInfo) ||
                        pp.catPatios.RazonSocial.Contains(filtroPatio.ProveedorInfo)) &&
                    (!filtroPatio.IdCatAduana.HasValue || pp.IdCatAduana == filtroPatio.IdCatAduana.Value) &&
                    pp.Activo
                )
                .Select(pp => pp.catPatios).Distinct()
                .ToListAsync();

            return lstCatPatios.Any() ? Ok(lstCatPatios) : NotFound("No se encontraron patios con los filtros proporcionados.");
        }

        // Lista los patios de acuerdo al RFC que se recibe por el parametro.
        [HttpGet("ObtenerPatio/{pInfoPatio}")]
        public async Task<IActionResult> ObtenerPatioPorRFC(string pInfoPatio)
        {
            var lstCatPatios = new List<CatPatiosDTO>();

            lstCatPatios = await _db.catProveedoresPatios
                .Include(pp => pp.catProveedores)
                .Include(pp => pp.catPatios)
                .Where(pp => pp.catProveedores.RFC == pInfoPatio ||
                             pp.catProveedores.Acronimo == pInfoPatio ||
                             pp.catProveedores.RazonSocial == pInfoPatio ||
                             pp.catPatios.RazonSocial == pInfoPatio &&
                             pp.Activo
                )
                .Select(
                p => new CatPatiosDTO
                {

                    IdCatPatios = p.catPatios.IdCatPatios,
                    RazonSocial = p.catPatios.RazonSocial,
                    IdCatAduana = p.catAduana.IdCatAduana,
                    IdCatProveedor = p.catProveedores.IdCatProveedor,
                    RazonSocialProveedor = p.catProveedores.RazonSocial,
                    Acronimo = p.catProveedores.Acronimo
                }
            ).ToListAsync();

            return lstCatPatios.Any() ? Ok(lstCatPatios) : NotFound("No se encontraron patios con los filtros proporcionados.");
        }
    }
}
