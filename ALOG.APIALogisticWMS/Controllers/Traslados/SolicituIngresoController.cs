using ALOG.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.APIALogisticWMS;
//[AllowAnonymous]
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SolicituIngresoController : ControllerBase
{
    private readonly ISolicitudIngresoRepositorio _ctSolicitudIngreso;

    public SolicituIngresoController(ISolicitudIngresoRepositorio ctSolicitudIngreso)
    {
        _ctSolicitudIngreso = ctSolicitudIngreso;
    }


}
