using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatProveedoresPatios : IActivable
    {

        [Key]
        public int IdCatProveedorPatio { get; set; }


        [Required]
        [ForeignKey("catProveedores")]
        public int IdCatProveedor { get; set; }
        public CatProveedores catProveedores { get; set; }

        [Required]
        [ForeignKey("catPatios")]
        public int IdCatPatio { get; set; }
        public virtual CatPatios catPatios { get; set; }

        [Required]
        [ForeignKey("catAduana")]
        public int IdCatAduana { get; set; }
        public virtual CatAduana catAduana { get; set; }


        [ForeignKey("catUsuarios")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuarios { get; set; }

        public DateTime FechaRegistro { get; set; }




        public bool Activo { get; set; }
    }
}
