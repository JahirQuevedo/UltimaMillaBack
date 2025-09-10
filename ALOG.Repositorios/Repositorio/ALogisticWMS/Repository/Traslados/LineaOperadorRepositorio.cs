using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class LineaOperadorRepositorio : GenericoRepositorio<LineaOperador>, ILineaOperadorRepositorio
{

    private readonly ApplicationDbContext _db;

    public LineaOperadorRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorLineaOperador> Guardar(MonitorLineaOperador monitorLineaTransporte)
    {
        throw new NotImplementedException();
    }

    public PaginadoResult<MonitorLineaOperador> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaLineaOperador> entidad)
    {
        throw new NotImplementedException();
    }

    public ResultBase<MonitorLineaOperador> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }

    public ResultBase<List<MonitorLineaOperador>> ObtenerPorNombreContains(int idCatTransportista, string nombre)
    {
        ResultBase<List<MonitorLineaOperador>> resultBase = new ResultBase<List<MonitorLineaOperador>>();

        var _listMonitorTransporte = (from p in _db.LineaOperadores
                                      join e in _db.catTransportistas on p.IdCatTransportista equals e.IdCatTransportista
                                      where (p.Nombre.Contains(nombre) || p.ApellidoPaterno.Contains(nombre))
                                              && p.IdCatTransportista == (idCatTransportista.Equals(0) ? p.IdCatTransportista : idCatTransportista)
                                      select new MonitorLineaOperador
                                      {
                                          IdLineaOperador = p.Id,
                                          IdCatTransportista = e.IdCatTransportista,
                                          RazonSocialLineaTransporte = e.RazonSocial,
                                          Nombre = p.Nombre,
                                          ApellidoPaterno = p.ApellidoPaterno,
                                          ApellidoMaterno = p.ApellidoMaterno,
                                          IdEmpresaLogin = e.IdCatEmpresas
                                      }).ToList();

        if (_listMonitorTransporte != null)
        {

            resultBase.Data = _listMonitorTransporte;

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        return resultBase;
    }
}
