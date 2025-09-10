using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Control;
using ALOG.Repositorios.Repositorio.Control.IControl;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using ALOG.Repositorios.Repositorio.RutasArchivos;
using ALOG.Repositorios.Utilerias;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Web;


namespace ApiVacios.Controllers
{
    //[Authorize(Roles = "ADMIN,SISTEMA")]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class VaciosController : ControllerBase
    {
        private readonly IVaciosRepositorio _ctRepovacios;
        private readonly IOrdenesRepositorio _ctRepoOrdenes;
        private readonly ApplicationDbContext _context;
        private readonly string _basePath;
        private string strPathCompleto = string.Empty;

        private readonly IMapper _mapper;
        //private List<string> lstError;
        //private PeticionesRespuesta objRespuesta = new PeticionesRespuesta();

        public VaciosController(ApplicationDbContext context, IVaciosRepositorio ctRepo, IOrdenesRepositorio ctRepoOrdenes, IMapper mapper, IOptions<RutasArchivosRepositorio> opciones)
        {
            _context = context;
            _ctRepovacios = ctRepo;
            _ctRepoOrdenes = ctRepoOrdenes;
            _mapper = mapper;
            _basePath = opciones.Value.PathBaseExpediente;
        }

        //######################################################## ENDPOINTS ESPECIFICOS PARA NAD ##################################################
        #region ENDPOINTS NAD
        [HttpGet("ConsultaReferenciaNAD/{referenciaNAD}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReferenciaNAD(string referenciaNAD)
        {
            // Decodificar el parámetro
            string decodedParameter = HttpUtility.UrlDecode(referenciaNAD);
            var lstServicios = _ctRepovacios.GetReferenciaNAD(decodedParameter);
            var lstServiciosDto = new List<PeticionesReferenciasDTO>();
            if (lstServicios != null)
            {
                foreach (var lista in lstServicios)
                {
                    lstServiciosDto.Add(_mapper.Map<PeticionesReferenciasDTO>(lista)); //se utiliza mapper para 
                }
                return Ok(lstServiciosDto);
            }
            else
                return BadRequest(StatusCodes.Status404NotFound);
        }

        //[HttpPost("CrearReferencia")]
        //[ProducesResponseType(201, Type = typeof(PeticionesReferenciasDTO))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> CrearReferencia([FromBody] PeticionesReferenciasDTO peticionesReferenciasDTO)
        //{
        //    #region Variables
        //    var objOrden = new Ordenes();
        //    var objRespuesta = new PeticionesRespuestaDTO();
        //    var lstError = new List<string>();
        //    var strErrores = new List<string>();
        //    #endregion Variables

        //    #region Asignación de valores por default a Objetos
        //    //Asignación de Valores.

        //    objRespuesta.IsSuccess = true;
        //    objRespuesta.StatusCode = HttpStatusCode.OK;
        //    objRespuesta.ErrorMessages = new List<string>();

        //    peticionesReferenciasDTO.IdCatReferenciaEstado = 7;
        //    peticionesReferenciasDTO.EstadoReferencia = "PE";
        //    peticionesReferenciasDTO.Procesado = "N";
        //    #endregion Asignación de valores por default a Objetos

        //    var referencia = _mapper.Map<PeticionesReferencias>(peticionesReferenciasDTO);

        //    if (ModelState.IsValid)
        //    {
        //        #region UsuarioAcceso
        //        var respuestaTokenDTO = HttpContext.ObtenerUsuarioTokenUnificado();
        //        #endregion UsuarioAcceso

        //        //Nota: Validar documentos vacíos. Todos los documentos deben estar vacíos por contenedor.
        //        //Validar en creación estados de cerrado false.
        //        //Incluir fechas de registro el momento de guardado para todos los niveles.
        //        //Fechas de cierre en nulas para todos los niveles en la creación.
        //        //Consultar Catálogos por RFC para obtenerl el ID de cada llave.
        //        //Validar tipo de servicio existente del catálogo.No puede ser 0.

        //        #region ValidarObjeto            
        //        lstError = _ctRepovacios.ValidaPeticionReferenciaNad(referencia);
        //        if (lstError.Count > 0)
        //        {
        //            objRespuesta.IsSuccess = false;
        //            objRespuesta.ErrorMessages = lstError;
        //            objRespuesta.StatusCode = HttpStatusCode.BadRequest;
        //            return BadRequest(objRespuesta);
        //        }
        //        #endregion ValidarObjeto

        //        //Creamos Orden de Servicio
        //        /*
        //         * Setting de Ordene de Servicio
        //         */
        //        #region Asignación de valores a las propiedades del modelo orden para su creación
        //        objOrden.IdCatCliente = respuestaTokenDTO.IdCatCliente != null ? (int)respuestaTokenDTO.IdCatCliente : 1;
        //        objOrden.IdCatSistema = respuestaTokenDTO.IdCatSistema;
        //        var aduana = referencia.Contenedores.FirstOrDefault()?.AduanaId;
        //        if (aduana != null && aduana > 0)
        //        {
        //            objOrden.IdCatAduana = _ctRepovacios.GetIdAduana(aduana);
        //        }

        //        //objOrden.IdCatEmpresa = respuestaTokenDTO.IdCatEmpresa;
        //        objOrden.IdCatEmpresa = 2;
        //        objOrden.IdCatSucursal = 1; //Veracruz.
        //        objOrden.IdCatLineaNegocio = 1; //Vacios
        //        objOrden.IdEstadoOrden = 5;
        //        objOrden.Activo = true;
        //        objOrden.FechaRegistro = DateTime.Now;
        //        #endregion

        //        //Generar folio de referencia ALO
        //        //var vStrReferenciaALO = _ctRepoOrdenes.ObtenerReferenciaALO(objOrden, 0, 0, objOrden.IdCatCliente).Result;
        //        var vStrReferenciaALO = await _ctRepoOrdenes.ObtenerReferenciaALO(objOrden, 0, 0, objOrden.IdCatCliente);
        //        if (!string.IsNullOrEmpty(vStrReferenciaALO))
        //        {
        //            objOrden.ReferenciaALO = vStrReferenciaALO;

        //            #region Operaciones
        //            //if (_ctRepoOrdenes.CrearOrden(objOrden) != null)
        //            var ordenInsertada = await _ctRepoOrdenes.CrearOrden(objOrden);
        //            if (ordenInsertada != null)
        //            {
        //                //Identificador de Orden de Servicio.
        //                referencia.IdOrden = objOrden.IdOrden;

        //                if (!_ctRepovacios.CrearReferencia(referencia, out strErrores))
        //                {
        //                    objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
        //                    objRespuesta.IsSuccess = false;
        //                    objRespuesta.ErrorMessages = new List<string>();
        //                    objRespuesta.ErrorMessages = strErrores;
        //                    objRespuesta.ErrorMessages.Add($"Error en guardar la referencia: Ticket: {referencia.Ticket}");
        //                    return StatusCode(500, objRespuesta);
        //                    //ModelState.AddModelError("", $"Algo salió mal guardando el registro{referencia.Ticket}");
        //                    //return StatusCode(500, ModelState);
        //                }
        //                else
        //                {
        //                    //Agregar a tablas de integración referencia e integración factura enc y det

        //                    objRespuesta.IsSuccess = true;
        //                    objRespuesta.IdOrdenServicio = objOrden.IdOrden;
        //                    objRespuesta.IdReferenciaALO = referencia.IdReferencia;
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", $"Algo salió mal guardando el registro {referencia.Ticket}");
        //                return StatusCode(500, ModelState);
        //            }

        //            var id = referencia.IdReferencia;
        //            var idOrden = objOrden.IdOrden;

        //            return Ok(objRespuesta);
        //            #endregion Operaciones
        //        }
        //        else
        //        {
        //            objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
        //            objRespuesta.IsSuccess = false;
        //            objRespuesta.ErrorMessages = new List<string>();
        //            objRespuesta.ErrorMessages = strErrores;
        //            objRespuesta.ErrorMessages.Add($"Error al generar la referencia ALO: Ticket: {referencia.Ticket}");
        //            return StatusCode(500, objRespuesta);
        //        }
        //    }
        //    else
        //    {
        //        objRespuesta.IsSuccess = false;
        //        objRespuesta.StatusCode = HttpStatusCode.BadRequest;
        //        objRespuesta.ErrorMessages = new List<string>();
        //        objRespuesta.ErrorMessages.Add("ModelState Invalido");
        //        objRespuesta.ErrorMessages.Add(ModelState.ToString());
        //        return BadRequest(objRespuesta);
        //    }
        //}

        //[HttpPost("CrearContenedor/{IdReferencia:int}")]
        //[ProducesResponseType(201, Type = typeof(PeticionesContenedoresDTO))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult CrearContenedor(int IdReferencia, [FromBody] PeticionesContenedoresDTO peticionesContenedoresDTO)
        //{
        //    #region Variables
        //    var contenedor = _mapper.Map<PeticionesContenedores>(peticionesContenedoresDTO);
        //    var objRespuesta = new PeticionesRespuesta();
        //    #endregion Variables

        //    #region Asignación de valores a variables
        //    objRespuesta.IsSuccess = false;
        //    objRespuesta.StatusCode = HttpStatusCode.BadRequest;

        //    contenedor.FechaRegistro = DateTime.Now;
        //    contenedor.IdReferencia = IdReferencia;
        //    contenedor.FechaCierre = null;
        //    contenedor.PatioId = null;
        //    contenedor.EstadoContenedor = "PE";
        //    contenedor.IdEstadoContenedor = (int)EnumEstados.EstadosReferencias.Pendiente;
        //    contenedor.Activo = true;
        //    #endregion

        //    //Asignación de Valores.
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var lstCrearContenedor = _ctRepovacios.CrearContenedor(IdReferencia, contenedor);
        //            if (lstCrearContenedor.Count > 0)
        //            {
        //                objRespuesta.ErrorMessages.AddRange(lstCrearContenedor);
        //                objRespuesta.ErrorMessages.Add($"Algo salió mal guardando el registro {contenedor.Contenedor}");

        //                return BadRequest(objRespuesta);
        //            }

        //            var id = contenedor.IdContenedor;
        //            objRespuesta.IsSuccess = true;
        //            objRespuesta.StatusCode = HttpStatusCode.OK;
        //            return Ok(objRespuesta);
        //        }
        //        else
        //        {
        //            objRespuesta.ErrorMessages.Add("ModelState Invalido");
        //            objRespuesta.ErrorMessages.Add(ModelState.ToString());
        //            return BadRequest(objRespuesta);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        objRespuesta.ErrorMessages.Add(ex.Message);
        //        return BadRequest(objRespuesta); ;
        //    }
        //}

        //[HttpPost("CrearServicio/{IdReferencia:int}/{IdContenedor:int}")]
        //[ProducesResponseType(201, Type = typeof(PeticionesServiciosDTO))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult CrearServicio(int IdReferencia, int IdContenedor, [FromBody] PeticionesServiciosDTO peticionesServiciosDTO)
        //{
        //    #region Variables
        //    var servicio = _mapper.Map<PeticionesServicios>(peticionesServiciosDTO);

        //    var objRespuesta = new PeticionesRespuesta();
        //    objRespuesta.ErrorMessages = new List<string>();
        //    #endregion Variables

        //    #region Asignación de Valores.
        //    objRespuesta.IsSuccess = false;
        //    objRespuesta.StatusCode = HttpStatusCode.BadRequest;

        //    servicio.IdContenedor = IdContenedor;
        //    #endregion

        //    var lstStrErrores = _ctRepovacios.ValidaServicioContenedor(servicio, IdReferencia);
        //    if (lstStrErrores != null && lstStrErrores.Count > 0)
        //    {
        //        objRespuesta.ErrorMessages.AddRange(lstStrErrores);
        //        return BadRequest(objRespuesta);
        //    }

        //    if (!_ctRepovacios.CrearServicio(IdReferencia, IdContenedor, servicio))
        //    {
        //        //ModelState.AddModelError("", $"Algo salió mal guardando el registro del servicio: {servicio.IdTipoServicio}");
        //        objRespuesta.StatusCode = System.Net.HttpStatusCode.InternalServerError;
        //        objRespuesta.ErrorMessages.Add($"Algo salió mal guardando el registro del servicio: {servicio.IdTipoServicio}");
        //        return StatusCode(500, objRespuesta);
        //    }

        //    objRespuesta.IsSuccess = true;

        //    return Ok(objRespuesta);
        //}
        #endregion ENDPOINTS NAD

        //######################################################## ENDPOINTS DE SERVICIOS EXTERNOS #################################################
        #region ENDPOINTS CLIENTES EXTERNOS
        [HttpPost("CrearOrdenServicio")]
        [ProducesResponseType(201, Type = typeof(PeticionesReferenciasClienteExternoDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearOrdenServicio([FromBody] PeticionesReferenciasClienteExternoDTO peticionesReferenciasClienteExternoDTO)
        {
            var objRespuesta = await _ctRepovacios.CrearOrden(peticionesReferenciasClienteExternoDTO);
            if (objRespuesta != null)
            {
                if (objRespuesta.IsSuccess && objRespuesta.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(objRespuesta);
                }
                return BadRequest(objRespuesta);
            }

            return StatusCode(500, objRespuesta);
        }

        [HttpPost("orden/{IdReferencia:int}/contenedores/nuevo")]
        [ProducesResponseType(201, Type = typeof(PeticionesContenedoresClienteExternoDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarContenedorAReferencia(int IdReferencia, [FromBody] PeticionesContenedoresClienteExternoDTO peticionesContenedoresClienteExternoDTO)
        {
            var objRespuesta = await _ctRepovacios.AgregarContenedorAReferencia(IdReferencia, peticionesContenedoresClienteExternoDTO);
            if (objRespuesta != null)
            {
                if (objRespuesta.IsSuccess && objRespuesta.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(objRespuesta);
                }
                return BadRequest(objRespuesta);
            }

            return StatusCode(500, objRespuesta);
        }

        [HttpPost("orden/{IdReferencia:int}/{IdContenedor:int}/servicios/nuevo")]
        [ProducesResponseType(201, Type = typeof(PeticionesServiciosClienteExternoDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarServicioAContenedor(int IdReferencia, int IdContenedor, [FromBody] PeticionesServiciosClienteExternoDTO peticionesServiciosClienteExternoDTO)
        {
            var objRespuesta = await _ctRepovacios.AgregarServicioAContenedor(IdReferencia, IdContenedor, peticionesServiciosClienteExternoDTO);
            if (objRespuesta.IsSuccess && objRespuesta.StatusCode == HttpStatusCode.OK)
                return Ok(objRespuesta);
            else if (objRespuesta.IsSuccess && objRespuesta.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(objRespuesta);

            return StatusCode(500, objRespuesta);
        }

        #endregion ENDPOINTS CLIENTES EXTERNOS

        //######################################################## METODOS GENERALES ###############################################################
        #region ENDPOINTS GENERALES
        #region GET
        [HttpGet("ConsultaReferenciaALO/{idALO:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReferenciaALO(int idALO)
        {
            #region Variables
            //var lstServiciosDto = new List<PeticionesReferenciasDTO>();
            var resp = new RespuestaGenericaDTO();
            #endregion Variables

            var objPeticionesReferencias = await _ctRepovacios.GetReferencia(idALO);
            if(objPeticionesReferencias == null)
            {
                resp.strMensaje = "No se encontró la referencia proporcionada.";
                resp.StatusCode = HttpStatusCode.NotFound;
                return NotFound(resp);
            }

            resp.IsSuccess = true;
            resp.StatusCode = HttpStatusCode.OK;
            resp.Entidad = objPeticionesReferencias;

            return Ok(resp);

            //// Extraer el claim de nombre de usuario del token JWT (usualmente "sub" o "name")
            //var usuarioId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            //// Si el nombre está en otro claim, puedes cambiar "sub" por el tipo correcto
            //if (string.IsNullOrEmpty(usuarioId))
            //{
            //    usuarioId = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            //}

            //if (!string.IsNullOrEmpty(usuarioId))
            //{
            //    return Ok(new { Usuario = usuarioId });
            //}
            //else
            //{
            //    return BadRequest("No se pudo identificar al usuario.");
            //}

            //if (lstServicios != null)
            //{

            //    //foreach (var lista in lstServicios)
            //    //{
            //    lstServiciosDto.Add(_mapper.Map<PeticionesReferenciasDTO>(lstServicios)); //se utiliza mapper para 
            //    return Ok(lstServiciosDto);                                                                //}
            //}
            //else
            //    return StatusCode(StatusCodes.Status404NotFound);

        }
        #endregion GET

        [HttpPost("subirArchivo")]
        public async Task<IActionResult> UploadFile([FromForm] SolCargarArchivoDTO solCargarArchivoDTO)
        {

            string resultado = string.Empty;
            string filePath = string.Empty;
            strPathCompleto = string.Empty;
            var _respuestaGenericaDTO = new RespuestaGenericaDTO();
            
            #region validaciones
            if (solCargarArchivoDTO.IdOrden <= 0)
                _respuestaGenericaDTO.lstrErrorMessages.Add("Favor de indicar el número de identificación de la orden.");
            if (solCargarArchivoDTO.IdReferencia <= 0)
                _respuestaGenericaDTO.lstrErrorMessages.Add("Favor de indicar el número de identificación de la referencia.");
            if (solCargarArchivoDTO.IdContenedor <= 0)
                _respuestaGenericaDTO.lstrErrorMessages.Add("Favor de indicar el número de identificación del contenedor al que quiere cargar el documento.");
            if (solCargarArchivoDTO.IdServicio <= 0)
                _respuestaGenericaDTO.lstrErrorMessages.Add("Favor de indicar el número de identificación del servicio al cual requiere cargar el documento.");
            if (solCargarArchivoDTO.IdCatDocumento <= 0)
                _respuestaGenericaDTO.lstrErrorMessages.Add("Favor de indicar el número de identificación del documento que subirá.");
            if (solCargarArchivoDTO.IdOrden <= 0)
                _respuestaGenericaDTO.lstrErrorMessages.Add("Favor de indicar el número de identificación de la linea de negocio.");
            if (solCargarArchivoDTO.File == null || solCargarArchivoDTO.File.Length == 0)
                _respuestaGenericaDTO.lstrErrorMessages.Add("No se ha envíado ningún archivo.");
            if (_respuestaGenericaDTO.lstrErrorMessages.Count > 0 && _respuestaGenericaDTO.lstrErrorMessages.Any())
            {
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaGenericaDTO);
            }
            #endregion validaciones

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

        #endregion ENDPOINTS GENERALES

        [HttpGet("ObtenerArchivos/{IdUUID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFileByUuid(string IdUUID)
        {
            // Simulación: Asumiendo que el archivo está en una carpeta específica y el nombre es derivado del UUID
            /*var filePath = Path.Combine("ruta/a/tus/archivos", $"{uuid}.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Devuelve 404 si el archivo no existe
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            // Determinar el tipo MIME del archivo (en este caso, PDF)
            string mimeType = "application/pdf";

            // Devuelve el archivo al cliente
            return File(memory, mimeType, Path.GetFileName(filePath));*/
            return NotFound(false);
        }

        #region METODOS COMENTADOS

        //#region BAJA
        //[HttpDelete("BajaReferencia/{IdReferencia:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult BajaReferencia(int IdReferencia)
        //{
        //    if (!_ctRepovacios.ExisteReferenciaActiva(IdReferencia))
        //    {
        //        return NotFound();
        //    }

        //    var referencia = _ctRepovacios.GetReferencia(IdReferencia);
        //    var resultado = _ctRepovacios.BorrarActivarReferencia(IdReferencia, false);
        //    if (!resultado || referencia == null)
        //    {
        //        ModelState.AddModelError("", $"Algo salió mal borrando el registro{IdReferencia}");
        //        return StatusCode(500, ModelState);
        //    }
        //    return Ok(true);
        //}

        //[HttpDelete("BajaContenedor/{IdReferencia:int}/{IdContenedor:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult BajaContenedor(int IdReferencia, int IdContenedor)
        //{
        //    #region Variables

        //    PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
        //    objRespuesta.IsSuccess = true;
        //    objRespuesta.StatusCode = HttpStatusCode.OK;
        //    objRespuesta.ErrorMessages = new List<string>();
        //    #endregion Variables

        //    if (!_ctRepovacios.ExisteReferenciaActiva(IdReferencia) || (!_ctRepovacios.ExisteContenedorId(IdReferencia, IdContenedor)))
        //    {
        //        return NotFound();
        //    }

        //    var referencia = _ctRepovacios.GetContenedor(IdReferencia, IdContenedor);
        //    var resultado = _ctRepovacios.BorrarActivarContenedor(IdReferencia, IdContenedor, false);
        //    if (!resultado || referencia == null)
        //    {
        //        objRespuesta.StatusCode = HttpStatusCode.NotFound;
        //        objRespuesta.IsSuccess = false;
        //        objRespuesta.ErrorMessages = new List<string>();
        //        objRespuesta.ErrorMessages.Add($"Error en Baja de Contenedor: IdReferencia: {IdReferencia} IdContenedor: {IdContenedor}");
        //        return StatusCode(500, objRespuesta);

        //    }

        //    return Ok(objRespuesta);
        //}

        //[HttpDelete("BajaServicio/{IdReferencia:int}/{IdContenedor:int}/{IdServicio:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult BajaServicio(int IdReferencia, int IdContenedor, int IdServicio)
        //{

        //    #region Variables
        //    //PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO { StatusCode= HttpStatusCode.InternalServerError};
        //    PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
        //    objRespuesta.IsSuccess = true;
        //    objRespuesta.StatusCode = HttpStatusCode.OK;
        //    objRespuesta.ErrorMessages = new List<string>();
        //    #endregion Variables

        //    if (!_ctRepovacios.ExisteReferenciaActiva(IdReferencia) || !_ctRepovacios.ExisteContenedorId(IdReferencia, IdContenedor))
        //    {
        //        objRespuesta.IsSuccess = false;
        //        return NotFound(objRespuesta);
        //    }

        //    var contenedor = _ctRepovacios.GetServicioContenedor(IdReferencia, IdContenedor, IdServicio);
        //    var resultado = _ctRepovacios.BorrarActivarServicioContenedor(IdReferencia, IdContenedor, IdServicio, false);
        //    if (!resultado || contenedor == null)
        //    {
        //        objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
        //        objRespuesta.IsSuccess = false;
        //        objRespuesta.ErrorMessages = new List<string>();
        //        objRespuesta.ErrorMessages.Add($"Error en Baja de Servicio: IdReferencia: {IdReferencia} IdContenedor: {IdContenedor} IdSevicio: {IdServicio}");
        //        return StatusCode(500, objRespuesta);
        //    }
        //    return Ok(objRespuesta);
        //}
        //#endregion BAJA

        //#region ACTIVAR
        //[HttpPatch("ActivarReferencia/{IdReferencia:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult ActivarReferencia(int IdReferencia)
        //{

        //    #region Variables

        //    PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
        //    objRespuesta.IsSuccess = true;
        //    objRespuesta.StatusCode = HttpStatusCode.OK;
        //    objRespuesta.ErrorMessages = new List<string>();
        //    #endregion Variables

        //    if (!_ctRepovacios.ExisteReferenciaActiva(IdReferencia))
        //    {
        //        return NotFound();
        //    }

        //    var referencia = _ctRepovacios.GetReferencia(IdReferencia);
        //    var resultado = _ctRepovacios.BorrarActivarReferencia(IdReferencia, true);
        //    if (!resultado || referencia == null)
        //    {
        //        objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
        //        objRespuesta.IsSuccess = false;
        //        objRespuesta.ErrorMessages = new List<string>();
        //        objRespuesta.ErrorMessages.Add($"Error en Activar Referencia: IdReferencia: {IdReferencia}");
        //        return StatusCode(500, objRespuesta);
        //    }
        //    return Ok(objRespuesta);
        //}

        //[HttpPatch("ActivarContenedor/{IdReferencia:int}/{IdContenedor:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult ActivarContenedor(int IdReferencia, int IdContenedor)
        //{
        //    #region Variables
        //    PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
        //    objRespuesta.IsSuccess = true;
        //    objRespuesta.StatusCode = HttpStatusCode.OK;
        //    objRespuesta.ErrorMessages = new List<string>();
        //    #endregion Variables

        //    if (!_ctRepovacios.ExisteReferenciaActiva(IdReferencia) || (!_ctRepovacios.ExisteContenedorId(IdReferencia, IdContenedor)))
        //    {

        //        return NotFound();
        //    }

        //    var referencia = _ctRepovacios.GetContenedor(IdReferencia, IdContenedor);
        //    var resultado = _ctRepovacios.BorrarActivarContenedor(IdReferencia, IdContenedor, true);
        //    if (!resultado || referencia == null)
        //    {
        //        objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
        //        objRespuesta.IsSuccess = false;
        //        objRespuesta.ErrorMessages = new List<string>();
        //        objRespuesta.ErrorMessages.Add($"Error en Activar Contenedor: IdReferencia: {IdReferencia} IdContenedor: {IdContenedor}");
        //        return StatusCode(500, objRespuesta);
        //    }
        //    return Ok(objRespuesta);
        //}

        //[HttpPatch("ActivarServicio/{IdReferencia:int}/{IdContenedor:int}/{IdServicio:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult ActivarServicio(int IdReferencia, int IdContenedor, int IdServicio)
        //{
        //    #region Variables

        //    PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
        //    objRespuesta.IsSuccess = true;
        //    objRespuesta.StatusCode = HttpStatusCode.OK;
        //    objRespuesta.ErrorMessages = new List<string>();
        //    #endregion Variables

        //    if (!_ctRepovacios.ExisteReferenciaActiva(IdReferencia) || !_ctRepovacios.ExisteContenedorId(IdReferencia, IdContenedor))
        //    {
        //        return NotFound();
        //    }

        //    var contenedor = _ctRepovacios.GetServicioContenedor(IdReferencia, IdContenedor, IdServicio);
        //    var resultado = _ctRepovacios.BorrarActivarServicioContenedor(IdReferencia, IdContenedor, IdServicio, true);
        //    if (!resultado || contenedor == null)
        //    {
        //        objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
        //        objRespuesta.IsSuccess = false;
        //        objRespuesta.ErrorMessages = new List<string>();
        //        objRespuesta.ErrorMessages.Add($"Error en Activar Servicio: IdReferencia: {IdReferencia} IdContenedor: {IdContenedor} IdServicio {IdServicio}");
        //        return StatusCode(500, objRespuesta);
        //    }
        //    return Ok(objRespuesta);
        //}
        //#endregion ACTIVAR

        //private RespuestaTokenDTO ObtenerSistemaToken()
        //{
        //    #region Variables
        //    RespuestaTokenDTO respuestaTokenDTO = new RespuestaTokenDTO();
        //    respuestaTokenDTO.RolesUsuario = new List<string>();
        //    #endregion Variables
        //    #region Operaciones
        //    try
        //    {

        //        var idCatClienteClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //        if (!string.IsNullOrEmpty(idCatClienteClaim) && int.TryParse(idCatClienteClaim, out int idCatCliente))
        //        {
        //            respuestaTokenDTO.IdCatCliente = idCatCliente;
        //        }

        //        // Get "IdCatSistema" from ClaimTypes.NameIdentifier
        //        var idCatSistemaClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //        if (!string.IsNullOrEmpty(idCatSistemaClaim) && int.TryParse(idCatSistemaClaim, out int idCatSistema))
        //        {
        //            respuestaTokenDTO.IdCatSistema = idCatSistema;
        //        }

        //        // Get "IdCatSistema" from ClaimTypes.NameIdentifier
        //        var idCatEmpresaClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
        //        if (!string.IsNullOrEmpty(idCatEmpresaClaim) && int.TryParse(idCatEmpresaClaim, out int idCatEmpresa))
        //        {
        //            respuestaTokenDTO.IdCatEmpresa = idCatEmpresa;
        //        }

        //        // Get "Role" from ClaimTypes.Role
        //        var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        //        if (!string.IsNullOrEmpty(roleClaim))
        //        {
        //            respuestaTokenDTO.RolesUsuario.Add(roleClaim);
        //        }

        //        return respuestaTokenDTO;
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //    #endregion Operaciones

        //}


        /*[HttpGet("referencia/{IdReferencia:int}", Name = "GetReferencia")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetReferencia(int IdReferencia)
        {
            var lstServicios = _ctRepovacios.GetReferencia(IdReferencia);
            //var lstServiciosDto = new List<PeticionesReferenciasDTO>();
            var lstServiciosDto = new PeticionesReferenciasDTO();
            //foreach (var lista in lstServicios)
            //{
            lstServiciosDto=(_mapper.Map<PeticionesReferenciasDTO>(lstServicios)); //se utiliza mapper para 
            //}
            return Ok(lstServiciosDto);
        }*/

        /*  [HttpPatch("{idALO:int}", Name = "ActualizaRefencia")]
          [ProducesResponseType(201, Type = typeof(ServiciosDTO))]
          [ProducesResponseType(204)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          [ProducesResponseType(StatusCodes.Status401Unauthorized)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]
          public IActionResult ActualizaRefencia(int idALO, [FromBody] PeticionesReferenciasDTO peticionesReferenciasDTO)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }
              if (peticionesReferenciasDTO == null || idALO != peticionesReferenciasDTO.IdReferencia)
              {
                  return BadRequest(ModelState);
              }

              var servicio = _mapper.Map<PeticionesReferencias>(peticionesReferenciasDTO);

              if (!_ctRepovacios.ActualizarServicio(servicio))
              {
                  ModelState.AddModelError("", $"Algo salió mal actualizando el registro{servicio.referenciaALO}");
                  return StatusCode(500, ModelState);
              }
              //return NoContent();
          }*/

        /*
        [HttpPost("ValidarSolicitud")]
        public async Task<IActionResult> ValidarSolicitud([FromBody] SolTicketDTO solTicket)
        {
            var resultado = await _ctRepovacios.ValidarSolicitudAsync(solTicket);

            if (!resultado.IsSuccess)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost("GuardarSolicitud")]
        public async Task<IActionResult> GuardarSolicitud([FromBody] SolTicketDTO solTicket)
        {
            var resultado = await _ctRepovacios.GuardarSolicitudAsync(solTicket);

            if (!resultado.IsSuccess)
                return StatusCode((int)resultado.StatusCode, resultado);

            return Ok(resultado);
        }
        */

        /*
        [AllowAnonymous]
        [HttpGet("Filtrar/", Name = "Filtrar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<PeticionesReferenciasDTO>>> Filtrar([FromBody] FiltroOrdenesReferenciasDTO pFiltro)
        {
            var lista = _ctRepovacios.GetReferencias(pFiltro);
            //var filtrada = lista.Where(t => (string.IsNullOrEmpty(prazonSocial) || t.RazonSocial.Contains(prazonSocial)) &&
            //                                 (string.IsNullOrEmpty(prfc) || t.RFC.Contains(prfc))).ToList();
            return Ok(lista);
        }
        */

        #region CARGA INICIAL
        //[HttpPost("cargaDatos/", Name = "GetCarga")]
        //[Authorize]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult GetCarga()
        //{
        //    var lstServicios = _ctRepovacios.CargaInicial();

        //    return Ok(lstServicios);
        //}
        #endregion CARGA INICIAL
        #endregion METODOS COMENTADOS
    }
}
