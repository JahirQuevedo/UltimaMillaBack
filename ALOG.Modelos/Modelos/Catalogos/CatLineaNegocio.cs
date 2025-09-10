using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatLineaNegocio : IActivable
    {
        [Key]
        public int IdCatLineaNegocio { get; set; }
        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        [MaxLength(20)]
        public string Acronimo { get; set; } //3PL 3PLQ LV
        [Required]

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public CatUsuarios catUsuario { get; set; }


        public ICollection<CatLineaNegocioTarifa> GetCatLineaNegocioTarifas { get; set; }
    }
}
