using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatPaisMunicipios : IActivable
    {
        [Key]
        public int IdCatMunicipios { get; set; }

        [ForeignKey("catPaisEstados")]
        public int IdPaisEstado { get; set; }
        public CatPaisEstados catPaisEstados { get; set; }
        [MaxLength(15)]
        public string ClaveEntidadSAT { get; set; }
        [MaxLength(15)]
        public string ClaveMunicipioSAT { get; set; }
        [MaxLength(15)]
        public string ClaveMunDel { get; set; }
        [MaxLength(150)]
        public string Nombre { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

    }
}
