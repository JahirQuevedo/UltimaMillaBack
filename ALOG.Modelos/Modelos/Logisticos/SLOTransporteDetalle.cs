using ALOG.Modelos.Modelos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALOG.Modelos.Modelos.Logisticos.SLOTransporteSolicitud;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("transporteDetalle", Schema = "SLO")]
    public class SLOTransporteDetalle
    {


        [Key]
        public int IdSLOTransporteDetalle { get; set; }

        [Required]
        [ForeignKey(nameof(catTipoEstados))]
        public int IdCatTipoEstados { get; set; }

        [Required]
        [ForeignKey(nameof(catTipoTransporte))]
        public int IdCatTipoTransporte { get; set; }

        [ForeignKey(nameof(sloTransporteSolicitud))]
        public int? IdSLOTransporteSolicitud { get; set; }

        [MaxLength(50)]
        public string? FolioUUID { get; set; }

        public DateTime FechaRegistro { get; set; }

        public bool Activo { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }

        [Required]
        [ForeignKey(nameof(sloSolicitudesDetalles))]
        public int IdSLOSolicitudDet { get; set; }

        [Required]
        [ForeignKey(nameof(sloSolicitudes))]
        public int IdSLOSolicitud { get; set; }

        // Relaciones de navegación

        public virtual CatTipoEstados catTipoEstados { get; set; }

        public virtual CatTipoTransporte catTipoTransporte { get; set; }

        public virtual SLOTransporteSolicitud? sloTransporteSolicitud { get; set; }

        public virtual CatUsuarios catUsuarios { get; set; }

        public virtual SLOSolicitudesDetalles sloSolicitudesDetalles { get; set; }

        public virtual SLOSolicitudes sloSolicitudes { get; set; }

    }
}
