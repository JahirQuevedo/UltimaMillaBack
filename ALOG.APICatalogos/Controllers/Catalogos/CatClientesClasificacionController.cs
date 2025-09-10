using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiVacios.Controllers.Catalogos
{
    [Authorize]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatClientesClasificacionController : GenericoController<CatClientesClasificacion>
    {
        private readonly IGenericoRepositorio<CatNavieras> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatClientesClasificacionController(ApplicationDbContext db, IGenericoRepositorio<CatClientesClasificacion> repositorio) : base(repositorio)
        {
            _db = db;
        }

    }
}
