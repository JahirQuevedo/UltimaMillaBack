using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("transporteSolicitud", Schema = "SLO")]
    public class SLOTransporteSolicitud : IActivable
    {


        [Key]
        public int IdSLOTransporteSolicitud { get; set; }

        [Required]
        [ForeignKey("catTransportistas")]
        public int IdCatTransportista { get; set; }
        public virtual CatTransportistas catTransportistas { get; set; }

        [Required]
        [ForeignKey("catTipoEstados")]
        public int IdCatTipoEstados { get; set; }
        public virtual CatTipoEstados catTipoEstados { get; set; }


        [Required]
        [ForeignKey("catUsuarios")]
        public int IdCatUsuarios { get; set; }
        public virtual CatUsuarios catUsuarios { get; set; }

        [Required]
        [ForeignKey("sloSolicitudes")]
        public int IdSLOSolicitud { get; set; }
        public virtual SLOSolicitudes sloSolicitudes { get; set; }


        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }

        [MaxLength(50)]
        public string? CAAT { get; set; }

        [ForeignKey("catTipoOperacionesTransportes")]
        public int IdCatTipoOperTransportes { get; set; }
        public virtual CatTipoOperacionesTransportes catTipoOperacionesTransportes { get; set; }        
        public ICollection<SLOTransporteDetalle> sloTransporteDetalle { get; set; }        
    }
}
