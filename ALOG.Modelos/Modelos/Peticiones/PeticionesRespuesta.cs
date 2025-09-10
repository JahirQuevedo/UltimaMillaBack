using System.Net;

namespace ALOG.Modelos.Modelos.Vacios
{
    public class PeticionesRespuesta
    {

        public PeticionesRespuesta()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = false;
        public List<string> ErrorMessages { get; set; }
        //public object Result { get; set; }
        public int IdReferenciaALO { get; set; }
        public int IdOrdenServicio { get; set; }

    }
}
