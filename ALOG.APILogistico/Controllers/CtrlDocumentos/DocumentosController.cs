using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.RutasArchivos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NPOI.HPSF;

namespace ALOG.APILogistico.Controllers.CtrlDocumentos
{
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class DocumentosController : Controller
    {
        //private readonly string _basePath = @"C:\alogistics";
        //private readonly string _basePath = @"C:\Alogistic";
        private readonly string _basePath;
        private readonly ApplicationDbContext _context;
        private string strPathCompleto = "";
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();
        public DocumentosController(ApplicationDbContext context, IOptions<RutasArchivosRepositorio> opciones)
        {
            _context = context;

            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.strMensaje = "Error al ejecutar el procedimiento";
            _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            _basePath = opciones.Value.PathBaseExpediente;
        }

        [HttpPost("subirArchivo")]
        public async Task<IActionResult> UploadFile([FromForm] SolCargarArchivoDTO solCargarArchivoDTO)
        {

            string resultado = "";
            string filePath;

            if (solCargarArchivoDTO.File == null || solCargarArchivoDTO.File.Length == 0)
            {
                return BadRequest("No se ha seleccionado ningún archivo.");
            }

            try
            {
                // Definir los parámetros del procedimiento almacenado
                var pIdOrden = new SqlParameter("@pIdOrden", solCargarArchivoDTO.IdOrden);
                var pIdContenedor = new SqlParameter("@pIdContenedor", solCargarArchivoDTO.IdContenedor);
                var pIdServicio = new SqlParameter("@pIdServicio", solCargarArchivoDTO.IdServicio);
                var pIdCatDocumento = new SqlParameter("@pIdCatDocumento", solCargarArchivoDTO.IdCatDocumento);
                var pIdLineaNegocio = new SqlParameter("@pIdLineaNegocio", solCargarArchivoDTO.IdCatLineaNegocio);
                var paramOutput = new SqlParameter
                {
                    ParameterName = "@PathDocumento",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 255,
                    Direction = System.Data.ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado
                await _context.Database
               .ExecuteSqlRawAsync("EXEC dbo.ObtenerPathExpediente @pIdOrden, @pIdContenedor, @pIdServicio, @pIdCatDocumento,@pIdLineaNegocio, @PathDocumento OUTPUT",
               pIdOrden, pIdContenedor, pIdServicio, pIdCatDocumento, pIdLineaNegocio, paramOutput);
                // Obtener el valor del parámetro de salida
                resultado = paramOutput.Value.ToString();
                strPathCompleto += _basePath + resultado;


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al ejecutar el procedimiento: {ex.Message}");
            }


            // Verificar si el directorio existe, si no, crearlo
            if (!Directory.Exists(strPathCompleto))
            {
                Directory.CreateDirectory(strPathCompleto);
            }
            var fileType = Path.GetExtension(solCargarArchivoDTO.File.FileName);
            var contentType = solCargarArchivoDTO.File.ContentType;
            
            try
            {
                var uuid = Guid.NewGuid().ToString();
                var nombreDocumentoUUID = uuid + Path.GetExtension(solCargarArchivoDTO.File.FileName);

                // Definir los parámetros del procedimiento almacenado
                var documentoUUIDParam = new SqlParameter("@documentoUUID", uuid);
                var Ubicacion = new SqlParameter("@ubicacion", resultado + "\\" + nombreDocumentoUUID);
                var IdTipoDocumento = new SqlParameter("@idTipoDocumento", solCargarArchivoDTO.IdCatDocumento);
                var mimeType = new SqlParameter("@mimeType", contentType);
                var nombreDocumento = new SqlParameter("@nombreDocumento", nombreDocumentoUUID);
                var IdContenedor = new SqlParameter("@idContenedor", solCargarArchivoDTO.IdContenedor);
                var IdUsuarioRegistro = new SqlParameter("@idUsuarioRegistro", 1);
                var IdServicio = new SqlParameter("@idServicio", solCargarArchivoDTO.IdServicio);

                filePath = Path.Combine(strPathCompleto, nombreDocumentoUUID);

                // Ejecutar el procedimiento almacenado
                await _context.Database
               .ExecuteSqlRawAsync("EXEC Bot.GuardarRelacionDocumentoContenedor @documentoUUID, @ubicacion, @idTipoDocumento, @mimeType, @nombreDocumento,@idContenedor, @idUsuarioRegistro,@idServicio ",
               documentoUUIDParam, Ubicacion, IdTipoDocumento, mimeType, nombreDocumento, IdContenedor, IdUsuarioRegistro, IdServicio);
            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.strMensaje = $"Error al ejecutar el procedimiento Registro Bot.GuardarRelacionDocumentoContenedor: {ex.Message}";
                return StatusCode(500, _respuestaGenericaDTO);
            }

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await solCargarArchivoDTO.File.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.strMensaje = $"Error al guarda documento: {ex.Message}";
                return StatusCode(500, _respuestaGenericaDTO);
            }

            return Ok(true);

        }

        [HttpDelete("eliminarArchivo/{pUUID}")]
        public async Task<IActionResult> DeleteFile(string pUUID)
        {
            /*  var filePath = Path.Combine(_basePath, pUUID);
              if (!System.IO.File.Exists(filePath))
              {
                  return NotFound();
              }*/
            try
            {
                var documento = _context.peticionesDocumentos.FirstOrDefaultAsync(x => x.DocumentoUUID.Equals(pUUID));
                if (documento == null)
                {
                    _respuestaGenericaDTO.IsSuccess = false;
                    _respuestaGenericaDTO.strMensaje = $"Error en la solicitud UUID no encontrado";
                    _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_respuestaGenericaDTO);
                }
                else
                {
                    documento.Result.Activo = false;
                    _context.peticionesDocumentos.Update(documento.Result);
                    await _context.SaveChangesAsync();

                    _respuestaGenericaDTO.IsSuccess = true;
                    _respuestaGenericaDTO.strMensaje = "Documento eliminado correctamente";
                    _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(_respuestaGenericaDTO);
                }
            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.IsSuccess = false;
                _respuestaGenericaDTO.strMensaje = $"Error en la solicitud {ex.Message}";
                _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(500, _respuestaGenericaDTO);
            }



        }

        [HttpGet("obtenerArchivo/{pUUID}")]
        public async Task<IActionResult> GetFileContent(string pUUID)
        {
            var objPeticionDocumento = _context.peticionesDocumentos.FirstOrDefault(x => x.DocumentoUUID == pUUID);
            if (objPeticionDocumento == null)
            {
                _respuestaGenericaDTO.IsSuccess = false;
                _respuestaGenericaDTO.strMensaje = $"Error en la solicitud UUID no encontrado";
                _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_respuestaGenericaDTO);
            }
            var filePath = Path.Combine(_basePath, objPeticionDocumento.Ubicacion);
            if (!System.IO.File.Exists(filePath))
            {
                _respuestaGenericaDTO.IsSuccess = false;
                _respuestaGenericaDTO.strMensaje = $"No se encuentra el archivo:" + filePath;
                _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_respuestaGenericaDTO);

            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = "application/octet-stream";
            return File(fileBytes, contentType, objPeticionDocumento.NombreDocumento);
        }
    }
}
