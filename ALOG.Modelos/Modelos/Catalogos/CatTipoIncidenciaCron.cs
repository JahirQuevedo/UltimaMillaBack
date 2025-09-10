using ALOG.Modelos.Modelos.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatTipoIncidenciaCron : IActivable
    {
        [Key]
        public int IdCatTipoIncidenciaCron { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdRegistroUsuario { get; set; }
    }
}
