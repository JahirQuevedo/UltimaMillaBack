using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using ALOG.Repositorios.Repositorio.SPFunciones;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace ALOG.Repositorios.Repositorio.IRepoOrdenes
{
    public class OrdenesRepositorio : GenericoRepositorio<Ordenes>, IOrdenesRepositorio
    {

        private readonly ApplicationDbContext _db;
        private DbSPFuncionesRepositorio _dbSpFunciones;
        public OrdenesRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool ExisteOrden(int IdOrden)
        {
            return _db.ordenes.Any(x => x.IdOrden == IdOrden);
        }

        public async Task<Ordenes> GetOrden(int IdOrden)
        {
            try
            {
                return await _db.ordenes
                    .Include(d => d.catAduana)
                    .Include(e => e.catClientes)
                    .Include(f => f.catUsuario)
                    .Include(g => g.CatEmpresas)
                    .Include(h => h.CatLineaNegocio)
                    .Include(i => i.CatSucursales)
                    .Include(a => a.peticionesReferencias).ThenInclude(b => b.Contenedores).ThenInclude(c => c.Servicios).FirstOrDefaultAsync(x => x.IdOrden == IdOrden);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ICollection<Ordenes>> GetOrdenesCliente(int IdCatCliente)
        {
            return await _db.ordenes.Where(x => x.IdCatCliente == IdCatCliente).ToListAsync();
        }

        public async Task<ICollection<Ordenes>> GetOrdenes(FiltroOrdenesReferenciasDTO pFiltro)
        {
            var lstOrdenesReferencias = await _db.ordenes.Include(a => a.peticionesReferencias).ThenInclude(b => b.Contenedores).ThenInclude(c => c.Servicios)
               .Where(a => (pFiltro.IdCliente <= 0 || a.IdCatEmpresa == pFiltro.IdCliente) &&
                           (pFiltro.IdAduana <= 0 || a.IdCatAduana == pFiltro.IdAduana) &&
                           (pFiltro.IdEmpresa.Count <= 0 || pFiltro.IdEmpresa.Contains(a.IdCatEmpresa.ToString())) &&
                           (pFiltro.IdLNegocio <= 0 || a.IdCatLineaNegocio == pFiltro.IdLNegocio) &&
                            (string.IsNullOrEmpty(pFiltro.Contenedor) || a.peticionesReferencias.Any(b => b.Contenedores.Any(c => c.Contenedor.Contains(pFiltro.Contenedor)))) &&
                            (string.IsNullOrEmpty(pFiltro.ReferenciaCliente) || a.peticionesReferencias.Any(b => b.Contenedores.Any(c => c.RefenciaCliente.Contains(pFiltro.ReferenciaCliente)))) &&
                            (string.IsNullOrEmpty(pFiltro.ReferenciaALO) || a.ReferenciaALO.Contains(pFiltro.ReferenciaALO)))

               .ToListAsync();
            return (ICollection<Ordenes>)lstOrdenesReferencias;
        }

        public async Task<Ordenes> CrearOrden(Ordenes pOrden)
        {
            try
            {

                Ordenes objOrden = new Ordenes
                {
                    IdCatCliente = pOrden.IdCatCliente,
                    IdCatSistema = pOrden.IdCatSistema ?? null,
                    IdCatLineaNegocio = pOrden.IdCatLineaNegocio,
                    IdCatEmpresa = pOrden.IdCatEmpresa,
                    IdCatSucursal = pOrden.IdCatSucursal,
                    IdCatAduana = pOrden.IdCatAduana,
                    IdCatProveedor = pOrden.IdCatProveedor ?? null,
                    IdUsuario = pOrden.IdUsuario ?? null,
                    IdEstadoOrden = pOrden.IdEstadoOrden,
                    ReferenciaALO = pOrden.ReferenciaALO
                };

                /*
                 * SOLO PARA LINEA DE NEGOCIO AUTOS
                 */
                if (pOrden.IdCatLineaNegocio == 4)
                {
                    objOrden.IdEstadoOrden = 1; //Iniciamos ABIERTA
                }


                _db.ordenes.Add(objOrden);
                if (_db.SaveChanges() > 0)
                {
                    if (pOrden.IdCatLineaNegocio == 4)
                    {
                        objOrden.ReferenciaALO = "INC-WA-" + DateTime.Now.Year.ToString() + "/" + objOrden.IdOrden.ToString();
                        var objRespuestaUpdate = ActualizarGenerico(objOrden.IdOrden, objOrden);
                    }

                    return objOrden;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Ordenes CrearOrdenServicio(Ordenes pOrden)
        {
            try
            {

                Ordenes objOrden = new Ordenes
                {
                    IdCatCliente = pOrden.IdCatCliente,
                    IdCatSistema = pOrden.IdCatSistema ?? null,
                    IdCatLineaNegocio = pOrden.IdCatLineaNegocio,
                    IdCatEmpresa = pOrden.IdCatEmpresa,
                    IdCatSucursal = pOrden.IdCatSucursal,
                    IdCatAduana = pOrden.IdCatAduana,
                    IdCatProveedor = pOrden.IdCatProveedor ?? null,
                    IdUsuario = pOrden.IdUsuario ?? null,
                    IdEstadoOrden = 5

                };

                /*
                 * SOLO PARA LINEA DE NEGOCIO AUTOS
                 */
                if (pOrden.IdCatLineaNegocio == 4)
                {
                    objOrden.IdEstadoOrden = 1; //Iniciamos ABIERTA
                }


                _db.ordenes.Add(objOrden);
                if (_db.SaveChanges() > 0)
                {
                    if (pOrden.IdCatLineaNegocio == 4)
                    {
                        objOrden.ReferenciaALO = "INC-WA-" + DateTime.Now.Year.ToString() + "/" + objOrden.IdOrden.ToString();
                        var objRespuestaUpdate = ActualizarGenerico(objOrden.IdOrden, objOrden);
                    }

                    return objOrden;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //public async Task<string> ObtenerReferenciaALO(Ordenes pOrden, int pIdCatMercancia, int pIdCatTipoOperacion, int pIdCatCliente)
        //{
        //    _dbSpFunciones = new DbSPFuncionesRepositorio(_db);

        //    return await _dbSpFunciones.ObtenerReferenciaALO(pOrden, pIdCatMercancia, pIdCatTipoOperacion, pIdCatCliente);
        //}


        public async Task<string> obtenerPathOrden(SolPathOrdenDTO pPathOrdenDTO)
        {
            try
            {


                if (pPathOrdenDTO.IdOrden > 0)
                {


                    var paramapIdOrden = new SqlParameter("@pIdOrden", pPathOrdenDTO.IdOrden);
                    var parampIdContenedor = new SqlParameter("@pIdContenedor", (object)pPathOrdenDTO.IdContenedor ?? DBNull.Value);
                    var paramapIdServicio = new SqlParameter("@pIdServicio", (object)pPathOrdenDTO.IdServicio ?? DBNull.Value);
                    var paramapIdCatDocumento = new SqlParameter("@pIdCatDocumento", (object)pPathOrdenDTO.IdCatDocumento ?? DBNull.Value);
                    var paramapIdLineaNegocio = new SqlParameter("@pIdLineaNegocio", (object)pPathOrdenDTO.IdLineaNegocio ?? DBNull.Value);

                    var paramorderPath = new SqlParameter
                    {
                        ParameterName = "@OrderPath",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 100,
                        Direction = System.Data.ParameterDirection.Output
                    };


                    await _context.Database.ExecuteSqlRawAsync(
                        "SELECT @OrderPath=dbo.fnObtenerPathExpediente(@pIdOrden,@pIdContenedor,@pIdServicio,@pIdCatDocumento,@pIdLineaNegocio); ",
                        paramapIdOrden, parampIdContenedor, paramapIdServicio, paramapIdCatDocumento, paramapIdLineaNegocio, paramorderPath);
                    var respath = (string)paramorderPath.Value;
                    respath = Regex.Replace(respath, @"\\{2}", @"\");
                    return respath;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public string ObtenerPathOrdenServicio(SolPathOrdenDTO pPathOrdenDTO)
        {
            try
            {
                if (pPathOrdenDTO.IdOrden > 0)
                {
                    var paramapIdOrden = new SqlParameter("@pIdOrden", pPathOrdenDTO.IdOrden);
                    var parampIdContenedor = new SqlParameter("@pIdContenedor", (object)pPathOrdenDTO.IdContenedor ?? DBNull.Value);
                    var paramapIdServicio = new SqlParameter("@pIdServicio", (object)pPathOrdenDTO.IdServicio ?? DBNull.Value);
                    var paramapIdCatDocumento = new SqlParameter("@pIdCatDocumento", (object)pPathOrdenDTO.IdCatDocumento ?? DBNull.Value);
                    var paramapIdLineaNegocio = new SqlParameter("@pIdLineaNegocio", (object)pPathOrdenDTO.IdLineaNegocio ?? DBNull.Value);

                    var paramorderPath = new SqlParameter
                    {
                        ParameterName = "@OrderPath",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 100,
                        Direction = System.Data.ParameterDirection.Output
                    };


                    _context.Database.ExecuteSqlRawAsync(
                        "SELECT @OrderPath=dbo.fnObtenerPathExpediente(@pIdOrden,@pIdContenedor,@pIdServicio,@pIdCatDocumento,@pIdLineaNegocio); ",
                        paramapIdOrden, parampIdContenedor, paramapIdServicio, paramapIdCatDocumento, paramapIdLineaNegocio, paramorderPath);
                    var respath = (string)paramorderPath.Value;
                    respath = Regex.Replace(respath, @"\\{2}", @"\");
                    return respath;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


    }
}
