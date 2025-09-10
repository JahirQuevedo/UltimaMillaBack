using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatProveedoresRepositorio
    {

        public CatProveedores obtenerPorRFC(string pRFC);
    }
}
