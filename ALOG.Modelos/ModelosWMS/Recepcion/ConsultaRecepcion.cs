using ALOG.Enums;

namespace ALOG.Modelos;

public class ConsultaRecepcion : BaseEntity
{

    public int FolioTarja { get; set; }

    public string ClaveReferenciaTarja { get; set; }

    public TipoClasificacionRecepcion TipoClasificacionRecepcion { get; set; }

    public string IdMercancia { get; set; } 

    public int IdInventario { get; set; }

    public bool TieneAveria { get; set; }

    public bool RequiereConfirmarRecepcion { get; set; }

    public bool ConfirmaRecepcionMercancia { get; set; }

    public int IdTarja { get; set; }



}
