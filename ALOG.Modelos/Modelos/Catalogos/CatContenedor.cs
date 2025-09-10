using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatContenedor : IActivable
    {

        [Key]
        public int IdCatContenedor { get; set; }

        [Required]
        [MaxLength(10)]
        public string Nomenclatura { get; set; }
        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuario { get; set; }

    }
}
