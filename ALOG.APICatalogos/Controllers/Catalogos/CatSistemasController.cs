using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Control;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Utilerias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiVacios.Controllers.Catalogos
{
    [Authorize(Roles = "ADMIN,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class CatSistemasController : GenericoController<CatSistemas>
    {
        private readonly IGenericoRepositorio<CatSistemas> _repositorio;
        private readonly ApplicationDbContext _db;
        private string strSalt = "";
        UtileriasCifrados clsCifrados = new UtileriasCifrados();
        string strError = "";
        public CatSistemasController(ApplicationDbContext db, IGenericoRepositorio<CatSistemas> repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
            _db = db;
        }


        //[AllowAnonymous]
        [HttpPost("CrearusuarioSistema/")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CrearUsuarioSistema([FromBody] SistemaRegistroDTO psistemaRegistroDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("No se recibieron datos de entrada");
            if (!clsCifrados.ValidarContraseña(psistemaRegistroDTO.Password))
                return BadRequest("La contraseña no cumple con los requisitos, debe incluir al menos 10 caracteres, Mayúsculas, minúsculas y números");

            CatSistemas objCatSistemas = new CatSistemas();
            objCatSistemas.nombre = psistemaRegistroDTO.nombre;
            objCatSistemas.userSistema = psistemaRegistroDTO.Usuario;
            strSalt = "";
            objCatSistemas.passSistema = clsCifrados.ComputeSha256HashWithSalt(psistemaRegistroDTO.Password, out strSalt);
            objCatSistemas.salt = strSalt;
            objCatSistemas.IdCatCliente = psistemaRegistroDTO.IdCatCliente;
            objCatSistemas.Activo = true;
            objCatSistemas.rol = psistemaRegistroDTO.rol;

            _db.catSistemas.Add(objCatSistemas);
            _db.SaveChanges();

            return Ok(true);
        }




    }
}
