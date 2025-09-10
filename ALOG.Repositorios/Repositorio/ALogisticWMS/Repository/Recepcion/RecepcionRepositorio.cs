using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace ALOG.Repositorios;

public class RecepcionRepositorio : GenericoRepositorio<SolicitudTraslado>, IRecepcionRepositorio
{

    private readonly ApplicationDbContext _db;

    public RecepcionRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorRecepcionMercancia> ConfirmarRecepcionMercanciaPorIdPartidaInventario(int idInventario, int tieneAveria)
    {
        var _result = new ResultBase<MonitorRecepcionMercancia>();

        _result.Data = new MonitorRecepcionMercancia();

        var _inventario = _db.Inventarios.Where(x => x.Id.Equals(idInventario)).FirstOrDefault();

        if (_inventario != null)
        {

            //Se registra la fecha en la que el transportista llevó a cabo la recolección de las unidades en el recinto.
            _inventario.FechaRecoleccion = DateTime.Now;

            _db.Inventarios.Update(_inventario);
            _db.SaveChanges();

            var _partida = _db.Partidas.Where(x => x.Id.Equals(_inventario.Id)).FirstOrDefault();

            _partida.TieneAveria = tieneAveria.Equals(1) ? true : false;
            _db.Partidas.Update(_partida);
            _db.SaveChanges();

            var _monitorRecepcion = (from p in _db.Partidas
                                     join _tarja in _db.Tarjas on p.IdTarja equals _tarja.Id
                                     join _inv in _db.Inventarios on p.IdInventario equals _inv.Id
                                     join j in _db.Referencias on _tarja.IdReferencia equals j.Id
                                     join i in _db.Viajes on j.IdViaje equals i.Id into _xrefviaje
                                     from _refviaje in _xrefviaje.DefaultIfEmpty()
                                     join k in _db.Barcos on _refviaje.IdBarco equals k.Id into _xbarco
                                     from _barco in _xbarco.DefaultIfEmpty()
                                     join n in _db.FolioServicios on _tarja.Id equals n.IdTarja
                                     join l in _db.SolicitudTraslados on n.Id equals l.IdFolioServicio
                                     join o in _db.ControlTransportes on l.IdControlTransporte equals o.Id
                                     join q in _db.Paquetes on n.IdPaquete equals q.Id
                                     join _mbr_o in _db.Maniobristas on o.IdManiobristaOrigen equals _mbr_o.Id
                                     join _mbr_d in _db.Maniobristas on o.IdManiobristaDestino equals _mbr_d.Id
                                     join _lntrans in _db.LineaTransportes on o.IdLineaTransTransporte equals _lntrans.Id
                                     join _lnopera in _db.LineaOperadores on o.IdLineaTransOperador equals _lnopera.Id
                                     join _ctrans in _db.catTransportistas on _lntrans.IdCatTransportista equals _ctrans.IdCatTransportista
                                     join _tptrans in _db.TipoTransportes on o.IdTipoTransporte equals _tptrans.Id
                                     where
                                         p.IdInventario.Equals(idInventario)
                                     select new MonitorRecepcionMercancia
                                     {
                                         IdSolicitudTraslado = l.Id,
                                         FolioSolicitudTraslado = l.Folio,
                                         FechaProgramacion = (DateTime)l.FechaSolicitudTraslado,
                                         IdReferencia = j.Id,
                                         IdOrdenServicio = (int)(j.IdOrdenServicio != null ? j.IdOrdenServicio : 0),
                                         IdTarja = _tarja.Id,
                                         FolioTarja = _tarja.Folio,
                                         IdInventario = (int)(p.IdInventario != null ? p.IdInventario : 0),
                                         IdMercancia = p.Numeros,
                                         Mercancia = j.Mercancia,
                                         FechaRecoleccion = (DateTime)(_inv.FechaRecoleccion != null ? _inv.FechaRecoleccion : DateTime.MinValue),
                                         FolioViaje = _refviaje.Folio,
                                         Buque = _barco.Nombre,
                                         FolioControlTransporte = o.Folio,
                                         OperadorTransporte = _lnopera.NombreCompleto,
                                         PlacasTransporte = _lntrans.Placas,
                                         LineaTransportista = _ctrans.RazonSocial,
                                         IdPaquete = q.Id,
                                         TipoEntrada = (TipoEntradaPaquete)q.TipoIngreso,
                                         ProcesoRecepcionCompletado = _inv.ProcesoRecepcionCompletado,
                                     }).SingleOrDefault();

            if (_monitorRecepcion != null)
            {

                _result.Data = _monitorRecepcion;
                _result.Id = idInventario;

            }

        }

        return _result;
    }

