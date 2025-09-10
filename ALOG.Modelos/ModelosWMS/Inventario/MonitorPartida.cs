namespace ALOG.Modelos;

public class MonitorPartida : BaseEntity
{

    public int IdPartida { get; set; }

    public int IdInventario { get; set; }

    public string ClaveReferenciaTarja { get; set; }

    public int NumeroPartida { get; set; }

    public string Marcas { get; set; }

    public string Numeros { get; set; }

    public string Mercancia { get; set; }

    public string Modelo { get; set; }

    public int BultosInicial { get; set; }

    public decimal PesoInicial { get; set; }

    public int SaldoBultos { get; set; }

    public decimal SaldoPeso { get; set; }

    public int Estadias { get; set; }

    public int TotalServicios { get; set; }

    public DateTime FechaRecoleccion { get; set; }

    public DateTime FechaDescarga { get; set; }

    public DateTime FechaTraslado { get; set; }

    public DateTime FechaIngreso { get; set; }

    public int IdZonaAlmacenaje { get; set; }

    public string ClaveZonaAlmacenaje { get; set;}

    public int IdUbicacion { get; set; }

    public string ClaveUbicacion { get; set; }

    public string Grupo { get; set; }

    public bool Existencia { get; set; }


}
