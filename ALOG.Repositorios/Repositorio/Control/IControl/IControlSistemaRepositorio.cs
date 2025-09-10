using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;

namespace ALOG.Repositorios.Repositorio.Control.IControl
{
    public interface IControlSistemaRepositorio
    {

        CatSistemas obtieneSistema(int IdSistema, out string strError);

        bool validaCredenciales(string userSistema, string passSistema, out string strError);

        CatSistemas obtieneSistema(string userSistema, string passSistema, out string strError);
    }
}
