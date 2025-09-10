using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatUsuariosRolesRepositorio : IGenericoRepositorio<CatUsuarioRoles>
    {


        ICollection<CatUsuarioRoles> ObtenerRolesPorUsuario(int IdCatUsuario);

        ICollection<CatUsuarios> ObtenerUsuariosPorRol(int IdRoles);
    }
}
