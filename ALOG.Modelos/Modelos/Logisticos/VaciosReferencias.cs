using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALOG.Modelos.Modelos.Logisticos
{
    public class VaciosReferencias
    {

        [Key]
        public int IdReferencia { get; set; }
        public int Ticket { get; set; }
        [Required]
        public string Transporte_RFC { get; set; }
        [Required]
        public string Transporte_RazonSocial { get; set; }

        public int TrasporteId { get; set; }
        [Required]
        public string Transporte_Usuario { get; set; }
        [Required]
        public string Transporte_UsuarioEmail { get; set; }
        public string Comentarios { get; set; }

        public int TipoReferencia { get; set; }
        public ICollection<VaciosContenedores> Contenedores { get; set; }
        public string Procesado { get; set; } = "N";
        public DateTime FechaProcesado { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
        public string EstadoReferencia { get; set; } = "A";

        [ForeignKey("catReferenciaEstado")]
        public int IdCatReferenciaEstado { get; set; }
        public CatReferenciaEstado catReferenciaEstado { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }
    }
}
