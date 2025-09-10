using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.Integración1G
{
    public class IntegraFacturaEncRepo : GenericoRepositorio<IntegraFacturaEnc>, IIntegraFacturaEncRepo
    {
        private readonly ApplicationDbContext _db;
        public IntegraFacturaEncRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IntegraFacturaEnc obtenerFacturasEnc1GPorId(int IdFacturaEnc)
        {
            try
            {
                return _db.integracionFacturaEnc.Include(a => a.integraFacturaDet).Include(a => a.integraFacturaEst).FirstOrDefault(x => x.IdIntFacturaEnc == IdFacturaEnc);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public ICollection<IntegraFacturaEnc> obtenerFascturasEnc1G(FiltroIntegraFacturas1GDTO pFiltro)
        {
            try
            {
                pFiltro.IdIntFacturaEnc = pFiltro.IdIntFacturaEnc == null ? 0 : pFiltro.IdIntFacturaEnc;
                pFiltro.IdIntFacturaDet = pFiltro.IdIntFacturaDet == null ? 0 : pFiltro.IdIntFacturaDet;
                pFiltro.IdOrden = pFiltro.IdOrden == null ? 0 : pFiltro.IdOrden;

                var lstobj = _db.integracionFacturaEnc.Include(a => a.integraFacturaDet).Include(b => b.integraFacturaEst)
                    .Where(a => (pFiltro.IdIntFacturaEnc <= 0 || a.IdIntFacturaEnc == pFiltro.IdIntFacturaEnc) &&
                                (pFiltro.IdIntFacturaDet <= 0 || a.integraFacturaDet.Any(a => a.IdIntFacturaDet == pFiltro.IdIntFacturaDet)) &&
                                (pFiltro.IdOrden <= 0 || a.IdOrden == pFiltro.IdOrden) &&
                                (a.Enviado == pFiltro.Enviado) &&
                                (a.CierreReferencia == pFiltro.CierreReferencia) &&
                                (a.Enviado == pFiltro.Enviado) &&

                                (string.IsNullOrEmpty(pFiltro.IdCompaniaExterna) || a.IdCompaniaExterna == pFiltro.IdCompaniaExterna) &&
                                (string.IsNullOrEmpty(pFiltro.IdSolicitudFacturacion) || a.IdSolicitudFacturacion == pFiltro.IdSolicitudFacturacion) &&
                                (string.IsNullOrEmpty(pFiltro.ClaveClienteExterno) || a.ClaveClienteExterno == pFiltro.ClaveClienteExterno) &&
                                 (string.IsNullOrEmpty(pFiltro.ConceptoFacturacion) || a.ConceptoFacturacion == pFiltro.ConceptoFacturacion) &&
                                 (string.IsNullOrEmpty(pFiltro.Referencia) || a.Referencia == pFiltro.Referencia) &&
                                 (string.IsNullOrEmpty(pFiltro.ClaveSATMoneda) || a.ClaveSATMoneda == pFiltro.ClaveSATMoneda) &&
                                 (string.IsNullOrEmpty(pFiltro.ClaveSATUsoCFDI) || a.ClaveSATUsoCFDI == pFiltro.ClaveSATUsoCFDI) &&
                                 (string.IsNullOrEmpty(pFiltro.Contenedor) || a.integraFacturaDet.Any(a => a.Contenedor == pFiltro.Contenedor)) &&
                                 (string.IsNullOrEmpty(pFiltro.CentroCostos) || a.integraFacturaDet.Any(a => a.CentroCostos == pFiltro.CentroCostos)) &&
                                 (string.IsNullOrEmpty(pFiltro.EIR) || a.integraFacturaDet.Any(a => a.EIR == pFiltro.EIR)) &&
                                 (string.IsNullOrEmpty(pFiltro.ClaveServicio) || a.integraFacturaDet.Any(a => a.ClaveServicio == pFiltro.ClaveServicio))
                                 )

                    .ToList();
                return lstobj;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
