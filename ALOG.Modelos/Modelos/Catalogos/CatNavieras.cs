using ALOG.Modelos.Modelos.DTO;


namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatNavieras : IActivable
    {
        [Key]
        public int IdCatNaviera { get; set; }

        [Required]
        [MaxLength(150)]
        public string RazonSocial { get; set; }
        [MaxLength(20)]
        public string Acronimo { get; set; }
        [Required]
        [MaxLength(15)]
        public string RFC { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
