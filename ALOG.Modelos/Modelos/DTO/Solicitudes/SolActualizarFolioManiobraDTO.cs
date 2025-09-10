namespace ALOG.Modelos.Modelos.DTO.Solicitudes
{
    public class SolActualizarFolioManiobraDTO
    {
        [Required]
        public int IdReferencia { get; set; }
        public int IdOrden { get; set; }
        [Required]
        public int IdContenedor { get; set; }
        public int IdServicio { get; set; }
        public int IdDocumento { get; set; }
        public int IdCatReferenciaEstado { get; set; }
        [Required]
        public string FolioManiobra { get; set; }
    }
}
