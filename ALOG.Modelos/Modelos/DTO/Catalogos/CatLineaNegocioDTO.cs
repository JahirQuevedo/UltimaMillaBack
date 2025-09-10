namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatLineaNegocioDTO
    {
        public int IdCatLineaNegocio { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public string Acronimo { get; set; } //3PL 3PLQ LV
        [Required]

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]

        public int IdUsuarioRegistro { get; set; }


        public ICollection<CatLineaNegocioTarifaDTO> GetCatLineaNegocioTarifas { get; set; }
    }
}
