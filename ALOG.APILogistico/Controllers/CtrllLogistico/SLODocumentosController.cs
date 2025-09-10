using ALOG.Modelos.Modelos.DTO.Logistica;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.RutasArchivos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol;

namespace ALOG.APILogistico.Controllers.CtrllLogistico
{
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    [Route("[controller]")]
    [ApiController]
    public class SLODocumentosController : Controller
    {
        private readonly string _basePath;
        private readonly ApplicationDbContext _context;
        private string strPathCompleto = "";
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();

        public SLODocumentosController(ApplicationDbContext context, IOptions<RutasArchivosRepositorio> opciones)
        {
            _context = context;

            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.strMensaje = "Error al ejecutar el procedimiento";
            _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            _basePath = opciones.Value.PathBaseExpediente;
        }

        #region GUARDAR ARCHIVO

        [HttpPost("subirArchivo")]
        public async Task<IActionResult> UploadFile([FromForm] SLODocumentoDTO sloDocumentoDTO)
        {
            if (sloDocumentoDTO.File == null || sloDocumentoDTO.File.Length == 0)
            {
                return BadRequest(new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    strMensaje = "No se ha seleccionado ningún archivo.",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            }

            try
            {
                // 1. Generar UUID y nombre físico
                var uuid = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(sloDocumentoDTO.File.FileName);
                var nombreDocumentoUUID = $"{uuid}{extension}";

                // 2. Buscar transporte asignado
                var transporte = await _context.sLOTransporteAsignados
                    .FirstOrDefaultAsync(t => t.Placas == sloDocumentoDTO.Identificador);

                if (transporte == null)
                {
                    return NotFound(new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = "No se encontró el transporte asignado para el servicio.",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    });
                }

                // 3. Obtener IdCatDocumento dinámicamente
                var catDocumento = await _context.catDocumento
                    .Where(d => d.Acronimo == sloDocumentoDTO.TipoDocumento)
                    .FirstOrDefaultAsync();

                if (catDocumento == null)
                {
                    return BadRequest(new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = $"No se encontró un documento con el acrónimo {sloDocumentoDTO.TipoDocumento}.",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    });
                }

                // 4. Definir ruta de guardado
                var fecha = DateTime.Now;
                string acronimoLineaNegocio = "SLO"; // fijo
                string identificador = "";
                string strPathCompleto = "";

                if (sloDocumentoDTO.TipoDocumento == "CARTAPORTE")
                {
                    identificador = Path.Combine(
                        "TERRESTRE",
                        sloDocumentoDTO.Identificador
                    );

                    strPathCompleto = Path.Combine(
                        _basePath,
                        "EXPEDIENTE",
                        fecha.Year.ToString(),
                        fecha.Month.ToString("D2"),
                        fecha.Day.ToString("D2"),
                        acronimoLineaNegocio,
                        sloDocumentoDTO.IdOrden.ToString(),
                        identificador
                    );

                    if (!Directory.Exists(strPathCompleto))
                        Directory.CreateDirectory(strPathCompleto);
                }
                else if (sloDocumentoDTO.TipoDocumento == "POD")
                {
                    
                    var transporteFolder = Path.Combine(
                        _basePath,
                        "EXPEDIENTE",
                        fecha.Year.ToString(),
                        fecha.Month.ToString("D2"),
                        fecha.Day.ToString("D2"),
                        acronimoLineaNegocio,
                        sloDocumentoDTO.IdOrden.ToString(),
                        "TERRESTRE",
                        sloDocumentoDTO.Identificador
                    );

                    if (!Directory.Exists(transporteFolder))
                    {
                        return NotFound(new RespuestaGenericaDTO
                        {
                            IsSuccess = false,
                            strMensaje = "No se encontró la ruta del transporte asignado para guardar el POD.",
                            StatusCode = System.Net.HttpStatusCode.NotFound
                        });
                    }

                    identificador = Path.Combine(
                        "TERRESTRE",
                        sloDocumentoDTO.Identificador,
                        "POD"
                    );

                    strPathCompleto = Path.Combine(
                        _basePath,
                        "EXPEDIENTE",
                        fecha.Year.ToString(),
                        fecha.Month.ToString("D2"),
                        fecha.Day.ToString("D2"),
                        acronimoLineaNegocio,
                        sloDocumentoDTO.IdOrden.ToString(),
                        identificador
                    );

                    if (!Directory.Exists(strPathCompleto))
                        Directory.CreateDirectory(strPathCompleto);
                }
                else if (sloDocumentoDTO.TipoDocumento == "INCIDENCIA")
                {
                    // Ruta base del transporte asignado
                    var transporteFolder = Path.Combine(
                        _basePath,
                        "EXPEDIENTE",
                        fecha.Year.ToString(),
                        fecha.Month.ToString("D2"),
                        fecha.Day.ToString("D2"),
                        acronimoLineaNegocio,
                        sloDocumentoDTO.IdOrden.ToString(),
                        "TERRESTRE",
                        sloDocumentoDTO.Identificador
                    );

                    if (!Directory.Exists(transporteFolder))
                    {
                        //return NotFound(new RespuestaGenericaDTO
                        //{
                        //    IsSuccess = false,
                        //    strMensaje = "No se encontró la ruta del transporte asignado para guardar la incidencia.",
                        //    StatusCode = System.Net.HttpStatusCode.NotFound
                        //});
                        Directory.CreateDirectory(transporteFolder);
                    }

                    identificador = Path.Combine(
                        "TERRESTRE",
                        sloDocumentoDTO.Identificador,
                        "INCIDENCIA"
                    );

                    strPathCompleto = Path.Combine(
                        _basePath,
                        "EXPEDIENTE",
                        fecha.Year.ToString(),
                        fecha.Month.ToString("D2"),
                        fecha.Day.ToString("D2"),
                        acronimoLineaNegocio,
                        sloDocumentoDTO.IdOrden.ToString(),
                        identificador
                    );

                    if (!Directory.Exists(strPathCompleto))
                        Directory.CreateDirectory(strPathCompleto);
                }
                else
                {
                    return BadRequest(new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = "Tipo de documento no soportado.",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    });
                }

