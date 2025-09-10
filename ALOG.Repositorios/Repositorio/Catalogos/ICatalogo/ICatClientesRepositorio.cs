using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatClientesRepositorio
    {
        CatClientes obtenerClienteRFC(string RFC);
    }
}
