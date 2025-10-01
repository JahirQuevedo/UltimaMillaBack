using ALOG.Modelos.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catDocumentosLNegocio", Schema = "dbo")]
    public class CatDocumentosLNegocio : IActivable
    {
        [Key]
        public int IdCatDocumentosLNegocio { get; set; }
        [ForeignKey(nameof(CatLineaNegocio))]
        public int IdCatLineaNegocio { get; set; }
        [ForeignKey(nameof(CatDocumentos))]
        public int IdCatDocumento { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        [ForeignKey(nameof(CatUsuarios))]
        public int IdCatUsuarios { get; set; }

        public virtual CatLineaNegocio CatLineaNegocio { get; set; }
        public virtual CatDocumentos CatDocumentos { get; set; }
        public virtual CatUsuarios CatUsuarios { get; set;}
    }
}
