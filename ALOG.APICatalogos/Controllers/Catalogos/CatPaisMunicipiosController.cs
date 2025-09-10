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
    public class CatPaisMunicipiosController : GenericoController<CatPaisMunicipios>
    {
        private readonly IGenericoRepositorio<CatPaisMunicipios> _repositorio;
        private readonly ApplicationDbContext _db;

        public CatPaisMunicipiosController(ApplicationDbContext db, IGenericoRepositorio<CatPaisMunicipios> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }
    }
}
