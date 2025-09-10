using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatSeriesFacturas : IActivable
    {
        [Key]
        public int IdCatSeriesFact { get; set; }

        [Required]
        [ForeignKey("catLineaNegocio")]
        public int IdCatLineaNegocio { get; set; }
        public virtual CatLineaNegocio catLineaNegocio { get; set; }

        [Required]
        [ForeignKey("catAduana")]
        public int IdCatAduana { get; set; }
        public virtual CatAduana catAduana { get; set; }

        [Required]
        [ForeignKey("catEmpresas")]
        public int IdCatEmpresa { get; set; }
        public virtual CatEmpresas catEmpresas { get; set; }

        [Required]
        [ForeignKey("catSucursal")]
        public int IdCatSucursal { get; set; }
        public virtual CatSucursales catSucursales { get; set; }

        [Required]
        [StringLength(10)]
        public string Serie { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
