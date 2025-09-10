using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Control.IControl;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ALOG.Repositorios.Repositorio.Control;
using System.Net;

using ALOG.Repositorios.Repositorio.Integracion1G.IIntegracion1G;
using Humanizer;

namespace ApiVacios.Controllers.CtrllVacios
{
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    //[AllowAnonymous]
    [Route("operativo/[controller]")]
    [ApiController]
    public class VReferenciasController : Controller
    {
        /*
         * AUTOR: RAZE
         * FECHA: 2024
         */
        private readonly IGenericoRepositorio<PeticionesReferencias> _repositorio;
        private readonly IVaciosRepositorio _ctRepovacios;
        private readonly IPeticionesContenedoresRepo _ctrepoContenedores;
        private readonly IOrdenesRepositorio _ctRepoOrdenes;
        private readonly IControlAccesoRepo _tokenUsuarioService;
        private readonly IPeticionesContenedorCronRepo _ctrepoContenedorCron;
        private readonly IIntegracion1GRepo _integracion1GRepo;

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();
        //PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
        public VReferenciasController(ApplicationDbContext db, IGenericoRepositorio<PeticionesReferencias> repositorio,
            IPeticionesContenedoresRepo ctrepoContenedores,
            IVaciosRepositorio ctRepovacios, IOrdenesRepositorio ctRepoOrdenes, IMapper mapper,
            IIntegracion1GRepo integracion1GRepo)
        {
            _repositorio = repositorio;
            _db = db;
            _ctRepovacios = ctRepovacios;
            _ctRepoOrdenes = ctRepoOrdenes;
            _ctrepoContenedores = ctrepoContenedores;
            _mapper = mapper;
            _integracion1GRepo = integracion1GRepo;
            //objRespuesta = InicializaRespuesta();

            //INICIALIZA RESPUESTA GENERICA
            _respuestaGenericaDTO = new RespuestaGenericaDTO();
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
        }

        //[HttpPost("CrearContenedor/{IdReferencia:int}")]
        //[ProducesResponseType(201, Type = typeof(PeticionesContenedoresDTO))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult CrearContenedor(int IdReferencia, [FromBody] PeticionesContenedoresDTO peticionesContenedoresDTO)
        //{
        //    #region Notas
        //    /*
        //     * Autor: RAZE
        //     * Fecha: 2024
        //     * Nota: Se crea el contenedor y se asigna el IdContenedor al servicio. 
        //     */
        //    #endregion Notas


        //    //objRespuesta.Result = false;
        //    //Asignación de Valores.
        //    var objRespuesta = new PeticionesRespuestaDTO();

        //    var contenedor = _mapper.Map<PeticionesContenedores>(peticionesContenedoresDTO);
        //    var lstErroresCrearContenedores = _ctRepovacios.CrearContenedor(IdReferencia, contenedor);
        //    if (lstErroresCrearContenedores.Count > 0)
        //    {
        //        ModelState.AddModelError("", $"Algo salió mal guardando el registro {contenedor.Contenedor}");

        //        return StatusCode(500, ModelState);
        //    }

        //    var id = contenedor.IdContenedor;
        //    objRespuesta.IsSuccess = true;
        //    //return Ok(true);
        //    return Ok(objRespuesta);
        //    //return Ok(true);
        //    //return CreatedAtRoute("GetReferencia", new { IdReferencia = referencia.IdReferencia }, referencia);
        //}

