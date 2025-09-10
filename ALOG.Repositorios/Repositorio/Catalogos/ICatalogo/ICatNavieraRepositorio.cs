using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatNavieraRepositorio
    {


        CatNavieras obtenerPorRFC(string pRFC);
        CatNavieras ObtenerPorID (int pID);
        List<CatNavieras> ObtenerPorRazonSocial(string pRazonSocial);

    }
}
