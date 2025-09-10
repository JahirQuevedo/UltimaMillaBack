using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ViajeController : ControllerBase
{
    private readonly IViajeRepositorio _ctViajes;

    public ViajeController(IViajeRepositorio ctViajes)
    {
        _ctViajes = ctViajes;
    }

    [HttpPost("Guardar", Name = "Guardar")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Guardar(MonitorViaje monitorViaje)
    {
        var resultBase = _ctViajes.Guardar(monitorViaje);

        return Ok(resultBase);
    }

    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaViajes")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaViajes(ConsultaCatalogoBase<ConsultaViaje> viaje)
    {
        PaginadoResult<MonitorViaje> paginadoResult = _ctViajes.ObtenerListaPaginada(viaje);

        return Ok(paginadoResult);
    }

    [HttpGet("ObtenerPorReferenciaBuqueViajeContiene/{referenciaBuqueViaje}", Name = "ObtenerPorReferenciaBuqueViajeContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorReferenciaBuqueViajeContiene(string referenciaBuqueViaje)
    {
        ResultBase<List<MonitorViaje>> resultBase = _ctViajes.ObtenerPorReferenciaBuqueViajeContains(referenciaBuqueViaje);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }
}

