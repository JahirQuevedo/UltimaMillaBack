using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ALOG.Modelos.Modelos.Integracion1G
{
    public class IntegraReferencia
    {
        [Key]
        public int IdIntReferencia { get; set; }
        [Required]
        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }

        [Required]
        public string IdCompaniaExterna { get; set; }
        [Required]
        public string ReferenciaALO { get; set; }
        [Required]
        public string ReferenciaClienteExterno { get; set; }
        [Required]
        public string ClaveClienteExterno { get; set; }
        [Required]
        public string Aduana { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime? FechaEnvio { get; set; }
        [Required]
        public bool Enviado { get; set; } = false;
        public string Estado1G { get; set; }
        public string RespuestaWS1G { get; set; }
        [JsonIgnore]
        public virtual ICollection<IntegraAnticipoSol> IntegracionAnticipoSol { get; set; }
    }
}
