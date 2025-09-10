using ALOG.Modelos.Modelos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;
using static ALOG.Modelos.Modelos.Logisticos.SLOTransporteDetalle;
using static ALOG.Modelos.Modelos.Logisticos.SLOTransporteSolicitud;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("solicitudesDocumentos", Schema = "SLO")]
    public class SLOSolicitudesDocumentos : IActivable
    {
        [Key]
        public int IdSLOSolicitudDocumentos { get; set; }

        [Required]
        [ForeignKey(nameof(sloSolicitudes))]
        public int IdSLOSolicitud { get; set; }

        [ForeignKey(nameof(catUsuarios))]
        public int? IdCatUsuarios { get; set; }

        public DateTime FechaRegistro { get; set; }

        [ForeignKey(nameof(catDocumentos))]
        public int? IdCatDocumento { get; set; }

        [MaxLength(150)]
        public string? Descripcion { get; set; }

        public bool Activo { get; set; }

        [ForeignKey(nameof(sloSolicitudesDetalles))]
        public int? IdSLOSolicitudDet { get; set; }

        [Required]
        [MaxLength(250)]
        public string Ubicacion { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreDocumento { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoArchivo { get; set; }

        [ForeignKey(nameof(sloTransporteSolicitud))]
        public int? IdSLOTransporteSolicitud { get; set; }

        [ForeignKey(nameof(sloTransporteDetalle))]
        public int? IdSLOTransporteDetalle { get; set; }

        public string DocumentoUUID { get; set; }

        // Relaciones de navegación

        public virtual SLOSolicitudes sloSolicitudes { get; set; }

        public virtual CatUsuarios catUsuarios { get; set; }

        public virtual CatDocumentos catDocumentos { get; set; }

        public virtual SLOSolicitudesDetalles sloSolicitudesDetalles { get; set; }

        public virtual SLOTransporteSolicitud sloTransporteSolicitud { get; set; }

        public virtual SLOTransporteDetalle sloTransporteDetalle { get; set; }
    }
}
