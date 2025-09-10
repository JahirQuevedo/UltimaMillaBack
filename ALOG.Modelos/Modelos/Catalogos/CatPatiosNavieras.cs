using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatPatiosNavieras : IActivable
    {
        [Key]
        public int IdCatPatiosNavieras { get; set; }
        [Required]
        [ForeignKey("CatNavieras")]
        public int IdCatNaviera { get; set; }
        public virtual CatNavieras CatNavieras { get; set; }

        [Required]
        [ForeignKey("CatPatios")]
        public int IdCatPatios { get; set; }
        public virtual CatPatios CatPatios { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]

        [ForeignKey("catUsuarios")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuarios { get; set; }

    }
}
