using ALOG.Modelos;
using ALOG.Modelos.ModelosWMS.Reportes.Salidas;

namespace ALOG.Repositorios;

public interface IReporteadorLiberacionRepositorio
{

    ResultBase<DocumentoBase> ObtieneReporteTarjaSalidaLiberacion(ConsultaLiberacionReporteador datosReporteador);

}
