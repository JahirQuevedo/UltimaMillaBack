using ApiVacios.Data;
using ApiModelos.Modelos.DTLogistico;
using ApiModelos.Modelos.DTO.Consultas;
using ApiModelos.Modelos.Integracion1G;
using ApiVacios.Repositorio.Integración1G.IIntegracion1G;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiVacios.Controllers.CtrllIntegrar1G
{
    [AllowAnonymous]
    [Route("integrar1g/[controller]")]
    [ApiController]
    
    public class Integrar1GController : Controller
    {
        private readonly ILogger<Integrar1GController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        private readonly IIntegraAnticiposSolRepo _ctintegraAnticiposSol;
        private readonly IIntegraReferenciaRepo _ctintegraReferencia;
        private readonly IIntegraFacturaEncRepo _ctintegraFacturaEnc;
        private readonly IIntegraFacturaDetRepo _ctintegraFacturaDet;
        private readonly IIntegraFacturaEstRepo _ctintegraFacturaEst;
        public Integrar1GController(IIntegraFacturaEncRepo ctintegraFacturaEncRepo, IIntegraFacturaDetRepo ctintegraFacturaDetRepo, IIntegraFacturaEstRepo ctintegraFacturaEstRepo, IIntegraReferenciaRepo ctintegraReferencia, IIntegraAnticiposSolRepo ctintegraAnticiposSol, ILogger<Integrar1GController> logger, IConfiguration configuration, ApplicationDbContext db)
        {
            _ctintegraFacturaEnc = ctintegraFacturaEncRepo;
            _ctintegraFacturaDet = ctintegraFacturaDetRepo;
            _ctintegraFacturaEst = ctintegraFacturaEstRepo;
            _ctintegraReferencia = ctintegraReferencia;
            _ctintegraAnticiposSol = ctintegraAnticiposSol;
            _logger = logger;
            _configuration = configuration;
            _db = db;
        }

        #region IntegrarAnticiposSol

        // Función para agregar entidad: Modelo + Crear
        [HttpPost("IntAticiposSolCrear")]
        public async Task<IActionResult> IntAticiposSolCrear([FromBody] IntegraAnticipoSol entidad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (entidad == null)
                {
                    return BadRequest("La entidad no puede ser nula");
                }

                _ctintegraAnticiposSol.AgregarAsync(entidad);
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Función para guardar cambios: Modelo + Guardar
        [HttpPut("IntAticiposSolActualizar/{id}")]
        public async Task<IActionResult> IntAticiposSolActualizar(int id, [FromBody] IntegraAnticipoSol entidad)
        {
            var entidadExistente = await _ctintegraAnticiposSol.obtenerPorIdAsync(id);

            if (entidadExistente == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            await _ctintegraAnticiposSol.ActualizarAsync(id, entidad);
            return Ok($"La entidad {entidad.IdIntAnticipoSol} fue actualizada exitosamente.");
        }



        // Función para obtener todas las entidades: Modelo + Listar
        [HttpGet("IntAticiposSolListar")]
        public async Task<IActionResult> IntAticiposSolListar()
        {
            var lista = await _ctintegraAnticiposSol.obtenerListaTodosAsync();
            return Ok(lista);
        }

        // Función para obtener entidad por ID: Modelo + Obtener
        [HttpGet("IntAticiposSolObtener/{id}")]
        public async Task<IActionResult> IntAticiposSolObtener(int id)
        {
            var entidad = await _ctintegraAnticiposSol.obtenerPorIdAsync(id);

            if (entidad == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            return Ok(entidad);
        }

        // Función para activar la entidad (Alta)
        [HttpPatch("IntAticiposSolAlta/{id}")]
        public async Task<IActionResult> IntAticiposSolAlta(int id)
        {
            bool resultado = await _ctintegraAnticiposSol.Alta(id, x => x.GetType().GetProperty("Activo"), true);

            if (resultado)
            {
                return Ok($"La entidad {id} fue activada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo activar la entidad.");
            }
        }

        // Función para desactivar la entidad (Baja)
        [HttpPatch("IntAticiposSolBaja/{id}")]
        public async Task<IActionResult> IntAticiposSolBaja(int id)
        {
            bool resultado = await _ctintegraAnticiposSol.Baja(id, x => x.GetType().GetProperty("Activo"), false);

            if (resultado)
            {
                return Ok($"La entidad {id} fue desactivada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo desactivar la entidad.");
            }
        }

        #endregion IntegrarAnticiposSol

        #region IntegrarReferencia
        // Función para agregar entidad: Modelo + Crear
        [HttpPost("integraReferenciaCrear")]
        public async Task<IActionResult> integraReferenciaCrear([FromBody] IntegraReferencia entidad)
        {
            try
            {
                if (entidad == null)
                {
                    return BadRequest("La entidad no puede ser nula");
                }

                _ctintegraReferencia.AgregarAsync(entidad);
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Función para guardar cambios: Modelo + Guardar
        [HttpPut("integraReferenciaActualizar/{id}")]
        public async Task<IActionResult> integraReferenciaActualizar(int id, [FromBody] IntegraReferencia entidad)
        {
            var entidadExistente = await _ctintegraReferencia.obtenerPorIdAsync(id);

            if (entidadExistente == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            await _ctintegraReferencia.ActualizarAsync(id,entidad);
            return Ok($"La entidad {entidad.IdIntReferencia} fue actualizada exitosamente.");
        }



        // Función para obtener todas las entidades: Modelo + Listar
        [HttpGet("integraReferenciaListar")]
        public async Task<IActionResult> integraReferenciaListar()
        {
            var lista = await _ctintegraReferencia.obtenerListaTodosAsync();
            return Ok(lista);
        }

        // Función para obtener todas las entidades: Modelo + Listar
        [HttpGet("integraReferenciaFiltro")]
        public async Task<IActionResult> integraReferenciaFiltro([FromBody] FiltroIntegraReferencias1GDTO pFiltro)
        {
            var lista = await _ctintegraReferencia.obtenerReferencias1G(pFiltro);
            return Ok(lista);
        }

        // Función para obtener entidad por ID: Modelo + Obtener
        [HttpGet("integraReferenciaObtener/{id}")]
        public async Task<IActionResult> integraReferenciaObtener(int id)
        {
            var entidad = await _ctintegraReferencia.obtenerPorIdAsync(id);

            if (entidad == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            return Ok(entidad);
        }

        // Función para activar la entidad (Alta)
        [HttpPatch("integraReferenciaAlta/{id}")]
        public async Task<IActionResult> integraReferenciaAlta(int id)
        {
            bool resultado = await _ctintegraReferencia.Alta(id, x => x.GetType().GetProperty("Activo"), true);

            if (resultado)
            {
                return Ok($"La entidad {id} fue activada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo activar la entidad.");
            }
        }

        // Función para desactivar la entidad (Baja)
        [HttpPatch("integraReferenciaBaja/{id}")]
        public async Task<IActionResult> integraReferenciaBaja(int id)
        {
            bool resultado = await _ctintegraReferencia.Baja(id, x => x.GetType().GetProperty("Activo"), false);

            if (resultado)
            {
                return Ok($"La entidad {id} fue desactivada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo desactivar la entidad.");
            }
        }

        #endregion IntegrarReferencia

    }
}
