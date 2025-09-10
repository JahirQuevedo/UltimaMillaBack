using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ControlTransporteController : ControllerBase
{
    private readonly IControlTransporteRepositorio _ctControlTransporte;

    public ControlTransporteController(IControlTransporteRepositorio ctControlTransporte)
    {
        _ctControlTransporte = ctControlTransporte;
    }

    [HttpPost("Guardar", Name = "GuardarControlTransporte")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarControlTransporte(MonitorCtrlTransporte monitorCtrlTransporte)
    {
        var resultBase = _ctControlTransporte.Guardar(monitorCtrlTransporte);

        return Ok(resultBase);
    }

    [HttpPost("GuardarRelacionSolicitudTraslado", Name = "GuardarRelacionSolicitudTraslado")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarRelacionSolicitudTraslado(MonitorCtrlTransporte monitorCtrlTransporte)
    {
        var resultBase = _ctControlTransporte.GuardarRelacionSolicitudTraslado(monitorCtrlTransporte);

        return Ok(resultBase);
    }

    [HttpGet("ObtenerPorId/{IdControlTransporte:int}", Name = "GetControlTransportePorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetControlTransportePorId(int IdControlTransporte)
    {
        var _referencia = _ctControlTransporte.ObtenerPorId(IdControlTransporte);

        if (_referencia != null)
        {
            return Ok(_referencia);
        }
        else
        {
            return BadRequest("No se pudo identificar el Control Transporte.");
        }

    }

    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaControlTransporte")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaControlTransporte(ConsultaCatalogoBase<ConsultaCtrlTransporte> consulta)
    {
        PaginadoResult<MonitorCtrlTransporte> paginadoResult = _ctControlTransporte.ObtenerListaPaginada(consulta);

        return Ok(paginadoResult);
    }

}
