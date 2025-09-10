using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReferenciaBookingBLController : ControllerBase
{

    private readonly IReferenciaBookingBLRepositorio _ctReferenciaBookingBL;

    public ReferenciaBookingBLController(IReferenciaBookingBLRepositorio ctReferenciaBookingBL)
    {
        _ctReferenciaBookingBL = ctReferenciaBookingBL;
    }

    [HttpPost("Guardar", Name = "GuardarReferenciaBookingBL")]
    public async Task<JsonResult> GuardarReferenciaBookingBL(MonitorReferenciaBookingBL monitorReferenciaBookingBL)
    {
        try
        {
            var resultBase = new ResultBase();

            resultBase = _ctReferenciaBookingBL.Guardar(monitorReferenciaBookingBL);

            return new JsonResult(resultBase);
        }
        catch
        {

            throw;
        }
    }


    [HttpDelete("Eliminar/{idReferenciaBookingBL:int}", Name = "EliminarReferenciaBookingBL")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EliminarReferenciaBookingBL(int idReferenciaBookingBL)
    {
        var resultBase = _ctReferenciaBookingBL.Eliminar(idReferenciaBookingBL);

        return Ok(resultBase);

    }

    /// <summary>
    /// Método para obtener servicio fotográfico por su Identificador (Id)
    /// </summary>
    /// <param name="IdTarja">Identificador de la Servicio Fotográfico</param>
    /// <returns>Información de la Servicio Fotográfico</returns>
    [HttpGet("ObtenerPorId/{idServicioFotografico:int}", Name = "GetReferenciaBookingBLPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetReferenciaBookingBLPorId(int idReferenciaBookingBL)
    {
        var _referenciaBookigBL = _ctReferenciaBookingBL.ObtenerPorId(idReferenciaBookingBL);

        if (_referenciaBookigBL != null)
        {
            return Ok(_referenciaBookigBL);
        }
        else
        {
            return BadRequest("No se pudo identificar la Referencia Booking BL.");
        }

    }

}
