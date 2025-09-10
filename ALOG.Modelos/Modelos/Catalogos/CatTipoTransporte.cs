using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catTipoTransporte", Schema = "dbo")]
    public class CatTipoTransporte : IActivable
    {


        [Key]
        public int IdCatTipoTransporte { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }

        // Relación de navegación
        public virtual CatUsuarios catUsuarios { get; set; }
    }


}
