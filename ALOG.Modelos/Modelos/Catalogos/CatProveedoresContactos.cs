using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatProveedoresContactos : IActivable
    {
        [Key]
        public int IdCatProvContacto { get; set; }
        [Required]
        [ForeignKey("catProveedores")]
        public int IdCatProveedor { get; set; }
        public CatProveedores catProveedores { get; set; }

        [Required]
        [ForeignKey("catTipoContactos")]
        public int IdCatTipoContacto { get; set; }
        public CatTipoContacto catTipoContacto { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
