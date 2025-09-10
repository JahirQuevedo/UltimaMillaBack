using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G
{
    public interface IIntegraReferenciaRepo : IGenericoRepositorio<IntegraReferencia>
    {
        Task<ICollection<IntegraReferencia>> obtenerReferencias1G(FiltroIntegraReferencias1GDTO pFiltro);

        IntegraReferencia obtenerreferencias1GPorId(int IdIntegraReferencia);

        Task<RespuestaGenericaDTO> CrearReferenciasFacturacion(List<int> ordenes);
        public Task<RespuestaGenericaDTO> GenerarFacturacionDesdeOrdenesAsync(Ordenes orden);
    }
}
