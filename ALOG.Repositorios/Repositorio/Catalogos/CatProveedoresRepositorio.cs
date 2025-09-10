using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatProveedoresRepositorio : ICatProveedoresRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CatProveedoresRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }
        public CatProveedores obtenerPorRFC(string pRFC)
        {
            CatProveedores objcatProveedores = _db.catProveedores.Where(x => x.RFC.Equals(pRFC)).FirstOrDefault();
            return objcatProveedores;
        }

    }
}
