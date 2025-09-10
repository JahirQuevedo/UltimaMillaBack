//using ApiVacios.Controllers.Catalogos;
using ApiVacios.Data;
using ApiModelos.Modelos.DTLogistico;
using ApiModelos.Modelos.DTO.Consultas;
using ApiModelos.Modelos.DTO.Vacios;
using ApiModelos.Modelos.Orden;
using ApiModelos.Modelos.Vacios;
using ApiVacios.Repositorio.DtLogistica.IDtLogistica;
using ApiVacios.Repositorio.Generico.IGenerico;
using ApiVacios.Repositorio.IRepoOrdenes;
using ApiVacios.Repositorio.Logistica.ILogisticosRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiVacios.Controllers;

namespace ApiVacios.Controllers.CtrlDTLogistico
{
    [AllowAnonymous]
    [Route("DTLogistica/[controller]")]
    [ApiController]
    public class DtAcarreosController : GenericoController<DtAcarreos>
    {
        private readonly IGenericoRepositorio<DtAcarreos> _ctRepoGen;
        private readonly IDtAcarreoRepositorio _ctRepoAcarreos;
        //private readonly IOrdenesRepositorio _ctRepoOrdenes;
        private readonly ApplicationDbContext _db;
        public DtAcarreosController(ApplicationDbContext db, IGenericoRepositorio<DtAcarreos> ctRepoGen, IDtAcarreoRepositorio ctRepoAcarreos) : base(ctRepoGen)
        {
            _ctRepoGen = ctRepoGen;
            _db = db;
            _ctRepoAcarreos = ctRepoAcarreos;


        }

        
        [HttpPost("FiltraAcarreos/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FiltrarReferencias([FromBody] FiltroDtAcarreosDTO pFiltro)
        {
            ICollection<DtAcarreos> objList = null;
            objList = _ctRepoAcarreos.obtenerAcarreos(pFiltro);

            return Ok(objList);
        }


        [HttpGet("ObtieneAcarreos/{IdAcarreo:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAcarreos(FiltroDtAcarreosDTO pFiltro)
        {
            #region Variables
            var lstServiciosDto = new List<PeticionesReferenciasDTO>();
            #endregion Variables

            var lstServicios = _ctRepoAcarreos.obtenerAcarreos(pFiltro);

            //// Extraer el claim de nombre de usuario del token JWT (usualmente "sub" o "name")
            //var usuarioId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            //// Si el nombre está en otro claim, puedes cambiar "sub" por el tipo correcto
            //if (string.IsNullOrEmpty(usuarioId))
            //{
            //    usuarioId = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            //}

            //if (!string.IsNullOrEmpty(usuarioId))
            //{
            //    return Ok(new { Usuario = usuarioId });
            //}
            //else
            //{
            //    return BadRequest("No se pudo identificar al usuario.");
            //}
            if (lstServicios != null)
            {

                //foreach (var lista in lstServicios)
                //{
                //lstServiciosDto.Add(_mapper.Map<PeticionesReferenciasDTO>(lstServicios)); //se utiliza mapper para 
                return Ok(lstServicios);                                                                //}
            }
            else
                return StatusCode(StatusCodes.Status404NotFound);

        }
    }
}
