using ALOG.Modelos;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios;

public interface IViajeRepositorio : IGenericoRepositorio<Viaje>
{
    Viaje ObtenerPorId(int id);

    ResultBase<MonitorViaje> Guardar(MonitorViaje monitorViaje);

    ResultBase Eliminar (int idViaje);

    PaginadoResult<MonitorViaje> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaViaje> entidad);


    ResultBase<List<MonitorViaje>> ObtenerPorReferenciaBuqueViajeContains(string referenciaBuqueViaje);

}