    public PaginadoResult<MonitorRecepcionMercancia> ConfirmarRecepcionMercanciaPorIdTarja(ConsultaCatalogoBase<ConsultaRecepcion> entidad)
    {
        List<MonitorRecepcionMercancia> _listaMonitorRecepcion = new List<MonitorRecepcionMercancia>();

        string _mensajeRespuesta = String.Empty;

        int _totalRegistros = 0;

        _listaMonitorRecepcion = (from p in _db.Partidas
            join _tarja in _db.Tarjas on p.IdTarja equals _tarja.Id
            join _inv in _db.Inventarios on p.IdInventario equals _inv.Id
            join _ubicaAux in _db.Ubicaciones on _inv.IdUbicacion equals _ubicaAux.Id into _ubicaTemp
            from _ubica in _ubicaTemp.DefaultIfEmpty()
            join _zonaAux in _db.ZonaAlmacenes on _ubica.IdZonaAlmacenaje equals _zonaAux.Id into _zonaTemp
            from _zona in _zonaTemp.DefaultIfEmpty()
            join j in _db.Referencias on _tarja.IdReferencia equals j.Id
            join i in _db.Viajes on j.IdViaje equals i.Id into _xrefviaje
            from _refviaje in _xrefviaje.DefaultIfEmpty()
            join k in _db.Barcos on _refviaje.IdBarco equals k.Id into _xbarco
            from _barco in _xbarco.DefaultIfEmpty()
            join n in _db.FolioServicios on _tarja.Id equals n.IdTarja
            join l in _db.SolicitudTraslados on n.Id equals l.IdFolioServicio
            join oAux in _db.ControlTransportes on l.IdControlTransporte equals oAux.Id into oTemp
            from o in oTemp.DefaultIfEmpty()
            join q in _db.Paquetes on n.IdPaquete equals q.Id
            join _mbr_o_aux in _db.Maniobristas on o.IdManiobristaOrigen equals _mbr_o_aux.Id into _mbr_o_temp 
            from _mbr_o in _mbr_o_temp.DefaultIfEmpty()
            join _mbr_d_aux in _db.Maniobristas on o.IdManiobristaDestino equals _mbr_d_aux.Id into _mbr_d_temp 
            from _mbr_d in _mbr_d_temp.DefaultIfEmpty()
            join _lntrans_aux in _db.LineaTransportes on o.IdLineaTransTransporte equals _lntrans_aux.Id into _lntrans_temp 
            from _lntrans in _lntrans_temp.DefaultIfEmpty()
            join _lnopera_aux in _db.LineaOperadores on o.IdLineaTransOperador equals _lnopera_aux.Id into _lnopera_temp
            from _lnopera in _lnopera_temp.DefaultIfEmpty()
            join _ctrans_aux in _db.catTransportistas on _lntrans.IdCatTransportista equals _ctrans_aux.IdCatTransportista into _ctrans_temp 
            from _ctrans in _ctrans_temp.DefaultIfEmpty()
            join _tptrans_aux in _db.TipoTransportes on o.IdTipoTransporte equals _tptrans_aux.Id into _tptrans_temp 
            from _tptrans in _tptrans_temp.DefaultIfEmpty()
        where
                                      _tarja.Id.Equals(entidad.Entidad.IdTarja)
        select new MonitorRecepcionMercancia
        {
            IdSolicitudTraslado = l.Id,
            FolioSolicitudTraslado = l.Folio,
            FechaProgramacion = (DateTime)l.FechaSolicitudTraslado,
            IdReferencia = j.Id,
            IdOrdenServicio = (int)(j.IdOrdenServicio != null ? j.IdOrdenServicio : 0),
            IdTarja = _tarja.Id,
            FolioTarja = _tarja.Folio,
            IdInventario = (int)(p.IdInventario != null ? p.IdInventario : 0),
            IdMercancia = p.Numeros,
            Mercancia = j.Mercancia,
            TieneAveria = p.TieneAveria,
            FechaIngreso = (DateTime)(_inv.FechaIngreso != null ? _inv.FechaIngreso : DateTime.MinValue),
            FechaRecoleccion = (DateTime)(_inv.FechaRecoleccion != null ? _inv.FechaRecoleccion : DateTime.MinValue),
            FolioViaje = _refviaje.Folio,
            Buque = _barco.Nombre,
            IdControlTransporte = o.Id,
            FolioControlTransporte = o.Folio,
            OperadorTransporte = String.IsNullOrEmpty(_lnopera.NombreCompleto) ? String.Empty : _lnopera.NombreCompleto,
            PlacasTransporte = String.IsNullOrEmpty(_lntrans.Placas) ? String.Empty : _lntrans.Placas,
            LineaTransportista = String.IsNullOrEmpty(_ctrans.RazonSocial) ? String.Empty : _ctrans.RazonSocial,
            IdPaquete = q.Id,
            TipoEntrada = (TipoEntradaPaquete)q.TipoIngreso,
            ProcesoRecepcionCompletado = _inv.ProcesoRecepcionCompletado,
            IdUbicacionAlmacen = _ubica.Id != null ? _ubica.Id : 0,
            ClaveUbicacion = String.IsNullOrEmpty(_ubica.Clave) ? String.Empty : _ubica.Clave,
            IdZonaAlmacenaje = _zona.Id != null ? _zona.Id : 0,
            ClaveZonaAlmacenaje = String.IsNullOrEmpty(_zona.Clave) ? String.Empty : _zona.Clave,
        }).ToList();

        _totalRegistros = _listaMonitorRecepcion.Count();

        var _fechaActual = DateTime.Now;

        using (var _dbContextTransaction = _db.Database.BeginTransaction())
        {

            try
            {

                var _monitorRecepcion = _listaMonitorRecepcion.Take(1).SingleOrDefault();

                var _controlTransporte = _db.ControlTransportes.Where(p => p.Id.Equals(_monitorRecepcion.IdControlTransporte)).FirstOrDefault();

                _controlTransporte.FechaSalida = _fechaActual;
                _controlTransporte.Estado = EstadoTurno.Iniciado;
                _controlTransporte.FechaIngreso = _fechaActual;
                _db.ControlTransportes.Update(_controlTransporte);
                _db.SaveChanges();

                var _solicitudTraslado = _db.SolicitudTraslados.Where(p => p.Id.Equals(_monitorRecepcion.IdSolicitudTraslado)).FirstOrDefault();

                _solicitudTraslado.EstadoTraslado = EstadoTraslado.EnTransito;
                _db.SolicitudTraslados.Update(_solicitudTraslado);
                _db.SaveChanges();

                foreach (var itemPartida in _listaMonitorRecepcion)
                {

                    var _partidaInventario = _db.Inventarios.Where(x => x.Id.Equals(itemPartida.IdInventario)).SingleOrDefault();

                    if (_partidaInventario != null) {
                        // Se verifica que la partida no tenga registro de la fecha de recolección.
                        if (_partidaInventario.FechaRecoleccion == null)
                        {
                            _partidaInventario.FechaRecoleccion = _fechaActual;
                            _db.Inventarios.Update(_partidaInventario);
                            _db.SaveChanges();

                            // Se actualiza fecha de recolección en el listado del Recepción

                            _listaMonitorRecepcion.Where(x => x.IdInventario.Equals(itemPartida.IdInventario))
                                                    .Select(p => { p.FechaRecoleccion = _fechaActual; return p; }).ToList();
                        }      
                    }

                }

            }
            catch (Exception ex)
            {
                _mensajeRespuesta = "Error: " + ex.Message;
                _dbContextTransaction.Rollback();
            }

            _dbContextTransaction.Commit();


        } // proceso database transaction


        _listaMonitorRecepcion.Select(p => { p.RecoleccionTarjaConfirmada = ValidaTarjaConfirmadaRecepcionMercancia(p.IdTarja); return p; }).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorRecepcionMercancia>(_listaMonitorRecepcion, paginadoInfo);

        return _paginadoResult;

    }

