using ALOG.Modelos.Modelos.Vacios;

namespace ALOG.Modelos.Modelos.Integracion1G
{
    public class IntegraFacturaDet
    {
        [Key]
        public int IdIntFacturaDet { get; set; }
        [Required]
        [ForeignKey("integracionFacturaEnc")]
        public int IddIntFacturaEnc { get; set; }
        public IntegraFacturaEnc integracionFacturaEnc { get; set; }
        [Required]
        public string ClaveServicio { get; set; }
        [Required]
        public string Cantidad { get; set; }
        [Required]
        public string Precio { get; set; }

        [Required]
        [ForeignKey("peticionesReferencias")]
        public int IdPeticionesReferencia { get; set; }
        public PeticionesReferencias peticionesReferencias { get; set; }

        [Required]
        [ForeignKey("peticionesContenedor")]
        public int IdPeticionesContenedor { get; set; }
        public PeticionesContenedores peticionesContenedor { get; set; }
        [Required]
        public string Contenedor { get; set; }
        [Required]
        public string CentroCostos { get; set; }
        [Required]
        public string EIR { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public string Nota { get; set; }
       public int IdCatServicio { get; set; }

    }
}
