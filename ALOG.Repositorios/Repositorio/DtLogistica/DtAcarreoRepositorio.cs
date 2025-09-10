using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica;
using ALOG.Repositorios.Repositorio.Generico;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.DtLogistica
{
    public class DtAcarreoRepositorio : GenericoRepositorio<DtAcarreos>, IDtAcarreoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public DtAcarreoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public DtAcarreos obtenerAcarreo(int IdAcarreo)
        {
            try
            {
                return _db.dtAcarreos
                    .Include(x => x.catTipoEstados)
                    .Include(x => x.catClientes)
                    .Include(x => x.catEmpresa)
                    .Include(x => x.catProveedores)
                    .Include(x => x.catServicios)
                    .Include(x => x.catUsuario)
                    .FirstOrDefault(x => x.IdDtAcarreos == IdAcarreo);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<ICollection<DtAcarreos>> obtenerAcarreos(FiltroDtAcarreosDTO pFiltro)
        {
            pFiltro.IdCliente = pFiltro.IdCliente == null ? 0 : pFiltro.IdCliente;
            pFiltro.IdEmpresa = pFiltro.IdEmpresa == null ? 0 : pFiltro.IdEmpresa;
            pFiltro.IdCatProveedor = pFiltro.IdCatProveedor == null ? 0 : pFiltro.IdCatProveedor;
            pFiltro.IdOrden = pFiltro.IdOrden == null ? 0 : pFiltro.IdOrden;
            pFiltro.IdCatTipoEstados = pFiltro.IdCatTipoEstados == null ? 0 : pFiltro.IdCatTipoEstados;
            pFiltro.Activo = pFiltro.Activo == null ? true : pFiltro.Activo;
            pFiltro.FSolicitudIni = (pFiltro.FSolicitudIni == null || pFiltro.FSolicitudIni == DateTime.MinValue) ? DateTime.Now.Date.AddDays(-7) : pFiltro.FSolicitudIni;
            pFiltro.FSolicitudFin = (pFiltro.FSolicitudFin == null || pFiltro.FSolicitudFin == DateTime.MinValue) ? DateTime.Now.Date : pFiltro.FSolicitudFin;


            var lstAcarreos = await _db.dtAcarreos
                .Include(a => a.catEmpresa)
                .Include(b => b.catClientes)
                .Include(c => c.catServicios)
                .Include(d => d.ordenes)
                .Include(e => e.catTipoEstados)
                .Include(f => f.catUsuario)
            .Where(a => (pFiltro.IdCliente <= 0 || a.IdCliente == pFiltro.IdCliente) &&
                        (pFiltro.IdEmpresa <= 0 || a.IdEmpresa == pFiltro.IdEmpresa) &&
                        (pFiltro.IdCatProveedor <= 0 || a.IdCatProveedor == pFiltro.IdCatProveedor) &&
                        (pFiltro.IdOrden <= 0 || a.IdOrden == pFiltro.IdOrden) &&
                        (pFiltro.IdCatTipoEstados <= 0 || a.catTipoEstados.IdCatTipoEstados == pFiltro.IdCatTipoEstados) &&
                        (pFiltro.Activo == a.Activo) &&
                        (a.FechaRegistro >= pFiltro.FSolicitudIni && a.FechaRegistro <= pFiltro.FSolicitudFin) &&
                        (string.IsNullOrEmpty(pFiltro.Contenedor) || a.Contenedor.Contains(pFiltro.Contenedor)) &&
                         (string.IsNullOrEmpty(pFiltro.Servicio) || a.catServicios.Descripcion.Contains(pFiltro.Servicio)) &&
                         (string.IsNullOrEmpty(pFiltro.Cliente) || a.catClientes.RazonSocial.Contains(pFiltro.Cliente))
        ).OrderByDescending(e => e.IdDtAcarreos).ToListAsync();


            //var lstAcarreos = await lstAcarreos.Skip((pFiltro.NumeroPagina - 1) * pFiltro.NumeroRegistros)
            //                             .Take(pFiltro.NumeroRegistros)
            //                             .ToListAsync();
            return lstAcarreos;

        }

        public async Task<bool> CambioEstado(int Id, int idCatTipoEstado)
        {
            try
            {
                //throw new NotImplementedException();
                var parentEntity = await _db.dtAcarreos.FirstOrDefaultAsync(x => x.IdDtAcarreos == Id);

                if (parentEntity == null)
                {
                    return false;
                }

                parentEntity.IdCatTipoEstado = idCatTipoEstado;
                _db.Entry(parentEntity).Property(e => e.IdCatTipoEstado).IsModified = true;


                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {

                return false;

            }

        }
    }
}
