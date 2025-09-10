using ALOG.Enums;
using ALOG.InfraestructuraReportes;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace ALOG.Repositorios;

public class TarjaRepositorio : GenericoRepositorio<Tarja>, ITarjaRepositorio
{

    private readonly ApplicationDbContext _db;

    public TarjaRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public ResultBase<MonitorTarja> Guardar(MonitorTarja monitorTarja)
    {
        string folioTemp = DateTime.Now.ToString("yy");
        var resultBase = new ResultBase<MonitorTarja>();

        Tarja _tarja = new Tarja();

        if (!monitorTarja.IdTarja.Equals(0))
            _tarja = _db.Tarjas.Where(x => x.Id.Equals(monitorTarja.IdTarja)).SingleOrDefault();

        _tarja.Folio = monitorTarja.Folio.Equals(0) ? _tarja.Folio : monitorTarja.Folio;
        _tarja.FechaAlta = monitorTarja.FechaAlta;
        _tarja.Estado = monitorTarja.Estado;
        _tarja.TipoServicio = monitorTarja.TipoServicio;
        _tarja.TipoTarja = monitorTarja.TipoTarja;
        _tarja.GrupoViaje = String.IsNullOrEmpty(monitorTarja.GrupoViaje) ? _tarja.GrupoViaje : monitorTarja.GrupoViaje;
        _tarja.Consecutivo = monitorTarja.Consecutivo.Equals(0) ? 1 : monitorTarja.Consecutivo;
        if (monitorTarja.IdReferencia != null && !monitorTarja.IdReferencia.Equals(0) && monitorTarja.CRUDAction.Equals(ECRUDAction.Create)) {
            _tarja.IdReferencia = monitorTarja.IdReferencia;
            var _refAux = _db.Referencias.Where(x => x.Id.Equals(monitorTarja.IdReferencia)).SingleOrDefault();
            if (_refAux != null)
            {
                var _consecutivo = _db.Tarjas.Where(x => x.IdReferencia.Equals(_refAux.Id)).Count();
                monitorTarja.Consecutivo = monitorTarja.Consecutivo.Equals(0) ? _consecutivo.Equals(0) ? 1 : ++_consecutivo : monitorTarja.Consecutivo;
                _tarja.ClaveReferencia = _refAux.Folio + "-" + monitorTarja.Consecutivo.ToString().PadLeft(4, '0');
            }
        }
        _tarja.IdEmpresa = (int)monitorTarja.IdEmpresaLogin;
        _tarja.Observaciones = monitorTarja.Observaciones;

        if (monitorTarja.CRUDAction.Equals(ECRUDAction.Create))
        {

            _db.Tarjas.Add(_tarja);

        }
        else if (monitorTarja.CRUDAction.Equals(ECRUDAction.Update))
        {

            _tarja.Id = monitorTarja.IdTarja;
            monitorTarja.Folio = _tarja.Folio;
            _db.Tarjas.Update(_tarja);

        }

        _db.SaveChanges();

        if (monitorTarja.CRUDAction.Equals(ECRUDAction.Create))
        {
            folioTemp = folioTemp + _tarja.Id.ToString().PadLeft(7, '0');

            _tarja.Folio = Convert.ToInt32(folioTemp);
            monitorTarja.Folio = Convert.ToInt32(folioTemp);

            _db.Tarjas.Update(_tarja);

            _db.SaveChanges();
        }

        monitorTarja.ClaveReferencia = _tarja.ClaveReferencia;

        resultBase.Id = _tarja.Id;
        resultBase.Data = monitorTarja;

        return resultBase;
    }

    public ResultBase Eliminar(int idTarja)
    {

        var resultBase = new ResultBase();

        var _tarja = _db.Tarjas.Where(x => x.Id == idTarja).SingleOrDefault();

        if (_tarja != null)
        {

            var _listPartidas = (from tPartidas in _db.Partidas
                                join tInv in _db.Inventarios on tPartidas.IdInventario equals tInv.Id
                                where tPartidas.IdTarja.Equals(_tarja.Id) 
                                && tInv.Existencia.Equals(true)
                                select tPartidas).ToList();

            if (_listPartidas.Count.Equals(0))
            {

                _tarja.Estado = EstadoTarja.Cancelado;
                _db.Tarjas.Update(_tarja);
                _db.SaveChanges();

            }
            else
            {
                resultBase.MensajeRespuesta = "La Tarja no se puede eliminar, cuenta con partidas en inventario.";
            }

        } else
        {
            resultBase.MensajeRespuesta = "No se encontró información del registro solicitado.";
        }

        return resultBase;
    }

