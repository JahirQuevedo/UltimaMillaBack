using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
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
    public class CatTipoOperacionesTransportesController : GenericoController<CatTipoOperacionesTransportes>
    {
        private readonly IGenericoRepositorio<CatTipoOperacionesTransportes> _repositorio;
        private readonly ApplicationDbContext _context;

        public CatTipoOperacionesTransportesController(ApplicationDbContext context, IGenericoRepositorio<CatTipoOperacionesTransportes> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _context = context;
        }

        [HttpGet("ListarCatTipoOperacionesTransportes")]
        public async Task<IActionResult> ListarCatTipoOperacionesTransportes()
        {
            RespuestaGenericaDTO respuestaGenericaDTO = new RespuestaGenericaDTO();

            try
            {
                var lstCatTipoOperacionesTransportes = await _context.catTipoOperacionesTransportes.Include(t => t.catTipoOperacionesSLO)
                    .Include(t => t.catTipoTransporte).ToListAsync();

                respuestaGenericaDTO.IsSuccess = true;
                respuestaGenericaDTO.strMensaje = "Lista obtenida correctamente";
                respuestaGenericaDTO.Entidades = lstCatTipoOperacionesTransportes.Cast<object>().ToList();
                return Ok(respuestaGenericaDTO);
            }
            catch (Exception ex)
            {
                respuestaGenericaDTO.IsSuccess = false;
                respuestaGenericaDTO.lstrErrorMessages.Add(ex.Message);
                return Ok(respuestaGenericaDTO);
            }
        }
    }
}
