using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatRolesPermisos : IActivable
    {
        [Key]
        public int IdCatRolesPermisos { get; set; }

        [ForeignKey("CatRoles")]
        [Required]
        public int IdCatRoles { get; set; }
        public CatRoles CatRoles { get; set; }

        [ForeignKey("CatPermisos")]
        [Required]
        public int IdCatPermisos { get; set; }
        public CatPermisos CatPermisos { get; set; }

        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
