using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatUsuarioAduanaRepositorio : IGenericoRepositorio<CatUsuariosAduanas>
    {

        CatUsuariosAduanas existeUsuarioAduana(int IdCatAduana, int IdCatUsuario);
        ICollection<CatUsuariosAduanas> ObtenerUsuariosPorAduana(int IdCatAduana);

        ICollection<CatUsuariosAduanas> ObtenerAduanasPorUsuario(int IdCatUsuario);
    }
}
