using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G;

namespace ALOG.Repositorios.Repositorio.Integración1G
{
    public class IntegraAnticiposSolRepo : GenericoRepositorio<IntegraAnticipoSol>, IIntegraAnticiposSolRepo
    {
        private readonly ApplicationDbContext _db;
        public IntegraAnticiposSolRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public IntegraAnticipoSol obtenerAnticipoSol(int IdAcarreo)
        {
            try
            {
                return _db.integracionAnticipoSol.FirstOrDefault(x => x.IdIntAnticipoSol == IdAcarreo);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public ICollection<IntegraAnticipoSol> obtenerAnticiposSol(FiltroAnticipos1GDTO pFiltro)
        {
            try
            {


                pFiltro.IdIntAnticipoSol = pFiltro.IdIntAnticipoSol == null ? 0 : pFiltro.IdIntAnticipoSol;
                pFiltro.IdOrden = pFiltro.IdOrden == null ? 0 : pFiltro.IdOrden;
                pFiltro.IdPeticionesReferencia = pFiltro.IdPeticionesReferencia == null ? 0 : pFiltro.IdPeticionesReferencia;
                pFiltro.IdPeticionesContenedor = pFiltro.IdPeticionesContenedor == null ? 0 : pFiltro.IdPeticionesContenedor;


                var objlst = _db.integracionAnticipoSol
                    .Where(a => (pFiltro.IdIntAnticipoSol <= 0 || a.IdIntAnticipoSol == pFiltro.IdIntAnticipoSol) &&
                                (pFiltro.IdOrden <= 0 || a.IdOrden == pFiltro.IdOrden) &&
                                (pFiltro.IdPeticionesReferencia <= 0 || a.IdPeticionesReferencia == pFiltro.IdPeticionesReferencia) &&
                                (pFiltro.IdPeticionesContenedor <= 0 || a.IdPeticionesContenedor == pFiltro.IdPeticionesContenedor) &&
                                (pFiltro.Activo == a.Activo) &&
                                (string.IsNullOrEmpty(pFiltro.Contenedor) || a.Contenedor == pFiltro.Contenedor)
                                &&
                                 (string.IsNullOrEmpty(pFiltro.IdCompaniaExterna) || a.IdCompaniaExterna == pFiltro.IdCompaniaExterna) &&
                                 (string.IsNullOrEmpty(pFiltro.ClaveProveedorExterno) || a.ClaveProveedorExterno == pFiltro.ClaveProveedorExterno) &&
                                 (string.IsNullOrEmpty(pFiltro.Referencia) || a.Referencia == pFiltro.Referencia) &&
                                 (string.IsNullOrEmpty(pFiltro.Estado1G) || a.Estado1G == pFiltro.Estado1G) &&
                                 (string.IsNullOrEmpty(pFiltro.RespuestaWS1G) || a.RespuestaWS1G.Contains(pFiltro.RespuestaWS1G)) &&
                                 (string.IsNullOrEmpty(pFiltro.IdSolicitudAnticipoProveedor) || a.IdSolicitudAnticipoProveedor == pFiltro.IdSolicitudAnticipoProveedor)
                                 )

                    .ToList();
                return objlst;
            }
            catch (Exception)
            {

                return null;
            }
        }


    }
}
