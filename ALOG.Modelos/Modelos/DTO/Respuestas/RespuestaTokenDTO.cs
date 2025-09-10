namespace ALOG.Modelos.Modelos.DTO.Respuestas
{
    public class RespuestaTokenDTO
    {
        public int IdCatUsuario { get; set; } // Campo usado para los usuario pertenecientes a la tabla catUsuario
        public int IdCatSistema { get; set; } // Campo utilizado para los usuarios pertenecientes a la tabla catSistema
        public string User { get; set; } // Se almacena el nombre del usuario logeado perteneciente a la tabla catUsuario
        public int? IdCatCliente { get; set; } //Se almacena el id del cliente al que pertenece el usuario
        public string Email { get; set; } //Se almacena el email del usuario obtenido de la tabla catUsuarios
        public List<int> IdCatEmpresa { get; set; } //Se guardan las empresas a las que perteneces los usuarios internos
        public List<String> RolesUsuario { get; set; } //Se guardan todos los roles que tenga el usuario
    }
}