using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class UbicacionAlmacenRepositorio : GenericoRepositorio<Ubicacion>, IUbicacionAlmacenRepositorio
{

    private readonly ApplicationDbContext _db;

    public UbicacionAlmacenRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


    public PaginadoResult<MonitorUbicacionAlmacen> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaUbicacion> entidad)
    {
        throw new NotImplementedException();
    }

    public ResultBase<MonitorUbicacionAlmacen> ObtenerPorClave(string clave)
    {
        var resultBase = new ResultBase<MonitorUbicacionAlmacen>();
        MonitorUbicacionAlmacen monitorUbicacion = new MonitorUbicacionAlmacen();

        try
        {

            var ubicacionTemp = (from p in _db.Ubicaciones
                                 join e in _db.ZonaAlmacenes on p.IdZonaAlmacenaje equals e.Id
                                 join d in _db.Inventarios on p.Id equals d.IdUbicacion into f
                                 from x in f.DefaultIfEmpty()
                                 where p.Clave.Equals(clave)
                                 select new MonitorUbicacionAlmacen
                                 {

                                     Clave = p.Clave,
                                     Descripcion = p.Descripcion,
                                     IdUbicacionAlmacen = p.Id,
                                     IdZonaAlmacenaje = p.IdZonaAlmacenaje != null ? (int)p.IdZonaAlmacenaje : 0,
                                     IdInventario = x.Id != null ? x.Id : 0,
                                     Disponible = x.Id != null ? false : true,
                                     ClaveZona = e.Clave,
                                     DescripcionZona = e.Descripcion

                                 }).FirstOrDefault();

            if (ubicacionTemp != null)
            {

                resultBase.Data = ubicacionTemp;

            }
            else
            {
                resultBase.MensajeRespuesta = "No se encontró la Ubicación.";
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

    public ResultBase<MonitorUbicacionAlmacen> ObtenerPorId(int IdUbicacion)
    {
        var resultBase = new ResultBase<MonitorUbicacionAlmacen>();
        MonitorUbicacionAlmacen monitorUbicacion = new MonitorUbicacionAlmacen();

        try
        {

            var ubicacionTemp = (from p in _db.Ubicaciones
                                 join e in _db.ZonaAlmacenes on p.IdZonaAlmacenaje equals e.Id
                                 join d in _db.Inventarios on p.Id equals d.IdUbicacion into f
                                 from x in f.DefaultIfEmpty()
                                 where p.Id == IdUbicacion
                                 select new MonitorUbicacionAlmacen
                                 {

                                     Clave = p.Clave,
                                     Descripcion = p.Descripcion,
                                     IdUbicacionAlmacen = p.Id,
                                     IdZonaAlmacenaje = p.IdZonaAlmacenaje != null ? (int)p.IdZonaAlmacenaje : 0,
                                     IdInventario = x.Id != null ? x.Id : 0,
                                     Disponible = x.Id != null ? false : true,
                                     ClaveZona = e.Clave,
                                     DescripcionZona = e.Descripcion

                                 }).FirstOrDefault();

            if (ubicacionTemp != null)
            {

                resultBase.Data = ubicacionTemp;

            }
            else
            {
                resultBase.MensajeRespuesta = "No se encontró la Ubicación.";
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


    public ResultBase<List<MonitorUbicacionAlmacen>> ObtenerPorClaveContains(int idZonaAlmacenaje, string clave)
    {
        ResultBase<List<MonitorUbicacionAlmacen>> resultBase = new ResultBase<List<MonitorUbicacionAlmacen>>();

        var _listMonitorUbicacion = (from p in _db.Ubicaciones
                                     join e in _db.Inventarios on p.Id equals e.IdUbicacion into f
                                     from x in f.DefaultIfEmpty()
                                     where p.Clave.Contains(clave) && x.Id == null && p.IdZonaAlmacenaje == idZonaAlmacenaje
                                     select new MonitorUbicacionAlmacen
                                     {

                                         Clave = p.Clave,
                                         Descripcion = p.Descripcion,
                                         IdInventario = x.Id != null ? x.Id : 0,
                                         Disponible = x.Id != null ? false : true,
                                         IdUbicacionAlmacen = p.Id,
                                         IdZonaAlmacenaje = (int)(p.IdZonaAlmacenaje != null ? p.IdZonaAlmacenaje : 0),

                                     }).OrderBy(x => x.Clave).ToList();

        if (_listMonitorUbicacion != null)
        {

            resultBase.Data = _listMonitorUbicacion;

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        return resultBase;
    }
    
    public ResultBase<List<MonitorUbicacionAlmacen>> ObtenerListaUbicacionPorIdAlmacenaje(int idZonaAlmacenaje)
    {
        ResultBase<List<MonitorUbicacionAlmacen>> resultBase = new ResultBase<List<MonitorUbicacionAlmacen>>();

        var _listMonitorUbicacion = (from tUbica in _db.Ubicaciones 
        join tInv in _db.Inventarios on tUbica.Id equals tInv.IdUbicacion 
        join tPartida in _db.Partidas on tInv.Id equals tPartida.IdInventario
        where tUbica.IdZonaAlmacenaje.Equals(idZonaAlmacenaje)
            && tInv.Existencia.Equals(true)
                select new MonitorUbicacionAlmacen
                                     {
                                         Clave = tUbica.Clave,
                                         Descripcion = tUbica.Descripcion,
                                         IdInventario = tInv.Id != null ? tInv.Id : 0,
                                         Numeros = tPartida.Numeros,
                                         Marcas = tPartida.Marcas,
                                         Modelo = tPartida.Modelo,
                                         Disponible = tInv.Id != null ? false : true,
                                         IdUbicacionAlmacen = tUbica.Id,
                                         IdZonaAlmacenaje = (int)(tUbica.IdZonaAlmacenaje != null ? tUbica.IdZonaAlmacenaje : 0),
                                     }).OrderBy(x => x.Clave).ToList();

        if (_listMonitorUbicacion != null)
        {

            resultBase.Data = _listMonitorUbicacion;

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontraron coincidencias con la búsqueda";
        }

        return resultBase;
    }
}
