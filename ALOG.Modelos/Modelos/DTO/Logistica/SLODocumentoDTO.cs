using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace ALOG.Modelos.Modelos.DTO.Logistica
{
    public class SLODocumentoDTO
    {
        public int IdOrden { get; set; }
        public int IdUsuario { get; set; }
        public string TipoDocumento { get; set; }
        public string Identificador { get; set; }
        public IFormFile File { get;set; }
    }

}
