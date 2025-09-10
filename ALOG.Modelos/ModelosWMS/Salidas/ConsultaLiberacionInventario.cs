using ALOG.Enums;

namespace ALOG.Modelos;

public class ConsultaLiberacionInventario : BaseEntity
{

    public int IdOrdenSalida { get; set; }
    
    public int FolioLiberacion { get; set; }

    public int FolioTarjaEntrada { get; set; }

    public EstadoLiberacion EstadoLiberacion { get; set; }

    public int FolioTarjaSalida { get; set; }

    public string ConocimientoBL { get; set; }

    public int IdCliente { get; set; }

    public string Cliente { get; set; }

    public int IdInventario { get; set; }

    public string IdMercancia { get; set; }

    public string NumerosPartida { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.Now.AddDays(-7);

    public DateTime FechaFin { get; set; } = DateTime.Now;


}
