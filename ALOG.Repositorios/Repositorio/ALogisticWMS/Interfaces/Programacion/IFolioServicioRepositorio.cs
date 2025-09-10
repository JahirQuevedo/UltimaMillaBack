using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IFolioServicioRepositorio
{

    ResultBase<MonitorFolioServicio> Guardar(MonitorFolioServicio monitorFolioServicio);

    ResultBase Eliminar(int idFolioServicio);

    ResultBase Confirmar(int idFolioServicio);

    ResultBase<MonitorFolioServicio> ObtenerPorId(int idFolioServicio);

    PaginadoResult<MonitorFolioServicio> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaFolioServicio> entidad);

}