    public ResultBase GuardarCargaMasiva(CargaMasiva cargaMasiva)
    {
        var resultBase = new ResultBase();
        var npoiExcelExportarDatos = new NpoiExcelExportarDatos();

        try
        {

            var tipoSubDirectorioGeneral = TipoSubDirectorioGeneral.IncluirAnioMesDia;

            string rutaBase = cargaMasiva.ArchivoBase.RutaBase;

            cargaMasiva.ArchivoBase.CarpetaIdentificacion = "temp";

            string rutaRelativa = $@"/Archivos/CargaMasiva/{cargaMasiva.ArchivoBase.CarpetaIdentificacion}";

            var documentoUtiliidad = new DocumentoUtilidad();

            (ResultBase resultBaseArchivo, string rutaCompleta, string rutaSinRutaBase) =
                    documentoUtiliidad.CrearArchivo(nombreArchivo: cargaMasiva.ArchivoBase.Archivo,
                                                        content: cargaMasiva.ArchivoBase.Content,
                                                        rutaBase: rutaBase,
                                                        rutaRelativa: rutaRelativa,
                                                        tipoSubDirectorioGeneral: tipoSubDirectorioGeneral,
                                                        sobreEscribirSiExiste: true);

            if (resultBaseArchivo?.Success == true && !string.IsNullOrWhiteSpace(rutaCompleta))
            {
                cargaMasiva.ArchivoBase.RutaNombreArchivo = rutaCompleta;
            }

            DataTable dtPrincipal = null;

            dtPrincipal = npoiExcelExportarDatos.GetDataTableFromExcel(rutaCompleta);

            if (dtPrincipal != null)
            {
                DataRow rows;
                rows = dtPrincipal.Rows[0];
                dtPrincipal.Rows.Remove(rows);

                cargaMasiva.ListaPartidas = dtPrincipal.AsEnumerable().Select(d => new PartidaAlmacen()
                {
                    Marcas = d.Field<string>("Marcas"),
                    Numeros = d.Field<string>("Número"),
                    Modelo = d.Field<string>("Modelo"),
                    Mercancia = d.Field<string>("Mercancía"),
                    CantidadActual = Convert.ToInt32(d.Field<string>("Bultos")),
                    PesoActual = Convert.ToDecimal(d.Field<string>("Peso")),
                    PesoNeto = Convert.ToDecimal(d.Field<string>("Peso Neto")),
                    CantidadAveriados = Convert.ToInt32(d.Field<string>("Bulto Averiado")),
                    PesoAveriado = Convert.ToDecimal(d.Field<string>("Peso Averiado")),
                    DescripcionAveria = d.Field<string>("Avería"),
                    DescripcionTipoEmbalaje = d.Field<string>("Embalaje"),
                    DescripcionUnidadMedida = d.Field<string>("Unidad de Medida"),
                    BlHouse = d.Field<string>("BL House"),
                    EsPerecedero = Convert.ToBoolean(!string.IsNullOrWhiteSpace(d.Field<string>("Perecedero"))),
                    EsRefrigerado = Convert.ToBoolean(!string.IsNullOrWhiteSpace(d.Field<string>("Refrigerado"))),
                    EsCongelado = Convert.ToBoolean(!string.IsNullOrWhiteSpace(d.Field<string>("Congelado"))),
                    EsSusceptible = Convert.ToBoolean(!string.IsNullOrWhiteSpace(d.Field<string>("Suceptible"))),
                    Grupo = string.IsNullOrWhiteSpace(d.Field<string>("Grupo")) ? "SD" : d.Field<string>("Grupo")
                }).ToList();

                resultBase = documentoUtiliidad.EliminarArchivo(rutaCompleta);

            }

            if (resultBase.Success)
            {
                resultBase = ValidaDatosCargaMasiva(cargaMasiva.ListaPartidas);

                if (resultBase.Success)
                {

                    // Se utiliza para determinar el numero de Tarjas a partir de los grupos identificados
                    var listGruposPartidas = cargaMasiva.ListaPartidas.GroupBy(x => x.Grupo).ToList();

                    cargaMasiva.CRUDAction = ECRUDAction.Create;
                    var json = JsonConvert.SerializeObject(cargaMasiva.ListaPartidas);

                    // usp_SORF_CPartidaCargaMasiva

                    //TODO: Si la tarja ya esta confirmada se obtiene la fecha de ingreso, estado y exisntencia en true

                    //TODO: Obtener el identificador de la referencia

                    var tarjaSelected = _db.Tarjas.Where(t => t.Id == cargaMasiva.IdTarja).FirstOrDefault();

                    if (tarjaSelected != null)
                    {

                        int maxNumPartida = 0;

                        int numPartida = 0;

                        MonitorTarja _monitorTarjaAux = new MonitorTarja();
                        ResultBase<MonitorTarja> _resultBaseTarja = new ResultBase<MonitorTarja>();

                        int _contadorTarjas = 1;

                        using (var _dbContextTransaction = _db.Database.BeginTransaction())
                        {


                            List<MonitorTarja> _listaTarjas = new List<MonitorTarja>();

                            try
                            {

                                foreach (var _grupoPartidas in listGruposPartidas)
                                {

                                    // TODO: Se crean las tarjas
                                    _monitorTarjaAux = new MonitorTarja();

                                    string _grupoPartida = _grupoPartidas.Key;

                                    if (tarjaSelected.IdReferencia != null)
                                    {
                                        _monitorTarjaAux.IdReferencia = (int)tarjaSelected.IdReferencia;
                                    }

                                    _monitorTarjaAux.Estado = tarjaSelected.Estado != null ? (EstadoTarja)tarjaSelected.Estado : EstadoTarja.None;
                                    _monitorTarjaAux.TipoServicio = tarjaSelected.TipoServicio != null ? (TipoServicioTarja)tarjaSelected.TipoServicio : TipoServicioTarja.None;
                                    _monitorTarjaAux.TipoTarja = tarjaSelected.TipoTarja;
                                    _monitorTarjaAux.FechaAlta = DateTime.Now;
                                    _monitorTarjaAux.FechaIngreso = tarjaSelected.FechaIngreso != null ? (DateTime)tarjaSelected.FechaIngreso : DateTime.MinValue;
                                    _monitorTarjaAux.Observaciones = tarjaSelected.Observaciones;
                                    _monitorTarjaAux.IdEmpresaLogin = tarjaSelected.IdEmpresa;
                                    _monitorTarjaAux.GrupoViaje = _grupoPartida;
                                    _monitorTarjaAux.Consecutivo = _contadorTarjas;
                                    _monitorTarjaAux.CRUDAction = ECRUDAction.Read;

                                    // Se toma la tarja ya creada para guardar las nuevas partidas
                                    if (_contadorTarjas.Equals(1))
                                    {
                                        _monitorTarjaAux.CRUDAction = ECRUDAction.Update;
                                        _monitorTarjaAux.IdTarja = tarjaSelected.Id; 
                                        _resultBaseTarja = Guardar(_monitorTarjaAux);                        
                                    }
                                    else
                                    {
                                        _monitorTarjaAux.CRUDAction = ECRUDAction.Create;
                                        _resultBaseTarja = Guardar(_monitorTarjaAux);
                                        if (_resultBaseTarja.Success)
                                        {
                                            _monitorTarjaAux.IdTarja = _resultBaseTarja.Id;
                                        }
                                        else
                                        {
                                            throw new Exception("Error al crear la tarja en proceso de carga masiva.");
                                        }
                                    }

                                    if (!_resultBaseTarja.Success)
                                    {

                                        _dbContextTransaction.Rollback();
                                        resultBase.MensajeRespuesta = _resultBaseTarja.MensajeRespuesta;
                                        return resultBase;

                                    }

                                    maxNumPartida = _db.Partidas.Where(x => x.IdTarja == _monitorTarjaAux.IdTarja)
                                        .Max(x => x.NumeroPartida as int?) ?? 0;

                                    numPartida = maxNumPartida + 1;

                                    foreach (var _partidaAlmacen in _grupoPartidas)
                                    {
                                        Inventario inventario = new Inventario();
                                        inventario.IdEmpresaLogin = _monitorTarjaAux.IdEmpresaLogin;
                                        inventario.Estado = EstadoInventario.PendienteIngresar;
                                        inventario.Mercancia = _partidaAlmacen.Mercancia;
                                        inventario.CantidadInicial = _partidaAlmacen.CantidadActual;
                                        inventario.PesoInicial = _partidaAlmacen.PesoActual;
                                        inventario.Grupo = _grupoPartida;
                                        //inventario.IdTipoEmbalaje = _partidaAlmacen.IdTipoEmbalaje;
                                        //inventario.IdUnidadMedida = _partidaAlmacen.IdUnidadMedida;
                                        inventario.Existencia = false;
                                        inventario.FechaAlta = DateTime.Now;

                                        _db.Inventarios.Add(inventario);

                                        _db.SaveChanges();

                                        if (_monitorTarjaAux.IdReferencia != null && !_monitorTarjaAux.IdReferencia.Equals(0))
                                        {

                                            InventarioALO inventarioALO = new InventarioALO();
                                            inventarioALO.IdEmpresaLogin = _monitorTarjaAux.IdEmpresaLogin;
                                            inventarioALO.TipoMercancia = TipoMercanciaInventario.CargaSuelta;
                                            inventarioALO.IdReferencia = _monitorTarjaAux.IdReferencia;
                                            inventarioALO.IdInventario = inventario.Id; // Identificador del Inventario
                                            inventarioALO.FechaAlta = DateTime.Now;

                                            _db.InventarioALOs.Add(inventarioALO);

                                            _db.SaveChanges();

                                        }

                                        Partida partida = new Partida();
                                        partida.IdTarja = _monitorTarjaAux.IdTarja;
                                        partida.NumeroPartida = numPartida;
                                        partida.Marcas = _partidaAlmacen.Marcas;
                                        partida.Numeros = _partidaAlmacen.Numeros;
                                        partida.Modelo = _partidaAlmacen.Modelo;
                                        partida.CargaMasiva = true;
                                        partida.IdInventario = inventario.Id;
                                        partida.FechaAlta = DateTime.Now;

                                        numPartida = numPartida + 1;

                                        _db.Partidas.Add(partida);

                                        _db.SaveChanges();

                                    }

                                    _contadorTarjas = _contadorTarjas + 1;

                                } // foreach recorre las partidas

                            }
                            catch (Exception ex)
                            {

                                resultBase.MensajeRespuesta = "Error: " + ex.Message;
                                _dbContextTransaction.Rollback();
                            }

                            if (resultBase.Success)
                            {
                                _dbContextTransaction.Commit();
                            }


                        } // proceso database transaction


                    }
                    else
                    {

                        resultBase.MensajeRespuesta = "No exisete la Traja Seleccionada";

                    }

                }

            }

        }
        catch (Exception ex)
        {
            resultBase.MensajeRespuesta = "Error: " + ex.Message;
        }

        return resultBase;
    }

