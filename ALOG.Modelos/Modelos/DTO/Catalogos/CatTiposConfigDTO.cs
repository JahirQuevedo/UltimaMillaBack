namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatTiposConfigDTO
    {
        public int IdCatTiposConfig { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Clave { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
