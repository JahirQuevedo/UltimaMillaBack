using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PaqueteController : ControllerBase
{
    private readonly IPaqueteRepositorio _ctPaquete;

    public PaqueteController(IPaqueteRepositorio ctPaquete)
    {
        _ctPaquete = ctPaquete;
    }

    [HttpGet("ObtenerPorDescripcionContiene/{descripcion}", Name = "ObtenerPorPaqueteDescripcionContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorPaqueteDescripcionContiene(string descripcion)
    {
        ResultBase<List<Paquete>> resultBase = _ctPaquete.ObtenerPorDescripcionContains(descripcion);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }
}
