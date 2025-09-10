using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Utilerias;
using ALOG.Modelos.Modelos.Peticiones;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Utilerias.IUtileria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ALOG.APILogistico.Controllers.CtrllUtilerias
{
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class UtileriaController : Controller
    {
        private readonly ApplicationDbContext _db;
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();
        private readonly IUtileriaExportacionExcel _ctRepoExportacionExcel;
        private readonly IGenericoRepositorio<PeticionesContenedoresCron> _genericRepository;

        public UtileriaController(ApplicationDbContext db,
            IUtileriaExportacionExcel ctRepoExportacionExcel)
        {
            _db = db;

            //INICIALIZA RESPUESTA GENERICA
            _respuestaGenericaDTO = new RespuestaGenericaDTO();
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
            _ctRepoExportacionExcel = ctRepoExportacionExcel;
        }

        [HttpPost("GenerarExcelDinamico")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GenerarExcelDinamico([FromBody] List<Dictionary<string, object>> data)
        {

            #region OPERACIONES
            var fileBytes = _ctRepoExportacionExcel.GenerarExcelDinamico(data);

            if (fileBytes == null || fileBytes.Length == 0)
                return BadRequest("No hay datos para exportar.");

            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Reporte.xlsx");
            #endregion OPERACIONES

        }

        [HttpPost("CargarExcelDinamico")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CargarExcel([FromForm] UploadFileDTO data)
        {
            if (data.File == null || data.File.Length == 0)
                return BadRequest("Archivo inválido.");

            var filas = await _ctRepoExportacionExcel.ProcesarExcel(data.File);
            return Ok(filas);
        }
    }
}