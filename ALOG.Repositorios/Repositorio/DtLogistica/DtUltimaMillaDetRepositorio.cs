using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica;
using ALOG.Repositorios.Repositorio.Generico;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.DtLogistica
{
    public class DtUltimaMillaDetRepositorio : GenericoRepositorio<DtUltimaMillaDet>, IDtUltimaMillaDetRepositorio
    {

        private readonly ApplicationDbContext _db;
        public DtUltimaMillaDetRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public async Task<ICollection<DtUltimaMillaDet>> ObtenerDetalles(int idUltimaMillaEncabezado)
        {
            return _db.dtUltimaMillaDet.Where(d => d.IdUltimaMilla == idUltimaMillaEncabezado).ToArray();
        }

        public DtUltimaMillaDet obtenerUltimaMillaDet(int IdUltimaMilla)
        {
            try
            {
                return _db.dtUltimaMillaDet.FirstOrDefault(x => x.IdDtUltimaMillaDet == IdUltimaMilla);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public ICollection<DtUltimaMillaDet> obtenerUltimaMillaDets(FiltroDtUltimaMillaDTO pFiltro)
        {
            pFiltro.IdCliente = pFiltro.IdCliente == null ? 0 : pFiltro.IdCliente;
            pFiltro.IdDtUltMillaEnc = pFiltro.IdDtUltMillaEnc == null ? 0 : pFiltro.IdDtUltMillaEnc;
            pFiltro.IdCatEmpresa = pFiltro.IdCatEmpresa == null ? 0 : pFiltro.IdCatEmpresa;
            pFiltro.IdDtUltimaMillaDet = pFiltro.IdDtUltimaMillaDet == null ? 0 : pFiltro.IdDtUltimaMillaDet;
            pFiltro.IdOrden = pFiltro.IdOrden == null ? 0 : pFiltro.IdOrden;
            pFiltro.IdTipoEstado = pFiltro.IdTipoEstado == null ? 0 : pFiltro.IdTipoEstado;
            pFiltro.Piezas = pFiltro.Piezas == null ? 0 : pFiltro.Piezas;
            pFiltro.Pallet = pFiltro.Pallet == null ? 0 : pFiltro.Pallet;




            var lstAcarreos = _db.dtUltimaMillaDet.Include(a => a.dtUltimaMillaEnc)
                .Where(a => (pFiltro.IdCliente <= 0 || a.dtUltimaMillaEnc.IdCliente == pFiltro.IdCliente) &&
                            (pFiltro.IdCatEmpresa <= 0 || a.dtUltimaMillaEnc.IdCatEmpresa == pFiltro.IdCatEmpresa) &&
                            (pFiltro.IdOrden <= 0 || a.dtUltimaMillaEnc.IdOrden == pFiltro.IdOrden) &&
                            (pFiltro.IdDtUltMillaEnc <= 0 || a.dtUltimaMillaEnc.IdDtUltMillaEnc == pFiltro.IdDtUltMillaEnc) &&
                            (pFiltro.IdTipoEstado <= 0 || a.dtUltimaMillaEnc.IdTipoEstado == pFiltro.IdTipoEstado) &&
                            (pFiltro.Piezas <= 0 || a.Piezas == pFiltro.Piezas) &&
                            (pFiltro.Pallet <= 0 || a.Pallet == pFiltro.Pallet) &&


                            (string.IsNullOrEmpty(pFiltro.NumeroParte) || a.NumeroParte.Equals(pFiltro.NumeroParte)) &&
                            (string.IsNullOrEmpty(pFiltro.FacturaCliente) || a.dtUltimaMillaEnc.FacturaCliente.Equals(pFiltro.FacturaCliente)))

                .ToList();
            return lstAcarreos;

        }
    }
}
