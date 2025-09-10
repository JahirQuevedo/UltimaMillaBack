namespace ALOG.Modelos.Modelos.DTO.Control
{
    public class UsuarioRegistroDTO
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
