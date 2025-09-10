using ALOG.Modelos;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios;

public interface IBarcoRepositorio : IGenericoRepositorio<Barco>
{

    ResultBase<MonitorBarco> Guardar(MonitorBarco MonitorBarco);

    ResultBase Eliminar(int idBarco);

    ResultBase<Barco> ObtenerPorId(int id);

    //ResultBase<List<MonitorBarco>> ObtenerPorNombreContains(string nombre);
    Task<ResultBase<List<MonitorBarco>>> ObtenerPorNombreContains(string nombre);
    PaginadoResult<MonitorBarco> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaBarco> entidad);


}
