using ALOG.Enums;

namespace ALOG.Modelos;

[Table("WMS_027_SOLICITUD_INGRESO", Schema = "WMS")]
public class SolicitudIngreso : BaseEntity
{

    [Key]
    [Column("nIdSolicitudIngreso027")]
    public int Id { get; set; }

    [Column("nFolio")]
    public int Folio { get; set; }

    [Column("nEstado")]
    public EstadoSolicitudIngreso? EstadoIngreso { get; set; }


}