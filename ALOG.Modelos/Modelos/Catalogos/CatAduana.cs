using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatAduana : IActivable
    {
        [Key]
        public int IdCatAduana { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        public int Aduana { get; set; }
        [Required]
        public int Seccion { get; set; }
        [Required]
        [MaxLength(10)]
        public string Acronimo { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuario { get; set; }

        [Required]
        [ForeignKey("catPaises")]
        public int IdCatPais { get; set; }
        public CatPaises catPaises { get; set; }

        [Required]
        [ForeignKey("catEstados")]
        public int IdCatPaisEstados { get; set; }
        public CatPaisEstados catEstados { get; set; }


    }
}
