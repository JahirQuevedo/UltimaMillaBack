using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("transporteAsignadoDetalle", Schema = "SLO")]
    public class SLOTransporteAsignadoDetalle
    {
        [Key]
        public int IdSLOTransporteAsignadoDetalle { get; set; }
        [ForeignKey("sloTransporteAsignado")]
        public int IdSLOTransporteAsignado { get; set; }
        public SLOTransporteAsignado sloTransporteAsignado { get; set; }
        [ForeignKey("sloTransporteDetalle")]
        public int IdSLOTransporteDetalle { get; set; }
        public SLOTransporteDetalle sloTransporteDetalle { get; set; }
        [ForeignKey("catUsuarioRegistro")]
        public int IdCatUsuarioRegistro { get; set; }
        public CatUsuarios catUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
    }
}
