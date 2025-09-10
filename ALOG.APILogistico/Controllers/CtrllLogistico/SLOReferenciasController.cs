using ALOG.Modelos.Modelos.DTO.Catalogos;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Logistica;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Peticiones;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.Logistica;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;
using System.Net;

namespace ALOG.APILogistico.Controllers.CtrllLogistico
{
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class SLOReferenciasController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPeticionesReferenciasSLORepo _ctRepoReferenciasSLO;
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();
        private readonly IGenericoRepositorio<SLOPeticionesReferencias> _genericRepository;
        public SLOReferenciasController(ApplicationDbContext db,
            IPeticionesReferenciasSLORepo ctRepoReferenciasSLO)
        {
            _db = db;
            _ctRepoReferenciasSLO = ctRepoReferenciasSLO;

            _respuestaGenericaDTO = new RespuestaGenericaDTO();
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
            _ctRepoReferenciasSLO = ctRepoReferenciasSLO;
        }

        //[HttpGet("ObtenerListaReferencias")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> ObtenerListaReferencias(
        //[FromQuery] int? idCliente,
        //[FromQuery] int? idClienteFacturarA,
        //[FromQuery] string? referenciaALO,
        //[FromQuery] string? referenciaCliente,
        //[FromQuery] int? idAduana,
        //[FromQuery] DateTime? fechaRegistro,
        //[FromQuery] string? estado1G,
        //[FromQuery] DateTime? fechaEnvio)
        //{
        //    var filtro = new FiltroOrdenesReferenciasDTO
        //    {
        //        IdCliente = idCliente,
        //        IdClienteFact = idClienteFacturarA,
        //        ReferenciaALO = referenciaALO,
        //        ReferenciaCliente = referenciaCliente,
        //        IdAduana = idAduana,
        //        FechaRegistro = fechaRegistro,
        //        Estado1G = estado1G,
        //        FechaEnvio1G = fechaEnvio
        //    };

        //    var result = await _ctRepoReferenciasSLO.obtenerReferencias(filtro);
        //    return Ok(result);
        //}

        [HttpGet("ObtenerListaReferencias")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerListaReferencias([FromQuery] FiltroOrdenesReferenciasDTO filtro)
        {
            var result = await _ctRepoReferenciasSLO.obtenerReferencias(filtro);
            return Ok(result);
        }

        [HttpGet("ObtenerFacturas/{idOrden}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerFacturasSLO(int idOrden)
        {
            try
            {
                var facturas = await _ctRepoReferenciasSLO.ObtenerFacturasPorOrden(idOrden);
                if (!facturas.Any())
                    return NotFound(new { mensaje = $"No hay facturas para la orden {idOrden}" });

                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener facturas: {ex.Message}" });
            }
        }

        [HttpGet("ObtenerServicios/{IdOrden}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerServicios(int idOrden)
        {
            var contenedores = await _ctRepoReferenciasSLO.obtenerServicios(idOrden);

            if (contenedores == null)
            {
                return NotFound(new { mensaje = "No se encontraron servicios ligados a esta orden." });
            }

            return Ok(contenedores);
        }

        [HttpPost("ActualizarReferenciaCliente")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarReferenciaCliente([FromBody] Ordenes orden)
        {
            try
            {
                var result = await _ctRepoReferenciasSLO.ActualizarReferenciaCliente(orden);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }

        [HttpPost("CrearReferenciaALO")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearReferenciaALO([FromBody] SLOReferenciasDTO datos)
        {
            try
            {
                var result = await _ctRepoReferenciasSLO.crearReferencia(datos);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }

        [HttpPost("GuardarServicio")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GuardarFacturacionCompleta([FromBody] List<SLOPeticionesContenedores> datos)
        {
            if (datos == null || !datos.Any())
            {
                return BadRequest(new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    strMensaje = "No se recibió información para procesar."
                });
            }

            var respuesta = await _ctRepoReferenciasSLO.GuardarFacturacionCompleta(datos);

            if (!respuesta.IsSuccess)
            {
                return BadRequest(respuesta);
            }

            return Ok(respuesta);
        }

        [HttpPost("EditarServicio")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RespuestaGenericaDTO>> ActualizarServicio([FromBody] SLOPeticionesServicios servicio)
        {
            if (servicio == null || servicio.IdServicio <= 0)
            {
                return BadRequest("El objeto servicio es inválido.");
            }

            var resultado = await _ctRepoReferenciasSLO.ActualizarServicioAsync(servicio);
            return Ok(resultado);
        }

        [HttpPost("EliminarServicio/{idServicio}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RespuestaGenericaDTO>> EliminarServicio(int idServicio)
        {
            if (idServicio <= 0)
            {
                return BadRequest("ID inválido.");
            }

            var resultado = await _ctRepoReferenciasSLO.EliminarServicioAsync(idServicio);
            return Ok(resultado);
        }

        [HttpPost("EnviarAFacturar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> EnvioFactura([FromBody] Ordenes datos)
        {
            if (datos == null)
            {
                return BadRequest(new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    strMensaje = "No se recibió información para procesar."
                });
            }

            var respuesta = await _ctRepoReferenciasSLO.envioFacturacion(datos);

            if (!respuesta.IsSuccess)
            {
                return BadRequest(respuesta);
            }

            return Ok(respuesta);
        }
    }
}
