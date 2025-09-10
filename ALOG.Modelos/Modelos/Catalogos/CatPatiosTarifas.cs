namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatPatiosTarifas
    {
        [Key]
        public int IdCatPatiosTarifas { get; set; }

        [ForeignKey("CatPatios")]
        public int IdCatPatios { get; set; }
        public CatPatios CatPatios { get; set; }

        [ForeignKey("catServicios")]
        public int IdCatServicios { get; set; }
        public CatServicios catServicios { get; set; }

        [Required]
        public double Tarifa { get; set; }
        [Required]
        public double Impuesto { get; set; }
        public bool Activo { get; set; }
    }
}
