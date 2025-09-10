using ALOG.Modelos;


namespace ALOG.Repositorios;

public interface IServicioWMSRepositorio
{
    ResultBase<Servicio> ObtenerPorId(int id);

    ResultBase<Servicio> ObtenerPorIdPaquete(int idPaquete);

    ResultBase<Servicio> Guardar(Servicio servicio);

    ResultBase<List<Servicio>> ObtenerPorDescripcionContains(string descripcion);

}
