using System;

namespace ALOG.Modelos.ModelosWMS.Reportes.Salidas;

public class ReporteDetalleTarjaLiberacion : BaseEntity
{

    public int NumeroRegistro { get; set; }

    public string Numeros { get; set; }

    public string TerminalOrigen { get; set; }

    public string TerminalDestino { get; set; }

    public string Distribuidor { get; set; }

    public string CodigoDannios { get; set; }

    public string Comentarios { get; set; }

}
