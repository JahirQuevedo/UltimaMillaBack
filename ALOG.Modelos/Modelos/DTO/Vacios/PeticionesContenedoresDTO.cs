namespace ALOG.Modelos.Modelos.DTO.Vacios
{
    public class PeticionesContenedoresDTO
    {
        public int IdContenedor { get; set; }
        [RegularExpression(@"^[A-Z]{4}\d{7}$", ErrorMessage = "El formato del contenedor no es válido. Debe tener 4 letras mayúsculas seguidas de 7 dígitos.")]
        public string Contenedor { get; set; }
        public string ClaveTipoContenedor { get; set; }
        public string RefenciaCliente { get; set; }
        public int PatioId { get; set; }
        public string Patio_RazonSocial { get; set; }
        [StringLength(13, MinimumLength = 12, ErrorMessage = "El RFC correspondiente al patio debe tener entre 12 y 13 caracteres")]
        public string Patio_RFC { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaTocaPiso { get; set; }
        public string BL { get; set; }
        public string Buque { get; set; }
        public int ClienteId { get; set; }
        public string Cliente_RazonSocial { get; set; }
        [StringLength(13, MinimumLength = 12, ErrorMessage = "El RFC correspondiente al cliente debe tener entre 12 y 13 caracteres")]
        public string Cliente_RFC { get; set; }
        public string Cliente_Solicitante { get; set; }
        public int ConsignadoId { get; set; }
        public string Consignado_RazonSocial { get; set; }
        public string Consignado_RFC { get; set; }
        public string FondoFinanciamiento { get; set; }
        public double MontoSolicitud { get; set; }
        public double MontoTotal { get; set; }
        public int AduanaId { get; set; }
        public DateTime FechaSolDevolucion { get; set; }
        public DateTime FechaPagoGarantiaNav { get; set; }
        public string Naviera_RFC { get; set; }
        public int Naviera_Id { get; set; }
        public string Naviera_RazonSocial { get; set; }
        public string Aduana { get; set; }
        //public ICollection<CatPatios> catPatios { get; set; }
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaCierre { get; set; }
        public string EstadoContenedor { get; set; } = "PE";
        public bool Activo { get; set; } = true;
        public string FolioManiobra { get; set; }
        public string NombreConductor { get; set; }
        public string LicenciaConductor { get; set; }
        public string NumeroUnidad { get; set; }
        public string PlacaUnidad { get; set; }
        public string ReferenciaFacturar { get; set; }
        public int IdClienteFacturarA { get; set; }
        public int IdCatTransportista { get; set; }
        public ICollection<PeticionesServiciosDTO> Servicios { get; set; }
    }
}
