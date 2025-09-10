using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ITipoSeveridadRepositorio
{

    ResultBase<TipoSeveridad> ObtenerPorId(int id);

    ResultBase<List<TipoSeveridad>> ObtenerPorDescripcionContains(string descripcion);

}
