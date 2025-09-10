using ALOG.Modelos.Modelos.DTO.Provision;
using ALOG.Modelos.Modelos.Provision;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Provision.IProvision;
using Microsoft.EntityFrameworkCore;

namespace ALOG.Repositorios.Repositorio.Provision
{
    public class ProvisionesEncRepositorio : IProvisionesEncRepositorio
    {
        #region Variables Globales
        private readonly ApplicationDbContext _db;
        #endregion Variables Globales

        public ProvisionesEncRepositorio(ApplicationDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public List<ProvisionEnc> obtenerProvisionPorUUID(List<string> pUuid)
        //{
        //    if (pUuid == null || !pUuid.Any())
        //        throw new ArgumentException("La lista enviada se encuentra vacía. Favor de enviar al menos un UUID.", nameof(pUuid));

        //    var tolerancia = 0.01;

        //    var objResp = _db.provisionesEnc
        //        .Include(pe => pe.provisionesDet)
        //            .ThenInclude(pd => pd.peticionesContenedor)
        //                .ThenInclude(pc => pc.IntegracionAnticipoSol)
        //        .Include(pe => pe.provisionesDet)
        //            .ThenInclude(pd => pd.peticionesReferencia)
        //                .ThenInclude(pr => pr.ordenes)
        //                    .ThenInclude(o => o.IntegracionReferencia)
        //                        .ThenInclude(ir => ir.IntegracionAnticipoSol)
        //        .Include(pe => pe.tipoMoneda)
        //        .Where(pe => pUuid.Contains(pe.UUID))
        //        .ToList()
        //        .Where(pe =>
        //            pe.provisionesDet.Any(pd =>
        //                pd.peticionesContenedor?.IntegracionAnticipoSol?.Any(ia =>
        //                    ia.IdOrden == pd.peticionesReferencia.IdOrden &&
        //                    ia.IdPeticionesReferencia == pd.peticionesReferencia.ordenes.IntegracionReferencia.IdIntReferencia &&
        //                    ia.IdPeticionesContenedor == pd.IdContenedor &&
        //                    Math.Abs(ia.MontoTotal - pd.TotalPartida) < tolerancia
        //                ) == true
        //            )
        //        ).ToList();

        //    return objResp;
        //}
        public List<ProvisionEnc> obtenerProvisionPorUUID(List<string> pUuid)
        {
            if (pUuid == null || !pUuid.Any())
                throw new ArgumentException("La lista enviada se encuentra vacía. Favor de enviar al menos un UUID.", nameof(pUuid));

            // Cargar provisiones con detalles y navegación
            var provisiones = _db.provisionesEnc
                .Include(pe => pe.provisionesDet)
                    .ThenInclude(pd => pd.peticionesContenedor)
                .Include(pe => pe.provisionesDet)
                    .ThenInclude(pd => pd.peticionesReferencia)
                .Include(pe => pe.tipoMoneda)
                .Where(pe => pUuid.Contains(pe.UUID))
                .ToList();

            // Filtrar solo provisiones con al menos un detalle válido según equivalencia de servicio
            var resultado = provisiones
                                .Where(pe => pe.provisionesDet.Any(pd =>
                                    _db.integracionFacturaDet
                                        .Where(ifd => ifd.IdPeticionesReferencia == pd.IdReferencia &&
                                                      ifd.IdPeticionesContenedor == pd.IdContenedor)
                                        .AsEnumerable() // 🔁 Forzar evaluación en memoria
                                        .Any(ifd => EsServicioCompatible(pd.Servicio, ifd.IdCatServicio))
                                ))
                                .ToList();


            // Asignar manualmente la colección de facturas a cada detalle
            AsignarFacturasAProvisiones(resultado);

            return resultado;
        }

        public List<ProvisionEnc> obtenerProvisionPorUUIDPart(List<ProvisionSolUUIDPartDTO> pUuid, out List<string> plstError)
        {
            var _lstProvisionesEnc = new List<ProvisionEnc>();
            plstError = new List<string>();

            foreach (var item in pUuid)
            {
                //Cargar provisión por UUID (sin filtrar aún la partida)
                var objRespuesta = _db.provisionesEnc
                    .Include(p => p.provisionesDet)
                        .ThenInclude(pd => pd.peticionesReferencia)
                            .ThenInclude(r => r.ordenes)
                    .Include(p => p.provisionesDet)
                        .ThenInclude(pd => pd.peticionesContenedor)
                    .Include(p => p.tipoMoneda)
                    .FirstOrDefault(x => x.UUID == item.UUID);

                if (objRespuesta != null)
                {
                    //Filtrar detalles por Partida exacta
                    objRespuesta.provisionesDet = objRespuesta.provisionesDet
                        .Where(pd => pd.Partida == item.Partida)
                        .ToList();

                    // Si no tiene partidas con ese número, lo ignoramos
                    if (objRespuesta.provisionesDet.Any())
                    {
                        _lstProvisionesEnc.Add(objRespuesta);
                    }
                    else
                    {
                        plstError.Add($"UUID:{item.UUID} Partida:{item.Partida} no tiene coincidencia en detalles.");
                    }
                }
                else
                {
                    plstError.Add($"UUID:{item.UUID} Partida:{item.Partida} no encontrado.");
                }
            }

            //Asignar facturas compatibles a cada detalle
            AsignarFacturasAProvisiones(_lstProvisionesEnc);

            return _lstProvisionesEnc;
        }

        public void AsignarFacturasAProvisiones(List<ProvisionEnc> listaProvisiones)
        {
            foreach (var pe in listaProvisiones)
            {
                foreach (var pd in pe.provisionesDet)
                {
                    var facturas = _db.integracionFacturaDet
                        .Include(ifd => ifd.integracionFacturaEnc)
                        .Where(ifd =>
                            ifd.IdPeticionesReferencia == pd.IdReferencia &&
                            ifd.IdPeticionesContenedor == pd.IdContenedor
                        )
                        .AsEnumerable() // 🔁 Evalúa en memoria
                        .Where(ifd => EsServicioCompatible(pd.Servicio, ifd.IdCatServicio))
                        .ToList();

                    pd.IntegracionesFacturaDet = facturas;
                }
            }
        }

        private bool EsServicioCompatible(string servicioProvision, int idCatServicioFactura)
        {
            return
                (servicioProvision == "5" && new[] { 27, 15, 5, 94 }.Contains(idCatServicioFactura)) ||
                (servicioProvision == "133" && (idCatServicioFactura == 27 || idCatServicioFactura == 15)) ||
                (servicioProvision != "5" && servicioProvision != "133" &&
                 servicioProvision == idCatServicioFactura.ToString());
        }
    }
}