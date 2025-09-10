namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatServiciosDTO
    {
        public int IdCatServicio { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]

        public int IdCatEmpresas { get; set; } = 1;


        [Required]

        public int IdUsuarioRegistro { get; set; }

    }
}
