namespace ALOG.Modelos.Modelos.Integracion1G
{
    public class IntegraFacturaEst
    {
        [Key]
        public int IdIntFacturaEst { get; set; }
        [Required]
        [ForeignKey("integracionFacturaEnc")]
        public int IdIntFacturaEnc { get; set; }
        public IntegraFacturaEnc integracionFacturaEnc { get; set; }
        [Required]
        public string Mensaje { get; set; }
        [Required]
        public string IdSolicitudFacturacion { get; set; }
        [Required]
        public string FolioFactura { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public double MontoTotal { get; set; }
        [Required]
        public double MontoPagado { get; set; }
        [Required]
        public double MontoNotaCredito { get; set; }
        [Required]
        public double SaldoFactura { get; set; }
        [Required]
        public DateTime FechaUltimoPago { get; set; }
        [Required]
        public string EstatusFactura { get; set; }
        [Required]
        public string EstatusSolicitud { get; set; }


    }
}
