namespace ALOG.Modelos;

[Table("WMS_031_TIPO_SEVERIDAD", Schema = "WMS")]
public class TipoSeveridad : BaseEntity
{
    [Key]
    [Column("nIdTipoSeveridad031")]
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
