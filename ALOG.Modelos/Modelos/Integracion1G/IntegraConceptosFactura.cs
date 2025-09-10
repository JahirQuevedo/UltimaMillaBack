namespace ALOG.Modelos.Modelos.Integracion1G
{
    public class IntegraConceptosFactura
    {
        [Key]
        public int IdConceptoFactura { get; set; }
        [Required]
        public int IdConcepto1G { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
