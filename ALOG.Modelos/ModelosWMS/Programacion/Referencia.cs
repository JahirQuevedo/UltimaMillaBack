using ALOG.Enums;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Orden;

namespace ALOG.Modelos;

[Table("WMS_001_REFERENCIA", Schema = "WMS")]
public class Referencia : BaseEntity
{

    [Key]
    [Column("nIdReferencia001")]
    public int Id { get; set; }

    [Column("sFolio")]
    /// <summary>
    /// Es el folio de referencia conformado por PREFIJO+YYYY+MM+DD+CONSECUTIVO DEL DIA [longitud máxima 15]
    /// </summary>
    public required string Folio { get; set; }

    [Column("nEstado")]
    /// <summary>
    /// Estado de la Referencia [1 - Preliminar, 2 - Programado, 3 - Cancelado]
    /// </summary>
    public EstadoReferencia EstadoReferencia { get; set; }

    [Column("nTipoOperacion")]
    /// <summary>
    /// Operacion: 1 - IMPORTACION, 2 - EXPORTACION  
    /// </summary>
    public TipoOperacionAduanera TipoOperacion { get; set; }

    [Column("nTipoMercancia")]
    public TipoMercanciaInventario TipoMercancia { get; set; }

    [Column("sReferenciaClienteExterno")]
    public string ReferenciaClienteExterno { get; set; }

    [Column("sMercancia")]
    public string Mercancia { get; set; }

    [Column("nBultosInicial")]
    public int? BultosInicial { get; set; }

    [Column("nPesoInicial")]
    public decimal? PesoInicial { get; set; }

    [Column("nBultosFinal")]
    public int? BultosFinal { get; set; }

    [Column("nPesoFinal")]
    public decimal? PesoFinal { get; set; }

    [Column("sManifiestoBuque")]
    public string ManifiestoBuque { get; set; }

    [Column("dFechaEntrada")]
    public DateTime? FechaEntrada { get; set; }

    [Column("sObservaciones")]
    public string Observaciones { get; set; }

    [Column("nIdCatAduana")]
    public int? IdAduanaSeccion { get; set; }

    [Column("nIdCatCliente")]
    public int? IdCliente { get; set; }

    [Column("nIdCatProveedor")]
    public int? IdProveedor { get; set; }

    [Column("nIdCatClienteImpoExpo")]
    public int? IdClienteImpoExpo { get; set; }

    [Column("nIdCatEmpresa")]
    public int IdEmpresa { get; set; }

    [Column("nIdViaje002")]
    public int? IdViaje { get; set; }

    [Column("nIdAgenteAduanal033")]
    public int? IdAgenteAduanal { get; set; }

    [Column("nIdOrdenServicio")]
    public int? IdOrdenServicio { get; set; }

    [Column("nIdCatClienteFacturarA")]
    public int? IdClienteFacturarA { get; set; }

    [Column("nIdManiobristaOrigen023")]
    public int? IdManiobristaOrigen { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual CatEmpresas Empresa { get; set; }

    [ForeignKey("IdCliente")]
    public virtual CatClientes Cliente { get; set; }

    [ForeignKey("IdClienteImpoExpo")]
    public virtual CatClientes ClienteImpoExpo { get; set; }

    [ForeignKey("IdProveedor")]
    public virtual CatProveedores Proveedor { get; set; }

    [ForeignKey("IdViaje")]
    public virtual Viaje Viaje { get; set; }

    [ForeignKey("IdAgenteAduanal")]
    public virtual AgenteAduanal AgenteAduanal { get; set; }


    [ForeignKey("IdClienteFacturarA")]
    public virtual CatClientes ClienteFacturarA { get; set; }

    [ForeignKey("IdAduanaSeccion")]
    public virtual CatAduana AduanaSeccion { get; set; }

    [ForeignKey("IdOrdenServicio")]
    public virtual Ordenes OrdenServicio { get; set; }

    [ForeignKey("IdManiobristaOrigen")]
    public virtual Maniobrista ManiobristaOrigen { get; set; }

}
