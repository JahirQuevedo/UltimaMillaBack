using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BarcoController : ControllerBase
{
    private readonly IBarcoRepositorio _ctBarcos;

    public BarcoController(IBarcoRepositorio ctBarcos)
    {
        _ctBarcos = ctBarcos;
    }

    [HttpPost("Guardar", Name = "GuardarBarco")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarBarco(MonitorBarco monitorBarco)
    {
        var resultBase = _ctBarcos.Guardar(monitorBarco);

        return Ok(resultBase);
    }

    [HttpGet("ObtenerPorNombreContiene/{nombre}", Name = "ObtenerPorNombreContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorNombreContiene(string nombre)
    {
        ResultBase<List<MonitorBarco>> resultBase = await _ctBarcos.ObtenerPorNombreContains(nombre);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }

    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaBarcos")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaBarcos(ConsultaCatalogoBase<ConsultaBarco> barco)
    {
        PaginadoResult<MonitorBarco> paginadoResult = _ctBarcos.ObtenerListaPaginada(barco);

        return Ok(paginadoResult);
    }

}