        [HttpPost("ActualizarFolioContenedor/")]
        [ProducesResponseType(201, Type = typeof(SolActualizarFolioManiobraDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarFolioContenedor([FromBody] SolActualizarFolioManiobraDTO pSolActualizarFolioManiobraDTO)
        {
            #region Notas
            /*
             * Autor: RAZE
             * Fecha: 2024
             * Nota: Asignación de folio en actualización directa en pantalla. 
             */
            #endregion Notas
            var objRespuesta = new PeticionesRespuestaDTO();

            //objRespuesta.Result = false;
            //Asignación de Valores.


            //var contenedor = _mapper.Map<PeticionesContenedores>(peticionesContenedoresDTO);
            var lstErroresCrearContenedores = _ctRepovacios.ActualizarFolioContenedor(pSolActualizarFolioManiobraDTO);
            if (lstErroresCrearContenedores.Count > 0)
            {
                objRespuesta.IsSuccess = false;
                //ModelState.AddModelError("", $"Algo salió mal guardando el registro {contenedor.Contenedor}");
                objRespuesta.StatusCode = System.Net.HttpStatusCode.NotFound;
                objRespuesta.ErrorMessages = new List<string>();
                objRespuesta.ErrorMessages.AddRange(lstErroresCrearContenedores);
                return NotFound(objRespuesta);
            }

            var id = pSolActualizarFolioManiobraDTO.IdContenedor;
            objRespuesta.IsSuccess = true;
            objRespuesta.StatusCode = HttpStatusCode.OK;
            //return Ok(true);
            return Ok(objRespuesta);
            //return Ok(true);
            //return CreatedAtRoute("GetReferencia", new { IdReferencia = referencia.IdReferencia }, referencia);
        }

        //[HttpPost("CrearServicio/{IdReferencia:int}/{IdContenedor:int}")]
        //[ProducesResponseType(201, Type = typeof(PeticionesServiciosDTO))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult CrearServicio(int IdReferencia, int IdContenedor, [FromBody] PeticionesServiciosDTO peticionesServiciosDTO)
        //{

        //    PeticionesRespuesta objRespuesta = new PeticionesRespuesta();
        //    objRespuesta.IsSuccess = false;
        //    //objRespuesta.Result = false;
        //    //Asignación de Valores.
        //    //Validar IdServicio-> asignar nombre de servicio.

        //    var servicio = _mapper.Map<PeticionesServicios>(peticionesServiciosDTO);
        //    if (!_ctRepovacios.CrearServicio(IdReferencia, IdContenedor, servicio))
        //    {
        //        ModelState.AddModelError("", $"Algo salió mal guardando el registro del servicio: {servicio.IdTipoServicio}");
        //        //return StatusCode(500, ModelState);

        //        objRespuesta.StatusCode = System.Net.HttpStatusCode.InternalServerError;
        //        objRespuesta.ErrorMessages = new List<string>();
        //        objRespuesta.ErrorMessages.Add($"Algo salió mal guardando el registro del servicio: {servicio.IdTipoServicio}");
        //        return StatusCode(500, objRespuesta);
        //    }

        //    var id = servicio.IdServicio;

        //    objRespuesta.IsSuccess = true;

        //    return Ok(objRespuesta);

        //    //return Ok(true);
        //    //return CreatedAtRoute("GetReferencia", new { IdReferencia = referencia.IdReferencia }, referencia);
        //}

        //[HttpPost("CrearReferencia/{pIdOrden:int}")]
        //[ProducesResponseType(201, Type = typeof(PeticionesReferencias))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> CrearReferencia(int pIdOrden, [FromBody] PeticionesReferencias peticionesReferencias)
        //{
        //    #region Variables
        //    Ordenes objOrden = new Ordenes();

        //    PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
        //    objRespuesta.IsSuccess = true;
        //    objRespuesta.StatusCode = HttpStatusCode.OK;
        //    objRespuesta.ErrorMessages = new List<string>();

        //    //Asignación de Valores.
        //    peticionesReferencias.IdCatReferenciaEstado = 7;
        //    peticionesReferencias.EstadoReferencia = "PE";


        //    //Asginación de datos de Apertura de referencia.
        //    peticionesReferencias.IdCatReferenciaEstado = 7;
        //    List<string> strErrores = new List<string>();
        //    #endregion Variables

        //    //Nota: Validar documentos vacíos. Todos los documentos deben estar vacíos por contenedor.
        //    //Validar en creación estados de cerrado false.
        //    //Incluir fechas de registro el momento de guardado para todos los niveles.
        //    //Fechas de cierre en nulas para todos los niveles en la creación.
        //    //Consultar Catálogos por RFC para obtenerl el ID de cada llave.
        //    //Validar tipo de servicio existente del catálogo.No puede ser 0.

        //    objOrden = await _ctRepoOrdenes.GetOrden(pIdOrden);

        //    if (objOrden != null)
        //    {
        //        //Identificador de Orden de Servicio.
        //        peticionesReferencias.IdOrden = objOrden.IdOrden;

        //        if (!_ctRepovacios.CrearReferencia(peticionesReferencias, out strErrores))
        //        {
        //            objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
        //            objRespuesta.IsSuccess = false;
        //            objRespuesta.ErrorMessages = new List<string>();
        //            objRespuesta.ErrorMessages = strErrores;
        //            objRespuesta.ErrorMessages.Add($"Error en guardar la referencia: Ticket: {peticionesReferencias.Ticket}");
        //            return StatusCode(500, objRespuesta);
        //            //ModelState.AddModelError("", $"Algo salió mal guardando el registro{referencia.Ticket}");
        //            //return StatusCode(500, ModelState);
        //        }
        //        else
        //        {
        //            objRespuesta.IsSuccess = true;
        //            objRespuesta.IdOrdenServicio = objOrden.IdOrden;
        //            objRespuesta.IdReferenciaALO = peticionesReferencias.IdReferencia;

        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", $"Algo salió mal guardando el registro{peticionesReferencias.Ticket}");
        //        return StatusCode(500, ModelState);


        //    }

        //    var id = peticionesReferencias.IdReferencia;
        //    var idOrden = objOrden.IdOrden;

        //    //return Ok(true);
        //    return Ok(objRespuesta);
        //    //return CreatedAtRoute("GetReferencia", new { IdReferencia = referencia.IdReferencia }, referencia);
        //}

        [HttpPost("FiltrarOrdenes/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FiltrarOrdenes([FromBody] FiltroOrdenesReferenciasDTO pFiltro)
        {
            ICollection<Ordenes> objList = null;
            objList = await _ctRepovacios.GetOrdenes(pFiltro);

            return Ok(objList);
        }

        [HttpPost("FiltrarReferencias/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FiltrarReferencias(FiltroOrdenesReferenciasDTO pFiltro)
        {
            ICollection<RespObtenerReferenciasDTO> objList = null;
            objList = await _ctRepovacios.GetReferencias(pFiltro);

            return Ok(objList);
        }

        [HttpGet("ObtieneReferencia/{idALO:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtieneReferencia(int idALO)
        {
            //#region Variables
            //var lstServiciosDto = new List<PeticionesReferencias>();
            //var obj = new List<PeticionesReferencias>();
            //#endregion Variables

            var lstServicios = await _ctRepovacios.GetReferencia(idALO);

            // Extraer el claim de nombre de usuario del token JWT (usualmente "sub" o "name")
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
            if (lstServicios != null)
            {




                return Ok(lstServicios);                                                                //}
            }
            else
                return StatusCode(StatusCodes.Status404NotFound);

        }

        //[HttpPatch("ActivarContenedor/{IdReferencia:int}/{IdContenedor:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> ActivarContenedor(int IdReferencia, int IdContenedor)
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

        //[HttpDelete("BajaContenedorV/{IdReferencia:int}/{IdContenedor:int}", Name = "BajaContenedorV")]
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

        [HttpPatch("CambiarEstadoServicio/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CambiarEstadoServicio([FromBody] SolCambioEstadoDTO pSolCambioEstadoDTO)
        {

            var respuesta = await _ctRepovacios.CambiarEstadoServicio(pSolCambioEstadoDTO);
            return StatusCode((int)respuesta.StatusCode, respuesta);

            //#region Variables

            //RespuestaGenericaDTO objRespuesta = new RespuestaGenericaDTO();
            //objRespuesta.IsSuccess = false;
            //objRespuesta.StatusCode = HttpStatusCode.NotFound;
            //objRespuesta.lstrErrorMessages = new List<string>();

            //#endregion Variables


            //try
            //{
            //    //_ctRepovacios

            //    if (!_db.catReferenciaEstado.Any(x => x.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado && x.Activo == true))
            //    {

            //        objRespuesta.lstrErrorMessages.Add(new string($"No existe el estado: {pSolCambioEstadoDTO.IdCatReferenciaEstado}"));
            //        return NotFound(objRespuesta);
            //    }
            //    if (!_db.peticionesContenedores.Any(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor && x.Activo == true))
            //    {

            //        objRespuesta.lstrErrorMessages.Add(new string($"No existe el contenedor: {pSolCambioEstadoDTO.IdContenedor}"));
            //        return NotFound(objRespuesta);
            //    }
            //    if (!_db.peticionesServicios.Any(x => x.IdServicio == pSolCambioEstadoDTO.IdServicio && x.Activo == true))
            //    {

            //        objRespuesta.lstrErrorMessages.Add(new string($"No existe el servicio: {pSolCambioEstadoDTO.IdServicio}"));
            //        return NotFound(objRespuesta);
            //    }

            //    //var referencia = _ctRepovacios.GetContenedor(pIdReferencia, pIdContenedor);
            //    var resultado = _db.peticionesContenedores.Any(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor && x.Activo == true);
            //    if (!resultado)
            //    {
            //        objRespuesta.lstrErrorMessages.Add(new string($"No existe el contenedor: {pSolCambioEstadoDTO.IdContenedor} "));
            //        return NotFound(objRespuesta);
            //    }
            //    /*
            //     * Tiene documentos asignados
            //     * VALIDAMOS SI TIENE AL MENOS UN DOCUMENTO
            //     * Nota: debemos validar si es un comento al menos del servicio.
            //     */

            //    var existeDocumentos = _db.peticionesDocumentos.Any(x => x.IdServicio == pSolCambioEstadoDTO.IdServicio && x.Activo == true);
            //    if (!existeDocumentos && pSolCambioEstadoDTO.IdCatReferenciaEstado == 5)
            //    {
            //        objRespuesta.lstrErrorMessages.Add(new string($"No existen documentos asignados al servicio"));
            //        return NotFound(objRespuesta);
            //    }

            //    //ACtualizar Servicio
            //    var lsrobjServicio = _db.peticionesServicios.Where(x => x.IdServicio == pSolCambioEstadoDTO.IdServicio).FirstOrDefault();
            //    var objServicio = lsrobjServicio;
            //    objServicio.IdEstadoServicio = pSolCambioEstadoDTO.IdCatReferenciaEstado;
            //    objServicio.EstadoServicio = _db.catReferenciaEstado.FirstOrDefault(x => x.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado).Nombre;
            //    _db.peticionesServicios.Update(objServicio);
            //    _db.SaveChanges();

            //    /*
            //     * VALIDAR CIERRE DE SERVICIOS
            //     */
            //    //var existeServicioEnProceso = _db.peticionesServicios.Any(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor && x.IdServicio == pSolCambioEstadoDTO.IdServicio && x.Activo == true && (x.IdEstadoServicio == 1 || x.IdEstadoServicio == 4));
            //    //if (existeServicioEnProceso)
            //    //{
            //    //    objRespuesta.lstrErrorMessages.Add(new string($"Existe servicios en proceso del contenedor: {pSolCambioEstadoDTO.IdContenedor} "));
            //    //    return BadRequest(objRespuesta);
            //    //}

            //    ////Actualizar estado del contenedor

            //    var objResp = _db.peticionesContenedores.Where(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor).ToList();
            //    var obj = objResp.First();
            //    List<string> lstlista = new List<string>();
            //    lstlista.Add("1");
            //    lstlista.Add("4");
            //    lstlista.Add("7");

            //    var lsrobjServicio2 = _db.peticionesServicios.Where(x => x.IdServicio == pSolCambioEstadoDTO.IdServicio).ToList();
            //    bool objresultado = lstlista.Any(item => lsrobjServicio2.Select(x => x.IdEstadoServicio.ToString()).Contains(item));
            //    if (objresultado)
            //    {
            //        obj.IdEstadoContenedor = 4;
            //        obj.EstadoContenedor = _db.catReferenciaEstado.FirstOrDefault(x => x.IdCatReferenciaEstado == 4).Nombre;
            //    }
            //    else
            //    {
            //        obj.IdEstadoContenedor = 5;
            //        obj.EstadoContenedor = _db.catReferenciaEstado.FirstOrDefault(x => x.IdCatReferenciaEstado == 5).Nombre;
            //    }

            //    _db.peticionesContenedores.Update(obj);
            //    _db.SaveChanges();



            //    /*
            //     * VALIDAR CIERRE DE REFERENCIA
            //     */
            //    var existeEnProceso = _db.peticionesContenedores.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true && (x.IdEstadoContenedor == 1 || x.IdEstadoContenedor == 4 || x.IdEstadoContenedor == 7));
            //    var objPeticioReferenciaRC = _db.peticionesReferencias.FirstOrDefault(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia);
            //    if (!existeEnProceso)
            //    {

            //        objPeticioReferenciaRC.IdCatReferenciaEstado = 5;
            //        objPeticioReferenciaRC.EstadoReferencia = _db.catReferenciaEstado.FirstOrDefault(x => x.IdCatReferenciaEstado == 5).Nombre; ;

            //    }
            //    else
            //    {
            //        objPeticioReferenciaRC.IdCatReferenciaEstado = 4;
            //        objPeticioReferenciaRC.EstadoReferencia = _db.catReferenciaEstado.FirstOrDefault(x => x.IdCatReferenciaEstado == 4).Nombre; ;
            //    }
            //    _db.peticionesReferencias.Update(objPeticioReferenciaRC);
            //    _db.SaveChanges();

            //    objRespuesta.StatusCode = HttpStatusCode.OK;
            //    objRespuesta.IsSuccess = true;
            //    objRespuesta.lstrErrorMessages = new List<string>();

            //    return Ok(objRespuesta);
            //}
            //catch (Exception ex)
            //{

            //    objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
            //    objRespuesta.IsSuccess = false;
            //    objRespuesta.lstrErrorMessages = new List<string>();
            //    objRespuesta.lstrErrorMessages.Add(ex.Message);
            //    return StatusCode(500, objRespuesta);
            //}
        }

        [HttpPatch("CambiarEstadoContenedor/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CambiarEstadoContenedor([FromBody] SolCambioEstadoDTO pSolCambioEstadoDTO)
        {
            #region Variables

            RespuestaGenericaDTO objRespuesta = new RespuestaGenericaDTO();
            objRespuesta.IsSuccess = false;
            objRespuesta.StatusCode = HttpStatusCode.NotFound;
            objRespuesta.lstrErrorMessages = new List<string>();
            #endregion Variables

            if (!_db.peticionesReferencias.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia))
            {

                objRespuesta.lstrErrorMessages.Add(new string($"No existe la referencia: {pSolCambioEstadoDTO.IdReferencia}"));
                return NotFound(objRespuesta);
            }
            if (!_db.catReferenciaEstado.Any(x => x.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado))
            {

                objRespuesta.lstrErrorMessages.Add(new string($"No existe el estado: {pSolCambioEstadoDTO.IdCatReferenciaEstado}"));
                return NotFound(objRespuesta);
            }
            if (!_db.peticionesContenedores.Any(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor))
            {

                objRespuesta.lstrErrorMessages.Add(new string($"No existe el contenedor: {pSolCambioEstadoDTO.IdContenedor}"));
                return NotFound(objRespuesta);
            }

            //var referencia = _ctRepovacios.GetContenedor(pIdReferencia, pIdContenedor);
            var resultado = _db.peticionesContenedores.Any(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor);
            if (!resultado)
            {
                objRespuesta.lstrErrorMessages.Add(new string($"No existe el contenedor: {pSolCambioEstadoDTO.IdContenedor} en la referencia {pSolCambioEstadoDTO.IdReferencia}"));
                return NotFound(objRespuesta);
            }

            //HAY SERVICIOS ACTIVOS
            if (pSolCambioEstadoDTO.IdCatReferenciaEstado == 4)
            {
                var existeServicioEnProceso1 = _db.peticionesServicios.Any(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor && x.Activo == true && (x.IdEstadoServicio == 4));
                if (!existeServicioEnProceso1)
                {
                    objRespuesta.lstrErrorMessages.Add(new string($"No existen servicios en proceso del contenedor: {pSolCambioEstadoDTO.IdContenedor} "));
                    return BadRequest(objRespuesta);
                }
            }

            //HAY SERVICIOS ACTIVOS
            if (pSolCambioEstadoDTO.IdCatReferenciaEstado == 5)
            {
                var existeServicioEnProceso = _db.peticionesServicios.Any(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor && x.Activo == true && (x.IdEstadoServicio == 1 || x.IdEstadoServicio == 4));
                if (existeServicioEnProceso)
                {
                    objRespuesta.lstrErrorMessages.Add(new string($"Existe servicios en proceso del contenedor: {pSolCambioEstadoDTO.IdContenedor} "));
                    return BadRequest(objRespuesta);
                }
            }

            var obj = _db.peticionesContenedores.Where(x => x.IdContenedor == pSolCambioEstadoDTO.IdContenedor).FirstOrDefault();
            obj.IdEstadoContenedor = pSolCambioEstadoDTO.IdCatReferenciaEstado;
            obj.EstadoContenedor = _db.catReferenciaEstado.FirstOrDefault(x => x.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado).Nombre;

            _db.peticionesContenedores.Update(obj);
            _db.SaveChanges();
            /*
             * VALIDAR CIERRE DE REFERENCIA
             */
            var existeEnProceso = _db.peticionesContenedores.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && (x.IdEstadoContenedor == 1 || x.IdEstadoContenedor == 4));
            var objPeticioReferenciaRC = _db.peticionesReferencias.FirstOrDefault(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia);
            if (!existeEnProceso)
            {

                objPeticioReferenciaRC.IdCatReferenciaEstado = 5;

            }
            else
            {
                objPeticioReferenciaRC.IdCatReferenciaEstado = 4;
            }
            _db.peticionesReferencias.Update(objPeticioReferenciaRC);
            _db.SaveChanges();

            objRespuesta.StatusCode = HttpStatusCode.OK;
            objRespuesta.IsSuccess = true;
            objRespuesta.lstrErrorMessages = new List<string>();

            return Ok(objRespuesta);
        }

        [HttpPost("CambiarEstadoTodosContenedor/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CambiarEstadoTodosContenedor([FromBody] SolCambioEstadoDTO pSolCambioEstadoDTO)
        {
            #region Variables

            RespuestaGenericaDTO objRespuesta = new RespuestaGenericaDTO();
            objRespuesta.IsSuccess = false;
            objRespuesta.StatusCode = HttpStatusCode.NotFound;
            objRespuesta.lstrErrorMessages = new List<string>();
            #endregion Variables


            //VALIDAR TIPO SOLICITUD
            /*
             *Validamos si es TIPO RC .- Referencia Cliente (IdOrden, Idreferencia, ReferenciaCliente, Estado)
             *Validamos si es TIPO R.- Referencia cambia estado todos los contenedores (Requiere IdReferencia, Estado)
             *Validamos si es TIPO C.- Contenedor cambia estado  por contenedor (IdContenedor, Estado)
             *
             */
            switch (pSolCambioEstadoDTO.TipoCambio)
            {
                case "RC":
                    if (!_db.ordenes.Any(x => x.IdOrden == pSolCambioEstadoDTO.IdOrden && x.Activo == true) ||
                        !_db.peticionesReferencias.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true) ||
                        !_db.peticionesContenedores.Any(x => x.RefenciaCliente == pSolCambioEstadoDTO.ReferenciaCliente && x.Activo == true) ||
                        !_db.catReferenciaEstado.Any(x => x.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado && x.Activo == true))
                    {
                        objRespuesta.lstrErrorMessages.Add(new string($"Para cierre por Referencia cliente requiere : IdOrden, Idreferencia, ReferenciaCliente, IdCatReferenciaEstado"));
                        return NotFound(objRespuesta);
                    }


                    //VALIDAMOS SERVICIOS ABIERTOS POR CONTENEDORES.
                    var existeServicioEnProceso = _db.peticionesServicios.Include(c => c.PeticionesContenedores.RefenciaCliente.Equals(pSolCambioEstadoDTO.ReferenciaCliente) && c.Activo == true).Any(x => x.Activo == true && (x.IdEstadoServicio == 1 || x.IdEstadoServicio == 4));
                    if (existeServicioEnProceso)
                    {
                        objRespuesta.lstrErrorMessages.Add(new string($"Existe servicios en proceso de los contenedores con referencia cliente: {pSolCambioEstadoDTO.ReferenciaCliente} en la referencia {pSolCambioEstadoDTO.IdReferencia}"));
                        return BadRequest(objRespuesta);
                    }

                    using (var contexto = _db)
                    {
                        contexto.Database.ExecuteSqlRaw(
                            "UPDATE peticionesContenedores SET IdEstadoContenedor = {0} WHERE IdReferencia = {1} and ReferenciaCliente={2}",
                            pSolCambioEstadoDTO.IdCatReferenciaEstado, pSolCambioEstadoDTO.IdReferencia, pSolCambioEstadoDTO.ReferenciaCliente);

                    }

                    var existeEnProceso = _db.peticionesContenedores.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && (x.IdEstadoContenedor == 1 || x.IdEstadoContenedor == 4));
                    var objPeticioReferenciaRC = _db.peticionesReferencias.FirstOrDefault(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia);
                    if (!existeEnProceso)
                    {

                        objPeticioReferenciaRC.IdCatReferenciaEstado = 5;

                    }
                    else
                    {
                        objPeticioReferenciaRC.IdCatReferenciaEstado = 4;
                    }
                    _db.peticionesReferencias.Update(objPeticioReferenciaRC);
                    _db.SaveChanges();

                    break;
                case "R":
                    if (
                        !_db.peticionesReferencias.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true) ||
                        !_db.catReferenciaEstado.Any(x => x.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado && x.Activo == true))
                    {
                        objRespuesta.lstrErrorMessages.Add(new string($"Para cierre por Referencia cliente requiere : Idreferencia, IdCatReferenciaEstado"));
                        return NotFound(objRespuesta);
                    }
                    //VALIDAMOS SERVICIOS ABIERTOS POR CONTENEDORES.
                    var existeServicioEnProcesoR = _db.peticionesServicios.Include(c => c.PeticionesContenedores).Where(x => x.PeticionesContenedores.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true && (x.IdEstadoServicio == 1 || x.IdEstadoServicio == 4)).ToList();
                    if (existeServicioEnProcesoR.Count() > 0)
                    {
                        List<string> lstContenedores = new List<string>();
                        foreach (var item in existeServicioEnProcesoR)
                        {
                            lstContenedores.Add(item.PeticionesContenedores.Contenedor);
                        }

                        objRespuesta.lstrErrorMessages.Add(new string($"Existe servicios en proceso de los contenedores {string.Join(",", lstContenedores)} con referencia : {pSolCambioEstadoDTO.IdReferencia} "));
                        return BadRequest(objRespuesta);
                    }
                    using (var contexto = _db)
                    {
                        contexto.Database.ExecuteSqlRaw(
                            "UPDATE peticionesContenedores SET IdEstadoContenedor = {0} WHERE IdReferencia = {1}",
                            pSolCambioEstadoDTO.IdCatReferenciaEstado, pSolCambioEstadoDTO.IdReferencia);

                    }
                    var existeEnProcesoR = _db.peticionesContenedores.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true && (x.IdEstadoContenedor == 1 || x.IdEstadoContenedor == 4));
                    var objPeticioReferenciaR = _db.peticionesReferencias.FirstOrDefault(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true);
                    if (!existeEnProcesoR)
                    {

                        objPeticioReferenciaR.IdCatReferenciaEstado = 5;
                        _db.peticionesReferencias.Update(objPeticioReferenciaR);
                        _db.SaveChanges();
                    }
                    else
                    {
                        objPeticioReferenciaR.IdCatReferenciaEstado = 4;
                    }
                    _db.peticionesReferencias.Update(objPeticioReferenciaR);
                    _db.SaveChanges();
                    break;
                case "C":
                    if (!_db.peticionesReferencias.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true) ||
                        !_db.peticionesContenedores.Any(x => x.RefenciaCliente == pSolCambioEstadoDTO.ReferenciaCliente && x.Activo == true) ||
                        !_db.catReferenciaEstado.Any(x => x.IdCatReferenciaEstado == pSolCambioEstadoDTO.IdCatReferenciaEstado && x.Activo == true))
                    {
                        objRespuesta.lstrErrorMessages.Add(new string($"Para cierre por Referencia cliente requiere : IdReferencia, IdContenedor,  IdCatReferenciaEstado"));
                        return NotFound(objRespuesta);
                    }

                    //VALIDAMOS SERVICIOS ABIERTOS POR CONTENEDORES.
                    var existeServicioEnProcesoC = _db.peticionesServicios.Include(c => c.PeticionesContenedores.IdContenedor == pSolCambioEstadoDTO.IdContenedor && c.Activo == true).Any(x => x.Activo == true && (x.IdEstadoServicio == 1 || x.IdEstadoServicio == 4));
                    if (existeServicioEnProcesoC)
                    {
                        objRespuesta.lstrErrorMessages.Add(new string($"Existe servicios en proceso del contenedor : {pSolCambioEstadoDTO.IdContenedor}"));
                        return BadRequest(objRespuesta);
                    }
                    using (var contexto = _db)
                    {
                        contexto.Database.ExecuteSqlRaw(
                            "UPDATE peticionesContenedores SET IdEstadoContenedor = {0} WHERE IdContenedor = {1}",
                            pSolCambioEstadoDTO.IdCatReferenciaEstado, pSolCambioEstadoDTO.IdContenedor);

                    }
                    var existeEnProcesoC = _db.peticionesContenedores.Any(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true && (x.IdEstadoContenedor == 1 || x.IdEstadoContenedor == 4));
                    var objPeticioReferencia = _db.peticionesReferencias.FirstOrDefault(x => x.IdReferencia == pSolCambioEstadoDTO.IdReferencia && x.Activo == true);
                    if (!existeEnProcesoC)
                    {

                        objPeticioReferencia.IdCatReferenciaEstado = 5;

                    }
                    else
                    {
                        objPeticioReferencia.IdCatReferenciaEstado = 4;
                    }
                    _db.peticionesReferencias.Update(objPeticioReferencia);
                    _db.SaveChanges();
                    break;
                default:
                    return NotFound(objRespuesta);
            }

            objRespuesta.StatusCode = HttpStatusCode.OK;
            objRespuesta.IsSuccess = true;
            objRespuesta.lstrErrorMessages = new List<string>();

            return Ok(objRespuesta);
        }

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
        //        return NotFound();
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

        [HttpPost("IniciarProcesoOrdenes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IniciarProcesoOrdenes([FromBody] List<int> ordenes)
        {
            var respuesta = new PeticionesRespuestaDTO
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>()
            };

            if (ModelState.IsValid)
            {
                var errores = await _ctRepovacios.IniciarProcesoOrden(ordenes);
                if (errores.Count == 0)
                {
                    respuesta.IsSuccess = true;
                    respuesta.StatusCode = HttpStatusCode.OK;
                    return Ok(respuesta);
                }

                respuesta.ErrorMessages.AddRange(errores);
            }
            else
            {
                respuesta.ErrorMessages.Add("ModelState inválido.");
            }

            return BadRequest(respuesta);
        }

        [HttpPost("CancelarOrdenes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelarOrdenes([FromBody] List<int> ordenes)
        {
            var respuesta = new PeticionesRespuestaDTO
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>()
            };

            if (ModelState.IsValid)
            {
                var errores = await _ctRepovacios.CancelarOrden(ordenes);
                if (errores.Count == 0)
                {
                    respuesta.IsSuccess = true;
                    respuesta.StatusCode = HttpStatusCode.OK;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.ErrorMessages.AddRange(errores);
                }
            }
            else
            {
                respuesta.ErrorMessages.Add("ModelState inválido.");
            }

            return BadRequest(respuesta);
        }

        [HttpPost("CancelarElemento/{parametrosEncriptados}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelarElemento(string parametrosEncriptados)
        {
            var respuesta = new PeticionesRespuestaDTO
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>()
            };

            var error = await _ctRepovacios.CancelarElemento(parametrosEncriptados);
            if (error == null)
            {
                respuesta.IsSuccess = true;
                respuesta.StatusCode = HttpStatusCode.OK;
                return Ok(respuesta);
            }

            respuesta.ErrorMessages.Add(error);
            return BadRequest(respuesta);
        }

        //// PARA PROBAR EL ENDPOINT GET OBTENER TARIFARIO SERVICIOS

        //[HttpGet("PruebaEncriptarTarifaServicios")]
        //public IActionResult PruebaEncriptarParametrosTarifas([FromQuery] int param1, int param2, [FromQuery] int? param3)
        //{
        //    var respuesta = new PeticionesRespuestaDTO
        //    {
        //        IsSuccess = false,
        //        StatusCode = HttpStatusCode.BadRequest,
        //        ErrorMessages = new List<string>()
        //    };

        //    var filtroTarifas = new FiltroTarifarioServicioDTO
        //    {
        //        IdCatEmpresa = param1,
        //        IdLineaNegocio = param2,
        //        IdCatAduana = param3
        //    };

        //    string queryString = FiltroMapperHelper.ConvertirFiltroAString(filtroTarifas);

        //    //Cifrar
        //    string textoCifrado = AesEncryptionHelper.Encrypt(queryString);

        //    if (!string.IsNullOrWhiteSpace(textoCifrado))
        //    {
        //        respuesta.IsSuccess = true;
        //        respuesta.StatusCode = HttpStatusCode.OK;
        //        respuesta.ErrorMessages.Add(textoCifrado);
        //        return Ok(respuesta);
        //    }

        //    respuesta.ErrorMessages.Add("No se pudo generar el token encriptado. Intenta nuevamente.");
        //    return BadRequest(respuesta);
        //}

        //// PARA PROBAR EL ENDPOINT DE CATALOGO DE PATIOS

        //[HttpGet("PruebaEncriptarCatalogoPatios")]
        //public IActionResult PruebaEncriptarParametrosPatios([FromQuery] string? parametro, [FromQuery] int? idCatAduana)
        //{
        //    var respuesta = new PeticionesRespuestaDTO
        //    {
        //        IsSuccess = false,
        //        StatusCode = HttpStatusCode.BadRequest,
        //        ErrorMessages = new List<string>()
        //    };
        //    var filtroPatio = new FiltroPatioDTO
        //    {
        //        ProveedorInfo = parametro,
        //        IdCatAduana = idCatAduana
        //    };

        //    string queryString = FiltroMapperHelper.ConvertirFiltroAString(filtroPatio);

        //    string textoCifrado = AesEncryptionHelper.Encrypt(queryString);

        //    if (!string.IsNullOrWhiteSpace(textoCifrado))
        //    {
        //        respuesta.IsSuccess = true;
        //        respuesta.StatusCode = HttpStatusCode.OK;
        //        respuesta.ErrorMessages.Add(textoCifrado);
        //        return Ok(respuesta);
        //    }

        //    respuesta.ErrorMessages.Add("No se pudo generar el token encriptado. Intenta nuevamente.");
        //    return BadRequest(respuesta);
        //}

        [HttpGet("ObtenerTarifarioServicios/{parametrosEncriptados}")]
        public async Task<IActionResult> ObtenerTarifarioServicios(string parametrosEncriptados)
        {
            var objTarifarioService = await _ctRepovacios.GetTarifarioServicios(parametrosEncriptados);

            if (objTarifarioService.Count() > 0)
                return Ok(objTarifarioService);

            return BadRequest(objTarifarioService);
        }

        #region Contenedores
        [HttpGet("ObtenerContenedor/{pContenedor:int}")]
        public async Task<IActionResult> ObtenerContenedor(int pContenedor)
        {
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.strMensaje = null;
            _respuestaGenericaDTO.lstrErrorMessages = new List<string>();


            if (pContenedor <= 0)
            {
                _respuestaGenericaDTO.strMensaje = "IdContenedor debe ser mayor a 0";
                return BadRequest(_respuestaGenericaDTO);
            }
            var resultado = await _ctrepoContenedores.obtenerContenedor(pContenedor);

            if (resultado == null)
                return BadRequest(resultado);

            return Ok(resultado);
        }
        #endregion Contenedores

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

            #region comentato
            //#region UsuarioAcceso
            //respuestaTokenDTO = HttpContext.ObtenerUsuarioTokenUnificado();
            //#endregion UsuarioAcceso

            //#region ASIGNACIÓN DE VALORES POR DEFAULT AL OBJETO Ordenes
            //objOrden.IdCatCliente = respuestaTokenDTO.IdCatCliente == null ? peticionesReferenciasClienteExternoDTO.IdCatClienteSolicitante : (int)respuestaTokenDTO.IdCatCliente;
            //objOrden.IdUsuario = respuestaTokenDTO.IdCatUsuario;
            //objOrden.IdCatSistema = null;
            //objOrden.IdCatEmpresa = 2;
            //objOrden.IdCatSucursal = 1; //Veracruz.
            //objOrden.IdCatLineaNegocio = 1; //Vacios
            //objOrden.IdEstadoOrden = 5;
            //objOrden.Activo = true;
            //objOrden.FechaRegistro = DateTime.Now;

            //var objCatAduana = _ctRepovacios.GetAduana(peticionesReferenciasClienteExternoDTO.IdCatAduana);
            //if (objCatAduana != null)
            //{
            //    objOrden.IdCatAduana = objCatAduana.IdCatAduana;
            //}
            //#endregion

            ////Nota: Validar documentos vacíos. Todos los documentos deben estar por contenedor.

            //#region ValidarObjeto            
            //var objPeticionesReferencias = _ctRepovacios.ValidaOrdenServicios(peticionesReferenciasClienteExternoDTO, out lstStrErrores);
            //if (lstStrErrores.Count > 0)
            //{
            //    objRespuesta.IsSuccess = false;
            //    objRespuesta.ErrorMessages.AddRange(lstStrErrores);
            //    objRespuesta.StatusCode = HttpStatusCode.BadRequest;
            //    return BadRequest(objRespuesta);
            //}
            //#endregion ValidarObjeto

            ////Generar folio de referencia ALO
            //var vStrReferenciaALO = await _ctRepoOrdenes.ObtenerReferenciaALO(objOrden, 0, 0, 1);
            //if (!string.IsNullOrEmpty(vStrReferenciaALO))
            //{
            //    objOrden.ReferenciaALO = vStrReferenciaALO;
            //    objOrden.IdEstadoOrden = objPeticionesReferencias.IdCatReferenciaEstado == 4 ? objPeticionesReferencias.IdCatReferenciaEstado : objOrden.IdEstadoOrden;

            //    using (var transaction = _db.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            #region Operaciones
            //            var ordenInsertada = await _ctRepoOrdenes.CrearOrden(objOrden);
            //            if (ordenInsertada != null)
            //            {
            //                //Identificador de Orden de Servicio.
            //                objPeticionesReferencias.IdOrden = ordenInsertada.IdOrden;
            //                objPeticionesReferencias.Contenedores
            //                                                    .ToList()
            //                                                    .ForEach(c =>
            //                                                    {
            //                                                        // Asigna la referencia ALO al contenedor
            //                                                        if (string.IsNullOrWhiteSpace(c.RefenciaCliente))
            //                                                            c.RefenciaCliente = vStrReferenciaALO;

            //                                                        // Y también a cada servicio dentro de ese contenedor,
            //                                                        // pero solo si aún no tiene valor
            //                                                        c.Servicios?
            //                                                         .ToList()
            //                                                         .ForEach(s =>
            //                                                         {
            //                                                             if (string.IsNullOrWhiteSpace(s.ReferenciaClienteFacturar))
            //                                                                 s.ReferenciaClienteFacturar = vStrReferenciaALO;
            //                                                         });
            //                                                    });

            //                if (_ctRepovacios.CrearReferencia(objPeticionesReferencias, out lstStrErrores))
            //                {
            //                    // Si todo sale bien, se confirma la transacción de la referencia
            //                    transaction.Commit();

            //                    objRespuesta.IsSuccess = true;
            //                    objRespuesta.IdOrdenServicio = objPeticionesReferencias.IdOrden;
            //                    objRespuesta.IdReferenciaALO = objPeticionesReferencias.IdReferencia;

            //                    var lstErroresOutDocumentos = await _ctRepovacios.CrearDocumentos(objPeticionesReferencias, peticionesReferenciasClienteExternoDTO);
            //                    if (lstErroresOutDocumentos.Count > 0)
            //                    {
            //                        objRespuesta.ErrorMessages.Add("Se creo la orden de servicio pero se presentaron problemas al guardar los documentos.");
            //                    }
            //                }
            //                else
            //                {
            //                    // En caso de error, se revierte la transacción.
            //                    transaction.Rollback();

            //                    objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
            //                    objRespuesta.IsSuccess = false;
            //                    objRespuesta.ErrorMessages.AddRange(lstStrErrores);
            //                    objRespuesta.ErrorMessages.Add($"Error al guardar la orden.");
            //                    return StatusCode(500, objRespuesta);
            //                }
            //            }
            //            else
            //            {
            //                // En caso de error, se revierte la transacción.
            //                transaction.Rollback();

            //                objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
            //                objRespuesta.IsSuccess = false;
            //                objRespuesta.ErrorMessages.AddRange(lstStrErrores);
            //                objRespuesta.ErrorMessages.Add($"Error al guardar la orden.");
            //                return StatusCode(500, objRespuesta);
            //            }

            //            return Ok(objRespuesta);
            //            #endregion Operaciones
            //        }
            //        catch (Exception ex)
            //        {
            //            // En caso de error, se revierte la transacción.
            //            //transaction.Rollback();

            //            objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
            //            objRespuesta.IsSuccess = false;
            //            objRespuesta.ErrorMessages.AddRange(lstStrErrores);
            //            objRespuesta.ErrorMessages.Add($"Error al guardar la orden.");
            //            return StatusCode(500, objRespuesta);
            //        }
            //    }
            //}
            //else
            //{
            //    objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
            //    objRespuesta.IsSuccess = false;
            //    objRespuesta.ErrorMessages.AddRange(lstStrErrores);
            //    objRespuesta.ErrorMessages.Add($"Error al generar la referencia ALO.");
            //    return StatusCode(500, objRespuesta);
            //}
            #endregion comentado
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

            #region comentado
            //#region Variables
            //var objContenedor = new PeticionesContenedores();
            //var objRespuesta = new PeticionesRespuestaDTO();
            //var lstStrErrores = new List<string>();
            //#endregion Variables

            //#region Asignación de valores a variables
            //objRespuesta.IsSuccess = false;
            //objRespuesta.StatusCode = HttpStatusCode.BadRequest;
            //#endregion

            //if (ModelState.IsValid)
            //{
            //    //1).- VALIDAMOS SI LA REFERENCIA SE ENCUENTRA ACTIVA Y EN CASO DE SER UN CLIENTE QUIEN CONSULTA, VALIDAMOS QUE LA REFERENCIA QUE CONSULTA LE PERTENEZCA.
            //    if (_ctRepovacios.ExisteReferenciaActiva(IdReferencia))
            //    {
            //        //2).- OBTENEMOS LA INFORMACIÓN DE LA ORDEN RELACIONADA A LA REFERENCIA
            //        var objOrden = _ctRepovacios.GetOrdenPorIdReferencia(IdReferencia);
            //        if (objOrden != null)
            //        {
            //            //3).- OBTENEMOS LA INFORMACIÓN DE LA ADUANA QUE SE ENCUENTRA EN LA ORDEN
            //            var objCatAduana = _ctRepovacios.GetAduana((int)objOrden.IdCatAduana);
            //            if (objCatAduana != null)
            //            {
            //                //4).- ASIGNAMOS LOS VALORES DE LA ADUANA AL CONTENEDOR
            //                objContenedor.AduanaId = objCatAduana.Aduana;
            //                objContenedor.Aduana = objCatAduana.Nombre;
            //                objContenedor.RefenciaCliente = string.IsNullOrWhiteSpace(peticionesContenedoresClienteExternoDTO.ReferenciaCliente) ? objOrden.ReferenciaALO : peticionesContenedoresClienteExternoDTO.ReferenciaCliente;
            //                objContenedor.IdReferencia = IdReferencia;
            //                objContenedor.IdCatTransportista = null;

            //                //5).- VALIDAMOS EL CONTENEDOR Y SUS SERVICIOS
            //                lstStrErrores = _ctRepovacios.ValidarOrdenContenedorClienteExterno(peticionesContenedoresClienteExternoDTO, objContenedor);
            //                if (lstStrErrores.Count == 0)
            //                {
            //                    using (var transaction = _db.Database.BeginTransaction())
            //                    {
            //                        try
            //                        {
            //                            //6).- CREAMOS EL CONTENEDOR JUNTO CON SUS SERVICIOS
            //                            lstStrErrores.AddRange(_ctRepovacios.CrearContenedor(IdReferencia, objContenedor));
            //                            if (lstStrErrores.Count == 0)
            //                            {
            //                                objRespuesta.IsSuccess = true;
            //                                objRespuesta.StatusCode = HttpStatusCode.OK;
            //                                objRespuesta.IdReferenciaALO = IdReferencia;
            //                                objRespuesta.IdOrdenServicio = objOrden.IdOrden;
            //                                // Si todo sale bien, se confirma la transacción
            //                                transaction.Commit();

            //                                //7).- OBTENEMOS LA INFORMACIÓN DE LA ORDEN Y FILTRAMOS LA REFERENCIA POR EL 
            //                                var peticionesReferencias = await _ctRepovacios.GetReferencia(IdReferencia);
            //                                //var peticionesReferencias = objOrden.peticionesReferencias.Where(pr => pr.IdReferencia.Equals(IdReferencia)).FirstOrDefault();

            //                                var peticionesReferenciasClienteExternoDTO = new PeticionesReferenciasClienteExternoDTO();
            //                                peticionesReferenciasClienteExternoDTO.Contenedores = new List<PeticionesContenedoresClienteExternoDTO>();
            //                                peticionesReferenciasClienteExternoDTO.Contenedores.Add(peticionesContenedoresClienteExternoDTO);

            //                                var lstErroresOutDocumentos = await _ctRepovacios.CrearDocumentos(peticionesReferencias, peticionesReferenciasClienteExternoDTO);
            //                                if (lstErroresOutDocumentos.Count > 0)
            //                                {
            //                                    objRespuesta.ErrorMessages.Add("El contenedor se creo correctamente, pero se presentaron problemas al guardar los documentos.");
            //                                }

            //                                return Ok(objRespuesta);
            //                            }
            //                            else
            //                            {
            //                                // En caso de error, se revierte la transacción.
            //                                transaction.Rollback();

            //                                objRespuesta.ErrorMessages = lstStrErrores;
            //                                return BadRequest(objRespuesta);
            //                            }
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            // En caso de error, se revierte la transacción.
            //                            transaction.Rollback();

            //                            objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
            //                            objRespuesta.ErrorMessages.Add($"Error al guardar el contenedor.");
            //                            return StatusCode(500, objRespuesta);
            //                        }
            //                    }
            //                }
            //            }
            //        }

            //        objRespuesta.ErrorMessages = lstStrErrores;
            //        objRespuesta.ErrorMessages.Add($"Error al guardar el contenedor {objContenedor.Contenedor}.");
            //        return BadRequest(objRespuesta);
            //    }
            //    else
            //    {
            //        objRespuesta.ErrorMessages.Add("No fue posible agregar el contenedor a la referencia, ya que la referencia no existo o no se encuentra activa.");
            //        return BadRequest(objRespuesta);
            //    }
            //}
            //else
            //{
            //    objRespuesta.ErrorMessages.Add(ModelState.ToString());
            //    return Unauthorized(objRespuesta);
            //}
            #endregion comentado
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

            #region COMENTADO
            //#region Variables
            //var objRespuesta = new PeticionesRespuestaDTO();
            //objRespuesta.ErrorMessages = new List<string>();
            //var lstStrErrores = new List<string>();
            //#endregion Variables

            //#region Asignación de Valores.
            //objRespuesta.IsSuccess = false;
            //objRespuesta.StatusCode = HttpStatusCode.BadRequest;
            //#endregion Asignación de Valores.

            //var objPeticionesContenedores = _ctRepovacios.GetContenedor(IdReferencia, IdContenedor);
            //if (objPeticionesContenedores != null)
            //{
            //    var objPeticionesServicios = _ctRepovacios.ValidarOrdenServiciosClienteExterno(objPeticionesContenedores, peticionesServiciosClienteExternoDTO, out var lstStrErroresOut);
            //    if (lstStrErroresOut.Count == 0)
            //    {
            //        if (objPeticionesServicios != null)
            //        {
            //            if (!_ctRepovacios.CrearServicio(IdReferencia, IdContenedor, objPeticionesServicios))
            //            {
            //                objRespuesta.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            //                objRespuesta.ErrorMessages.Add($"Algo salió mal guardando el registro del servicio: {objPeticionesServicios.IdTipoServicio}");
            //                return StatusCode(500, objRespuesta);
            //            }

            //            objRespuesta.StatusCode = HttpStatusCode.OK;
            //            objRespuesta.IsSuccess = true;
            //            return Ok(objRespuesta);
            //        }
            //    }
            //    else
            //        lstStrErrores.AddRange(lstStrErroresOut);

            //    objRespuesta.ErrorMessages.AddRange(lstStrErrores);
            //    return BadRequest(objRespuesta);
            //}
            //else
            //{
            //    objRespuesta.ErrorMessages.Add("No se encontro información con la referencia y contenedor proporcionado. Verifique la información.");
            //    return BadRequest(objRespuesta);
            //}
            #endregion COMENTADO

        }
        #endregion ENDPOINTS CLIENTES EXTERNOS

        #region Integración 1G
        [HttpPost("Integracion1G/{idOrden:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Integracion1G(int idOrden) {
            var respuesta = new PeticionesRespuestaDTO {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>()
            };
            var orden = await _ctRepoOrdenes.GetOrden(idOrden);
            var response = await _integracion1GRepo.GenerarFacturacionDesdeOrdenesAsync(orden);

            respuesta.StatusCode = response.StatusCode;
            respuesta.IsSuccess = response.IsSuccess;
            respuesta.ErrorMessages = response.lstrErrorMessages;

            if (respuesta.ErrorMessages.Count() == 0) {
                return Ok(respuesta);
            }
            return BadRequest(respuesta);
        }
        #endregion Integración 1G

        #region Generación de anticipo
        [HttpPost("GenerarAnticipo/{idOrden:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerarAnticipo(int idOrden) {
            var respuesta = new PeticionesRespuestaDTO {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>()
            };
            var orden = await _ctRepoOrdenes.GetOrden(idOrden);
            var response = await _ctRepovacios.GenerarAnticipo(orden);

            respuesta.StatusCode = response.StatusCode;
            respuesta.IsSuccess = response.IsSuccess;
            respuesta.ErrorMessages = response.lstrErrorMessages;

            if (respuesta.ErrorMessages.Count() == 0) {
                return Ok(respuesta);
            }
            return BadRequest(respuesta);
        }
        #endregion Generación de anticipo

        #region Métodos comentados
        //[HttpGet("ObtenerArchivos/{IdUUID}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetFileByUuid(string IdUUID)
        //{
        //    // Simulación: Asumiendo que el archivo está en una carpeta específica y el nombre es derivado del UUID
        //    /*var filePath = Path.Combine("ruta/a/tus/archivos", $"{uuid}.pdf");

        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound(); // Devuelve 404 si el archivo no existe
        //    }

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(filePath, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }
        //    memory.Position = 0;

        //    // Determinar el tipo MIME del archivo (en este caso, PDF)
        //    string mimeType = "application/pdf";

        //    // Devuelve el archivo al cliente
        //    return File(memory, mimeType, Path.GetFileName(filePath));*/
        //    return NotFound(false);
        //}

        //[HttpGet("{tipo}/{nombre}")]
        //public async Task<IActionResult> ObtenerObjDocumento(string IdUUID)
        //{
        //    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Documentos");
        //    string filePath = Path.Combine(folderPath, $"{nombre}.{tipo}");

        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound();
        //    }

        //    var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //    var mimeType = tipo switch
        //    {
        //        "pdf" => "application/pdf",
        //        "png" => "image/png",
        //        "jpg" => "image/jpeg",
        //        _ => "application/octet-stream"
        //    };

        //    var documento = new PeticionesDocumentos
        //    {
        //        IdDocumento = new Random().Next(1, 1000), // Ejemplo de ID generado
        //        Documento = fileBytes,
        //        DocumentoUUID = Guid.NewGuid().ToString(),
        //        Ubicacion = filePath,
        //        IdTipoDocumento = tipo switch
        //        {
        //            "pdf" => 1,
        //            "png" => 2,
        //            "jpg" => 3,
        //            _ => 0
        //        },
        //        MimeType = mimeType,
        //        NombreDocumento = $"{nombre}.{tipo}",
        //        //CatDocumento = new CatDocumentos { Id = 1, Nombre = "Documento de Ejemplo" },
        //        TipoDocumentoNombre = tipo.ToUpper(),
        //        IdServicio = 1,
        //        FechaRegistro = DateTime.Now
        //    };

        //    return Ok(documento);
        //}

        //[HttpGet("{tipo}/{nombre}")]
        //public async Task<IActionResult> ObtenerDocumento(string IdUUID)
        //{
        //    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Documentos");
        //    string filePath = Path.Combine(folderPath, $"{nombre}.{tipo}");

        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound();
        //    }

        //    string contentType = tipo switch
        //    {
        //        "pdf" => "application/pdf",
        //        "png" => "image/png",
        //        "jpg" => "image/jpeg",
        //        _ => "application/octet-stream"
        //    };

        //    var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //    return File(fileBytes, contentType, $"{nombre}.{tipo}");
        //}


        //private PeticionesRespuestaDTO InicializaRespuesta()
        //{
        //    PeticionesRespuestaDTO objRespuestaGenericaDTO = new PeticionesRespuestaDTO();
        //    objRespuestaGenericaDTO.IsSuccess = false;
        //    objRespuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
        //    objRespuestaGenericaDTO.ErrorMessages = new List<string>();
        //    return objRespuestaGenericaDTO;
        //}

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

        //[Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
        //[HttpPost("ValidarSolicitud")]
        //public async Task<IActionResult> ValidarSolicitud([FromBody] SolTicketDTO solTicket) {
        //    var resultado = await _ctRepovacios.ValidarSolicitudAsync(solTicket);

        //    if (!resultado.IsSuccess) {
        //        return BadRequest(resultado);
        //    }

        //    return Ok(resultado);
        //}

        //[Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
        //[HttpPost("GuardarSolicitud")]
        //public async Task<IActionResult> GuardarSolicitud([FromBody] SolTicketDTO solTicket) {
        //    var resultado = await _ctRepovacios.GuardarSolicitudAsync(solTicket);

        //    if (!resultado.IsSuccess)
        //        return StatusCode((int)resultado.StatusCode, resultado);

        //    return Ok(resultado);
        //}
        #endregion Métodos comentados
    }
}
