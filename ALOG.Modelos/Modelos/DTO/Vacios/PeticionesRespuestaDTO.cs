using System.Net;

namespace ALOG.Modelos.Modelos.DTO.Vacios
{
    public class PeticionesRespuestaDTO
    {
        public PeticionesRespuestaDTO()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        //public object Result { get; set; }
        public int IdReferenciaALO { get; set; }
        public int IdOrdenServicio { get; set; }
    }
}
