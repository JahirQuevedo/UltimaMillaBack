namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatProveedorTarifaAduana
    {
        [Key]
        public int IdProvTarifaAduana { get; set; }

        [Required]
        [ForeignKey("catAduana")]
        public int IdCatAduana { get; set; }
        public CatAduana catAduana { get; set; }

        [Required]
        [ForeignKey("catProveedorTarifa")]
        public int IdCatProvTarifas { get; set; }
        public virtual CatProveedoresTarifas catProveedorTarifa { get; set; }

        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey("catUsuarios")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuarios { get; set; }


    }
}
