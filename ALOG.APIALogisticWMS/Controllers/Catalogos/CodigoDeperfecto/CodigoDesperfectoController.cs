using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CodigoDesperfectoController : ControllerBase
{
    private readonly ICodigoDesperfectoRepositorio _ctCodigoDesperfecto;

    public CodigoDesperfectoController(ICodigoDesperfectoRepositorio ctCodigoDesperfecto)
    {
        _ctCodigoDesperfecto = ctCodigoDesperfecto;
    }

    [HttpGet("ObtenerPorDescripcionContiene/{descripcion}", Name = "ObtenerPorCodigoDesperfectoDescripcionContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorCodigoDesperfectoDescripcionContiene(string descripcion)
    {
        ResultBase<List<CodigoDesperfecto>> resultBase = _ctCodigoDesperfecto.ObtenerPorDescripcionContains(descripcion);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }
}
