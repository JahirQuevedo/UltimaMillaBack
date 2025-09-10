namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatUsuariosClientes
    {
        [Key]
        public int IdCatUsuarioCliente { get; set; }

        [ForeignKey("catUsuarios")]
        public int IdCatUsuario { get; set; }
        public CatUsuarios catUsuarios { get; set; }

        [ForeignKey("catClientes")]
        public int? IdCatCliente { get; set; }
        public CatClientes catClientes { get; set; }

        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
