using ApiVacios.Modelos;
using ApiVacios.Modelos.DTO;
using ApiVacios.Modelos.Vacios;
using ApiVacios.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiVacios.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/servicios")]
    public class ServiciosController : ControllerBase
    {

        private readonly IServicioRepositorio _ctRepo;
        private readonly IMapper _mapper;
        public ServiciosController(IServicioRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
                
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetServicios()
        {
            var lstServicios= _ctRepo.GetServicios();
            var lstServiciosDto = new List<ServiciosDTO>();
            foreach (var lista in lstServicios)
            {
                lstServiciosDto.Add(_mapper.Map<ServiciosDTO>(lista)); //se utiliza mapper para 
            }
            return Ok(lstServiciosDto);
        }
        [HttpGet("{servicioId:int}", Name ="GetServicio")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetServicio(int servicioId)
        {
            var itemServicio = _ctRepo.GetServicios(servicioId);
            if(itemServicio==null)
            {
                return NotFound();
            }
            var itemServicioDto = _mapper.Map<ServiciosDTO>(itemServicio);
            return Ok(itemServicioDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ServiciosDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearServicio([FromBody] ServiciosDTO serviciosDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (serviciosDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctRepo.ExisteServicio(serviciosDTO.referenciaALO))
            {
                ModelState.AddModelError("", "La película ya existe");
                return StatusCode(404, ModelState);
            }

            var servicio = _mapper.Map<Servicios>(serviciosDTO);

            if (!_ctRepo.CrearServicio(servicio))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{servicio.referenciaALO}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetServicio", new { servicioId = servicio.id }, servicio);
        }

        [HttpPost("CrearServicio1")]
        [ProducesResponseType(201, Type = typeof(PeticionesReferencias))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearServicio1([FromBody] PeticionesReferencias vaciosReferencias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (vaciosReferencias == null)
            {
                return BadRequest(ModelState);
            }

           /* if (_ctRepo.ExisteServicio(vaciosReferencias.Id))
            {
                ModelState.AddModelError("", "La película ya existe");
                return StatusCode(404, ModelState);
            }*/

            /*var servicio = _mapper.Map<Servicios>(vaciosReferencias);

            if (!_ctRepo.CrearServicio(servicio))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{servicio.referenciaALO}");
                return StatusCode(500, ModelState);
            }*/
            //return CreatedAtRoute("GetServicio", new { servicioId = servicio.id }, servicio);
            return Ok(vaciosReferencias);
        }

        [HttpPatch("{servicioId:int}", Name="ActualizaPatchServicio")]
        [ProducesResponseType(201, Type = typeof(ServiciosDTO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizaPatchServicio(int servicioId, [FromBody] ServiciosDTO serviciosDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (serviciosDTO == null || servicioId != serviciosDTO.id)
            {
                return BadRequest(ModelState);
            }

            var servicio = _mapper.Map<Servicios>(serviciosDTO);

            if (!_ctRepo.ActualizarServicio(servicio))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro{servicio.referenciaALO}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{servicioId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarCategoria(int servicioId)
        {
         

            if (!_ctRepo.ExisteServicio(servicioId))
            {
                ModelState.AddModelError("", "El servicio no existe");
                return NotFound();
            }

            var servicio = _ctRepo.GetServicios(servicioId);

            if (!_ctRepo.BorrarServicio(servicio))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro{servicio.referenciaALO}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }



    }
}
