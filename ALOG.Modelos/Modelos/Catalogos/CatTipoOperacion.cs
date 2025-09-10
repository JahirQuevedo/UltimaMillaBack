using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catTiposOperaciones",Schema = "dbo")]
    public class CatTipoOperacion : IActivable
    {
        [Key]
        public int IdCatTipoOperacion { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(200)]
        public string Acronimo { get; set; }

        [Required]
        [StringLength(1000)]
        public string Descripcion { get; set; }

        [Required]
        public bool Activo { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [ForeignKey("catEmpresa")]
        public int IdCatEmpresa { get; set; }
        public virtual CatEmpresas catEmpresa { get; set; }

        [Required]
        [ForeignKey("catLineaNegocio")]
        public int IdCatLineaNegocio { get; set; }
        public virtual CatLineaNegocio catLineaNegocio { get; set; }

        [Required]
        [ForeignKey("catUsuarios")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuarios { get; set; }                
                             
    }
}
