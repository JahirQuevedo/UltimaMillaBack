
namespace ALOG.Modelos.Modelos.Catalogos {
    public class CatCuentaBancaria {
        public int IdCatCuentaBancaria { get; set; }
        public string Cuenta { get; set; }
        public string Clabe { get; set; }
        public string Sucursal { get; set; }
        public string ClaveMonedaSAT { get; set; }
        public string ReferenciaNumerica { get; set; }
        public int IdCatCondicionPago { get; set; }
        public int IdCatProveedor { get; set; }
        public int IdCatAduana { get; set; }
        public int IdCatBanco { get; set; }
        public int IdCatLineaNegocio { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int IdCatUsuario { get; set; }

    }
}
