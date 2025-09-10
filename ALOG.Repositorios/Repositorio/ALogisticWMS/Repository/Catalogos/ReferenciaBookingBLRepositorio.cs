using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios;

public class ReferenciaBookingBLRepositorio : GenericoRepositorio<ReferenciaBookingBl>, IReferenciaBookingBLRepositorio
{

    private readonly ApplicationDbContext _db;

    public ReferenciaBookingBLRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase Eliminar(int IdReferenciaBookingBl)
    {
        var resultBase = new ResultBase();

        var _referenciaBookingBl = _db.ReferenciaBookingBls.Where(x => x.Id == IdReferenciaBookingBl).SingleOrDefault();

        if (_referenciaBookingBl != null)
        {

            _db.ReferenciaBookingBls.Remove(_referenciaBookingBl);
            _db.SaveChanges();

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontró información del registro solicitado.";
        }

        return resultBase;
    }

    public ResultBase<MonitorReferenciaBookingBL> Guardar(MonitorReferenciaBookingBL monitorReferenciaBookingBL)
    {

        var resultBase = new ResultBase<MonitorReferenciaBookingBL>();

        ReferenciaBookingBl _referenciaBookingBl = new ReferenciaBookingBl();

        if (monitorReferenciaBookingBL.CRUDAction.Equals(ECRUDAction.Update))
        {
            _referenciaBookingBl = _db.ReferenciaBookingBls.Where(x => x.Id.Equals(monitorReferenciaBookingBL.IdReferenciaBookingBl)).SingleOrDefault();

        }

        _referenciaBookingBl.BookingBl = monitorReferenciaBookingBL.BookingBl;
        _referenciaBookingBl.IdReferencia = monitorReferenciaBookingBL.IdReferencia;

        if (monitorReferenciaBookingBL.CRUDAction.Equals(ECRUDAction.Create))
        {
            _referenciaBookingBl.FechaAlta = DateTime.Now;
            _db.ReferenciaBookingBls.Add(_referenciaBookingBl);
            _db.SaveChanges();
        }
        else
        {
            _referenciaBookingBl.Id = monitorReferenciaBookingBL.IdReferenciaBookingBl;
            _db.ReferenciaBookingBls.Update(_referenciaBookingBl);
            _db.SaveChanges();
        }

        monitorReferenciaBookingBL.IdReferenciaBookingBl = _referenciaBookingBl.Id;

        resultBase.Id = _referenciaBookingBl.Id;
        resultBase.Data = monitorReferenciaBookingBL;

        return resultBase;
    }

    public ResultBase<MonitorReferenciaBookingBL> ObtenerPorId(int idReferenciaBookingBL)
    {
        var resultBase = new ResultBase<MonitorReferenciaBookingBL>();

        var _referenciaBookingBl = (from p in _db.ReferenciaBookingBls
                                    where p.Id.Equals(idReferenciaBookingBL)
                                    select new MonitorReferenciaBookingBL
                                    {
                                        IdReferenciaBookingBl = p.Id,
                                        IdReferencia = (int)(p.IdReferencia != null ? p.IdReferencia : 0),
                                        BookingBl = p.BookingBl,
                                    }).FirstOrDefault();

        if (_referenciaBookingBl != null)
        {

            resultBase.Data = _referenciaBookingBl;

        }
        else
        {

            resultBase.MensajeRespuesta = "No se encontró la Referencia Booking BL.";

        }


        return resultBase;
    }
}
