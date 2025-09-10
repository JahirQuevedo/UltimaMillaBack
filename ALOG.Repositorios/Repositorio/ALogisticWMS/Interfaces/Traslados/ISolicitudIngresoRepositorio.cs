using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ISolicitudIngresoRepositorio
{
    ResultBase<MonitorSolicitudIngreso> Guardar(MonitorSolicitudIngreso monitorSolicitudIngreso);

    ResultBase<MonitorSolicitudIngreso> ObtenerPorId(int idSolicitudIngreso);

    PaginadoResult<MonitorSolicitudIngreso> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaSolicitudIngreso> entidad);

}
