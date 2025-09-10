using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica
{
    public interface IDtUltimaMillaEncRepositorio : IGenericoRepositorio<DtUltimaMillaEnc>
    {
        Task<DtUltimaMillaEnc> obtenerUltimaMillaEnc(int IdUltimaMilla);
        Task<ICollection<DtUltimaMillaEnc>> obtenerUltimaMillaEncs(FiltroDtUltimaMillaDTO pFiltro);
        Task<DtUltimaMillaEnc> Actualizar(DtUltimaMillaEnc entity);
        Task<bool> CambioEstado(int Id, int idCatTipoEstado);

        Task<RespuestaGenericaDTO> CrearReferenciaALO(DtUltimaMillaEnc encabezado);
    }

}
