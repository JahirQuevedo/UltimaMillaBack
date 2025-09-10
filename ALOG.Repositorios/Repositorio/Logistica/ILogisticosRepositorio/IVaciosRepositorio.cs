using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;

namespace ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio
{

    public interface IVaciosRepositorio
    {
        //Metodos que se expondran para la creación de una orden, agregar contenedor, servicio y toda la logica se dejara en el repositorio
        Task<PeticionesRespuestaDTO> CrearOrden(PeticionesReferenciasClienteExternoDTO peticionesReferenciasClienteExternoDTO);
        Task<PeticionesRespuestaDTO> AgregarContenedorAReferencia(int idReferencia, PeticionesContenedoresClienteExternoDTO peticionesContenedoresClienteExternoDTO);
        Task<PeticionesRespuestaDTO> AgregarServicioAContenedor(int idReferencia, int idContenedor, PeticionesServiciosClienteExternoDTO pServClienteExterno);
        Task<RespuestaGenericaDTO> CambiarEstadoServicio(SolCambioEstadoDTO pSolCambioEstadoDTO);
        Task<ICollection<RespObtenerReferenciasDTO>> GetReferencias(FiltroOrdenesReferenciasDTO pFiltro);
        bool ExisteReferenciaNAD(string pReferenciaAlo);
        bool ExisteContenedorRefCliente(string pRefCliente, string contenedor);
        bool ExisteContenedorOperando(string pReferenciaCliente, string pContenedor);
        bool ExisteTicket(int IdTicket);
        List<string> ActualizarFolioContenedor(SolActualizarFolioManiobraDTO contenedor);
        List<string> ActualizarContenedor(PeticionesContenedores pCont);
        bool BorrarActivarReferencia(int IdReferencia, bool status);
        bool BorrarActivarContenedor(int Idreferencia, int IdContenedor, bool status);
        bool BorrarActivarServicioContenedor(int IdreferenciaALO, int IdContenedor, int IdServicio, bool status);
        Task<List<string>> IniciarProcesoOrden(List<int> ordenes);
        Task<List<string>> CancelarOrden(List<int> ordenes);
        Task<string?> CancelarElemento(string parametrosEncriptados);
        bool CrearServicio(int IdReferencia, int IdContenedor, PeticionesServicios servicio);

        //<------------------------------------------------------------------------------------->
        ICollection<PeticionesReferencias> GetReferenciaNAD(string referenciaNAD);
        Task<ICollection<Ordenes>> GetOrdenes(FiltroOrdenesReferenciasDTO pFiltro);
        PeticionesContenedores GetContenedor(int IdReferencia, int IdContenedor);
        PeticionesServicios GetServicioContenedor(int IdReferencia, int IdContenedor, int IdServicio);
        int? GetIdAduana(int? aduana);
        Task<List<CatLineaNegocioTariPrecio>> GetTarifarioServicios(string parametrosEncriptados);
        bool Guardar(out string strError);
        Task<PeticionesReferencias> GetReferencia(int IdReferencia);
        public Task<RespuestaGenericaDTO> GenerarAnticipo(Ordenes orden);

        //bool ExisteReferenciaActiva(int pReferenciaAlo);
        //bool ExisteContenedor(int pIdReferencia, string pContenedor);
        //bool ExisteContenedorId(int pIdReferencia, int pIdcontenedor);
        //bool ExisteContenedorActivo(string contenedor);
        //bool ExisteServicioContenedor(int IdreferenciaALO, int IdContenedor, int IdServicio);
        //bool CrearReferencia(PeticionesReferencias referencia, out List<string> strError);
        //List<string> ValidaPeticionReferenciaNad(PeticionesReferencias peticionesReferencias);
        //List<string> ValidaServicioContenedor(PeticionesServicios peticionesServicios, int IdReferencia);
        //List<string> CrearContenedor(int IdReferencia, PeticionesContenedores contenedor);
        //PeticionesReferencias ValidaOrdenServicios(PeticionesReferenciasClienteExternoDTO peticionesReferenciasClienteExterno, out List<string> lstStrErrores);
        //List<string> ValidarOrdenContenedorClienteExterno(PeticionesContenedoresClienteExternoDTO pCont, PeticionesContenedores objContenedor);
        //PeticionesServicios ValidarOrdenServiciosClienteExterno(PeticionesContenedores objContenedor, PeticionesServiciosClienteExternoDTO servicio, out List<string> lstStrErrores);
        //Task<List<string>> CrearDocumentos(PeticionesReferencias pRef, PeticionesReferenciasClienteExternoDTO pRefExterno);
        //bool ActualizarReferencia(PeticionesReferencias referencia);
        //Task<RespuestaGenericaDTO> ValidarSolicitudAsync(SolTicketDTO solTicketDTO);
        //Task<RespuestaGenericaDTO> GuardarSolicitudAsync(SolTicketDTO solTicketDTO);
        //bool CargaInicial();
        //CatAduana GetAduana(int IdCatAduana);
        //Ordenes GetOrdenPorIdReferencia(int IdReferencia);
    }
}
