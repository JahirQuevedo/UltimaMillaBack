using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("solicitudesDetalle",Schema ="SLO")]
    public class SLOSolicitudesDetalle:IActivable
    {
        [Key]
        public int IdSLOSolicitudDet { get; set; }

        [ForeignKey("catSLOSolicitudes")]
        public int IdSLOSolicitud { get; set; }

        [ForeignKey("catTipoMercancia")]
        public int IdCatMercancia { get; set; }

        [StringLength(12)]
        public string Contenedor { get; set; }

        [ForeignKey("TipoContenedor")]
        public int? IdCatTipoContenedor { get; set; }


        [ForeignKey("catTipoEmbalajes")]
        public int IdCatTipoEmbalaje { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Peso { get; set; }

        [Required]
        public bool MciaPeligrosa { get; set; }

        [ForeignKey("catTipoIMO")]
        public int? IdCatTipoImo { get; set; }
        public virtual CatTipoIMO? catTipoIMO { get; set; }

        [StringLength(50)]
        public string UNN { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Volumen { get; set; }

        [StringLength(50)]
        public string NoParte { get; set; }

        public int? Cantidad { get; set; }

        public int? Piezas { get; set; }

        public int? CantidadTransportes { get; set; }

        public int? CantidadCircuitos { get; set; }

        [StringLength(50)]
        public string Booking { get; set; }

        [StringLength(250)]
        public string DescMercancia { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }

        // Propiedades de navegación
        public virtual SLOSolicitudes catSLOSolicitudes { get; set;}
        public virtual CatMercancias catTipoMercancia { get; set; }

        

        public virtual CatTipoEmbalajes catTipoEmbalajes { get; set; }
        //public virtual CatTipoContenedor TipoContenedor { get; set; }
        //public virtual CatTipoEmbajale TipoEmbajale { get; set; } 

    }
}
