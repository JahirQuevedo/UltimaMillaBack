using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.Logistica
{
    public class SLOSolicitudesRepositorio : GenericoRepositorio<SLOSolicitudes>, ISLOSolicitudesRepositorio
    {
        private readonly ApplicationDbContext _db;

        public SLOSolicitudesRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<RespuestaGenericaDTO> SLOSolicitudesCrear(SLOSolicitudes solicitud)
        {
            RespuestaGenericaDTO respuestaGenericaDTO = new RespuestaGenericaDTO();
            if (solicitud == null)
            {
                respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                respuestaGenericaDTO.lstrErrorMessages = new List<string>
                {
                    "La solicitud no puede ser null"
                };
                return respuestaGenericaDTO;
            }

            Ordenes nueva_orden = new Ordenes();

            OrdenesReferenciaSLO ordenesReferenciaSLO = new OrdenesReferenciaSLO(_db);

            //Asignamos los datos necesarios para generar la orden
            nueva_orden.IdCatCliente = solicitud.IdCatCliente;
            nueva_orden.IdCatSistema = 1;
            nueva_orden.IdCatAduana = 1;
            //nueva_orden.IdCatProveedor = 1;
            nueva_orden.IdCatEmpresa = 1;
            nueva_orden.IdCatSucursal = 1;
            nueva_orden.IdCatLineaNegocio = 3;
            nueva_orden.IdUsuario = solicitud.IdCatUsuario;
            nueva_orden.FechaRegistro = solicitud.FechaRegistro;
            nueva_orden.IdEstadoOrden = 1;
            nueva_orden.IdCatProyecto = 1;
            nueva_orden.Activo = true;
            //Llamamos la clase con el metodo que crea la orden y asigna la referencia ALO
            try
            {
                var orden_creada = await ordenesReferenciaSLO.CrearOrden(nueva_orden);
                //Validamos la creación de la orden
                if (orden_creada != null)
                {
                    //Creamos la solicitud de servicio
                    try
                    {
                        solicitud.IdOrden = orden_creada.IdOrden;
                        //solicitud.ReferenciaCliente = "referencia";
                        //solicitud.IdCatTipoOperacion = 1;
                        //solicitud.IdCatTipoOperComercio = 1;
                        //solicitud.IdCatUbicacionOrigen = 2;
                        //solicitud.IdCatUbicacionDestino = 2;                        
                        await _db.SLOSolicitudes.AddAsync(solicitud);
                        var response = await _db.SaveChangesAsync();

                        //Validamos que el objeto fue guardado correctamente
                        if (response >= 0 ? true : false)
                        {
                            respuestaGenericaDTO.IsSuccess = true;
                            respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                            respuestaGenericaDTO.strMensaje = "Solicitud creada correctamente";
                            return respuestaGenericaDTO;
                        }
                        else
                        {
                            respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                            respuestaGenericaDTO.strMensaje = "Error al crear la solicitud";
                            return respuestaGenericaDTO;

                        }
                    }
                    catch (Exception ex)
                    {
                        respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                        respuestaGenericaDTO.lstrErrorMessages = new List<string>
                        {
                            "Datos incompletos o nulos al crear la solicitud",
                            ex.Message,
                            ex.InnerException.Message

                        };
                        return respuestaGenericaDTO;
                    }
                }
                else
                {
                    respuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
                    respuestaGenericaDTO.lstrErrorMessages = new List<string>
                    {
                        "La orden generada no fue retornada o fue encontrada null en la validación."
                    };
                }
            }
            catch (Exception ex)
            {
                respuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
                respuestaGenericaDTO.lstrErrorMessages = new List<string>
                {
                    "Error al crear la orden",
                    ex.Message
                };
                return respuestaGenericaDTO;
            }
            return respuestaGenericaDTO;
        }

        //public async Task<ICollection<SLOSolicitudes>> SLOSolicitudesObtener(FiltroSLOSolicitudes pFiltro)
        //{
        //    pFiltro.Activo = true;
        //    try
        //    {
        //        var fechaPosIni = pFiltro.FechaPosicionamientoInicio?.Date ?? DateTime.Today.AddDays(-20);
        //        var fechaPosFin = pFiltro.FechaPosicionamientoFin ?? DateTime.Now;

        //        var query = _db.SLOSolicitudes
        //            .Include(a => a.Cliente)
        //            .Include(a => a.catUsuario)
        //            .Include(a => a.catTipoEstado)
        //            .Include(c => c.TipoCarga)
        //            .Include(o => o.Orden)                    
        //            .Include(u => u.catClienteUbicacionOrigen).ThenInclude(cuo => cuo.catPaises).Include(u => u.catClienteUbicacionOrigen).ThenInclude(cuo => cuo.catPaisEstados).Include(u => u.catClienteUbicacionOrigen).ThenInclude(cuo => cuo.catPaisMunicipios)
        //            .Include(u => u.catClienteUbicacionDestino).ThenInclude(cud => cud.catPaises).Include(u => u.catClienteUbicacionDestino).ThenInclude(cud => cud.catPaisEstados).Include(u => u.catClienteUbicacionDestino).ThenInclude(cud => cud.catPaisMunicipios)
        //            .Include(d => d.sloSOlicitudesDetalle).ThenInclude(m => m.catTipoMercancia).Include(d => d.sloSOlicitudesDetalle).ThenInclude(slo => slo.catTipoIMO)
        //            .Include(o => o.catTipoOperacion)
        //            .AsQueryable();

        //        if (pFiltro.IdSLOSolicitud != null)
        //            query = query.Where(a => a.IdSLOSolicitud == pFiltro.IdSLOSolicitud);

        //        if (pFiltro.IdCatCliente != null)
        //            query = query.Where(a => a.IdCatCliente == pFiltro.IdCatCliente);

        //        if (pFiltro.IdCatUbicacionOrigen != null)
        //            query = query.Where(a => a.IdCatUbicacionOrigen == pFiltro.IdCatUbicacionOrigen);

        //        if (pFiltro.IdCatUbicacionDestino != null)
        //            query = query.Where(a => a.IdCatUbicacionDestino == pFiltro.IdCatUbicacionDestino);

        //        if (pFiltro.IdCatTipoOperComercio != null)
        //            query = query.Where(a => a.IdCatTipoOperComercio == pFiltro.IdCatTipoOperComercio);

        //        if (pFiltro.IdCatTipoCarga != null)
        //            query = query.Where(a => a.IdCatTipoCarga == pFiltro.IdCatTipoCarga);

        //        if (pFiltro.IdCatUsuario != null)
        //            query = query.Where(a => a.IdCatUsuario == pFiltro.IdCatUsuario);

        //        if (pFiltro.IdCatTipoEstado != null)
        //            query = query.Where(a => a.IdCatTipoEstado == pFiltro.IdCatTipoEstado);

        //        if (pFiltro.IdCatTipoOperacion != null)
        //            query = query.Where(a => a.IdCatTipoOperacion == pFiltro.IdCatTipoOperacion);

        //        if (pFiltro.IdOrden != null)
        //            query = query.Where(a => a.IdOrden == pFiltro.IdOrden);

        //        if (!string.IsNullOrEmpty(pFiltro.ReferenciaCliente))
        //            query = query.Where(a => a.ReferenciaCliente.Contains(pFiltro.ReferenciaCliente));

        //        if (!string.IsNullOrEmpty(pFiltro.Booking))
        //            query = query.Where(a => a.Booking.Contains(pFiltro.Booking));

        //        query = query.Where(a => a.FechaPosicionamiento >= fechaPosIni && a.FechaPosicionamiento <= fechaPosFin); //

        //        if (pFiltro.FechaInicio != null)
        //            query = query.Where(a => a.FechaInicio >= pFiltro.FechaInicio);

        //        if (pFiltro.FechaFin != null)
        //            query = query.Where(a => a.FechaFin <= pFiltro.FechaFin);

        //        if (pFiltro.Activo != null)
        //            query = query.Where(a => a.Activo == pFiltro.Activo);

        //        var solicitudes = await query
        //            .OrderByDescending(a => a.IdSLOSolicitud)
        //            .ToListAsync();
        //        foreach(var solicitud in solicitudes)
        //        {
        //            solicitud.sloSOlicitudesDetalle = solicitud.sloSOlicitudesDetalle
        //                .Where(det => det.Activo).ToList();
        //        }
        //        return solicitudes;
        //    } catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<ICollection<SLOSolicitudes>> SLOSolicitudesObtener(FiltroSLOSolicitudes pFiltro)
        {
            pFiltro.Activo = true;

            var fechaPosIni = pFiltro.FechaPosicionamientoInicio?.Date ?? DateTime.Today.AddDays(-20);
            var fechaPosFin = pFiltro.FechaPosicionamientoFin ?? DateTime.Now;

            var query = _db.SLOSolicitudes
                .AsNoTracking()

                // ===== Includes del padre =====
                .Include(a => a.Cliente)
                .Include(a => a.catUsuario)
                .Include(a => a.catTipoEstado)
                .Include(c => c.TipoCarga)
                .Include(o => o.Orden)
                .Include(t => t.catTipoOperComercio)

                .Include(u => u.catClienteUbicacionOrigen).ThenInclude(cuo => cuo.catPaises)
                .Include(u => u.catClienteUbicacionOrigen).ThenInclude(cuo => cuo.catPaisEstados)
                .Include(u => u.catClienteUbicacionOrigen).ThenInclude(cuo => cuo.catPaisMunicipios)

                .Include(u => u.catClienteUbicacionDestino).ThenInclude(cud => cud.catPaises)
                .Include(u => u.catClienteUbicacionDestino).ThenInclude(cud => cud.catPaisEstados)
                .Include(u => u.catClienteUbicacionDestino).ThenInclude(cud => cud.catPaisMunicipios)

                // ===== Filtered Include: SOLO detalles NO asignados =====
                .Include(s => s.sloSOlicitudesDetalle
                    .Where(sd =>
                        sd.Activo &&
                        !_db.SLOTransporteDetalles.Any(td =>
                            td.Activo &&
                            td.IdSLOSolicitudDet == sd.IdSLOSolicitudDet &&
                            _db.SLOTransporteSolicitudes.Any(ts =>
                                ts.Activo &&
                                ts.IdSLOTransporteSolicitud == td.IdSLOTransporteSolicitud))))
                    .ThenInclude(sd => sd.catTipoMercancia)

        // Si necesitas incluir otra navegación del detalle, repite la cadena
        .Include(s => s.sloSOlicitudesDetalle
            .Where(sd =>
                sd.Activo &&
                !_db.SLOTransporteDetalles.Any(td =>
                    td.Activo &&
                    td.IdSLOSolicitudDet == sd.IdSLOSolicitudDet &&
                    _db.SLOTransporteSolicitudes.Any(ts =>
                        ts.Activo &&
                        ts.IdSLOTransporteSolicitud == td.IdSLOTransporteSolicitud))))
            .ThenInclude(sd => sd.catTipoIMO)

        .Include(o => o.catTipoOperacion)

        // Evita explosión cartesiana con muchos includes
        .AsSplitQuery()
        .AsQueryable();

            // ===== Filtros del padre =====
            if (pFiltro.IdSLOSolicitud != null)
                query = query.Where(a => a.IdSLOSolicitud == pFiltro.IdSLOSolicitud);

            if (pFiltro.IdCatCliente != null)
                query = query.Where(a => a.IdCatCliente == pFiltro.IdCatCliente);

            if (pFiltro.IdCatUbicacionOrigen != null)
                query = query.Where(a => a.IdCatUbicacionOrigen == pFiltro.IdCatUbicacionOrigen);

            if (pFiltro.IdCatUbicacionDestino != null)
                query = query.Where(a => a.IdCatUbicacionDestino == pFiltro.IdCatUbicacionDestino);

            if (pFiltro.IdCatTipoOperComercio != null)
                query = query.Where(a => a.IdCatTipoOperComercio == pFiltro.IdCatTipoOperComercio);

            if (pFiltro.IdCatTipoCarga != null)
                query = query.Where(a => a.IdCatTipoCarga == pFiltro.IdCatTipoCarga);

            if (pFiltro.IdCatUsuario != null)
                query = query.Where(a => a.IdCatUsuario == pFiltro.IdCatUsuario);

            if (pFiltro.IdCatTipoEstado != null)
                query = query.Where(a => a.IdCatTipoEstado == pFiltro.IdCatTipoEstado);

            if (pFiltro.IdCatTipoOperacion != null)
                query = query.Where(a => a.IdCatTipoOperacion == pFiltro.IdCatTipoOperacion);

            if (pFiltro.IdOrden != null)
                query = query.Where(a => a.IdOrden == pFiltro.IdOrden);

            if (!string.IsNullOrEmpty(pFiltro.ReferenciaCliente))
                query = query.Where(a => a.ReferenciaCliente.Contains(pFiltro.ReferenciaCliente));

            if (!string.IsNullOrEmpty(pFiltro.Booking))
                query = query.Where(a => a.Booking.Contains(pFiltro.Booking));

            query = query.Where(a => a.FechaPosicionamiento >= fechaPosIni && a.FechaPosicionamiento <= fechaPosFin);

            if (pFiltro.FechaInicio != null)
                query = query.Where(a => a.FechaInicio >= pFiltro.FechaInicio);

            if (pFiltro.FechaFin != null)
                query = query.Where(a => a.FechaFin <= pFiltro.FechaFin);

            if (pFiltro.Activo != null)
                query = query.Where(a => a.Activo == pFiltro.Activo);

            // ===== Ejecutar =====
            var solicitudes = await query
                .OrderByDescending(a => a.IdSLOSolicitud)
                .ToListAsync();

            // Ya no hace falta filtrar en memoria; el Include viene filtrado
            return solicitudes;
        }



        //public async Task<RespuestaGenericaDTO> ObtenerDetallesSolicitudSinAsignarAsync(int idSLOSolicitud)
        //{
        //    var respuestaGenerica = new RespuestaGenericaDTO 
        //    { 
        //        StatusCode = HttpStatusCode.BadRequest
        //    };




        //    return respuestaGenerica;

        //}

        public async Task<RespuestaGenericaDTO> SLOSolicitudesEditar(int IdSolicitud)
        {
            RespuestaGenericaDTO respuesta = new();
            try
            {
                var solicitud = await _db.SLOSolicitudes
                    .Include(s => s.sloSOlicitudesDetalle)
                    .FirstOrDefaultAsync(s => s.IdSLOSolicitud == IdSolicitud);

                if (solicitud == null)
                {
                    respuesta.IsSuccess = false;
                    respuesta.StatusCode = HttpStatusCode.NotFound;
                    respuesta.lstrErrorMessages = new List<string>
            {
                "No se encontró la solicitud con el ID proporcionado."
            };
                    return respuesta;
                }

                respuesta.IsSuccess = true;
                respuesta.Entidad = solicitud;
                respuesta.StatusCode = HttpStatusCode.OK;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.IsSuccess = false;
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.lstrErrorMessages = new List<string>
        {
            "Error al ejecutar la consulta",
            ex.InnerException?.Message ?? ex.Message
        };
                return respuesta;
            }
        }

        public async Task<RespuestaGenericaDTO> SLOSolicitudesActualizar(int idSolicitud, SLOSolicitudes solicitud)
        {
            RespuestaGenericaDTO respuesta = new();

            try
            {
                var solicitudExistente = await _context.SLOSolicitudes
                    .Include(s => s.sloSOlicitudesDetalle) // Asegúrate de tener esta navegación en tu modelo
                    .FirstOrDefaultAsync(s => s.IdSLOSolicitud == idSolicitud);

                if (solicitudExistente == null)
                {
                    respuesta.IsSuccess = false;
                    respuesta.StatusCode = HttpStatusCode.NotFound;
                    respuesta.lstrErrorMessages = new List<string> { "No se encontró la solicitud con el ID proporcionado." };
                    return respuesta;
                }

                // Actualiza las propiedades de la solicitud principal
                _context.Entry(solicitudExistente).CurrentValues.SetValues(solicitud);

                // Manejo de los detalles: eliminamos los existentes y agregamos los nuevos
                solicitudExistente.sloSOlicitudesDetalle.Clear();

                //if (solicitud.sloSOlicitudesDetalle != null && solicitud.sloSOlicitudesDetalle.Count > 0)
                //{
                //    foreach (var detalle in solicitud.sloSOlicitudesDetalle)
                //    {
                //        _context.Entry(detalle).State = EntityState.Detached;

                //        detalle.IdSLOSolicitud = idSolicitud;
                //        solicitudExistente.sloSOlicitudesDetalle.Add(detalle);
                //    }
                //}

                if (solicitud.sloSOlicitudesDetalle != null && solicitud.sloSOlicitudesDetalle.Count > 0)
                {
                    foreach (var detalle in solicitud.sloSOlicitudesDetalle)
                    {
                        // 👉 Asegurar que EF no intente rastrear otra instancia ya existente
                        _context.Entry(detalle).State = EntityState.Detached;

                        var nuevoDetalle = new SLOSolicitudesDetalle
                        {
                            IdSLOSolicitud = idSolicitud,
                            IdCatMercancia = detalle.IdCatMercancia,
                            Contenedor = detalle.Contenedor,
                            IdCatTipoContenedor = detalle.IdCatTipoContenedor,
                            IdCatTipoEmbalaje = detalle.IdCatTipoEmbalaje,
                            Peso = detalle.Peso,
                            MciaPeligrosa = detalle.MciaPeligrosa,
                            IdCatTipoImo = detalle.IdCatTipoImo,
                            UNN = detalle.UNN,
                            Volumen = detalle.Volumen,
                            NoParte = detalle.NoParte,
                            Cantidad = detalle.Cantidad,
                            Piezas = detalle.Piezas,
                            CantidadTransportes = detalle.CantidadTransportes,
                            CantidadCircuitos = detalle.CantidadCircuitos,
                            Booking = detalle.Booking,
                            DescMercancia = detalle.DescMercancia,
                            Activo = detalle.Activo,
                            FechaRegistro = DateTime.Now
                            // 👇 OJO: NO copies catTipoMercancia, catTipoEmbalajes, etc.
                        };


                        solicitudExistente.sloSOlicitudesDetalle.Add(nuevoDetalle);
                    }
                }

                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    respuesta.IsSuccess = true;
                    respuesta.StatusCode = HttpStatusCode.OK;
                    respuesta.Entidad = solicitudExistente;
                    return respuesta;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuesta.lstrErrorMessages = new List<string>
                    {
                        "Error al actualizar la entidad"
                    };
                    return respuesta;
                }

            }
            catch (Exception ex)
            {
                respuesta.IsSuccess = false;
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.lstrErrorMessages = new List<string>
        {
            "Ocurrió un error al actualizar la solicitud.",
            ex.InnerException?.Message ?? ex.Message
        };
                return respuesta;
            }
        }

        public async Task<RespuestaGenericaDTO> SLOSolicitudesAlta(int IdSolicitud)
        {
            var respuesta = new RespuestaGenericaDTO();

            try
            {
                var solicitud = await _db.SLOSolicitudes.FindAsync(IdSolicitud);

                if (solicitud == null)
                {
                    respuesta.StatusCode = HttpStatusCode.NotFound;
                    respuesta.lstrErrorMessages = new List<string> { "No se encontró la solicitud." };
                    return respuesta;
                }

                solicitud.Activo = true;
                await _db.SaveChangesAsync();

                respuesta.IsSuccess = true;
                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.strMensaje = "Solicitud activada correctamente.";
                respuesta.Entidad = solicitud;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.lstrErrorMessages = new List<string>
        {
            "Error al activar la solicitud.",
            ex.InnerException?.Message ?? ex.Message
        };
                return respuesta;
            }
        }

        public async Task<RespuestaGenericaDTO> SLOSolicitudesBaja(int IdSolicitud)
        {
            var respuesta = new RespuestaGenericaDTO();

            try
            {
                var solicitud = await _db.SLOSolicitudes.FindAsync(IdSolicitud);

                if (solicitud == null)
                {
                    respuesta.StatusCode = HttpStatusCode.NotFound;
                    respuesta.lstrErrorMessages = new List<string> { "No se encontró la solicitud" };
                    return respuesta;
                }
                solicitud.Activo = false;
                await _db.SaveChangesAsync();

                respuesta.IsSuccess = true;
                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.strMensaje = "Solicitud desactivada correctamente";
                respuesta.Entidad = solicitud;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.lstrErrorMessages = new List<string>
                {
                    "Error al desactivar la solicitud",
                    ex.InnerException.Message
                };
            }
            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> SLOSolicitudesDetalleObtener(int idSLOSolicitud)
        {
            RespuestaGenericaDTO respuesta = new();
            List<SLOSolicitudesDetalle> lstDetalles = new();

            try
            {
                lstDetalles = await _db.sloSolicitudesDetalle
                .Include(d => d.catTipoMercancia)
                .Include(d => d.catTipoIMO)
                .Where(d => d.IdSLOSolicitud == idSLOSolicitud && d.Activo)
                .ToListAsync();

                respuesta.IsSuccess = true;
                respuesta.strMensaje = "Consulta Exitosa";
                respuesta.Entidades = lstDetalles.Cast<object>().ToList();
                respuesta.StatusCode = HttpStatusCode.OK;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.IsSuccess = false;
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.lstrErrorMessages = new List<string>
                {
                    "Error al ejecutar la consulta",
                    ex.InnerException?.Message ?? ex.Message
                };
                return respuesta;
            }
        }
    }
}
