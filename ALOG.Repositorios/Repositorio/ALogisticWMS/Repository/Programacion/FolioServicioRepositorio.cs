using System.Reflection.Metadata;
using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using MathNet.Numerics.LinearAlgebra.Complex.Solvers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios;

public class FolioServicioRepositorio : GenericoRepositorio<Referencia>, IFolioServicioRepositorio
{

    private readonly ApplicationDbContext _db;

    public FolioServicioRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


    /// <summary>
    /// Método para validar los datos que se desean guardar o actualizar.
    /// </summary>
    /// <param name="preTurno">Entidad que se validara con su información.</param>
    /// <returns>ResultBase que indica si la operación se realizo de manera correcta.</returns>
    public ResultBase<MonitorFolioServicio> Guardar(MonitorFolioServicio monitorFolioServicio)
    {
        string folioTemp = DateTime.Now.ToString("yy");
        var resultBase = new ResultBase<MonitorFolioServicio>();

        try
        {

            FolioServicio _folioServicio = new FolioServicio();
            RelacionInventarioServicio _relacionInventarioServicio = new RelacionInventarioServicio();

            List<RelacionInventarioServicio> _relacionInventarioServicioList = new List<RelacionInventarioServicio>();

            using (var _dbContextTransaction = _db.Database.BeginTransaction())
            {

                try
                {
                    //TODO: Se recorre la lista de Tarjas Selecciondas
                    int _contadorServicios = 0;
                    foreach (RelacionInventarioServicio item in monitorFolioServicio.ListaRelacionInventarioServicio)
                    {

                        _folioServicio = new FolioServicio();
                        _relacionInventarioServicio = new RelacionInventarioServicio();
                        int _IdFolioServicioAux = monitorFolioServicio.IdFolioServicio;

                        folioTemp = DateTime.Now.ToString("yy");

                        // Se crea el servicio ------------------------------------------

                        if (!item.IdFolioServicio.Equals(0))
                        {
                            _folioServicio = _db.FolioServicios.Where(x => x.Id == item.IdFolioServicio).SingleOrDefault();
                        }


                        _folioServicio.EstadoFolioServicio = monitorFolioServicio.EstadoFolioServicio;
                        _folioServicio.FechaProgramada = monitorFolioServicio.FechaProgramada;
                        _folioServicio.IdPaquete = monitorFolioServicio.IdPaquete;
                        _folioServicio.ServicioPara = monitorFolioServicio.ServicioPara;

                        if (monitorFolioServicio.ServicioPara.Equals((int)ServicioAplicado.Tarja))
                        {

                            _folioServicio.IdTarja = item.IdTarja;

                        }
                        if (monitorFolioServicio.ServicioPara.Equals((int)ServicioAplicado.Referencia))
                        {

                            if (monitorFolioServicio.IdReferencia != null && !monitorFolioServicio.IdReferencia.Equals(0))
                                _folioServicio.IdReferencia = monitorFolioServicio.IdReferencia;

                        }

                        if (monitorFolioServicio.ServicioPara.Equals((int)ServicioAplicado.Inventario))
                        {
                            _folioServicio.IdInventario = item.IdPartidaInventario;

                        }

                        _folioServicio.IdEmpresa = (int)monitorFolioServicio.IdEmpresaLogin;
                        _folioServicio.Instrucciones = monitorFolioServicio.InstruccionesDelServicio;
                        _folioServicio.Solicitante = monitorFolioServicio.Solicito;

                        if (monitorFolioServicio.CRUDAction.Equals(ECRUDAction.Create))
                        {

                            _folioServicio.FechaAlta = monitorFolioServicio.FechaAlta;

                            _db.FolioServicios.Add(_folioServicio);

                        }
                        else if (monitorFolioServicio.CRUDAction.Equals(ECRUDAction.Update))
                        {

                            _folioServicio.Id = item.IdFolioServicio;

                            _db.FolioServicios.Update(_folioServicio);

                        }

                        _db.SaveChanges();

                        if (monitorFolioServicio.CRUDAction.Equals(ECRUDAction.Create))
                        {
                            folioTemp = folioTemp + _folioServicio.Id.ToString().PadLeft(7, '0');

                            _folioServicio.Folio = Convert.ToInt32(folioTemp);

                            _db.FolioServicios.Update(_folioServicio);

                            _db.SaveChanges();
                        }

                        // --------------------------------------------------------------

                        _relacionInventarioServicio.IdTarja = item.IdTarja;
                        _relacionInventarioServicio.IdPartidaInventario = item.IdPartidaInventario;
                        _relacionInventarioServicio.FolioTarjaInventario = item.FolioTarjaInventario;
                        _relacionInventarioServicio.Folio = item.Folio;
                        _relacionInventarioServicio.IdReferencia = item.IdReferencia;
                        _relacionInventarioServicio.IdFolioServicio = _folioServicio.Id;
                        _relacionInventarioServicio.CRUDAction = ECRUDAction.Read;
                        _relacionInventarioServicioList.Add(_relacionInventarioServicio);

                        _contadorServicios = _contadorServicios + 1;

                    } //endForeach

                }
                catch (Exception ex)
                {

                    resultBase.MensajeRespuesta = "Error: " + ex.Message;
                    _dbContextTransaction.Rollback();
                }

                _dbContextTransaction.Commit();


            } // proceso database transaction

            var _folioServicioTemp = _db.FolioServicios.Where(p => p.Id.Equals(_relacionInventarioServicioList[0].IdFolioServicio)).SingleOrDefault();

            monitorFolioServicio.ListaRelacionInventarioServicio = _relacionInventarioServicioList;
            monitorFolioServicio.IdFolioServicio = _folioServicioTemp.Id;
            monitorFolioServicio.FechaAlta = _folioServicioTemp.FechaAlta;
            if (monitorFolioServicio.ServicioPara.Equals((int)ServicioAplicado.Tarja))
                monitorFolioServicio.IdTarja = (int)_folioServicioTemp.IdTarja;
            monitorFolioServicio.Folio = _folioServicioTemp.Folio;

            resultBase.Data = monitorFolioServicio;

        }
        catch (Exception ex)
        {
            resultBase.MensajeRespuesta = "Error: " + ex.Message;
        }

        return resultBase;
    }

