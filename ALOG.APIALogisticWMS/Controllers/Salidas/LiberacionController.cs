using ALOG.Modelos;
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

namespace ALOG.APIALogisticWMS;

//[Authorize(Roles = "Admin,Externo")]
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LiberacionController : ControllerBase
{
    private readonly ILogger<LiberacionController> _logger;
    private readonly IConfiguration _configuration;

    private readonly ILiberacionRepositorio _ctLiberacion;


    public LiberacionController(ILiberacionRepositorio ctLiberacion)
    {
        _ctLiberacion = ctLiberacion;
    }


    [HttpPost("CrearOrdenSalida", Name = "CrearOrdenSalida")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CrearOrdenSalida(MonitorLiberacionInventario monitorLiberacionInventario)
    {
        var resultBase = _ctLiberacion.CrearOrdenSalida(monitorLiberacionInventario);

        return Ok(resultBase);
    }

    [HttpPost("EliminarLiberacion", Name = "EliminarLiberacion")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EliminarLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var resultBase = _ctLiberacion.EliminarLiberacion(consultaLiberacionInventario);

        return Ok(resultBase);
    }

    [HttpPost("EliminarMercanciaLiberacion", Name = "EliminarMercanciaLiberacion")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult EliminarMercanciaLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var resultBase = _ctLiberacion.EliminarMercanciaLiberacion(consultaLiberacionInventario);

        return Ok(resultBase);
    }

    [HttpPost("ObtenerListaPaginada", Name = "ObtenerListaPaginadaLiberacionInventario")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObtenerListaPaginadaLiberacionInventario(ConsultaCatalogoBase<ConsultaLiberacionInventario> consulta)
    {
        PaginadoResult<MonitorLiberacionInventario> paginadoResult = _ctLiberacion.ObtenerListaPaginada(consulta);

        return Ok(paginadoResult);
    }

    [HttpPost("ObtenerDetalleInventarioListaPaginada", Name = "ObtenerDetalleInventarioListaPaginadaPorIdOrdenSalida")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObtenerDetalleInventarioListaPaginadaPorIdOrdenSalida(ConsultaCatalogoBase<ConsultaLiberacionInventario> consulta)
    {
        PaginadoResult<MonitorInventarioAlmacen> paginadoResult = _ctLiberacion.ObtenerDetalleLiberacionPorIdOrdenSalida(consulta);

        return Ok(paginadoResult);
    }

    [HttpPost("ObtenerLiberacionControlEmbarqueListaPaginada", Name = "ObtenerLiberacionControlEmbarqueListaPaginada")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObtenerLiberacionControlEmbarqueListaPaginada(ConsultaCatalogoBase<ConsultaControlEmbarque> entidad)
    {
        PaginadoResult<MonitorLiberacionInventario> paginadoResult = _ctLiberacion.ObtenerLiberacionControlEmbarqueListaPaginada(entidad);

        return Ok(paginadoResult);
    }

    [HttpPost("AutorizarLiberacion", Name = "AutorizarLiberacion")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AutorizarLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var resultBase = _ctLiberacion.AutorizarLiberacion(consultaLiberacionInventario);

        return Ok(resultBase);
    }

    [HttpPost("SalidaAlmacen", Name = "SalidaAlmacen")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SalidaAlmacen(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var resultBase = _ctLiberacion.SalidaAlmacen(consultaLiberacionInventario);

        return Ok(resultBase);
    }

    [HttpPost("ControlEmbarqueAlmacen", Name = "ControlEmbarqueAlmacen")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ControlEmbarqueAlmacen(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var resultBase = _ctLiberacion.ControlEmbarqueAlmacen(consultaLiberacionInventario);

        return Ok(resultBase);
    }

}

