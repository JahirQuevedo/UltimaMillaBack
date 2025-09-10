namespace ALOG.Modelos;

[Table("WMS_038_LIBERACION_INVENTARIO", Schema = "WMS")]
public class LiberacionInventario : BaseEntity
{

    [Key]
    [Column("nIdLiberacionInventario038")]
    public int Id { get; set; }

    [Column("nIdOrdenSalidaInventario037")]
    public int? IdOrdenSalidaInventario { get; set; }

    [Column("nIdTarja015")]
    public int? IdTarja { get; set; }

    [ForeignKey("IdTarja")]
    public virtual Tarja Tarja { get; set; }

    [Column("nNumeroPartida")]
    public int NumeroPartida { get; set; }

    [Column("nPeso")]
    public decimal? Peso { get; set; }

    [Column("nCantidad")]
    public int? Cantidad { get; set; }

    [Column("bLiberacionParcial")]
    public bool LiberacionParcial { get; set; } = false;

}
