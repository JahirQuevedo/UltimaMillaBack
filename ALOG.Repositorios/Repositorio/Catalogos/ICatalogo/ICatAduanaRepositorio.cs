using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatAduanaRepositorio
    {
        CatAduana obtenerAduanaPorNombre(string pNombre);
        CatAduana obtenerPorIdAduana(int pIdAduana);


    }
}
