using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class ZonaAlmacenajeRepositorio : GenericoRepositorio<Tarja>, IZonaAlmacenRepositorio
{


    private readonly ApplicationDbContext _db;

    public ZonaAlmacenajeRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorZonaAlmacen> Guardar(MonitorZonaAlmacen monitorZonaAlmacenaje)
    {
        throw new NotImplementedException();
    }

    public ResultBase<List<MonitorZonaAlmacen>> ObtenerLista()
    {
        var resultBase = new ResultBase<List<MonitorZonaAlmacen>>();
        MonitorZonaAlmacen monitorZonaAlmacen = new MonitorZonaAlmacen();

        try
        {
            var listZonaAlmacenes = (from p in _db.ZonaAlmacenes
                                     select new MonitorZonaAlmacen
                                     {
                                         IdZonaAlmacenaje = p.Id,
                                         Clave = p.Clave,
                                         Descripcion = p.Descripcion,
                                         TotalColumnas = p.TotalColumnas,
                                         TotalFilas = p.TotalFilas
                                     }).ToList();

            if (listZonaAlmacenes != null)
            {

                resultBase.Data = listZonaAlmacenes;

            }
            else
            {

                resultBase.MensajeRespuesta = "No se encontró información.";

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

    public ResultBase<MonitorZonaAlmacen> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }

}
