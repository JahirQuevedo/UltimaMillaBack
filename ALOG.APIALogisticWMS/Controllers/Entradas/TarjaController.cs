using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

/// <summary>
/// TarjaController
/// </summary>
//[Authorize(Roles = "Admin,Externo")]
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TarjaController : ControllerBase
{

    private readonly ILogger<TarjaController> _logger;
    private readonly IConfiguration _appsettings;
    private readonly ITarjaRepositorio _ctTarjas;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ctTarjas">Interfaz de tipo ITarjaRepositorio</param>
    /// <param name="appsettings">Interfaz de tipo IConfiguration</param>
    public TarjaController(ITarjaRepositorio ctTarjas, IConfiguration appsettings)
    {
        _ctTarjas = ctTarjas;
        _appsettings = appsettings;
    }

    /// <summary>
    /// Registro y Actualizacion de Tarja
    /// </summary>
    /// <param name="monitorTarja">Entity MonitorTarja</param>
    /// <returns>Paginado de información</returns>

    [HttpPost("Guardar", Name = "GuardarTarja")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GuardarTarja(MonitorTarja monitorTarja)
    {
        var resultBase = _ctTarjas.Guardar(monitorTarja);

        return Ok(resultBase);
    }

    /// <summary>
    /// Método para obtener la Tarja por su Identificador (Id)
    /// </summary>
    /// <param name="IdTarja">Identificador de la Tarja</param>
    /// <returns>Información de la Tarja</returns>
    [HttpGet("ObtenerPorId/{IdTarja:int}", Name = "GetTarjaPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetTarjaPorId(int IdTarja)
    {
        var _tarja = _ctTarjas.ObtenerPorId(IdTarja);

        if (_tarja != null)
        {
            return Ok(_tarja);
        }
        else
        {
            return BadRequest("No se pudo identificar la tarja.");
        }

    }

    [HttpPost("GuardarCargaMasiva", Name = "GuardarCargaMasiva")]
    public async Task<JsonResult> GuardarCargaMasiva(CargaMasiva cargaMasiva)
    {
        try
        {
            var resultBase = new ResultBase();

            cargaMasiva.ArchivoBase.RutaBase = _appsettings["ArchivoBase:RutaBase"];

            resultBase = _ctTarjas.GuardarCargaMasiva(cargaMasiva);

            return new JsonResult(resultBase);
        }
        catch
        {

            throw;
        }
    }

    [HttpPost("Confirmar", Name = "ConfirmarTarja")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ConfirmarTarja(MonitorTarja monitorTarja)
    {
        var resultBase = _ctTarjas.Confirmar(monitorTarja);

        return Ok(resultBase);
    }

    /// <summary>
    /// Listado paginado de tarjas 
    /// </summary>
    /// <param name="tarja">Entity ConsultaTarja con filtros para la consulta</param>
    /// <returns>Paginado de información</returns>
    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaTarjas")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaTarjas(ConsultaCatalogoBase<ConsultaTarja> tarja)
    {
        PaginadoResult<MonitorTarja> paginadoResult = _ctTarjas.ObtenerListaPaginada(tarja);

        return Ok(paginadoResult);
    }


    [HttpPost("ObtenerListaPaginadaPorIdReferencia", Name = "GetListaPaginadaTarjasPorIdReferencia")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaTarjasPorIdReferencia(ConsultaCatalogoBase<ConsultaTarja> tarja)
    {
        PaginadoResult<MonitorTarja> paginadoResult = _ctTarjas.ObtenerListaPaginadaPorIdReferencia(tarja);

        return Ok(paginadoResult);
    }

    [HttpPost("ObtenerListaPaginadaTarjaInventarioPorIdReferencia", Name = "GetListaPaginadaTarjasInventarioPorIdReferencia")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaTarjasInventarioPorIdReferencia(ConsultaCatalogoBase<ConsultaTarja> tarja)
    {
        PaginadoResult<MonitorTarjaInventario> paginadoResult = _ctTarjas.ObtenerListaPaginadaTarjaInventarioPorIdReferencia(tarja);

        return Ok(paginadoResult);
    }

    [HttpDelete("Eliminar/{idTarja:int}", Name = "EliminarTarjaPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EliminarTarjaPorId(int idTarja)
    {
        var resultBase = _ctTarjas.Eliminar(idTarja);

        return Ok(resultBase);

    }


}
