using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica
{
    public interface IDtAcarreoRepositorio : IGenericoRepositorio<DtAcarreos>
    {
        Task<ICollection<DtAcarreos>> obtenerAcarreos(FiltroDtAcarreosDTO pFiltro);

        DtAcarreos obtenerAcarreo(int IdAcarreo);

        Task<bool> CambioEstado(int Id, int idCatTipoEstado);
    }
}
