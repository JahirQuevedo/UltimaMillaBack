using ALOG.Modelos;
using ALOG.Enums;
using ALOG.Modelos.Modelos;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios;
using ALOG.Repositorios.Repositorio;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Web;
using ALOG.Modelos.Modelos.DTO.Control;
using ALOG.Modelos.ModelosWMS.Reportes.Salidas;

namespace ALOG.APIALogisticWMS;

/// <summary>
/// ReporteadorController
/// </summary>
//[Authorize(Roles = "Admin,Externo")]
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReporteadorLiberacionController : ControllerBase
{
    private readonly IConfiguration _appsettings;
    private readonly IReporteadorLiberacionRepositorio _ctReporteadorLiberacionRepositorio;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="_ctReporteadorLiberacionRepositorio">Interfaz de tipo IReporteadorLiberacionRepositorio</param>
    /// <param name="appsettings">Interfaz de tipo IConfiguration</param>
    public ReporteadorLiberacionController(IReporteadorLiberacionRepositorio ctReporteadorLiberacionRepositorio, IConfiguration appsettings)
    {
        _ctReporteadorLiberacionRepositorio = ctReporteadorLiberacionRepositorio;
        _appsettings = appsettings;
    }

    [HttpPost("ObtieneReporteTarjaSalidaLiberacion", Name = "ObtieneReporteTarjaSalidaLiberacion")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult ObtieneReporteTarjaSalidaLiberacion(ConsultaLiberacionReporteador consultaLiberacionReporteador)
    {
        try
        {
            consultaLiberacionReporteador.ArchivoBase = new ArchivoBase();
            consultaLiberacionReporteador.ArchivoBase.RutaBase = _appsettings["ArchivoBase:LayoutTarjaSalidaLiberacion"];

            ResultBase<DocumentoBase> resultBaseDocumentoBase = _ctReporteadorLiberacionRepositorio.ObtieneReporteTarjaSalidaLiberacion(consultaLiberacionReporteador);

            return Ok(resultBaseDocumentoBase);
        }
        catch
        {
            throw;
        }
    }
}

