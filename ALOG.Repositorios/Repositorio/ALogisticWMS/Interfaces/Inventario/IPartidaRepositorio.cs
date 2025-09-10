using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IPartidaRepositorio
{

    ResultBase<MonitorPartida> Guardar(MonitorPartida monitorPartida);

    ResultBase<MonitorPartida> ObtenerPorId(int idTarja);

    PaginadoResult<MonitorPartida> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaPartida> entidad);

    PaginadoResult<MonitorPartida> ObtenerListaPaginadaPorIdTarja(ConsultaCatalogoBase<ConsultaPartida> entidad);

}
