using ALOG.Enums;
using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos;

[Table("WMS_035_ORDEN_SALIDA", Schema = "WMS")]
public class OrdenSalida : BaseEntity
{

    [Key]
    [Column("nIdOrdenSalida035")]
    public int Id { get; set; }

    [Column("nFolio")]
    public int Folio { get; set; }

    [Column("nTipo")]
    public TipoLiberacion TipoLiberacion { get; set; }

    [Column("nEstado")]
    public EstadoLiberacion Estado { get; set; }

    [Column("sObservaciones")]
    [MaxLength(4000)]
    public string? Observaciones { get; set; }

    [Column("dFechaSalidaProgramada")]
    public DateTime? FechaSalidaProgramada { get; set; }

    [Column("dFechaAutorizacion")]
    public DateTime? FechaAutorizacion { get; set; }

    [Column("dFechaLiberacion")]
    public DateTime? FechaLiberacion { get; set; }

    [Column("sMotivoCancelacion")]
    [MaxLength(4000)]
    public string? MotivoCancelacion { get; set; }

    [Column("nIdCatEmpresa")]
    public int IdEmpresa { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual CatEmpresas Empresa { get; set; }

}
