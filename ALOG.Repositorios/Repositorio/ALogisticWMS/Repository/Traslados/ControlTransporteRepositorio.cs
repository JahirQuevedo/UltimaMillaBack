using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NPOI.OpenXmlFormats.Dml;

namespace ALOG.Repositorios;

public class ControlTransporteRepositorio : GenericoRepositorio<ControlTransporte>, IControlTransporteRepositorio
{
    private readonly ApplicationDbContext _db;

    public ControlTransporteRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorCtrlTransporte> Guardar(MonitorCtrlTransporte monitorControlTransporte)
    {
        string folioTemp = DateTime.Now.ToString("yy");
        var resultBase = new ResultBase<MonitorCtrlTransporte>();

        using (var _dbContextTransaction = _db.Database.BeginTransaction())
        {

            try
            {

                ControlTransporte _controlTransporte = new ControlTransporte();

                if (!monitorControlTransporte.IdControlTransporte.Equals(0))
                {
                    _controlTransporte = _db.ControlTransportes.Where(x => x.Id.Equals(monitorControlTransporte.IdControlTransporte)).SingleOrDefault();
                }
                else
                {
                    monitorControlTransporte.Estado = EstadoTurno.Creado;
                }

                _controlTransporte.IdManiobristaOrigen = monitorControlTransporte.IdManiobristaOrigen.Equals(0) ? _controlTransporte.IdManiobristaOrigen : monitorControlTransporte.IdManiobristaOrigen;
                _controlTransporte.IdManiobristaDestino = monitorControlTransporte.IdManiobristaDestino.Equals(0) ? _controlTransporte.IdManiobristaDestino : monitorControlTransporte.IdManiobristaDestino;
                _controlTransporte.Estado = monitorControlTransporte.Estado;
                _controlTransporte.TipoEntrada = TipoEntrada.Carretero;
                _controlTransporte.TipoViaje = monitorControlTransporte.TipoViaje;
                _controlTransporte.FechaSalida = !monitorControlTransporte.FechaInicio.Equals(DateTime.MinValue) ? monitorControlTransporte.FechaInicio : _controlTransporte.FechaSalida;
                _controlTransporte.FechaLlegada = !monitorControlTransporte.FechaInicio.Equals(DateTime.MinValue) ? monitorControlTransporte.FechaFin : _controlTransporte.FechaLlegada;
                _controlTransporte.IdTipoTransporte = monitorControlTransporte.IdTipoTransporte.Equals(0) ? _controlTransporte.IdTipoTransporte : monitorControlTransporte.IdTipoTransporte;
                _controlTransporte.Viajes = monitorControlTransporte.ClaveViaje;
                _controlTransporte.IdCatTransportista = monitorControlTransporte.IdCatTransportista.Equals(0) ? _controlTransporte.IdCatTransportista : monitorControlTransporte.IdCatTransportista;
                _controlTransporte.IdLineaTransTransporte = monitorControlTransporte.IdLineaTransTransporte.Equals(0) ? _controlTransporte.IdLineaTransTransporte : monitorControlTransporte.IdLineaTransTransporte;
                _controlTransporte.IdLineaTransOperador = monitorControlTransporte.IdLineaTransOperador.Equals(0) ? _controlTransporte.IdLineaTransTransporte : monitorControlTransporte.IdLineaTransOperador;
                _controlTransporte.NombreOperador = monitorControlTransporte.NombreOperadorLineaTransporte;
                _controlTransporte.Placas = monitorControlTransporte.PlacasLineaTransporte;
                _controlTransporte.NumeroEconomico = monitorControlTransporte.NumEcoLineaTransporte;
                _controlTransporte.TerminalOrigen = monitorControlTransporte.ManiobristaOrigen != null ? monitorControlTransporte.ManiobristaOrigen : _controlTransporte.TerminalOrigen;
                _controlTransporte.TerminalDestino = monitorControlTransporte.ManiobristaDestino != null ? monitorControlTransporte.ManiobristaDestino : _controlTransporte.TerminalDestino;
                _controlTransporte.Observacion = monitorControlTransporte.Observaciones;
                _controlTransporte.IdEmpresa = (int)monitorControlTransporte.IdEmpresaLogin;

                if (monitorControlTransporte.CRUDAction.Equals(ECRUDAction.Create))
                {

                    _db.ControlTransportes.Add(_controlTransporte);

                }
                else if (monitorControlTransporte.CRUDAction.Equals(ECRUDAction.Update))
                {

                    _controlTransporte.Id = monitorControlTransporte.IdControlTransporte;

                    _db.ControlTransportes.Update(_controlTransporte);

                }

                _db.SaveChanges();

                if (monitorControlTransporte.CRUDAction.Equals(ECRUDAction.Create))
                {
                    folioTemp = folioTemp + _controlTransporte.Id.ToString().PadLeft(7, '0');

                    _controlTransporte.Folio = Convert.ToInt32(folioTemp);
                    monitorControlTransporte.FolioTurno = Convert.ToInt32(folioTemp);

                    _db.ControlTransportes.Update(_controlTransporte);
                    _db.SaveChanges();

                    // Si es una salida, se obtiene la liberación y el control de trasnporte para crear la salida de transporte.
                    if (!monitorControlTransporte.IdOrdenSalida.Equals(0) && monitorControlTransporte.TipoViaje.Equals(TipoViaje.Salida))
                    {
                        SalidaControlTransporte _salidaControlTransporte = new SalidaControlTransporte();
                        _salidaControlTransporte.IdControlTransporte = _controlTransporte.Id;
                        _salidaControlTransporte.IdOrdenSalida = monitorControlTransporte.IdOrdenSalida;

                        _db.SalidaControlTransportes.Add(_salidaControlTransporte);
                        _db.SaveChanges();
                    }

                    if (!monitorControlTransporte.IdSolicitudTraslado.Equals(0) && monitorControlTransporte.TipoViaje.Equals(TipoViaje.Entrada))
                    {
                        SolicitudTraslado _solicitudTraslado = new SolicitudTraslado();
                        _solicitudTraslado = _db.SolicitudTraslados.Where(s => s.Id.Equals(monitorControlTransporte.IdSolicitudTraslado)).SingleOrDefault();
                        _solicitudTraslado.IdControlTransporte = _controlTransporte.Id;
                        _solicitudTraslado.EstadoTraslado = EstadoTraslado.Asignado;
                        _db.SolicitudTraslados.Update(_solicitudTraslado);
                        _db.SaveChanges();
                    }
                    
                }

                monitorControlTransporte.IdControlTransporte = _controlTransporte.Id;
                resultBase.Id = _controlTransporte.Id;
                resultBase.Data = monitorControlTransporte;

            }
            catch (Exception ex)
            {

                resultBase.MensajeRespuesta = "Error: " + ex.Message;
                _dbContextTransaction.Rollback();
            }

            _dbContextTransaction.Commit();


        } // proceso database transaction

        return resultBase;
    }

