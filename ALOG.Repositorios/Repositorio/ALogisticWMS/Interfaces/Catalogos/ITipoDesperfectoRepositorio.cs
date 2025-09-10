using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ITipoDesperfectoRepositorio
{

    ResultBase<TipoDesperfecto> ObtenerPorId(int id);

    ResultBase<List<TipoDesperfecto>> ObtenerPorDescripcionContains(string descripcion);

}
