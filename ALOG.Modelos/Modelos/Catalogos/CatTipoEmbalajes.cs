using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catTipoEmbalajes", Schema = "dbo")]
    public class CatTipoEmbalajes
    {
        [Key]
        public int IdCatTipoEmbalaje { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuarios { get; set; }

        [MaxLength(50)]
        public string? Acronimo { get; set; }

        // Relación de navegación
        public virtual CatUsuarios catUsuarios { get; set; }
    }
}
