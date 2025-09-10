using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatAduanaRepositorio : ICatAduanaRepositorio
    {
        GenericoRepositorio<CatAduana> genericoRepositorio;
        private readonly ApplicationDbContext _db;
        public CatAduanaRepositorio(GenericoRepositorio<CatAduana> _genericoRepositorio, ApplicationDbContext db)
        {
            genericoRepositorio = _genericoRepositorio;
            _db = db;
        }

        public CatAduana obtenerAduanaPorNombre(string pNombre)
        {
            CatAduana objcatAduana = _db.catAduana.Where(x => x.Nombre.Equals(pNombre)).FirstOrDefault();
            return objcatAduana;
        }

        public CatAduana obtenerPorIdAduana(int pIdAduana)
        {
            CatAduana objcatAduana = _db.catAduana.Where(x => x.Aduana == pIdAduana).FirstOrDefault();
            return objcatAduana;
        }

        public async Task<bool> Agregar(CatAduana obj)
        {
            //_db.catAduana.Add(obj);
            var objtask = genericoRepositorio.AgregarGenerico(obj);
            return objtask == null ? false : true;
        }




    }
}
