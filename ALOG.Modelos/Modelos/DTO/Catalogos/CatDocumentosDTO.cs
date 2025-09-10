namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatDocumentosDTO
    {
        public int IdCatDocumento { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public int IdUsuarioRegistro { get; set; }

    }
}
