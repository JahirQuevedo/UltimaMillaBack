using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatUsuariosEmpresaRepositorio : GenericoRepositorio<CatUsuariosEmpresa>, ICatUsuariosEmpresaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CatUsuariosEmpresaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public CatUsuariosEmpresa exiteUsuarioEmpresa(int IdEmpresa, int IdCatUsuario)
        {
            try
            {
                return _db.catUsuariosEmpresa.FirstOrDefault(x => x.idCatEmpresa == IdEmpresa && x.IdCatUsuarios == IdCatUsuario);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public CatUsuariosEmpresa exiteUsuarioCliente(int IdCliente, int IdCatUsuario)
        {
            try
            {
                return _db.catUsuariosEmpresa.FirstOrDefault(x => x.IdCatCliente == IdCliente && x.IdCatUsuarios == IdCatUsuario);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public ICollection<CatUsuariosEmpresa> ObtenerEmpresasClientesPorUsuario(int IdCatUsuario)
        {
            try
            {
                return _db.catUsuariosEmpresa.Where(x => x.IdCatUsuarios == IdCatUsuario).ToList();
            }
            catch (Exception)
            {

                return null;
            }

        }


        public ICollection<CatUsuariosEmpresa> ObtenerUsuariosEmpresa(int IdCatEmpresa)
        {
            try
            {
                return _db.catUsuariosEmpresa.Where(x => x.idCatEmpresa == IdCatEmpresa).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public ICollection<CatUsuariosEmpresa> ObtenerUsuariosCliente(int IdCatCliente)
        {
            try
            {
                return _db.catUsuariosEmpresa.Where(x => x.IdCatCliente == IdCatCliente).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
