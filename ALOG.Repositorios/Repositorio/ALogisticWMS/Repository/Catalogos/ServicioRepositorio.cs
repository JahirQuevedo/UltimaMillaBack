using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class ServicioWMSRepositorio : GenericoRepositorio<Servicio>, IServicioWMSRepositorio
{

    private readonly ApplicationDbContext _db;

    public ServicioWMSRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<Servicio> Guardar(Servicio servicio)
    {
        throw new NotImplementedException();
    }

    public ResultBase<List<Servicio>> ObtenerPorDescripcionContains(string descripcion)
    {
        throw new NotImplementedException();
    }

    public ResultBase<Servicio> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }

    public ResultBase<Servicio> ObtenerPorIdPaquete(int idPaquete)
    {
        throw new NotImplementedException();
    }
}