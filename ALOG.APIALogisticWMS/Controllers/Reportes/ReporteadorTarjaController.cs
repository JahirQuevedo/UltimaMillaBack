using ALOG.Modelos;
using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;

/// <summary>
/// ReporteadorController
/// </summary>
//[Authorize(Roles = "Admin,Externo")]
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReporteadorTarjaController : ControllerBase
{

    private readonly IConfiguration _appsettings;
    private readonly IReporteadorTarjaRepositorio _ctReporteadorTarjaRepositorio;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="_ctReporteadorTarjaRepositorio">Interfaz de tipo ITarjaRepositorio</param>
    /// <param name="appsettings">Interfaz de tipo IConfiguration</param>
    public ReporteadorTarjaController(IReporteadorTarjaRepositorio ctReporteadorTarjaRepositorio, IConfiguration appsettings)
    {
        _ctReporteadorTarjaRepositorio = ctReporteadorTarjaRepositorio;
        _appsettings = appsettings;
    }

    [HttpPost("ObtieneReporteTarjaPatioExterno", Name = "ObtieneReporteTarjaPatioExterno")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult ObtieneReporteTarjaPatioExterno(ConsultaTarjaReporteador consultaTarjaReporteador)
    {
        try
        {
            consultaTarjaReporteador.ArchivoBase = new ArchivoBase();
            consultaTarjaReporteador.ArchivoBase.RutaBase = _appsettings["ArchivoBase:LayoutTarjaRecepcionTransPatio"];

            ResultBase<DocumentoBase> resultBaseDocumentoBase = _ctReporteadorTarjaRepositorio.ObtieneReporteTarjaPatioExterno(consultaTarjaReporteador);

            return Ok(resultBaseDocumentoBase);
        }
        catch
        {
            throw;
        }
    }

}
