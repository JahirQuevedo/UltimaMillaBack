namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatLineaNegocioTarifaDTO
    {
        public int IdCatLineaNegocioTarifa { get; set; }
        [Required]
        //Servicio

        public int IdCatServicio { get; set; }


        //LN
        [Required]

        public int IdCatLineaNegocio { get; set; }


        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]

        public int IdUsuarioRegistro { get; set; }



        public ICollection<CatLineaNegocioTariPrecioDTO> GetCatLineaNegocioTariPrecios { get; set; }
    }
}