    public ResultBase<MonitorRecepcionMercancia> FinalizarRecepcionMercancia(MonitorRecepcionMercancia monitorRecepcionMercancia)
    {
        var result = new ResultBase<MonitorRecepcionMercancia>();

        var _resultBase = ValidarDatosRecepcionMercancia(monitorRecepcionMercancia);

        if (_resultBase.Success)
        {
            monitorRecepcionMercancia.ConfirmaRecepcionMercancia = true;
        }

        if (monitorRecepcionMercancia.ConfirmaRecepcionMercancia)
        {

            using (var _dbContextTransaction = _db.Database.BeginTransaction())
            {

                try
                {

                    bool _procesoRecepcionCompletado = _resultBase.Success;

                    DateTime _fechaRegistroIngreso = DateTime.Now;

                    var _inventario = _db.Inventarios.Where(p => p.Id.Equals(monitorRecepcionMercancia.IdInventario)).FirstOrDefault();

                    if (_inventario != null)
                    {

                        if (!_inventario.FechaIngreso.HasValue)
                        {
                            _inventario.FechaIngreso = _fechaRegistroIngreso;
                        }
                        _inventario.ProcesoRecepcionCompletado = _procesoRecepcionCompletado;
                        _inventario.IdUbicacion = monitorRecepcionMercancia.IdUbicacionAlmacen.Equals(0) ? _inventario.IdUbicacion : monitorRecepcionMercancia.IdUbicacionAlmacen;
                        _inventario.Existencia = monitorRecepcionMercancia.AplicaUbicacionMercancia ? true : false;
                        _db.Inventarios.Update(_inventario);
                        _db.SaveChanges();

                    }

                    var _tarja = _db.Tarjas.Where(p => p.Id.Equals(monitorRecepcionMercancia.IdTarja)).FirstOrDefault();

                    if (_tarja != null)
                    {

                        var _partidasNoIngresadas = (from p in _db.Tarjas
                                                     join e in _db.Partidas on p.Id equals e.IdTarja
                                                     join d in _db.Inventarios on e.IdInventario equals d.Id
                                                     where p.Id.Equals(_tarja.Id) && !d.FechaIngreso.HasValue
                                                     select new
                                                     MonitorPartida
                                                     {
                                                         IdPartida = e.Id,
                                                         IdInventario = (int)e.IdInventario,
                                                         FechaIngreso = (DateTime)(d.FechaIngreso != null ? d.FechaIngreso : DateTime.MinValue),
                                                     }).ToList();

                        if (_partidasNoIngresadas.Count.Equals(0))
                        {

                            _tarja.Estado = EstadoTarja.Confirmado;
                            _tarja.FechaIngreso = _fechaRegistroIngreso;
                            _db.Tarjas.Update(_tarja);
                            _db.SaveChanges();

                            var _folioServicio = _db.FolioServicios.Where(p => p.Id.Equals(monitorRecepcionMercancia.IdFolioServicio)).FirstOrDefault();

                            if (_folioServicio != null)
                            {
                                _folioServicio.EstadoFolioServicio = EstadoFolioServicio.Terminado;
                                _db.FolioServicios.Update(_folioServicio);
                                _db.SaveChanges();
                            }

                            var _solicitudTraslado = _db.SolicitudTraslados.Where(p => p.IdFolioServicio.Equals(_folioServicio.Id)).FirstOrDefault();

                            if (_solicitudTraslado != null)
                            {

                                _solicitudTraslado.EstadoTraslado = EstadoTraslado.Completado;
                                _db.SolicitudTraslados.Update(_solicitudTraslado);
                                _db.SaveChanges();

                            }

                            var _controlTransporte = _db.ControlTransportes.Where(p => p.Id.Equals(_solicitudTraslado.IdControlTransporte)).FirstOrDefault();

                            if (_controlTransporte != null)
                            {

                                _controlTransporte.FechaLlegada = _fechaRegistroIngreso;
                                _controlTransporte.Estado = EstadoTurno.Completado;
                                _db.ControlTransportes.Update(_controlTransporte);
                                _db.SaveChanges();

                            }

                        }

                    }



                }
                catch (Exception ex)
                {

                    result.MensajeRespuesta = "Error: " + ex.Message;
                    _dbContextTransaction.Rollback();
                }
                finally
                {

                    
                }

                if (result.Success)
                {
                    _dbContextTransaction.Commit();
                }


            } // proceso database transaction

        }
        else
        {

            if (monitorRecepcionMercancia.AplicaConfirmarMercancia)
            {
                monitorRecepcionMercancia.ConfirmaRecepcionMercancia = true;
            }
            else
            {
                monitorRecepcionMercancia.ConfirmaRecepcionMercancia = false;
            }
            result.Data = monitorRecepcionMercancia;
            result.MensajeRespuesta = _resultBase.MensajeRespuesta;
        }

        return result;
    }

