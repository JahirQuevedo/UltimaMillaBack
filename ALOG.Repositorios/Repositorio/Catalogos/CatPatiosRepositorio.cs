using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatPatiosRepositorio : ICatPatiosRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CatPatiosRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public CatPatios obtenerPatioRFC(string pRFC)
        {
            try
            {
                //CAMBIO RAZE
                //var objPatio = _db.catPatios.Where(x => x.catProveedoresPatios.Any(p => p.catProveedores.RFC == pRFC)).FirstOrDefault();
                //return objPatio;
                return null;

            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }

}
