using ALOG.Enums;

namespace ALOG.Modelos;

public class MonitorLiberacionInventario : BaseEntity
{

    public int IdLiberacion { get; set; }

    public int FolioLiberacion { get; set; }

    public EstadoLiberacion EstadoLiberacion { get; set; }

    public DateTime FechaLiberacion { get; set; }

    public TipoMercanciaInventario TipoMercanciaInventario { get; set; }

    public string Marcas { get; set; }

    public string Modelo { get; set; }

    public int IdTarja { get; set; }    
    
    public int FolioTarja { get; set; }

    public string ClaveReferenciaTarja { get; set; }

    public int Cantidad { get; set; }

    public decimal Peso { get; set; }

    public int IdControlTransporte { get; set; }

    public int FolioViaje { get; set; }

    public string ClaveViaje { get; set; }

    public string Placas { get; set; }

    public string NumeroEconomico { get; set; }

    public string LineaTransportista { get; set; }

    public string OperadorTransporte { get; set; }

    public string TipoUnidadTransporte { get; set; } 

    public int IdCatCliente { get; set; }

    public string Cliente { get; set; }

    public int IdManiobristaDestino { get; set; }

    public string ManiobristaDestino { get; set; }

    public DateTime FechaSalidaProgramada { get; set; }

    public int TotalInventarioLiberacion { get; set; }

    public int TotalInventarioEmbarcado { get; set; }

    public List<MonitorInventarioAlmacen> ListaMonitorInventarioAlmacen { get; set; }



}
