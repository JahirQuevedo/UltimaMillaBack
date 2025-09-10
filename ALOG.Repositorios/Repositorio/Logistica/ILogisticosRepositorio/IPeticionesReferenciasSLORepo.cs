using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Logistica;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Modelos.Modelos.Orden;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio
{
    public interface IPeticionesReferenciasSLORepo
    {
        Task<IEnumerable<Ordenes>> obtenerReferencias(FiltroOrdenesReferenciasDTO filtro);
        Task<IEnumerable<SLOIntegraFacturaEnc>> ObtenerFacturasPorOrden(int idOrden);
        //Task<IEnumerable<SLOPeticionesContenedores>> obtenerServicios(int idOrden);
        Task<Ordenes> obtenerServicios(int idOrden);
        Task<RespuestaGenericaDTO> ActualizarReferenciaCliente(Ordenes orden);
        Task<RespuestaGenericaDTO> crearReferencia(SLOReferenciasDTO dto);
        Task<RespuestaGenericaDTO> GuardarFacturacionCompleta(List<SLOPeticionesContenedores> contenedores);
        Task<RespuestaGenericaDTO> ActualizarServicioAsync(SLOPeticionesServicios servicioActualizado);
        Task<RespuestaGenericaDTO> EliminarServicioAsync(int idServicio);
        Task<RespuestaGenericaDTO> envioFacturacion(Ordenes servicios);
    }
}
