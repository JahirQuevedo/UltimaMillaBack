namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatProveedoresTarifaPatioDTO
    {
        public int IdCatProvTarifaPatio { get; set; }

        public int IdCatProvTarifas { get; set; }



        public int IdCatPatio { get; set; }

        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Required]

        public int IdUsuarioRegistro { get; set; }

    }
}
