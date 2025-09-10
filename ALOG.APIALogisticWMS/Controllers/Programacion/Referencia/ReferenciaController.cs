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
public class ReferenciaController : ControllerBase
{
    private readonly IReferenciaRepositorio _ctReferencias;

    public ReferenciaController(IReferenciaRepositorio ctReferencias)
    {
        _ctReferencias = ctReferencias;
    }

    [HttpPost("Guardar", Name = "GuardarReferencia")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarReferencia(MonitorReferencia monitorReferencia)
    {
        var resultBase = _ctReferencias.Guardar(monitorReferencia);

        return Ok(resultBase);
    }

    [HttpGet("ObtenerPorId/{IdReferencia:int}", Name = "GetReferenciaPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetReferenciaPorId(int IdReferencia)
    {
        var _referencia = _ctReferencias.ObtenerReferenciaPorId(IdReferencia);

        if (_referencia != null)
        {
            return Ok(_referencia);
        }
        else
        {
            return BadRequest("No se pudo identificar la referencia.");
        }

    }

    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaReferencias")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaReferencias(ConsultaCatalogoBase<ConsultaReferencia> referencia)
    {
        PaginadoResult<MonitorReferencia> paginadoResult = _ctReferencias.ObtenerListaPaginada(referencia);

        return Ok(paginadoResult);
    }

    [HttpDelete("Eliminar/{idReferencia:int}", Name = "EliminarReferenciaPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EliminarReferenciaPorId(int idReferencia)
    {
        var resultBase = _ctReferencias.Eliminar(idReferencia);

        return Ok(resultBase);

    }

}
