using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ICodigoDesperfectoRepositorio
{
    ResultBase<CodigoDesperfecto> ObtenerPorId(int id);

    ResultBase<List<CodigoDesperfecto>> ObtenerPorDescripcionContains(string descripcion);

}
