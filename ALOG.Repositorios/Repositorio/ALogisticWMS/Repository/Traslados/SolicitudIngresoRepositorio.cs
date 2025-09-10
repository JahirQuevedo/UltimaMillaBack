using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class SolicitudIngresoRepositorio : GenericoRepositorio<ControlTransporte>, ISolicitudIngresoRepositorio
{

    private readonly ApplicationDbContext _db;

    public SolicitudIngresoRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<MonitorSolicitudIngreso> Guardar(MonitorSolicitudIngreso monitorSolicitudIngreso)
    {
        throw new NotImplementedException();
    }

    public PaginadoResult<MonitorSolicitudIngreso> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaSolicitudIngreso> entidad)
    {
        throw new NotImplementedException();
    }

    public ResultBase<MonitorSolicitudIngreso> ObtenerPorId(int idSolicitudIngreso)
    {
        throw new NotImplementedException();
    }
}
