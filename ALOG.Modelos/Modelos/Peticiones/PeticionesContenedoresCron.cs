using System.Text.Json.Serialization;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Vacios;

namespace ALOG.Modelos.Modelos.Peticiones
{
    [Table("peticionesContenedorCron", Schema = "vacios")]
    public class PeticionesContenedoresCron
    {
        [Key]
        public int IdContenedorCron { get; set; }
        [ForeignKey("catTipoIncidenciaEvento")]
        public int IdCatTipoIncidenciaEvento { get; set; }
        public CatTipoIncidenciaEvento catTipoIncidenciaEvento { get; set; }

        [ForeignKey("catUsuarios")]
        public int IdRegistroUsuario { get; set; }
        public CatUsuarios catUsuarios { get; set; }

        [ForeignKey("peticionesContenedores")]
        public int IdContenedor { get; set; }
        public PeticionesContenedores peticionesContenedores { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Comentarios { get; set; }
        public DateTime FechaEvento { get; set; }

        [ForeignKey("peticionesServicios")]
        public int IdServicio { get; set; } 
        public PeticionesServicios peticionesServicios { get; set; }
    }
}
