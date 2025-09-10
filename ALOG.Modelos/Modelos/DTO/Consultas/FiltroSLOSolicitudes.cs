using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.DTO.Consultas
{
    public class FiltroSLOSolicitudes
    {
        public string? RazonSocial { get; set; }
        public int? IdSLOSolicitud { get; set; }
        public int? IdCatCliente { get; set; }
        public int? IdCatUbicacionOrigen { get; set; }
        public int? IdCatUbicacionDestino { get; set; }
        public int? IdCatTipoOperComercio { get; set; }
        public int? IdCatTipoCarga { get; set; }
        public int? IdCatUsuario { get; set; }
        public int? IdCatTipoEstado { get; set; }
        public int? IdCatTipoOperacion { get; set; }
        public int? IdOrden { get; set; }

        public string ReferenciaCliente { get; set; }
        public string Booking { get; set; }

        public DateTime? FechaPosicionamientoInicio { get; set; }
        public DateTime? FechaPosicionamientoFin { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public bool? Activo { get; set; }
    }
}
