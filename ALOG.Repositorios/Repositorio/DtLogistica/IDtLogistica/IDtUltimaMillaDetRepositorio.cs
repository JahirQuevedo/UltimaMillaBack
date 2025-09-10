using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica
{
    public interface IDtUltimaMillaDetRepositorio : IGenericoRepositorio<DtUltimaMillaDet>
    {
        ICollection<DtUltimaMillaDet> obtenerUltimaMillaDets(FiltroDtUltimaMillaDTO pFiltro);
        DtUltimaMillaDet obtenerUltimaMillaDet(int IdUltimaMilla);

        Task<ICollection<DtUltimaMillaDet>> ObtenerDetalles(int idUltimaMillaEncabezado);
    }

}
