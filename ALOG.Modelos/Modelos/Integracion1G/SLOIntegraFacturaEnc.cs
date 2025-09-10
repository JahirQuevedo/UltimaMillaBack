using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Modelos.Modelos.Orden;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ALOG.Modelos.Modelos.Integracion1G
{
    [Table("integracionFacturaEnc", Schema = "SLO")]
    public class SLOIntegraFacturaEnc
    {
        [Key]
        public int IdIntFacturaEnc { get; set; }
        
        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public virtual Ordenes ordenes { get; set; }
        
        [ForeignKey("SLOpeticionesReferencias")]
        public int IdPeticionesReferencia { get; set; }
        public virtual SLOPeticionesReferencias SLOpeticionesReferencias { get; set; }

        [ForeignKey("dtAcarreos")]
        public int? IdDtAcarreos { get; set; }
        public virtual DtAcarreos dtAcarreos { get; set; }

        [ForeignKey("dtUltimaMillaEnc")]
        public int? IdDtUltimaMillaEnc { get; set; }
        public virtual DtUltimaMillaEnc dtUltimaMillaEnc { get; set; }

        public string? IdSolicitudFacturacion { get; set;  }
        public string? IdCompaniaExterna { get; set; } 
        public string? ClaveClienteExterno { get; set; }
        public string? Fecha { get; set; }
        public string? ConceptoFacturacion { get; set; }
        public string? Comentario { get; set; }
        public string? Nota { get; set; }
        public string? Referencia { get; set; }
        public string? ClaveSATMoneda { get; set; }
        public string? ClaveSATUsoCFDI { get; set; }
        public string? RFC { get; set; }
        public bool FacturacionAutomatica { get; set; }
        public bool CierreReferencia { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public bool Enviado { get; set; }
        public string? Estado1G { get; set; }
        public string? RespuestaWS1G { get; set; }
        public int? IdSucursalExterna { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Serie { get; set; }

        [ForeignKey("SLOintegracionReferencia")]
        public int IdIntReferencia { get; set; }
        public virtual SLOIntegracionReferencia SLOintegracionReferencia { get; set; }

        public bool EstaListo { get; set; }

        public virtual ICollection<SLOIntegraFacturaDet> SLOintegracionFacturaDet { get; set; } = new List<SLOIntegraFacturaDet>();
    }
}
