using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatSeriesFacturacion : IActivable
    {
        [Key]
        public int IdCatSeriesFact { get; set; }

        [ForeignKey("catLineaNegocio")]
        public int IdCatLineaNegocio { get; set; }
        public CatLineaNegocio catLineaNegocio { get; set; }

        [ForeignKey("catAduana")]
        public int IdCatAduana { get; set; }
        public CatAduana catAduana { get; set; }

        public string Serie { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
