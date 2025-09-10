using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UbicacionAlmacenController : ControllerBase
{
    private readonly IUbicacionAlmacenRepositorio _ctUbicacionAlmacen;

    public UbicacionAlmacenController(IUbicacionAlmacenRepositorio ctUbicacionAlmacen)
    {
        _ctUbicacionAlmacen = ctUbicacionAlmacen;
    }

    [HttpGet("ObtenerPorClave/{clave}", Name = "GetUbicacionPorClave")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUbicacionPorClave(string clave)
    {
        var _ubicacion = _ctUbicacionAlmacen.ObtenerPorClave(clave);

        if (_ubicacion != null)
        {
            return Ok(_ubicacion);
        }
        else
        {
            return BadRequest("No se pudo identificar la ubicación.");
        }

    }

    [HttpGet("ObtenerPorId/{IdUbicacion:int}", Name = "GetUbicacionPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUbicacionPorId(int IdUbicacion)
    {
        var _ubicacion = _ctUbicacionAlmacen.ObtenerPorId(IdUbicacion);

        if (_ubicacion != null)
        {
            return Ok(_ubicacion);
        }
        else
        {
            return BadRequest("No se pudo identificar la ubicación.");
        }

    }

    [HttpGet("ObtenerPorClaveContiene/{idZonaAlmacenaje:int}/{clave}", Name = "ObtenerPorClaveContiene")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorClaveContiene(int idZonaAlmacenaje, string clave)
    {
        ResultBase<List<MonitorUbicacionAlmacen>> resultBase = _ctUbicacionAlmacen.ObtenerPorClaveContains(idZonaAlmacenaje, clave);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }

    [HttpGet("ObtenerListaUbicacionPorIdAlmacenaje/{idZonaAlmacenaje:int}", Name = "ObtenerListaUbicacionPorIdAlmacenaje")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObtenerListaUbicacionPorIdAlmacenaje(int idZonaAlmacenaje)
    {
        ResultBase<List<MonitorUbicacionAlmacen>> resultBase = _ctUbicacionAlmacen.ObtenerListaUbicacionPorIdAlmacenaje(idZonaAlmacenaje);
        if (resultBase != null)
        {
            return Ok(resultBase);
        }
        else
            return BadRequest(StatusCodes.Status404NotFound);
    }

}