    public ResultBase<MonitorCtrlTransporte> GuardarRelacionSolicitudTraslado(MonitorCtrlTransporte monitorControlTransporte)
    {
        var resultBase = new ResultBase<MonitorCtrlTransporte>();
        ControlTransporte _controlTransporte = new ControlTransporte();
        SolicitudTraslado _solicitudTraslado = new SolicitudTraslado();

        if (!monitorControlTransporte.IdControlTransporte.Equals(0))
        {
            _controlTransporte = _db.ControlTransportes.Where(x => x.Id.Equals(monitorControlTransporte.IdControlTransporte)).SingleOrDefault();

            using (var _dbContextTransaction = _db.Database.BeginTransaction())
            {

                try
                {

                    foreach (var item in monitorControlTransporte.listaSolicitudTraslado)
                    {
                        _solicitudTraslado = new SolicitudTraslado();
                        _solicitudTraslado = _db.SolicitudTraslados.Where(x => x.Id.Equals(item.IdSolicitudTraslado)).SingleOrDefault();
                        if (_solicitudTraslado != null)
                        {

                            _solicitudTraslado.IdControlTransporte = _controlTransporte.Id;
                            _solicitudTraslado.EstadoTraslado = EstadoTraslado.Asignado;
                            _db.SolicitudTraslados.Update(_solicitudTraslado);

                            _db.SaveChanges();

                        }
                    }

                }
                catch (Exception ex)
                {

                    resultBase.MensajeRespuesta = "Error: " + ex.Message;
                    _dbContextTransaction.Rollback();
                }

                _dbContextTransaction.Commit();


            } // proceso database transaction

        }
        else
        {
            resultBase.MensajeRespuesta = "Falta declarar identificador del Control Transporte";
        }

        return resultBase;
    }

