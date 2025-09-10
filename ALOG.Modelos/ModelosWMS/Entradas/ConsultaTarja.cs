using ALOG.Enums;

//ROM : Se agrega identificador de la Tarja

namespace ALOG.Modelos;

public class ConsultaTarja : BaseEntity
{
    public int IdTarja { get; set; }

    public int IdReferencia { get; set; }

    public int Folio { get; set; }

    public string FolioReferencia { get; set; }

    public string RazonSocialCliente { get; set; }

    public string ClaveReferenciaTarja { get; set; }

    public EstadoTarja EstadoTarja { get; set; }

    public TipoServicioTarja TipoServicioTarja { get; set; }




}
