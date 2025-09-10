using ALOG.Enums;

namespace ALOG.Modelos;

public class ConsultaSolicitudTraslado : BaseEntity
{

    public int IdSolicitudTraslado { get; set; }
    
    public int FolioSolicitudTraslado { get; set; }

    public string FolioReferenciaTarja { get; set; }

    public string NombreCliente { get; set; }

    public string Transportista { get; set; }

    public string Placas { get; set; }

    public string NumeroEconomico { get; set; }

    public string TerminalOrigen { get; set; }

    public string TerminalDestino { get; set; }

    public EstadoTraslado EstadoSolicitudTraslado { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.Now.AddDays(-7);

    public DateTime FechaFin { get; set; } = DateTime.Now;

    public int IdControlTransporte { get; set; }

    public int FolioControlTransporte { get; set; }

    public int IdFolioServicio { get; set; }

    public int FolioServicio { get; set; }

    public string DescripcionServicio { get; set; }

}