    public PaginadoResult<MonitorCtrlTransporte> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaCtrlTransporte> entidad)
    {
        var resultBase = new ResultBase<MonitorCtrlTransporte>();
        List<MonitorCtrlTransporte> _listControlTransporte = new List<MonitorCtrlTransporte>();

        int _totalRegistros = 0;

        _listControlTransporte = (from p in _db.ControlTransportes
                join eAux in _db.Maniobristas on p.IdManiobristaOrigen equals eAux.Id into eTemp
                from e in eTemp.DefaultIfEmpty()
                join fAux in _db.Maniobristas on p.IdManiobristaDestino equals fAux.Id into fTemp
                from f in fTemp.DefaultIfEmpty()
                join gAux in _db.LineaTransportes on p.IdLineaTransTransporte equals gAux.Id into gTemp
                from g in gTemp.DefaultIfEmpty()
                join hAux in _db.LineaOperadores on p.IdLineaTransOperador equals hAux.Id into hTemp
                from h in hTemp.DefaultIfEmpty()
                join iAux in _db.catTransportistas on g.IdCatTransportista equals iAux.IdCatTransportista into iTemp
                from i in iTemp.DefaultIfEmpty()
                join jAux in _db.TipoTransportes on p.IdTipoTransporte equals jAux.Id into jTemp
                from j in jTemp.DefaultIfEmpty()
                join kAux in _db.catTransportistas on p.IdCatTransportista equals kAux.IdCatTransportista into kTemp
                from k in kTemp.DefaultIfEmpty()
                where p.FechaAlta >= entidad.Entidad.FechaInicio.Date.Add(DateTime.MinValue.TimeOfDay) &&
                    p.FechaAlta <= entidad.Entidad.FechaFin.Date.Add(DateTime.MaxValue.TimeOfDay) &&
                    p.Estado == (entidad.Entidad.EstadoTurno.Equals(EstadoTurno.None) ? p.Estado : entidad.Entidad.EstadoTurno) &&
                    p.TipoViaje == (entidad.Entidad.TipoViaje.Equals(TipoViaje.None) ? p.TipoViaje : entidad.Entidad.TipoViaje)  &&
                    (g.Placas != null ? g.Placas : (String.IsNullOrEmpty(p.Placas) ? String.Empty : p.Placas)).Contains(String.IsNullOrEmpty(entidad.Entidad.Placas) ? String.Empty : entidad.Entidad.Placas) &&
                    (g.NumeroEconomico != null ? g.NumeroEconomico : (String.IsNullOrEmpty(p.NumeroEconomico) ? String.Empty : p.NumeroEconomico)).Contains(String.IsNullOrEmpty(entidad.Entidad.NumeroEconomico) ? String.Empty : entidad.Entidad.NumeroEconomico) &&
                    (e.NombreCorto != null ? e.NombreCorto : String.IsNullOrEmpty(p.TerminalOrigen) ? String.Empty : p.TerminalOrigen).Contains(String.IsNullOrEmpty(entidad.Entidad.TerminalOrigen) ? String.Empty : entidad.Entidad.TerminalOrigen) &&
                    (f.NombreCorto != null ? f.NombreCorto : (String.IsNullOrEmpty(p.TerminalDestino) ? String.Empty : p.TerminalDestino)).Contains(String.IsNullOrEmpty(entidad.Entidad.TerminalDestino) ? String.Empty : entidad.Entidad.TerminalDestino) &&
                    (i.RazonSocial != null ? i.RazonSocial : (k.RazonSocial != null ? k.RazonSocial : String.Empty)).Contains(String.IsNullOrEmpty(entidad.Entidad.NombreTransportista) ? String.Empty : entidad.Entidad.NombreTransportista)
                select new MonitorCtrlTransporte
                {
                    IdControlTransporte = p.Id,
                    FolioTurno = p.Folio,
                    Estado = (EstadoTurno)p.Estado,
                    TipoViaje = (TipoViaje)p.TipoViaje,
                    ClaveViaje = p.Viajes != null ? p.Viajes : String.Empty,
                    TipoTransporte = j.Descripcion != null ? j.Descripcion : String.Empty,
                    IdTipoTransporte = (int)(p.IdTipoTransporte != null ? p.IdTipoTransporte : 0),
                    IdCatTransportista = (int)(g.IdCatTransportista != null ? g.IdCatTransportista : (p.IdCatTransportista != null ? p.IdCatTransportista : 0)),
                    IdLineaTransOperador = h.Id != null ? h.Id : 0,
                    NombreOperadorLineaTransporte = h.NombreCompleto != null ? h.NombreCompleto : (String.IsNullOrEmpty(p.NombreOperador) ? String.Empty : p.NombreOperador),
                    IdLineaTransTransporte = g.Id != null ? g.Id : 0,
                    RazonSocialLineaTransporte = i.RazonSocial != null ? i.RazonSocial : (k.RazonSocial != null ? k.RazonSocial : String.Empty),
                    PlacasLineaTransporte = g.Placas != null ? g.Placas : (String.IsNullOrEmpty(p.Placas) ? String.Empty : p.Placas),
                    NumEcoLineaTransporte = g.NumeroEconomico != null ? g.NumeroEconomico : (String.IsNullOrEmpty(p.NumeroEconomico) ? String.Empty : p.NumeroEconomico),
                    IdManiobristaOrigen = e.Id != null ? e.Id : 0,
                    ManiobristaOrigen = e.RazonSocial != null ? e.RazonSocial : (String.IsNullOrEmpty(p.TerminalOrigen) ? String.Empty : p.TerminalOrigen),
                    ManiobristaNombreCortoOrigen = e.NombreCorto != null ? e.NombreCorto : String.IsNullOrEmpty(p.TerminalOrigen) ? String.Empty : p.TerminalOrigen,
                    IdManiobristaDestino = f.Id != null ? f.Id : 0,
                    ManiobristaDestino = f.RazonSocial != null ? f.RazonSocial : (String.IsNullOrEmpty(p.TerminalDestino) ? String.Empty : p.TerminalDestino),
                    ManiobristaNombreCortoDestino = f.NombreCorto != null ? f.NombreCorto : (String.IsNullOrEmpty(p.TerminalDestino) ? String.Empty : p.TerminalDestino),
                    Observaciones = p.Observacion,
                    TieneTrasladosAsignado = _db.SolicitudTraslados.Where(x => x.IdControlTransporte.Equals(p.Id)).Any(),
                    FechaAlta = p.FechaAlta,
                    FechaInicio = (DateTime)((TipoViaje)p.TipoViaje == TipoViaje.Entrada ? (p.FechaSalida != null ? p.FechaSalida : DateTime.MinValue) : (p.FechaLlegada != null ? p.FechaLlegada : DateTime.MinValue)),
                    FechaFin = (DateTime)((TipoViaje)p.TipoViaje == TipoViaje.Entrada ? (p.FechaLlegada != null ? p.FechaLlegada : DateTime.MinValue) : (p.FechaSalida != null ? p.FechaSalida : DateTime.MinValue)),
                    IdEmpresaLogin = p.IdEmpresa
                }).ToList();

        _totalRegistros = _listControlTransporte.Count;

        _listControlTransporte = _listControlTransporte.OrderByDescending(x => x.FechaAlta).ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorCtrlTransporte>(_listControlTransporte, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorCtrlTransporte> ObtenerPorId(int id)
    {
        var resultBase = new ResultBase<MonitorCtrlTransporte>();
        MonitorCtrlTransporte monitorTarja = new MonitorCtrlTransporte();

        try
        {

            var _controlTransporte = (from p in _db.ControlTransportes
                                      join eAux in _db.Maniobristas on p.IdManiobristaOrigen equals eAux.Id into eTemp
                                      from e in eTemp.DefaultIfEmpty()
                                      join fAux in _db.Maniobristas on p.IdManiobristaDestino equals fAux.Id into fTemp
                                      from f in fTemp.DefaultIfEmpty()
                                      join gAux in _db.LineaTransportes on p.IdLineaTransTransporte equals gAux.Id into gTemp
                                      from g in gTemp.DefaultIfEmpty()
                                      join hAux in _db.LineaOperadores on p.IdLineaTransOperador equals hAux.Id into hTemp
                                      from h in hTemp.DefaultIfEmpty()
                                      join iAux in _db.catTransportistas on g.IdCatTransportista equals iAux.IdCatTransportista into iTemp
                                      from i in iTemp.DefaultIfEmpty()
                                      join jAux in _db.TipoTransportes on p.IdTipoTransporte equals jAux.Id into jTemp
                                      from j in jTemp.DefaultIfEmpty()
                                      join kAux in _db.catTransportistas on p.IdCatTransportista equals kAux.IdCatTransportista into kTemp
                                      from k in kTemp.DefaultIfEmpty()
                                      where p.Id.Equals(id)
                                      select new MonitorCtrlTransporte
                                      {
                                          IdControlTransporte = p.Id,
                                          FolioTurno = p.Folio,
                                          Estado = (EstadoTurno)p.Estado,
                                          TipoViaje = (TipoViaje)p.TipoViaje,
                                          ClaveViaje = p.Viajes != null ? p.Viajes : String.Empty,
                                          TipoTransporte = j.Descripcion != null ? j.Descripcion : String.Empty,
                                          IdTipoTransporte = (int)(p.IdTipoTransporte != null ? p.IdTipoTransporte : 0),
                                          IdCatTransportista = (int)(g.IdCatTransportista != null ? g.IdCatTransportista : (p.IdCatTransportista != null ? p.IdCatTransportista : 0)),
                                          IdLineaTransOperador = h.Id != null ? h.Id : 0,
                                          NombreOperadorLineaTransporte = h.NombreCompleto != null ? h.NombreCompleto : (String.IsNullOrEmpty(p.NombreOperador) ? String.Empty : p.NombreOperador),
                                          IdLineaTransTransporte = g.Id != null ? g.Id : 0,
                                          RazonSocialLineaTransporte = i.RazonSocial != null ? i.RazonSocial : (k.RazonSocial != null ? k.RazonSocial : String.Empty),
                                          PlacasLineaTransporte = g.Placas != null ? g.Placas : (String.IsNullOrEmpty(p.Placas) ? String.Empty : p.Placas),
                                          NumEcoLineaTransporte = g.NumeroEconomico != null ? g.NumeroEconomico : (String.IsNullOrEmpty(p.NumeroEconomico) ? String.Empty : p.NumeroEconomico),
                                          IdManiobristaOrigen = e.Id != null ? e.Id : 0,
                                          ManiobristaOrigen = e.RazonSocial != null ? e.RazonSocial : (String.IsNullOrEmpty(p.TerminalOrigen) ? String.Empty : p.TerminalOrigen),
                                          ManiobristaNombreCortoOrigen = e.NombreCorto != null ? e.NombreCorto : String.IsNullOrEmpty(p.TerminalOrigen) ? String.Empty : p.TerminalOrigen,
                                          IdManiobristaDestino = f.Id != null ? f.Id : 0,
                                          ManiobristaDestino = f.RazonSocial != null ? f.RazonSocial : (String.IsNullOrEmpty(p.TerminalDestino) ? String.Empty : p.TerminalDestino),
                                          ManiobristaNombreCortoDestino = f.NombreCorto != null ? f.NombreCorto : (String.IsNullOrEmpty(p.TerminalDestino) ? String.Empty : p.TerminalDestino),
                                          Observaciones = p.Observacion,
                                          TieneTrasladosAsignado = _db.SolicitudTraslados.Where(x => x.IdControlTransporte.Equals(p.Id)).Any(),
                                          FechaAlta = p.FechaAlta,
                                          FechaInicio = (DateTime)((TipoViaje)p.TipoViaje == TipoViaje.Entrada ? (p.FechaSalida != null ? p.FechaSalida : DateTime.MinValue) : (p.FechaLlegada != null ? p.FechaLlegada : DateTime.MinValue)),
                                          FechaFin = (DateTime)((TipoViaje)p.TipoViaje == TipoViaje.Entrada ? (p.FechaLlegada != null ? p.FechaLlegada : DateTime.MinValue) : (p.FechaSalida != null ? p.FechaSalida : DateTime.MinValue)),
                                          IdEmpresaLogin = p.IdEmpresa
                                      }).FirstOrDefault();

            if (_controlTransporte != null)
            {

                resultBase.Data = _controlTransporte;

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
}
