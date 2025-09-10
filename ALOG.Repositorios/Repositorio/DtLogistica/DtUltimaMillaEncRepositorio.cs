using System.Data;
using System.Net;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Logistica;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.DtLogistica
{
    public class DtUltimaMillaEncRepositorio : GenericoRepositorio<DtUltimaMillaEnc>, IDtUltimaMillaEncRepositorio
    {

        private readonly ApplicationDbContext _db;
        public DtUltimaMillaEncRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public async Task<RespuestaGenericaDTO> CrearReferenciaALO(DtUltimaMillaEnc encabezado)
        {
            RespuestaGenericaDTO respuestaGenericaDTO = new RespuestaGenericaDTO();
            Ordenes nueva_orden = new Ordenes();
            OrdenesReferencia orden_referencia = new OrdenesReferencia(_db);

            nueva_orden.IdCatCliente = (int)encabezado.IdCliente;
            nueva_orden.IdCatLineaNegocio = 5;
            nueva_orden.IdCatSistema = 1;
            nueva_orden.IdCatAduana = encabezado.IdCatAduana;
            nueva_orden.IdCatEmpresa = 1;
            nueva_orden.IdCatSucursal = 1;
            nueva_orden.IdUsuario = 40;
            nueva_orden.IdEstadoOrden = 1;
            nueva_orden.catProyectos = null;

            #region Crear Orden con Referencia ALO

            try {
               var orden_creada = await orden_referencia.CrearOrden(nueva_orden);
                

                
                respuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
                return respuestaGenericaDTO;
            }
            catch
            {
                respuestaGenericaDTO.StatusCode = HttpStatusCode.BadGateway;
                respuestaGenericaDTO.strMensaje = "Error al generar la Respuesta Generica ALO";
                return respuestaGenericaDTO;
            }
            #endregion
        }

        public async Task<DtUltimaMillaEnc> obtenerUltimaMillaEnc(int IdUltimaMilla)
        {
            try
            {
                return await _db.dtUltimaMillaEnc
                 .Include(a => a.DtUltimaMillaDets)
                .Include(b => b.catClientes)
                .Include(c => c.ordenes)
                .Include(d => d.catServicios)
                .Include(d => d.catTipoEstados)
                .Include(d => d.catProveedores)
                    .FirstOrDefaultAsync(x => x.IdDtUltMillaEnc == IdUltimaMilla);
            }
            catch (Exception)
            {

                return null;
            }

        }
        public async Task<bool> CambioEstado(int Id, int idCatTipoEstado)
        {
            try
            {
                //throw new NotImplementedException();
                var parentEntity = await _db.dtUltimaMillaEnc.FirstOrDefaultAsync(x => x.IdDtUltMillaEnc == Id);

                if (parentEntity == null)
                {
                    return false;
                }

                parentEntity.IdTipoEstado = idCatTipoEstado;
                parentEntity.FechaSalida = DateTime.Now;
                _db.Entry(parentEntity).Property(e => e.IdTipoEstado).IsModified = true;


                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {

                return false;

            }

        }

        public async Task<DtUltimaMillaEnc> Actualizar(DtUltimaMillaEnc entity)
        {
            if (entity != null)
            {
                var parentEntity = _db.dtUltimaMillaEnc.FirstOrDefault(x => x.IdDtUltMillaEnc == entity.IdDtUltMillaEnc);
                if (parentEntity != null)
                {
                    _db.Entry(parentEntity).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                    return parentEntity;


                }
                //}else
                //{
                //    _db.dtUltimaMillaEnc.Update(entity);
                //}


            }
            return null;
        }

        public async Task<ICollection<DtUltimaMillaEnc>> obtenerUltimaMillaEncs(FiltroDtUltimaMillaDTO pFiltro)
        {
            pFiltro.IdCliente = pFiltro.IdCliente ?? 0;
            pFiltro.IdDtUltMillaEnc = pFiltro.IdDtUltMillaEnc ?? 0;
            pFiltro.IdCatEmpresa = pFiltro.IdCatEmpresa ?? 0;
            pFiltro.IdCatProveedor = pFiltro.IdCatProveedor ?? 0;
            pFiltro.IdDtUltimaMillaDet = pFiltro.IdDtUltimaMillaDet ?? 0;
            pFiltro.IdOrden = pFiltro.IdOrden ?? 0;
            pFiltro.IdTipoEstado = pFiltro.IdTipoEstado ?? 0;
            pFiltro.Piezas = pFiltro.Piezas ?? 0;
            pFiltro.Pallet = pFiltro.Pallet ?? 0;
            pFiltro.Viaje = pFiltro.Viaje ?? 0;
            pFiltro.FechaSolicitudIni = (pFiltro.FechaSolicitudIni == null || pFiltro.FechaSolicitudIni == DateTime.MinValue) ? DateTime.Now.Date.AddDays(-7) : pFiltro.FechaSolicitudIni;
            pFiltro.FechaSolicitudFin = (pFiltro.FechaSolicitudFin == null || pFiltro.FechaSolicitudFin == DateTime.MinValue) ? DateTime.Now.Date : pFiltro.FechaSolicitudFin;
            //pFiltro.FechaSalidaIni = (pFiltro.FechaSalidaIni == null || pFiltro.FechaSalidaIni == DateTime.MinValue) ? DateTime.Now.Date.AddDays(-7) : pFiltro.FechaSalidaIni;
            //pFiltro.FechaSalidaIni = (pFiltro.FechaSalidaIni == null || pFiltro.FechaSalidaIni == DateTime.MinValue) ? DateTime.Now.Date.AddDays(-7) : pFiltro.FechaSalidaIni;
            pFiltro.Activo = pFiltro.Activo ?? true;


            var query = await _db.dtUltimaMillaEnc.Include(a => a.DtUltimaMillaDets)
                .Include(b => b.catClientes)
                .Include(c => c.ordenes)
                    .ThenInclude(a => a.catAduana)
                .Include(d => d.catServicios)
                .Include(d => d.catTipoEstados)
                .Include(d => d.catTipoTransporte)                    
            .Where(a => (pFiltro.IdCliente <= 0 || a.IdCliente == pFiltro.IdCliente) &&
                        (pFiltro.IdCatEmpresa <= 0 || a.IdCatEmpresa == pFiltro.IdCatEmpresa) &&
                        (pFiltro.IdCatProveedor <= 0 || a.IdCatProveedor == pFiltro.IdCatProveedor) &&
                        (pFiltro.IdOrden <= 0 || a.IdOrden == pFiltro.IdOrden) &&
                        (pFiltro.IdDtUltMillaEnc <= 0 || a.IdDtUltMillaEnc == pFiltro.IdDtUltMillaEnc) &&
                        (pFiltro.IdTipoEstado <= 0 || a.IdTipoEstado == pFiltro.IdTipoEstado) &&
                        //(a.FechaSolicitud >= pFiltro.FechaSolicitudIni && a.FechaSolicitud <= pFiltro.FechaSolicitudFin) &&
                        (pFiltro.Piezas <= 0 || a.DtUltimaMillaDets.Any(a => a.Piezas == pFiltro.Piezas)) &&
                        (pFiltro.Viaje <= 0 || a.Viaje == pFiltro.Viaje) &&
                        (pFiltro.Activo == a.Activo) &&
                        (pFiltro.Pallet <= 0 || a.DtUltimaMillaDets.Any(a => a.Pallet == pFiltro.Pallet)) &&
                        (string.IsNullOrEmpty(pFiltro.NumeroParte) || a.DtUltimaMillaDets.Any(a => a.NumeroParte.Equals(pFiltro.NumeroParte))) &&
                        (string.IsNullOrEmpty(pFiltro.FacturaCliente) || a.FacturaCliente.Equals(pFiltro.FacturaCliente))
            ).OrderByDescending(e => e.IdDtUltMillaEnc).ToListAsync();


            /*var lstUltMilla = await query.Skip((pFiltro.NumeroPagina - 1) * pFiltro.NumeroRegistros)
                                         .Take(pFiltro.NumeroRegistros)
                                         .ToListAsync();*/

            return query;

        }
    }
}
