namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatLineaNegocioTariPrecio
    {
        [Key]
        public int IdCatLineNegocioTariPrecio { get; set; }
        [Required]
        [ForeignKey("catLineaNegocioTarifa")]
        public int IdCatLineaNegocioTarifa { get; set; }
        public virtual CatLineaNegocioTarifa catLineaNegocioTarifa { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public double Impuesto { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuario { get; set; }
        [Required]
        [ForeignKey("catAduana")]
        public int IdCatAduana { get; set; }
        public virtual CatAduana catAduana { get; set; }
        [Required]
        [ForeignKey("catEmpresas")]
        public int IdCatEmpresa { get; set; }
        public virtual CatEmpresas catEmpresas { get; set; }
        //Id
        //Id CatLineaNegocioTarifa
        //Id Padre
        //Pecios
        //Impuesto
        //Fecha de operación inicio, fin,
        //Precio        
        //Impuesto
        //Aduana

    }
}
