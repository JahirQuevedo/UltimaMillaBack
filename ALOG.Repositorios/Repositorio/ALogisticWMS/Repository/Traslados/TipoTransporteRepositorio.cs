using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class TipoTransporteRepositorio : GenericoRepositorio<TipoTransporte>, ITipoTransporteRepositorio
{

    private readonly ApplicationDbContext _db;

    public TipoTransporteRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<List<TipoTransporte>> ObtenerPorNombreContains(string nombre)
    {
        ResultBase<List<TipoTransporte>> resultBase = new ResultBase<List<TipoTransporte>>();

        var _listTipoTransporte = _db.TipoTransportes.Where(t => t.Descripcion.Contains(nombre)).ToList();

        if (_listTipoTransporte != null)
        {

            resultBase.Data = _listTipoTransporte;

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        return resultBase;
    }
}
