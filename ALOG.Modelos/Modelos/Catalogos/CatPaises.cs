using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatPaises : IActivable
    {
        [Key]
        public int IdCatPaises { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        [MaxLength(15)]
        public string ClavePaisSAT { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public virtual ICollection<CatPaisEstados> GetCatPaisEstados { get; set; }
    }
}
