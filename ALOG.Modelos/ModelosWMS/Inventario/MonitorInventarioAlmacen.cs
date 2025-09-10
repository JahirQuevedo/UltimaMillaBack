using System.Text;
using ALOG.Enums;

namespace ALOG.Modelos;

public class MonitorInventarioAlmacen
{

    public int IdInventario { get; set; }

    public int IdTarja { get; set; }

    public string ClaveReferenciaTarja { get; set; }

    public int IdReferencia { get; set; }

    public string FolioReferencia { get; set; }

    public int IdOrdenServicio { get; set; }
    
    public int IdOrdenSalida { get; set; }

    public int IdOrdenSalidaInventario { get; set; }

    public int IdCatCliente { get; set; }

    public string RazonSocialCliente { get; set; }

    public TipoOperacionAduanera TipoOperacionAduanera { get; set; }

    public string Numeros { get; set; }

    public string Marcas { get; set; }

    public string Modelo { get; set; }

    public int Cantidad { get; set; }

    public decimal Peso { get; set; }

    public int Estadias { get; set; }

    public DateTime FechaRecoleccion { get; set; }

    public DateTime FechaIngreso { get; set; }

    public DateTime FechaEmbarque { get; set; }

    public DateTime FechaVerificacion { get; set; }

    public DateTime FechaSalida { get; set; }

    public string BookingBl { get; set; }

    public string FolioViaje { get; set; }

    public string NombreBuque { get; set; }

    public string PaisOrigen { get; set; }

    public string PuertoOrigen { get; set; }

    public string PaisDestino { get; set; }

    public string PuertoDestino { get; set; }

    public bool ProcesoRecepcionCompletado { get; set; }

    public bool RecoleccionTarjaConfirmada { get; set; }

    public bool IngresoTarjaConfirmada { get; set; }

    public bool TieneAveria { get; set; }

    public string CodigoDannios { get; set; }

    public string ComentariosDannios { get; set; }

    public string ZonalAlmacen { get; set; }

    public string Ubicacion { get; set; }

    public string Observaciones { get; set; }

}
