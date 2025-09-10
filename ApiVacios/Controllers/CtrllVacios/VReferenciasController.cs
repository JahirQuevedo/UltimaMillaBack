


using ApiVacios.Data;
using ApiModelos.Modelos;
using ApiModelos.Modelos.Catalogos;
using ApiModelos.Modelos.DTO;
using ApiModelos.Modelos.DTO.Consultas;
using ApiModelos.Modelos.DTO.Vacios;
using ApiModelos.Modelos.Orden;
using ApiModelos.Modelos.Vacios;
using ApiVacios.Repositorio;
using ApiVacios.Repositorio.Generico.IGenerico;
using ApiVacios.Repositorio.IRepoOrdenes;
using ApiVacios.Repositorio.Logistica.ILogisticosRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Web;


namespace ApiVacios.Controllers.CtrllVacios
{
    [AllowAnonymous]
    [Route("operativo/[controller]")]
    [ApiController]
    public class VReferenciasController : GenericoController<PeticionesReferencias>
    {
        private readonly IGenericoRepositorio<PeticionesReferencias> _repositorio;
        private readonly IVaciosRepositorio _ctRepovacios;
        private readonly IOrdenesRepositorio _ctRepoOrdenes;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VReferenciasController(ApplicationDbContext db, IGenericoRepositorio<PeticionesReferencias> repositorio, IVaciosRepositorio ctRepovacios, IOrdenesRepositorio ctRepoOrdenes, IMapper mapper) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
            _ctRepovacios = ctRepovacios;
            _ctRepoOrdenes = ctRepoOrdenes;
            _mapper = mapper;

        }

