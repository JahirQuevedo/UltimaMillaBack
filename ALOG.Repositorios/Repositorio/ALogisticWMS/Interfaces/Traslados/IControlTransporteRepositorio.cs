using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IControlTransporteRepositorio
{

    ResultBase<MonitorCtrlTransporte> Guardar(MonitorCtrlTransporte monitorControlTransporte);

    ResultBase<MonitorCtrlTransporte> GuardarRelacionSolicitudTraslado(MonitorCtrlTransporte monitorControlTransporte);

    ResultBase<MonitorCtrlTransporte> ObtenerPorId(int id);

    PaginadoResult<MonitorCtrlTransporte> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaCtrlTransporte> entidad);


}
