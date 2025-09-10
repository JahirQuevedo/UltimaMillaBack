using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatTipoEstados : IActivable
    {
        [Key]
        public int IdCatTipoEstados { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required, MaxLength(50)]
        public string TipoEstado { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;


    }
}
