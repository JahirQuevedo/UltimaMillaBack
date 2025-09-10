using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatUsuariosAduanaRepositorio : GenericoRepositorio<CatUsuariosAduanas>, ICatUsuarioAduanaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CatUsuariosAduanaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public CatUsuariosAduanas existeUsuarioAduana(int IdCatAduana, int IdCatUsuario)
        {
            try
            {
                return _db.catUsuariosAduanas.FirstOrDefault(x => x.IdCatAduana == IdCatAduana && x.IdCatUsuario == IdCatUsuario);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public ICollection<CatUsuariosAduanas> ObtenerAduanasPorUsuario(int IdCatUsuario)
        {
            try
            {
                return _db.catUsuariosAduanas.Where(x => x.IdCatUsuario == IdCatUsuario).ToList();
            }
            catch (Exception)
            {

                return null;
            }

        }

        public ICollection<CatUsuariosAduanas> ObtenerUsuariosPorAduana(int IdCatAduana)
        {
            try
            {
                return _db.catUsuariosAduanas.Where(x => x.IdCatAduana == IdCatAduana).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }



    }
}
