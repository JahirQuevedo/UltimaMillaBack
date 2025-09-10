using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SolicitudTrasladoController : ControllerBase
{
    private readonly ISolicitudTrasladoRepositorio _ctSolicitudTraslado;

    public SolicitudTrasladoController(ISolicitudTrasladoRepositorio ctSolicitudTraslado)
    {
        _ctSolicitudTraslado = ctSolicitudTraslado;
    }

    [HttpPost("Guardar", Name = "GuardarSolicitudTraslado")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarSolicitudTraslado(MonitorSolicitudTraslado monitorSolicitudTraslado)
    {
        var resultBase = _ctSolicitudTraslado.Guardar(monitorSolicitudTraslado);

        return Ok(resultBase);
    }

    [HttpPost("CancelarSolicitudTraslado", Name = "CancelarSolicitudTraslado")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CancelarSolicitudTraslado(ConsultaSolicitudTraslado consultaSolicitudTraslado)
    {
        var resultBase = _ctSolicitudTraslado.CancelarSolicitudTraslado(consultaSolicitudTraslado);

        return Ok(resultBase);
    }

    [HttpPost("GuardarServicioSolicitudTraslado", Name = "GuardarServicioSolicitudTraslado")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarServicioSolicitudTraslado(MonitorSolicitudTraslado monitorSolicitudTraslado)
    {
        var resultBase = _ctSolicitudTraslado.GuardarServicioSolicitudTraslado(monitorSolicitudTraslado);

        return Ok(resultBase);
    }

    [HttpGet("ObtenerPorId/{IdSolicitudTraslado:int}", Name = "GetSolicitudTrasladoPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetSolicitudTrasladoPorId(int IdSolicitudTraslado)
    {
        var _referencia = _ctSolicitudTraslado.ObtenerPorId(IdSolicitudTraslado);

        if (_referencia != null)
        {
            return Ok(_referencia);
        }
        else
        {
            return BadRequest("No se pudo identificar el Control Transporte.");
        }

    }

    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaSolicitudTraslado")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaSolicitudTraslado(ConsultaCatalogoBase<ConsultaSolicitudTraslado> consulta)
    {
        PaginadoResult<MonitorSolicitudTraslado> paginadoResult = _ctSolicitudTraslado.ObtenerListaPaginada(consulta);

        return Ok(paginadoResult);
    }

}
