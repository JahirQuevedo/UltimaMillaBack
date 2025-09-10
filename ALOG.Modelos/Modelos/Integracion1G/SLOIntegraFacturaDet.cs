using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Logisticos;

namespace ALOG.Modelos.Modelos.Integracion1G
{
    [Table("integracionFacturaDet", Schema = "SLO")]
    public class SLOIntegraFacturaDet
    {
        [Key]
        public int IdIntFacturaDet { get; set; }

        [ForeignKey("SLOintegracionFacturaEnc")]
        public int IddIntFacturaEnc { get; set; }
        public virtual SLOIntegraFacturaEnc SLOintegracionFacturaEnc { get; set; }

        public string ClaveServicio { get; set; }
        public string Cantidad { get; set; }
        public string Precio { get; set; }

        [ForeignKey("SLOpeticionesReferencias")]
        public int IdPeticionesReferencia { get; set; }
        public virtual SLOPeticionesReferencias SLOpeticionesReferencias { get; set; }

        [ForeignKey("SLOPeticionesContenedores")]
        public int IdPeticionesContenedor { get; set; }
        public virtual SLOPeticionesContenedores SLOPeticionesContenedores { get; set; }
        
        public string? Contenedor { get; set; }
        public string? Bl { get; set; }
        public string? CentroCostos { get; set; }
        public string? EIR { get; set; }
        public string? Comentario { get; set; }
        public string? Nota { get; set; }

        [ForeignKey("CatServicios")]
        public int IdCatServicio { get; set; }
        public virtual CatServicios CatServicios { get; set; }

        [ForeignKey("SLOpeticionesServicios")]
        public int Idpservicios {  get; set; }
        public virtual SLOPeticionesServicios SLOpeticionesServicios { get; set; }
    }
}
