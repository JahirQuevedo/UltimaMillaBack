using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class SolicitudTrasladoRepositorio : GenericoRepositorio<ControlTransporte>, ISolicitudTrasladoRepositorio
{

    private readonly ApplicationDbContext _db;

    public SolicitudTrasladoRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase CancelarSolicitudTraslado(ConsultaSolicitudTraslado entidad)
    {
        ResultBase resultBase = new ResultBase();

        using (var _dbContextTransaction = _db.Database.BeginTransaction()) {

            try {

                var _solicitudTraslado = _db.SolicitudTraslados.Where(s => s.Id.Equals(entidad.IdSolicitudTraslado)).SingleOrDefault();

                if (_solicitudTraslado != null)
                {

                    if (_solicitudTraslado.EstadoTraslado.Equals(EstadoTraslado.Programado) || _solicitudTraslado.EstadoTraslado.Equals(EstadoTraslado.Asignado))
                    {

                        int _IdFolioServicio = (int)_solicitudTraslado.IdFolioServicio;

                        if (!_IdFolioServicio.Equals(0))
                        {
                            var _folioServicio = _db.FolioServicios.Where(f => f.Id.Equals(_IdFolioServicio)).SingleOrDefault();

                            if (_folioServicio != null)
                            {
                                if (_folioServicio.EstadoFolioServicio.Equals(EstadoFolioServicio.Pendiente))
                                {

                                    _folioServicio.EstadoFolioServicio = EstadoFolioServicio.Cancelado;
                                    _db.FolioServicios.Update(_folioServicio);
                                    _db.SaveChanges();

                                    // Si la solicitud de traslado ya se encuentra asociado a un transporte, al cancelar en estado Asignado el Trasporte queda liberado de la Solicitud de Traslado.
                                    if (_solicitudTraslado.EstadoTraslado.Equals(EstadoTraslado.Asignado))
                                    {
                                        _solicitudTraslado.IdControlTransporte = null;
                                    }
                                    _solicitudTraslado.EstadoTraslado = EstadoTraslado.Cancelado;
                                    _db.SolicitudTraslados.Update(_solicitudTraslado);
                                    _db.SaveChanges();

                                }
                            }
                        }
                        else
                        {
                            resultBase.MensajeRespuesta = "Favor de revisar el estado del Folio de Servicio de la Solictitud de Traslado.";
                        }


                    }
                    else
                    {
                        resultBase.MensajeRespuesta = "Favor de revisar el estado de la Solicitud de Traslado.";
                    }

                }

            } catch(Exception ex) {
                resultBase.MensajeRespuesta = "Error: " + ex.Message;
                _dbContextTransaction.Rollback();
            }

            _dbContextTransaction.Commit();


        } // proceso database transaction

        return resultBase;
    }

    public ResultBase<MonitorSolicitudTraslado> Guardar(MonitorSolicitudTraslado monitorSolicitudTraslado)
    {
        string folioTemp = DateTime.Now.ToString("yy");
        var resultBase = new ResultBase<MonitorSolicitudTraslado>();

        SolicitudTraslado _solicitudTraslado = new SolicitudTraslado();

        if (!monitorSolicitudTraslado.IdSolicitudTraslado.Equals(0))
        {
            _solicitudTraslado = _db.SolicitudTraslados.Where(x => x.Id.Equals(monitorSolicitudTraslado.IdSolicitudTraslado)).SingleOrDefault();
        }
        else
        {
            monitorSolicitudTraslado.EstadoTraslado = EstadoTraslado.Programado;
        }

        _solicitudTraslado.IdManiobristaOrigen = monitorSolicitudTraslado.IdManiobristaOrigen;
        _solicitudTraslado.IdManiobristaDestino = monitorSolicitudTraslado.IdManiobristaDestino;
        _solicitudTraslado.FechaSolicitudTraslado = monitorSolicitudTraslado.FechaSolicitudTraslado;
        _solicitudTraslado.IdEmpresa = (int)monitorSolicitudTraslado.IdEmpresaLogin;

        if (monitorSolicitudTraslado.CRUDAction.Equals(ECRUDAction.Create))
        {

            _db.SolicitudTraslados.Add(_solicitudTraslado);

        }
        else if (monitorSolicitudTraslado.CRUDAction.Equals(ECRUDAction.Update))
        {

            _solicitudTraslado.Id = monitorSolicitudTraslado.IdSolicitudTraslado;

            _db.SolicitudTraslados.Update(_solicitudTraslado);

        }

        _db.SaveChanges();

        if (monitorSolicitudTraslado.CRUDAction.Equals(ECRUDAction.Create))
        {
            folioTemp = folioTemp + _solicitudTraslado.Id.ToString().PadLeft(7, '0');

            _solicitudTraslado.Folio = Convert.ToInt32(folioTemp);
            monitorSolicitudTraslado.FolioTraslado = Convert.ToInt32(folioTemp);

            _db.SolicitudTraslados.Update(_solicitudTraslado);

            _db.SaveChanges();
        }

        monitorSolicitudTraslado.IdSolicitudTraslado = _solicitudTraslado.Id;
        resultBase.Id = _solicitudTraslado.Id;
        resultBase.Data = monitorSolicitudTraslado;

        return resultBase;
    }

    public ResultBase<MonitorSolicitudTraslado> GuardarServicioSolicitudTraslado(MonitorSolicitudTraslado monitorSolicitudTraslado)
    {
        string folioTemp = string.Empty;
        var resultBase = new ResultBase<MonitorSolicitudTraslado>();

        SolicitudTraslado _solicitudTraslado = new SolicitudTraslado();

        using (var _dbContextTransaction = _db.Database.BeginTransaction())
        {

            try
            {

                foreach (var _servicioInventario in monitorSolicitudTraslado.ListaInventarioServicios)
                {

                    _solicitudTraslado = new SolicitudTraslado();

                    folioTemp = DateTime.Now.ToString("yy");

                    _solicitudTraslado.EstadoTraslado = EstadoTraslado.Programado;
                    _solicitudTraslado.IdFolioServicio = _servicioInventario.IdFolioServicio;
                    _solicitudTraslado.IdManiobristaOrigen = monitorSolicitudTraslado.IdManiobristaOrigen;
                    _solicitudTraslado.IdManiobristaDestino = monitorSolicitudTraslado.IdManiobristaDestino;
                    _solicitudTraslado.FechaSolicitudTraslado = monitorSolicitudTraslado.FechaSolicitudTraslado;
                    _solicitudTraslado.Prioridad = monitorSolicitudTraslado.Prioridad;
                    _solicitudTraslado.Observaciones = monitorSolicitudTraslado.Observaciones;
                    _solicitudTraslado.IdEmpresa = (int)monitorSolicitudTraslado.IdEmpresaLogin;

                    if (monitorSolicitudTraslado.CRUDAction.Equals(ECRUDAction.Create))
                    {

                        _db.SolicitudTraslados.Add(_solicitudTraslado);

                    }
                    else if (monitorSolicitudTraslado.CRUDAction.Equals(ECRUDAction.Update))
                    {

                        _solicitudTraslado.Id = monitorSolicitudTraslado.IdSolicitudTraslado;

                        _db.SolicitudTraslados.Update(_solicitudTraslado);

                    }

                    _db.SaveChanges();

                    if (monitorSolicitudTraslado.CRUDAction.Equals(ECRUDAction.Create))
                    {
                        folioTemp = folioTemp + _solicitudTraslado.Id.ToString().PadLeft(7, '0');

                        _solicitudTraslado.Folio = Convert.ToInt32(folioTemp);
                        monitorSolicitudTraslado.FolioTraslado = Convert.ToInt32(folioTemp);

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

        resultBase.Data = monitorSolicitudTraslado;

        return resultBase;
    }

    public PaginadoResult<MonitorSolicitudTraslado> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaSolicitudTraslado> entidad)
    {
        var resultBase = new ResultBase<MonitorSolicitudTraslado>();

        List<MonitorSolicitudTraslado> _listMonitorSolicitudTraslado = new List<MonitorSolicitudTraslado>();

        int _totalRegistros = 0;

        //TODO: Enviar Id y Folio de la Referencia, para poder enlazar la página de Tarjas

        var listadoMonitorSolicitudTrasladoTemp = (from p in _db.SolicitudTraslados
                join e in _db.FolioServicios on p.IdFolioServicio equals e.Id 
                join h in _db.Paquetes on e.IdPaquete equals h.Id
                join f in _db.Maniobristas on p.IdManiobristaOrigen equals f.Id
                join g in _db.Maniobristas on p.IdManiobristaDestino equals g.Id
                join i in _db.Referencias on e.IdReferencia equals i.Id into reftmp
                from x in reftmp.DefaultIfEmpty()
                join j in _db.Tarjas on e.IdTarja equals j.Id into tarjtmp
                from y in tarjtmp.DefaultIfEmpty()
                join k in _db.ControlTransportes on p.IdControlTransporte equals k.Id into ctrltransptmp
                from z in ctrltransptmp.DefaultIfEmpty()
                join _tarja in _db.Tarjas on e.IdTarja equals _tarja.Id
                join _ref in _db.Referencias on _tarja.IdReferencia equals _ref.Id
                join tClieAux in _db.catClientes on _ref.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                from tClie in tClieTemp.DefaultIfEmpty()
                where 
                    p.IdControlTransporte == (entidad.Entidad.IdControlTransporte.Equals(0) ? p.IdControlTransporte : entidad.Entidad.IdControlTransporte)
                    && p.IdFolioServicio.Equals(entidad.Entidad.IdFolioServicio.Equals(0) ?  p.IdFolioServicio : entidad.Entidad.IdFolioServicio)
                    && p.EstadoTraslado.Equals(entidad.Entidad.EstadoSolicitudTraslado.Equals(EstadoTraslado.None) ? p.EstadoTraslado : entidad.Entidad.EstadoSolicitudTraslado)
                    && _tarja.ClaveReferencia.Contains(String.IsNullOrEmpty(entidad.Entidad.FolioReferenciaTarja) ? _tarja.ClaveReferencia : entidad.Entidad.FolioReferenciaTarja)
                    && f.NombreCorto.Contains(String.IsNullOrEmpty(entidad.Entidad.TerminalOrigen) ? f.NombreCorto : entidad.Entidad.TerminalOrigen)
                    && g.NombreCorto.Contains(String.IsNullOrEmpty(entidad.Entidad.TerminalDestino) ? g.NombreCorto : entidad.Entidad.TerminalDestino)
                    && (String.IsNullOrEmpty(tClie.RazonSocial) ? String.Empty : tClie.RazonSocial).Contains(String.IsNullOrEmpty(entidad.Entidad.NombreCliente) ? String.Empty : entidad.Entidad.NombreCliente)
                    && h.Descripcion.Contains(String.IsNullOrEmpty(entidad.Entidad.DescripcionServicio) ? h.Descripcion : entidad.Entidad.DescripcionServicio)
                    && p.EstadoTraslado.Equals(entidad.Entidad.EstadoSolicitudTraslado.Equals(EstadoTraslado.None) ? p.EstadoTraslado : entidad.Entidad.EstadoSolicitudTraslado)
                    && p.FechaSolicitudTraslado >= (entidad.Entidad.FechaInicio.Equals(DateTime.MinValue) ? p.FechaSolicitudTraslado : entidad.Entidad.FechaInicio.Date.Add(DateTime.MinValue.TimeOfDay)) &&
                    p.FechaSolicitudTraslado <= (entidad.Entidad.FechaFin.Equals(DateTime.MinValue) ? p.FechaSolicitudTraslado : entidad.Entidad.FechaFin.Date.Add(DateTime.MaxValue.TimeOfDay))
                select new MonitorSolicitudTraslado
                                                   {
                                                       IdSolicitudTraslado = p.Id,
                                                       FolioTraslado = p.Folio,
                                                       FechaSolicitudTraslado = (DateTime)(p.FechaSolicitudTraslado != null ? p.FechaSolicitudTraslado : DateTime.MinValue),
                                                       TipoMercancia = ((TipoMercanciaInventario)x.TipoMercancia).ToString(),
                                                       IdReferencia = _ref.Id,
                                                       FolioReferencia = _ref.Folio,
                                                       IdTarja = _tarja.Id,
                                                       FolioTarja = _tarja.Folio,
                                                       ClaveReferenciaTarja = _tarja.ClaveReferencia,
                                                       Cliente = String.IsNullOrEmpty(tClie.RazonSocial) ? String.Empty : tClie.RazonSocial,
                                                       IdFolioServicio = (int)(p.IdFolioServicio != null ? p.IdFolioServicio : 0),
                                                       FolioServicio = e.Folio,
                                                       EstadoFolioServicio = (EstadoFolioServicio)e.EstadoFolioServicio,
                                                       DescripcionFolioServicio = h.Descripcion,
                                                       EstadoTraslado = (EstadoTraslado)p.EstadoTraslado,
                                                       IdManiobristaOrigen = (int)(p.IdManiobristaOrigen != null ? p.IdManiobristaOrigen : 0),
                                                       ManiobristaOrigen = f.RazonSocial,
                                                       ManiobristaNombreCortoOrigen = f.NombreCorto,
                                                       ManiobristaDestino = g.RazonSocial,
                                                       ManiobristaNombreCortoDestino = g.NombreCorto,
                                                       IdControlTransporte = (int)(z.Id != null ? z.Id : 0),
                                                       FolioControlTransporte = (int)(z.Folio != null ? z.Folio : 0),
                                                       ClaveViaje = String.IsNullOrEmpty(z.Viajes) ? String.Empty : z.Viajes,
                                                       IdManiobristaDestino = (int)(p.ManiobristaDestino != null ? p.IdManiobristaDestino : 0),
                                                       FechaAlta = p.FechaAlta != null ? (DateTime)p.FechaAlta : DateTime.MinValue,
                                                   }).ToList();

        _totalRegistros = listadoMonitorSolicitudTrasladoTemp.Count;

        listadoMonitorSolicitudTrasladoTemp = listadoMonitorSolicitudTrasladoTemp.OrderByDescending(x => x.FolioTraslado).ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        //listadoTarjasTemp = AddItems(listadoTarjas);

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorSolicitudTraslado>(listadoMonitorSolicitudTrasladoTemp, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorSolicitudTraslado> ObtenerPorId(int idSolicitudTraslado)
    {
        throw new NotImplementedException();
    }
}
