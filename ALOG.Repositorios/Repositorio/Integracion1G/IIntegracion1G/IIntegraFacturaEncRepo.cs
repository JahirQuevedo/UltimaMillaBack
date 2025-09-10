using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G
{
    public interface IIntegraFacturaEncRepo : IGenericoRepositorio<IntegraFacturaEnc>
    {
        ICollection<IntegraFacturaEnc> obtenerFascturasEnc1G(FiltroIntegraFacturas1GDTO pFiltro);

        IntegraFacturaEnc obtenerFacturasEnc1GPorId(int IdIntegraReferencia);
    }
}
