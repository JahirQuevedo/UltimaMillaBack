using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G;

namespace ALOG.Repositorios.Repositorio.Integración1G
{
    public class IntegraFacturaDetRepo : GenericoRepositorio<IntegraFacturaDet>, IIntegraFacturaDetRepo
    {
        private readonly ApplicationDbContext _db;
        public IntegraFacturaDetRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IntegraFacturaDet obtenerFacturasDet1GPorId(int IdIntegraReferencia)
        {
            throw new NotImplementedException();
        }

        public ICollection<IntegraFacturaDet> obtenerFascturasDet1G(FiltroIntegraFacturas1GDTO pFiltro)
        {
            throw new NotImplementedException();
        }
    }
}
