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
public class FolioServicioController : ControllerBase
{
    private readonly IFolioServicioRepositorio _ctFolioServicio;

    public FolioServicioController(IFolioServicioRepositorio ctFolioServicio)
    {
        _ctFolioServicio = ctFolioServicio;
    }

    [HttpGet("ObtenerPorId/{IdFolioServicio:int}", Name = "GetFolioServicioPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetFolioServicioPorId(int IdFolioServicio)
    {
        var _referencia = _ctFolioServicio.ObtenerPorId(IdFolioServicio);

        if (_referencia != null)
        {
            return Ok(_referencia);
        }
        else
        {
            return BadRequest("No se pudo identificar la referencia.");
        }

    }

    [HttpPost("Guardar", Name = "GuardarFolioServicio")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarFolioServicio(MonitorFolioServicio monitorFolioServicio)
    {
        var resultBase = _ctFolioServicio.Guardar(monitorFolioServicio);

        return Ok(resultBase);
    }

    [HttpDelete("Confirmar/{idFolioServicio:int}", Name = "ConfirmarFolioServicioPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ConfirmarFolioServicioPorId(int idFolioServicio)
    {
        var resultBase = _ctFolioServicio.Confirmar(idFolioServicio);

        return Ok(resultBase);

    }

    [HttpDelete("Eliminar/{idFolioServicio:int}", Name = "EliminarFolioServicioPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EliminarFolioServicioPorId(int idFolioServicio)
    {
        var resultBase = _ctFolioServicio.Eliminar(idFolioServicio);

        return Ok(resultBase);

    }

    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaFolioServicios")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaFolioServicios(ConsultaCatalogoBase<ConsultaFolioServicio> folioServicio)
    {
        PaginadoResult<MonitorFolioServicio> paginadoResult = _ctFolioServicio.ObtenerListaPaginada(folioServicio);

        return Ok(paginadoResult);
    }

}
