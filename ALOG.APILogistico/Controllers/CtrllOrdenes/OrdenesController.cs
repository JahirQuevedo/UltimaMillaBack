//using ApiVacios.Controllers.Catalogos;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using ALOG.Repositorios.Utilerias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace ApiVacios.Controllers.CtrllOrdenes
{
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    //[AllowAnonymous]
    [Route("operativo/[controller]")]
    [ApiController]
    public class OrdenesController : GenericoController<Ordenes>
    {
        private readonly IGenericoRepositorio<Ordenes> _ctRepoGen;
        private readonly IOrdenesRepositorio _ctRepoOrdenes;
        private RespuestaGenericaDTO respuestaGenericaDTO = new RespuestaGenericaDTO();
        private UtileriasRespuestas objUtileriasRespuestas = new UtileriasRespuestas();
        private readonly ApplicationDbContext _context;
        //private readonly IVaciosRepositorio _ctRepovacios;
        //private readonly IOrdenesRepositorio _ctRepoOrdenes;

        public OrdenesController(IOrdenesRepositorio ctRepoOrdenes, ApplicationDbContext context, IGenericoRepositorio<Ordenes> ctRepoGen) : base(ctRepoGen)
        {
            //_ctRepoGen = ctRepoGen;
            _ctRepoOrdenes = ctRepoOrdenes;
            _ctRepoGen = ctRepoGen;
            _context = context;
            #region Variables
            respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            #endregion Variables
        }

        // ObtenerOrdenes
        [HttpGet("obtenerOrden/{idOrden:int}")]
        public async Task<IActionResult> obtenerOrden(int idOrden)
        {
            var catOrden = await _ctRepoOrdenes.GetOrden(idOrden);


            return Ok(catOrden);
        }


        [HttpPost("crearOrden")]
        public async Task<IActionResult> crearOrden([FromBody] SolOrdenDTO pOrden)
        {
            respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            Ordenes objRespuesta = null;
            if (ModelState.IsValid && pOrden != null)
            {

                Ordenes objOrden = new Ordenes
                {
                    IdCatCliente = pOrden.IdCatCliente,
                    IdCatSistema = pOrden.IdCatSistema ?? null,
                    IdCatLineaNegocio = pOrden.IdCatLineaNegocio,
                    IdCatEmpresa = pOrden.IdCatEmpresa,
                    IdCatSucursal = pOrden.IdCatSucursal,
                    IdCatAduana = pOrden.IdCatAduana,
                    IdCatProveedor = pOrden.IdCatProveedor ?? null,
                    IdUsuario = pOrden.IdUsuario ?? null,
                    IdEstadoOrden = 5

                };


                objRespuesta = await _ctRepoOrdenes.CrearOrden(objOrden);

                if (objRespuesta != null)
                {
                    respuestaGenericaDTO.IsSuccess = true;
                    respuestaGenericaDTO.strMensaje = "Orden creada correctamente";
                    respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
                    respuestaGenericaDTO.Entidad = objRespuesta;
                    return Ok(respuestaGenericaDTO);
                }
                else
                {

                    respuestaGenericaDTO.strMensaje = "No se pudo crear la orden";
                    respuestaGenericaDTO.Entidad = null;
                    return NotFound(respuestaGenericaDTO);
                }
            }

            respuestaGenericaDTO.strMensaje = ModelState.ToString();
            return BadRequest(respuestaGenericaDTO);
        }
        // ObtenerOrdenes
        [HttpGet("obtenerOrdenes/")]
        public async Task<IActionResult> obtenerOrdenes(FiltroOrdenesReferenciasDTO pFiltro)
        {
            var catOrden = await _ctRepoOrdenes.GetOrdenes(pFiltro);


            return Ok(catOrden);
        }

        [HttpGet("cambiarEstadoOrden/")]
        public async Task<IActionResult> cambiarEstado(SolCambioEstadoDTO pSolCambioEstadoDTO)
        {

            if (pSolCambioEstadoDTO.IdOrden <= 0 || pSolCambioEstadoDTO.IdCatReferenciaEstado <= 0)
            {
                respuestaGenericaDTO.IsSuccess = false;
                respuestaGenericaDTO.strMensaje = "Faltan datos";
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
                respuestaGenericaDTO.Entidad = null;
                return BadRequest(respuestaGenericaDTO);
            }
            var existeEstado = _context.catTipoEstados.Any(x => x.IdCatTipoEstados == pSolCambioEstadoDTO.IdCatReferenciaEstado);
            if (!existeEstado)
            {
                respuestaGenericaDTO.IsSuccess = false;
                respuestaGenericaDTO.strMensaje = "No existe el tipo de estado";
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
                respuestaGenericaDTO.Entidad = null;
                return BadRequest(respuestaGenericaDTO);
            }
            var catOrden = await _ctRepoOrdenes.GetOrden(pSolCambioEstadoDTO.IdOrden);
            catOrden.IdEstadoOrden = pSolCambioEstadoDTO.IdCatReferenciaEstado;
            _ctRepoOrdenes.ActualizarGenerico(catOrden.IdOrden, catOrden);

            respuestaGenericaDTO.IsSuccess = true;
            respuestaGenericaDTO.strMensaje = "";
            respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;
            respuestaGenericaDTO.Entidad = catOrden;

            return Ok(respuestaGenericaDTO);
        }


        [HttpPost("obtenerPath/")]
        public async Task<IActionResult> obtenerPath(SolPathOrdenDTO pSolPathOrdenDTO)
        {
            if (!ModelState.IsValid)
            {
                respuestaGenericaDTO.IsSuccess = false;
                respuestaGenericaDTO.strMensaje = "Faltan datos";
                return BadRequest(respuestaGenericaDTO);
            }
            var objPath = await _ctRepoOrdenes.obtenerPathOrden(pSolPathOrdenDTO);

            if (objPath == null)
            {
                respuestaGenericaDTO.IsSuccess = false;
                respuestaGenericaDTO.strMensaje = "No se pudo obtener el path";
                respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(respuestaGenericaDTO);
            }
            respuestaGenericaDTO.IsSuccess = true;
            respuestaGenericaDTO.strMensaje = objPath;
            respuestaGenericaDTO.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(respuestaGenericaDTO);


        }
    }
}