    public ResultBase Confirmar(int idFolioServicio)
    {

        var resultBase = new ResultBase();

        var _folioServicio = _db.FolioServicios.Where(x => x.Id == idFolioServicio &&
        x.EstadoFolioServicio.Equals(EstadoFolioServicio.Pendiente)).SingleOrDefault();

        if (_folioServicio != null)
        {
            _folioServicio.EstadoFolioServicio = EstadoFolioServicio.Terminado;
            _db.FolioServicios.Update(_folioServicio);
            _db.SaveChanges();

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontró información del folio solicitado o El Folio de Servicio tiene que estar en estado Pendiente.";
        }

        return resultBase;
    }

    public ResultBase Eliminar(int idFolioServicio)
    {

        var resultBase = new ResultBase();

        var _folioServicio = _db.FolioServicios.Where(x => x.Id == idFolioServicio &&
        x.EstadoFolioServicio.Equals(EstadoFolioServicio.Pendiente)).SingleOrDefault();

        if (_folioServicio != null)
        {
            if (_folioServicio.EstadoFolioServicio.Equals(EstadoFolioServicio.Pendiente))
            {
                _folioServicio.EstadoFolioServicio = EstadoFolioServicio.Cancelado;
                _db.FolioServicios.Update(_folioServicio);
                _db.SaveChanges();

            }

        }
            else
            {
                resultBase.MensajeRespuesta = "No se encontró información del folio solicitado o El Folio de Servicio tiene que estar en estado Pendiente.";
            }

        return resultBase;
    }

