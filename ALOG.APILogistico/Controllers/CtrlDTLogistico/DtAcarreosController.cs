//using ApiVacios.Controllers.Catalogos;
using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using ALOG.Repositorios.Utilerias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiVacios.Controllers.CtrlDTLogistico
{
    //[AllowAnonymous]
    [Authorize(Roles = "ADMIN,CLIENTE,ADMINUSER")]
    [Route("DTLogistica/[controller]")]
    [ApiController]
    public class DtAcarreosController : Controller
    {
        private readonly IGenericoRepositorio<DtAcarreos> _ctRepoGen;
        private readonly IDtAcarreoRepositorio _ctRepoAcarreos;
        private readonly IOrdenesRepositorio _ctRepoOrdenes;
        private readonly ApplicationDbContext _db;
        private RespuestaGenericaDTO _respuestaGenericaDTO = new RespuestaGenericaDTO();
        private UtileriasRespuestas objUtileriasRespuestas = new UtileriasRespuestas();
        private List<string> _lstErrores = new List<string>();
        public DtAcarreosController(ApplicationDbContext db, IGenericoRepositorio<DtAcarreos> ctRepoGen, IDtAcarreoRepositorio ctRepoAcarreos, IOrdenesRepositorio ctReporOrdenes)
        {
            _ctRepoGen = ctRepoGen;
            _db = db;
            _ctRepoAcarreos = ctRepoAcarreos;
            _ctRepoOrdenes = ctReporOrdenes;
            _respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();

        }



        // Función para desactivar la entidad (Baja)
        [HttpPatch("Baja/{id}")]
        public async Task<IActionResult> Baja(int id)
        {
            #region Variables
            _respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            #endregion Variables

            bool resultado = _ctRepoAcarreos.BajaGenerico(id, x => x.GetType().GetProperty("Activo"), false);

            if (resultado)
            {
                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.strMensaje = $"La entidad {id} fue desactivada exitosamente.";
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                return Ok(_respuestaGenericaDTO);
            }
            else
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add("No se pudo dar de baja la entidad.");
                return BadRequest(_respuestaGenericaDTO);
            }
        }


        // Función para agregar entidad: Modelo + Crear
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] DtAcarreos entidad)
        {
            #region Variables
            _respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            Ordenes objOrden = new Ordenes();
            #endregion Variables

            #region Operaciones
            try
            {
                if (!ModelState.IsValid)
                {
                    _respuestaGenericaDTO.lstrErrorMessages.Add("Error en validación de datos.");
                    _respuestaGenericaDTO.lstrErrorMessages.Add(ModelState.ToString());
                    return BadRequest(_respuestaGenericaDTO);
                }

                if (entidad == null)
                {
                    _respuestaGenericaDTO.lstrErrorMessages.Add("La entidad no puede ser nula");
                    return BadRequest(_respuestaGenericaDTO);
                }

                //Crear Orden
                objOrden = new Ordenes();
                objOrden.IdOrden = 0;
                objOrden.IdCatCliente = entidad.IdCliente;
                objOrden.IdCatEmpresa = entidad.IdEmpresa;
                objOrden.IdCatAduana = 1;
                objOrden.IdCatLineaNegocio = 3;
                objOrden.IdUsuario = 1;
                objOrden.IdCatSucursal = 1;
                objOrden.IdCatSistema = 1;

                //Crear Solicitud

                var objResp = await _ctRepoOrdenes.CrearOrden(objOrden);

                if (objResp != null)
                {
                    entidad.IdOrden = objResp.IdOrden;
                    if (_ctRepoAcarreos.AgregarGenerico(entidad) != null)
                    {
                        _respuestaGenericaDTO.IsSuccess = true;
                        _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                        _respuestaGenericaDTO.strMensaje = "Creado con éxito";
                        _respuestaGenericaDTO.Entidad = entidad;
                        return Ok(_respuestaGenericaDTO);
                    }
                    else
                    {
                        _respuestaGenericaDTO.lstrErrorMessages.Add("Error al guardar la información verifique con Soporte ALOGISTICS");
                        return BadRequest(_respuestaGenericaDTO);
                    }
                }
                else
                {
                    _respuestaGenericaDTO.lstrErrorMessages.Add("Error al generar la orden la información verifique con Soporte ALOGISTICS");
                    return BadRequest(_respuestaGenericaDTO);
                }
            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add($"Error interno del servidor: {ex.Message}");
                return StatusCode(500, _respuestaGenericaDTO);
            }
        }

        // Función para guardar cambios: Modelo + Guardar
        [HttpPut("Actualizar")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Actualizar([FromBody] DtAcarreos entidad)
        {
            #region Variables
            _respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            #endregion Variables
            try
            {
                if (!ModelState.IsValid)
                {

                    _respuestaGenericaDTO.lstrErrorMessages.Add(ModelState.ValidationState.ToString());
                    return NotFound(_respuestaGenericaDTO);
                }


                var entidadExistente = await _ctRepoAcarreos.obtenerPorIdGenerico(entidad.IdDtAcarreos);

                if (entidadExistente == null)
                {
                    _respuestaGenericaDTO.lstrErrorMessages.Add("$\"La entidad con ID {id} no fue encontrada.\"");
                    //_lstErrores.Add($"La entidad con ID {entidad.IdDtUltMillaEnc} no fue encontrada.");
                    return NotFound(_respuestaGenericaDTO);
                }



                if (_ctRepoAcarreos.ActualizarGenerico(entidad.IdDtAcarreos, entidad) == null)
                {
                    _respuestaGenericaDTO.lstrErrorMessages.Add($"La entidad {entidad.IdDtAcarreos} fue actualizada exitosamente.");
                    return NotFound(_respuestaGenericaDTO);
                }

                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                _respuestaGenericaDTO.strMensaje = $"La entidad {entidad.IdDtAcarreos} fue actualizada exitosamente.";
                _respuestaGenericaDTO.Entidad = entidad;
                //return Ok($"La entidad {typeof(T).Name} fue actualizada exitosamente.");
                return Ok(_respuestaGenericaDTO);

            }
            catch (Exception ex)
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add(ex.Message);
                return BadRequest(_respuestaGenericaDTO);
            }
            #endregion Operaciones
        }




        // Función para obtener todas las entidades: Modelo + Listar
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var lista = await _ctRepoAcarreos.obtenerListaTodosGenerico();
            return Ok(lista);
        }

        // Función para obtener entidad por ID: Modelo + Obtener
        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            #region Variables
            _respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            #endregion Variables
            var entidad = _ctRepoAcarreos.obtenerAcarreo(id);

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
            #region Variables
            _respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            #endregion Variables
            bool resultado = _ctRepoAcarreos.AltaGenerico(id, x => x.GetType().GetProperty("Activo"), true);

            if (resultado)
            {
                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                _respuestaGenericaDTO.strMensaje = $"La entidad {id} fue activada exitosamente.";
                return Ok(_respuestaGenericaDTO);
            }
            else
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add("No se pudo activar la entidad.");
                return BadRequest(_respuestaGenericaDTO);
            }
        }




        [HttpPut("CambiarEstado/{idEncabezado}/{idTipoEstado}")]
        public async Task<IActionResult> CambiarEstado(int idEncabezado, int idTipoEstado)
        {

            #region Variables
            _respuestaGenericaDTO = objUtileriasRespuestas.InicializaRespuesta();
            #endregion Variables


            if (idEncabezado <= 0 && idTipoEstado <= 0)
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add("Alguno de los valores es inválido");
                return NotFound(_respuestaGenericaDTO);
            }

            var encabezado = await _ctRepoAcarreos.obtenerPorIdGenerico(idEncabezado);

            if (encabezado == null)
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add("Id no encontrada");
                return NotFound(_respuestaGenericaDTO);
            }
            //             Terminado             Cancelado
            if (encabezado.IdCatTipoEstado == 3 || encabezado.IdCatTipoEstado == 4)
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add("No se puede cambiar el estado de la entidad " + idEncabezado + " cuando el estado está en Terminado o Cancelado");
                return BadRequest(_respuestaGenericaDTO);
            }

            bool respuesta = await _ctRepoAcarreos.CambioEstado(idEncabezado, idTipoEstado);

            if (respuesta)
            {
                _respuestaGenericaDTO.strMensaje = $"La entidad {idEncabezado} fue actualizada exitosamente.";
                _respuestaGenericaDTO.IsSuccess = true;
                _respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                return Ok(_respuestaGenericaDTO);
            }
            else
            {
                _respuestaGenericaDTO.lstrErrorMessages.Add("No se puede cambiar el estado de la entidad " + idEncabezado);
                return NotFound(_respuestaGenericaDTO);
            }
        }



        [HttpPost("ObtieneAcarreos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtieneAcarreos(FiltroDtAcarreosDTO pFiltro)
        {
            #region Variables
            var lstServiciosDto = new List<PeticionesReferenciasDTO>();
            List<RespObtenerAcarreosDTO> respObtenerAcarreosDTOs = new List<RespObtenerAcarreosDTO>();
            #endregion Variables

            var lstServicios = await _ctRepoAcarreos.obtenerAcarreos(pFiltro);


            if (lstServicios != null)
            {
                foreach (var i in lstServicios)
                {
                    respObtenerAcarreosDTOs.Add(new RespObtenerAcarreosDTO
                    {
                        idDtAcarreos = i.IdDtAcarreos,
                        IdCatServicio = i.IdCatServicio,
                        IdCatTipoEstado = i.IdCatTipoEstado,
                        IdCatProveedor = i.IdCatProveedor,
                        IdCliente = i.IdCliente,
                        IdEmpresa = i.IdEmpresa,
                        IdOrden = i.IdOrden,
                        EmpresaRazonSocial = i.catEmpresa.RazonSocial,
                        NombreCliente = i.catClientes.RazonSocial,
                        Activo = i.Activo,
                        NombreServicio = i.catServicios.Nombre,
                        TipoEstado = i.catTipoEstados.Nombre,
                        Fecha = i.Fecha,
                        FechaRegistro = i.FechaRegistro,
                        IdUsarioRegistro = i.IdUsuarioRegistro,
                        Usuario = i.catUsuario.Usuario,
                        NumeroContenedor = i.Contenedor




                    });

                }


                return Ok(respObtenerAcarreosDTOs);
            }
            else
                return StatusCode(StatusCodes.Status404NotFound);

        }
    }
}
