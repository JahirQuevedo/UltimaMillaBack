using ALOG.Enums;

namespace ALOG.Modelos;

public class ConsultaFolioServicio : BaseEntity
{
    public int IdFolioServicio { get; set; }

    public int IdTarja { get; set; }

    public EstadoFolioServicio EstadoFolioServicio { get; set; }

    public string DescripcionServicio { get; set; }

    public string SerieFactura { get; set; }

    public string Factura { get; set; }
    
    public int IdReferencia { get; set; }

    public int IdInventario { get; set; }

}
