using ALOG.Modelos.Modelos.Vacios;

namespace ALOG.Modelos.Modelos.DTO.Respuestas
{
    public class RespObtenerReferenciasDTO
    {
        public int IdReferencia { get; set; } = 0;
        public int Ticket { get; set; } = 0;
        public string Transporte_RFC { get; set; }
        public string Transporte_RazonSocial { get; set; }
        public int TrasporteId { get; set; } = 0;
        public string Transporte_Usuario { get; set; }
        public string Transporte_UsuarioEmail { get; set; }
        public string Comentarios { get; set; }
        public int TipoReferencia { get; set; }
        public string Procesado { get; set; }
        public DateTime FechaProcesado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string EstadoReferencia { get; set; }
        public int IdCatReferenciaEstado { get; set; }
        public bool Activo { get; set; }
        public int IdOrden { get; set; }
        public int IdCatcliente { get; set; }
        public string RazonSocialCliente { get; set; }
        public int IdCatProveedor { get; set; }
        public string RazonSocialProveedor { get; set; }
        public int IdCatAduana { get; set; }
        public string Aduana { get; set; }
        public int IdCatSistema { get; set; }
        public string Sistema { get; set; }
        public int IdCatEmpresa { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public int IdCatsucursal { get; set; }
        public string Sucursal { get; set; }
        public int IdCatTransporte { get; set; }
        public int IdLNegocio { get; set; }
        public string LineaNegocio { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public int IdCatEstadoOrden { get; set; }
        public int EstadoOrden { get; set; }
        public int IdCatEstadoReferencia { get; set; }
        public bool ActivoOrden { get; set; }
        public DateTime FechaRegistroOrden { get; set; }
        public DateTime FechaRegistroReferencia { get; set; }
        public DateTime? FechaCierreOrden { get; set; }
        public DateTime? FechaCierreReferencia { get; set; }
        public string ReferenciaALO { get; set; }
        public string ReferenciaCliente { get; set; }
        public List<PeticionesContenedores> peticionesContenedores { get; set; }
    }
}
