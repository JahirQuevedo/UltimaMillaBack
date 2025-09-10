using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTLogistico;
using ALOG.Modelos.Modelos.Integracion1G;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("peticionesServicios", Schema = "SLO")]

    public class SLOPeticionesServicios
    {
        [Key]
        public int IdServicio { get; set; }

        [ForeignKey("catServicios")]
        public int? IdCatServicio { get; set; }
        public CatServicios catServicios { get; set; }

        public double? Cantidad { get; set; }
        public double? Monto { get; set; }

        public string? DescServicio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }


        [ForeignKey("SLOpeticionesContenedores")]
        public int? IdContenedor { get; set; }
        public SLOPeticionesContenedores SLOpeticionesContenedores { get; set; }

        public string? EstadoServicio { get; set; }

        [ForeignKey("catReferenciaEstado")]
        public int? IdEstadoServicio { get; set; }
        public CatReferenciaEstado catReferenciaEstado { get; set; }

        public bool Activo { get; set; } = true;

        //Servicios Facturados a otro cliente.
        [ForeignKey("catClientesFacturarA")]
        public int? IdClienteFacturarA { get; set; }
        public CatClientes catClientesFacturarA { get; set; }

        public string ReferenciaClienteFactura { get; set; }

        public string Moneda {  get; set; }

        public ICollection<SLOIntegraFacturaDet> SLOintegraFacturaDet { get; set; }

    }
}
