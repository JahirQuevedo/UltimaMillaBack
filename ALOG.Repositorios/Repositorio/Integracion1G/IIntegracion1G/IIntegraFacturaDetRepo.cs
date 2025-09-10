using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G
{
    public interface IIntegraFacturaDetRepo : IGenericoRepositorio<IntegraFacturaDet>
    {
        ICollection<IntegraFacturaDet> obtenerFascturasDet1G(FiltroIntegraFacturas1GDTO pFiltro);

        IntegraFacturaDet obtenerFacturasDet1GPorId(int IdIntegraReferencia);
    }
}
