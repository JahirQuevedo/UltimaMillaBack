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
    public class CatDocumentosController : GenericoController<CatDocumentos>
    {
        private readonly IGenericoRepositorio<CatDocumentos> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatDocumentosController(ApplicationDbContext db, IGenericoRepositorio<CatDocumentos> repositorio) : base(repositorio)
        {
            _db = db;
        }
    }
}
