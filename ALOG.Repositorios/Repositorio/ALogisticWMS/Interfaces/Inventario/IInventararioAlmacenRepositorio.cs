using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IInventararioAlmacenRepositorio
{

    PaginadoResult<MonitorInventarioAlmacen> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaMonitorInventario> consulta);

    PaginadoResult<MonitorInventarioAlmacen> ObtenerExistenciaMercanciaListaPaginada(ConsultaCatalogoBase<ConsultaMonitorInventario> consulta);

    PaginadoResult<MonitorInventarioAlmacen> ObtenerMercanciaInventarioListaPaginada(ConsultaCatalogoBase<ConsultaMonitorInventario> consulta);


}
