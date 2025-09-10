using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catTipoOperacionComercio", Schema = "dbo")]
    public class CatTipoOperacionComercio : IActivable
    {
        [Key]
        public int IdCatTipoOperComercio { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        [Required]
        public int IdCatUsuario { get; set; }
        [Required]
        [StringLength(10)]
        public string Acronimo { get; set; }
    }
}
