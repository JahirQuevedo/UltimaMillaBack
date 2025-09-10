using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IRecepcionRepositorio
{
    PaginadoResult<MonitorRecepcionMercancia> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaRecepcion> entidad);

    ResultBase<MonitorRecepcionMercancia> FinalizarRecepcionMercancia(MonitorRecepcionMercancia monitorRecepcionMercancia);

    ResultBase<MonitorRecepcionMercancia> ConfirmarRecepcionMercanciaPorIdPartidaInventario(int idInventario, int tieneAveria);

    PaginadoResult<MonitorRecepcionMercancia> ConfirmarRecepcionMercanciaPorIdTarja(ConsultaCatalogoBase<ConsultaRecepcion> entidad);
}
