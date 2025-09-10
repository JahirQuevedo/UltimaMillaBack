namespace ALOG.Modelos;

[Table("WMS_029_CODIGO_DESPERFECTO", Schema = "WMS")]
public class CodigoDesperfecto : BaseEntity
{

    [Key]
    [Column("nIdCodigoDesperfecto029")]
    public int Id { get; set; }

    [Column("sClave")]
    [MaxLength(10)]
    public string Clave { get; set; }

    [Column("sDescripcion")]
    [MaxLength(250)]
    public string Descripcion { get; set; }

    [Column("bActivo")]
    public bool Activo { get; set; }

}
