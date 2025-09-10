using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatPatios : IActivable
    {

        [Key]
        public int IdCatPatios { get; set; }
        [Required]
        [MaxLength(200)]
        public string RazonSocial { get; set; }

        public DateTime FechaRegistro { get; set; }

        [ForeignKey("catUsuarios")]
        public int IdUsuarioRegistro { get; set; }
        public CatUsuarios catUsuarios { get; set; }

        public ICollection<CatPatiosConfig> catPatiosConfigs { get; set; }
        public ICollection<CatPatiosNavieras> catPatiosNavieras { get; set; }

        public CatProveedoresPatios catProveedoresPatios { get; set; }

        public bool Activo { get; set; }
        public string Acronimo { get; set; }
    }
}
