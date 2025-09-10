using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatTipoContenedor : IActivable
    {
        [Key]
        public int IdCatTipoContenedor { get; set; }
        [Required]
        public string Nomenclatura { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuario { get; set; }
    }
}
