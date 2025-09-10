using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ILiberacionRepositorio
{

    ResultBase<MonitorLiberacionInventario> Guardar(MonitorLiberacionInventario monitorLiberacionInventario);

    ResultBase EliminarLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario);

    ResultBase<MonitorLiberacionInventario> ObtenerPorId(int idOrdenSalida);

    ResultBase CrearOrdenSalida(MonitorLiberacionInventario monitorLiberacionInventario);

    ResultBase EliminarMercanciaLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario);

    PaginadoResult<MonitorLiberacionInventario> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaLiberacionInventario> entidad);

    PaginadoResult<MonitorInventarioAlmacen> ObtenerDetalleLiberacionPorIdOrdenSalida(ConsultaCatalogoBase<ConsultaLiberacionInventario> entidad);

    PaginadoResult<MonitorLiberacionInventario> ObtenerLiberacionControlEmbarqueListaPaginada(ConsultaCatalogoBase<ConsultaControlEmbarque> entidad);

    ResultBase AutorizarLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario);

    ResultBase SalidaAlmacen(ConsultaLiberacionInventario consultaLiberacionInventario);

    ResultBase ControlEmbarqueAlmacen(ConsultaLiberacionInventario consultaLiberacionInventario);

}
