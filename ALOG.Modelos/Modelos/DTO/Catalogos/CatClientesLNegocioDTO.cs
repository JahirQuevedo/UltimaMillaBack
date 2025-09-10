namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatClientesLNegocioDTO
    {
        public int IdCatClientesLNegocio { get; set; }
        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdLineaNegocio { get; set; }

        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public int IdUsuarioRegistro { get; set; }

    }
}
