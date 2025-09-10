using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ILineaOperadorRepositorio
{

    ResultBase<MonitorLineaOperador> ObtenerPorId(int id);

    ResultBase<MonitorLineaOperador> Guardar(MonitorLineaOperador monitorLineaTransporte);

    ResultBase<List<MonitorLineaOperador>> ObtenerPorNombreContains(int idCatTransportista, string nombre);

    PaginadoResult<MonitorLineaOperador> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaLineaOperador> entidad);

}
