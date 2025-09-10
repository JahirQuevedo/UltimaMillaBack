using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Modelos.Modelos.DTLogistico;

namespace ALOG.Modelos.Modelos.Integracion1G
{
    public class IntegraFacturaEnc
    {
        [Key]
        public int IdIntFacturaEnc { get; set; }
        [Required]
        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }

        [ForeignKey("peticionesReferencias")]
        public int? IdPeticionesReferencia { get; set; }
        public PeticionesReferencias peticionesReferencias { get; set; }

        [ForeignKey("dtAcarreos")]
        public int? IdDtAcarreos { get; set; }
        public DtAcarreos dtAcarreos { get; set; }

        [ForeignKey("dtUltimaMillaEnc")]
        public int? IdDtUltimaMillaEnc { get; set; }
        public DtUltimaMillaEnc dtUltimaMillaEnc { get; set; }

        [Required]
        public string IdSolicitudFacturacion { get; set; }
        [Required]
        public string IdCompaniaExterna { get; set; }
        [Required]
        public string ClaveClienteExterno { get; set; }
        [Required]
        public string Fecha { get; set; }
        [Required]
        public string ConceptoFacturacion { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public string Nota { get; set; }
        [Required]
        public string Referencia { get; set; }
        [Required]
        public string ClaveSATMoneda { get; set; }
        [Required]
        public string ClaveSATUsoCFDI { get; set; }
        [Required]
        public string RFC { get; set; }
        [Required]
        public bool FacturacionAutomatica { get; set; } = false;
        [Required]
        public bool CierreReferencia { get; set; } = false;

        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime FechaEnvio { get; set; }

        public bool Enviado { get; set; } = false;

        public string Estado1G { get; set; }
        public string RespuestaWS1G { get; set; }
        public int IdSucursalExterna { get; set; }
        public string CodigoPostal { get; set; }
        public string Serie { get; set; }
        
        //[ForeignKey("IdIntReferencia")]
        public int IdIntReferencia { get; set; }
        public ICollection<IntegraFacturaDet> integraFacturaDet {  get; set; }

        public ICollection<IntegraFacturaEst> integraFacturaEst { get; set; }



    }
}
