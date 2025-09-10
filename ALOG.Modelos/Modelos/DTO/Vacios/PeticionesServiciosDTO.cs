namespace ALOG.Modelos.Modelos.DTO.Vacios
{
    public class PeticionesServiciosDTO
    {
        public int IdServicio { get; set; }
        public int IdTipoServicio { get; set; }
        public string DescServicio { get; set; }
        public int IdContenedor { get; set; }
        public string EstadoServicio { get; set; } = "A";
        public int IdClienteFacturar { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaCierre { get; set; }
        public bool Activo { get; set; } = true;
        public PeticionesDocumentosClienteExternoDTO BL { get; set; }
        //public ICollection<PeticionesDocumentosDTO> Documentos { get; set; }
    }
}
