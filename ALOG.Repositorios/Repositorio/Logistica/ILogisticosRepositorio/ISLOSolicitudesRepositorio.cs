using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Respuestas;

namespace ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio
{
    public interface ISLOSolicitudesRepositorio : IGenericoRepositorio<SLOSolicitudes>
    {
        Task<ICollection<SLOSolicitudes>> SLOSolicitudesObtener(FiltroSLOSolicitudes pFiltro);
        Task<RespuestaGenericaDTO> SLOSolicitudesCrear(SLOSolicitudes solicitud);

        Task<RespuestaGenericaDTO> SLOSolicitudesEditar(int IdSolicitud);
        Task<RespuestaGenericaDTO> SLOSolicitudesActualizar(int id, SLOSolicitudes solicitud);
        Task<RespuestaGenericaDTO> SLOSolicitudesBaja(int IdSolicitud);
        Task<RespuestaGenericaDTO> SLOSolicitudesAlta(int IdSolicitud);
        //Task<RespuestaGenericaDTO> ObtenerDetallesSolicitudSinAsignarAsync(int idSLOSolicitud);
        Task<RespuestaGenericaDTO> SLOSolicitudesDetalleObtener(int idSLOSolicitud);
    }
}
