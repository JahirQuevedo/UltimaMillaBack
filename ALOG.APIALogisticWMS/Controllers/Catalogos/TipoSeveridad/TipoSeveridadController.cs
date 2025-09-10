using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
public class TipoSeveridadController : ControllerBase
{
    private readonly ITipoSeveridadRepositorio _ctTipoSeveridad;

    public TipoSeveridadController(ITipoSeveridadRepositorio ctTipoSeveridad)
    {
        _ctTipoSeveridad = ctTipoSeveridad;
    }

    [HttpGet("ObtenerPorDescripcionContiene/{descripcion}", Name = "ObtenerPorTipoSeveridadDescripcionContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorTipoSeveridadDescripcionContiene(string descripcion)
    {
        ResultBase<List<TipoSeveridad>> resultBase = _ctTipoSeveridad.ObtenerPorDescripcionContains(descripcion);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }
}
