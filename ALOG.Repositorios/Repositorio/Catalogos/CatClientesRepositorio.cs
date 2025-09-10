using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Utilerias;
using Microsoft.Data.SqlClient;

namespace ALOG.Repositorios.Repositorio.Catalogos
{

    public class CatClientesRepositorio : ICatClientesRepositorio
    {
        private readonly ApplicationDbContext _db;
        UtileriasCifrados clsCifrados = new UtileriasCifrados();
        private string claveSecreta;
        public CatClientesRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public CatClientes obtenerClienteRFC(string pRFC)
        {
            try
            {
                //CatClientes objCatClientes = _db.catClientes.Where(c => c.RFC == pRFC).FirstOrDefault();
                return _db.catClientes.Where(c => c.RFC == pRFC && c.Activo == true).FirstOrDefault();
                //return objCatClientes;
            }
            catch (SqlException sqlex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
