using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IReferenciaRepositorio
{
    Task<ResultBase<MonitorReferencia>> Guardar(MonitorReferencia monitorReferencia);

    ResultBase<MonitorReferencia> ObtenerReferenciaPorId(int idReferencia);

    PaginadoResult<MonitorReferencia> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaReferencia> entidad);

    ResultBase Eliminar(int idReferencia);

}
