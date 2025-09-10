using ALOG.Modelos.Modelos.DTO.Vacios;
using System.Web;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Repositorio.Provision.IProvision;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ALOG.Modelos.Modelos.DTO.Provision;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using Microsoft.EntityFrameworkCore;
using ALOG.Modelos;

namespace ALOG.APIFacturacion.Controllers
{
    [Authorize(Roles = "ADMIN,SISTEMA,ADMINUSER,USUARIO")]
    //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvisionesController : ControllerBase
    {
        private readonly IProvisionesEncRepositorio _ctRepoProvisionesEnc;
        //private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ProvisionesController(IProvisionesEncRepositorio ctRepoProvisionesEnc, IMapper mapper, ApplicationDbContext context)
        {
            _ctRepoProvisionesEnc = ctRepoProvisionesEnc;
            //_mapper = mapper;
            _context = context;
        }

        [HttpPost("ConsultaProvisionUUID")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProvisionUUID([FromBody] List<string> pUUIDs)
        {
            #region DECLARACIÓN DE VARIABLES
            var lstprovisionEncDTO = new List<ProvisionEncDTO>();
            var _respuestaGenericaDTO = new RespuestaGenericaDTO();
            #endregion DECLARACIÓN DE VARIABLES

            #region ASIGNACIÓN DE VALORES A VARIABLES
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
            #endregion ASIGNACIÓN DE VALORES A VARIABLES

            if (pUUIDs == null || !pUUIDs.Any())
            {
                _respuestaGenericaDTO.strMensaje = "La lista de UUIDs no debe estar vacía.";
                _respuestaGenericaDTO.lstrErrorMessages.Add("La lista de UUIDs no debe estar vacía.");
                return BadRequest(_respuestaGenericaDTO);
            }

            try
            {
                var provisiones = _ctRepoProvisionesEnc.obtenerProvisionPorUUID(pUUIDs);

                if (provisiones == null || !provisiones.Any())
                {
                    _respuestaGenericaDTO.strMensaje = "No se encontraron provisiones para los UUIDs proporcionados.";
                    _respuestaGenericaDTO.lstrErrorMessages.Add("No se encontraron provisiones para los UUIDs proporcionados.");
                    _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_respuestaGenericaDTO);
                }
                else
                {
                    if (provisiones.Count() < pUUIDs.Count())
                    {

                        var provisionesUUIDs = provisiones.Select(x => x.UUID).ToList();
                        var UUIDsNoEncontrados = pUUIDs.Except(provisionesUUIDs).ToList();
                        _respuestaGenericaDTO.lstrErrorMessages = new List<string>();
                        _respuestaGenericaDTO.strMensaje = "No se encontraron provisiones para los siguientes UUIDs: " + string.Join(", ", UUIDsNoEncontrados);
                        _respuestaGenericaDTO.lstrErrorMessages.AddRange(UUIDsNoEncontrados);
                    }

                    foreach (var item in provisiones)
                    {
                        var provisionEncDTO = new ProvisionEncDTO();

                        provisionEncDTO.TipoCambio = item.TipoCambio;
                        provisionEncDTO.FechaTimbrado = item.FechaTimbrado;
                        provisionEncDTO.IdProvisionesEnc = item.IdProvisionesEnc;
                        provisionEncDTO.Folio = item.Folio;
                        provisionEncDTO.Importe = item.Importe;
                        provisionEncDTO.ImporteTotal = item.ImporteTotal;
                        provisionEncDTO.tipoMoneda = item.tipoMoneda.Descripcion;
                        provisionEncDTO.UUID = item.UUID;

                        if (item.provisionesDet != null)
                        {
                            provisionEncDTO.provisionesDet = new List<ProvisionDetDTO>();

                            foreach (var itemDet in item.provisionesDet)
                            {
                                var provisionDetDTO = new ProvisionDetDTO();

                                provisionDetDTO.IdProvisionesDet = itemDet.IdProvisionesDet;
                                provisionDetDTO.Cantidad = itemDet.Cantidad;
                                provisionDetDTO.Partida = itemDet.Partida;
                                provisionDetDTO.ReferenciaFacturaALO = itemDet.IntegracionesFacturaDet.FirstOrDefault()?.integracionFacturaEnc.Referencia ?? string.Empty;
                                provisionDetDTO.Cantidad = itemDet.Cantidad;
                                provisionDetDTO.TotalPartida = itemDet.TotalPartida;
                                provisionDetDTO.ClaveServicioExt = itemDet.Servicio;
                                provisionDetDTO.Contenedor = itemDet.peticionesContenedor?.Contenedor?? string.Empty;
                                provisionDetDTO.Costo = itemDet.Costo;
                                provisionDetDTO.Observaciones = itemDet.Observaciones;
                                provisionDetDTO.IdProvisionesDet = itemDet.IdProvisionesDet;
                                provisionDetDTO.IdProvisionesEnc = itemDet.IdProvisionesEnc;
                                provisionDetDTO.Impuesto = itemDet.Impuesto;

                                provisionEncDTO.provisionesDet.Add(provisionDetDTO);
                            }
                        }

                        lstprovisionEncDTO.Add(provisionEncDTO);
                    }
                }

                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                _respuestaGenericaDTO.Entidades = lstprovisionEncDTO.Cast<object>().ToList();
                return Ok(_respuestaGenericaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpPost("ConsultaProvisionUUIDPart")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProvisionUUIDPart([FromBody] List<ProvisionSolUUIDPartDTO> pUUIDs)
        {
            #region DECLARACIÓN DE VARIABLES
            var _respuestaGenericaDTO = new RespuestaGenericaDTO();
            var lstprovisionEncDTO = new List<ProvisionEncDTO>();
            var lstOutError = new List<string>();
            #endregion DECLARACIÓN DE VARIABLES

            //ProvisionEncDTO provisionEncDTO = new ProvisionEncDTO();
            //ProvisionDetDTO provisionDetDTO = new ProvisionDetDTO();
            //List<ProvisionDetDTO> lstprovisionDetDTO = new List<ProvisionDetDTO>();

            #region ASIGNACIÓN DE VALORES A VARIABLES
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
            #endregion ASIGNACIÓN DE VALORES A VARIABLES

            if (pUUIDs == null || !pUUIDs.Any())
            {
                _respuestaGenericaDTO.strMensaje = "La lista de UUIDs no debe estar vacía.";
                _respuestaGenericaDTO.lstrErrorMessages.Add("La lista de UUIDs no debe estar vacía.");
                _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.NotFound;
                return BadRequest(_respuestaGenericaDTO);
            }

            try
            {
                var provisiones = _ctRepoProvisionesEnc.obtenerProvisionPorUUIDPart(pUUIDs, out lstOutError);

                if (provisiones == null || !provisiones.Any())
                {
                    _respuestaGenericaDTO.strMensaje = "No se encontraron provisiones para los UUIDs proporcionados.";
                    _respuestaGenericaDTO.lstrErrorMessages.Add("No se encontraron provisiones para los UUIDs proporcionados.");
                    _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_respuestaGenericaDTO);
                }
                else
                {
                    foreach (var item in provisiones)
                    {
                        var provisionEncDTO = new ProvisionEncDTO();
                        
                        provisionEncDTO.TipoCambio = item.TipoCambio;
                        provisionEncDTO.FechaTimbrado = item.FechaTimbrado;
                        provisionEncDTO.IdProvisionesEnc = item.IdProvisionesEnc;
                        provisionEncDTO.Folio = item.Folio;
                        provisionEncDTO.Importe = item.Importe;
                        provisionEncDTO.ImporteTotal = item.ImporteTotal;
                        provisionEncDTO.tipoMoneda = item.tipoMoneda.Descripcion;
                        provisionEncDTO.UUID = item.UUID;

                        if (item.provisionesDet != null)
                        {
                            provisionEncDTO.provisionesDet = new List<ProvisionDetDTO>();

                            foreach (var itemDet in item.provisionesDet)
                            {
                                var provisionDetDTO = new ProvisionDetDTO();

                                provisionDetDTO.IdProvisionesDet = itemDet.IdProvisionesDet;
                                provisionDetDTO.Cantidad = itemDet.Cantidad;
                                provisionDetDTO.Partida = itemDet.Partida;

                                //var objIntFactEnc = _context.integracionFacturaEnc.FirstOrDefault(x => x.IdPeticionesReferencia == itemDet.IdReferencia && x.integraFacturaDet.Any(d => d.peticionesReferencias.Contenedores.Any(c => c.Contenedor.Equals(itemDet.peticionesContenedor.Contenedor))));
                                //provisionDetDTO.ReferenciaFacturaALO = objIntFactEnc?.Referencia;
                                provisionDetDTO.ReferenciaFacturaALO = itemDet.peticionesReferencia.ordenes.ReferenciaALO;
                                provisionDetDTO.Cantidad = itemDet.Cantidad;
                                provisionDetDTO.TotalPartida = itemDet.TotalPartida;
                                //var objIntFactDet = _context.integracionFacturaDet.FirstOrDefault(f => f.IdPeticionesReferencia == itemDet.IdReferencia && f.IdPeticionesContenedor == itemDet.IdContenedor && f.IddIntFacturaEnc == objIntFactEnc.IdIntFacturaEnc);
                                //provisionDetDTO.ClaveServicioExt = objIntFactDet?.ClaveServicio;
                                provisionDetDTO.ClaveServicioExt = itemDet.Servicio;
                                provisionDetDTO.Contenedor = itemDet.peticionesContenedor.Contenedor;
                                provisionDetDTO.Costo = itemDet.Costo;
                                provisionDetDTO.Observaciones = itemDet.Observaciones;
                                provisionDetDTO.IdProvisionesDet = itemDet.IdProvisionesDet;
                                provisionDetDTO.IdProvisionesEnc = itemDet.IdProvisionesEnc;
                                provisionDetDTO.Impuesto = itemDet.Impuesto;

                                provisionEncDTO.provisionesDet.Add(provisionDetDTO);
                            }
                        }
                        lstprovisionEncDTO.Add(provisionEncDTO);
                    }
                }

                //var provisionesDTO = _mapper.Map<List<ProvisionEncDTO>>(provisiones);
                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                _respuestaGenericaDTO.Entidades = lstprovisionEncDTO.Cast<object>().ToList();
                if (lstOutError.Any())
                {
                    _respuestaGenericaDTO.strMensaje = "No se encontraron provisiones para algunos los UUIDs proporcionados.";
                    _respuestaGenericaDTO.lstrErrorMessages.AddRange(lstOutError);
                }
                return Ok(_respuestaGenericaDTO);

                //var provisionesDTO = _mapper.Map<List<ProvisionEncDTO>>(provisiones);
            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.IsSuccess = false;
                _respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                _respuestaGenericaDTO.strMensaje = $"Error interno del servidor: {ex.Message}";

                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaGenericaDTO);
            }
        }
    }
}
