using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G
{
    public interface IIntegraFacturaEstRepo : IGenericoRepositorio<IntegraFacturaEst>
    {
        ICollection<IntegraFacturaEst> obtenerFascturasEst1G(FiltroIntegraFacturas1GDTO pFiltro);

        IntegraFacturaEst obtenerFacturasEst1GPorId(int IdIntegraFactura);
    }
}
