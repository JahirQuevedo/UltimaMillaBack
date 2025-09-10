using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ALOG.APIALogisticWMS;

/// <summary>
/// BitacoraAveriaController
/// </summary>
//[Authorize(Roles = "Admin,Externo")]
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BitacoraAveriaController : ControllerBase
{
    private readonly IBitacoraAveriaRepositorio _ctBitacoraAveria;

    public BitacoraAveriaController(IBitacoraAveriaRepositorio ctBitacoraAveria)
    {
        _ctBitacoraAveria = ctBitacoraAveria;
    }

    /// <summary>
    /// Registro y Actualizacion de Bitácora de Averia
    /// </summary>
    /// <param name="monitorBitacoraAveria">Entity MonitorBitacoraAveria</param>
    /// <returns>Información de la Bitácora de Averia</returns>

    [HttpPost("Guardar", Name = "GuardarBitacoraAveria")]
    public IActionResult GuardarBitacoraAveria(MonitorBitacoraAveria monitorBitacoraAveria)
    {
        var resultBase = _ctBitacoraAveria.Guardar(monitorBitacoraAveria);

        return Ok(resultBase);
    }

    [HttpDelete("Eliminar/{IdBitacoraAveria:int}", Name = "EliminarBitacoraAveriaPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EliminarBitacoraAveriaPorId(int IdBitacoraAveria)
    {
        var resultBase = _ctBitacoraAveria.Eliminar(IdBitacoraAveria);

        return Ok(resultBase);

    }

    /// <summary>
    /// Método para obtener la Bitácora de Averia por su Identificador (Id)
    /// </summary>
    /// <param name="IdBitacoraAveria">Identificador de la Bitácora de Averia</param>
    /// <returns>Información de la Bitácora de Avería</returns>
    [HttpGet("ObtenerPorId/{IdBitacoraAveria:int}", Name = "GetBitacoraAveriaPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetBitacoraAveriaPorId(int IdBitacoraAveria)
    {
        var _bitacoraAveria = _ctBitacoraAveria.ObtenerPorId(IdBitacoraAveria);

        if (_bitacoraAveria != null)
        {
            return Ok(_bitacoraAveria);
        }
        else
        {
            return BadRequest("No se pudo identificar la Bitácora de Avería.");
        }

    }

    [HttpPost("ObtenerListaPaginadaPorIdInventario", Name = "GetListaPaginadaBitacoraAveriaPorIdInventario")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaBitacoraAveriaPorIdInventario(ConsultaCatalogoBase<ConsultaBitacoraAveria> bitacoraAveria)
    {
        PaginadoResult<MonitorBitacoraAveria> paginadoResult = _ctBitacoraAveria.ObtenerListaPaginadaPorIdInventario(bitacoraAveria);

        return Ok(paginadoResult);
    }

}
