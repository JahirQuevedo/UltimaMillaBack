namespace ALOG.Modelos.Modelos.DTO.Logistica
{
    public class SLOReferenciasDTO
    {
        public int IdCliente { get; set; }
        public int IdClienteFacturar { get; set; }
        public int IdCatAduana { get; set; }
        public int IdCatLineaNegocio { get; set; } = 3;
        public int IdCatUsuario { get; set; }
        public string? ReferenciaCliente { get; set; }
        public int TipoServicio { get; set; }

        public int IdOrden { get; set; }
        public string ReferenciaALO { get; set; }
        public string Mensaje { get; set; }
    }
}
