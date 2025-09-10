using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface IReporteadorTarjaRepositorio
{
    ResultBase<DocumentoBase> ObtieneReporteTarjaPatioExterno(ConsultaTarjaReporteador datosReporteador);
}
