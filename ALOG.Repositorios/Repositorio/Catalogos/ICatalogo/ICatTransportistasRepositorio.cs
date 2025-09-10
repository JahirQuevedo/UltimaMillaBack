using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatTransportistasRepositorio
    {
        CatTransportistas ObtenerPorRFC(string pRFC);
    }
}
