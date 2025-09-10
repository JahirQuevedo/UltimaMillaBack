using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.Integración1G
{
    public class IntegraFacturaEstRepo : GenericoRepositorio<IntegraFacturaEst>, IIntegraFacturaEstRepo
    {
        private readonly ApplicationDbContext _db;
        public IntegraFacturaEstRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IntegraFacturaEst obtenerFacturasEst1GPorId(int IdIntegraFacturaEst)
        {
            try
            {
                return _db.integracionFacturaEst.Include(a => a.integracionFacturaEnc).FirstOrDefault(x => x.IdIntFacturaEst == IdIntegraFacturaEst);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public ICollection<IntegraFacturaEst> obtenerFascturasEst1G(FiltroIntegraFacturas1GDTO pFiltro)
        {
            try
            {
                pFiltro.IdIntFacturaEnc = pFiltro.IdIntFacturaEnc == null ? 0 : pFiltro.IdIntFacturaEnc;
                pFiltro.IdIntFacturaEst = pFiltro.IdIntFacturaEst == null ? 0 : pFiltro.IdIntFacturaEst;


                var lstobj = _db.integracionFacturaEst.Include(a => a.integracionFacturaEnc)
                    .Where(a => (pFiltro.IdIntFacturaEnc <= 0 || a.IdIntFacturaEnc == pFiltro.IdIntFacturaEnc) &&
                                (pFiltro.IdIntFacturaEst <= 0 || a.IdIntFacturaEst == pFiltro.IdIntFacturaEst) &&

                                (string.IsNullOrEmpty(pFiltro.FolioFactura) || a.FolioFactura == pFiltro.FolioFactura) &&
                                (string.IsNullOrEmpty(pFiltro.IdSolicitudFacturacion) || a.IdSolicitudFacturacion == pFiltro.IdSolicitudFacturacion) &&
                                (string.IsNullOrEmpty(pFiltro.EstatusFactura) || a.EstatusFactura == pFiltro.EstatusFactura) &&
                                 (string.IsNullOrEmpty(pFiltro.EstatusSolicitud) || a.EstatusSolicitud == pFiltro.EstatusSolicitud)

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
