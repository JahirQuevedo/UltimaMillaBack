using ALOG.Modelos.Modelos.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatTipoMoneda : IActivable
    {
        [Key]
        public int IdCatTipoMoneda { get; set; }

        [Required]
        [MaxLength(20)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        public bool Activo { get; set; }
        public string ClaveSAT { get; set; }

        [ForeignKey("catUsuarios")]
        public int IdUsuarioRegistro { get; set; }
        public CatUsuarios catUsuarios { get; set; }
    }
}