    public ResultBase<MonitorTarja> Confirmar(MonitorTarja monitorTarja)
    {
        var resultBase = new ResultBase<MonitorTarja>();

        try
        {
            var _resultValidarTarja = ValidarDatosTarja(monitorTarja);

            if (_resultValidarTarja.Success)
            {
                using (var _dbContextTransaction = _db.Database.BeginTransaction())
                {

                    try
                    {

                        var _tarja = _db.Tarjas.Where(x => x.Id.Equals(monitorTarja.IdTarja)).SingleOrDefault();

                        _tarja.Estado = EstadoTarja.Confirmado;
                        _tarja.FechaIngreso = DateTime.Now;
                        _db.Tarjas.Update(_tarja);
                        _db.SaveChanges();

                        var _listInventario = (from tInv in _db.Inventarios
                                               join tPartida in _db.Partidas on tInv.Id equals tPartida.IdInventario

                                               where tPartida.IdTarja.Equals(monitorTarja.IdTarja)
                                               select tInv).ToList();

                        foreach (var _invTemp in _listInventario)
                        {
                            _invTemp.Estado = EstadoInventario.Ingresado;
                            _invTemp.Existencia = true;
                            _db.Inventarios.Update(_invTemp);
                        }

                        _db.SaveChanges();

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

                resultBase.MensajeRespuesta = _resultValidarTarja.MensajeRespuesta;
            }

        }
        catch (Exception ex)
        {
            resultBase.MensajeRespuesta = "Error: " + ex.Message;
        }
        finally
        {
            

        }

        return resultBase;

    }

    public PaginadoResult<MonitorTarja> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaTarja> entidad)
    {
        var resultBase = new ResultBase<MonitorTarja>();
        // List<Tarja> listadoTarjas;
        List<MonitorTarja> _listMonitorTarjas = new List<MonitorTarja>();

        int _totalRegistros = 0;

        // _totalRegistros = _db.Tarjas.ToList().Count;

        // listadoTarjas = _db.Tarjas.OrderByDescending(x => x.Folio).ToList()
        //     .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
        //     .Take(entidad.RegistrosPorPagina).ToList();

        var listadoTarjasTemp = (from p in _db.Tarjas
                                 join e in _db.Referencias on p.IdReferencia equals e.Id into f
                                 from x in f.DefaultIfEmpty()
                                 join tClieAux in _db.catClientes on x.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                 from tClie in tClieTemp.DefaultIfEmpty()
                                 where
                                     p.ClaveReferencia.Contains(String.IsNullOrEmpty(entidad.Entidad.ClaveReferenciaTarja) ? p.ClaveReferencia : entidad.Entidad.ClaveReferenciaTarja)
                                     && x.Folio.Contains(String.IsNullOrEmpty(entidad.Entidad.FolioReferencia) ? x.Folio : entidad.Entidad.FolioReferencia)
                                     && tClie.RazonSocial.Contains(String.IsNullOrEmpty(entidad.Entidad.RazonSocialCliente) ? tClie.RazonSocial : entidad.Entidad.RazonSocialCliente)
                                     && p.Estado.Equals(entidad.Entidad.EstadoTarja.Equals(EstadoTarja.None) ? p.Estado : entidad.Entidad.EstadoTarja)
                                     && p.TipoServicio.Equals(entidad.Entidad.TipoServicioTarja.Equals(TipoServicioTarja.None) ? p.TipoServicio : entidad.Entidad.TipoServicioTarja)
                                 select new MonitorTarja
                                 {
                                     IdTarja = p.Id,
                                     Folio = p.Folio,
                                     IdReferencia = x.Id != null ? (int)x.Id : 0,
                                     FolioReferencia = x.Folio != null ? x.Folio : string.Empty,
                                     TipoTarja = p.TipoTarja,
                                     TipoServicio = p.TipoServicio != null ? (TipoServicioTarja)p.TipoServicio : TipoServicioTarja.None,
                                     Estado = p.Estado != null ? p.Estado : EstadoTarja.None,
                                     FechaIngreso = p.FechaIngreso != null ? (DateTime)p.FechaIngreso : DateTime.MinValue,
                                     FechaAlta = p.FechaAlta != null ? (DateTime)p.FechaAlta : DateTime.MinValue,
                                     GrupoViaje = p.GrupoViaje != null ? p.GrupoViaje : String.Empty,
                                     Observaciones = p.Observaciones,
                                     Consecutivo = (int)(p.Consecutivo != null ? p.Consecutivo : 0),
                                     ClaveReferencia = p.ClaveReferencia != null ? p.ClaveReferencia : String.Empty,
                                     IdClienteReferencia = (int)(x.IdCliente != null ? x.IdCliente : 0),
                                     RazonSocialCliente = String.IsNullOrEmpty(tClie.RazonSocial) ? String.Empty : tClie.RazonSocial,
                                 }).ToList();

        _totalRegistros = listadoTarjasTemp.Count;

        _listMonitorTarjas = listadoTarjasTemp.OrderByDescending(x => x.FechaAlta).ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        //listadoTarjasTemp = AddItems(listadoTarjas);

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorTarja>(_listMonitorTarjas, paginadoInfo);

        return _paginadoResult;
    }

    public PaginadoResult<MonitorTarjaInventario> ObtenerListaPaginadaTarjaInventarioPorIdReferencia(ConsultaCatalogoBase<ConsultaTarja> entidad)
    {
        int _totalRegistros = 0;

        List<MonitorTarjaInventario> _listMonitorTarjasInventario = new List<MonitorTarjaInventario>();

        var listadoTarjasTemp = (from p in _db.Tarjas
                                 join e in _db.Referencias on p.IdReferencia equals e.Id into f
                                 from x in f.DefaultIfEmpty()
                                 where p.IdReferencia == entidad.Entidad.IdReferencia
                                 select new MonitorTarja
                                 {
                                     IdTarja = p.Id,
                                     Folio = p.Folio,
                                     IdReferencia = x.Id != null ? (int)x.Id : 0,
                                     FolioReferencia = x.Folio != null ? x.Folio : string.Empty,
                                     TipoTarja = p.TipoTarja,
                                     TipoServicio = p.TipoServicio != null ? (TipoServicioTarja)p.TipoServicio : TipoServicioTarja.None,
                                     Estado = p.Estado != null ? p.Estado : EstadoTarja.None,
                                     FechaIngreso = p.FechaIngreso != null ? (DateTime)p.FechaIngreso : DateTime.MinValue,
                                     FechaAlta = p.FechaAlta != null ? (DateTime)p.FechaAlta : DateTime.MinValue,
                                     Observaciones = p.Observaciones,
                                     GrupoViaje = p.GrupoViaje != null ? p.GrupoViaje : String.Empty,
                                     Consecutivo = (int)(p.Consecutivo != null ? p.Consecutivo : 0),
                                     ClaveReferencia = p.ClaveReferencia != null ? p.ClaveReferencia : String.Empty,
                                 }).ToList();

        foreach (MonitorTarja _monitorTarjaAux in listadoTarjasTemp)
        {

            MonitorTarjaInventario _monitorTarjaInventario = new MonitorTarjaInventario();

            _monitorTarjaInventario.Tarja = new MonitorTarja();
            _monitorTarjaInventario.ListaPartidas = new List<MonitorPartida>();

            var listMonitorPartidaTemp = (from p in _db.Partidas
                                          join e in _db.Inventarios on p.IdInventario equals e.Id
                                          join d in _db.Ubicaciones on e.IdUbicacion equals d.Id into f
                                          from x in f.DefaultIfEmpty()
                                          where p.IdTarja == _monitorTarjaAux.IdTarja
                                          select new MonitorPartida
                                          {
                                              NumeroPartida = p.NumeroPartida,
                                              IdPartida = p.Id,
                                              IdInventario = e.Id,
                                              IdUbicacion = e.IdUbicacion != null ? (int)e.IdUbicacion : 0,
                                              ClaveUbicacion = String.IsNullOrEmpty(x.Clave) ? "" : x.Clave,
                                              Marcas = p.Marcas,
                                              Numeros = p.Numeros,
                                              BultosInicial = (int)e.CantidadInicial,
                                              PesoInicial = (decimal)e.PesoInicial,
                                              Mercancia = e.Mercancia,
                                              Existencia = e.Existencia
                                          }).ToList();


            _monitorTarjaAux.NumeroPartidas = listMonitorPartidaTemp.Count();
            _monitorTarjaAux.TotalPeso = listMonitorPartidaTemp.Sum(x => x.PesoInicial);

            _monitorTarjaInventario.ListaPartidas = listMonitorPartidaTemp;
            _monitorTarjaInventario.Tarja = _monitorTarjaAux;

            _listMonitorTarjasInventario.Add(_monitorTarjaInventario);

        }

        _totalRegistros = _listMonitorTarjasInventario.Count();

        _listMonitorTarjasInventario = _listMonitorTarjasInventario
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorTarjaInventario>(_listMonitorTarjasInventario, paginadoInfo);

        return _paginadoResult;
    }

    public PaginadoResult<MonitorTarja> ObtenerListaPaginadaPorIdReferencia(ConsultaCatalogoBase<ConsultaTarja> entidad)
    {
        var resultBase = new ResultBase<MonitorTarja>();
        List<Tarja> listadoTarjas;
        List<MonitorTarja> _listMonitorTarjas = new List<MonitorTarja>();

        int _totalRegistros = 0;

        listadoTarjas = _db.Tarjas.Where(x => x.IdReferencia.Equals(entidad.Entidad.IdReferencia)).ToList();

        _totalRegistros = listadoTarjas.Count;

        listadoTarjas = listadoTarjas.ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        _listMonitorTarjas = AddItems(listadoTarjas);

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorTarja>(_listMonitorTarjas, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorTarja> ObtenerPorId(int idTarja)
    {
        var resultBase = new ResultBase<MonitorTarja>();
        MonitorTarja monitorTarja = new MonitorTarja();

        try
        {
            var tarja = _db.Tarjas.Where(x => x.Id == idTarja).FirstOrDefault();

            var tarjaTemp = (from p in _db.Tarjas
                             join e in _db.Referencias on p.IdReferencia equals e.Id
                             where p.Id == idTarja
                             select new MonitorTarja
                             {
                                 IdTarja = p.Id,
                                 Folio = p.Folio,
                                 IdReferencia = e.Id,
                                 FolioReferencia = e.Folio,
                                 TipoTarja = p.TipoTarja,
                                 TipoServicio = p.TipoServicio != null ? (TipoServicioTarja)p.TipoServicio : TipoServicioTarja.None,
                                 Estado = p.Estado != null ? p.Estado : EstadoTarja.None,
                                 FechaIngreso = p.FechaIngreso != null ? (DateTime)p.FechaIngreso : DateTime.MinValue,
                                 Observaciones = p.Observaciones,
                                 Consecutivo = (int)(p.Consecutivo != null ? p.Consecutivo : 0),
                                 ClaveReferencia = p.ClaveReferencia != null ? p.ClaveReferencia : String.Empty,
                                 GrupoViaje = p.GrupoViaje != null ? p.GrupoViaje : String.Empty,
                             }).FirstOrDefault();

            if (tarjaTemp != null)
            {

                resultBase.Data = tarjaTemp;

            }
            else if (tarja != null)
            {

                resultBase.Id = tarja.Id;

                List<MonitorTarja> listMonitorTarja = AddItems(new List<Tarja> { tarja });
                resultBase.Data = listMonitorTarja.FirstOrDefault();

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

    private ResultBase ValidarDatosTarja(MonitorTarja monitorTarja)
    {
        var _result = new ResultBase();


        var _tarja = (from tTarja in _db.Tarjas
                      where tTarja.Id.Equals(monitorTarja.IdTarja) select tTarja).SingleOrDefault();

        if (_tarja != null)
        {
            if (_tarja.Estado.Equals(EstadoTarja.Preliminar))
            {

                var _listPartidas = (from tPartidas in _db.Partidas
                                    join tInv in _db.Inventarios on tPartidas.IdInventario equals tInv.Id
                                    where tPartidas.IdTarja.Equals(_tarja.Id) && tInv.FechaIngreso.HasValue
                                    select tPartidas).ToList();

                if (_listPartidas.Count.Equals(0))
                {
                    _result.MensajeRespuesta = "La Tarja no se puede Confirmar, faltan partidas por ingresar.";
                    return _result;
                }

            }
            else if (_tarja.FechaIngreso.HasValue && _tarja.Estado.Equals(EstadoTarja.Confirmado))
            {
                _result.MensajeRespuesta = "La Tarja ya ha sido confirmada y cuenta con Fecha de Ingreso.";
                return _result;
            }
            else if (_tarja.Estado.Equals(EstadoTarja.Cancelado))
            {
                _result.MensajeRespuesta = "La Tarja esta cancelada.";
                return _result;
            }

        }

        return _result;
        
    }

    private ResultBase ValidaDatosCargaMasiva(List<PartidaAlmacen> partidaAlmacenList)
    {
        try
        {
            var resultBase = new ResultBase();
            var mensaje = new StringBuilder();

            // Se obtiene el número de partidas
            int numPartidas = partidaAlmacenList.Count();

            // Se verifica que no existan grupos sin identificación
            int numGrupoSinDato = partidaAlmacenList.Where(p => p.Grupo.Equals("SD")).Count();

            if (!numGrupoSinDato.Equals(0))
            {
                if (numPartidas != numGrupoSinDato)
                {
                    resultBase.MensajeRespuesta = "Existen Partidas que no tienen identificado el GRUPO";
                    return resultBase;
                }
            }

            foreach (var p in partidaAlmacenList)
            {
                if (!string.IsNullOrWhiteSpace(p.Marcas))
                {
                    p.Marcas = p.Marcas.Trim();

                    if (p.Marcas.Length > 255)
                    {
                        resultBase.MensajeRespuesta = "Marcas debe tener una longitud menir a 255.";
                        return resultBase;
                    }
                }
                else
                {
                    resultBase.MensajeRespuesta = "Marcas es un campo requerido. Verifique.";
                    return resultBase;
                }

                if (!string.IsNullOrWhiteSpace(p.Numeros))
                {
                    p.Numeros = p.Numeros.Trim();

                    if (p.Numeros.Length > 50)
                    {
                        resultBase.MensajeRespuesta = "Números debe tener una longitud menor a 50.";
                        return resultBase;
                    }
                    else
                    {

                        var _existePartidaAlmacen = (from _tarja in _db.Tarjas
                                                     join _part in _db.Partidas on _tarja.Id equals _part.IdTarja
                                                     where _part.Numeros.Equals(p.Numeros) && _tarja.Estado != EstadoTarja.Cancelado
                                                     select new { p = _part }).Any();

                        if (_existePartidaAlmacen)
                        {

                            resultBase.MensajeRespuesta = $"Números: {p.Numeros}, ya existe el registro en el inventario.";
                            return resultBase;

                        }

                    }
                }

                if (!string.IsNullOrWhiteSpace(p.Modelo))
                {
                    p.Modelo = p.Modelo.Trim();

                    if (p.Modelo.Length > 50)
                    {
                        resultBase.MensajeRespuesta = "Modelo debe tener una longitud menor a 50.";
                        return resultBase;
                    }
                }

                if (!string.IsNullOrWhiteSpace(p.Mercancia))
                {
                    p.Mercancia = p.Mercancia.Trim();

                    if (p.Mercancia.Length > 4000)
                    {
                        resultBase.MensajeRespuesta = "Mercancía debe tener una longitud menor a 4000.";
                        return resultBase;
                    }
                }
                else
                {
                    resultBase.MensajeRespuesta = "Mercancía es un campo requerido. Verifique.";
                    return resultBase;
                }

                if (p.CantidadActual <= 0)
                {
                    resultBase.MensajeRespuesta = "Bultos es un campo requerido. Verifique.";
                    return resultBase;
                }

                if (p.PesoActual <= 0)
                {
                    resultBase.MensajeRespuesta = "Peso es un campo requerido. Verifique.";
                    return resultBase;
                }

                if (!string.IsNullOrWhiteSpace(p.DescripcionAveria))
                {
                    p.DescripcionAveria = p.DescripcionAveria.Trim();

                    if (p.DescripcionAveria.Length > 255)
                    {
                        resultBase.MensajeRespuesta = "Avería debe tener una longitud menor a 255.";
                        return resultBase;
                    }
                }

                if (!string.IsNullOrWhiteSpace(p.DescripcionTipoEmbalaje))
                {
                    p.DescripcionTipoEmbalaje = p.DescripcionTipoEmbalaje.Trim();

                    if (p.DescripcionTipoEmbalaje.Length > 10)
                    {
                        resultBase.MensajeRespuesta = "Embalaje debe tener una longitud menor a 10.";
                        return resultBase;
                    }
                }
                else
                {
                    resultBase.MensajeRespuesta = "Embalaje es un campo requerido. Verifique.";
                    return resultBase;
                }

                if (!string.IsNullOrWhiteSpace(p.DescripcionUnidadMedida))
                {
                    p.DescripcionUnidadMedida = p.DescripcionUnidadMedida.Trim();

                    if (p.DescripcionUnidadMedida.Length > 10)
                    {
                        resultBase.MensajeRespuesta = "Unidad de de Medida debe tener una longitud menor a 10.";
                        return resultBase;
                    }
                }
                else
                {
                    resultBase.MensajeRespuesta = "Unidad de Medida es un campo requerido. Verifique.";
                    return resultBase;
                }

                if (!string.IsNullOrWhiteSpace(p.BlHouse))
                {
                    p.BlHouse = p.BlHouse.Trim();

                    if (p.BlHouse.Length > 50)
                    {
                        resultBase.MensajeRespuesta = "BL House debe tener una longitud menor a 50";
                        return resultBase;
                    }
                }
            }

            return resultBase;
        }
        catch
        {
            throw;
        }
    }
    
    private List<MonitorTarja> AddItems(List<Tarja> listadoTarjas) {

        MonitorTarja monitorTarja = new MonitorTarja();

        List<MonitorTarja> listMonitorTarja = new List<MonitorTarja>();

        foreach (Tarja _tarja in listadoTarjas)
        {

            monitorTarja = new MonitorTarja();
            monitorTarja.IdTarja = _tarja.Id;
            monitorTarja.IdReferencia = _tarja.IdReferencia != null ? (int)_tarja.IdReferencia : 0;
            monitorTarja.Folio = _tarja.Folio;
            monitorTarja.Estado = _tarja.Estado != null ? (EstadoTarja)_tarja.Estado : EstadoTarja.None;
            monitorTarja.TipoServicio = _tarja.TipoServicio != null ? (TipoServicioTarja)_tarja.TipoServicio : TipoServicioTarja.None;
            monitorTarja.TipoTarja = _tarja.TipoTarja;
            monitorTarja.FechaAlta = _tarja.FechaAlta;
            monitorTarja.NumeroPartidas = _db.Partidas.Where(x => x.IdTarja.Equals(_tarja.Id)).ToList().Count;
            monitorTarja.TotalPeso = obtenerTotalPesoPartidaByIdTarja(_tarja.Id);
            monitorTarja.CantidadServicios = _db.FolioServicios.Where(x => x.IdTarja.Equals(_tarja.Id)).ToList().Count;
            monitorTarja.FechaIngreso = _tarja.FechaIngreso != null ? (DateTime)_tarja.FechaIngreso : DateTime.MinValue;
            monitorTarja.ClaveReferencia = _tarja.ClaveReferencia;
            monitorTarja.Observaciones = _tarja.Observaciones;

            listMonitorTarja.Add(monitorTarja);

        }


        return listMonitorTarja;

    }

    private decimal obtenerTotalPesoPartidaByIdTarja(int _idTarja)
    {


        var _listaPartidasTotalPesoAux = (from p in _db.Partidas
                                          join e in _db.Inventarios on p.IdInventario equals e.Id
                                          where p.IdTarja.Equals(_idTarja)
                                          select new { p.IdInventario, e.PesoInicial }).Sum(x => x.PesoInicial);

        return (decimal)_listaPartidasTotalPesoAux;


    }

}
