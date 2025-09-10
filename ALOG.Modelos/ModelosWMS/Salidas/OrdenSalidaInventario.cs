namespace ALOG.Modelos;

[Table("WMS_037_ORDEN_SALIDA_INVENTARIO", Schema = "WMS")]

public class OrdenSalidaInventario : BaseEntity
{

    [Key]
    [Column("nIdOrdenSalidaInventario037")]
    public int Id { get; set; }

    [Column("nIdOrdenSalida035")]
    public int? IdOrdenSalida { get; set; }

    [ForeignKey("IdOrdenSalida")]
    public virtual OrdenSalida OrdenSalida { get; set; }

    [Column("nIdInventario014")]
    public int? IdInventario { get; set; }

    [ForeignKey("IdInventario")]
    public virtual Inventario Inventario { get; set; }


}
