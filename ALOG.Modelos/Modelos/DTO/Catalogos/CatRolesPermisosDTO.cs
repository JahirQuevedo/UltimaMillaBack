using ALOG.Modelos.Modelos.Catalogos;
namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatRolesPermisosDTO
    {
        public int IdCatRolesPermisos { get; set; }

        [Required]
        public int IdCatRoles { get; set; }
        public CatRoles CatRoles { get; set; }

        [Required]
        public int IdCatPermisos { get; set; }
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
