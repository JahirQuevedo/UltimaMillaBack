using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class CodigoDesperfectoRepositorio : GenericoRepositorio<CodigoDesperfecto>, ICodigoDesperfectoRepositorio
{

    private readonly ApplicationDbContext _db;

    public CodigoDesperfectoRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<List<CodigoDesperfecto>> ObtenerPorDescripcionContains(string descripcion)
    {
        ResultBase<List<CodigoDesperfecto>> resultBase = new ResultBase<List<CodigoDesperfecto>>();

        var _listData = _db.CodigoDesperfectos.Where(p => p.Descripcion.Contains(descripcion) || p.Clave.Contains(descripcion)).ToList();

        if (_listData != null)
        {

            resultBase.Data = _listData;

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        return resultBase;
    }

    public ResultBase<CodigoDesperfecto> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }
}
