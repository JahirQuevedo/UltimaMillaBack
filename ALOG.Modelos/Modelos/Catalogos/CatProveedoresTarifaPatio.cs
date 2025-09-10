using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatProveedoresTarifaPatio : IActivable
    {
        [Key]
        public int IdCatProvTarifaPatio { get; set; }
        [ForeignKey("catProveedoresTarifas")]
        public int IdCatProvTarifas { get; set; }
        public CatProveedoresTarifas catProveedoresTarifas { get; set; }

        [ForeignKey("catPatio")]
        public int IdCatPatio { get; set; }
        public virtual CatPatios catPatio { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey("catUsuarios")]
        public int IdUsuarioRegistro { get; set; }
        public CatUsuarios catUsuarios { get; set; }






    }
}
