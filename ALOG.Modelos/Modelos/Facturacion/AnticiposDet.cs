using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Orden;
namespace ALOG.Modelos.Modelos.Facturacion
{
    public class AnticiposDet
    {
        [Key]
        public int IdAnticiposDet { get; set; }
        [Required]
        [ForeignKey("anticiposEnc")]
        public int IdAnticiposEnc { get; set; }
        public AnticiposEnc anticiposEnc { get; set; }
        [Required]

        [ForeignKey("catLineaNegocio")]
        public int IdLineaNegocio { get; set; }
        public CatLineaNegocio catLineaNegocio { get; set; }
        [Required]
        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }
        [Required]
        public double Importe { get; set; }
        [Required]
        public string RefTransferencia { get; set; }
        [Required]
        [ForeignKey("catProveedores")]
        public int IdProveedor { get; set; }
        public CatProveedores catProveedores { get; set; }
        [Required]
        public string RFC { get; set; }
        [Required]
        public string CuentaClabe { get; set; }
        public string RefProveedor { get; set; }
        [Required]
        [ForeignKey("catServicios")]
        public int IdCatServicios { get; set; }
        public CatServicios catServicios { get; set; }
        [Required]
        public string CveServicio { get; set; }

        public int IdCarga { get; set; }

        public DateTime FechaAplicacion { get; set; }
        public int TipoProv { get; set; }
        public string refNumero { get; set; }
        public int CuentaBancaria { get; set; }
        public double SaldoAplicado { get; set; }

        public string txt_app_monex_descargado { get; set; }

        public int Clave1G { get; set; }
        public string Estado1G { get; set; }






    }
}
