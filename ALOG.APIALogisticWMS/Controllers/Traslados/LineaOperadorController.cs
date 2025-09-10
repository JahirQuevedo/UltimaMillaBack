using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LineaOperadorController : ControllerBase
{

    private readonly ILineaOperadorRepositorio _ctLineaOperador;

    public LineaOperadorController(ILineaOperadorRepositorio ctLineaOperador)
    {
        _ctLineaOperador = ctLineaOperador;
    }

    [HttpGet("ObtenerPorNombreContiene/{idCatTransportista}/{nombre}", Name = "ObtenerPorNombreOperadorContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorNombreOperadorContiene(int idCatTransportista, string nombre)
    {
        ResultBase<List<MonitorLineaOperador>> resultBase = _ctLineaOperador.ObtenerPorNombreContains(idCatTransportista, nombre);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }

}
