using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Control;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatSistemasRepositorio
    {

        CatSistemas obtenerSistema(int id);

        ICollection<CatSistemas> obtenerSistemas();

        bool crearSistema(SistemaRegistroDTO sistemaRegistroDTO, out string strError);

        bool bajaSistema(int IdCatSistema, out string strError);

        bool actualizarSistema(int IdCatSistemas, SistemaRegistroDTO sistemaRegistroDTO, out string strError);

        bool validaCredenciales(string userSistema, string passSistema, out string strError);

        CatSistemas obtieneSistema(string userSistema, string passSistema, out string strError);

        Task<SistemaLoginRespuestaDTO> Login(SistemaLoginDTO usuarioLoginDto);



    }
}
