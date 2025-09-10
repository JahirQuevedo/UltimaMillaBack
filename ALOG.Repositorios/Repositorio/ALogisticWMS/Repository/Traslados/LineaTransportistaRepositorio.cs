using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class LineaTransportistaRepositorio : GenericoRepositorio<LineaTransporte>, ILineaTransportistaRepositorio
{

    private readonly ApplicationDbContext _db;

    public LineaTransportistaRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorLineaTransporte> Guardar(MonitorLineaTransporte monitorLineaTransporte)
    {
        throw new NotImplementedException();
    }

    public PaginadoResult<MonitorLineaTransporte> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaLineaTransporte> entidad)
    {
        throw new NotImplementedException();
    }

    public ResultBase<MonitorLineaTransporte> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }

    public ResultBase<MonitorLineaTransporte> ObtenerPorIdTransportista(int id)
    {
        throw new NotImplementedException();
    }

    public ResultBase<List<MonitorLineaTransporte>> ObtenerPorPlacasContains(int idCatTransportista, string placas)
    {
        ResultBase<List<MonitorLineaTransporte>> resultBase = new ResultBase<List<MonitorLineaTransporte>>();

        List<MonitorLineaTransporte> _listMonitorTransporte = new List<MonitorLineaTransporte>();

        _listMonitorTransporte = (from p in _db.LineaTransportes
                                  join e in _db.catTransportistas on p.IdCatTransportista equals e.IdCatTransportista
                                  where p.Placas.Contains(placas)
                                          && p.IdCatTransportista == (idCatTransportista.Equals(0) ? p.IdCatTransportista : idCatTransportista)
                                  select new MonitorLineaTransporte
                                  {
                                      IdLineaTransporte = p.Id,
                                      IdCatTransportista = e.IdCatTransportista,
                                      RazonSocialLineaTransporte = e.RazonSocial,
                                      Placas = p.Placas,
                                      NumeroEconomico = String.IsNullOrEmpty(p.NumeroEconomico) ? String.Empty : p.NumeroEconomico,
                                      PlacasPlana1 = String.IsNullOrEmpty(p.PlacasPlana1) ? String.Empty : p.PlacasPlana1,
                                      PlacasPlana2 = String.IsNullOrEmpty(p.PlacasPlana2) ? String.Empty : p.PlacasPlana2,
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

    public ResultBase<List<MonitorLineaTransporte>> ObtenerPorRazonSocialContains(string razonSocial)
    {
        ResultBase<List<MonitorLineaTransporte>> resultBase = new ResultBase<List<MonitorLineaTransporte>>();

        var _listMonitorTransporte = (from p in _db.catTransportistas
                                      where p.RazonSocial.Contains(razonSocial)
                                                  && p.IdCatPaises.Equals(1)
                                      select new MonitorLineaTransporte
                                      {
                                          IdCatTransportista = p.IdCatTransportista,
                                          RazonSocialLineaTransporte = p.RazonSocial,
                                          RFC = p.RFC,
                                          IdEmpresaLogin = p.IdCatEmpresas

                                      }).OrderBy(x => x.RazonSocialLineaTransporte).ToList();

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
