using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos;

[Table("WMS_040_SALIDA_CONTROL_TRANSPORTE", Schema = "WMS")]
public class SalidaControlTransporte : BaseEntity
{

    [Key]
    [Column("nIdSalidaControlTransporte040")]
    public int Id { get; set; }

    [Column("nIdOrdenSalida035")]
    public int? IdOrdenSalida { get; set; }

    [ForeignKey("IdOrdenSalida")]
    public virtual OrdenSalida OrdenSalida { get; set; }

    [Column("nIdSalida039")]
    public int? IdSalida { get; set; }

    [ForeignKey("IdSalida")]
    public virtual Salida Salida { get; set; }

    [Column("nIdControlTransporte024")]
    public int? IdControlTransporte { get; set; }

    [ForeignKey("IdControlTransporte")]
    public virtual ControlTransporte ControlTransporte { get; set; }

    [Column("bActivo")]
    public bool Activo { get; set; } = false;

    [Column("dFechaCancelacion")]
    public DateTime? FechaCancelacion { get; set; }

    [Column("sMotivoCancelacion")]
    [MaxLength(4000)]
    public string? MotivoCancelacion { get; set; }

    [Column("nIdCatUsuarioCancelacion")]
    public int? IdUsuarioCancelacion { get; set; }

    [ForeignKey("IdUsuarioCancelacion")]
    public virtual CatUsuarios UsuarioCancelacion { get; set; }

}
