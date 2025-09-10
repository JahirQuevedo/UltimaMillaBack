using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catTipoCarga", Schema = "dbo")]
    public class CatTipoCarga : IActivable
    {
        [Key]
        public int IdCatTipoCarga { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        [Required]
        [MaxLength(15)]
        public string Acronimo { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdCatUsuario { get; set; }

        public virtual CatUsuarios catUsuarios { get; set; }
    }
}
