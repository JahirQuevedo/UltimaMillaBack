using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiVacios.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatUsuariosPermisosController : GenericoController<CatUsuariosPermisos>
    {
        private readonly IGenericoRepositorio<CatUsuariosPermisos> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatUsuariosPermisosController(ApplicationDbContext db, IGenericoRepositorio<CatUsuariosPermisos> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE")]
        [HttpPost("PermisosUsuario")]
        public async Task<IActionResult> PermisosUsuario([FromBody] Dictionary<string, object> filtros)
        {
            // Implementar lógica de filtrado aquí usando los parámetros recibidos en `filtros`
            // Ejemplo: filtros["RazonSocial"], filtros["Activo"]
            // Tendrías que aplicar linq dinámico para manejar los filtros.

            // TODO: Implementar lógica de filtrado.
            return Ok();
        }

    }
}
