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
public class PartidaController : ControllerBase
{

    private readonly ILogger<PartidaController> _logger;
    private readonly IConfiguration _configuration;

    private readonly IPartidaRepositorio _ctPartidas;

    public PartidaController(IPartidaRepositorio ctPartidas)
    {
        _ctPartidas = ctPartidas;
    }

    [HttpPost("Guardar", Name = "GuardarPartida")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarPartida(MonitorPartida monitorPartida)
    {
        var resultBase = _ctPartidas.Guardar(monitorPartida);

        return Ok(resultBase);
    }

    [HttpGet("ObtenerPorId/{IdPartida:int}", Name = "GetPartidaPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetPartidaPorId(int IdPartida)
    {
        var _partida = _ctPartidas.ObtenerPorId(IdPartida);

        if (_partida != null)
        {
            return Ok(_partida);
        }
        else
        {
            return BadRequest("No se pudo identificar la partida.");
        }

    }

    [HttpPost("ObtenerListaPaginadaPorIdTarja", Name = "GetListaPaginadaPartidasPorIdTarja")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaPartidasPorIdTarja(ConsultaCatalogoBase<ConsultaPartida> partida)
    {
        PaginadoResult<MonitorPartida> paginadoResult = _ctPartidas.ObtenerListaPaginadaPorIdTarja(partida);

        return Ok(paginadoResult);
    }

}
