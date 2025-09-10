using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class PartidaRepositorio : GenericoRepositorio<Referencia>, IPartidaRepositorio
{
    private readonly ApplicationDbContext _db;

    public PartidaRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorPartida> Guardar(MonitorPartida monitorPartida)
    {
        var resultBase = new ResultBase<MonitorPartida>();

        Inventario _inventario = new Inventario();
        Partida _partida = new Partida();

        if (monitorPartida.CRUDAction.Equals(ECRUDAction.Update))
        {
            _inventario = _db.Inventarios.Where(x => x.Id.Equals(monitorPartida.IdInventario)).SingleOrDefault();
            _partida = _db.Partidas.Where(x => x.IdInventario.Equals(monitorPartida.IdInventario)).SingleOrDefault();
        }

        _inventario.Mercancia = String.IsNullOrEmpty(monitorPartida.Mercancia) ? _inventario.Mercancia : monitorPartida.Mercancia;
        _inventario.CantidadInicial = monitorPartida.BultosInicial != null ? monitorPartida.BultosInicial : _inventario.CantidadInicial;
        _inventario.PesoInicial = monitorPartida.PesoInicial != null ? monitorPartida.PesoInicial : _inventario.PesoInicial;
        if (!monitorPartida.IdUbicacion.Equals(0)) {
            _inventario.IdUbicacion = monitorPartida.IdUbicacion;
        }
        _inventario.IdEmpresaLogin = monitorPartida.IdEmpresaLogin;
                
        _partida.NumeroPartida = (monitorPartida.NumeroPartida != null || monitorPartida.NumeroPartida.Equals(0)) ? _partida.NumeroPartida : monitorPartida.NumeroPartida;
        _partida.Numeros = String.IsNullOrEmpty(monitorPartida.Numeros) ? _partida.Numeros : monitorPartida.Numeros;
        _partida.Marcas = String.IsNullOrEmpty(monitorPartida.Marcas) ? _partida.Marcas : monitorPartida.Marcas;
        _partida.Modelo = String.IsNullOrEmpty(monitorPartida.Modelo) ? _partida.Marcas : monitorPartida.Modelo;
        _partida.IdEmpresaLogin = monitorPartida.IdEmpresaLogin;

        if (monitorPartida.CRUDAction.Equals(ECRUDAction.Create))
        {

            using (var _dbContextTransaction = _db.Database.BeginTransaction())
            {

                try
                {

                    _inventario.FechaAlta = DateTime.Now;

                    _db.Inventarios.Add(_inventario);

                    _db.SaveChanges();

                    _partida.IdInventario = _inventario.Id;
                    _partida.FechaAlta = DateTime.Now;

                    _db.Partidas.Add(_partida);

                    _db.SaveChanges();

                }
                catch (Exception ex)
                {

                    resultBase.MensajeRespuesta = "Error: " + ex.Message;
                    _dbContextTransaction.Rollback();

                }

                _dbContextTransaction.Commit();

            }

        }
        else if (monitorPartida.CRUDAction.Equals(ECRUDAction.Update))
        {

            using (var _dbContextTransaction = _db.Database.BeginTransaction())
            {

                try
                {

                    _db.Inventarios.Update(_inventario);
                    _db.Partidas.Update(_partida);

                    _db.SaveChanges();

                }
                catch (Exception ex)
                {

                    resultBase.MensajeRespuesta = "Error: " + ex.Message;
                    _dbContextTransaction.Rollback();

                }

                _dbContextTransaction.Commit();

            }

        }

        monitorPartida.IdPartida = _partida.Id;

        resultBase.Id = _partida.Id;
        resultBase.Data = monitorPartida;

        return resultBase;
    }

    public PaginadoResult<MonitorPartida> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaPartida> entidad)
    {
        throw new NotImplementedException();
    }

    public PaginadoResult<MonitorPartida> ObtenerListaPaginadaPorIdTarja(ConsultaCatalogoBase<ConsultaPartida> entidad)
    {
        var resultBase = new ResultBase<MonitorPartida>();
        List<Partida> listadoPartidas;
        List<MonitorPartida> _listMonitorPartidas = new List<MonitorPartida>();

        int _totalRegistros = 0;

        listadoPartidas = _db.Partidas.Where(x => x.IdTarja == entidad.Entidad.IdTarja).ToList();

        var listMonitorPartidaTemp = (from p in _db.Partidas
                join tTarja in _db.Tarjas on p.IdTarja equals tTarja.Id
                join e in _db.Inventarios on p.IdInventario equals e.Id 
                join d in _db.Ubicaciones on e.IdUbicacion equals d.Id into f
                from x in f.DefaultIfEmpty()
                where p.IdTarja == entidad.Entidad.IdTarja
                select new MonitorPartida
                {
                    NumeroPartida = p.NumeroPartida,
                    IdPartida = p.Id,
                    IdInventario = e.Id,
                    ClaveReferenciaTarja = tTarja.ClaveReferencia + "/" + p.NumeroPartida,
                    IdUbicacion = e.IdUbicacion != null ? (int)e.IdUbicacion : 0,
                    ClaveUbicacion = String.IsNullOrEmpty(x.Clave) ? "" : x.Clave,
                    FechaIngreso = (DateTime)(e.FechaIngreso != null ? e.FechaIngreso : DateTime.MinValue),
                    FechaRecoleccion = (DateTime)(e.FechaRecoleccion != null ? e.FechaRecoleccion : DateTime.MinValue),
                    Marcas = p.Marcas,
                    Modelo = p.Modelo,
                    Numeros = p.Numeros,
                    BultosInicial = (int)e.CantidadInicial,
                    PesoInicial = (decimal)e.PesoInicial,
                    Mercancia = e.Mercancia,
                    Existencia = e.Existencia,
                    Estadias = (int)((TimeSpan)((e.FechaSalida != null ? e.FechaSalida : DateTime.Now) - (e.FechaIngreso != null ? e.FechaIngreso : DateTime.Now))).TotalDays,
                }).ToList();

        _totalRegistros = listMonitorPartidaTemp.Count;

        listMonitorPartidaTemp = listMonitorPartidaTemp
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        //_listMonitorPartidas = AddItems(listadoPartidas);

        _listMonitorPartidas = listMonitorPartidaTemp;

        /*
        var item = (
                from ai in context.app_information
                join al in context.app_language on ai.languageid equals al.id 
                where (al.languagecode == "es")
                select new AppInformation 
                {
                    id = ai.id,
                    title = ai.title,
                    description = ai.description,
                    coverimageurl = ai.coverimageurl
                }).FirstOrDefault();

            or try shorter

            var item = context.app_information
                .Where(ai => ai.app_language.languagecode == "es")
                .Select(ai => new AppInformation 
                {
                    id = ai.id,
                    title = ai.title,
                    description = ai.description,
                    coverimageurl = ai.coverimageurl
                })
                .FirstOrDefault();

        */

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorPartida>(_listMonitorPartidas, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorPartida> ObtenerPorId(int idTarja)
    {
        throw new NotImplementedException();
    }

    private List<MonitorPartida> AddItems(List<Partida> listadoPartidas)
    {

        MonitorPartida monitorPartida = new MonitorPartida();

        List<MonitorPartida> listMonitorPartida = new List<MonitorPartida>();

        foreach (Partida _partida in listadoPartidas)
        {

            monitorPartida = new MonitorPartida();

            monitorPartida.NumeroPartida = _partida.NumeroPartida;
            monitorPartida.IdPartida = _partida.Id;
            monitorPartida.Marcas = _partida.Marcas;
            monitorPartida.Numeros = _partida.Numeros;

            listMonitorPartida.Add(monitorPartida);
        }

        return listMonitorPartida;
    }
}
