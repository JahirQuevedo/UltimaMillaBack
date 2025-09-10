using ALOG.Modelos.Modelos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("transporteAsignado", Schema = "SLO")]
    public class SLOTransporteAsignado
    {
        [Key]
        public int IdSLOTransporteAsignado { get; set; }

        [ForeignKey(nameof(sloTransporteDetalle))]
        public int? IdSLOTransporteDetalle { get; set; }

        [Required]
        [ForeignKey(nameof(catTipoTransporte))]
        public int IdCatTipoTransporte { get; set; }

        [Required]
        [MaxLength(10)]
        public string Placas { get; set; }

        [Required]
        [MaxLength(10)]
        public string Economico { get; set; }

        [Required]
        [MaxLength(150)]
        public string Operador { get; set; }

        [MaxLength(20)]
        public string? Color { get; set; }

        [MaxLength(50)]
        public string? Marca { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }

        public DateTime FechaRegistro { get; set; }

        public bool Activo { get; set; }

        [Required]
        [ForeignKey(nameof(sloTransporteSolicitud))]
        public int IdSLOTransporteSolicitud { get; set; }

        public List<SLOTransporteAsignadoDetalle> sloTransporteAsignadoDetalle { get; set; }
        // Relaciones de navegación

        public virtual CatTipoTransporte catTipoTransporte { get; set; }

        public virtual SLOTransporteDetalle sloTransporteDetalle { get; set; }

        public virtual SLOTransporteSolicitud sloTransporteSolicitud { get; set; }

        public virtual CatUsuarios catUsuarios { get; set; }
    }
}
