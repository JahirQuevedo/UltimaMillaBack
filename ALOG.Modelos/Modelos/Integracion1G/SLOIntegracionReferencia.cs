using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.Orden;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ALOG.Modelos.Modelos.Integracion1G
{
    [Table("integracionReferencia", Schema = "SLO")]
    public class SLOIntegracionReferencia
    {
        [Key]
        public int IdIntReferencia { get; set; }
        
        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }

        public string? IdCompaniaExterna { get; set; }
        public string? ReferenciaALO {  get; set; }
        public string? ReferenciaClienteExterno { get; set; }
        public string? ClaveClienteExterno { get; set; }
        public string? Aduana { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public bool Enviado { get; set; }
        public string? Estado1G { get; set; }
        public string? RespuestaWS1G { get; set; }

        public ICollection <SLOIntegraFacturaEnc> SLOintegracionFacturaEnc { get; set; }
    }

}
