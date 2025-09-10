using System;
using ALOG.Enums;

namespace ALOG.Modelos;

public class ConsultaControlEmbarque : BaseEntity
{

    public TipoBusquedaEmbarque TipoBusquedaEmbarque { get; set; }
    
    public string ValorBusqueda { get; set; }

}
