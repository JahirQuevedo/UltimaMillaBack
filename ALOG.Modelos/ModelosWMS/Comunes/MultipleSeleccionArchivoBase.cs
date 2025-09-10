using ALOG.Enums;

namespace ALOG.Modelos;

public partial class MultipleSeleccionArchivoBase : BaseEntity
{

    public List<int> Ids { get; set; }

    public TipoProcesoArchivoControlDocumento TipoProcesoArchivoControlDocumento { get; set; }

    public ArchivoBase ArchivoBase { get; set; }

}
