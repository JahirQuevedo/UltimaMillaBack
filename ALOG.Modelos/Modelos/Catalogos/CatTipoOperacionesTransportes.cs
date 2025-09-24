using ALOG.Modelos.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catTipoOperacionesTransportes", Schema ="dbo")]
    public class CatTipoOperacionesTransportes : IActivable
    {
        [Key]
        public int IdCatTipoOperTransportes { get; set; }

        [ForeignKey(nameof(catTipoOperacionesSLO))]
        public int IdCatTipoOperacionesSLO { get; set; }

        [ForeignKey(nameof(catTipoTransporte))]
        public int IdCatTipoTransporte { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }
        public virtual CatTipoOperacionesSLO catTipoOperacionesSLO { get; set; }
        public virtual CatTipoTransporte catTipoTransporte { get; set; }
        public virtual CatUsuarios catUsuarios { get; set; }
    }
}
