namespace ALOG.Modelos.Modelos.DTO.Respuestas
{
    public class RespEntidadErrorDTO
    {
        public object entidad { get; set; }
        public string MensajeError { get; set; }
        public List<string> lstErrores { get; set; }
    }
}
