namespace ALOG.Modelos.Modelos.DTO.Control
{
    public class RespuestaRegistroUsuarioDTO
    {
        public bool registroCorrecto { get; set; }
        public IEnumerable<string> Errores { get; set; }
    }
}
