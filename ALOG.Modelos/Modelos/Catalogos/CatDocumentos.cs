using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatDocumentos : IActivable
    {
        [Key]
        public int IdCatDocumento { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]
        public string Acronimo { get; set; }
        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuario { get; set; }


    }
}
