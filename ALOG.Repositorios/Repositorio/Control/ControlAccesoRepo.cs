using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Control.IControl;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ALOG.Repositorios.Repositorio.Control
{
    public class ControlAccesoRepo : IControlAccesoRepo
    {
        private readonly ApplicationDbContext _context;
        public ControlAccesoRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CatRolesPermisos> ObtenerCatRolesPermisos(int pIdCatRoles, bool pActivo)
        {
            var obj = await _context.catRolesPermisos
                .Include(cr => cr.CatRoles)
                .Include(cp => cp.CatPermisos)
                .Where(x => x.IdCatRoles == pIdCatRoles && x.Activo == pActivo).FirstOrDefaultAsync();
            return obj;
        }

        public async Task<CatUsuarios> ObtenerCatUsuarios(int pIdCatUsuario, bool pActivo)
        {
            var obj = await _context.catUsuarios.Include(up => up.catUsuariosPermisos).ThenInclude(up1 => up1.CatPermisos)
                .Include(up => up.catUsuarioRoles).ThenInclude(up1 => up1.CatRoles)
                .Include(up => up.catUsuariosAduanas).ThenInclude(up1 => up1.catAduana)
                .Include(up => up.catUsuariosEmpresas).ThenInclude(up1 => up1.catEmpresas)
                .Include(up => up.catUsuariosClientes).ThenInclude(up1 => up1.catClientes)
                .Include(up => up.catTipoPuesto)
                .Where(x => x.IdCatUsuarios == pIdCatUsuario && x.Activo == pActivo).FirstOrDefaultAsync();
            return obj;
        }

        public async Task<CatUsuariosPermisos> ObtenerCatUsuariosPermisos(int pIdCatUsuario, bool pActivo)
        {
            var obj = await _context.catUsuariosPermisos
                .Include(cp => cp.CatPermisos)
                .Where(x => x.IdCatUsuarios == pIdCatUsuario && x.Activo == pActivo).FirstOrDefaultAsync();
            return obj;
        }

        public async Task<CatUsuarioRoles> ObtenerCatUsuariosRoles(int pIdCatUsuario, bool pActivo)
        {
            var obj = await _context.catUsuarioRoles
             .Include(cr => cr.CatRoles)
            .Where(x => x.IdCatUsuarios == pIdCatUsuario && x.Activo == pActivo).FirstOrDefaultAsync();
            return obj;
        }
    }
}
