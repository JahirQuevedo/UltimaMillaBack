namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatClientesProyectosDTO
    {
        public int IdCatClientesProyectos { get; set; }

        [Required]
        public int IdCatCliente { get; set; }

        [Required]
        public int IdCatProyecto { get; set; }

        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        public int IdUsuarioRegistro { get; set; }

    }
}
