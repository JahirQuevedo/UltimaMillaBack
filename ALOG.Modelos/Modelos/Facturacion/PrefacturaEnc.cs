using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Orden;
namespace ALOG.Modelos.Modelos.Facturacion
{
    public class PrefacturaEnc
    {
        [Key]
        public int IdPrefacturaEnc { get; set; }
        [Required]
        [ForeignKey("catLineaNegocio")]
        public int IdCatLineaNegocio { get; set; }
        public CatLineaNegocio catLineaNegocio { get; set; }

        public DateTime FechaPrefactura { get; set; }

        //IDREF
        [Required]
        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }

        public string ReferenciaCliente { get; set; }
        [Required]
        [ForeignKey("catClientes")]
        public int IdCliente { get; set; }
        public CatClientes catClientes { get; set; }
        [Required]
        public string RFC { get; set; }
        [Required]
        public string NombreCliente { get; set; }
        [Required]
        public int Moneda { get; set; }
        [Required]
        public int TipoCambio { get; set; }
        [Required]
        public double Importe { get; set; }
        [Required]
        [ForeignKey("catAduana")]
        public int IdAduana { get; set; }

        public CatAduana catAduana { get; set; }
        [Required]
        public string Aduana { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [ForeignKey("catUsuarios")]
        public int IdUsuario { get; set; }
        public CatUsuarios catUsuarios { get; set; }


        public string EstadoPrefactra { get; set; }
        public bool Activo { get; set; }

        public int Clave1G { get; set; }
        public string Estado1G { get; set; }



        public virtual ICollection<PrefacturaDet> PrefacturaDet { get; set; }
    }
}
