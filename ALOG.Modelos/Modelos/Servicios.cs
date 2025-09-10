namespace ALOG.Modelos.Modelos
{
    public class Servicios
    {

        [Key]
        public int id { get; set; }

        [Required]
        public string referenciaALO { get; set; }

        public DateTime fechaSolicitud { get; set; }

        public int idCliente { get; set; }
        public int idLineaNegocio { get; set; }
        public int idEmpresa { get; set; }
        public int idSistema { get; set; }
        public int idSucursal { get; set; }



    }
}
