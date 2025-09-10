using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class TipoDesperfectoRepositorio : GenericoRepositorio<TipoDesperfecto>, ITipoDesperfectoRepositorio
{
    private readonly ApplicationDbContext _db;

    public TipoDesperfectoRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<List<TipoDesperfecto>> ObtenerPorDescripcionContains(string descripcion)
    {
        ResultBase<List<TipoDesperfecto>> resultBase = new ResultBase<List<TipoDesperfecto>>();

        var _listData = _db.TipoDesperfectos
                            .Where(p => p.Descripcion.Contains(descripcion) || p.Clave.Contains(descripcion)).ToList();

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

    public ResultBase<TipoDesperfecto> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }
}
