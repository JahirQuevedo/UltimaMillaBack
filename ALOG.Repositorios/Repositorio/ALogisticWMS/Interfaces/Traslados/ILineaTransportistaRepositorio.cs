using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ILineaTransportistaRepositorio
{
    ResultBase<MonitorLineaTransporte> ObtenerPorId(int id);

    ResultBase<MonitorLineaTransporte> ObtenerPorIdTransportista(int id);

    ResultBase<MonitorLineaTransporte> Guardar(MonitorLineaTransporte monitorLineaTransporte);

    ResultBase<List<MonitorLineaTransporte>> ObtenerPorRazonSocialContains(string razonSocial);

    ResultBase<List<MonitorLineaTransporte>> ObtenerPorPlacasContains(int idCatTransportista, string placas);

    PaginadoResult<MonitorLineaTransporte> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaLineaTransporte> entidad);

}