    public PaginadoResult<MonitorFolioServicio> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaFolioServicio> entidad)
    {
        var resultBase = new ResultBase<MonitorFolioServicio>();
        // List<Tarja> listadoTarjas;
        List<MonitorFolioServicio> _listMonitorFolioServicio = new List<MonitorFolioServicio>();

        int _totalRegistros = 0;

        // _totalRegistros = _db.Tarjas.ToList().Count;

        // listadoTarjas = _db.Tarjas.OrderByDescending(x => x.Folio).ToList()
        //     .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
        //     .Take(entidad.RegistrosPorPagina).ToList();

        var listadoMonitorFolioServiciosTemp = (from p in _db.FolioServicios
                                                join e in _db.Referencias on p.IdReferencia equals e.Id into reftemp01
                                                from x in reftemp01.DefaultIfEmpty()
                                                join d in _db.Tarjas on p.IdTarja equals d.Id into tarjatemp
                                                from y in tarjatemp.DefaultIfEmpty()
                                                join f in _db.Referencias on y.IdReferencia equals f.Id into reftemp02
                                                from z in reftemp02.DefaultIfEmpty()
                                                join tPartAux in _db.Partidas on p.IdInventario equals tPartAux.IdInventario into tPartTemp
                                                from tPart in tPartTemp.DefaultIfEmpty()
                                                join tTarjaPartAux in _db.Tarjas on tPart.IdTarja equals tTarjaPartAux.Id into tTarjaPartTemp
                                                from tTarjaPart in tTarjaPartTemp.DefaultIfEmpty()
                                                join tRefPartAux in _db.Referencias on tTarjaPart.IdReferencia equals tRefPartAux.Id into tRefPartTemp
                                                from tRefPart in tRefPartTemp.DefaultIfEmpty()
                                                join tPaqueteAux in _db.Paquetes on p.IdPaquete equals tPaqueteAux.Id into tPaqueteTemp
                                                from tPaquete in tPaqueteTemp.DefaultIfEmpty()
                                                where
                                                    entidad.Entidad.IdReferencia.Equals(x.Id != null ? x.Id : (
                                                        z.Id != null ? z.Id : (
                                                            tRefPart.Id != null ? tRefPart.Id : 0
                                                        ))
                                                    )
                                                    && p.EstadoFolioServicio.Equals(entidad.Entidad.EstadoFolioServicio.Equals(EstadoFolioServicio.None) ? p.EstadoFolioServicio : entidad.Entidad.EstadoFolioServicio)
                                                    && tPaquete.Descripcion.Contains(String.IsNullOrEmpty(entidad.Entidad.DescripcionServicio) ? tPaquete.Descripcion : entidad.Entidad.DescripcionServicio)
                                                select new MonitorFolioServicio
                                                {
                                                    IdFolioServicio = p.Id,
                                                    Folio = p.Folio,
                                                    IdPaquete = tPaquete.Id,
                                                    DescripcionPaquete = tPaquete.Descripcion,
                                                    FolioReferencia = "",
                                                    IdTarja = y.Id != null ? y.Id : 0,
                                                    FolioTarja = y.Folio != null ? y.Folio : 0,
                                                    FolioReferenciaTarja = String.IsNullOrEmpty(y.ClaveReferencia) ? String.Empty : y.ClaveReferencia,
                                                    FolioTarjaPartida = tPart.NumeroPartida != null ? tTarjaPart.ClaveReferencia + "/" + tPart.NumeroPartida.ToString() : "",
                                                    EstadoFolioServicio = (EstadoFolioServicio)p.EstadoFolioServicio,
                                                    ServicioPara = (int)p.ServicioPara,
                                                    FechaProgramada = (p.FechaProgramada != null ? (DateTime)p.FechaProgramada : DateTime.MinValue),
                                                    IdReferencia = x.Id != null ? x.Id : 0,
                                                    IdInventario = (int)(tPart.IdInventario != null ? tPart.IdInventario : 0),
                                                    FechaAlta = p.FechaAlta != null ? (DateTime)p.FechaAlta : DateTime.MinValue,
                                                    TipoIngreso = (TipoEntradaPaquete)(tPaquete.TipoIngreso != null ? tPaquete.TipoIngreso : TipoEntradaPaquete.None),
                                                    InstruccionesDelServicio = p.Instrucciones,
                                                    Solicito = p.Solicitante
                                                }).ToList();

        _totalRegistros = listadoMonitorFolioServiciosTemp.Count;

        listadoMonitorFolioServiciosTemp = listadoMonitorFolioServiciosTemp.OrderByDescending(x => x.Folio).ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        //listadoTarjasTemp = AddItems(listadoTarjas);

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorFolioServicio>(listadoMonitorFolioServiciosTemp, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorFolioServicio> ObtenerPorId(int idFolioServicio)
    {
        var resultBase = new ResultBase<MonitorFolioServicio>();
        MonitorFolioServicio monitorTarja = new MonitorFolioServicio();

        try
        {
            //var tarja = _db.FolioServicios.Where(x => x.Id == idFolioServicio).FirstOrDefault();

            var _folioServicio = (from p in _db.FolioServicios
                                  join e in _db.Referencias on p.IdReferencia equals e.Id into reftmp
                                  from y in reftmp.DefaultIfEmpty()
                                  join tarj in _db.Tarjas on p.IdTarja equals tarj.Id into tarjatemp
                                  from z in tarjatemp.DefaultIfEmpty()
                                  join winv in _db.Inventarios on p.IdInventario equals winv.Id into winvtemp
                                  from w in winvtemp.DefaultIfEmpty()
                                  join d in _db.Paquetes on p.IdPaquete equals d.Id into f
                                  from x in f.DefaultIfEmpty()
                                  where p.Id == idFolioServicio
                                  select new MonitorFolioServicio
                                  {
                                      IdFolioServicio = p.Id,
                                      Folio = p.Folio,
                                      ServicioPara = (int)p.ServicioPara,
                                      EstadoFolioServicio = (EstadoFolioServicio)(p.EstadoFolioServicio != null ? p.EstadoFolioServicio : 0),
                                      IdReferencia = (int)(p.IdReferencia != null ? p.IdReferencia : 0),
                                      IdTarja = (int)(p.IdTarja != null ? p.IdTarja : 0),
                                      FolioReferenciaTarja = z.ClaveReferencia,
                                      IdInventario = (int)(p.IdInventario != null ? p.IdInventario : 0),
                                      IdPaquete = (int)(p.IdPaquete != null ? p.IdPaquete : 0),
                                      DescripcionPaquete = x.Descripcion,
                                      InstruccionesDelServicio = p.Instrucciones,
                                      Solicito = p.Solicitante,
                                      FechaProgramada = (DateTime)p.FechaProgramada,
                                      TipoIngreso = (TipoEntradaPaquete)(x.TipoIngreso != null ? x.TipoIngreso : TipoEntradaPaquete.None),
                                      FechaAlta = p.FechaAlta,
                                  }).FirstOrDefault();

            if (_folioServicio != null)
            {

                //TODO: Obtener Datos del Cliente identificando el servicio aplicado a la REFERENCIA, TARJA o INVENTARIO;

                if (_folioServicio.ServicioPara.Equals(ServicioAplicado.Referencia))
                {

                    var _dataCliente = (from tRef in _db.Referencias
                                        join tClie in _db.catClientes on tRef.IdCliente equals tClie.IdCatCliente
                                        join tFactAAux in _db.catClientes on tRef.IdClienteFacturarA equals tFactAAux.IdCatCliente into tFactTemp
                                        from tFactA in tFactTemp.DefaultIfEmpty()
                                        where tRef.Id.Equals(_folioServicio.IdReferencia)
                                        select new
                                        {
                                            IdCliente = tClie.IdCatCliente,
                                            RazonSocialCliente = tClie.RazonSocial,
                                            IdFacturarA = tFactA.IdCatCliente != null ? tFactA.IdCatCliente : 0,
                                            RazonSocialFacturarA = String.IsNullOrEmpty(tFactA.RazonSocial) ? String.Empty : tFactA.RazonSocial,
                                        }).SingleOrDefault();

                    if (_dataCliente != null)
                    {
                        _folioServicio.IdCliente = _dataCliente.IdCliente;
                        _folioServicio.RazonSocialCliente = _dataCliente.RazonSocialCliente;
                        _folioServicio.IdFacturarA = _dataCliente.IdFacturarA;
                        _folioServicio.NombreFacturarA = _dataCliente.RazonSocialFacturarA;
                    }
                }
                else if (_folioServicio.ServicioPara.Equals(ServicioAplicado.Tarja))
                {
                    var _dataCliente = (from tRef in _db.Referencias
                                        join tClie in _db.catClientes on tRef.IdCliente equals tClie.IdCatCliente
                                        join tFactAAux in _db.catClientes on tRef.IdClienteFacturarA equals tFactAAux.IdCatCliente into tFactTemp
                                        from tFactA in tFactTemp.DefaultIfEmpty()
                                        join tTarja in _db.Tarjas on tRef.Id equals tTarja.IdReferencia
                                        where tTarja.Id.Equals(_folioServicio.IdTarja)
                                        select new
                                        {
                                            IdCliente = tClie.IdCatCliente,
                                            RazonSocialCliente = tClie.RazonSocial,
                                            IdFacturarA = tFactA.IdCatCliente != null ? tFactA.IdCatCliente : 0,
                                            RazonSocialFacturarA = String.IsNullOrEmpty(tFactA.RazonSocial) ? String.Empty : tFactA.RazonSocial,
                                        }).SingleOrDefault();

                    if (_dataCliente != null)
                    {
                        _folioServicio.IdCliente = _dataCliente.IdCliente;
                        _folioServicio.RazonSocialCliente = _dataCliente.RazonSocialCliente;
                        _folioServicio.IdFacturarA = _dataCliente.IdFacturarA;
                        _folioServicio.NombreFacturarA = _dataCliente.RazonSocialFacturarA;
                    }
                }
                else if (_folioServicio.ServicioPara.Equals(ServicioAplicado.Inventario))
                {
                    var _dataCliente = (from tRef in _db.Referencias
                                        join tClie in _db.catClientes on tRef.IdCliente equals tClie.IdCatCliente
                                        join tFactAAux in _db.catClientes on tRef.IdClienteFacturarA equals tFactAAux.IdCatCliente into tFactTemp
                                        from tFactA in tFactTemp.DefaultIfEmpty()
                                        join tTarja in _db.Tarjas on tRef.Id equals tTarja.IdReferencia
                                        join tPartida in _db.Partidas on tTarja.Id equals tPartida.IdTarja
                                        where tPartida.IdInventario.Equals(_folioServicio.IdInventario)
                                        select new
                                        {
                                            IdCliente = tClie.IdCatCliente,
                                            RazonSocialCliente = tClie.RazonSocial,
                                            IdFacturarA = tFactA.IdCatCliente != null ? tFactA.IdCatCliente : 0,
                                            RazonSocialFacturarA = String.IsNullOrEmpty(tFactA.RazonSocial) ? String.Empty : tFactA.RazonSocial,
                                        }).SingleOrDefault();

                    if (_dataCliente != null)
                    {
                        _folioServicio.IdCliente = _dataCliente.IdCliente;
                        _folioServicio.RazonSocialCliente = _dataCliente.RazonSocialCliente;
                        _folioServicio.IdFacturarA = _dataCliente.IdFacturarA;
                        _folioServicio.NombreFacturarA = _dataCliente.RazonSocialFacturarA;
                    }
                }

                //CatClientes catClientes = 

                resultBase.Data = _folioServicio;

            }

            else
            {
                resultBase.MensajeRespuesta = "No se encontró el Folio de Servicio.";
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

    private int ObtieneIdClienteServicio(FolioServicio folioServicio)
    {

        return 0;
    }


}
