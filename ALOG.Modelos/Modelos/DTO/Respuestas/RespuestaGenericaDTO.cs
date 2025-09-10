using System.Net;

namespace ALOG.Modelos.Modelos.DTO.Respuestas
{
    public class RespuestaGenericaDTO
    {


        public RespuestaGenericaDTO()
        {
            lstrErrorMessages = new List<string>();
            IsSuccess = false;
            strMensaje = string.Empty;
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string strMensaje { get; set; } = string.Empty;
        public List<string> lstrErrorMessages { get; set; }
        public object Entidad { get; set; }
        public List<object> Entidades { get; set; }



    }
}
