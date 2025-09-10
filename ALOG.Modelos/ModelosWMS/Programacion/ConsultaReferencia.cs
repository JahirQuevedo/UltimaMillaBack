using ALOG.Enums;

namespace ALOG.Modelos;

public class ConsultaReferencia : BaseEntity
{
    public int IdReferencia { get; set; }

    public string Folio { get; set; }

    public string RazonSocialCliente { get; set; }

    public TipoOperacionAduanera TipoOperacionAduanera { get; set; }

    public EstadoReferencia Estado { get; set; }

    public string Mercancias { get; set; }

    public TipoFechaEdicion tipoFechaEdicion { get; set; }

    public DateTime Fecha { get; set; }


}
