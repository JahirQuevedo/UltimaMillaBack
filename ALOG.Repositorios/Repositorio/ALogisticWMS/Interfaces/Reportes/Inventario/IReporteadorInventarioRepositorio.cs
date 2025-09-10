using System;
using ALOG.Modelos;


namespace ALOG.Repositorios;

public interface IReporteadorInventarioRepositorio
{

    ResultBase<DocumentoBase> ObtieneReporteDescargaInventario(ConsultaMonitorInventario datosReporteador);

}
