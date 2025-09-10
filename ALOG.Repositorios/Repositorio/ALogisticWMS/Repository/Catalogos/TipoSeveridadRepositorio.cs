using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class TipoSeveridadRepositorio : GenericoRepositorio<TipoDesperfecto>, ITipoSeveridadRepositorio
{
    private readonly ApplicationDbContext _db;

    public TipoSeveridadRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<List<TipoSeveridad>> ObtenerPorDescripcionContains(string descripcion)
    {
        ResultBase<List<TipoSeveridad>> resultBase = new ResultBase<List<TipoSeveridad>>();

        var _listData = _db.TipoSeveridades
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

    public ResultBase<TipoSeveridad> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }
}
