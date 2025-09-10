using ALOG.Repositorios.Data;
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Repositorio.Control.IControl;
using Microsoft.Data.SqlClient;

namespace ALOG.Repositorios.Repositorio.Control
{
    public class ControlSistemaRepositorio : IControlSistemaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ControlSistemaRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public CatSistemas obtieneSistema(int IdSistema, out string strError)
        {
            strError = "";
            try
            {
                return _db.catSistemas.Where(x => x.IdCatSistema == IdSistema).FirstOrDefault();
            }
            catch (SqlException sqlEX)
            {
                strError = sqlEX.Message;
                return null;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        public CatSistemas obtieneSistema(string userSistema, string passSistema, out string strError)
        {
            strError = "";
            try
            {
                return _db.catSistemas.Where(a => a.userSistema.Equals(userSistema) && a.passSistema.Equals(passSistema)).FirstOrDefault();

            }
            catch (SqlException sqlEx)
            {
                strError = sqlEx.Message;
                return null;
            }
            catch (Exception ex)
            {

                strError = ex.Message;
                return null;
            }
        }


        public bool validaCredenciales(string userSistema, string passSistema, out string strError)
        {
            throw new NotImplementedException();
        }
    }
}
