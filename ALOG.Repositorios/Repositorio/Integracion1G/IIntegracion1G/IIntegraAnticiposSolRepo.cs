using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G
{
    public interface IIntegraAnticiposSolRepo : IGenericoRepositorio<IntegraAnticipoSol>
    {
        ICollection<IntegraAnticipoSol> obtenerAnticiposSol(FiltroAnticipos1GDTO pFiltro);

        IntegraAnticipoSol obtenerAnticipoSol(int IdAcarreo);

    }
}
