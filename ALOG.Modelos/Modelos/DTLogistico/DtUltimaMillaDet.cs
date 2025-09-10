using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos.Modelos.DTLogistico
{
    public class DtUltimaMillaDet
    {
        [Key]
        public int IdDtUltimaMillaDet { get; set; }
        [Required]
        public string NumeroParte { get; set; }
        [Required]
        public int Piezas { get; set; }

        public int? Pallet { get; set; } = 0;

        [ForeignKey("dtUltimaMillaEnc")]
        public int IdUltimaMilla { get; set; }
        public DtUltimaMillaEnc dtUltimaMillaEnc { get; set; }

        public bool Activo { get; set; }

        [ForeignKey("CatMercancia")]
        public int? IdCatMercancia { get; set; }
        public CatMercancias CatMercancia { get; set; }

    }
}
