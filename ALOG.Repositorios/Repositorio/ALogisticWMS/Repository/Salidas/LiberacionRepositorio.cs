using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Encryption;
using NPOI.SS.Formula.PTG;
using NPOI.Util;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Plugins;

namespace ALOG.Repositorios;

public class LiberacionRepositorio : GenericoRepositorio<OrdenSalida>, ILiberacionRepositorio
{
    private readonly ApplicationDbContext _db;

    public LiberacionRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase CrearOrdenSalida(MonitorLiberacionInventario monitorLiberacionInventario)
    {
        var _resultBase = new ResultBase();

        using (var _dbContextTransaction = _db.Database.BeginTransaction())
        {

            try
            {

                DateTime _fechaAlta = DateTime.Now;
                string folioTemp = DateTime.Now.ToString("yy");

                // Se crea instancia de la orden de salida / liberacion
                OrdenSalida _liberacionOrdenSalida = new OrdenSalida();

                // Se crea tarja de salida
                Tarja _tarjaSalida = new Tarja();

                if (monitorLiberacionInventario.CRUDAction.Equals(ECRUDAction.Create))
                {
                    _liberacionOrdenSalida.Estado = monitorLiberacionInventario.EstadoLiberacion;
                    _liberacionOrdenSalida.FechaAlta = _fechaAlta;
                    _liberacionOrdenSalida.IdEmpresa = (int)monitorLiberacionInventario.IdEmpresaLogin;

                    _db.OrdenSalidas.Add(_liberacionOrdenSalida);
                    _db.SaveChanges();

                    if (monitorLiberacionInventario.CRUDAction.Equals(ECRUDAction.Create))
                    {
                        folioTemp = folioTemp + _liberacionOrdenSalida.Id.ToString().PadLeft(3, '0');
                        _liberacionOrdenSalida.Folio = Convert.ToInt32(folioTemp);

                        _db.OrdenSalidas.Update(_liberacionOrdenSalida);
                        _db.SaveChanges();
                    }

                    Liberacion _liberacion = new Liberacion();

                    _liberacion.IdCliente = monitorLiberacionInventario.IdCatCliente;
                    _liberacion.IdOrdenSalida = _liberacionOrdenSalida.Id;

                    _db.Liberaciones.Add(_liberacion);
                    _db.SaveChanges();

                    _tarjaSalida.Folio = Convert.ToInt32(folioTemp + "001");
                    _tarjaSalida.ClaveReferencia = folioTemp + "-001";
                    _tarjaSalida.TipoTarja = TipoTarja.Salida;
                    _tarjaSalida.Estado = EstadoTarja.Preliminar;
                    _tarjaSalida.TipoServicio = TipoServicioTarja.SalidaAlmacen;
                    _tarjaSalida.FechaAlta = _fechaAlta;
                    _tarjaSalida.IdEmpresa = (int)monitorLiberacionInventario.IdEmpresaLogin;

                    _db.Tarjas.Add(_tarjaSalida);
                    _db.SaveChanges();

                }
                else if (monitorLiberacionInventario.CRUDAction.Equals(ECRUDAction.Update))
                {
                    // Se obtiene la información de la liberación.
                    _liberacionOrdenSalida = _db.OrdenSalidas.Where(x => x.Id.Equals(monitorLiberacionInventario.IdLiberacion)).SingleOrDefault();
                    // Se obtiene la información de la tarja de salida.
                    _tarjaSalida = _db.Tarjas.Where(x => x.Id.Equals(monitorLiberacionInventario.IdTarja)).SingleOrDefault();

                    if (_tarjaSalida == null)
                    {
                        throw new Exception("No existe la Tarja de Salida, verifique la infomación enviada.");
                    }

                }

                foreach (var _monitorInventarioAlnacen in monitorLiberacionInventario.ListaMonitorInventarioAlmacen)
                {
                    // Se relaciona la orden de salida con el inventario
                    OrdenSalidaInventario _ordenSalidaInventario = new OrdenSalidaInventario();
                    _ordenSalidaInventario.CRUDAction = ECRUDAction.Create;
                    _ordenSalidaInventario.IdInventario = _monitorInventarioAlnacen.IdInventario;
                    _ordenSalidaInventario.IdOrdenSalida = _liberacionOrdenSalida.Id;

                    _db.OrdenSalidaInventarios.Add(_ordenSalidaInventario);
                    _db.SaveChanges();

                    var _partida = _db.Partidas.Where(x => x.IdInventario.Equals(_monitorInventarioAlnacen.IdInventario)).SingleOrDefault();

                    LiberacionInventario _liberacionInventario = new LiberacionInventario();
                    _liberacionInventario.IdOrdenSalidaInventario = _ordenSalidaInventario.Id;
                    _liberacionInventario.IdTarja = _tarjaSalida.Id;
                    _liberacionInventario.NumeroPartida = _partida.NumeroPartida;
                    _liberacionInventario.Cantidad = _monitorInventarioAlnacen.Cantidad;
                    _liberacionInventario.Peso = _monitorInventarioAlnacen.Peso;
                    // TODO: Verificar saldos de cantidad y peso inicial contra el saldo liberado
                    _liberacionInventario.LiberacionParcial = false;

                    _db.LiberacionInventarios.Add(_liberacionInventario);
                    _db.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                _resultBase.MensajeRespuesta = "Error: " + ex.Message;
                _dbContextTransaction.Rollback();

            }

            _dbContextTransaction.Commit();

        }

        return _resultBase;
    }

    public ResultBase EliminarMercanciaLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var _resultBase = new ResultBase();

        using (var _dbContextTransaction = _db.Database.BeginTransaction())
        {

            try
            {

                var _ordenSalidaInventario = _db.OrdenSalidaInventarios.Where(x => x.IdInventario.Equals(consultaLiberacionInventario.IdInventario)).SingleOrDefault();

                var countSalidaInventario = _db.OrdenSalidaInventarios.Where(_x => _x.IdOrdenSalida.Equals(_ordenSalidaInventario.IdOrdenSalida)).Count();

                if (countSalidaInventario > 1)
                {

                    if (_ordenSalidaInventario != null)
                    {
                        var _liberacionInventario = _db.LiberacionInventarios.Where(x => x.IdOrdenSalidaInventario.Equals(_ordenSalidaInventario.Id)).SingleOrDefault();

                        if (_liberacionInventario != null)
                        {
                            _db.LiberacionInventarios.Remove(_liberacionInventario);
                            _db.SaveChanges();

                            _db.OrdenSalidaInventarios.Remove(_ordenSalidaInventario);
                            _db.SaveChanges();
                        }

                    }
                    else
                    {
                        _resultBase.MensajeRespuesta = "No se encontró información para ejecutar la operación solicitada.";
                    }

                }
                else
                {
                    _resultBase.MensajeRespuesta = "La Liberación no puede quedar vacía, sin relacionar al menos una mercancía.";
                }

            }
            catch (Exception ex)
            {
                _resultBase.MensajeRespuesta = "Error: " + ex.Message;
                _dbContextTransaction.Rollback();

            }

            _dbContextTransaction.Commit();

        }

        return _resultBase;
    }

