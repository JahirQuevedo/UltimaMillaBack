using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class ViajeRepositorio : GenericoRepositorio<Viaje>, IViajeRepositorio
{

    private readonly UnitOfWorkWMS _unitOfWorkWMS;

    private readonly ApplicationDbContext _db;

    public ViajeRepositorio(ApplicationDbContext db, UnitOfWorkWMS unitOfWorkWMS) : base(db)
    {
        _db = db;
        _unitOfWorkWMS = unitOfWorkWMS;
    }

    public ResultBase<MonitorViaje> Guardar(MonitorViaje _monitorViaje)
    {
        var resultBase = new ResultBase<MonitorViaje>();

        try
        {
            //TODO : Validar Datos
            Viaje _viaje = new Viaje() { Folio = _monitorViaje.FolioViaje };

            _viaje.IdEmpresa = (int)_monitorViaje.IdEmpresaLogin;
            _viaje.TipoOperacion = _monitorViaje.TipoOperacion;
            _viaje.Activo = true;
            _viaje.IdBarco = _monitorViaje.Barco?.IdBarco == 0 ? _viaje.IdBarco : _monitorViaje.Barco?.IdBarco;
            _viaje.FechaArriboSalida = _monitorViaje.FechaArriboSalida != DateTime.MinValue ? _monitorViaje.FechaArriboSalida : null;
            _viaje.FechaFondeo = _monitorViaje.FechaFondeo != DateTime.MinValue ? _monitorViaje.FechaFondeo : null;

            if (!String.IsNullOrEmpty(_monitorViaje.Barco?.Nombre))
            {
                var _barco = _db.Barcos.Where(x => x.Nombre.Equals(_monitorViaje.Barco.Nombre)).FirstOrDefault();
                if (_barco != null)
                {
                    _viaje.IdBarco = _barco.Id;
                }
                else
                {
                    resultBase.MensajeRespuesta = "No existe el Barco registrado en el Viaje, favor de verificar.";
                    return resultBase;
                }
            }
            _viaje.ReferenciaBuque = _monitorViaje.ReferenciaBuque;

            bool _existeFolioViaje = _db.Viajes.Any(v => v.Folio == _viaje.Folio && v.IdEmpresa == _viaje.IdEmpresa);

            if (_monitorViaje.CRUDAction.Equals(ECRUDAction.Create))
            {

                if (!_existeFolioViaje)
                {
                    // Se obtiene el valor máximo de la Referencia de Buque
                    int _maxReferenciaBuque = (int)_db.Viajes.DefaultIfEmpty().Max(p => p == null ? 0 : p.ReferenciaBuque) + 1;
                    _viaje.ReferenciaBuque = _maxReferenciaBuque;
                    _db.Viajes.Add(_viaje);
                }
                else
                {
                    resultBase.MensajeRespuesta = "Ya existe el Folio de Viaje. Favor de verificar la información.";
                }

            }
            else if (_monitorViaje.CRUDAction.Equals(ECRUDAction.Update))
            {

                _viaje.Id = _monitorViaje.IdViaje;

                if (_existeFolioViaje)
                {
                    _db.Viajes.Update(_viaje);
                }
                else
                {
                    resultBase.MensajeRespuesta = "No existe el Folio de Viaje. Favor de verificar la información";
                }
            }

            _db.SaveChanges();

            resultBase.Id = _viaje.Id;
            _monitorViaje.IdViaje = _viaje.Id;
            _monitorViaje.CRUDAction = ECRUDAction.Read;

            resultBase.Data = _monitorViaje;

        }
        catch (Exception ex)
        {

            resultBase.MensajeRespuesta = ex.Message + " - " + ex.InnerException.Message;

        }
        finally
        {

        }

        return resultBase;
    }

    public ResultBase Eliminar(int idViaje)
    {

        var resultBase = new ResultBase();

        var _viajes = _db.Viajes.Where(x => x.Id == idViaje).SingleOrDefault();

        if (_viajes != null)
        {

            _db.Viajes.Remove(_viajes);
            _db.SaveChanges();

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontró información del registro solicitado.";
        }

        return resultBase;
    }

    public PaginadoResult<MonitorViaje> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaViaje> entidad)
    {
        List<Viaje> listadoViajes;
        List<MonitorViaje> _listMonitorViajes = new List<MonitorViaje>();

        try
        {

            int _totalRegistros = _db.Viajes.Count();

            if (String.IsNullOrEmpty(entidad.Entidad.Folio))
            {
                listadoViajes = _db.Viajes.Where(x => x.Activo.Equals(true)).ToList()
                    .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
                    .Take(entidad.RegistrosPorPagina).ToList();
            }
            else
            {
                listadoViajes = _db.Viajes.Where(x => x.Folio.Contains(entidad.Entidad.Folio) && x.Activo.Equals(true)).ToList();

                _totalRegistros = listadoViajes.Count();

                listadoViajes = listadoViajes.Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
                    .Take(entidad.RegistrosPorPagina).ToList();
            }

            _listMonitorViajes = AddItems(listadoViajes);

            var paginadoInfo = new PaginadoInfo()
            {
                RegistrosPorPagina = entidad.RegistrosPorPagina,
                NumeroDePagina = entidad.NumeroDePagina,
                TotalRegistros = _totalRegistros
            };

            var _paginadoResult = new PaginadoResult<MonitorViaje>(_listMonitorViajes, paginadoInfo);

            return _paginadoResult;

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public Viaje ObtenerPorId(int id)
    {
        try
        {
            var viaje = _db.Viajes.Where(x => x.Id == id).FirstOrDefault();

            return viaje;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    public ResultBase<List<MonitorViaje>> ObtenerPorReferenciaBuqueViajeContains(string referenciaBuqueViaje)
    {
        ResultBase<List<MonitorViaje>> resultBase = new ResultBase<List<MonitorViaje>>();

        var _listMonitorViaje = (from p in _db.Viajes
                                 join _barc in _db.Barcos on p.IdBarco equals _barc.Id into f
                                 from _xbarc in f.DefaultIfEmpty()
                                 where p.Folio.Contains(referenciaBuqueViaje)
                                 select new MonitorViaje
                                 {
                                     IdViaje = p.Id,
                                     FolioViaje = p.Folio,
                                     ReferenciaBuque = (int)(p.ReferenciaBuque != null ? p.ReferenciaBuque : 0),
                                     Barco = new MonitorBarco
                                     {
                                         IdBarco = (int)(p.IdBarco != null ? p.IdBarco : 0),
                                         Nombre = _xbarc.Nombre,
                                     }
                                 }).ToList();

        if (_listMonitorViaje != null)
        {

            resultBase.Data = _listMonitorViaje;

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        return resultBase;
    }

    private List<MonitorViaje> AddItems(List<Viaje> listadoViajes)
    {

        List<MonitorViaje> _listMonitorViajes = new List<MonitorViaje>();

        MonitorViaje _monitorViaje = new MonitorViaje();

        foreach (Viaje _viaje in listadoViajes)
        {

            _monitorViaje = new MonitorViaje();

            _monitorViaje.IdViaje = _viaje.Id;

            _monitorViaje.FolioViaje = _viaje.Folio;

            _monitorViaje.Barco = new MonitorBarco();

            _monitorViaje.Barco.IdBarco = (int)(_viaje.IdBarco != null ? _viaje.IdBarco : 0);

            if (!_monitorViaje.Barco.IdBarco.Equals(0))
            {

                var _resultBarco = _unitOfWorkWMS.BarcoSeviceProvider.ObtenerPorId(_monitorViaje.Barco.IdBarco);

                if (_resultBarco.Success)
                {
                    _monitorViaje.Barco.Nombre = _resultBarco.Data.Nombre;
                }

            }

            _monitorViaje.ReferenciaBuque = (int)(_viaje.ReferenciaBuque != null ? _viaje.ReferenciaBuque : 0);

            _monitorViaje.TipoOperacion = _viaje.TipoOperacion;

            switch (_viaje.TipoOperacion)
            {
                case Enums.TipoOperacionAduanera.Importacion:
                    _monitorViaje.Operacion = "Importación";
                    break;
                case Enums.TipoOperacionAduanera.Exportacion:
                    _monitorViaje.Operacion = "Exportación";
                    break;
                default:
                    _monitorViaje.Operacion = String.Empty; break;
            }
            _monitorViaje.FechaArriboSalida = Convert.ToDateTime(_viaje.FechaArriboSalida);
            _monitorViaje.FechaFondeo = Convert.ToDateTime(_viaje.FechaFondeo);
            _monitorViaje.FechaAtraque = Convert.ToDateTime(_viaje.FechaAtraque);
            _monitorViaje.FechaDesatraque = Convert.ToDateTime(_viaje.FechaDesatraque);
            _monitorViaje.FechaInicioCargaDescarga = Convert.ToDateTime(_viaje.FechaInicioCargaDescarga);
            _monitorViaje.FechaFinCargaDescarga = Convert.ToDateTime(_viaje.FechaFinCargaDescarga);

            _monitorViaje.Exterior = _viaje.Exterior;

            _listMonitorViajes.Add(_monitorViaje);

        }

        return _listMonitorViajes;

    }

}
