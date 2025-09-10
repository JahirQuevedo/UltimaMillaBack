using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IManiobristaRepositorio
{
    ResultBase<MonitorManiobrista> ObtenerPorId(int id);

    ResultBase<MonitorManiobrista> Guardar(MonitorManiobrista monitorLineaTransporte);

    ResultBase<List<MonitorManiobrista>> ObtenerPorRazonSocialContains(string razonSocial);

    PaginadoResult<MonitorManiobrista> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaManiobrista> entidad);
}
