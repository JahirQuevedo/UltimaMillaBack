using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IBitacoraAveriaRepositorio
{

    ResultBase<MonitorBitacoraAveria> ObtenerPorId(int idBitacoraAveria);

    ResultBase<MonitorBitacoraAveria> Guardar(MonitorBitacoraAveria monitorBitacoraAveria);

    ResultBase Eliminar(int idBitacoraAveria);

    PaginadoResult<MonitorBitacoraAveria> ObtenerListaPaginadaPorIdInventario(ConsultaCatalogoBase<ConsultaBitacoraAveria> entidad);

}
