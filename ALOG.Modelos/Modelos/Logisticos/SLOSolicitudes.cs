using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO;
using ALOG.Modelos.Modelos.Orden;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("solicitudes", Schema = "SLO")]
    public class SLOSolicitudes : IActivable
    {
        [Key]
        public int IdSLOSolicitud { get; set; }
        
        [Required]
        [ForeignKey("Cliente")]
        public int IdCatCliente { get; set; }
        [Required]
        [ForeignKey("catClienteUbicacionOrigen")]
        public int IdCatUbicacionOrigen { get; set; }
        [Required]
        [ForeignKey("catClienteUbicacionDestino")]
        public int IdCatUbicacionDestino { get; set; }
            

        [ForeignKey("TipoCarga")]
        public int? IdCatTipoCarga { get; set; }

        [Required]
        [ForeignKey("catTipoOperComercio")]
        public int IdCatTipoOperComercio { get; set; }
        public virtual CatTipoOperacionComercio catTipoOperComercio { get; set; }
        public DateTime? FechaPosicionamiento { get; set; }

        [StringLength(50)]
        public string ReferenciaCliente { get; set; }

        [StringLength(50)]
        public string Booking { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        [ForeignKey("catUsuario")]
        public int IdCatUsuario { get; set; }

        [StringLength(500)]
        public string Notas { get; set; }
        [Required]
        [ForeignKey("catTipoEstado")]
        public int IdCatTipoEstado { get; set; }
        [Required]
        [ForeignKey("Orden")]
        public int IdOrden { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }
        [Required]
        [ForeignKey("catTipoOperacion")]
        public int IdCatTipoOperacion { get; set; }

        // Propiedades de navegación
        public virtual CatClientes Cliente { get; set; }
        //public virtual CatClientesUbicaciones UbicacionOrigen { get; set; }
        //public virtual CatClientesUbicaciones UbicacionDestino { get; set; }
        //public virtual CatTipoOperacionComercio TipoOperComercio { get; set; }
        public virtual CatTipoCarga TipoCarga { get; set; }
        public virtual CatUsuarios catUsuario { get; set; }
        public virtual CatClientesUbicaciones catClienteUbicacionOrigen { get; set; }
        public virtual CatClientesUbicaciones catClienteUbicacionDestino { get; set; }
        public Ordenes Orden { get; set; }
        public virtual CatTipoEstados catTipoEstado { get; set; }
        public virtual CatTipoOperacion catTipoOperacion { get; set; }

        public virtual ICollection<SLOSolicitudesDetalle> sloSOlicitudesDetalle { get; set; }

    }
}
