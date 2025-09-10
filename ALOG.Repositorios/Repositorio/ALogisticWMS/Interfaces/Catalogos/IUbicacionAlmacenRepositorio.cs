using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IUbicacionAlmacenRepositorio
{

    ResultBase<MonitorUbicacionAlmacen> ObtenerPorId(int IdUbicacion);

    ResultBase<List<MonitorUbicacionAlmacen>> ObtenerPorClaveContains(int idZonaAlmacenaje, string clave);

    ResultBase<List<MonitorUbicacionAlmacen>> ObtenerListaUbicacionPorIdAlmacenaje(int idZonaAlmacenaje);

    PaginadoResult<MonitorUbicacionAlmacen> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaUbicacion> entidad);

    ResultBase<MonitorUbicacionAlmacen> ObtenerPorClave(string clave);

}
