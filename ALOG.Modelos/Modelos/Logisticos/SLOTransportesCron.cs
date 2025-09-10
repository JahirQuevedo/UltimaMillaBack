using ALOG.Modelos.Modelos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("transporteCron", Schema = "SLO")]
    public class SLOTransportesCron : IActivable
    {
        [Key]
        public int IdSLOTransporteCron { get; set; }

        [Required]
        [ForeignKey(nameof(catTipoEventosCron))]
        public int IdCatTipoEventoCron { get; set; }

        [Required]
        [MaxLength(250)]
        public string Comentarios { get; set; }

        public DateTime FechaRegistro { get; set; }

        public bool Activo { get; set; }

        [Required]
        public DateTime FechaEvento { get; set; }

        [MaxLength(250)]
        public string? URLMaps { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }

        [ForeignKey(nameof(sloTransporteAsignado))]
        public int? IdSLOTransporteAsignado { get; set; }

        // Relaciones de navegación
        public virtual CatTipoEventosCron catTipoEventosCron { get; set; }

        public virtual CatUsuarios catUsuarios { get; set; }

        public virtual SLOTransporteAsignado sloTransporteAsignado { get; set; }
    }
}
