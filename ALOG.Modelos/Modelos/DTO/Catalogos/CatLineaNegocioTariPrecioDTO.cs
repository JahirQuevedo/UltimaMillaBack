namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatLineaNegocioTariPrecioDTO
    {
        public int IdCatLineNegocioTariPrecio { get; set; }
        [Required]

        public int IdCaIdCatLineaNegocioTarifa { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public double Impuesto { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        public int IdUsuarioRegistro { get; set; }
        [Required]
        public int IdCatAduana { get; set; }
    }
}
