using ALOG.Enums;

namespace ALOG.Modelos;

public class ConsultaCtrlTransporte : BaseEntity
{

    public int IdControlTransporte { get; set; }

    public string FolioTurno { get; set; }

    public string NombreEstadoTurno { get; set; }

    public string NombreTransportista { get; set; }

    public string Placas { get; set; }

    public string NumeroEconomico { get; set; }

    public string TerminalOrigen { get; set; }

    public string TerminalDestino { get; set; }

    public TipoViaje TipoViaje { get; set; }

    public EstadoTurno EstadoTurno { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.Now.AddDays(-7);

    public DateTime FechaFin { get; set; } = DateTime.Now;

}
