namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatRolesDTO
    {
        public int IdCatRoles { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;



        public int IdUsuarioRegistro { get; set; }

    }
}
