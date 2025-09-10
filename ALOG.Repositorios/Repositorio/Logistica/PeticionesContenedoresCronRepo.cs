using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Peticiones;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Utilerias;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ALOG.Repositorios.Repositorio.Logistica
{
    public class PeticionesContenedoresCronRepo : IPeticionesContenedorCronRepo
    {
        private readonly ApplicationDbContext _context;
        private IGenericoRepositorio<PeticionesContenedoresCron> _ctgenericoRepositorio;
        //private UtileriasRespuestas objUtileriasRespuestas = new UtileriasRespuestas();
        //private RespuestaGenericaDTO objRespuesta;
        
        public PeticionesContenedoresCronRepo(ApplicationDbContext context, IGenericoRepositorio<PeticionesContenedoresCron> ctgenericoRepositorio)
        {
            _context = context;
            _ctgenericoRepositorio = ctgenericoRepositorio;
            //objRespuesta = objUtileriasRespuestas.InicializaRespuestaIncorrecta();
        }

        public async Task<RespuestaGenericaDTO> actualizarCronologiaporContenedor(PeticionesContenedoresCron pPeticionesContenedoresCron)
        {
            var objUtileriasRespuestas = new UtileriasRespuestas();
            var objRespuesta = new RespuestaGenericaDTO();
            
            try
            {
                // Validar existencia
                var objExistente = await _context.peticionesContenedoresCron
                    .FirstOrDefaultAsync(x => x.IdContenedorCron == pPeticionesContenedoresCron.IdContenedorCron);

                if (objExistente == null)
                {
                    objRespuesta.strMensaje = "No se encontró el registro";
                    objRespuesta.StatusCode = HttpStatusCode.NotFound;
                    return objRespuesta;
                }

                // Actualizar campos específicos
                objExistente.FechaEvento = pPeticionesContenedoresCron.FechaEvento;
                objExistente.IdCatTipoIncidenciaEvento = pPeticionesContenedoresCron.IdCatTipoIncidenciaEvento;
                objExistente.Comentarios = pPeticionesContenedoresCron.Comentarios;

                _context.Entry(objExistente).Property(x => x.FechaEvento).IsModified = true;
                _context.Entry(objExistente).Property(x => x.IdCatTipoIncidenciaEvento).IsModified = true;
                _context.Entry(objExistente).Property(x => x.Comentarios).IsModified = true;

                await _context.SaveChangesAsync();

                objRespuesta = objUtileriasRespuestas.InicializaRespuestaCorrecta();
                objRespuesta.strMensaje = "Se ha actualizado el evento correctamente";
                objRespuesta.Entidad = objExistente;
                return objRespuesta;
            }
            catch (Exception ex)
            {
                objRespuesta.strMensaje = "Error al actualizar el evento: " + ex.Message;
                objRespuesta.StatusCode = HttpStatusCode.InternalServerError;
                return objRespuesta;
            }
        }

        public async Task<RespuestaGenericaDTO> agregarCronologiaporContenedor(PeticionesContenedoresCron pPeticionesContenedoresCron)
        {
            #region VARIABLES
            var objUtileriasRespuestas = new UtileriasRespuestas();
            var objRespuesta = new RespuestaGenericaDTO();

            #endregion VARIABLES
            //if (pPeticionesContenedoresCron.FechaEvento > pPeticionesContenedoresCron.FechaRegistro)
            //{
            //    objRespuesta.IsSuccess = false;
            //    objRespuesta.strMensaje = "La fecha del evento no puede ser mayor a la fecha de registro.";
            //    objRespuesta.StatusCode = System.Net.HttpStatusCode.BadRequest;
            //    return objRespuesta;
            //}
            try
            {
                await _context.peticionesContenedoresCron.AddAsync(pPeticionesContenedoresCron);
                await _context.SaveChangesAsync();
                objRespuesta = objUtileriasRespuestas.InicializaRespuestaCorrecta();
                // Si el guardado fue correcto, se manda esto
                objRespuesta.IsSuccess = true;
                objRespuesta.strMensaje = "Se ha registrado el evento correctamente";
                // Si el guardado no fue correcto
                //objRespuesta.IsSuccess = false;
                //objRespuesta.strMensaje = "Error al registrar el evento.";
                return objRespuesta;
            }
            catch (Exception ex)
            {
                objRespuesta.strMensaje = "Error al registrar el evento: " + ex.Message;
                return objRespuesta;
            }

        }

        public async Task<RespuestaGenericaDTO> bajaCronologiaporContenedor(int pIdContenedorCron)
        {
            #region VARIABLES
            var objUtileriasRespuestas = new UtileriasRespuestas();
            var objRespuesta = new RespuestaGenericaDTO();

            #endregion VARIABLES
            #region OPERACION
            //var objContenedorCron = await _context.peticionesContenedoresCron
            //    .AnyAsync(x => x.IdContenedorCron == pIdContenedorCron);
            //if (!objContenedorCron)
            //{
            //    objRespuesta.strMensaje = "No se encontró el registro";
            //    objRespuesta.StatusCode = HttpStatusCode.NotFound;
            //    return objRespuesta;
            //}
            //var objContenedor = await _context.peticionesContenedoresCron
            //    .FirstOrDefaultAsync(x => x.IdContenedorCron == pIdContenedorCron);
            //if (objContenedor != null)
            //{
            //    objContenedor.Activo = false;
            //    _context.peticionesContenedoresCron.Update(objContenedor);
            //    await _context.SaveChangesAsync();
            //    objRespuesta = objUtileriasRespuestas.InicializaRespuestaCorrecta();
            //    objRespuesta.strMensaje = "Se ha dado de baja el registro correctamente";
            //    return objRespuesta;
            //}
            //else
            //{
            //    objRespuesta.strMensaje = "No se encontró el registro";
            //    objRespuesta.StatusCode = HttpStatusCode.NotFound;
            //    return objRespuesta;
            //}
            #endregion OPERACION

            #region OPERACION2
            var objContenedor = await _context.peticionesContenedoresCron
                .FirstOrDefaultAsync(x => x.IdContenedorCron == pIdContenedorCron);

            if (objContenedor == null)
            {
                objRespuesta.strMensaje = "No se encontró el registro";
                objRespuesta.StatusCode = HttpStatusCode.NotFound;
                return objRespuesta;
            }

            // Solo actualizar la propiedad Activo
            objContenedor.Activo = false;
            _context.Entry(objContenedor).Property(x => x.Activo).IsModified = true;

            await _context.SaveChangesAsync();

            objRespuesta = objUtileriasRespuestas.InicializaRespuestaCorrecta();
            objRespuesta.strMensaje = "Se ha dado de baja el registro correctamente";
            return objRespuesta;
            #endregion OPERACION2
        }

        public async Task<List<PeticionesContenedoresCron>> obtenerCronologiaporContenedor(int pIdContenedor, int pIdServicio)
        {

            #region VARIABLES
            //*RespuestaGenericaDTO objRespuesta = objUtileriasRespuestas.InicializaRespuesta();
            var lstPeticionesContenedoresCron = new List<PeticionesContenedoresCron>();
            #endregion VARIABLES

            #region OPERACION
            
            #region VALIDACIONES
            if (pIdContenedor > 0)
            {
                //objRespuesta = objUtileriasRespuestas.InicializaRespuestaIncorrecta();
                //objRespuesta.strMensaje = "Por favor verifique el parámetro: IdContenedor";
                //return objRespuesta;

                lstPeticionesContenedoresCron = await _context.peticionesContenedoresCron
                    .Include(p => p.catTipoIncidenciaEvento)
                        .ThenInclude(i => i.catTipoEventosCron)
                    .Include(p => p.catTipoIncidenciaEvento)
                        .ThenInclude(i => i.catTipoIncidenciaCron)
                    .Include(p => p.catUsuarios)
                    .Include(p => p.peticionesServicios)
                        .ThenInclude(s => s.catServicios)
                    .Include(p => p.peticionesContenedores)
                    .Where(p => p.IdContenedor == pIdContenedor && 
                                p.IdServicio == pIdServicio &&
                                p.Activo == true
                    )
                    .ToListAsync();
            }

            return lstPeticionesContenedoresCron;
            #endregion VALIDACIONES
            #endregion OPERACION
            //if (!_context.peticionesContenedores.Any(x => x.IdContenedor == pIdContenedor))
            //{
            //    objRespuesta = objUtileriasRespuestas.InicializaRespuestaIncorrecta();
            //    objRespuesta.strMensaje = "IdContenedor no existe en la base de datos";
            //    return objRespuesta;
            //}



            //try
            //{
            //    lstPeticionesContenedoresCron = await _context.peticionesContenedoresCron
            //        .Include(p => p.catTipoEventosCron)
            //        .Include(p => p.catTipoIncidenciaCron)
            //        .Include(p => p.catUsuarios)
            //        .Where(p => p.IdContenedor == pIdContenedor && p.Activo == true)
            //        .ToListAsync();


            //    objRespuesta = objUtileriasRespuestas.InicializaRespuestaCorrecta();
            //    if (lstPeticionesContenedoresCron.Count() > 0)
            //    {
            //        if (objRespuesta.Entidades == null)
            //            objRespuesta.Entidades = new List<object>();
            //        objRespuesta.Entidades.AddRange(lstPeticionesContenedoresCron);
            //        objRespuesta.IsSuccess = true;
            //        objRespuesta.StatusCode = HttpStatusCode.OK;

            //    }
            //    else
            //    {
            //        objRespuesta.IsSuccess = false;
            //        objRespuesta.StatusCode = HttpStatusCode.NotFound;
            //    }

            //    return objRespuesta;


            //}
            //catch (Exception ex)
            //{
            //    objRespuesta = objUtileriasRespuestas.InicializaRespuestaIncorrecta();
            //    objRespuesta.strMensaje = "Error en la consulta: " + ex.Message;
            //    return objRespuesta;


            //}


        }
    }
}
