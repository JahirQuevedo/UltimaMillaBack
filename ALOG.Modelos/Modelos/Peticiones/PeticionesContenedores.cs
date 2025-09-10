using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Peticiones;
using ALOG.Modelos.Modelos.Integracion1G;
using System.Text.Json.Serialization;

namespace ALOG.Modelos.Modelos.Vacios
{
    public class PeticionesContenedores
    {

        [Key]
        public int IdContenedor { get; set; }
        [Required]
        public string Contenedor { get; set; }
        [Required]
        [ForeignKey("catTipoContenedor")]
        public int IdCatTipoContenedor { get; set; }
        public CatTipoContenedor catTipoContenedor { get; set; }
        [Required]
        public string RefenciaCliente { get; set; }
        [Required]
        public string ClaveTipoContenedor { get; set; }

        [ForeignKey("catPatios")]
        public int? PatioId { get; set; }
        public CatPatios catPatios { get; set; }
        public string Patio_RazonSocial { get; set; }
        public string Patio_RFC { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaTocaPiso { get; set; }
        public string BL { get; set; }
        public string Buque { get; set; }
        public string BuqueViaje { get; set; }
        [ForeignKey("catClientes")]
        public int? ClienteId { get; set; }
        public CatClientes catClientes { get; set; }

        public string Cliente_RazonSocial { get; set; }

        public string Cliente_RFC { get; set; }

        public string Cliente_Solicitante { get; set; }
        public int? ConsignadoId { get; set; }
        public string Consignado_RazonSocial { get; set; }
        public string Consignado_RFC { get; set; }
        public string FondoFinanciamiento { get; set; }
        public double? MontoSolicitud { get; set; }
        public double? MontoTotal { get; set; }
        [ForeignKey("catAduana")]
        public int? AduanaId { get; set; }
        public CatAduana catAduana { get; set; }
        public DateTime? FechaSolDevolucion { get; set; }
        public DateTime? FechaPagoGarantiaNav { get; set; }

        public string Naviera_RFC { get; set; }
        [ForeignKey("catNavieras")]
        public int? Naviera_Id { get; set; }
        public CatNavieras catNavieras { get; set; }

        public string Naviera_RazonSocial { get; set; }

        public string Aduana { get; set; }

        public ICollection<PeticionesServicios> Servicios { get; set; }


        [ForeignKey("PeticionesReferencias")]
        public int IdReferencia { get; set; }
        [JsonIgnore]
        public PeticionesReferencias PeticionesReferencias { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }

        public string EstadoContenedor { get; set; }

        [ForeignKey("catReferenciaEstado")]
        public int IdEstadoContenedor { get; set; }
        public CatReferenciaEstado catReferenciaEstado { get; set; }

        public bool Activo { get; set; }

        public string ReferenciaFacturacion { get; set; }

        [ForeignKey("catClientesFacturarA")]
        public int? IdClienteFacturarA { get; set; }
        public CatClientes catClientesFacturarA { get; set; }
        public string FolioManiobra { get; set; }
        public int? IdCatTransportista { get; set; }
        public string NombreConductor { get; set; }
        public string LicenciaConductor { get; set; }
        public string NumeroUnidad { get; set; }
        public string PlacaUnidad { get; set; }
        public List<PeticionesContenedoresCron> peticionesContenedoresCron { get; set; }
    }
}
