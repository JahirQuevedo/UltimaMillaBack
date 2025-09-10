namespace ALOG.Modelos;

[Table("WMS_008_ALMACEN", Schema = "WMS")]
public class Almacen : BaseEntity
{
    [Key]
    [Column("nIdAlmacen008")]
    public int Id { get; set; }

    [Column("sClave")]
    [MaxLength(6)]
    public string Clave { get; set; }

    [Column("sDescripcion")]
    [MaxLength(100)]
    public string? Descripcion { get; set; }

    [Column("sColor")]
    [MaxLength(7)]
    public string? Color { get; set; }

    [Column("bActivo")]
    public bool Activo { get; set; }

    [Column("nIdTipoCargaAlmacen007")]
    public int? IdTipoCargaAlmacen { get; set; }

    [ForeignKey("IdTipoCargaAlmacen")]
    public virtual TipoCargaAlmacen TipoCargaAlmacen { get; set; }

}
