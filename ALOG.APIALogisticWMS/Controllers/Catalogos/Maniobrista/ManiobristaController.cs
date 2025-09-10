using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ManiobristaController : ControllerBase
{
    private readonly IManiobristaRepositorio _ctManiobrista;

    public ManiobristaController(IManiobristaRepositorio ctManiobrista)
    {
        _ctManiobrista = ctManiobrista;
    }

    [HttpGet("ObtenerPorRazonSocialContiene/{razonSocial}", Name = "ObtenerPorManiobristaRazonSocialContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorManiobristaRazonSocialContiene(string razonSocial)
    {
        ResultBase<List<MonitorManiobrista>> resultBase = _ctManiobrista.ObtenerPorRazonSocialContains(razonSocial);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }

}
