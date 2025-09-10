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
    public class CatRolesController : GenericoController<CatRoles>
    {
        private readonly IGenericoRepositorio<CatRoles> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatRolesController(ApplicationDbContext db, IGenericoRepositorio<CatRoles> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }

        [Authorize(Roles = "ADMIN,ADMINUSER,CLIENTE")]
        [HttpPost("RolesUsuarios")]
        public async Task<IActionResult> RolesUsuarios([FromBody] Dictionary<string, object> filtros)
        {
            // Implementar lógica de filtrado aquí usando los parámetros recibidos en `filtros`
            // Ejemplo: filtros["RazonSocial"], filtros["Activo"]
            // Tendrías que aplicar linq dinámico para manejar los filtros.
            var intIdUsuario = int.Parse(filtros["IdUsuario"].ToString());
            var blActivo = bool.Parse(filtros["Activo"].ToString());
            // TODO: Implementar lógica de filtrado.
            return Ok();
        }
    }
}
