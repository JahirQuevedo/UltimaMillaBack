using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class ManiobristaRepositorio : GenericoRepositorio<Maniobrista>, IManiobristaRepositorio
{
    private readonly ApplicationDbContext _db;

    public ManiobristaRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorManiobrista> Guardar(MonitorManiobrista monitorLineaTransporte)
    {
        throw new NotImplementedException();
    }

    public PaginadoResult<MonitorManiobrista> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaManiobrista> entidad)
    {
        throw new NotImplementedException();
    }

    public ResultBase<MonitorManiobrista> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }

    public ResultBase<List<MonitorManiobrista>> ObtenerPorRazonSocialContains(string razonSocial)
    {
        ResultBase<List<MonitorManiobrista>> resultBase = new ResultBase<List<MonitorManiobrista>>();

        var _listMonitorManiobrista = (from p in _db.Maniobristas
                                       where p.RazonSocial.Contains(razonSocial)
                                       select new MonitorManiobrista
                                       {
                                           IdManiobrista = p.Id,
                                           RazonSocial = p.RazonSocial,
                                           RFC = p.RFC,
                                           NombreCorto = p.NombreCorto

                                       }).OrderBy(x => x.RazonSocial).ToList();

        if (_listMonitorManiobrista != null)
        {

            resultBase.Data = _listMonitorManiobrista;

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        return resultBase;
    }
}
