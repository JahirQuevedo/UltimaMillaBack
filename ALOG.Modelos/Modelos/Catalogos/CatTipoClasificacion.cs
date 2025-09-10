using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatTipoClasificacion : IActivable
    {
        [Key]
        public int IdCatTipoClasificacion { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }
        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
