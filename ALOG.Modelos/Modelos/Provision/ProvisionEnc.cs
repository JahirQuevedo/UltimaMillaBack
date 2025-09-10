using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Provision
{
    public class ProvisionEnc : IActivable
    {
        [Key]
        public int IdProvisionesEnc { get; set; }

        [Required, MaxLength(50)]
        public string UUID { get; set; }

        [Required, MaxLength(50)]
        public string Folio { get; set; }

        [Required, ForeignKey("tipoMoneda")]
        public int IdCatTipoMoneda { get; set; }
        public CatTipoMoneda tipoMoneda { get; set; }

        [Required]
        public double TipoCambio { get; set; }

        [Required]
        public double ImporteTotal { get; set; }

        [Required]
        public double Importe { get; set; }

        [Required, ForeignKey("usuarios")]
        public int IdCatUsuarios { get; set; }
        public CatUsuarios usuarios { get; set; }

        [Required, MaxLength(10)]
        public string Estatus { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        public DateTime FechaTimbrado { get; set; }

        public ICollection<ProvisionDet> provisionesDet { get; set; }
    }
}
