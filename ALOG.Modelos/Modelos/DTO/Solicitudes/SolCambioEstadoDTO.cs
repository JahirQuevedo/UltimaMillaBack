namespace ALOG.Modelos.Modelos.DTO.Solicitudes
{
    public class SolCambioEstadoDTO
    {
        public string TipoCambio { get; set; }
        public int IdReferencia { get; set; }
        public int IdOrden { get; set; }
        public int IdContenedor { get; set; }
        public int IdServicio { get; set; }
        public int IdDocumento { get; set; }
        public string ReferenciaCliente { get; set; }
        public int IdCatReferenciaEstado { get; set; }
        public string FolioReferencia { get; set; }
    }
}
