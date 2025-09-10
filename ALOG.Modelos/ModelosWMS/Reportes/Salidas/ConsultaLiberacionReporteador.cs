using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Modelos.ModelosWMS.Reportes.Salidas
{
    public class ConsultaLiberacionReporteador : BaseEntity
    {
        public int IdOrdenSalida { get; set; }
        public ArchivoBase ArchivoBase { get; set; }
    }
}