    private ResultBase ValidarDatosRecepcionMercancia(MonitorRecepcionMercancia monitorRecepcionMercancia)
    {

        var result = new ResultBase();

        var _servicioFotograficoRecepcion = _db.ServicioFotografias.
                                    Where(p => p.IdInventario.Equals(monitorRecepcionMercancia.IdInventario) && p.TipoFoto.Equals(TipoFoto.Recepcion)).
                                        ToList();

        if (_servicioFotograficoRecepcion == null)
        {

            result.MensajeRespuesta = "Falta evidencia fotográfica de RECEPCION para poder finalizar el registro.";
            return result;

        }
        else if (!_servicioFotograficoRecepcion.Count.Equals(3))
        {

            result.MensajeRespuesta = "Falta evidencia fotográfica de RECEPCION para poder finalizar el registro.";
            return result;

        }

        var _bitacoraAveriaInventario = _db.BitacoraAveriaInventarios.Where(p => p.IdInventario.Equals(monitorRecepcionMercancia.IdInventario)).ToList();

        if (!_bitacoraAveriaInventario.Count.Equals(0))
        {

            _servicioFotograficoRecepcion = _db.ServicioFotografias.
                                    Where(p => p.IdInventario.Equals(monitorRecepcionMercancia.IdInventario) && p.TipoFoto.Equals(TipoFoto.ReporteDannios)).
                                        ToList();

            if (_servicioFotograficoRecepcion.Count.Equals(0))
            {

                result.MensajeRespuesta = "Falta evidencia fotográfica para el REPORTE DE DAÑOS registrado.";
                return result;

            }
            else if (!(_servicioFotograficoRecepcion.Count >= _bitacoraAveriaInventario.Count))
            {

                result.MensajeRespuesta = "La evidencia fotográfica para el REPORTE DE DAÑOS registrado no esta completa.";
                return result;

            }

        }
        
        if (monitorRecepcionMercancia.AplicaUbicacionMercancia)
        {
            if (monitorRecepcionMercancia.IdUbicacionAlmacen.Equals(0))
            {
                result.MensajeRespuesta = "No se permite finalizar sin el registro de la ubicación en almacen.";
                return result;
            }
        }

        return result;

    }

