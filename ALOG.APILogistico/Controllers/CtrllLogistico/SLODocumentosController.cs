using ALOG.Modelos.Modelos.DTO.Consultas;
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

            RespuestaGenericaDTO respuestaGenericaDTO = new RespuestaGenericaDTO();
            SLOTransporteAsignado transporte = new SLOTransporteAsignado();

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
                int idSLOTransporteSolicitud = int.TryParse(sloDocumentoDTO.Identificador, out int n) ? n : 0;
                if (idSLOTransporteSolicitud <= 0)
                {
                    respuestaGenericaDTO.IsSuccess = false;
                    respuestaGenericaDTO.lstrErrorMessages.Add("No se pudo obtener la solicitud del transportista.");
                    respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }else if(idSLOTransporteSolicitud > 0)
                {
                    transporte = await _context.sLOTransporteAsignados
                        .FirstOrDefaultAsync(t => t.IdSLOTransporteSolicitud == idSLOTransporteSolicitud);


                    if (transporte.IdSLOTransporteAsignado <= 0)
                    {
                        respuestaGenericaDTO.IsSuccess = false;
                        respuestaGenericaDTO.lstrErrorMessages.Add("No se encontró el transporte asignado para el servicio.");
                        respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.NotFound;
                    }
                }                    

                // 3. Obtener IdCatDocumento dinámicamente
                var catDocumento = await _context.catDocumento
                    .Where(d => d.Acronimo == sloDocumentoDTO.TipoDocumento)
                    .FirstOrDefaultAsync();

                if (catDocumento == null)
                {

                    respuestaGenericaDTO.IsSuccess = false;
                    respuestaGenericaDTO.lstrErrorMessages.Add($"No se encontró un documento con el acrónimo {sloDocumentoDTO.TipoDocumento}.");                        
                    
                }

                // 4. Definir ruta de guardado
                var fecha = DateTime.Now;
                string acronimoLineaNegocio = "SLO"; // fijo
                string identificador = "";
                string strPathCompleto = "";

                if (sloDocumentoDTO.TipoDocumento == "CARTAPORTE" && transporte.IdSLOTransporteAsignado > 0)
                {
                    identificador = Path.Combine(
                        "TERRESTRE",
                        transporte.Placas
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
                else if (sloDocumentoDTO.TipoDocumento == "POD" && transporte.IdSLOTransporteAsignado > 0)
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
                        transporte.Placas
                    );

                    if (!Directory.Exists(transporteFolder))
                    {
                        //return NotFound(new RespuestaGenericaDTO
                        //{
                        //    IsSuccess = false,
                        //    strMensaje = "No se encontró la ruta del transporte asignado para guardar el POD.",
                        //    StatusCode = System.Net.HttpStatusCode.NotFound
                        //});
                        Directory.CreateDirectory(transporteFolder);
                    }

                    identificador = Path.Combine(
                        "TERRESTRE",
                        transporte.Placas,
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
                else if (sloDocumentoDTO.TipoDocumento == "INCIDENCIA" && transporte.IdSLOTransporteAsignado > 0)
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
                        transporte.Placas
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
                        transporte.Placas,
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
                    //Si es un documento general
                    strPathCompleto = Path.Combine(
                        _basePath,
                        "EXPEDIENTE",
                        fecha.Year.ToString(),
                        fecha.Month.ToString("D2"),
                        fecha.Day.ToString("D2"),
                        acronimoLineaNegocio,
                        sloDocumentoDTO.IdOrden.ToString()                        
                    );

                    if(!Directory.Exists(strPathCompleto))
                    {
                        Directory.CreateDirectory(strPathCompleto);
                    }
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
                    IdSLOSolicitud = _context.sloSolicitudes
                                        .Where(s => s.IdOrden == sloDocumentoDTO.IdOrden)
                                        .Select(s => s.IdSLOSolicitud)
                                        .FirstOrDefault(),
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };

                if(transporte.IdSLOTransporteAsignado > 0)
                {
                    documento.IdSLOTransporteDetalle = transporte.IdSLOTransporteDetalle;
                    documento.IdSLOTransporteSolicitud = transporte.IdSLOTransporteSolicitud;
                }
                    

                _context.sloSolicitudesDocumentos.Add(documento);
                await _context.SaveChangesAsync();

                // 8. Respuesta
                respuestaGenericaDTO.IsSuccess = true;
                respuestaGenericaDTO.strMensaje = "Archivo subido y registrado correctamente.";
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                respuestaGenericaDTO.Entidad = new
                {
                    documento.DocumentoUUID,
                    documento.NombreDocumento,
                    documento.TipoArchivo,
                    documento.Ubicacion,
                    documento.IdSLOSolicitudDocumentos
                };
                return Ok(respuestaGenericaDTO);
            }
            catch (Exception ex)
            {
                respuestaGenericaDTO.lstrErrorMessages.Add(ex.Message);
                respuestaGenericaDTO.strMensaje = "Error al subir el archivo.";
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                return StatusCode(500, respuestaGenericaDTO);
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

        [HttpPost("listarArchivo")]
        public async Task<IActionResult> ListFiles([FromBody] FiltroGenericoDTO filtroGenericoDTO)
        {
            var IdSolicitud = filtroGenericoDTO.Id;
            var IdTransporteSolicitud = filtroGenericoDTO.IdTipoDocumento;
            if (filtroGenericoDTO.Id <= 0)
            {
                return BadRequest(new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    strMensaje = "No se proporcionó un IdSLOTransporteSolicitud válido."
                });
            }

            //var documentos = await _context.sloSolicitudesDocumentos
            //    .Include(s => s.catDocumentos)
            //    .Where(d => d.IdSLOTransporteSolicitud == filtroGenericoDTO.Id && d.Activo && d.IdSLOTransporteSolicitud != null)
            //    .ToListAsync();

            //var algo = await _context.sloSolicitudesDocumentos
            //        .Include(s => s.sloSolicitudes)
            //            .ThenInclude(s => s.Orden)
            //    .Where(d => d.IdSLOSolicitud == IdSolicitud && (d.IdSLOTransporteSolicitud == IdTransporteSolicitud || d.IdSLOTransporteSolicitud is )).ToListAsync();

            var documentos = await _context.sloSolicitudesDocumentos
                .Include(s => s.catDocumentos)
                .Where(d => d.IdSLOSolicitud == IdSolicitud &&
                        (IdTransporteSolicitud == null
                    ? d.IdSLOTransporteSolicitud == null
                    : d.IdSLOTransporteSolicitud == IdTransporteSolicitud)
                    && d.Activo)
                    .ToListAsync();


            //var documentos = await _context.sloSolicitudesDocumentos
            //    .Include(s => s.catDocumentos)
            //    .Where(d => d.IdSLOTransporteSolicitud == idSLOTransporteSolicitud && d.Activo && d.IdSLOTransporteSolicitud != null)
            //    .ToListAsync();

            //if (documentos.Count == 0)
            //{
            //    return NotFound(new RespuestaGenericaDTO
            //    {
            //        IsSuccess = false,
            //        StatusCode = System.Net.HttpStatusCode.NotFound,
            //        strMensaje = "No se encontraron documentos para esta solicitud de transporte."
            //    });
            //}

            return Ok(new RespuestaGenericaDTO
            {
                IsSuccess = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                strMensaje = "Documentos obtenidos correctamente.",
                Entidad = documentos
            });
        }

        [HttpGet("listarArchivoSolicitud/{idSolicitud}")]
        public async Task<IActionResult> listarArchivoSolicitud(int idSolicitud)
        {
            RespuestaGenericaDTO respuestaGenericaDTO = new RespuestaGenericaDTO();
            if (idSolicitud <= 0)
            {
                respuestaGenericaDTO.IsSuccess = false;
                respuestaGenericaDTO.strMensaje = "No se proporcionó un IdSLOSolicitud valido.";
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(respuestaGenericaDTO);
            }
            else
            {
                var documentos = await _context.sloSolicitudesDocumentos
                    .Include(s => s.catDocumentos)
                    .Where(d => d.IdSLOSolicitud == idSolicitud && d.Activo && d.IdSLOTransporteSolicitud == null)
                    .ToListAsync();

                respuestaGenericaDTO.IsSuccess = true;
                respuestaGenericaDTO.strMensaje = "Documentos obtenidos correctamente.";
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                respuestaGenericaDTO.Entidad = documentos;
                return Ok(respuestaGenericaDTO);
            }
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
