namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatPermisosDTO
    {
        public int IdCatPermisos { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public bool Activo { get; set; } = true;

        public int IdUsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
