namespace ALOG.Modelos;

[Table("WMS_033_AGENTE_ADUANAL", Schema = "WMS")]
public class AgenteAduanal : BaseEntity
{

    [Key]
    [Column("nIdAgenteAduanal033")]
    public int Id { get; set; }

    [Column("sPatente")]
    [MaxLength(4)]
    public string Patente { get; set; }

    [Column("sNombre")]
    [MaxLength(100)]
    public string Nombre { get; set; }

    [Column("sRFC")]
    [MaxLength(100)]
    public string? RFC { get; set; }

    [Column("sAlias")]
    [MaxLength(100)]
    public string? Alias { get; set; }

    [Column("sCURP")]
    [MaxLength(100)]
    public string? CURP { get; set; }

    [Column("bEsAgenteDeCarga")]
    public bool? EsAgenteDeCarga { get; set; }

    [Column("bActivo")]
    public bool Activo { get; set; }

}
