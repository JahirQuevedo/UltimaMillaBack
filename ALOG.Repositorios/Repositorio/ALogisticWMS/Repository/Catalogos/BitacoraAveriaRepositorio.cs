using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

public class BitacoraAveriaRepositorio : GenericoRepositorio<BitacoraAveriaInventario>, IBitacoraAveriaRepositorio
{

    private readonly ApplicationDbContext _db;

    public BitacoraAveriaRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase Eliminar(int idBitacoraAveria)
    {
        var resultBase = new ResultBase();

        var _bitacoraAveriaInventario = _db.BitacoraAveriaInventarios.Where(x => x.Id == idBitacoraAveria).SingleOrDefault();

        if (_bitacoraAveriaInventario != null)
        {

            _db.BitacoraAveriaInventarios.Remove(_bitacoraAveriaInventario);
            _db.SaveChanges();

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontró información del registro solicitado.";
        }

        return resultBase;
    }

    public ResultBase<MonitorBitacoraAveria> Guardar(MonitorBitacoraAveria monitorBitacoraAveria)
    {
        BitacoraAveriaInventario _bitacoraAveriaInventario = new BitacoraAveriaInventario();

        ResultBase<MonitorBitacoraAveria> _result = new ResultBase<MonitorBitacoraAveria>();

        if (!monitorBitacoraAveria.IdBitacoraAveria.Equals(0))
            _bitacoraAveriaInventario = _db.BitacoraAveriaInventarios.Where(x => x.Id == monitorBitacoraAveria.IdBitacoraAveria).SingleOrDefault();

        var _existeBitacoraAveria = _db.BitacoraAveriaInventarios
                    .Where(x => x.IdCodigoDesperfecto.Equals(monitorBitacoraAveria.IdCodigoDesperfecto)
                            && x.IdTipoDesperfecto.Equals(monitorBitacoraAveria.IdTipoDesperfecto)
                            && x.IdTipoSeveridad.Equals(monitorBitacoraAveria.IdTipoSeveridad)
                            && x.IdInventario.Equals(monitorBitacoraAveria.IdInventario)).Any();

        if (!_existeBitacoraAveria)
        {

            _bitacoraAveriaInventario.IdInventario = monitorBitacoraAveria.IdInventario;
            _bitacoraAveriaInventario.IdCodigoDesperfecto = monitorBitacoraAveria.IdCodigoDesperfecto;
            _bitacoraAveriaInventario.IdTipoDesperfecto = monitorBitacoraAveria.IdTipoDesperfecto;
            _bitacoraAveriaInventario.IdTipoSeveridad = monitorBitacoraAveria.IdTipoSeveridad;

            _bitacoraAveriaInventario.DescripcionAveria = monitorBitacoraAveria.DescripcionAveria;

            if (monitorBitacoraAveria.CRUDAction.Equals(ECRUDAction.Create))
            {
                _db.BitacoraAveriaInventarios.Add(_bitacoraAveriaInventario);
            }
            else if (monitorBitacoraAveria.CRUDAction.Equals(ECRUDAction.Update))
            {
                _db.BitacoraAveriaInventarios.Update(_bitacoraAveriaInventario);
            }

            _db.SaveChanges();

            monitorBitacoraAveria.IdBitacoraAveria = _bitacoraAveriaInventario.Id;

            _result.Id = _bitacoraAveriaInventario.Id;
            _result.Data = monitorBitacoraAveria;

        }
        else
        {
            _result.MensajeRespuesta = "Ya se encuentra registrado el Código de Daño - Tipo - Severidad para la unidad seleccionada.";
        }

        return _result;
    }

    public PaginadoResult<MonitorBitacoraAveria> ObtenerListaPaginadaPorIdInventario(ConsultaCatalogoBase<ConsultaBitacoraAveria> entidad)
    {

        int _totalRegistros = 0;


        List<MonitorBitacoraAveria> _listMonitorBitacoraAveria = new List<MonitorBitacoraAveria>();

        _listMonitorBitacoraAveria = (from p in _db.BitacoraAveriaInventarios
                                      join e in _db.Inventarios on p.IdInventario equals e.Id into f
                                      from x in f.DefaultIfEmpty()
                                      join _part in _db.Partidas on p.IdInventario equals _part.IdInventario
                                      join _codDesperfecto in _db.CodigoDesperfectos on p.IdCodigoDesperfecto equals _codDesperfecto.Id
                                      join _tipoDesperfecto in _db.TipoDesperfectos on p.IdTipoDesperfecto equals _tipoDesperfecto.Id
                                      join _tipoSeveridad in _db.TipoSeveridades on p.IdTipoSeveridad equals _tipoSeveridad.Id
                                      where p.IdInventario == entidad.Entidad.IdInventario
                                      select new MonitorBitacoraAveria
                                      {
                                          IdBitacoraAveria = p.Id,
                                          IdInventario = x.Id,
                                          IdCodigoDesperfecto = _codDesperfecto.Id,
                                          ClaveCodigoDesperfecto = _codDesperfecto.Clave,
                                          DescripcionCodgioDesperfecto = _codDesperfecto.Descripcion,
                                          IdTipoDesperfecto = _tipoDesperfecto.Id,
                                          ClaveTipoDesperfecto = _tipoDesperfecto.Clave,
                                          DescripcionTipoDesperfecto = _tipoDesperfecto.Descripcion,
                                          IdTipoSeveridad = _tipoSeveridad.Id,
                                          ClaveTipoSeveridad = _tipoSeveridad.Clave,
                                          DescripcionTipoSeveridad = _tipoSeveridad.Descripcion,
                                          DescripcionAveria = p.DescripcionAveria,

                                      }).ToList();

        _totalRegistros = _listMonitorBitacoraAveria.Count;

        _listMonitorBitacoraAveria = _listMonitorBitacoraAveria
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorBitacoraAveria>(_listMonitorBitacoraAveria, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorBitacoraAveria> ObtenerPorId(int idBitacoraAveria)
    {
        throw new NotImplementedException();
    }
}
