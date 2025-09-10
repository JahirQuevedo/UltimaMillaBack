using ALOG.Enums;
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
public class ServicioFotograficoController : ControllerBase
{

    private readonly ILogger<TarjaController> _logger;
    private readonly IConfiguration _appsettings;
    private readonly IServicioFotograficoRepositorio _ctServicioFotografico;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ctServicioFotografico">Interfaz de tipo IServicioFotograficoRepositorio</param>
    /// <param name="appsettings">Interfaz de tipo IConfiguration</param>
    public ServicioFotograficoController(IServicioFotograficoRepositorio ctServicioFotografico, IConfiguration appsettings)
    {
        _ctServicioFotografico = ctServicioFotografico;
        _appsettings = appsettings;
    }

    [HttpPost("Guardar", Name = "GuardarArchivoFotografico")]
    public async Task<JsonResult> GuardarArchivoFotografico(CargaArchivoFotografico cargaArchivoFotografico)
    {
        try
        {
            var resultBase = new ResultBase();

            cargaArchivoFotografico.ArchivoBase.RutaBase = String.IsNullOrEmpty(cargaArchivoFotografico.ArchivoBase.RutaBase) ?
             _appsettings["ArchivoBase:RutaArchivoFotos"] : (_appsettings["ArchivoBase:RutaArchivoFotos"] + cargaArchivoFotografico.ArchivoBase.RutaBase);

            resultBase = _ctServicioFotografico.Guardar(cargaArchivoFotografico);

            return new JsonResult(resultBase);
        }
        catch
        {

            throw;
        }
    }


    [HttpDelete("Eliminar/{idServicioFotografico:int}", Name = "EliminarServicioFotograficoPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EliminarServicioFotograficoPorId(int idServicioFotografico)
    {
        var resultBase = _ctServicioFotografico.Eliminar(idServicioFotografico);

        return Ok(resultBase);

    }

    /// <summary>
    /// Método para obtener servicio fotográfico por su Identificador (Id)
    /// </summary>
    /// <param name="IdTarja">Identificador de la Servicio Fotográfico</param>
    /// <returns>Información de la Servicio Fotográfico</returns>
    [HttpGet("ObtenerPorId/{idServicioFotografico:int}", Name = "GetServicioFotograficoPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetServicioFotograficoPorId(int idServicioFotografico)
    {
        var _servicioFotografico = _ctServicioFotografico.ObtenerPorId(idServicioFotografico);

        if (_servicioFotografico != null)
        {
            return Ok(_servicioFotografico);
        }
        else
        {
            return BadRequest("No se pudo identificar la servicio fotográfico.");
        }

    }

    /// <summary>
    /// Listado paginado de Fotografias 
    /// </summary>
    /// <param name="tarja">Entity ConsultaServicioFotografico con filtros para la consulta</param>
    /// <returns>Paginado de información</returns>
    [HttpPost("ObtenerListaPaginada", Name = "GetListaPaginadaServicioFotografico")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetListaPaginadaServicioFotografico(ConsultaCatalogoBase<ConsultaServicioFotografico> entidad)
    {
        PaginadoResult<MonitorServicioFotografico> paginadoResult = _ctServicioFotografico.ObtenerListaPaginada(entidad);

        return Ok(paginadoResult);
    }

    [HttpPost]
    [HttpPost("ObtenerZipArchivos", Name = "GetZipArchivos")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetZipArchivos(MultipleSeleccionArchivoBase multipleSeleccionArchivoBase)
    {
        try
        {

            //multipleSeleccionArchivoBase.ArchivoBase = new ArchivoBase();
            multipleSeleccionArchivoBase.TipoProcesoArchivoControlDocumento = TipoProcesoArchivoControlDocumento.ExpedienteRecepcion;
            multipleSeleccionArchivoBase.ArchivoBase.RutaBase = String.IsNullOrEmpty(multipleSeleccionArchivoBase.ArchivoBase.RutaBase) ?
             _appsettings["ArchivoBase:RutaArchivoFotos"] : (_appsettings["ArchivoBase:RutaArchivoFotos"] + multipleSeleccionArchivoBase.ArchivoBase.RutaBase);

            ResultBase<ArchivoBase> resultBaseArchivoBaseZip = _ctServicioFotografico.ObtenerZipArchivos(multipleSeleccionArchivoBase);

            if (resultBaseArchivoBaseZip == null)
            {
                return NotFound();
            }

            return Ok(resultBaseArchivoBaseZip);
        }
        catch
        {
            throw;
        }
    }

}
