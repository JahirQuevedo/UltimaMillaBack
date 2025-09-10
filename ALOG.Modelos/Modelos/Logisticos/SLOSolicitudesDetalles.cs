using ALOG.Modelos.Modelos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Logisticos
{
    public class SLOSolicitudesDetalles
    {
        [Key]
        public int IdSLOSolicitudDet { get; set; }

        [Required]
        [ForeignKey(nameof(sloSolicitudes))]
        public int IdSLOSolicitud { get; set; }

        [Required]
        [ForeignKey(nameof(catMercancias))]
        public int IdCatMercancia { get; set; }

        [MaxLength(12)]
        public string? Contenedor { get; set; }

        public int? IdCatTipoContenedor { get; set; } // FK no declarada, puede omitirse o relacionarse si existe

        public decimal? Peso { get; set; }

        public bool MciaPeligrosa { get; set; }

        [ForeignKey(nameof(catTipoIMO))]
        public int? IdCatTipoIMO { get; set; }

        [MaxLength(50)]
        public string? UNN { get; set; }

        public decimal? Volumen { get; set; }

        [MaxLength(50)]
        public string? NoParte { get; set; }

        public int? Cantidad { get; set; }

        public int? Piezas { get; set; }

        public int? CantidadTransportes { get; set; }

        public int? CantidadCircuitos { get; set; }

        [MaxLength(50)]
        public string? Booking { get; set; }

        [MaxLength(250)]
        public string? DescMercancia { get; set; }

        [Required]
        [ForeignKey(nameof(catTipoEmbalajes))]
        public int IdCatTipoEmbalaje { get; set; }

        // Relaciones de navegación

        public virtual SLOSolicitudes sloSolicitudes { get; set; }

        public virtual CatMercancias catMercancias { get; set; }

        public virtual CatTipoIMO? catTipoIMO { get; set; }

        public virtual CatTipoEmbalajes catTipoEmbalajes { get; set; }
    }
}
