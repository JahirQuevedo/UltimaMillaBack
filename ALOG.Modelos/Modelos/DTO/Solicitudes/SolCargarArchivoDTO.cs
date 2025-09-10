using Microsoft.AspNetCore.Http;

namespace ALOG.Modelos.Modelos.DTO.Solicitudes
{
    public class SolCargarArchivoDTO
    {
        //public string NombreArchivo { get; set; }
        //public string ArchivoBase64 { get; set; }
        public int IdOrden { get; set; }
        public int IdReferencia { get; set; }
        public int IdContenedor { get; set; }
        public int IdServicio { get; set; }
        public int IdCatDocumento { get; set; }
        public int IdCatLineaNegocio { get; set; }
        public IFormFile File { get; set; }

    }
}
