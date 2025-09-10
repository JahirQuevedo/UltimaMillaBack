using ALOG.Enums;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ALOG.Modelos;

public class ConsultaMonitorInventario : BaseEntity
{

    public int TipoFecha { get; set; } = 1;

    public DateTime FechaInicio { get; set; } = DateTime.Now;

    public DateTime FechaFin { get; set; } = DateTime.Now;

    public int IdOrdenSalida { get; set; }

    public int IdCatCliente { get; set; }

    public string RazonSocialCliente { get; set; }

    public string Modelo { get; set; }

    public string Marcas { get; set; }

    public string IDMercancia { get; set; }

    public List<int> ListaIdCatCliente { get; set; }

    public BuscarPorConsultaInventario BuscarPorConsultaInventario { get; set; }

    public string CodigoDannios { get; set; }

    public string ValorBusqueda { get; set; }

    public string ZonaAlmacen { get; set; }

    public string Ubicacion { get; set; }

    public ArchivoBase ArchivoBase { get; set; }

}
