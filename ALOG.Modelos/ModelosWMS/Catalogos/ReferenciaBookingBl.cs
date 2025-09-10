namespace ALOG.Modelos;

[Table("WMS_034_REFERENCIA_BOOKING_BL", Schema = "WMS")]
public class ReferenciaBookingBl : BaseEntity
{

    [Key]
    [Column("nIdReferenciaBookingBl034")]
    public int Id { get; set; }

    [Column("sBookingBl")]
    [MaxLength(50)]
    public string BookingBl { get; set; }

    [Column("nIdReferencia001")]
    public int? IdReferencia { get; set; }

    [ForeignKey("IdReferencia")]
    public virtual Referencia Referencia { get; set; }

}
