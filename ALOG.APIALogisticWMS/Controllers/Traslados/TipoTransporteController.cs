using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TipoTransporteController : ControllerBase
{
    private readonly ITipoTransporteRepositorio _cTipoTransporte;

    public TipoTransporteController(ITipoTransporteRepositorio cTipoTransporte)
    {
        _cTipoTransporte = cTipoTransporte;
    }

    [HttpGet("ObtenerPorNombreContiene/{nombre}", Name = "ObtenerPorNomnbreTipoTrnasporteContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorNomnbreTipoTrnasporteContiene(string nombre)
    {
        ResultBase<List<TipoTransporte>> resultBase = _cTipoTransporte.ObtenerPorNombreContains(nombre);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }

}
