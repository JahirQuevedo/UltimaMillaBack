using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatProveedoresClasif : IActivable
    {
        [Key]
        public int IdCatProvClasif { get; set; }
        [Required]
        [ForeignKey("catProveedores")]
        public int IdCatProveedor { get; set; }
        public CatProveedores catProveedores { get; set; }

        [Required]
        [ForeignKey("catTipoClasificacion")]
        public int IdCatTipoClasificacion { get; set; }
        public CatTipoClasificacion catTipoClasificacion { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
