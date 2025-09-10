using System;

namespace ALOG.Modelos.ModelosWMS.Reportes.Salidas;

public class ReporteTarjaLiberacion : BaseEntity
{

    public int FolioLiberacion { get; set; }

    public string Viaje { get; set; }

    public string Marca { get; set; }

    public string Modelo { get; set; }

    public DateTime FechaHoraIngreso { get; set; }

    public DateTime FechaHoraSalida { get; set; }

    public DateTime FechaLiberacion { get; set; }

    public string Operador { get; set; }

    public string Placas { get; set; }

    public string NumeroEconomico { get; set; }

    public string Transporte { get; set; }

    public List<ReporteDetalleTarjaLiberacion> ListaReporteDetalleTarjaLiberacion { get; set; }

}
