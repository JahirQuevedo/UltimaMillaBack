using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("transporteCronDocumentos", Schema = "SLO")]
    public class SLOTransporteCronDocumentos : IActivable
    {
        [Key]
        public int IdTransporteCronDocumentos { get; set; }

        [Required]
        [ForeignKey(nameof(SLOTransportesCron))]
        public int IdSLOTransporteCron { get; set; }

        [Required]
        [ForeignKey(nameof(SLOSolicitudesDocumentos))]
        public int IdSLOSolicitudDocumentos { get; set; }

        [Required]
        public bool Activo { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [ForeignKey(nameof(CatUsuarios))]
        public int IdUsuarioRegistro { get; set; }

        public virtual SLOTransportesCron SLOTransportesCron { get; set; }

        ICollection<SLOSolicitudesDocumentos> SLOSolicitudesDocumentos { get; set; }

        public virtual CatUsuarios CatUsuarios { get; set; }
    }
}
