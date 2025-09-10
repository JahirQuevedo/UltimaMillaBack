using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;

namespace ALOG.Repositorios.Repositorio.Control.IControl
{
    public interface IControlAccesoRepo
    {
        Task<CatRolesPermisos> ObtenerCatRolesPermisos(int pIdCatRoles, bool pActivo);


        Task<CatUsuarios> ObtenerCatUsuarios(int pIdCatUsuario, bool pActivo);


        Task<CatUsuariosPermisos> ObtenerCatUsuariosPermisos(int pIdCatUsuario, bool pActivo);


        Task<CatUsuarioRoles> ObtenerCatUsuariosRoles(int pIdCatUsuario, bool pActivo);
    }
}