    public PaginadoResult<MonitorRecepcionMercancia> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaRecepcion> entidad)
    {

        List<MonitorRecepcionMercancia> _listaMonitorRecepcion = new List<MonitorRecepcionMercancia>();

        int _totalRegistros = 0;

        _listaMonitorRecepcion = (from p in _db.Partidas  
        join _tarja in _db.Tarjas on p.IdTarja equals _tarja.Id 
        join _inv in _db.Inventarios on p.IdInventario equals _inv.Id
        join _ubicaAux in _db.Ubicaciones on _inv.IdUbicacion equals _ubicaAux.Id into _ubicaTemp
        from _ubica in _ubicaTemp.DefaultIfEmpty()
        join _zonaAux in _db.ZonaAlmacenes on _ubica.IdZonaAlmacenaje equals _zonaAux.Id into _zonaTemp
        from _zona in _zonaTemp.DefaultIfEmpty()
        join j in _db.Referencias on _tarja.IdReferencia equals j.Id
        join i in _db.Viajes on j.IdViaje equals i.Id into _xrefviaje 
        from _refviaje in _xrefviaje.DefaultIfEmpty()
        join k in _db.Barcos on _refviaje.IdBarco equals k.Id into _xbarco
        from _barco in _xbarco.DefaultIfEmpty()
        join n in _db.FolioServicios on _tarja.Id equals n.IdTarja
        join l in _db.SolicitudTraslados on n.Id equals l.IdFolioServicio
        join o in _db.ControlTransportes on l.IdControlTransporte equals o.Id
        join q in _db.Paquetes on n.IdPaquete equals q.Id
        join _serv in _db.Servicios on q.IdServicio equals _serv.Id
        join _mbr_o_aux in _db.Maniobristas on o.IdManiobristaOrigen equals _mbr_o_aux.Id into _mbr_o_tepm
        from _mbr_o in _mbr_o_tepm.DefaultIfEmpty()
        join _mbr_d_aux in _db.Maniobristas on o.IdManiobristaDestino equals _mbr_d_aux.Id into _mbr_d_temp
        from _mbr_d in _mbr_d_temp.DefaultIfEmpty() 
        join _lntrans_aux in _db.LineaTransportes on o.IdLineaTransTransporte equals _lntrans_aux.Id into _lntrans_temp
        from _lntrans in _lntrans_temp.DefaultIfEmpty()
        join _lnopera_aux in _db.LineaOperadores on o.IdLineaTransOperador equals _lnopera_aux.Id into _lnopera_temp
        from _lnopera in _lnopera_temp.DefaultIfEmpty()
        join _ctrans_aux in _db.catTransportistas on _lntrans.IdCatTransportista equals _ctrans_aux.IdCatTransportista into _ctrans_temp
        from _ctrans in _ctrans_temp.DefaultIfEmpty()
        join _tptrans_aux in _db.TipoTransportes on o.IdTipoTransporte equals _tptrans_aux.Id into _tptrans_temp
        from _tptrans in _tptrans_temp.DefaultIfEmpty()
        join _cltransAux in _db.catTransportistas on o.IdCatTransportista equals _cltransAux.IdCatTransportista into _cltransTemp 
        from _cltrans in _cltransTemp.DefaultIfEmpty()
        where
                                      p.Numeros.Equals(String.IsNullOrEmpty(entidad.Entidad.IdMercancia) ? p.Numeros : entidad.Entidad.IdMercancia)
                                      && _tarja.ClaveReferencia.Equals(String.IsNullOrEmpty(entidad.Entidad.ClaveReferenciaTarja) ? _tarja.ClaveReferencia : entidad.Entidad.ClaveReferenciaTarja)
                                      && q.TipoIngreso.Equals(TipoEntradaPaquete.Traslado)
                                      && p.IdInventario.Equals(entidad.Entidad.IdInventario.Equals(0) ? p.IdInventario : entidad.Entidad.IdInventario)
                                      && _inv.FechaIngreso.Equals(entidad.Entidad.TipoClasificacionRecepcion.Equals(TipoClasificacionRecepcion.Todos) || entidad.Entidad.TipoClasificacionRecepcion.Equals(TipoClasificacionRecepcion.None) ? _inv.FechaIngreso
                                      : entidad.Entidad.TipoClasificacionRecepcion.Equals(TipoClasificacionRecepcion.Pendientes) ? null : (_inv.FechaIngreso != null ? _inv.FechaIngreso : DateTime.MinValue))
                                      && _inv.FechaSalida.Equals(null)
            select new MonitorRecepcionMercancia
                                  {
                                      IdSolicitudTraslado = l.Id,
                                      FolioSolicitudTraslado = l.Folio,
                                      FechaProgramacion = (DateTime)l.FechaSolicitudTraslado,
                                      IdReferencia = j.Id,
                                      IdOrdenServicio = (int)(j.IdOrdenServicio != null ? j.IdOrdenServicio : 0),
                                      IdTarja = _tarja.Id,
                                      FolioTarja = _tarja.Folio,
                                      ClaveReferenciaTarja = _tarja.ClaveReferencia,
                                      IdFolioServicio = n.Id,
                                      FolioServicio = n.Folio,
                                      IdInventario = (int)(p.IdInventario != null ? p.IdInventario : 0),
                                      IdMercancia = p.Numeros,
                                      Mercancia = j.Mercancia,
                                      TieneAveria = p.TieneAveria,
                                      FechaIngreso = (DateTime)(_inv.FechaIngreso != null ? _inv.FechaIngreso : DateTime.MinValue),
                                      FechaRecoleccion = (DateTime)(_inv.FechaRecoleccion != null ? _inv.FechaRecoleccion : DateTime.MinValue),
                                      FolioViaje = _refviaje.Folio,
                                      Buque = _barco.Nombre,
                                      FolioControlTransporte = o.Folio,
                                      ClaveViaje = o.Viajes,
                                      OperadorTransporte = String.IsNullOrEmpty(_lnopera.NombreCompleto) ? o.NombreOperador : _lnopera.NombreCompleto,
                                      PlacasTransporte = String.IsNullOrEmpty(_lntrans.Placas) ? o.Placas : _lntrans.Placas,
                                      LineaTransportista = String.IsNullOrEmpty(_ctrans.RazonSocial) ? String.IsNullOrEmpty(_cltrans.RazonSocial) ? String.Empty : _cltrans.RazonSocial : _ctrans.RazonSocial,
                                      IdPaquete = q.Id,
                                      TipoEntrada = (TipoEntradaPaquete)q.TipoIngreso,
                                      ProcesoRecepcionCompletado = _inv.ProcesoRecepcionCompletado,
                                      AplicaExtraccionPatioExterno = (bool)(_serv.ExtraccionPatioExterno != null ? _serv.ExtraccionPatioExterno : false),
                                      AplicaUbicacionMercancia = (bool)(_serv.EsAlmacenaje != null ? _serv.EsAlmacenaje : false),
                                      IdUbicacionAlmacen = _ubica.Id != null ? _ubica.Id : 0,
                                      ClaveUbicacion = String.IsNullOrEmpty(_ubica.Clave) ? String.Empty : _ubica.Clave,
                                      IdZonaAlmacenaje = _zona.Id != null ? _zona.Id : 0,
                                      ClaveZonaAlmacenaje = String.IsNullOrEmpty(_zona.Clave) ? String.Empty : _zona.Clave,
                                  }).ToList();

        _totalRegistros = _listaMonitorRecepcion.Count();

        _listaMonitorRecepcion.Select(p => { p.RecoleccionTarjaConfirmada = ValidaTarjaConfirmadaRecepcionMercancia(p.IdTarja); return p; }).ToList();

        _listaMonitorRecepcion = _listaMonitorRecepcion
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorRecepcionMercancia>(_listaMonitorRecepcion, paginadoInfo);

        return _paginadoResult;

    }

    private bool ValidaTarjaConfirmadaRecepcionMercancia(int idTarja)
    {

        bool _tarjaConPartidasSinConfirmar = false;

        _tarjaConPartidasSinConfirmar = (from p in _db.Partidas
                                         join _tarja in _db.Tarjas on p.IdTarja equals _tarja.Id
                                         join _inv in _db.Inventarios on p.IdInventario equals _inv.Id
                                         where p.IdTarja == idTarja &&
                                         _inv.FechaRecoleccion == null
                                         select new { p.IdInventario }).Any();

        return !_tarjaConPartidasSinConfirmar;


    }

}
