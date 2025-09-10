using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatTransportitasRepositorio : ICatTransportistasRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CatTransportitasRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }
        public CatTransportistas ObtenerPorRFC(string pRFC)
        {
            CatTransportistas objcatTransportistas = _db.catTransportistas.Where(x => x.RFC.Equals(pRFC)).FirstOrDefault();
            return objcatTransportistas;
        }
    }
}
