using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class PaqueteRepositorio : GenericoRepositorio<Paquete>, IPaqueteRepositorio
{

    private readonly ApplicationDbContext _db;

    public PaqueteRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<Paquete> Guardar(Paquete paquete)
    {
        throw new NotImplementedException();
    }

    public ResultBase<List<Paquete>> ObtenerPorDescripcionContains(string descripcion)
    {
        ResultBase<List<Paquete>> resultBase = new ResultBase<List<Paquete>>();

        var _listPaquetes = _db.Paquetes.Where(pa => pa.Descripcion.Contains(descripcion)).ToList();

        if (_listPaquetes != null)
        {

            resultBase.Data = _listPaquetes;

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        return resultBase;
    }

    public ResultBase<Paquete> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }

    public ResultBase<Paquete> ObtenerPorIdServicio(int idServicio)
    {
        throw new NotImplementedException();
    }
}
