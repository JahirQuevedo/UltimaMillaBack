using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO;
using ALOG.Modelos.Modelos.Orden;
namespace ALOG.Modelos.Modelos.DTLogistico
{
    public class DtUltimaMillaEnc : IActivable
    {
        [Key]
        public int IdDtUltMillaEnc { get; set; }

        public DateTime FechaSolicitud { get; set; }
        [Required]
        public int Viaje { get; set; }

        [ForeignKey("catClientes")]
        public int? IdCliente { get; set; }
        public CatClientes catClientes { get; set; }

        [Required]
        public string Cliente { get; set; }

        [Required]
        public string FacturaCliente { get; set; }

        public string Bodega { get; set; }

        [ForeignKey("catEmpresa")]
        public int IdCatEmpresa { get; set; }
        public CatEmpresas catEmpresa { get; set; }

        public DateTime FechaSalida { get; set; }

        [ForeignKey("catTipoEstados")]
        public int IdTipoEstado { get; set; }
        public CatTipoEstados catTipoEstados { get; set; }

        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }

        [ForeignKey("catServicios")]
        public int IdCatServicio { get; set; }
        public CatServicios catServicios { get; set; }

        [ForeignKey("catProveedores")]
        public int IdCatProveedor { get; set; }
        public CatProveedores catProveedores { get; set; }


        public ICollection<DtUltimaMillaDet> DtUltimaMillaDets { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        [ForeignKey("catTipoTransporte")]
        public int? IdCatTipoTransporte { get; set; }
        public CatTipoTransporte catTipoTransporte { get;set; }
        public string? Origen { get; set; }
        public string? Destino { get; set; }
        public string? LugarPosicionamiento { get; set; }
        public string? LugarEntregaDestino { get; set; }
        public DateTime? FechaPosicionamiento { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? Booking { get; set; }
        public bool? CargaPeligrosa { get; set; }
        public string? TipoOperacion { get; set; }

        [ForeignKey("catAduana")]
        public int? IdCatAduana { get; set; }
        public CatAduana catAduana { get; set; }
    }
}
