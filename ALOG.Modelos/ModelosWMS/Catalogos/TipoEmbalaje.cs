namespace ALOG.Modelos;

[Table("WMS_013_TIPO_EMBALAJE", Schema = "WMS")]
public class TipoEmbalaje : BaseEntity
{
    [Key]
    [Column("nIdTipoEmbalaje013")]
    public int Id { get; set; }
    [Column("sClave")]
    [MaxLength(7)]
    public string Clave { get; set; }

    [Column("sDescripcion")]
    [MaxLength(100)]
    public string Descripcion { get; set; }

    [Column("bActivo")]
    public bool Activo { get; set; }
}
