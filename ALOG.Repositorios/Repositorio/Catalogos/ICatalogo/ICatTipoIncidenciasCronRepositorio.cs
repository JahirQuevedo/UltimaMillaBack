using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatTipoIncidenciasCronRepositorio
    {
        Task<List<CatTipoIncidenciaEvento>> obtenerCatTipoIncidenciaEvento();

    }
}
