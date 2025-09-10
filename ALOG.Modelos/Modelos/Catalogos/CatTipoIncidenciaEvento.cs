using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatTipoIncidenciaEvento : IActivable
    {
        [Key]
        public int IdCatTipoIncidenciaEvento { get; set; }
        [ForeignKey("catTipoEventosCron")]
        public int IdCatTipoEventoCron { get; set; }
        public CatTipoEventosCron catTipoEventosCron { get; set; }
        [ForeignKey("catTipoIncidenciaCron")]
        public int IdCatTipoIncidenciaCron { get; set; }
        public CatTipoIncidenciaCron catTipoIncidenciaCron { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set ; }
    }
}
