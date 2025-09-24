using ALOG.Modelos.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catTipoOperacionesSLO", Schema = "dbo")]
    public class CatTipoOperacionesSLO : IActivable
    {
        [Key]
        public int IdCatTipoOperacionesSLO { get; set; }
        public string Nombre { get; set; }

        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }

        public virtual CatUsuarios catUsuarios { get; set; }
    }
}
