using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Orden;

namespace ALOG.Modelos.Modelos.Facturacion
{
    public class PrefacturaDet
    {
        [Key]
        public int IdPrefacturaDet { get; set; }

        [Required]
        [ForeignKey("prefacturaEnc")]
        public int IdPrefacturaEnc { get; set; }
        public PrefacturaEnc prefacturaEnc { get; set; }
        [Required]

        [ForeignKey("orden")]
        public int IdOrden { get; set; }
        public Ordenes Ordenes { get; set; }
        [Required]

        [ForeignKey("catLineaNegocio")]
        public int IdLineaNegocio { get; set; }
        public CatLineaNegocio catLineaNegocio { get; set; }
        [Required]
        public int numeroPartida { get; set; }
        public string ReferenciaCliente { get; set; }
        [Required]
        [ForeignKey("catServicios")]
        public int IdServicio { get; set; }
        public CatServicios catServicios { get; set; }
        public int IdServicio1G { get; set; }
        [Required]
        public string DescripcionServicio { get; set; }
        [Required]
        public float Cantidad { get; set; }
        public double Factconv { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public double Descuento1 { get; set; }
        public int Cve_esq { get; set; }
        public double Impuesto4 { get; set; }
        public double TotalImpuesto4 { get; set; }
        [Required]
        public double TipoCambio { get; set; }
        [Required]
        public double TotalPartida { get; set; }
        public string Contenedor { get; set; }
        public string Bl { get; set; }
        public string Sello { get; set; }
        public string Factura { get; set; }
        public string NumeroParte { get; set; }
        [Required]
        [ForeignKey("catProyectos")]
        public int IdProyectos { get; set; }
        public CatProyectos catProyectos { get; set; }
        [Required]
        [ForeignKey("catAduana")]
        public int IdAduana { get; set; }
        public CatAduana catAduana { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Observaciones { get; set; }
        public string Folio { get; set; }
        public string Serie { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Enviado { get; set; } = false;
        public bool Pagado { get; set; } = false;



    }
}
