using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IReferenciaBookingBLRepositorio
{

    ResultBase<MonitorReferenciaBookingBL> ObtenerPorId(int id);

    ResultBase<MonitorReferenciaBookingBL> Guardar(MonitorReferenciaBookingBL paquete);

    ResultBase Eliminar(int IdReferenciaBookingBl);



}
