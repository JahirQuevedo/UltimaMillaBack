namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatPatiosNavierasDTO
    {
        public int IdCatPatiosNavieras { get; set; }
        [Required]

        public int IdCatNaviera { get; set; }


        [Required]

        public int IdCatPatios { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;


        public int IdUsuarioRegistro { get; set; }

    }
}
