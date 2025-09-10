using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;

namespace ALOG.Modelos.Modelos.Logisticos
{
    [Table("peticionesReferencias", Schema = "SLO")]
    public class SLOPeticionesReferencias
    {
        [Key]
        public int IdReferencia { get; set; }

        public int? Ticket { get; set; }
        public string? Transporte_RFC { get; set; }
        public string? Transporte_RazonSocial { get; set; }
        public int? TrasporteId { get; set; }
        public string? Transporte_Usuario { get; set; }
        public string? Transporte_UsuarioEmail { get; set; }
        public string? Comentarios { get; set; }
        public int? TipoReferencia { get; set; }
        public string? Procesado { get; set; }
        public string? EstadoReferencia { get; set; }


        [ForeignKey("catReferenciaEstado")]
        public int? IdCatReferenciaEstado { get; set; }
        public CatReferenciaEstado catReferenciaEstado { get; set; }

        public DateTime? FechaProcesado { get; set; }
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
        public DateTime? FechaSolicitud { get; set; } = DateTime.Now;
        public DateTime? FechaCierre { get; set; }

        public bool Activo { get; set; } = true;

        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }

        public ICollection <SLOPeticionesContenedores> SLOpeticionesContenedores { get; set; }
    }
}
