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
public class InventarioAlmacenController : ControllerBase
{

    private readonly ILogger<PartidaController> _logger;
    private readonly IConfiguration _configuration;

    private readonly IInventararioAlmacenRepositorio _ctInventarioAlmacen;

    public InventarioAlmacenController(IInventararioAlmacenRepositorio ctInventarioAlmacen)
    {
        _ctInventarioAlmacen = ctInventarioAlmacen;
    }

    [HttpPost("ObtenerListaPaginada", Name = "ObtenerListaPaginadaInventarioAlmacen")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObtenerListaPaginadaInventarioAlmacen(ConsultaCatalogoBase<ConsultaMonitorInventario> consulta)
    {
        PaginadoResult<MonitorInventarioAlmacen> paginadoResult = _ctInventarioAlmacen.ObtenerListaPaginada(consulta);

        return Ok(paginadoResult);
    }

    [HttpPost("ObtenerExistenciaMercanciaListaPaginada", Name = "ObtenerExistenciaMercanciaListaPaginada")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObtenerExistenciaMercanciaListaPaginada(ConsultaCatalogoBase<ConsultaMonitorInventario> consulta)
    {
        PaginadoResult<MonitorInventarioAlmacen> paginadoResult = _ctInventarioAlmacen.ObtenerExistenciaMercanciaListaPaginada(consulta);

        return Ok(paginadoResult);
    }

    [HttpPost("ObtenerMercanciaInventarioListaPaginada", Name = "ObtenerMercanciaInventarioListaPaginada")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObtenerMercanciaInventarioListaPaginada(ConsultaCatalogoBase<ConsultaMonitorInventario> consulta)
    {
        PaginadoResult<MonitorInventarioAlmacen> paginadoResult = _ctInventarioAlmacen.ObtenerMercanciaInventarioListaPaginada(consulta);

        return Ok(paginadoResult);
    }

}
