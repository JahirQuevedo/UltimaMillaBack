using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatNavierasRepositorio : GenericoRepositorio<CatNavieras>, ICatNavieraRepositorio
    {
        private readonly ApplicationDbContext _db;
        //private readonly ILogger<CatNavierasRepositorio> _logger;

        public CatNavierasRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
            //_logger = logger;

        }


        public CatNavieras obtenerPorRFC(string pRFC)
        {

            try
            {
                var objNaviera = _db.catNavieras.Where(x => x.RFC == pRFC).FirstOrDefault();
                if (objNaviera == null)
                {
                    return null;
                }
                return objNaviera;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public CatNavieras ObtenerPorID(int pID)
        {

            try
            {
                var objNaviera = _db.catNavieras.Where(x => x.IdCatNaviera == pID).FirstOrDefault();
                if (objNaviera == null)
                {
                    return null;
                }
                return objNaviera;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<CatNavieras> ObtenerPorRazonSocial(string pRazonSocial)
        {
            try
            {
                var objNaviera = _db.catNavieras.Where(n => n.RazonSocial.Contains(pRazonSocial)).ToList();
                if (objNaviera != null)
                    return objNaviera;
                else 
                    return null;
            }
            catch (Exception) 
            {
                return null;
            }
        }
    }
}
