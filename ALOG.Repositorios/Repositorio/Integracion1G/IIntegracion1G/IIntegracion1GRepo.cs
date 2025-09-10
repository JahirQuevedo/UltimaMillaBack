
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Orden;

namespace ALOG.Repositorios.Repositorio.Integracion1G.IIntegracion1G {
    public interface IIntegracion1GRepo {

        public Task<RespuestaGenericaDTO> GenerarFacturacionDesdeOrdenesAsync(Ordenes orden);

    }
}
