using System.Data;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Logistica;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Repositorio.SPFunciones;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.Logistica
{
    public class PeticionesReferenciasSLORepo : IPeticionesReferenciasSLORepo
    {
        private readonly ApplicationDbContext _context;
        private DbSPFuncionesRepositorio _dbSpFunciones;
        //private IGenericoRepositorio<SLOPeticionesReferencias> _ctgenericoRepositorio;
        private IGenericoRepositorio<Ordenes> _ctgenericoRepositorio;

        public PeticionesReferenciasSLORepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ordenes>> obtenerReferencias(FiltroOrdenesReferenciasDTO filtro)
        {
            var query = _context.ordenes
                .Include(r => r.catClientes)
                .Include(r => r.SLOintegracionReferencias)
                    .ThenInclude(s => s.SLOintegracionFacturaEnc)
                        .ThenInclude(t => t.SLOintegracionFacturaDet)
                .Include(r => r.SLOpeticionesReferencias)
                    .ThenInclude(s => s.SLOpeticionesContenedores)
                        .ThenInclude(t => t.SLOpeticionesServicios)
                            .ThenInclude(u => u.catClientesFacturarA)
                .Include(r => r.SLOpeticionesReferencias)
                    .ThenInclude(s => s.SLOpeticionesContenedores)
                        .ThenInclude(t => t.SLOpeticionesServicios)
                            .ThenInclude(u => u.catServicios)
                .Include(r => r.catAduana)
                .AsQueryable();

            // Filtro por defecto de Línea de Negocio 3
            query = query.Where(r => r.IdCatLineaNegocio == 3);
            var s = "__";
            query = query.Where(r => (r.ReferenciaALO != null && !r.ReferenciaALO.Equals(s)));

            if (filtro.IdCliente.HasValue)
            {
                query = query.Where(r => r.IdCatCliente == filtro.IdCliente.Value);
            }

            if (filtro.IdClienteFact.HasValue)
            {
                var idClienteFactStr = filtro.IdClienteFact.Value.ToString();
                query = query.Where(r => r.SLOintegracionReferencias.Any(c => c.ClaveClienteExterno == idClienteFactStr));
            }

            if (!string.IsNullOrWhiteSpace(filtro.ReferenciaALO))
            {
                query = query.Where(r => r.ReferenciaALO.Contains(filtro.ReferenciaALO));
            }

            if (!string.IsNullOrWhiteSpace(filtro.ReferenciaCliente))
            {
                query = query.Where(r => r.SLOintegracionReferencias.Any(c => c.ReferenciaClienteExterno.Contains(filtro.ReferenciaCliente)));
            }

            if (filtro.IdAduana.HasValue)
            {
                query = query.Where(r => r.IdCatAduana == filtro.IdAduana);
            }

            if (filtro.FechaRegistro.HasValue)
            {
                query = query.Where(r => r.FechaRegistro.Date == filtro.FechaRegistro.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Estado1G))
            {
                query = query.Where(r => r.SLOintegracionReferencias
                    .Any(i => i.Estado1G.Contains(filtro.Estado1G)));
            }

            if (filtro.FechaEnvio1G.HasValue)
            {
                query = query.Where(r => r.SLOintegracionReferencias
                    .Any(i => i.FechaEnvio.HasValue && i.FechaEnvio.Value.Date == filtro.FechaEnvio1G.Value.Date));
            }

            var a = await query.ToListAsync();

            return a.OrderByDescending(o => o.IdOrden);
        }

        public async Task<IEnumerable<SLOIntegraFacturaEnc>> ObtenerFacturasPorOrden(int idOrden)
        {
            return await _context.SLOintegracionFacturaEnc
                .Where(f => f.Activo && f.IdOrden == idOrden)
                .ToListAsync();
        }

        //public async Task<IEnumerable<SLOPeticionesContenedores>> obtenerServicios(int idOrden)
        //{
        //    var contenedores = await _context.SLOpeticionesContenedores
        //    .Where(c => c.SLOpeticionesReferencias.IdOrden == idOrden && c.Activo)
        //    .Include(c => c.SLOpeticionesReferencias)
        //        .ThenInclude(s => s.ordenes)
        //    .Include(c => c.SLOpeticionesServicios.Where(s => s.Activo))
        //        .ThenInclude(s => s.catServicios)
        //    .Include(c => c.SLOpeticionesServicios.Where(s => s.Activo))
        //        .ThenInclude(s => s.catClientesFacturarA)
        //    .ToListAsync();

        //    return contenedores;
        //}

        public async Task<Ordenes> obtenerServicios(int idOrden)
        {
            var orden = await _context.ordenes
                .Include(r => r.catClientes)
                .Include(r => r.SLOintegracionReferencias)
                    .ThenInclude(s => s.SLOintegracionFacturaEnc)
                        .ThenInclude(t => t.SLOintegracionFacturaDet)
                .Include(r => r.SLOpeticionesReferencias)
                    .ThenInclude(s => s.SLOpeticionesContenedores)
                        .ThenInclude(t => t.SLOpeticionesServicios)
                            .ThenInclude(u => u.catClientesFacturarA)
                .Include(r => r.SLOpeticionesReferencias)
                    .ThenInclude(s => s.SLOpeticionesContenedores)
                        .ThenInclude(t => t.SLOpeticionesServicios)
                            .ThenInclude(u => u.catServicios)
                .Include(r => r.catAduana)
                .FirstOrDefaultAsync(o => o.IdOrden == idOrden);

            return orden;
        }


        public async Task<RespuestaGenericaDTO> ActualizarReferenciaCliente(Ordenes orden)
        {
            try
            {
                if (orden == null || orden.IdOrden <= 0 || orden.IdCatCliente <= 0)
                {
                    return new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = "Datos de orden incompletos o inválidos."
                    };
                }

                // Verificar si existe la orden
                var ordenDb = await _context.ordenes
                    .Include(o => o.SLOintegracionReferencias)
                    .FirstOrDefaultAsync(o => o.IdOrden == orden.IdOrden);

                if (ordenDb == null)
                {
                    return new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = "No se encontró la orden."
                    };
                }

                // Validar que ninguna referencia esté marcada como Enviada
                var hayReferenciasEnviadas = ordenDb.SLOintegracionReferencias
                    .Any(refe => refe.Enviado);

                if (hayReferenciasEnviadas)
                {
                    return new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = "No se puede actualizar. Existen referencias ya enviadas."
                    };
                }

                // Actualizar el IdCatCliente
                ordenDb.IdCatCliente = orden.IdCatCliente;
                _context.ordenes.Update(ordenDb);
                await _context.SaveChangesAsync();

                return new RespuestaGenericaDTO
                {
                    IsSuccess = true,
                    strMensaje = "El cliente de la orden fue actualizado correctamente."
                };
            }
            catch (Exception ex)
            {
                return new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    strMensaje = "Error al actualizar el cliente de la orden."
                };
            }
        }
        

        public async Task<RespuestaGenericaDTO> crearReferencia(SLOReferenciasDTO dto)
        {
            _dbSpFunciones = new DbSPFuncionesRepositorio(_context);

            return await _dbSpFunciones.crearReferenciaALO(dto);
        }

        public async Task<RespuestaGenericaDTO> GuardarFacturacionCompleta(List<SLOPeticionesContenedores> contenedores)
        {
            _dbSpFunciones = new DbSPFuncionesRepositorio(_context);
            return await _dbSpFunciones.crearServicios(contenedores);

        }

        public async Task<RespuestaGenericaDTO> ActualizarServicioAsync(SLOPeticionesServicios servicioActualizado)
        {
            var respuesta = new RespuestaGenericaDTO();

            var servicioExistente = await _context.SLOpeticionesServicios
                .Where(s => s.IdServicio == servicioActualizado.IdServicio && s.Activo)
                .FirstOrDefaultAsync();

            if (servicioExistente != null)
            {
                // Comparar y actualizar solo si hubo cambio

                if (servicioExistente.Cantidad != servicioActualizado.Cantidad)
                {
                    servicioExistente.Cantidad = servicioActualizado.Cantidad ?? 0;
                    _context.Entry(servicioExistente).Property(x => x.Cantidad).IsModified = true;
                }

                if (servicioExistente.Monto != servicioActualizado.Monto)
                {
                    servicioExistente.Monto = servicioActualizado.Monto ?? 0;
                    _context.Entry(servicioExistente).Property(x => x.Monto).IsModified = true;
                }

                if (servicioExistente.ReferenciaClienteFactura != servicioActualizado.ReferenciaClienteFactura)
                {
                    servicioExistente.ReferenciaClienteFactura = servicioActualizado.ReferenciaClienteFactura;
                    _context.Entry(servicioExistente).Property(x => x.ReferenciaClienteFactura).IsModified = true;
                }

                if (servicioExistente.IdClienteFacturarA != servicioActualizado.IdClienteFacturarA)
                {
                    servicioExistente.IdClienteFacturarA = servicioActualizado.IdClienteFacturarA ?? 0;
                    _context.Entry(servicioExistente).Property(x => x.IdClienteFacturarA).IsModified = true;
                }

                await _context.SaveChangesAsync();

                respuesta.IsSuccess = true;
                respuesta.strMensaje = "Servicio actualizado correctamente.";
            }
            else
            {
                respuesta.strMensaje = "No se encontró el servicio para actualizar.";
            }

            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> EliminarServicioAsync(int idServicio)
        {
            var respuesta = new RespuestaGenericaDTO();

            var servicio = await _context.SLOpeticionesServicios
                .Where(s => s.IdServicio == idServicio && s.Activo)
                .FirstOrDefaultAsync();

            if (servicio != null)
            {
                servicio.Activo = false;
                _context.Entry(servicio).Property(x => x.Activo).IsModified = true;

                await _context.SaveChangesAsync();

                respuesta.IsSuccess = true;
                respuesta.strMensaje = "Servicio eliminado correctamente.";
            }
            else
            {
                respuesta.strMensaje = "No se encontró el servicio para eliminar.";
            }

            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> envioFacturacion(Ordenes servicios)
        {
            _dbSpFunciones = new DbSPFuncionesRepositorio(_context);
            return await _dbSpFunciones.EnvioaFacturacion(servicios);

        }
    }
}
