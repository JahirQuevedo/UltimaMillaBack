using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IZonaAlmacenRepositorio
{
    ResultBase<MonitorZonaAlmacen> ObtenerPorId(int id);

    ResultBase<MonitorZonaAlmacen> Guardar(MonitorZonaAlmacen monitorZonaAlmacenaje);

    ResultBase<List<MonitorZonaAlmacen>> ObtenerLista();


}
