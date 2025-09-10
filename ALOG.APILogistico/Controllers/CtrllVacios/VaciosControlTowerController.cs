using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Peticiones;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Repositorio.SPFunciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ALOG.APILogistico.Controllers.CtrllVacios
{
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    //[AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class VaciosControlTowerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();
        private readonly IPeticionesContenedoresRepo _ctRepoContenedores;
        private readonly IVaciosRepositorio _vaciosRepositorio;
        private readonly IIntegraReferenciaRepo _integraRepositorio;
        private readonly IPeticionesContenedorCronRepo _ctRepoContenedoresCron;

        public VaciosControlTowerController(ApplicationDbContext db, IPeticionesContenedoresRepo ctRepoContenedores, IVaciosRepositorio vaciosRepositorio, IIntegraReferenciaRepo integraRepositorio, IPeticionesContenedorCronRepo ctRepoContenedoresCron)
        {
            _db = db;
            _ctRepoContenedores = ctRepoContenedores;

            //INICIALIZA RESPUESTA GENERICA
            _respuestaGenericaDTO = new RespuestaGenericaDTO();
            _respuestaGenericaDTO.IsSuccess = false;
            _respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
            _vaciosRepositorio = vaciosRepositorio;
            _integraRepositorio = integraRepositorio;
            _ctRepoContenedoresCron = ctRepoContenedoresCron;
        }


        #region OPERACION CONTENEDORES
        [HttpPost("ObtenerContenedores")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerContenedores([FromBody] FiltroOrdenesReferenciasDTO pFiltroOrdenesReferenciasDTO)
        {
            #region NOTAS
            /*
             * Autor: RAZE
             * Fecha: 2025
             * Nota: Se crea el contenedor y se asigna el IdContenedor al servicio. 
             */
            #endregion NOTAS

            #region OPERACIONES
            var lstErroresCrearContenedores = await _ctRepoContenedores.obtenerContenedores(pFiltroOrdenesReferenciasDTO);

            return Ok(lstErroresCrearContenedores);
            #endregion OPERACIONES

        }

        [HttpPost("ActualizarContenedor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarContenedor([FromBody] PeticionesContenedores pPeticionesContenedores)
        {
            #region NOTAS
            /*
             * Autor: RAZE
             * Fecha: 2025
             * Nota: Se crea el contenedor y se asigna el IdContenedor al servicio. 
             */
            #endregion NOTAS

            #region OPERACIONES
            var lstErroresCrearContenedores = await _ctRepoContenedores.actualizarContenedor(pPeticionesContenedores);
            if (!lstErroresCrearContenedores)
            {
                _respuestaGenericaDTO.IsSuccess = false;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                _respuestaGenericaDTO.strMensaje = "Error al actualizar el contenedor";
                return BadRequest(_respuestaGenericaDTO);
            }
            _respuestaGenericaDTO.IsSuccess = true;
            _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
            _respuestaGenericaDTO.strMensaje = $"Contenedor {pPeticionesContenedores.Contenedor} Actualizado correctamamente";
            return Ok(_respuestaGenericaDTO);
            #endregion OPERACIONES

        }

        [HttpPost("ActualizarPatio")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarPatioContenedor([FromBody] PeticionesContenedores pPeticionesContenedores)
        {
            #region NOTAS
            /*
             * Autor: RAZE
             * Fecha: 2025
             * Nota: . 
             */
            #endregion NOTAS

            #region OPERACIONES
            //var lstErroresCrearContenedores = await _ctRepoContenedores.ActualizarPatioContenedor(pPeticionesContenedores);
            var respuesta = await _ctRepoContenedores.AsignarPatioContenedor(pPeticionesContenedores);
                _respuestaGenericaDTO = respuesta;
                if ((respuesta.IsSuccess == false && respuesta.StatusCode != HttpStatusCode.OK) || respuesta.lstrErrorMessages.Count() > 0)
                    return BadRequest(_respuestaGenericaDTO);
                
                
            return Ok(_respuestaGenericaDTO);
            //if (!lstErroresCrearContenedores)
            //{
            //    _respuestaGenericaDTO.IsSuccess = false;
            //    _respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
            //    _respuestaGenericaDTO.strMensaje = $"Error al actualizar el contenedor {pPeticionesContenedores.Contenedor}";
            //    return BadRequest(_respuestaGenericaDTO);
            //}
            //_respuestaGenericaDTO.IsSuccess = true;
            //_respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
            //_respuestaGenericaDTO.strMensaje = $"Contenedor {pPeticionesContenedores.Contenedor} Actualizado correctamamente";
            //return Ok(_respuestaGenericaDTO);
            #endregion OPERACIONES

        }

        [HttpPost("ActualizarNaviera")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarNavieraContenedor([FromBody] PeticionesContenedores pPeticionesContenedores)
        {
            #region NOTAS
            /*
             * Autor: RAZE
             * Fecha: 2025
             * Nota: . 
             */
            #endregion NOTAS

            #region OPERACIONES
            var lstErroresCrearContenedores = await _ctRepoContenedores.actualizarNavieraContenedor(pPeticionesContenedores);
            return Ok(lstErroresCrearContenedores);
            #endregion OPERACIONES

        }
        #endregion OPERACION CONTENEDORES

        #region OPERACION CRONOLOGIA
        [HttpGet("ObtenerCronologiaPorContenedor/{pIdContenedor}/{pIdServicio}")]
        public async Task<IActionResult> ObtenerCronologiaPorContenedor(int pIdContenedor, int pIdServicio)
        {
            #region NOTAS
            /*
             * Autor: RAZE
             * Fecha: 2025
             * Nota: . 
             */
            #endregion NOTAS

            #region OPERACIONES
            var lstPeticionesContenedorCron = await _ctRepoContenedoresCron.obtenerCronologiaporContenedor(pIdContenedor, pIdServicio);
            return Ok(lstPeticionesContenedorCron);
            #endregion OPERACIONES

        }

        [HttpPost("AgregarCronologiaContenedor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarCronologiaPorContenedor([FromBody] PeticionesContenedoresCron pPeticionesContenedoresCron)
        {
            #region NOTAS
            /*
             * Autor: RAZE
             * Fecha: 2025
             * Nota: . 
             */
            #endregion NOTAS

            #region OPERACIONES
            var lstErroresCrearContenedores = await _ctRepoContenedoresCron.agregarCronologiaporContenedor(pPeticionesContenedoresCron);
            return Ok(lstErroresCrearContenedores);
            #endregion OPERACIONES

        }

        [HttpPost("BajaCronologiaporContenedor/{pIdContenedorCron}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BajaCronologiaporContenedor(int pIdContenedorCron)
        {

            #region OPERACIONES
            var lstPeticionesContenedorCron = await _ctRepoContenedoresCron.bajaCronologiaporContenedor(pIdContenedorCron);
            return Ok(lstPeticionesContenedorCron);
            #endregion OPERACIONES

        }

        [HttpPost("ActualizarCronologiaporContenedor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarCronologiaporContenedor([FromBody] PeticionesContenedoresCron pPeticionesContenedoresCron)
        {

            #region OPERACIONES
            var lstPeticionesContenedorCron = await _ctRepoContenedoresCron.actualizarCronologiaporContenedor(pPeticionesContenedoresCron);
            return Ok(lstPeticionesContenedorCron);
            #endregion OPERACIONES

        }
        #endregion OPERACION CRONOLOGIA

        #region Generación de anticipos

        [HttpPost("GenerarAnticipo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerarAnticipo(List<int> idOrdenes)
        {

            var respuestaIntegracion = await _integraRepositorio.CrearReferenciasFacturacion(idOrdenes);
            return Ok(respuestaIntegracion);

        }

        #endregion Generación de anticipos
    }
}
