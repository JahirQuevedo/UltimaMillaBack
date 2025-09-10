using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class InventarioAlmacenRepositorio : GenericoRepositorio<InventarioALO>, IInventararioAlmacenRepositorio
{

    private readonly ApplicationDbContext _db;

    public InventarioAlmacenRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public PaginadoResult<MonitorInventarioAlmacen> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaMonitorInventario> entidad)
    {
        var resultBase = new ResultBase<MonitorInventarioAlmacen>();
        List<MonitorInventarioAlmacen> _listMonitorInventario = new List<MonitorInventarioAlmacen>();

        int _totalRegistros = 0;

        _listMonitorInventario = (from p in _db.InventarioALOs
                                  join part in _db.Partidas on p.IdInventario equals part.IdInventario
                                  join invt in _db.Inventarios on p.IdInventario equals invt.Id
                                  join refncia in _db.Referencias on p.IdReferencia equals refncia.Id
                                  join catClie in _db.catClientes on refncia.IdCliente equals catClie.IdCatCliente
                                  join refBookingAux in _db.ReferenciaBookingBls on p.IdReferencia equals refBookingAux.IdReferencia into refBooking
                                  from xRefBooking in refBooking.DefaultIfEmpty()
                                  join refViajeAux in _db.Viajes on refncia.IdViaje equals refViajeAux.Id into refViaje
                                  from xRefViaje in refViaje.DefaultIfEmpty()
                                  where
                                      //entidad.Entidad.ListaIdCatCliente.Contains((int)refncia.IdCliente)
                                      //&& 
                                      xRefBooking.BookingBl.Contains(entidad.Entidad.BuscarPorConsultaInventario.Equals(BuscarPorConsultaInventario.BookingBL) ? entidad.Entidad.ValorBusqueda : xRefBooking.BookingBl)
                                      && xRefViaje.Folio.Contains(entidad.Entidad.BuscarPorConsultaInventario.Equals(BuscarPorConsultaInventario.Viaje) ? entidad.Entidad.ValorBusqueda : xRefViaje.Folio)
                                      && (entidad.Entidad.TipoFecha.Equals(1) ? invt.FechaRecoleccion : invt.FechaIngreso) >= entidad.Entidad.FechaInicio.Date.Add(DateTime.MinValue.TimeOfDay) &&
                                          (entidad.Entidad.TipoFecha.Equals(1) ? invt.FechaRecoleccion : invt.FechaIngreso) <= entidad.Entidad.FechaFin.Date.Add(DateTime.MaxValue.TimeOfDay)
                                  select new MonitorInventarioAlmacen
                                  {
                                      IdInventario = (int)(p.IdInventario != null ? p.IdInventario : 0),
                                      IdReferencia = refncia.Id,
                                      IdOrdenServicio = (int)(refncia.IdOrdenServicio != null ? refncia.IdOrdenServicio : 0),
                                      TipoOperacionAduanera = refncia.TipoOperacion,
                                      IdCatCliente = (int)(refncia.IdCliente != null ? refncia.IdCliente : 0),
                                      RazonSocialCliente = catClie.RazonSocial,
                                      FechaIngreso = (DateTime)(invt.FechaIngreso != null ? invt.FechaIngreso : DateTime.MinValue),
                                      FechaRecoleccion = (DateTime)(invt.FechaRecoleccion != null ? invt.FechaRecoleccion : DateTime.MinValue),
                                      Marcas = part.Marcas,
                                      Numeros = part.Numeros,
                                  }).Distinct().ToList();

        _totalRegistros = _listMonitorInventario.Count;

        _listMonitorInventario = _listMonitorInventario
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorInventarioAlmacen>(_listMonitorInventario, paginadoInfo);

        return _paginadoResult;
    }

    public PaginadoResult<MonitorInventarioAlmacen> ObtenerExistenciaMercanciaListaPaginada(ConsultaCatalogoBase<ConsultaMonitorInventario> entidad)
    {
        var resultBase = new ResultBase<MonitorInventarioAlmacen>();
        List<MonitorInventarioAlmacen> _listMonitorInventario = new List<MonitorInventarioAlmacen>();

        int _totalRegistros = 0;

        _listMonitorInventario = (from p in _db.InventarioALOs
                                  join part in _db.Partidas on p.IdInventario equals part.IdInventario
                                  join invt in _db.Inventarios on part.IdInventario equals invt.Id
                                  join tarja in _db.Tarjas on part.IdTarja equals tarja.Id
                                  join refncia in _db.Referencias on p.IdReferencia equals refncia.Id
                                  join catClie in _db.catClientes on refncia.IdCliente equals catClie.IdCatCliente
                                  join refBookingAux in _db.ReferenciaBookingBls on p.IdReferencia equals refBookingAux.IdReferencia into refBooking
                                  from xRefBooking in refBooking.DefaultIfEmpty()
                                  join refViajeAux in _db.Viajes on refncia.IdViaje equals refViajeAux.Id into refViaje
                                  from xRefViaje in refViaje.DefaultIfEmpty()
                                  join OSInvTemp in _db.OrdenSalidaInventarios on invt.Id equals OSInvTemp.IdInventario into OSInvAux
                                  from OSInv in OSInvAux.DefaultIfEmpty()
                                  join LibInvTemp in _db.OrdenSalidas on OSInv.IdOrdenSalida equals LibInvTemp.Id into LibInvAux
                                  from LibInv in LibInvAux.DefaultIfEmpty()
                                  where
                                      entidad.Entidad.ListaIdCatCliente.Count.Equals(0) ? refncia.IdCliente.Equals(refncia.IdCliente) :
                                      entidad.Entidad.ListaIdCatCliente.Contains((int)refncia.IdCliente)
                                      && invt.Existencia.Equals(true)
                                      && invt.FechaIngreso != null
                                      && tarja.TipoTarja.Equals(TipoTarja.Entrada) && tarja.Estado.Equals(EstadoTarja.Confirmado)
                                      && OSInv.IdOrdenSalida.GetValueOrDefault() == (LibInv.Estado.Equals(EstadoLiberacion.Cancelada) ? 0 : 0)
                                      && part.Numeros.Contains(String.IsNullOrEmpty(entidad.Entidad.IDMercancia) ? part.Numeros : entidad.Entidad.IDMercancia)
                                  select new MonitorInventarioAlmacen
                                  {
                                      IdInventario = p.Id,
                                      IdReferencia = refncia.Id,
                                      IdOrdenServicio = (int)(refncia.IdOrdenServicio != null ? refncia.IdOrdenServicio : 0),
                                      TipoOperacionAduanera = refncia.TipoOperacion,
                                      IdCatCliente = (int)(refncia.IdCliente != null ? refncia.IdCliente : 0),
                                      RazonSocialCliente = catClie.RazonSocial,
                                      FechaIngreso = (DateTime)(invt.FechaIngreso != null ? invt.FechaIngreso : DateTime.MinValue),
                                      FechaRecoleccion = (DateTime)(invt.FechaRecoleccion != null ? invt.FechaRecoleccion : DateTime.MinValue),
                                      Cantidad = (int)(invt.CantidadInicial != null ? invt.CantidadInicial : 0),
                                      Peso = (decimal)(invt.PesoInicial != null ? invt.PesoInicial : 0),
                                      Marcas = part.Marcas,
                                      Modelo = part.Modelo,
                                      Numeros = part.Numeros
                                  }).Distinct().ToList();

        _totalRegistros = _listMonitorInventario.Count;

        _listMonitorInventario = _listMonitorInventario
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorInventarioAlmacen>(_listMonitorInventario, paginadoInfo);

        return _paginadoResult;
    }

    public PaginadoResult<MonitorInventarioAlmacen> ObtenerMercanciaInventarioListaPaginada(ConsultaCatalogoBase<ConsultaMonitorInventario> entidad)
    {
        var resultBase = new ResultBase<MonitorInventarioAlmacen>();
        List<MonitorInventarioAlmacen> _listMonitorInventario = new List<MonitorInventarioAlmacen>();

        int _totalRegistros = 0;
        string _claveReferenciaTarja = string.Empty;
        string _razonSocialCliente = string.Empty;

        // TODO: Inicializa filtros de búsqueda
        if (entidad.Entidad.BuscarPorConsultaInventario.Equals(BuscarPorConsultaInventario.TarjaEntrada))
        {
            _claveReferenciaTarja = (string)entidad.Entidad.ValorBusqueda;
        }

        if (!String.IsNullOrEmpty(entidad.Entidad.RazonSocialCliente))
        {
            _razonSocialCliente = entidad.Entidad.RazonSocialCliente;
        }

        // TODO: Obtener listado de Inventario en Existencia
        _listMonitorInventario = (from p in _db.InventarioALOs
                                  join part in _db.Partidas on p.IdInventario equals part.IdInventario
                                  join invt in _db.Inventarios on p.IdInventario equals invt.Id
                                  join tarja in _db.Tarjas on part.IdTarja equals tarja.Id
                                  join refncia in _db.Referencias on p.IdReferencia equals refncia.Id
                                  join catClie in _db.catClientes on refncia.IdCliente equals catClie.IdCatCliente
                                  join refBookingAux in _db.ReferenciaBookingBls on p.IdReferencia equals refBookingAux.IdReferencia into refBooking
                                  from xRefBooking in refBooking.DefaultIfEmpty()
                                  join refViajeAux in _db.Viajes on refncia.IdViaje equals refViajeAux.Id into refViaje
                                  from xRefViaje in refViaje.DefaultIfEmpty()
                                  join OSInvTemp in _db.OrdenSalidaInventarios on invt.Id equals OSInvTemp.IdInventario into OSInvAux
                                  from OSInv in OSInvAux.DefaultIfEmpty()
                                  join LibInvTemp in _db.OrdenSalidas on OSInv.IdOrdenSalida equals LibInvTemp.Id into LibInvAux
                                  from LibInv in LibInvAux.DefaultIfEmpty()
                                  join tUbicaTemp in _db.Ubicaciones on invt.IdUbicacion equals tUbicaTemp.Id into tUbicaAux
                                  from tUbica in tUbicaAux.DefaultIfEmpty()
                                  join tZonaTemp in _db.ZonaAlmacenes on tUbica.IdZonaAlmacenaje equals tZonaTemp.Id into tZonaAux
                                  from tZona in tZonaAux.DefaultIfEmpty()
                                  where
                                      tarja.TipoTarja.Equals(TipoTarja.Entrada) && tarja.Estado.Equals(EstadoTarja.Confirmado)
                                      && invt.Existencia.Equals(true)
                                      && tarja.ClaveReferencia.Contains(String.IsNullOrEmpty(_claveReferenciaTarja) ? tarja.ClaveReferencia : _claveReferenciaTarja)
                                      && catClie.RazonSocial.Contains(String.IsNullOrEmpty(_razonSocialCliente) ? catClie.RazonSocial : _razonSocialCliente)
                                      && part.Numeros.Contains(String.IsNullOrEmpty(entidad.Entidad.IDMercancia) ? part.Numeros : entidad.Entidad.IDMercancia)
                                      && part.Modelo.Contains(String.IsNullOrEmpty(entidad.Entidad.Modelo) ? part.Modelo : entidad.Entidad.Modelo)
                                      && part.Marcas.Contains(String.IsNullOrEmpty(entidad.Entidad.Marcas) ? part.Marcas : entidad.Entidad.Marcas)
                                      && (String.IsNullOrEmpty(tUbica.Clave) ? String.Empty : tUbica.Clave).Contains(String.IsNullOrEmpty(entidad.Entidad.Ubicacion) ? (String.IsNullOrEmpty(tUbica.Clave) ? String.Empty : tUbica.Clave) : entidad.Entidad.Ubicacion)
                                      && (String.IsNullOrEmpty(tZona.Clave) ? String.Empty : "ZONA " + tZona.Clave).Contains(String.IsNullOrEmpty(entidad.Entidad.ZonaAlmacen) ? (String.IsNullOrEmpty(tZona.Clave) ? String.Empty : "ZONA " + tZona.Clave) : entidad.Entidad.ZonaAlmacen)
                                  select new MonitorInventarioAlmacen
                                  {
                                      IdInventario = p.Id,
                                      IdTarja = tarja.Id,
                                      IdReferencia = refncia.Id,
                                      FolioReferencia = refncia.Folio,
                                      IdOrdenServicio = (int)(refncia.IdOrdenServicio != null ? refncia.IdOrdenServicio : 0),
                                      ClaveReferenciaTarja = tarja.ClaveReferencia,
                                      TipoOperacionAduanera = refncia.TipoOperacion,
                                      IdCatCliente = (int)(refncia.IdCliente != null ? refncia.IdCliente : 0),
                                      RazonSocialCliente = catClie.RazonSocial,
                                      FechaIngreso = (DateTime)(invt.FechaIngreso != null ? invt.FechaIngreso : DateTime.MinValue),
                                      FechaRecoleccion = (DateTime)(invt.FechaRecoleccion != null ? invt.FechaRecoleccion : DateTime.MinValue),
                                      Cantidad = (int)(invt.CantidadInicial != null ? invt.CantidadInicial : 0),
                                      Peso = (decimal)(invt.PesoInicial != null ? invt.PesoInicial : 0),
                                      Marcas = part.Marcas,
                                      Modelo = part.Modelo,
                                      Numeros = part.Numeros,
                                      ZonalAlmacen = String.IsNullOrEmpty(tZona.Clave) ? String.Empty : "ZONA " + tZona.Clave,
                                      Ubicacion = String.IsNullOrEmpty(tUbica.Clave) ? String.Empty : tUbica.Clave,
                                  }).Distinct().ToList();

        _totalRegistros = _listMonitorInventario.Count();

        _listMonitorInventario = _listMonitorInventario
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        _listMonitorInventario.Select(p => { p.CodigoDannios = ObtenerCodigDanniosInventario(p.IdInventario); return p; }).ToList();


        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorInventarioAlmacen>(_listMonitorInventario, paginadoInfo);

        return _paginadoResult;
    }

    private string ObtenerCodigDanniosInventario(int idInventario)
    {
        string _codigoDannios = String.Empty;

        var _listaBitacoraAveria = (from tBitAv in _db.BitacoraAveriaInventarios
                                    join tCodAve in _db.CodigoDesperfectos on tBitAv.IdCodigoDesperfecto equals tCodAve.Id
                                    join tTipoAv in _db.TipoDesperfectos on tBitAv.IdTipoDesperfecto equals tTipoAv.Id
                                    join tTipoSev in _db.TipoSeveridades on tBitAv.IdTipoSeveridad equals tTipoSev.Id
                                    where tBitAv.IdInventario == idInventario
                                    select new MonitorBitacoraAveria
                                    {
                                        ClaveCodigoDesperfecto = tCodAve.Clave,
                                        ClaveTipoDesperfecto = tTipoAv.Clave,
                                        ClaveTipoSeveridad = tTipoSev.Clave,
                                    }).ToList();

        foreach (var itemBitAveria in _listaBitacoraAveria) {
            _codigoDannios += itemBitAveria.ClaveCodigoDesperfecto + "-" + itemBitAveria.ClaveTipoDesperfecto + "-" + itemBitAveria.ClaveTipoSeveridad + ", ";
        }

        _codigoDannios = _codigoDannios.Trim().Trim(',');

        return _codigoDannios;

    }

}
