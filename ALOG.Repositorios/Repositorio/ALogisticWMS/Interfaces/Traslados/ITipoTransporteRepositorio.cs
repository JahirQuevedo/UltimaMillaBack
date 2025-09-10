using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ITipoTransporteRepositorio
{
    ResultBase<List<TipoTransporte>> ObtenerPorNombreContains(string nombre);
}
