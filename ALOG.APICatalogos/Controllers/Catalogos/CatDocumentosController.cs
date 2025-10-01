using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
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
    public class CatDocumentosController : GenericoController<CatDocumentos>
    {
        private readonly IGenericoRepositorio<CatDocumentos> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatDocumentosController(ApplicationDbContext db, IGenericoRepositorio<CatDocumentos> repositorio) : base(repositorio)
        {
            _db = db;
        }

        [HttpGet("ListarDocumentosLineaNegocio/{idLineaNegocio}")]
        public async Task<IActionResult> ListarDocumentosLineaNegocio(int idLineaNegocio)
        {
            RespuestaGenericaDTO respuestaGenericaDTO = new RespuestaGenericaDTO();
            respuestaGenericaDTO.IsSuccess = false;
            try
            {
                List<CatDocumentos> documentos = new List<CatDocumentos>();

                documentos = await _db.catDocumentosLNegocios
                    .Where(ln => ln.IdCatLineaNegocio == idLineaNegocio)
                    .Select(ln => ln.CatDocumentos)
                    .ToListAsync();

                respuestaGenericaDTO.Entidades = documentos.Cast<object>().ToList();
                respuestaGenericaDTO.IsSuccess = true;
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(respuestaGenericaDTO);
            }
            catch (Exception ex)
            {
                respuestaGenericaDTO.IsSuccess = false;
                respuestaGenericaDTO.strMensaje = $"Error al obtener los documentos: {ex.InnerException}";
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                return StatusCode(500, respuestaGenericaDTO);
            }
        }
    }
}
