using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catTipoIMO", Schema = "dbo")]
    public class CatTipoIMO : IActivable
    {
        [Key]
        public int IdCatTipoIMO { get; set; }

        public DateTime FechaRegistro { get; set; }

        public bool Activo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }

        // Relación de navegación
        public virtual CatUsuarios catUsuarios { get; set; }
    }
}
