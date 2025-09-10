using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ISolicitudTrasladoRepositorio
{

    ResultBase<MonitorSolicitudTraslado> Guardar(MonitorSolicitudTraslado monitorSolicitudTraslado);

    ResultBase<MonitorSolicitudTraslado> GuardarServicioSolicitudTraslado(MonitorSolicitudTraslado monitorSolicitudTraslado);

    ResultBase<MonitorSolicitudTraslado> ObtenerPorId(int idSolicitudTraslado);

    PaginadoResult<MonitorSolicitudTraslado> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaSolicitudTraslado> entidad);


    ResultBase CancelarSolicitudTraslado(ConsultaSolicitudTraslado entidad);

}
