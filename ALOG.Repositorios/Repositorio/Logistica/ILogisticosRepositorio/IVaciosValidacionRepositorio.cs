using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;

namespace ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio
{
    public interface IVaciosValidacionRepositorio
    {
        bool validaReferencia(PeticionesReferencias peticionesReferencias, out List<string> pError);
        bool validaOrdenEnProceso(Ordenes pIdOrden, out List<string> pError);

        bool validaReferenciaClienteExistente(string pReferenciaCliente, out string pError);
        bool validaReferenciaClienteActiva(string pReferenciaCliente, out string pError);

        bool validaServicioExistente(int pIdServicio, PeticionesContenedores pContenedor, PeticionesReferencias pReferencia, out string pError);
        bool validaContenedorExistente(PeticionesContenedores pContenedor, PeticionesReferencias pReferencia, out List<string> pError);
        bool validaReferenciaExistente(string pReferenciaCliente, out string pError);
        bool validaTicketExistente(int pIdTicket, out string pError);
        bool validaOrdenExistente(int pIdOrden);
        bool validaServicioActivo(int pIdServicio);
        bool validaContenedorActivo(string pContenedor);
        bool validaReferenciaActivo(int pIdReferencia);

        string validaServicioEstado();
        string validaContenedorEstado();
        string validaReferenciaEstado();

        CatClientes obtenerClienteRFC(string pRFC, List<string> pError);
        CatPatios obtenerPatioNombre(string pNombre, List<string> pError);

        CatNavieras obtenerNavieraRFC(string pRFC, List<string> pError);
        CatTransportistas obtenerTransporteRFC(string pRFC, List<string> pError);

        CatClientes obtenerConsignadoRFC(string pRFC, List<string> pError);
        CatAduana obtenerAduanaClave(string pClaveAduana, List<string> pError);










    }
}
