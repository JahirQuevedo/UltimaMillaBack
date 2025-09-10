using ALOG.Modelos.Modelos.DTO.Respuestas;
using System.Net;

namespace ALOG.Repositorios.Utilerias
{
    public class UtileriasRespuestas
    {
        public RespuestaGenericaDTO InicializaRespuesta()
        {
            RespuestaGenericaDTO objRespuestaGenericaDTO = new RespuestaGenericaDTO();
            objRespuestaGenericaDTO.IsSuccess = false;
            objRespuestaGenericaDTO.strMensaje = string.Empty;
            objRespuestaGenericaDTO.StatusCode = HttpStatusCode.NotFound;
            objRespuestaGenericaDTO.lstrErrorMessages = new List<string>();
            objRespuestaGenericaDTO.Entidad = null;
            return objRespuestaGenericaDTO;
        }

        public RespuestaGenericaDTO InicializaRespuestaIncorrecta()
        {
            RespuestaGenericaDTO objRespuestaGenericaDTO = new RespuestaGenericaDTO();
            objRespuestaGenericaDTO.IsSuccess = false;
            objRespuestaGenericaDTO.strMensaje = string.Empty;
            objRespuestaGenericaDTO.StatusCode = HttpStatusCode.BadRequest;
            objRespuestaGenericaDTO.lstrErrorMessages = new List<string>();
            objRespuestaGenericaDTO.Entidad = null;
            return objRespuestaGenericaDTO;
        }
        public RespuestaGenericaDTO InicializaRespuestaCorrecta()
        {
            RespuestaGenericaDTO objRespuestaGenericaDTO = new RespuestaGenericaDTO();
            objRespuestaGenericaDTO.IsSuccess = true;
            objRespuestaGenericaDTO.strMensaje = string.Empty;
            objRespuestaGenericaDTO.StatusCode = HttpStatusCode.OK;
            objRespuestaGenericaDTO.lstrErrorMessages = new List<string>();
            objRespuestaGenericaDTO.Entidad = null;
            return objRespuestaGenericaDTO;
        }
    }
}
