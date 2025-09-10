//using ApiVacios.Controllers.Catalogos;
using ApiVacios.Data;
using ApiModelos.Modelos.DTLogistico;
using ApiVacios.Repositorio.DtLogistica.IDtLogistica;
using ApiVacios.Repositorio.Generico.IGenerico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVacios.Controllers.CtrlDTLogistico
{
    [AllowAnonymous]
    [Route("DTLogistica/[controller]")]
    [ApiController]
    public class DtUltimaMillaController : Controller
    {
        //private readonly IGenericoRepositorio<DtUltimaMillaEnc> _ctRepoGenEnc;
        //private readonly IGenericoRepositorio<DtUltimaMillaDet> _ctRepoGenDet;
        private readonly IDtUltimaMillaEncRepositorio _ctRepoUMillaEnc;
        //private readonly IDtUltimaMillaDetRepositorio _ctRepoUMillaDet;
        
        //private readonly IOrdenesRepositorio _ctRepoOrdenes;
        //private readonly ApplicationDbContext _db;
        public DtUltimaMillaController(IDtUltimaMillaEncRepositorio ctRepoUMillaEnc) 
        {


            _ctRepoUMillaEnc = ctRepoUMillaEnc;
        }


        #region UltMillaEnc

        // Función para desactivar la entidad (Baja)
        [HttpPatch("CambioEstado/{id}")]
        public async Task<IActionResult> CambioEstado(int id)
        {

            bool resultado = await _ctRepoUMillaEnc.Baja(id, x => x.GetType().GetProperty("Activo"), false);

            if (resultado)
            {
                return Ok($"La entidad {id} fue desactivada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo desactivar la entidad.");
            }
        }


        // Función para agregar entidad: Modelo + Crear
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] DtUltimaMillaEnc entidad)
        {
            try
            {
                if (entidad == null)
                {
                    return BadRequest("La entidad no puede ser nula");
                }

                _ctRepoUMillaEnc.AgregarAsync(entidad);
                
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Función para guardar cambios: Modelo + Guardar
        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] DtUltimaMillaEnc entidad)
        {
            var entidadExistente = await _ctRepoUMillaEnc.obtenerUltimaMillaEnc(entidad.IdDtUltMillaEnc);

            if (entidadExistente == null)
            {
                return NotFound($"La entidad con ID {entidad.IdDtUltMillaEnc} no fue encontrada.");
            }
           await _ctRepoUMillaEnc.Actualizar(entidad);
            return Ok($"La entidad {entidad.IdDtUltMillaEnc} fue actualizada exitosamente.");
        }



        // Función para obtener todas las entidades: Modelo + Listar
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var lista = await _ctRepoUMillaEnc.obtenerListaTodosAsync();
            return Ok(lista);
        }

        // Función para obtener entidad por ID: Modelo + Obtener
        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var entidad = await _ctRepoUMillaEnc.obtenerPorIdAsync(id);

            if (entidad == null)
            {
                return NotFound($"La entidad con ID {id} no fue encontrada.");
            }

            return Ok(entidad);
        }

        // Función para activar la entidad (Alta)
        [HttpPatch("Alta/{id}")]
        public async Task<IActionResult> Alta(int id)
        {
            bool resultado = await _ctRepoUMillaEnc.Alta(id, x => x.GetType().GetProperty("Activo"), true);

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
        [HttpPatch("Baja/{id}")]
        public async Task<IActionResult> Baja(int id)
        {
            bool resultado = await _ctRepoUMillaEnc.Baja(id, x => x.GetType().GetProperty("Activo"), false);

            if (resultado)
            {
                return Ok($"La entidad {id} fue desactivada exitosamente.");
            }
            else
            {
                return BadRequest("No se pudo desactivar la entidad.");
            }
        }


        #endregion UltMillaEnc

        //#region UMillaDet
        //// Función para agregar entidad: Modelo + Crear
        //[HttpPost("UMillaDetCrear")]
        //public async Task<IActionResult> UMillaDetCrear([FromBody] DtUltimaMillaDet entidad)
        //{
        //    try
        //    {
        //        if (entidad == null)
        //        {
        //            return BadRequest("La entidad no puede ser nula");
        //        }

        //        _ctRepoUMillaDet.AgregarAsync(entidad);
        //        return Ok(entidad);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        //    }
        //}

        //// Función para guardar cambios: Modelo + Guardar
        //[HttpPut("UMillaDetActualizar/{id}")]
        //public async Task<IActionResult> UMillaDetActualizar(int id, [FromBody] DtUltimaMillaDet entidad)
        //{
        //    var entidadExistente = await _ctRepoUMillaDet.obtenerPorIdAsync(id);

        //    if (entidadExistente == null)
        //    {
        //        return NotFound($"La entidad con ID {id} no fue encontrada.");
        //    }

        //    await _ctRepoUMillaDet.ActualizarAsync(entidad);
        //    return Ok($"La entidad {entidad.IdDtUltimaMillaDet} fue actualizada exitosamente.");
        //}
        
        

        //// Función para obtener todas las entidades: Modelo + Listar
        //[HttpGet("UMillaDetListar")]
        //public async Task<IActionResult> UMillaDetListar()
        //{
        //    var lista = await _ctRepoUMillaDet.obtenerListaTodosAsync();
        //    return Ok(lista);
        //}

        //// Función para obtener entidad por ID: Modelo + Obtener
        //[HttpGet("UMillaDetObtener/{id}")]
        //public async Task<IActionResult> UMillaDetObtener(int id)
        //{
        //    var entidad = await _ctRepoUMillaDet.obtenerPorIdAsync(id);

        //    if (entidad == null)
        //    {
        //        return NotFound($"La entidad con ID {id} no fue encontrada.");
        //    }

        //    return Ok(entidad);
        //}

        //// Función para activar la entidad (Alta)
        //[HttpPatch("UMillaDetAlta/{id}")]
        //public async Task<IActionResult> UMillaDetAlta(int id)
        //{
        //    bool resultado = await _ctRepoUMillaDet.Alta(id, x => x.GetType().GetProperty("Activo"), true);

        //    if (resultado)
        //    {
        //        return Ok($"La entidad {id} fue activada exitosamente.");
        //    }
        //    else
        //    {
        //        return BadRequest("No se pudo activar la entidad.");
        //    }
        //}

        //// Función para desactivar la entidad (Baja)
        //[HttpPatch("UMillaDetBaja/{id}")]
        //public async Task<IActionResult> UMillaDetBaja(int id)
        //{
        //    bool resultado = await _ctRepoUMillaDet.Baja(id, x => x.GetType().GetProperty("Activo"), false);

        //    if (resultado)
        //    {
        //        return Ok($"La entidad {id} fue desactivada exitosamente.");
        //    }
        //    else
        //    {
        //        return BadRequest("No se pudo desactivar la entidad.");
        //    }
        //}

        //#endregion UMillaDet
    }
}
