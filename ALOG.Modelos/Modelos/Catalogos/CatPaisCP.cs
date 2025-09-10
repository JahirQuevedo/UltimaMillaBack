using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatPaisCP : IActivable
    {

        [Key]
        public int IdCodigoPostal { get; set; }
        [Required]
        [MaxLength(20)]
        public string CodigoPostal { get; set; }
        [Required]
        [MaxLength(20)]
        public string ClaveEstado { get; set; }
        [Required]
        [MaxLength(20)]
        public string ClaveMunicipio { get; set; }
        [Required]
        [MaxLength(20)]
        public string ClaveMunDel { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

    }
}
