using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio
{
    public interface IPeticionesContenedoresRepo
    {
        bool contenedorActivo(int IdContenedor);
        bool contenedorActivo(string sContenedor);

        Task<PeticionesContenedores> obtenerContenedor(int pIdContenedor);

        Task<ICollection<PeticionesContenedores>> obtenerContenedores(FiltroOrdenesReferenciasDTO pFiltro);

        Task<bool> ActualizarPatioContenedor(PeticionesContenedores pContenedor);
        //Task<bool> actualizarNavieraContenedor(PeticionesContenedores pContenedor);
        Task<RespuestaGenericaDTO> actualizarNavieraContenedor(PeticionesContenedores pContenedor);

        Task<bool> actualizarTransportistaContenedor(PeticionesContenedores pContenedor);

        Task<bool> actualizarContenedor(PeticionesContenedores pContenedor);
        Task<RespuestaGenericaDTO> AsignarPatioContenedor(PeticionesContenedores pContenedor);




    }
}
