using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos.Modelos.DTO.Solicitudes
{
    public class SolTicketDTO
    {

        public int Id { get; set; }
        public int IdCatEmpresa { get; set; }
        public int IdCatLineaNegocio { get; set; }
        public int IdCatAduana { get; set; }
        public int IdCatProyecto { get; set; } = 0;
        public int IdCatClienteSolicitante { get; set; }
        public int IdCatClienteFacturar { get; set; }
        public int IdCatServicio { get; set; }
        public int IdCatUsuario { get; set; }
        public int IdCatPatio { get; set; }
        public int IdCatNaviera { get; set; }
        public string RfcCLienteFacturar { get; set; }
        public string RazonSocialCLienteFacturar { get; set; }
        public int Ticket { get; set; }
        public string Moneda { get; set; }
        public string ReferenciaCliente { get; set; }
        public string ReferenciaClienteFacturar { get; set; }
        public string Contenedor { get; set; }
        public string ClaveTipoContenedor { get; set; }
        public string ReferenciaAlo { get; set; }
        public string Estatus { get; set; }
        public bool Seleccionado { get; set; }
        public List<CatServicios> Servicios { get; set; }
        public List<string> Errores { get; set; } = new List<string>();


    }
}
