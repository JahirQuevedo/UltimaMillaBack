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

namespace ALOG.APIALogisticWMS;

/// <summary>
/// ReporteadorController
/// </summary>
//[Authorize(Roles = "Admin,Externo")]
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReporteadorInventarioController : ControllerBase
{
    private readonly IConfiguration _appsettings;
    private readonly IReporteadorInventarioRepositorio _ctReporteadorInventarioRepositorio;

    public ReporteadorInventarioController(IReporteadorInventarioRepositorio ctReporteadorInventarioRepositorio, IConfiguration appsettings)
    {
        _ctReporteadorInventarioRepositorio = ctReporteadorInventarioRepositorio;
        _appsettings = appsettings;
    }

    [HttpPost("ObtieneReporteDescargaInventario", Name = "ObtieneReporteDescargaInventario")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult ObtieneReporteDescargaInventario(ConsultaMonitorInventario consultaMonitorInventario)
    {
        try
        {
            consultaMonitorInventario.ArchivoBase = new ArchivoBase();
            consultaMonitorInventario.ArchivoBase.RutaBase = _appsettings["ArchivoBase:LayoutReporteDescargaPorTerminal"];

            ResultBase<DocumentoBase> resultBaseDocumentoBase = _ctReporteadorInventarioRepositorio.ObtieneReporteDescargaInventario(consultaMonitorInventario);

            return Ok(resultBaseDocumentoBase);
        }
        catch
        {
            throw;
        }
    }

}

