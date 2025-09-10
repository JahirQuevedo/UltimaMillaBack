using ALOG.Modelos.Modelos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("tcontrolTerrestre", Schema = "SLO")]
    public class SLOTControlTerrestre
    {
        [Key]
        public int IdTControlTerrestre { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FConfirmaBooking { get; set; }
        public DateTime? FUnidensitiocarga { get; set; }
        public DateTime? FUnidadencarga { get; set; }
        public DateTime? FFinalizaCarga { get; set; }
        public DateTime? FIniciaTransito { get; set; }
        public DateTime? FEnFrontera { get; set; }
        public DateTime? FInicioDespachoAA { get; set; }
        public DateTime? FFinDespachoAA { get; set; }
        public DateTime? FIniciaTransitoEXPO { get; set; }
        public DateTime? FPuntoDescarga { get; set; }
        public DateTime? FEnProcesoDescarga { get; set; }
        public DateTime? FFinDescarga { get; set; }
        public DateTime? FRecepcionPOD { get; set; }
        public DateTime? FFinOperacion { get; set; }

        public bool Activo { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }

        [ForeignKey(nameof(sloTransporteAsignado))]
        public int? IdSLOTransporteAsignado { get; set; }

        [ForeignKey(nameof(sloSolicitudesDetalles))]
        public int? IdSLOSolicitudDet { get; set; }

        [Required]
        [ForeignKey(nameof(sloSolicitudes))]
        public int IdSLOSolicitud { get; set; }

        // Relaciones de navegación

        public virtual CatUsuarios catUsuarios { get; set; }

        public virtual SLOTransporteAsignado sloTransporteAsignado { get; set; }

        public virtual SLOSolicitudesDetalles? sloSolicitudesDetalles { get; set; }

        public virtual SLOSolicitudes sloSolicitudes { get; set; }
    }
}
