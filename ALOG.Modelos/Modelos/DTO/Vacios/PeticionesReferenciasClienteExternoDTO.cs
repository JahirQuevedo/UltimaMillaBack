namespace ALOG.Modelos.Modelos.DTO.Vacios
{
    public class PeticionesReferenciasClienteExternoDTO
    {
        public int? IdCatClienteSolicitante { get; set; }
        public int? Ticket { get; set; }
        public int IdCatAduana { get; set; }
        public string Comentarios { get; set; }
        public DateTime? FechaSolicitud {  get; set; }
        public ICollection<PeticionesContenedoresClienteExternoDTO> Contenedores { get; set; }
    }
}
