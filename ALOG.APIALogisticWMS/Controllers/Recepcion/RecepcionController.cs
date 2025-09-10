using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RecepcionController : ControllerBase
{
    private readonly IRecepcionRepositorio _ctRecepcion;

    public RecepcionController(IRecepcionRepositorio ctRecepcion)
    {
        _ctRecepcion = ctRecepcion;
    }

    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaRecepcion")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaRecepcion(ConsultaCatalogoBase<ConsultaRecepcion> consulta)
    {
        PaginadoResult<MonitorRecepcionMercancia> paginadoResult = _ctRecepcion.ObtenerListaPaginada(consulta);

        return Ok(paginadoResult);
    }

    [HttpGet("ConfirmarRecepcionPorIdPartidaInventario/{idPartidaInventario:int}/{tieneAveria}", Name = "ConfirmarRecepcionPorIdPartidaInventario")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ConfirmarRecepcionPorIdPartidaInventario(int idPartidaInventario, int tieneAveria)
    {
        var _result = _ctRecepcion.ConfirmarRecepcionMercanciaPorIdPartidaInventario(idPartidaInventario, tieneAveria);

        if (_result != null)
        {
            return Ok(_result);
        }
        else
        {
            return BadRequest("No se pudo identificar la operación.");
        }

    }

    [HttpPost("ConfirmarRecepcionPorIdTarja", Name = "ConfirmarRecepcionPorIdTarja")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ConfirmarRecepcionPorIdTarja(ConsultaCatalogoBase<ConsultaRecepcion> consulta)
    {
        PaginadoResult<MonitorRecepcionMercancia> paginadoResult = _ctRecepcion.ConfirmarRecepcionMercanciaPorIdTarja(consulta);

        return Ok(paginadoResult);
    }

    [HttpPost("FinalizarRecepcion", Name = "FinalizarRecepcion")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult FinallizarRecepcionMercancia(MonitorRecepcionMercancia monitorRecepcionMercancia)
    {
        ResultBase<MonitorRecepcionMercancia> paginadoResult = _ctRecepcion.FinalizarRecepcionMercancia(monitorRecepcionMercancia);

        return Ok(paginadoResult);
    }


}
