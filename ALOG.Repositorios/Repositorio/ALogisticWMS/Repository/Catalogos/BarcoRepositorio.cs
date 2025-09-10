using ALOG.Modelos;
using ALOG.Enums;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using Microsoft.EntityFrameworkCore.Storage;
using NPOI.OpenXmlFormats.Dml.Spreadsheet;
using NPOI.SS.Formula.Functions;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios;

public class BarcoRepositorio : GenericoRepositorio<Barco>, IBarcoRepositorio
{

    private readonly ApplicationDbContext _db;

    public BarcoRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorBarco> Guardar(MonitorBarco _monitorBarco)
    {
        var resultBase = new ResultBase<MonitorBarco>();

        try
        {
            //TODO : Validar Datos
            Barco _barco = new Barco() { Nombre = String.Empty };

            if (!_monitorBarco.IdBarco.Equals(0))
            {
                _barco = _db.Barcos.Where(x => x.Id.Equals(_monitorBarco.IdBarco)).FirstOrDefault();
            }

            _barco.IdCatPais = _monitorBarco.IdPais.Equals(0) ? _barco.IdCatPais : _monitorBarco.IdPais;
            _barco.IdCatNaviera = _monitorBarco.IdNaviera.Equals(0) ? _barco.IdCatNaviera : _monitorBarco.IdNaviera;
            _barco.Nombre = String.IsNullOrEmpty(_monitorBarco.Nombre) ? _barco.Nombre : _monitorBarco.Nombre;
            _barco.Activo = true;

            if (_monitorBarco.CRUDAction.Equals(ECRUDAction.Create))
            {
                _barco.FechaAlta = DateTime.Now;
                _db.Barcos.Add(_barco);
            }
            else if (_monitorBarco.CRUDAction.Equals(ECRUDAction.Update))
            {
                _db.Barcos.Update(_barco);
            }

            _db.SaveChanges();

        }
        catch (Exception ex)
        {

            resultBase.MensajeRespuesta = ex.Message + " - " + ex.InnerException.Message;

        }
        finally
        {

        }

        return resultBase;
    }

    public ResultBase Eliminar(int idBarco)
    {

        var resultBase = new ResultBase();

        var _barcos = _db.Barcos.Where(x => x.Id == idBarco).SingleOrDefault();

        if (_barcos != null)
        {

            _db.Barcos.Remove(_barcos);
            _db.SaveChanges();

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontró información del registro solicitado.";
        }

        return resultBase;
    }

    public PaginadoResult<MonitorBarco> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaBarco> entidad)
    {
        // List<Barco> listadoBarcos;
        List<MonitorBarco> _listMonitorBarcos = new List<MonitorBarco>();
        try
        {

            int _totalRegistros = 0;

            // listadoBarcos = _db.Barcos.Where(x => x.Activo.Equals(true)).ToList()
            //         .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            //         .Take(entidad.RegistrosPorPagina).ToList();


            // _listMonitorBarcos = AddItems(listadoBarcos);

            _listMonitorBarcos = (from tBarco in _db.Barcos
                                  join tPaisAux in _db.catPaises on tBarco.IdCatPais equals tPaisAux.IdCatPaises into tPaisTemp
                                  from tPais in tPaisTemp.DefaultIfEmpty()
                                  join tNavieraAux in _db.catNavieras on tBarco.IdCatNaviera equals tNavieraAux.IdCatNaviera into tNavieraTemp
                                  from tNaviera in tNavieraTemp.DefaultIfEmpty()
                                  where
                                    tBarco.Activo.Equals(true)
                                    && tBarco.Nombre.Contains(String.IsNullOrEmpty(entidad.Entidad.Nombre) ? tBarco.Nombre : entidad.Entidad.Nombre)
                                  select new MonitorBarco
                                  {
                                      IdBarco = tBarco.Id,
                                      Nombre = tBarco.Nombre,
                                      IdPais = tPais.IdCatPaises != null ? tPais.IdCatPaises : 0,
                                      Pais = String.IsNullOrEmpty(tPais.Nombre) ? String.Empty : tPais.Nombre,
                                      IdNaviera = tNaviera.IdCatNaviera != null ? tNaviera.IdCatNaviera : 0,
                                      Naviera = String.IsNullOrEmpty(tNaviera.RazonSocial) ? String.Empty : tNaviera.RazonSocial,
                                  }).ToList();

            _totalRegistros = _listMonitorBarcos.Count();

            _listMonitorBarcos = _listMonitorBarcos.OrderBy(x => x.Nombre).ToList()
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

            var paginadoInfo = new PaginadoInfo()
            {
                RegistrosPorPagina = entidad.RegistrosPorPagina,
                NumeroDePagina = entidad.NumeroDePagina,
                TotalRegistros = _totalRegistros
            };

            var _paginadoResult = new PaginadoResult<MonitorBarco>(_listMonitorBarcos, paginadoInfo);

            return _paginadoResult;

        }
        catch (Exception ex)
        {

            throw;
        }

    }

    public async Task<ResultBase<List<MonitorBarco>>> ObtenerPorNombreContains(string nombre)
    {

        ResultBase<List<MonitorBarco>> resultBase = new ResultBase<List<MonitorBarco>>();
        List<MonitorBarco> _listMonitorBarco = new List<MonitorBarco>();


        var litsBarco = await _db.Barcos.Where(x => x.Nombre.Contains(nombre)).ToListAsync();
        if (litsBarco != null)
        {

            foreach (Barco _barco in litsBarco)
            {

                MonitorBarco _monitorBarco = new MonitorBarco();

                _monitorBarco.IdBarco = _barco.Id;
                _monitorBarco.Nombre = _barco.Nombre;

                _listMonitorBarco.Add(_monitorBarco);

            }

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        resultBase.Data = _listMonitorBarco;

        return resultBase;

    }

    public ResultBase<Barco> ObtenerPorId(int id)
    {
        ResultBase<Barco> result = new ResultBase<Barco>();

        try
        {
            var barco = _db.Barcos.Where(x => x.Id == id).FirstOrDefault();
            if (barco != null)
            {

                result.Data = barco;

            }
            else
            {
                result.MensajeRespuesta = "No se encontró el identificador del Barco";
            }
        }
        catch (Exception ex)
        {
            result.MensajeRespuesta = ex.Message;
        }

        return result;
    }

    private List<MonitorBarco> AddItems(List<Barco> listadoBarcos)
    {

        List<MonitorBarco> _listMonitorBarcos = new List<MonitorBarco>();

        MonitorBarco _monitorBarco = new MonitorBarco();

        foreach (Barco _barco in listadoBarcos)
        {

            _monitorBarco = new MonitorBarco();

            _monitorBarco.Nombre = _barco.Nombre;
            _monitorBarco.Naviera = String.IsNullOrEmpty(_barco.Naviera?.RazonSocial) ? String.Empty : _barco.Naviera?.RazonSocial;
            _monitorBarco.Pais = String.IsNullOrEmpty(_barco.Pais?.Nombre) ? String.Empty : _barco.Pais?.Nombre;

            _listMonitorBarcos.Add(_monitorBarco);
        }

        return _listMonitorBarcos;

    }
}
