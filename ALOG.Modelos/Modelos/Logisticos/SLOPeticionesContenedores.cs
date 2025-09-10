using System.Text.Json.Serialization;
using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("peticionesContenedores", Schema = "SLO")]

    public class SLOPeticionesContenedores
    {

        [Key]
        public int IdContenedor { get; set; }

        public string? Contenedor { get; set; }
        public string? FolioManiobra {  get; set; }
        public string? RefenciaCliente { get; set; }
        public string? ClaveTipoContenedor { get; set; }

        [ForeignKey("catPatios")]
        public int? PatioId { get; set; }
        public virtual CatPatios catPatios { get; set; }

        public string? Patio_RazonSocial { get; set; }
        public string? Patio_RFC { get; set; }
        public string? Moneda { get; set; }
        public DateTime? FechaTocaPiso { get; set; }
        public string? BL { get; set; }
        public string? Buque { get; set; }

        [ForeignKey("catClientes")]
        public int? ClienteId { get; set; }
        public virtual CatClientes catClientes { get; set; }

        public string? Cliente_RazonSocial { get; set; }
        public string? Cliente_RFC { get; set; }
        public string? Cliente_Solicitante { get; set; }
        public int? ConsignadoId { get; set; }
        public string? Consignado_RazonSocial { get; set; }
        public string? Consignado_RFC { get; set; }
        public string? FondoFinanciamiento { get; set; }
        public double? MontoSolicitud { get; set; }
        public double? MontoTotal { get; set; }

        [ForeignKey("catAduana")]
        public int? AduanaId { get; set; }
        public CatAduana catAduana { get; set; }

        public DateTime? FechaSolDevolucion { get; set; }
        public DateTime? FechaPagoGarantiaNav { get; set; }
        public string? Naviera_RFC { get; set; }

        [ForeignKey("catNavieras")]
        public int? Naviera_Id { get; set; }
        public virtual CatNavieras catNavieras { get; set; }

        public string? Naviera_RazonSocial { get; set; }
        public string? Aduana { get; set; }

        public ICollection<SLOPeticionesServicios> SLOpeticionesServicios { get; set; }


        [ForeignKey("SLOpeticionesReferencias")]
        public int? IdReferencia { get; set; }
        public SLOPeticionesReferencias SLOpeticionesReferencias { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string? EstadoContenedor { get; set; }

        [ForeignKey("catReferenciaEstado")]
        public int? IdEstadoContenedor { get; set; }
        public virtual CatReferenciaEstado catReferenciaEstado { get; set; }

        public bool Activo { get; set; } = true;
        public string ReferenciaFacturacion { get; set; }

        [ForeignKey("catClientesFacturarA")]
        public int? IdClienteFacturarA { get; set; }
        public CatClientes catClientesFacturarA { get; set; }
    }
}
