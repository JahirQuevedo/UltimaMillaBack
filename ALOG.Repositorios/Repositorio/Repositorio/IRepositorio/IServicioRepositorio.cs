using ALOG.Modelos.Modelos;

namespace ALOG.Repositorios.Repositorio.Repositorio.IRepositorio
{
    public interface IServicioRepositorio
    {
        ICollection<Servicios> GetServicios();

        Servicios GetServicios(int idServicio);
        bool ExisteServicio(string referenciaAlo);
        bool ExisteServicio(int idServicio);

        bool CrearServicio(Servicios servicios);
        bool ActualizarServicio(Servicios servicios);
        bool BorrarServicio(Servicios servicios);
        bool Guardar();

    }
}
