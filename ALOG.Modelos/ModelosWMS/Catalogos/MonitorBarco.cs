namespace ALOG.Modelos;

public class MonitorBarco : BaseEntity
{

    public int IdBarco { get; set; } = 0;
    public string Nombre { get; set; } = string.Empty;

    public int IdNaviera { get; set; } = 0;

    public string Naviera { get; set; } = string.Empty;

    public int IdPais { get; set; } = 0;

    public string Pais { get; set; } = string.Empty;



}