    public ResultBase<MonitorLiberacionInventario> Guardar(MonitorLiberacionInventario monitorLiberacionInventario)
    {
        throw new NotImplementedException();
    }

    public ResultBase EliminarLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var _resultBase = new ResultBase();

        using (var _dbContextTransaction = _db.Database.BeginTransaction())
        {

            try
            {
                // Se obtienen la lista de inventario
                var _listOrdenSalidaInventario = _db.OrdenSalidaInventarios.Where(x => x.IdOrdenSalida.Equals(consultaLiberacionInventario.IdOrdenSalida)).ToList();

                if (!_listOrdenSalidaInventario.Count.Equals(0))
                {
                    // Se define para poder guardar la tarja de salida.
                    var _idTarjaSalida = 0;

                    foreach (var _itemOSI in _listOrdenSalidaInventario)
                    {
                        var _liberacionInventario = _db.LiberacionInventarios.Where(x => x.IdOrdenSalidaInventario.Equals(_itemOSI.Id)).SingleOrDefault();

                        _idTarjaSalida = (int)_liberacionInventario.IdTarja;

                        if (_liberacionInventario != null)
                        {
                            _db.LiberacionInventarios.Remove(_liberacionInventario);
                            _db.SaveChanges();

                            var _ordenSalidaInventario = _db.OrdenSalidaInventarios.Where(x => x.Id.Equals(_itemOSI.Id)).SingleOrDefault();

                            _db.OrdenSalidaInventarios.Remove(_ordenSalidaInventario);
                            _db.SaveChanges();
                        }

                    }

                    if (!_idTarjaSalida.Equals(0))
                    {
                        // La Tarja de Salida queda cancelada
                        // TODO: Revisar si es mejor eliminar el registro en vez del borrado lógico 
                        var _tarjaSalida = _db.Tarjas.Where(x => x.Id.Equals(_idTarjaSalida)).SingleOrDefault();
                        _tarjaSalida.Estado = EstadoTarja.Cancelado;
                        _db.Tarjas.Update(_tarjaSalida);
                        _db.SaveChanges();

                    }

                    // La Orden de Salida (Liberación) queda cancelada
                    var _ordenSalida = _db.OrdenSalidas.Where(x => x.Id.Equals(consultaLiberacionInventario.IdOrdenSalida)).SingleOrDefault();
                    _ordenSalida.Estado = EstadoLiberacion.Cancelada;
                    _db.OrdenSalidas.Update(_ordenSalida);
                    _db.SaveChanges();


                }
                else
                {
                    _resultBase.MensajeRespuesta = "No se cuenta el identificador de la liberación. Verifique la información.";
                }

            }
            catch (Exception ex)
            {
                _resultBase.MensajeRespuesta = "Error: " + ex.Message;
                _dbContextTransaction.Rollback();

            }

            _dbContextTransaction.Commit();

        }