        [HttpPost("CrearContenedor/{IdReferencia:int}")]
        [ProducesResponseType(201, Type = typeof(PeticionesContenedoresDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearContenedor(int IdReferencia, [FromBody] PeticionesContenedoresDTO peticionesContenedoresDTO)
        {

            PeticionesRespuesta objRespuesta = new PeticionesRespuesta();
            //objRespuesta.Result = false;
            //Asignación de Valores.


            var contenedor = _mapper.Map<PeticionesContenedores>(peticionesContenedoresDTO);
            if (!_ctRepovacios.CrearContenedor(IdReferencia, contenedor))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {contenedor.Contenedor}");

                return StatusCode(500, ModelState);
            }

            var id = contenedor.IdContenedor;
            objRespuesta.IsSuccess = true;
            //return Ok(true);
            return Ok(objRespuesta);
            //return Ok(true);
            //return CreatedAtRoute("GetReferencia", new { IdReferencia = referencia.IdReferencia }, referencia);
        }

        [HttpPost("CrearServicio/{IdReferencia:int}/{IdContenedor:int}")]
        [ProducesResponseType(201, Type = typeof(PeticionesServiciosDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearServicio(int IdReferencia, int IdContenedor, [FromBody] PeticionesServiciosDTO peticionesServiciosDTO)
        {

            PeticionesRespuesta objRespuesta = new PeticionesRespuesta();
            objRespuesta.IsSuccess = false;
            //objRespuesta.Result = false;
            //Asignación de Valores.
            //Validar IdServicio-> asignar nombre de servicio.



            var servicio = _mapper.Map<PeticionesServicios>(peticionesServiciosDTO);
            if (!_ctRepovacios.CrearServicio(IdReferencia, IdContenedor, servicio))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro del servicio: {servicio.IdTipoServicio}");
                //return StatusCode(500, ModelState);

                objRespuesta.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                objRespuesta.ErrorMessages = new List<string>();
                objRespuesta.ErrorMessages.Add($"Algo salió mal guardando el registro del servicio: {servicio.IdTipoServicio}");
                return StatusCode(500, objRespuesta);
            }

            var id = servicio.IdServicio;

            objRespuesta.IsSuccess = true;

            return Ok(objRespuesta);

            //return Ok(true);
            //return CreatedAtRoute("GetReferencia", new { IdReferencia = referencia.IdReferencia }, referencia);
        }

        [HttpPost("CrearReferencia/{pIdOrden:int}")]
        [ProducesResponseType(201, Type = typeof(PeticionesReferencias))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearReferencia(int pIdOrden, [FromBody] PeticionesReferencias peticionesReferencias)
        {
            #region Variables
            Ordenes objOrden = new Ordenes();

            PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.IsSuccess = true;
            objRespuesta.StatusCode = HttpStatusCode.OK;
            objRespuesta.ErrorMessages = new List<string>();

            //Asignación de Valores.
            peticionesReferencias.EstadoReferencia = "A";


            //Asginación de datos de Apertura de referencia.
            peticionesReferencias.IdCatReferenciaEstado = 1;
            List<string> strErrores = new List<string>();
            #endregion Variables

            //Nota: Validar documentos vacíos. Todos los documentos deben estar vacíos por contenedor.
            //Validar en creación estados de cerrado false.
            //Incluir fechas de registro el momento de guardado para todos los niveles.
            //Fechas de cierre en nulas para todos los niveles en la creación.
            //Consultar Catálogos por RFC para obtenerl el ID de cada llave.
            //Validar tipo de servicio existente del catálogo.No puede ser 0.


            
             objOrden = _ctRepoOrdenes.GetOrden(pIdOrden);

            if (objOrden != null)
            {
                //Identificador de Orden de Servicio.
                peticionesReferencias.IdOrden = objOrden.IdOrden;

                if (!_ctRepovacios.CrearReferencia(peticionesReferencias, out strErrores))
                {
                    objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                    objRespuesta.IsSuccess = false;
                    objRespuesta.ErrorMessages = new List<string>();
                    objRespuesta.ErrorMessages = strErrores;
                    objRespuesta.ErrorMessages.Add($"Error en guardar la referencia: Ticket: {peticionesReferencias.Ticket}");
                    return StatusCode(500, objRespuesta);
                    //ModelState.AddModelError("", $"Algo salió mal guardando el registro{referencia.Ticket}");
                    //return StatusCode(500, ModelState);
                }
                else
                {
                    objRespuesta.IsSuccess = true;
                    objRespuesta.IdOrdenServicio = objOrden.IdOrden;
                    objRespuesta.IdReferenciaALO = peticionesReferencias.IdReferencia;

                }
            }
            else
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{peticionesReferencias.Ticket}");
                return StatusCode(500, ModelState);


            }

            var id = peticionesReferencias.IdReferencia;
            var idOrden = objOrden.IdOrden;

            //return Ok(true);
            return Ok(objRespuesta);
            //return CreatedAtRoute("GetReferencia", new { IdReferencia = referencia.IdReferencia }, referencia);
        }


        [AllowAnonymous]
        [HttpPost("FiltrarOrdenes/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FiltrarOrdenes([FromBody] FiltroOrdenesReferenciasDTO pFiltro)
        {
            ICollection<Ordenes> objList = null; 
            objList = _ctRepovacios.GetOrdenes(pFiltro);
            
            return Ok( objList);
        }


        [AllowAnonymous]
        [HttpPost("FiltrarReferencias/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FiltrarReferencias([FromBody] FiltroOrdenesReferenciasDTO pFiltro)
        {
            ICollection<PeticionesReferencias> objList = null;
            objList = _ctRepovacios.GetReferencias(pFiltro);

            return Ok(objList);
        }


        [HttpGet("ObtieneReferenciaALOV/{idALO:int}", Name = "ObtieneReferenciaALO")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetReferenciaALO(int idALO)
        {
            #region Variables
            var lstServiciosDto = new List<PeticionesReferenciasDTO>();
            #endregion Variables

            var lstServicios = _ctRepovacios.GetReferencia(idALO);

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
            if (lstServicios != null)
            {

                //foreach (var lista in lstServicios)
                //{
                lstServiciosDto.Add(_mapper.Map<PeticionesReferenciasDTO>(lstServicios)); //se utiliza mapper para 
                return Ok(lstServiciosDto);                                                                //}
            }
            else
                return StatusCode(StatusCodes.Status404NotFound);

        }


        [HttpPatch("ActivarContenedorV/{IdReferencia:int}/{IdContenedor:int}", Name = "ActivarContenedorV")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActivarContenedor(int IdReferencia, int IdContenedor)
        {
            #region Variables
            PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.IsSuccess = true;
            objRespuesta.StatusCode = HttpStatusCode.OK;
            objRespuesta.ErrorMessages = new List<string>();
            #endregion Variables

            if (!_ctRepovacios.ExisteReferencia(IdReferencia) || (!_ctRepovacios.ExisteContenedor(IdReferencia, IdContenedor)))
            {

                return NotFound();
            }

            var referencia = _ctRepovacios.GetContenedor(IdReferencia, IdContenedor);
            var resultado = _ctRepovacios.BorrarActivarContenedor(IdReferencia, IdContenedor, true);
            if (!resultado || referencia == null)
            {
                objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                objRespuesta.IsSuccess = false;
                objRespuesta.ErrorMessages = new List<string>();
                objRespuesta.ErrorMessages.Add($"Error en Activar Contenedor: IdReferencia: {IdReferencia} IdContenedor: {IdContenedor}");
                return StatusCode(500, objRespuesta);
            }
            return Ok(objRespuesta);
        }

        [HttpPatch("ActivarServicioV/{IdReferencia:int}/{IdContenedor:int}/{IdServicio:int}", Name = "ActivarServicioV")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActivarServicio(int IdReferencia, int IdContenedor, int IdServicio)
        {
            #region Variables

            PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.IsSuccess = true;
            objRespuesta.StatusCode = HttpStatusCode.OK;
            objRespuesta.ErrorMessages = new List<string>();
            #endregion Variables

            if (!_ctRepovacios.ExisteReferencia(IdReferencia) || !_ctRepovacios.ExisteContenedor(IdReferencia, IdContenedor))
            {
                return NotFound();
            }

            var contenedor = _ctRepovacios.GetServicioContenedor(IdReferencia, IdContenedor, IdServicio);
            var resultado = _ctRepovacios.BorrarActivarServicioContenedor(IdReferencia, IdContenedor, IdServicio, true);
            if (!resultado || contenedor == null)
            {
                objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                objRespuesta.IsSuccess = false;
                objRespuesta.ErrorMessages = new List<string>();
                objRespuesta.ErrorMessages.Add($"Error en Activar Servicio: IdReferencia: {IdReferencia} IdContenedor: {IdContenedor} IdServicio {IdServicio}");
                return StatusCode(500, objRespuesta);
            }
            return Ok(objRespuesta);
        }

        [HttpPatch("ActivarReferenciaV/{IdReferencia:int}", Name = "ActivarReferenciaV")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActivarReferencia(int IdReferencia)
        {

            #region Variables

            PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.IsSuccess = true;
            objRespuesta.StatusCode = HttpStatusCode.OK;
            objRespuesta.ErrorMessages = new List<string>();
            #endregion Variables

            if (!_ctRepovacios.ExisteReferencia(IdReferencia))
            {
                return NotFound();
            }

            var referencia = _ctRepovacios.GetReferencia(IdReferencia);
            var resultado = _ctRepovacios.BorrarActivarReferencia(IdReferencia, true);
            if (!resultado || referencia == null)
            {
                objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                objRespuesta.IsSuccess = false;
                objRespuesta.ErrorMessages = new List<string>();
                objRespuesta.ErrorMessages.Add($"Error en Activar Referencia: IdReferencia: {IdReferencia}");
                return StatusCode(500, objRespuesta);
            }
            return Ok(objRespuesta);
        }


        [HttpDelete("BajaContenedorV/{IdReferencia:int}/{IdContenedor:int}", Name = "BajaContenedorV")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BajaContenedor(int IdReferencia, int IdContenedor)
        {
            #region Variables

            PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.IsSuccess = true;
            objRespuesta.StatusCode = HttpStatusCode.OK;
            objRespuesta.ErrorMessages = new List<string>();
            #endregion Variables

            if (!_ctRepovacios.ExisteReferencia(IdReferencia) || (!_ctRepovacios.ExisteContenedor(IdReferencia, IdContenedor)))
            {
                return NotFound();
            }

            var referencia = _ctRepovacios.GetContenedor(IdReferencia, IdContenedor);
            var resultado = _ctRepovacios.BorrarActivarContenedor(IdReferencia, IdContenedor, false);
            if (!resultado || referencia == null)
            {
                objRespuesta.StatusCode = HttpStatusCode.NotFound;
                objRespuesta.IsSuccess = false;
                objRespuesta.ErrorMessages = new List<string>();
                objRespuesta.ErrorMessages.Add($"Error en Baja de Contenedor: IdReferencia: {IdReferencia} IdContenedor: {IdContenedor}");
                return StatusCode(500, objRespuesta);

            }

            return Ok(objRespuesta);
        }

        [HttpDelete("BajaServicioV/{IdReferencia:int}/{IdContenedor:int}/{IdServicio:int}", Name = "BajaServicioV")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BajaServicio(int IdReferencia, int IdContenedor, int IdServicio)
        {

            #region Variables
            //PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO { StatusCode= HttpStatusCode.InternalServerError};
            PeticionesRespuestaDTO objRespuesta = new PeticionesRespuestaDTO();
            objRespuesta.IsSuccess = true;
            objRespuesta.StatusCode = HttpStatusCode.OK;
            objRespuesta.ErrorMessages = new List<string>();
            #endregion Variables

            if (!_ctRepovacios.ExisteReferencia(IdReferencia) || !_ctRepovacios.ExisteContenedor(IdReferencia, IdContenedor))
            {
                return NotFound();
            }

            var contenedor = _ctRepovacios.GetServicioContenedor(IdReferencia, IdContenedor, IdServicio);
            var resultado = _ctRepovacios.BorrarActivarServicioContenedor(IdReferencia, IdContenedor, IdServicio, false);
            if (!resultado || contenedor == null)
            {
                objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                objRespuesta.IsSuccess = false;
                objRespuesta.ErrorMessages = new List<string>();
                objRespuesta.ErrorMessages.Add($"Error en Baja de Servicio: IdReferencia: {IdReferencia} IdContenedor: {IdContenedor} IdSevicio: {IdServicio}");
                return StatusCode(500, objRespuesta);
            }
            return Ok(objRespuesta);
        }

        [HttpDelete("BajaReferencia/{IdReferencia:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BajaReferencia(int IdReferencia)
        {
            if (!_ctRepovacios.ExisteReferencia(IdReferencia))
            {
                return NotFound();
            }

            var referencia = _ctRepovacios.GetReferencia(IdReferencia);
            var resultado = _ctRepovacios.BorrarActivarReferencia(IdReferencia, false);
            if (!resultado || referencia == null)
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro{referencia.IdReferencia}");
                return StatusCode(500, ModelState);
            }
            return Ok(true);
        }

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
    }
}
