using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;

namespace ALOG.Repositorios;

public class ReferenciaRepositorio : GenericoRepositorio<Referencia>, IReferenciaRepositorio
{

    private readonly UnitOfWorkWMS _unitOfWorkWMS;
    private IOrdenesRepositorio _iordenesRepositorio { get; set; }

    private readonly ApplicationDbContext _db;

    public ReferenciaRepositorio(ApplicationDbContext db, UnitOfWorkWMS unitOfWorkWMS, IOrdenesRepositorio ordenesRepositorio) : base(db)
    {
        _db = db;
        _unitOfWorkWMS = unitOfWorkWMS;
        _iordenesRepositorio = ordenesRepositorio;
    }

    public async Task<ResultBase<MonitorReferencia>> Guardar(MonitorReferencia monitorReferencia)
    {
        string folioTemp = DateTime.Now.ToString("yy");
        var resultBase = new ResultBase<MonitorReferencia>();

        using (var _dbContextTransaction = _db.Database.BeginTransaction())
        {
            try
            {

                Referencia _referencia = new Referencia() { Folio = String.Empty };

                if (monitorReferencia.CRUDAction.Equals(ECRUDAction.Update))
                {
                    _referencia = _db.Referencias.Where(p => p.Id.Equals(monitorReferencia.IdReferencia)).SingleOrDefault();
                }

                _referencia.Mercancia = monitorReferencia.Mercancias;
                _referencia.TipoMercancia = monitorReferencia.TipoMercancia;
                _referencia.TipoOperacion = monitorReferencia.TipoOperacion;
                _referencia.BultosInicial = monitorReferencia.BultosInical;
                _referencia.ManifiestoBuque = monitorReferencia.ManifiestoBuque;
                _referencia.PesoInicial = monitorReferencia.PesoInicial;
                _referencia.Observaciones = monitorReferencia.Observaciones;
                _referencia.FechaEntrada = monitorReferencia.FechaEntrada;
                _referencia.FechaAlta = monitorReferencia.FechaAlta;
                _referencia.EstadoReferencia = monitorReferencia.EstadoReferencia;
                _referencia.IdViaje = monitorReferencia.IdViaje == 0 ? _referencia.IdViaje : monitorReferencia.IdViaje;
                _referencia.IdCliente = monitorReferencia.IdCliente == 0 ? _referencia.IdCliente : monitorReferencia.IdCliente;
                _referencia.IdClienteFacturarA = monitorReferencia.IdFacturarA == 0 ? _referencia.IdClienteFacturarA : monitorReferencia.IdFacturarA;
                _referencia.IdProveedor = monitorReferencia.IdProveedorDest == 0 ? _referencia.IdProveedor : monitorReferencia.IdProveedorDest;
                _referencia.IdAduanaSeccion = monitorReferencia.IdCatAduana == 0 ? _referencia.IdAduanaSeccion : monitorReferencia.IdCatAduana;
                _referencia.IdEmpresa = (int)monitorReferencia.IdEmpresaLogin!;

                if (monitorReferencia.CRUDAction.Equals(ECRUDAction.Create))
                {
                    _db.Referencias.Add(_referencia);
                }
                else if (monitorReferencia.CRUDAction.Equals(ECRUDAction.Update))
                {
                    _referencia.Id = monitorReferencia.IdReferencia;
                    _referencia.Folio = monitorReferencia.Folio;
                    _db.Referencias.Update(_referencia);
                }

                _db.SaveChanges();

                if (monitorReferencia.CRUDAction.Equals(ECRUDAction.Create))
                {
                    folioTemp = folioTemp + _referencia.Id.ToString().PadLeft(3, '0');

                    _referencia.Folio = folioTemp;
                    monitorReferencia.IdReferencia = _referencia.Id;
                    monitorReferencia.Folio = folioTemp;

                    //TODO: Crear Orden de Servicio

                    var _ordenServicio = new Ordenes();
                    _ordenServicio.IdCatAduana = _referencia.IdAduanaSeccion;
                    _ordenServicio.IdCatEmpresa = _referencia.IdEmpresa;
                    _ordenServicio.IdCatCliente = (int)_referencia.IdCliente;
                    _ordenServicio.IdCatSistema = 1;
                    _ordenServicio.IdCatSucursal = 1;
                    _ordenServicio.IdCatLineaNegocio = 4; //WMS ALOgistics

                    var _catOrden = _unitOfWorkWMS.OrdenServiceProvider.CrearOrdenServicio(_ordenServicio);
                    //var _catOrden = await _iordenesRepositorio.CrearOrdenServicio(_ordenServicio);

                    if (_catOrden != null)
                    {

                        _referencia.IdOrdenServicio = _catOrden.IdOrden;

                    }

                    _db.Referencias.Update(_referencia);

                    _db.SaveChanges();
                }

                resultBase.Id = _referencia.Id;
                resultBase.Data = monitorReferencia;

            }
            catch (Exception ex)
            {

                resultBase.MensajeRespuesta = "Error: " + ex.Message;
                _dbContextTransaction.Rollback();

            }

            _dbContextTransaction.Commit();

        }

        return resultBase;

    }

