using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

//[Authorize(Roles = "Admin,Externo")]
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ZonaAmacenajeController : ControllerBase
{

    private readonly ILogger<ZonaAmacenajeController> _logger;
    private readonly IConfiguration _configuration;

    private readonly IZonaAlmacenRepositorio _ctZonaAlmacenes;

    public ZonaAmacenajeController(IZonaAlmacenRepositorio ctZonaAlmacenes)
    {
        _ctZonaAlmacenes = ctZonaAlmacenes;
    }

    [HttpGet("ObtenerLista", Name = "GetListaZonaAlmacenajes")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaZonaAlmacenajes()
    {
        ResultBase<List<MonitorZonaAlmacen>> paginadoResult = _ctZonaAlmacenes.ObtenerLista();

        return Ok(paginadoResult);
    }

}
