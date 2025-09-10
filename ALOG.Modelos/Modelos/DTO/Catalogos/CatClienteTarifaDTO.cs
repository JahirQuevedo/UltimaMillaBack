namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatClienteTarifaDTO
    {
        public int IdCatClienteTarifa { get; set; }

        [Required]
        public int IdCatCteServAduana { get; set; }

        [Required]
        public double Precio { get; set; }
        [Required]
        public double Impuesto { get; set; }

        public int? IdCatContenedor { get; set; }

        [Required]

        public int IdUsuarioRegistro { get; set; }


        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
