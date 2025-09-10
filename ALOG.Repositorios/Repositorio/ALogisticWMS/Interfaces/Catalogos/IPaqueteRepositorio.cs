using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IPaqueteRepositorio
{
    ResultBase<Paquete> ObtenerPorId(int id);

    ResultBase<Paquete> ObtenerPorIdServicio(int idServicio);

    ResultBase<Paquete> Guardar(Paquete paquete);

    ResultBase<List<Paquete>> ObtenerPorDescripcionContains(string descripcion);

}
