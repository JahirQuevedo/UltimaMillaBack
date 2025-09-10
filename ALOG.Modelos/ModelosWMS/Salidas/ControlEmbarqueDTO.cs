using System;
using ALOG.Enums;

namespace ALOG.Modelos;

public class ControlEmbarqueDTO
{

    public int IdLiberacion { get; set; }

    public int IdTarjaSalida { get; set; }

    public int IdControlTransporte { get; set; }

    public TipoBusquedaEmbarque tipoBusquedaEmbarque { get; set; }
    
}
