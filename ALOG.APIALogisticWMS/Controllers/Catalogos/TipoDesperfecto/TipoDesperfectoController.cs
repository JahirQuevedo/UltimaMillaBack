using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
public class TipoDesperfectoController : ControllerBase
{
    private readonly ITipoDesperfectoRepositorio _ctTipoDesperfecto;

    public TipoDesperfectoController(ITipoDesperfectoRepositorio ctTipoDesperfecto)
    {
        _ctTipoDesperfecto = ctTipoDesperfecto;
    }

    [HttpGet("ObtenerPorDescripcionContiene/{descripcion}", Name = "ObtenerPorTipoDesperfectoDescripcionContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorTipoDesperfectoDescripcionContiene(string descripcion)
    {
        ResultBase<List<TipoDesperfecto>> resultBase = _ctTipoDesperfecto.ObtenerPorDescripcionContains(descripcion);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }
}