        return _resultBase;
    }

    public PaginadoResult<MonitorInventarioAlmacen> ObtenerDetalleLiberacionPorIdOrdenSalida(ConsultaCatalogoBase<ConsultaLiberacionInventario> entidad)
    {
        var resultBase = new ResultBase<MonitorLiberacionInventario>();

        List<MonitorInventarioAlmacen> listInventarioAlmacen = new List<MonitorInventarioAlmacen>();

        listInventarioAlmacen = (from tOS in _db.OrdenSalidas
                                 join tLib in _db.Liberaciones on tOS.Id equals tLib.IdOrdenSalida
                                 join tClieAux in _db.catClientes on tLib.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                 from tClie in tClieTemp.DefaultIfEmpty()
                                 join tOSI in _db.OrdenSalidaInventarios on tOS.Id equals tOSI.IdOrdenSalida
                                 join tLibInv in _db.LiberacionInventarios on tOSI.Id equals tLibInv.IdOrdenSalidaInventario
                                 join tTS in _db.Tarjas on tLibInv.IdTarja equals tTS.Id
                                 join tPartida in _db.Partidas on tOSI.IdInventario equals tPartida.IdInventario
                                 join tInv in _db.Inventarios on tPartida.IdInventario equals tInv.Id
                                 where tOS.Id.Equals(entidad.Entidad.IdOrdenSalida)
                                 select new MonitorInventarioAlmacen
                                 {
                                     IdOrdenSalida = tOS.Id,
                                     IdOrdenSalidaInventario = tOSI.Id,
                                     IdInventario = (int)(tOSI.IdInventario != null ? tOSI.IdInventario : 0),
                                     IdCatCliente = (int)(tLib.IdCliente != null ? tLib.IdCliente : 0),
                                     Numeros = tPartida.Numeros,
                                     Marcas = tPartida.Marcas,
                                     Modelo = tPartida.Modelo,
                                     Cantidad = (int)(tLibInv.Cantidad != null ? tLibInv.Cantidad : 0),
                                     Peso = (decimal)(tLibInv.Peso != null ? tLibInv.Peso : 0),
                                     FechaIngreso = (DateTime)tInv.FechaIngreso,
                                     FechaSalida = (DateTime)(tInv.FechaSalida != null ? tInv.FechaSalida : DateTime.MinValue),
                                     FechaEmbarque = (DateTime)(tInv.FechaEmbarque != null ? tInv.FechaEmbarque : DateTime.MinValue),
                                     Estadias = (int)((TimeSpan)((tInv.FechaSalida != null ? tInv.FechaSalida : DateTime.Now) - tInv.FechaIngreso)).TotalDays,
                                 }).ToList();

        int _totalRegistros = listInventarioAlmacen.Count;

        listInventarioAlmacen = listInventarioAlmacen.OrderByDescending(x => x.Numeros).ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorInventarioAlmacen>(listInventarioAlmacen, paginadoInfo);

        return _paginadoResult;
    }

    public PaginadoResult<MonitorLiberacionInventario> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaLiberacionInventario> entidad)
    {
        var resultBase = new ResultBase<MonitorLiberacionInventario>();
        List<MonitorLiberacionInventario> listLiberacionInventario = new List<MonitorLiberacionInventario>();

        int _totalRegistros = 0;

        listLiberacionInventario = (from tOS in _db.OrdenSalidas
                                    join tLib in _db.Liberaciones on tOS.Id equals tLib.IdOrdenSalida
                                    join tClieAux in _db.catClientes on tLib.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                    from tClie in tClieTemp.DefaultIfEmpty()
                                    join tOSIAux in _db.OrdenSalidaInventarios on tOS.Id equals tOSIAux.IdOrdenSalida into tOSITemp
                                    from tOSI in tOSITemp.DefaultIfEmpty()
                                    join tLibInvAux in _db.LiberacionInventarios on tOSI.Id equals tLibInvAux.IdOrdenSalidaInventario into tLibInvTemp
                                    from tLibInv in tLibInvTemp.DefaultIfEmpty()
                                    join tPartAux in _db.Partidas on tOSI.IdInventario equals tPartAux.IdInventario into tPartTemp
                                    from tPart in tPartTemp.DefaultIfEmpty()
                                    join tTSAux in _db.Tarjas on tLibInv.IdTarja equals tTSAux.Id into tTSTemp
                                    from tTS in tTSTemp.DefaultIfEmpty()
                                    join tSalidaCtrlTransAux in _db.SalidaControlTransportes on tOS.Id equals tSalidaCtrlTransAux.IdOrdenSalida into tSalidaCtrlTransTemp
                                    from tSalidaCtrlTrans in tSalidaCtrlTransTemp.DefaultIfEmpty()
                                    join tCtrlTransAux in _db.ControlTransportes on tSalidaCtrlTrans.IdControlTransporte equals tCtrlTransAux.Id into tCtrlTransTemp
                                    from tCtrlTrans in tCtrlTransTemp.DefaultIfEmpty()
                                    /*----------------------------------------------------*/
                                    join eAux in _db.Maniobristas on tCtrlTrans.IdManiobristaOrigen equals eAux.Id into eTemp
                                    from e in eTemp.DefaultIfEmpty()
                                    join fAux in _db.Maniobristas on tCtrlTrans.IdManiobristaDestino equals fAux.Id into fTemp
                                    from f in fTemp.DefaultIfEmpty()
                                    join gAux in _db.LineaTransportes on tCtrlTrans.IdLineaTransTransporte equals gAux.Id into gTemp
                                    from g in gTemp.DefaultIfEmpty()
                                    join hAux in _db.LineaOperadores on tCtrlTrans.IdLineaTransOperador equals hAux.Id into hTemp
                                    from h in hTemp.DefaultIfEmpty()
                                    join iAux in _db.catTransportistas on g.IdCatTransportista equals iAux.IdCatTransportista into iTemp
                                    from i in iTemp.DefaultIfEmpty()
                                    join jAux in _db.TipoTransportes on tCtrlTrans.IdTipoTransporte equals jAux.Id into jTemp
                                    from j in jTemp.DefaultIfEmpty()
                                    join kAux in _db.catTransportistas on tCtrlTrans.IdCatTransportista equals kAux.IdCatTransportista into kTemp
                                    from k in kTemp.DefaultIfEmpty()
                                    /*----------------------------------------------------*/
                                    group tOS by new
                                    {
                                        tOS.Id,
                                        tOS.FechaAlta,
                                        tOS.Folio,
                                        tOS.Estado,
                                        IdCliente = tClie.IdCatCliente,
                                        Cliente = tClie.RazonSocial,
                                        IdTarjaSalida = tTS.Id != null ? tTS.Id : 0,
                                        FolioTarja = tTS.Folio != null ? tTS.Folio : 0,
                                        FolioReferenciaTarja = tTS.ClaveReferencia != null ? tTS.ClaveReferencia : String.Empty,
                                        IdControlTransporte = tCtrlTrans.Id != null ? tCtrlTrans.Id : 0,
                                        FolioViaje = tCtrlTrans.Folio != null ? tCtrlTrans.Folio : 0,
                                        ClaveViaje = tCtrlTrans.Viajes != null ? tCtrlTrans.Viajes : String.Empty,
                                        NumerosVIN = String.IsNullOrEmpty(tPart.Numeros) ? String.Empty : tPart.Numeros,
                                    } into grLiberacion
                                    where
                                        grLiberacion.Key.FechaAlta >= entidad.Entidad.FechaInicio.Date.Add(DateTime.MinValue.TimeOfDay) &&
                                        grLiberacion.Key.FechaAlta <= entidad.Entidad.FechaFin.Date.Add(DateTime.MaxValue.TimeOfDay) &&
                                        grLiberacion.Key.NumerosVIN.Contains(String.IsNullOrEmpty(entidad.Entidad.IdMercancia) ? grLiberacion.Key.NumerosVIN : entidad.Entidad.IdMercancia) &&
                                        grLiberacion.Key.Estado.Equals(entidad.Entidad.EstadoLiberacion.Equals(EstadoLiberacion.None) ? grLiberacion.Key.Estado : entidad.Entidad.EstadoLiberacion )
                                    select new MonitorLiberacionInventario
                                    {
                                        IdLiberacion = grLiberacion.Key.Id,
                                        FolioLiberacion = grLiberacion.Key.Folio,
                                        EstadoLiberacion = grLiberacion.Key.Estado,
                                        IdCatCliente = grLiberacion.Key.IdCliente,
                                        Cliente = grLiberacion.Key.Cliente,
                                        IdTarja = grLiberacion.Key.IdTarjaSalida,
                                        FolioTarja = grLiberacion.Key.Folio,
                                        ClaveReferenciaTarja = grLiberacion.Key.FolioReferenciaTarja,
                                        IdControlTransporte = grLiberacion.Key.IdControlTransporte,
                                        FolioViaje = grLiberacion.Key.FolioViaje,
                                        ClaveViaje = grLiberacion.Key.ClaveViaje,
                                        FechaAlta = (DateTime)grLiberacion.Key.FechaAlta,
                                        Cantidad = grLiberacion.Key.Estado.Equals(EstadoLiberacion.Cancelada) ? 0 : grLiberacion.Count(),
                                    }).Distinct().ToList();

        _totalRegistros = listLiberacionInventario.Count;

        listLiberacionInventario = listLiberacionInventario.OrderByDescending(x => x.FolioLiberacion).ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        //listadoTarjasTemp = AddItems(listadoTarjas);

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorLiberacionInventario>(listLiberacionInventario, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorLiberacionInventario> ObtenerPorId(int idOrdenSalida)
    {
        throw new NotImplementedException();
    }

    public ResultBase AutorizarLiberacion(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        throw new NotImplementedException();
    }

    public ResultBase SalidaAlmacen(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var _resultBase = new ResultBase();

        DateTime _fechaSalida = DateTime.Now;
        
        MonitorLiberacionInventario _liberacionInventario = (from tOS in _db.OrdenSalidas
                                    join tLib in _db.Liberaciones on tOS.Id equals tLib.IdOrdenSalida
                                    join tClieAux in _db.catClientes on tLib.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                    from tClie in tClieTemp.DefaultIfEmpty()
                                    join tOSIAux in _db.OrdenSalidaInventarios on tOS.Id equals tOSIAux.IdOrdenSalida into tOSITemp
                                    from tOSI in tOSITemp.DefaultIfEmpty()
                                    join tLibInvAux in _db.LiberacionInventarios on tOSI.Id equals tLibInvAux.IdOrdenSalidaInventario into tLibInvTemp
                                    from tLibInv in tLibInvTemp.DefaultIfEmpty()
                                    join tTSAux in _db.Tarjas on tLibInv.IdTarja equals tTSAux.Id into tTSTemp
                                    from tTS in tTSTemp.DefaultIfEmpty()
                                    join tSalidaCtrlTransAux in _db.SalidaControlTransportes on tOS.Id equals tSalidaCtrlTransAux.IdOrdenSalida into tSalidaCtrlTransTemp
                                    from tSalidaCtrlTrans in tSalidaCtrlTransTemp.DefaultIfEmpty()
                                    join tCtrlTransAux in _db.ControlTransportes on tSalidaCtrlTrans.IdControlTransporte equals tCtrlTransAux.Id into tCtrlTransTemp
                                    from tCtrlTrans in tCtrlTransTemp.DefaultIfEmpty()
                                    group tOS by new
                                    {
                                        tOS.Id,
                                        tOS.FechaAlta,
                                        tOS.Folio,
                                        tOS.Estado,
                                        IdCliente = tClie.IdCatCliente,
                                        Cliente = tClie.RazonSocial,
                                        IdTarjaSalida = tTS.Id != null ? tTS.Id : 0,
                                        FolioTarja = tTS.Folio != null ? tTS.Folio : 0,
                                        FolioReferenciaTarja = tTS.ClaveReferencia != null ? tTS.ClaveReferencia : String.Empty,
                                        IdControlTransporte = tCtrlTrans.Id != null ? tCtrlTrans.Id : 0,
                                        FolioViaje = tCtrlTrans.Folio != null ? tCtrlTrans.Folio : 0,
                                        ClaveViaje = tCtrlTrans.Viajes != null ? tCtrlTrans.Viajes : String.Empty,
                                    } into grLiberacion
                                    where
                                        grLiberacion.Key.Id.Equals(consultaLiberacionInventario.IdOrdenSalida)
                                        && grLiberacion.Key.Estado.Equals(EstadoLiberacion.Pendiente)
                                    select new MonitorLiberacionInventario
                                                             {
                                                                 IdLiberacion = grLiberacion.Key.Id,
                                                                 FolioLiberacion = grLiberacion.Key.Folio,
                                                                 EstadoLiberacion = grLiberacion.Key.Estado,
                                                                 IdCatCliente = grLiberacion.Key.IdCliente,
                                                                 Cliente = grLiberacion.Key.Cliente,
                                                                 IdTarja = grLiberacion.Key.IdTarjaSalida,
                                                                 FolioTarja = grLiberacion.Key.Folio,
                                                                 ClaveReferenciaTarja = grLiberacion.Key.FolioReferenciaTarja,
                                                                 IdControlTransporte = grLiberacion.Key.IdControlTransporte,
                                                                 FolioViaje = grLiberacion.Key.FolioViaje,
                                                                 ClaveViaje = grLiberacion.Key.ClaveViaje,
                                                                 FechaAlta = (DateTime)grLiberacion.Key.FechaAlta,
                                                             }).SingleOrDefault();

        if (_liberacionInventario != null)
        {

            if (!_liberacionInventario.IdControlTransporte.Equals(0))
            {

                using (var _dbContextTransaction = _db.Database.BeginTransaction())
                {

                    try
                    {
                        // Se obtienen datos del inventario
                        var _listaOrdenSalidaInventario = (from tOS in _db.OrdenSalidas
                                                           join tLib in _db.Liberaciones on tOS.Id equals tLib.IdOrdenSalida
                                                           join tClieAux in _db.catClientes on tLib.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                                           from tClie in tClieTemp.DefaultIfEmpty()
                                                           join tOSI in _db.OrdenSalidaInventarios on tOS.Id equals tOSI.IdOrdenSalida
                                                           join tLibInv in _db.LiberacionInventarios on tOSI.Id equals tLibInv.IdOrdenSalidaInventario
                                                           join tTS in _db.Tarjas on tLibInv.IdTarja equals tTS.Id
                                                           join tPartida in _db.Partidas on tOSI.IdInventario equals tPartida.IdInventario
                                                           join tInv in _db.Inventarios on tPartida.IdInventario equals tInv.Id
                                                           where tOS.Id.Equals(consultaLiberacionInventario.IdOrdenSalida)
                                                           && tOS.Estado.Equals(EstadoLiberacion.Pendiente)
                                                           select new
                                                           {
                                                               IdInventario = tInv.Id,
                                                               IdOrdenSalidaInventario = tOSI.Id,
                                                               IdLiberacion = tOS.Id
                                                           }).ToList();


                        if (!_listaOrdenSalidaInventario.Count.Equals(0))
                        {

                            // Se obtiene la información de la liberación.
                            var _liberacionOrdenSalida = _db.OrdenSalidas.Where(x => x.Id.Equals(_liberacionInventario.IdLiberacion)).SingleOrDefault();
                            _liberacionOrdenSalida.FechaSalidaProgramada = _fechaSalida;
                            _liberacionOrdenSalida.Estado = EstadoLiberacion.Cerrada;
                            _db.OrdenSalidas.Update(_liberacionOrdenSalida);
                            _db.SaveChanges();

                            // Se obtiene la información de la tarja de salida.
                            var _tarjaSalida = _db.Tarjas.Where(x => x.Id.Equals(_liberacionInventario.IdTarja)).SingleOrDefault();
                            _tarjaSalida.Estado = EstadoTarja.Confirmado;
                            _db.Tarjas.Update(_tarjaSalida);
                            _db.SaveChanges();

                            // Se obtiene la información de controlTransporte.
                            var _controlTransporte = _db.ControlTransportes.Where(x => x.Id.Equals(_liberacionInventario.IdControlTransporte)).SingleOrDefault();
                            _controlTransporte.Estado = EstadoTurno.Completado;
                            _db.ControlTransportes.Update(_controlTransporte);
                            _db.SaveChanges();                    

                            foreach (var itemOSI in _listaOrdenSalidaInventario)
                            {

                                // Obtiene información del inventario
                                Inventario _inventario = new Inventario();
                                _inventario = _db.Inventarios.Where(i => i.Id.Equals(itemOSI.IdInventario)).SingleOrDefault();

                                // Verifica que la fecha de embarque
                                if (_inventario.FechaEmbarque != null)
                                {

                                    // Verifica que la fecha de salida no se haya registrado anteriormente.
                                    if (_inventario.FechaSalida == null)
                                    {
                                        // Se guarda la fecha de Salida
                                        _inventario.Estado = EstadoInventario.Liberado;
                                        _inventario.IdUbicacion = null;
                                        _inventario.Existencia = false;
                                        _inventario.FechaSalida = _fechaSalida;
                                        _db.Update(_inventario);
                                        _db.SaveChanges();

                                    }
                                    else
                                    {
                                        _resultBase.MensajeRespuesta = "Ya cuenta con fecha de salida. Verifique la información.";
                                    }

                                }
                                else
                                {
                                    throw new Exception("La mercancía asociada a la liberación tiene que completar el embarque, para poder dar salida.");
                                }

                            }
                        }
                        else
                        {
                            _resultBase.MensajeRespuesta = "No se puede dar Salida a la Mercancía. Verifique la información.";
                        }

                    }
                    catch (Exception ex)
                    {
                        _resultBase.MensajeRespuesta = "Error: " + ex.Message;
                        _dbContextTransaction.Rollback();

                    }

                    if (_resultBase.Success)
                    {
                        _dbContextTransaction.Commit();
                    }

                }
            }
            else
            {
                _resultBase.MensajeRespuesta = "Verifique los datos de la Liberación. Aún no tiene asociado un Viaje.";
            }

        }
        else
        {
            _resultBase.MensajeRespuesta = "Verifique los datos de la Liberación. No se encontró información para dar Salida.";
        }

        return _resultBase;

    }

    public ResultBase ControlEmbarqueAlmacen(ConsultaLiberacionInventario consultaLiberacionInventario)
    {
        var _resultBase = new ResultBase();

        DateTime _fechaEmbarque = DateTime.Now;

        using (var _dbContextTransaction = _db.Database.BeginTransaction())
        {

            try
            {
                // Se obtienen datos del inventario

                var _controlEmbarqueInventario = (from tOS in _db.OrdenSalidas
                                                  join tLib in _db.Liberaciones on tOS.Id equals tLib.IdOrdenSalida
                                                  join tClieAux in _db.catClientes on tLib.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                                  from tClie in tClieTemp.DefaultIfEmpty()
                                                  join tOSI in _db.OrdenSalidaInventarios on tOS.Id equals tOSI.IdOrdenSalida
                                                  join tLibInv in _db.LiberacionInventarios on tOSI.Id equals tLibInv.IdOrdenSalidaInventario
                                                  join tTS in _db.Tarjas on tLibInv.IdTarja equals tTS.Id
                                                  join tPartida in _db.Partidas on tOSI.IdInventario equals tPartida.IdInventario
                                                  join tInv in _db.Inventarios on tPartida.IdInventario equals tInv.Id
                                                  where tOS.Id.Equals(consultaLiberacionInventario.IdOrdenSalida)
                                                  && tOS.Estado.Equals(EstadoLiberacion.Pendiente)
                                                  && tPartida.Numeros.Equals(consultaLiberacionInventario.IdMercancia)
                                                  select new
                                                  {
                                                      IdInventario = tInv.Id,
                                                      IdOrdenSalidaInventario = tOSI.Id
                                                  }).SingleOrDefault();

                if (_controlEmbarqueInventario != null)
                {
                    // Obtiene información del inventario
                    Inventario _inventario = new Inventario();
                    _inventario = _db.Inventarios.Where(i => i.Id.Equals(_controlEmbarqueInventario.IdInventario)).SingleOrDefault();

                    // Verifica que la fecha de embarque no se haya registrado anteriormente.
                    if (_inventario.FechaEmbarque == null)
                    {
                        // Se guarda la fecha de embarque
                        _inventario.FechaEmbarque = _fechaEmbarque;
                        _db.Update(_inventario);
                        _db.SaveChanges();

                    }
                    else
                    {
                        _resultBase.MensajeRespuesta = "Ya cuenta con fecha de embarque. Verifique la información.";
                    }
                }
                else
                {
                    _resultBase.MensajeRespuesta = "No se cuenta el identificador de la mercancía. Verifique la información.";
                }

            }
            catch (Exception ex)
            {
                _resultBase.MensajeRespuesta = "Error: " + ex.Message;
                _dbContextTransaction.Rollback();

            }

            _dbContextTransaction.Commit();

        }

        return _resultBase;
    }

    public PaginadoResult<MonitorLiberacionInventario> ObtenerLiberacionControlEmbarqueListaPaginada(ConsultaCatalogoBase<ConsultaControlEmbarque> entidad)
    {
        var resultBase = new ResultBase<MonitorLiberacionInventario>();
        List<MonitorLiberacionInventario> listLiberacionInventario = new List<MonitorLiberacionInventario>();

        int _totalRegistros = 0;

        string _FolioViaje = String.Empty;
        string _TarjaSalida = String.Empty;

        if (entidad.Entidad.TipoBusquedaEmbarque.Equals(TipoBusquedaEmbarque.TarjaSalida))
        {
            _TarjaSalida = entidad.Entidad.ValorBusqueda;
        }
        else if (entidad.Entidad.TipoBusquedaEmbarque.Equals(TipoBusquedaEmbarque.Viaje))
        {
            _FolioViaje = entidad.Entidad.ValorBusqueda;
        }

        // TODO: Consulta de Liberaciones listas para Salida de Almacen.
        // ------------------------------------------

        listLiberacionInventario = (from tOS in _db.OrdenSalidas
                                    join tLib in _db.Liberaciones on tOS.Id equals tLib.IdOrdenSalida
                                    join tClieAux in _db.catClientes on tLib.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                    from tClie in tClieTemp.DefaultIfEmpty()
                                    join tOSIAux in _db.OrdenSalidaInventarios on tOS.Id equals tOSIAux.IdOrdenSalida into tOSITemp
                                    from tOSI in tOSITemp.DefaultIfEmpty()
                                    join tLibInvAux in _db.LiberacionInventarios on tOSI.Id equals tLibInvAux.IdOrdenSalidaInventario into tLibInvTemp
                                    from tLibInv in tLibInvTemp.DefaultIfEmpty()
                                    join tTSAux in _db.Tarjas on tLibInv.IdTarja equals tTSAux.Id into tTSTemp
                                    from tTS in tTSTemp.DefaultIfEmpty()
                                    join tSalidaCtrlTransAux in _db.SalidaControlTransportes on tOS.Id equals tSalidaCtrlTransAux.IdOrdenSalida into tSalidaCtrlTransTemp
                                    from tSalidaCtrlTrans in tSalidaCtrlTransTemp.DefaultIfEmpty()
                                    join tCtrlTransAux in _db.ControlTransportes on tSalidaCtrlTrans.IdControlTransporte equals tCtrlTransAux.Id into tCtrlTransTemp
                                    from tCtrlTrans in tCtrlTransTemp.DefaultIfEmpty()
                                    /*----------------------------------------------------*/
                                    join eAux in _db.Maniobristas on tCtrlTrans.IdManiobristaOrigen equals eAux.Id into eTemp
                                    from e in eTemp.DefaultIfEmpty()
                                    join fAux in _db.Maniobristas on tCtrlTrans.IdManiobristaDestino equals fAux.Id into fTemp
                                    from f in fTemp.DefaultIfEmpty()
                                    join gAux in _db.LineaTransportes on tCtrlTrans.IdLineaTransTransporte equals gAux.Id into gTemp
                                    from g in gTemp.DefaultIfEmpty()
                                    join hAux in _db.LineaOperadores on tCtrlTrans.IdLineaTransOperador equals hAux.Id into hTemp
                                    from h in hTemp.DefaultIfEmpty()
                                    join iAux in _db.catTransportistas on g.IdCatTransportista equals iAux.IdCatTransportista into iTemp
                                    from i in iTemp.DefaultIfEmpty()
                                    join jAux in _db.TipoTransportes on tCtrlTrans.IdTipoTransporte equals jAux.Id into jTemp
                                    from j in jTemp.DefaultIfEmpty()
                                    join kAux in _db.catTransportistas on tCtrlTrans.IdCatTransportista equals kAux.IdCatTransportista into kTemp
                                    from k in kTemp.DefaultIfEmpty()
                                    /*----------------------------------------------------*/
                                    group tOS by new
                                    {
                                        tOS.Id,
                                        tOS.FechaAlta,
                                        tOS.Folio,
                                        tOS.Estado,
                                        IdCliente = tClie.IdCatCliente,
                                        Cliente = tClie.RazonSocial,
                                        IdTarjaSalida = tTS.Id != null ? tTS.Id : 0,
                                        FolioTarja = tTS.Folio != null ? tTS.Folio : 0,
                                        FolioReferenciaTarja = String.IsNullOrEmpty(tTS.ClaveReferencia) ? String.Empty : tTS.ClaveReferencia,
                                        IdControlTransporte = tCtrlTrans.Id != null ? tCtrlTrans.Id : 0,
                                        FolioViaje = tCtrlTrans.Folio != null ? tCtrlTrans.Folio : 0,
                                        ClaveViaje = tCtrlTrans.Viajes != null ? tCtrlTrans.Viajes : String.Empty,
                                        Transportista = String.IsNullOrEmpty(i.RazonSocial) ? String.IsNullOrEmpty(k.RazonSocial) ? String.Empty : k.RazonSocial : i.RazonSocial,
                                        NombreOperador = String.IsNullOrEmpty(h.Nombre) ? String.IsNullOrEmpty(tCtrlTrans.NombreOperador) ? String.Empty : tCtrlTrans.NombreOperador : (h.Nombre + "" + h.ApellidoPaterno),
                                        Placas = String.IsNullOrEmpty(g.Placas) ? String.IsNullOrEmpty(tCtrlTrans.Placas) ? String.Empty : tCtrlTrans.Placas : g.Placas,
                                    } into grLiberacion
                                    where
                                        grLiberacion.Key.ClaveViaje.Equals(String.IsNullOrEmpty(_FolioViaje) ? grLiberacion.Key.ClaveViaje : _FolioViaje)
                                        && grLiberacion.Key.FolioReferenciaTarja.Contains(String.IsNullOrEmpty(_TarjaSalida) ? grLiberacion.Key.FolioReferenciaTarja : _TarjaSalida)
                                    select new MonitorLiberacionInventario
                                    {
                                        IdLiberacion = grLiberacion.Key.Id,
                                        FolioLiberacion = grLiberacion.Key.Folio,
                                        EstadoLiberacion = grLiberacion.Key.Estado,
                                        IdCatCliente = grLiberacion.Key.IdCliente,
                                        Cliente = grLiberacion.Key.Cliente,
                                        IdTarja = grLiberacion.Key.IdTarjaSalida,
                                        FolioTarja = grLiberacion.Key.Folio,
                                        ClaveReferenciaTarja = grLiberacion.Key.FolioReferenciaTarja,
                                        IdControlTransporte = grLiberacion.Key.IdControlTransporte,
                                        FolioViaje = grLiberacion.Key.FolioViaje,
                                        ClaveViaje = grLiberacion.Key.ClaveViaje,
                                        LineaTransportista = grLiberacion.Key.Transportista,
                                        OperadorTransporte = grLiberacion.Key.NombreOperador,
                                        Placas = grLiberacion.Key.Placas,
                                        FechaAlta = (DateTime)grLiberacion.Key.FechaAlta,
                                    }).ToList();

        _totalRegistros = listLiberacionInventario.Count();

        if (!listLiberacionInventario.Count.Equals(0))
        {
            var _monitorLiberacionInventario = listLiberacionInventario.Take(1).ToList();

            ConsultaCatalogoBase<ConsultaLiberacionInventario> _consultaLibInv = new ConsultaCatalogoBase<ConsultaLiberacionInventario>();
            _consultaLibInv.Entidad = new ConsultaLiberacionInventario();
            _consultaLibInv.Entidad.IdOrdenSalida = _monitorLiberacionInventario[0].IdLiberacion;
            var _listaDetalleLiberacionInventario = ObtenerDetalleLiberacionPorIdOrdenSalida(_consultaLibInv);

            listLiberacionInventario.Select(p => { p.TotalInventarioLiberacion = _listaDetalleLiberacionInventario.Data.Count() ; return p; }).ToList();
            listLiberacionInventario.Select(p => { p.TotalInventarioEmbarcado = _listaDetalleLiberacionInventario.Data.Where(x => !x.FechaEmbarque.Equals(DateTime.MinValue)).Count() ; return p; }).ToList();

        }           

        listLiberacionInventario = listLiberacionInventario.OrderByDescending(x => x.FolioLiberacion).ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorLiberacionInventario>(listLiberacionInventario, paginadoInfo);

        return _paginadoResult;

    }
}
