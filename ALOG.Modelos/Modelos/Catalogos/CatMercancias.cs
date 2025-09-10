using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catMercancias", Schema = "dbo")]
    public class CatMercancias : IActivable
    {
        [Key]
        public int IdCatMercancia { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(200)]
        public string Acronimo { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Required]
        [ForeignKey(nameof(catEmpresas))]
        public int IdCatEmpresa { get; set; }

        [Required]
        [ForeignKey(nameof(catLineaNegocio))]
        public int IdCatLineaNegocio { get; set; }

        [Required]
        [ForeignKey(nameof(catUsuarios))]
        public int IdUsuarioRegistro { get; set; }

        // Relaciones de navegación

        public virtual CatEmpresas catEmpresas { get; set; }

        public virtual CatLineaNegocio catLineaNegocio { get; set; }

        public virtual CatUsuarios catUsuarios { get; set; }
    }
}
