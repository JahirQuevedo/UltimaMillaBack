namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatClientesConfigDTO
    {
        public int IdCatClientesConfig { get; set; }
        [Required]
        public int IdCatClientes { get; set; }

        [Required]
        public int IdCatTipoConfig { get; set; }

        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public Double Valor1 { get; set; }
        public String Valor2 { get; set; }

        public int IdUsuario { get; set; }

    }
}