    public PaginadoResult<MonitorReferencia> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaReferencia> entidad)
    {
        List<Referencia> listadoReferencias = new List<Referencia>();
        List<MonitorReferencia> _listMonitorReferencias = new List<MonitorReferencia>();

        int _totalRegistros = _db.Referencias.Count();

        // listadoReferencias = _db.Referencias.Where(
        //     x => x.Folio.Contains(!String.IsNullOrEmpty(entidad.Entidad.Folio) ? entidad.Entidad.Folio: x.Folio)
        //     && x.EstadoReferencia.Equals(entidad.Entidad.Estado != EstadoReferencia.None ? entidad.Entidad.Estado : x.EstadoReferencia)
        // ).OrderByDescending(x => x.Folio).ToList();

        // _totalRegistros = listadoReferencias.Count();

        _listMonitorReferencias = (from tRef in _db.Referencias
                                   join tClieAux in _db.catClientes on tRef.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                   from tClie in tClieTemp.DefaultIfEmpty()
                                   join tFacturarAux in _db.catClientes on tRef.Id equals tFacturarAux.IdCatCliente into tFacturarTemp
                                   from tFacturar in tFacturarTemp.DefaultIfEmpty()
                                   join tViajesAux in _db.Viajes on tRef.IdViaje equals tViajesAux.Id into tViajeTemp
                                   from tViaje in tViajeTemp.DefaultIfEmpty()
                                   join tAduanaAux in _db.catAduana on tRef.IdAduanaSeccion equals tAduanaAux.IdCatAduana into tAduanaTemp
                                   from tAduana in tAduanaTemp.DefaultIfEmpty()
                                   join tProveedorAux in _db.catProveedores on tRef.IdProveedor equals tProveedorAux.IdCatProveedor into tProveedorTemp
                                   from tProveedor in tProveedorTemp.DefaultIfEmpty()
                                   where
                                       tRef.Folio.Contains(!String.IsNullOrEmpty(entidad.Entidad.Folio) ? entidad.Entidad.Folio : tRef.Folio)
                                       && tRef.EstadoReferencia.Equals(entidad.Entidad.Estado != EstadoReferencia.None ? entidad.Entidad.Estado : tRef.EstadoReferencia)
                                       && tClie.RazonSocial.Contains(!String.IsNullOrEmpty(entidad.Entidad.RazonSocialCliente) ? entidad.Entidad.RazonSocialCliente : tClie.RazonSocial)
                                       && tRef.Mercancia.Contains(!String.IsNullOrEmpty(entidad.Entidad.Mercancias) ? entidad.Entidad.Mercancias : tRef.Mercancia)
                                   select new MonitorReferencia
                                   {

                                       IdReferencia = tRef.Id,
                                       Folio = tRef.Folio,
                                       Mercancias = tRef.Mercancia,
                                       EstadoReferencia = tRef.EstadoReferencia,
                                       ManifiestoBuque = tRef.ManifiestoBuque,
                                       Observaciones = tRef.Observaciones,
                                       TipoOperacion = tRef.TipoOperacion,
                                       TipoMercancia = tRef.TipoMercancia,
                                       FechaEntrada = tRef.FechaEntrada,
                                       FechaAlta = tRef.FechaAlta,
                                       BultosInical = tRef.BultosInicial ?? 0,
                                       PesoInicial = tRef.PesoInicial ?? 0,
                                       IdViaje = tViaje.Id != null ? tViaje.Id : 0,
                                       FolioViaje = tViaje.Folio,
                                       ReferenciaBuque = (int)(tViaje.ReferenciaBuque != null ? tViaje.ReferenciaBuque : 0),
                                       IdCatAduana = tAduana.IdCatAduana != null ? tAduana.IdCatAduana : 0,
                                       AduanaSeccion = tAduana.Nombre,
                                       IdCliente = tClie.IdCatCliente != null ? tClie.IdCatCliente : 0,
                                       Cliente = tClie.RazonSocial,
                                       IdFacturarA = tFacturar.IdCatCliente != null ? tFacturar.IdCatCliente : 0,
                                       FacturarA = tFacturar.RazonSocial,
                                       IdProveedorDest = tProveedor.IdCatProveedor != null ? tProveedor.IdCatProveedor : 0,
                                       ProveedorDest = tProveedor.RazonSocial,


                                   }).ToList();

        _totalRegistros = _listMonitorReferencias.Count();

        _listMonitorReferencias = _listMonitorReferencias.OrderByDescending(x => x.FechaAlta)
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        //_listMonitorReferencias = AddItems(listadoReferencias);

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorReferencia>(_listMonitorReferencias, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorReferencia> ObtenerReferenciaPorId(int idReferencia)
    {
        var resultBase = new ResultBase<MonitorReferencia>();
        MonitorReferencia monitorReferencia = new MonitorReferencia();

        try
        {
            var referencia = _db.Referencias.Where(x => x.Id == idReferencia).FirstOrDefault();

            if (referencia != null)
            {

                resultBase.Id = referencia.Id;

                // Se verifica que no existan tarjas sin confirmar, para que la referencia pueda pasar a Estado Programado
                var _bTarjasNoConfirmadas = _db.Tarjas.Any(x => x.IdReferencia.Equals(idReferencia) && !x.Estado.Equals(EstadoTarja.Confirmado));

                if (!_bTarjasNoConfirmadas)
                {
                    if (referencia.EstadoReferencia.Equals(EstadoReferencia.Preliminar))
                    {
                        referencia.EstadoReferencia = EstadoReferencia.Programado;
                        _db.Referencias.Update(referencia);
                        _db.SaveChanges();
                    }
                }


                List<MonitorReferencia> listMonitorReferencia = AddItems(new List<Referencia> { referencia });
                resultBase.Data = listMonitorReferencia.FirstOrDefault();

            }
            else
            {
                resultBase.MensajeRespuesta = "No se encontró la Referencia.";
            }

        }
        catch (Exception ex)
        {
            resultBase.MensajeRespuesta = ex.Message;

        }
        finally
        {

        }

        return resultBase;
    }

    private List<MonitorReferencia> AddItems(List<Referencia> listadoReferencias)
    {

        List<MonitorReferencia> _listMonitorReferencias = new List<MonitorReferencia>();

        MonitorReferencia _monitorReferencia = new MonitorReferencia();

        foreach (Referencia _referencia in listadoReferencias)
        {

            _monitorReferencia = new MonitorReferencia();

            _monitorReferencia.IdReferencia = _referencia.Id;
            _monitorReferencia.Folio = _referencia.Folio;
            _monitorReferencia.Mercancias = _referencia.Mercancia;
            _monitorReferencia.EstadoReferencia = _referencia.EstadoReferencia;
            _monitorReferencia.ManifiestoBuque = _referencia.ManifiestoBuque;
            _monitorReferencia.Observaciones = _referencia.Observaciones;
            _monitorReferencia.TipoOperacion = _referencia.TipoOperacion;
            _monitorReferencia.TipoMercancia = _referencia.TipoMercancia;
            _monitorReferencia.FechaEntrada = _referencia.FechaEntrada;
            _monitorReferencia.FechaAlta = _referencia.FechaAlta;
            _monitorReferencia.BultosInical = _referencia.BultosInicial ?? 0;
            _monitorReferencia.PesoInicial = _referencia.PesoInicial ?? 0;

            if (_referencia.IdViaje != null)
            {

                var _viaje = _db.Viajes.Where(p => p.Id.Equals(_referencia.IdViaje)).SingleOrDefault();
                _monitorReferencia.IdViaje = _viaje.Id;
                _monitorReferencia.FolioViaje = _viaje.Folio;
                _monitorReferencia.ReferenciaBuque = (int)_viaje.ReferenciaBuque;
            }

            if (_referencia.IdAduanaSeccion != null)
            {

                var _aduanaSeccion = _db.catAduana.Where(p => p.IdCatAduana.Equals(_referencia.IdAduanaSeccion)).SingleOrDefault();
                _monitorReferencia.IdCatAduana = _aduanaSeccion.IdCatAduana;
                _monitorReferencia.AduanaSeccion = _aduanaSeccion.Nombre;

            }

            if (_referencia.IdCliente != null)
            {

                var _cliente = _db.catClientes.Where(p => p.IdCatCliente.Equals(_referencia.IdCliente)).SingleOrDefault();
                _monitorReferencia.IdCliente = _cliente.IdCatCliente;
                _monitorReferencia.Cliente = _cliente.RazonSocial;
            }

            if (_referencia.IdClienteFacturarA != null)
            {

                var _cliente = _db.catClientes.Where(p => p.IdCatCliente.Equals(_referencia.IdClienteFacturarA)).SingleOrDefault();
                _monitorReferencia.IdFacturarA = _cliente.IdCatCliente;
                _monitorReferencia.FacturarA = _cliente.RazonSocial;
            }

            if (_referencia.IdProveedor != null)
            {

                var _proveedor = _db.catProveedores.Where(p => p.IdCatProveedor.Equals(_referencia.IdProveedor)).SingleOrDefault();
                _monitorReferencia.IdProveedorDest = _proveedor.IdCatProveedor;
                _monitorReferencia.ProveedorDest = _proveedor.RazonSocial;
            }

            _monitorReferencia.ListaReferenciaBookingBLs = new List<MonitorReferenciaBookingBL>();

            var _listaMonitorReferenciaBookingBL = _db.ReferenciaBookingBls.Where(p => p.IdReferencia.Equals(_referencia.Id)).ToList();

            if (!_listaMonitorReferenciaBookingBL.Count.Equals(0))
            {

                _monitorReferencia.ListaReferenciaBookingBLs = (from p in _listaMonitorReferenciaBookingBL
                                                                select new MonitorReferenciaBookingBL
                                                                {

                                                                    IdReferenciaBookingBl = p.Id,
                                                                    IdReferencia = (int)(p.IdReferencia != null ? p.IdReferencia : 0),
                                                                    BookingBl = p.BookingBl,

                                                                }).ToList();

            }

            _listMonitorReferencias.Add(_monitorReferencia);

        }

        return _listMonitorReferencias;

    }

    public ResultBase Eliminar(int idReferencia)
    {

        var resultBase = new ResultBase();

        var _referencia = _db.Referencias.Where(x => x.Id == idReferencia).SingleOrDefault();

        if (_referencia != null)
        {

            _db.Referencias.Remove(_referencia);
            _db.SaveChanges();

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontró información del registro solicitado.";
        }

        return resultBase;
    }
}
