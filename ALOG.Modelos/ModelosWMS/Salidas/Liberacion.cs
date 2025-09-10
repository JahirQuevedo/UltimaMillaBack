using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos;

[Table("WMS_036_LIBERACION", Schema = "WMS")]
public class Liberacion : BaseEntity
{

    [Key]
    [Column("nIdLiberacion036")]
    public int Id { get; set; }

    [Column("nIdReferencia001")]
    public int? IdReferencia { get; set; }

    [ForeignKey("IdReferencia")]
    public virtual Referencia Referencia { get; set; }

    [Column("nIdOrdenSalida035")]
    public int? IdOrdenSalida { get; set; }

    [ForeignKey("IdOrdenSalida")]
    public virtual OrdenSalida OrdenSalida { get; set; }

    [Column("nIdCatCliente")]
    public int? IdCliente { get; set; }

    [ForeignKey("IdCliente")]
    public virtual CatClientes Cliente { get; set; }

    [Column("nIdAgenteAduanal033")]
    public int? IdAgenteAduanal { get; set; }

    [ForeignKey("IdAgenteAduanal")]
    public virtual AgenteAduanal AgenteAduanal { get; set; }

    [Column("nIdManiobristaDestino023")]
    public int? IdManiobristaDestino { get; set; }

    [ForeignKey("IdManiobristaDestino")]
    public virtual Maniobrista ManiobristaDestino { get; set; }

}
