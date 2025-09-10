using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatUsuarioRoles : IActivable
    {
        [Key]
        public int IdCatUsuariosRoles { get; set; }

        [ForeignKey("CatUsuarios")]
        public int IdCatUsuarios { get; set; }
        public CatUsuarios CatUsuarios { get; set; }

        [ForeignKey("CatRoles")]
        public int IdCatRoles { get; set; }
        public CatRoles CatRoles { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        //[ForeignKey("catUsuariosRegistro")]
        //public int? IdUsuarioRegistro { get; set; }
        //public CatUsuarios catUsuariosRegistro { get; set; }
    }
}
