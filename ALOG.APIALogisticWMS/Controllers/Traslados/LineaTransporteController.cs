using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LineaTransporteController : ControllerBase
{
    private readonly ILineaTransportistaRepositorio _ctLineaTransportista;

    public LineaTransporteController(ILineaTransportistaRepositorio ctLineaTransportista)
    {
        _ctLineaTransportista = ctLineaTransportista;
    }

    [HttpGet("ObtenerPorRazonSocialContiene/{razonSocial}", Name = "ObtenerPorRazonSocialContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorRazonSocialContiene(string razonSocial)
    {
        ResultBase<List<MonitorLineaTransporte>> resultBase = _ctLineaTransportista.ObtenerPorRazonSocialContains(razonSocial);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }

    [HttpGet("ObtenerPorPlacasContiene/{idCatTransportista}/{placas}", Name = "ObtenerPorPlacasContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorPlacasContiene(int idCatTransportista, string placas)
    {
        ResultBase<List<MonitorLineaTransporte>> resultBase = _ctLineaTransportista.ObtenerPorPlacasContains(idCatTransportista, placas);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }


}
