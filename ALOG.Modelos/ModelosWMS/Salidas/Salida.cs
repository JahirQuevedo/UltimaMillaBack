using ALOG.Enums;
using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos;

[Table("WMS_039_SALIDA", Schema = "WMS")]
public class Salida : BaseEntity
{

    [Key]
    [Column("nIdSalida039")]
    public int Id { get; set; }

    [Column("nIdOrdenSalida035")]
    public int? IdOrdenSalida { get; set; }

    [ForeignKey("IdOrdenSalida")]
    public virtual OrdenSalida OrdenSalida { get; set; }

    [Column("nFolio")]
    public int Folio { get; set; }

    [Column("nEstado")]
    public EstadoSalida Estado { get; set; }

    [Column("dFechaSalida")]
    public DateTime? FechaSalida { get; set; }

    [Column("dFechaSalidaManual")]
    public DateTime? FechaSalidaManual { get; set; }

    [Column("sMotivoCancelacion")]
    [MaxLength(4000)]
    public string? MotivoCancelacion { get; set; }

    [Column("nIdCatEmpresa")]
    public int IdEmpresa { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual CatEmpresas Empresa { get; set; }

}