                // 5. Ruta completa del archivo
                var filePath = Path.Combine(strPathCompleto, nombreDocumentoUUID);

                // 6. Guardar archivo en disco
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await sloDocumentoDTO.File.CopyToAsync(stream);
                }

                // 7. Registrar en BD
                var documento = new SLOSolicitudesDocumentos
                {
                    DocumentoUUID = uuid,
                    NombreDocumento = sloDocumentoDTO.File.FileName,
                    Ubicacion = filePath,
                    TipoArchivo = sloDocumentoDTO.File.ContentType,
                    IdCatUsuarios = sloDocumentoDTO.IdUsuario,
                    IdCatDocumento = catDocumento.IdCatDocumento,
                    IdSLOTransporteDetalle = transporte.IdSLOTransporteDetalle,
                    IdSLOTransporteSolicitud = transporte.IdSLOTransporteSolicitud,
                    IdSLOSolicitud = _context.sloSolicitudes
                                        .Where(s => s.IdOrden == sloDocumentoDTO.IdOrden)
                                        .Select(s => s.IdSLOSolicitud)
                                        .FirstOrDefault(),
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };

                _context.sloSolicitudesDocumentos.Add(documento);
                await _context.SaveChangesAsync();

                // 8. Respuesta
                return Ok(new RespuestaGenericaDTO
                {
                    IsSuccess = true,
                    strMensaje = "Archivo subido y registrado correctamente.",
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Entidad = new
                    {
                        documento.DocumentoUUID,
                        documento.NombreDocumento,
                        documento.TipoArchivo,
                        documento.Ubicacion
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    strMensaje = $"Error al subir documento: {ex.InnerException?.Message ?? ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                });
            }
        }


        #endregion


        #region LISTAR ARCHIVOS

        //[HttpGet("listarArchivo/{idSLOTransporteSolicitud}")]
        //public async Task<IActionResult> ListFiles(int idSLOTransporteSolicitud)
        //{
        //    var respuesta = new RespuestaGenericaDTO 
        //    {
        //        IsSuccess = false,
        //        StatusCode = System.Net.HttpStatusCode.BadRequest
        //    };

        //    try
        //    {
        //        //// 1. Obtener transporte asignado (con la navegación cargada opcionalmente)
        //        //var transporte = await _context.sLOTransporteAsignados
        //        //    .Include(t => t.sloTransporteSolicitud)// <-- CORRECCIÓN
        //        //    .FirstOrDefaultAsync(t => t.Placas == placa);

        //        if (idSLOTransporteSolicitud <= 0)
        //        {
        //            respuesta.strMensaje = "No se proporcionó el número de la solicitud de transporte.";
        //            return StatusCode((int)respuesta.StatusCode, respuesta);
        //        }

        //        // 2. Obtener los documentos que pertenecen a esa solicitud de transporte
        //        var documentos = await _context.sloSolicitudesDocumentos
        //                                       .Include(s => s.catDocumentos)
        //                                       .Where(d => d.IdSLOTransporteSolicitud == idSLOTransporteSolicitud
        //                                           && d.Activo).ToListAsync();
        //        if (!documentos.Any())
        //        {
        //            respuesta.strMensaje = "No se encontrarón documentos para esta solicitud de transporte.";
        //            respuesta.StatusCode = System.Net.HttpStatusCode.NotFound;
        //            return StatusCode((int)respuesta.StatusCode, respuesta);
        //        }


        //        respuesta.strMensaje = "No se encontrarón documentos para esta solicitud de transporte.";
        //        respuesta.StatusCode = System.Net.HttpStatusCode.OK;
        //        respuesta.Entidad = documentos;

        //        return StatusCode((int)respuesta.StatusCode, respuesta);

        //        //return Ok(new RespuestaGenericaDTO
        //        //{
        //        //    IsSuccess = true,
        //        //    //strMensaje = "No se encontrarón documentos para esta solicitud de transporte.",
        //        //    StatusCode = System.Net.HttpStatusCode.OK,
        //        //    Entidades.Add(documentos)
        //        //});

        //        //return Ok(documentos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new RespuestaGenericaDTO
        //        {
        //            IsSuccess = false,
        //            strMensaje = $"Error al listar documentos: {ex.InnerException?.Message ?? ex.Message}",
        //            StatusCode = System.Net.HttpStatusCode.BadRequest
        //        });
        //    }
        //}

        [HttpGet("listarArchivo/{idSLOTransporteSolicitud:int}")]
        public async Task<IActionResult> ListFiles(int idSLOTransporteSolicitud)
        {
            if (idSLOTransporteSolicitud <= 0)
            {
                return BadRequest(new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    strMensaje = "No se proporcionó un IdSLOTransporteSolicitud válido."
                });
            }

            var documentos = await _context.sloSolicitudesDocumentos
                .Include(s => s.catDocumentos)
                .Where(d => d.IdSLOTransporteSolicitud == idSLOTransporteSolicitud && d.Activo)
                .ToListAsync();

            if (documentos.Count == 0)
            {
                return NotFound(new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    strMensaje = "No se encontraron documentos para esta solicitud de transporte."
                });
            }

            return Ok(new RespuestaGenericaDTO
            {
                IsSuccess = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                strMensaje = "Documentos obtenidos correctamente.",
                Entidad = documentos
            });
        }

        #endregion

        #region OBTENER ARCHIVO

        [HttpGet("obtenerArchivo/{pUUID}")]
        public async Task<IActionResult> GetFileContent(string pUUID)
        {
            var objPeticionDocumento = _context.sloSolicitudesDocumentos.FirstOrDefault(x => x.DocumentoUUID == pUUID);
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
            //var contentType = "application/octet-stream";
            var contentType = "application/pdf";
            return File(fileBytes, contentType, objPeticionDocumento.NombreDocumento);
        }

        #endregion

        #region BAJA ARCHIVO

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
                var documento = _context.sloSolicitudesDocumentos.FirstOrDefaultAsync(x => x.DocumentoUUID.Equals(pUUID));
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
                    _context.sloSolicitudesDocumentos.Update(documento.Result);
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

        #endregion
    }
}
