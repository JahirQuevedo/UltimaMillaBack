using ALOG.Modelos.Modelos.DTO;
using Newtonsoft.Json;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatTipoEventosCron : IActivable
    {
        [Key]
        public int IdCatTipoEventoCron { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdRegistroUsuario { get; set; }
    }
}
