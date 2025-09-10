
using ALOG.Modelos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Integracion1G.IIntegracion1G;
using ALOG.Repositorios.Repositorio.SPFunciones;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ALOG.Repositorios.Repositorio.Integracion1G {
    public class Integracion1GRepo : IIntegracion1GRepo{

        private readonly ApplicationDbContext _db;
        private DbSPFuncionesRepositorio _dbSPFuncionesRepositorio;
        public Integracion1GRepo(ApplicationDbContext db) { 
            _db = db;
        }

        public async Task<RespuestaGenericaDTO> GenerarFacturacionDesdeOrdenesAsync(Ordenes orden) {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();
            respuesta.IsSuccess = true;
            respuesta.StatusCode = HttpStatusCode.OK;

            try {

                var contenedores = orden.peticionesReferencias
                    .SelectMany(r => r.Contenedores)
                    .ToList();
                // Generar combinaciones contenedor-servicio
                var serviciosPorContenedor = contenedores
                    .SelectMany(c => c.Servicios.Select(s => new { Contenedor = c, Servicio = s }))
                    .ToList();
                // Agrupaciones por tipo de servicio y ReferenciaClienteFacturar
                var solicitudesServiciosGarantias = serviciosPorContenedor
                    .Where(x => x.Servicio.IdTipoServicio == 3)
                    .GroupBy(x => x.Servicio.ReferenciaClienteFacturar)
                    .ToList();
                var solicitudesServiciosDemoras = serviciosPorContenedor
                    .Where(x => x.Servicio.IdTipoServicio == 4)
                    .GroupBy(x => x.Servicio.ReferenciaClienteFacturar)
                    .ToList();
                var solicitudes = serviciosPorContenedor
                    .Where(x => x.Servicio.IdTipoServicio != 3 && x.Servicio.IdTipoServicio != 4)
                    .GroupBy(x => x.Servicio.ReferenciaClienteFacturar)
                    .ToList();

                var respuestaSolicitudes = await GenerarSolicitudFacturacion1G(solicitudes, orden);
                var respuestaSolicitudesDemostas = await GenerarSolicitudFacturacion1G(solicitudesServiciosDemoras, orden);
                var respuetsaSolicitudesGarantias = await GenerarSolicitudFacturacion1G(solicitudesServiciosGarantias, orden);


                respuesta.lstrErrorMessages.AddRange(respuestaSolicitudes.lstrErrorMessages);
                respuesta.lstrErrorMessages.AddRange(respuetsaSolicitudesGarantias.lstrErrorMessages);
                respuesta.lstrErrorMessages.AddRange(respuestaSolicitudesDemostas.lstrErrorMessages);

            } catch (Exception ex) {
                Console.WriteLine($"ERROR: {ex.Message}");
                respuesta.lstrErrorMessages.AddRange(new List<string> { ex.Message });
            }

            if (respuesta.lstrErrorMessages.Count() > 0) {
                respuesta.IsSuccess = false;
                respuesta.StatusCode = HttpStatusCode.BadRequest;
            }

            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> GenerarSolicitudFacturacion1G(IEnumerable<IGrouping<string, dynamic>> agrupadoPorReferenciaFacturar, Ordenes orden) {
            _dbSPFuncionesRepositorio = new DbSPFuncionesRepositorio(_db);
            var respuesta = new RespuestaGenericaDTO();
            var errores = new List<string>();
            var idsIntFacturaEnc = new List<int>();
            string idCompaniaExterna = orden != null ? (orden.IdCatEmpresa + 1).ToString() : "0";

           

            try {
                int grupoIndex = 0;

                foreach (var grupo in agrupadoPorReferenciaFacturar) {
                    grupoIndex++;
                    if (!grupo.Any()) continue;
                    var savepointName = $"Grupo_{grupoIndex}";

                    //await transaction.CreateSavepointAsync(savepointName);

                    try {
                        RespuestaGenericaDTO respuestaIntReferencia = new RespuestaGenericaDTO();
                        var integraReferencia = new IntegraReferencia { IdIntReferencia = 0 };
                        bool esServicioGestion = grupo.Any(item => item.Servicio.IdTipoServicio == 3 || item.Servicio.IdTipoServicio == 4);
                        var ejemplo = grupo.First();

                        if (esServicioGestion) {
                            respuestaIntReferencia = await _dbSPFuncionesRepositorio.GetReferenciaAloFacturacionPorReferenciaCliente(grupo.Key);
                            if (respuestaIntReferencia.lstrErrorMessages.Any()) {
                                errores.AddRange(respuestaIntReferencia.lstrErrorMessages);
                                //await transaction.RollbackToSavepointAsync(savepointName);
                                continue;
                            }

                            integraReferencia = (IntegraReferencia?)respuestaIntReferencia.Entidad ?? new IntegraReferencia { IdIntReferencia = 0 };
                        }

                        if (integraReferencia.IdIntReferencia <= 0) {
                            var data = await _dbSPFuncionesRepositorio.GetReferenciaALOFacturacion(orden.IdCatLineaNegocio, orden.IdOrden, grupo.Key);

                            if (data.Any() && string.IsNullOrWhiteSpace(data.GetValueOrDefault("mensaje")))
                            {
                                var referenciaAlo = data["referenciaAloFacturar"];
                                integraReferencia = new IntegraReferencia
                                {
                                    IdOrden = orden.IdOrden,
                                    IdCompaniaExterna = idCompaniaExterna,
                                    ReferenciaALO = string.IsNullOrWhiteSpace(referenciaAlo)? orden.ReferenciaALO : referenciaAlo,
                                    ReferenciaClienteExterno = grupo.Key,
                                    ClaveClienteExterno = ejemplo.Servicio.IdClienteFacturarA.ToString(),
                                    Aduana = orden.catAduana?.Aduana.ToString() ?? "00",
                                    Enviado = false
                                    //FechaEnvio = DateTime.Now
                                };

                                respuestaIntReferencia = await _dbSPFuncionesRepositorio.GuardarIntegracionReferenciaAsync(integraReferencia);
                                if (respuestaIntReferencia.lstrErrorMessages.Any())
                                {
                                    errores.AddRange(respuestaIntReferencia.lstrErrorMessages);
                                    //await transaction.RollbackToSavepointAsync(savepointName);
                                    continue;
                                }

                                integraReferencia = (IntegraReferencia)respuestaIntReferencia.Entidad;
                            }
                        }

                        // VALIDACIÓN básica de integridad
                        if (string.IsNullOrWhiteSpace(integraReferencia.ReferenciaALO)) {
                            errores.Add("No se pudo determinar una referencia ALO válida.");
                            //await transaction.RollbackToSavepointAsync(savepointName);
                            continue;
                        }

                        var facturaEnc = new IntegraFacturaEnc {
                            IdOrden = orden.IdOrden,
                            IdPeticionesReferencia = ejemplo.Contenedor.IdReferencia,
                            IdSolicitudFacturacion = orden.IdOrden.ToString(),
                            IdCompaniaExterna = idCompaniaExterna,
                            ClaveClienteExterno = ejemplo.Servicio.IdClienteFacturarA.ToString(),
                            Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                            ConceptoFacturacion = "3",
                            Comentario = "",
                            Nota = "",
                            Referencia = integraReferencia.ReferenciaALO,
                            ClaveSATMoneda = "MXN",
                            FacturacionAutomatica = false,
                            CierreReferencia = false,
                            Activo = true,
                            FechaRegistro = DateTime.Now,
                            //FechaEnvio = DateTime.Now,
                            Enviado = false,
                            IdIntReferencia = integraReferencia.IdIntReferencia
                        };
                        await using var transaction = await _db.Database.BeginTransactionAsync();
                        // beginTransaction
                        var respuestaFacturaEnc = await _dbSPFuncionesRepositorio.GuardarIntegracionFacturaEncAsync(facturaEnc);

                        if (ejemplo.Servicio.IdTipoServicio == 1 && ejemplo.Servicio.IdContenedor == 7799) {
                            respuestaFacturaEnc.lstrErrorMessages.Add("Esto es un error forzado");
                        }

                        if (respuestaFacturaEnc.lstrErrorMessages.Any()) {
                            errores.AddRange(respuestaFacturaEnc.lstrErrorMessages);
                            await transaction.RollbackToSavepointAsync(savepointName);
                            continue;
                        }

                        var intFacturaEnc = (IntegraFacturaEnc)respuestaFacturaEnc.Entidad;
                        bool errorEnDetalles = false;

                        foreach (var item in grupo) {
                            var detalle = new IntegraFacturaDet {
                                IddIntFacturaEnc = intFacturaEnc.IdIntFacturaEnc,
                                ClaveServicio = item.Servicio.IdTipoServicio.ToString(),
                                Cantidad = "1",
                                Precio = item.Servicio.IdTipoServicio == 3 ? "500" : "5670", // Puedes extraer a método
                                IdPeticionesReferencia = item.Contenedor.IdReferencia,
                                IdPeticionesContenedor = item.Contenedor.IdContenedor,
                                Contenedor = item.Contenedor.Contenedor,
                                EIR = "",
                                Comentario = "",
                                Nota = "",
                                IdCatServicio = item.Servicio.IdTipoServicio
                            };

                            var respuestaDet = await _dbSPFuncionesRepositorio.GuardarIntegracionFacturaDetAsync(detalle);

                            if (respuestaDet.lstrErrorMessages.Any()) {
                                errores.AddRange(respuestaDet.lstrErrorMessages);
                                errorEnDetalles = true;
                                break;
                            }
                        }

                        if (errorEnDetalles) {
                            await transaction.RollbackToSavepointAsync(savepointName);
                            continue;
                        }

                        if (errores.Count == 0) {

                            await transaction.CommitAsync();

                            var respuestaMarcarSolicitud = await _dbSPFuncionesRepositorio.MarcarSolicitudFacturaListaAsync(intFacturaEnc.IdIntFacturaEnc, orden.IdCatEmpresa);

                            if (respuestaMarcarSolicitud.IsSuccess == false || respuestaMarcarSolicitud.StatusCode != HttpStatusCode.OK || respuesta.lstrErrorMessages.Count() > 0) {
                                errores.AddRange(respuestaMarcarSolicitud.lstrErrorMessages);
                            }

                            if (errores.Count == 0) {
                                //await transaction.CommitAsync();
                                respuesta.IsSuccess = true;
                                respuesta.StatusCode = HttpStatusCode.OK;
                            } else {
                                //await transaction.RollbackToSavepointAsync(savepointName);
                                respuesta.IsSuccess = false;
                                respuesta.StatusCode = HttpStatusCode.BadRequest;
                            }
                        } else {
                            //await transaction.RollbackToSavepointAsync(savepointName);
                            respuesta.IsSuccess = false;
                            respuesta.StatusCode = HttpStatusCode.BadRequest;
                        }


                    } catch (Exception exGrupo) {
                        errores.Add($"Error en grupo {grupoIndex}: {exGrupo.Message}");
                        //await transaction.RollbackToSavepointAsync(savepointName);
                        continue;
                    }
                }

            } catch (Exception ex) {
                //await transaction.RollbackAsync();
                errores.Add($"Excepción general: {ex.Message}");
                respuesta.IsSuccess = false;
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
            }

            respuesta.lstrErrorMessages = errores;
            return respuesta;
        }


        //public async Task<RespuestaGenericaDTO> GenerarSolicitudFacturacion1G(IEnumerable<IGrouping<string, dynamic>> agrupadoPorReferenciaFacturar, Ordenes orden) {
        //    _dbSPFuncionesRepositorio = new DbSPFuncionesRepositorio(_db);
        //    RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();
        //    List<int> idsIntFacturaEnc = new List<int>();
        //    List<string> errores = new List<string>();
        //    string idCompaniaExterna = orden != null ? (orden.IdCatEmpresa + 1).ToString() : "0";
        //    // Se engloba la transacción por si algo malo sucede dar retroceso
        //    using var transaction = await _db.Database.BeginTransactionAsync();

        //    try {
        //        int indiceGrupo = 0;
        //        // Se iteran sobre las referencias de clientes agrupadas
        //        foreach (var grupo in agrupadoPorReferenciaFacturar) {

        //            indiceGrupo++;
        //            var savepointName = $"Grupo_{indiceGrupo}";
        //            await transaction.CreateSavepointAsync(savepointName);

        //            RespuestaGenericaDTO respuestaIntReferencia = new RespuestaGenericaDTO();
        //            IntegraReferencia integraReferencia = new IntegraReferencia { IdIntReferencia = 0 };
        //            // Valida si el servicio que se va a generar su solicitud de factura es un servicio de
        //            // GESTION RECUPERACION DE GARANTIAS (3) o GESTION CORTE DE DEMORAS (4)
        //            bool esServicioGestion = grupo.Any(item => item.Servicio.IdTipoServicio == 3 || item.Servicio.IdTipoServicio == 4);
        //            // Se obtiene el primer grupo de servicio por referencia de cliente
        //            var ejemplo = grupo.First();

        //            if (esServicioGestion) {
        //                // Busca si la referencia de cliente en cuestión ya cuenta con una referencia alo de facturación
        //                // Este paso se realiza porque puede existir una referencia alo de facturación para algún otro servicio (MV, limpieza, reparaciones)
        //                // Con esto se pretende reutilizar la referencia alo de facturación ya existente
        //                // para las nuevas solicitudes de facturación de servicios de gestión de demoras y garantías
        //                respuestaIntReferencia = await _dbSPFuncionesRepositorio.GetReferenciaAloFacturacion(grupo.Key);

        //                if (respuestaIntReferencia.lstrErrorMessages.Any()) {
        //                    errores.AddRange(respuestaIntReferencia.lstrErrorMessages);
        //                    await transaction.RollbackToSavepointAsync(savepointName);
        //                    continue;
        //                }
        //                integraReferencia = (IntegraReferencia)respuestaIntReferencia.Entidad ?? new IntegraReferencia { IdIntReferencia = 0 };
        //            }
        //            // Si la referencia de cliente en cuestión no cuenta con una referencia alo de facturación
        //            if (integraReferencia.IdIntReferencia <= 0) {
        //                // Se obtiene el formato para la nueva referencia alo de facturación para la referencia de cliente
        //                string referenciaAlo = await _dbSPFuncionesRepositorio.ObtenerReferenciaALO(orden, 0, 0, (int)ejemplo.Servicio.IdClienteFacturarA);
        //                integraReferencia.ReferenciaALO = referenciaAlo;

        //                integraReferencia = new IntegraReferencia {
        //                    IdOrden = orden.IdOrden,
        //                    IdCompaniaExterna = idCompaniaExterna,
        //                    ReferenciaALO = referenciaAlo,
        //                    ReferenciaClienteExterno = grupo.Key,
        //                    ClaveClienteExterno = ejemplo.Servicio.IdClienteFacturarA.ToString(),
        //                    Aduana = orden.catAduana?.Aduana.ToString() ?? "00",
        //                    Enviado = false,
        //                    FechaEnvio = DateTime.Now
        //                };

        //                respuestaIntReferencia = await _dbSPFuncionesRepositorio.GuardarIntegracionReferenciaAsync(integraReferencia);

        //                if (respuestaIntReferencia.lstrErrorMessages.Any()) {
        //                    errores.AddRange(respuestaIntReferencia.lstrErrorMessages);
        //                    await transaction.RollbackToSavepointAsync(savepointName);
        //                    continue;
        //                }

        //                integraReferencia = (IntegraReferencia)respuestaIntReferencia.Entidad;
        //            }

        //            var facturaEnc = new IntegraFacturaEnc {
        //                IdOrden = orden.IdOrden,
        //                IdPeticionesReferencia = ejemplo.Contenedor.IdReferencia,
        //                IdSolicitudFacturacion = orden.IdOrden.ToString(),
        //                IdCompaniaExterna = idCompaniaExterna,
        //                ClaveClienteExterno = ejemplo.Servicio.IdClienteFacturarA.ToString(),
        //                Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
        //                ConceptoFacturacion = "3",
        //                Comentario = "",
        //                Nota = "",
        //                Referencia = integraReferencia.ReferenciaALO,
        //                ClaveSATMoneda = "MXN",
        //                FacturacionAutomatica = false,
        //                CierreReferencia = false,
        //                Activo = true,
        //                FechaRegistro = DateTime.Now,
        //                FechaEnvio = DateTime.Now,
        //                Enviado = false,
        //                IdIntReferencia = integraReferencia.IdIntReferencia
        //            };

        //            var respuestaFacturaEnc = await _dbSPFuncionesRepositorio.GuardarIntegracionFacturaEncAsync(facturaEnc);

        //            if (ejemplo.Servicio.IdTipoServicio == 1 && ejemplo.Servicio.IdContenedor == 7799) {
        //                respuestaFacturaEnc.lstrErrorMessages.Add("Esto es un error forzado");
        //            }
        //            if (respuestaFacturaEnc.lstrErrorMessages.Any()) {
        //                errores.AddRange(respuestaFacturaEnc.lstrErrorMessages);
        //                await transaction.RollbackToSavepointAsync(savepointName);
        //                continue;
        //            }

        //            var intFacturaEnc = (IntegraFacturaEnc)respuestaFacturaEnc.Entidad;

        //            bool errorEnDetalles = false;

        //            foreach (var item in grupo) {
        //                var detalle = new IntegraFacturaDet {
        //                    IddIntFacturaEnc = intFacturaEnc.IdIntFacturaEnc,
        //                    ClaveServicio = item.Servicio.IdTipoServicio.ToString(),
        //                    Cantidad = "1",
        //                    Precio = item.Servicio.IdTipoServicio == 3 ? "500" : "5670",
        //                    IdPeticionesReferencia = item.Contenedor.IdReferencia,
        //                    IdPeticionesContenedor = item.Contenedor.IdContenedor,
        //                    Contenedor = item.Contenedor.Contenedor,
        //                    EIR = "",
        //                    Comentario = "",
        //                    Nota = "",
        //                    IdCatServicio = item.Servicio.IdTipoServicio
        //                };

        //                var respuestaDet = await _dbSPFuncionesRepositorio.GuardarIntegracionFacturaDetAsync(detalle);

        //                if (respuestaDet.lstrErrorMessages.Any()) {
        //                    errores.AddRange(respuestaDet.lstrErrorMessages);
        //                    errorEnDetalles = true;
        //                    break;
        //                }
        //            }
        //            // Si existieron errores en la transacción de los detallees de la solicitud de factura
        //            if (errorEnDetalles) {
        //                // Se realiza el rollback de la solicitud de factura en cuestión
        //                //await transaction.RollbackAsync();
        //                await transaction.RollbackToSavepointAsync(savepointName);
        //                respuesta.IsSuccess = false;
        //                respuesta.lstrErrorMessages = errores;
        //                respuesta.StatusCode = HttpStatusCode.BadRequest;
        //                return respuesta;
        //            }

        //            idsIntFacturaEnc.Add(intFacturaEnc.IdIntFacturaEnc);
        //        }

        //        if (!errores.Any()) {
        //            foreach (var id in idsIntFacturaEnc) {
        //                var respuestaMarcarSolicitud = await _dbSPFuncionesRepositorio.MarcarSolicitudFacturaListaAsync(id, orden.IdCatEmpresa);

        //                if (respuestaMarcarSolicitud.IsSuccess == false || respuestaMarcarSolicitud.StatusCode != HttpStatusCode.OK || respuesta.lstrErrorMessages.Count() > 0) {
        //                    errores.AddRange(respuestaMarcarSolicitud.lstrErrorMessages);
        //                }
        //            }

        //            await transaction.CommitAsync();
        //            respuesta.IsSuccess = true;
        //            respuesta.StatusCode = HttpStatusCode.OK;
        //        } else {
        //            await transaction.RollbackAsync();
        //            respuesta.IsSuccess = false;
        //            respuesta.StatusCode = HttpStatusCode.BadRequest;
        //        }
        //    } catch (Exception ex) {
        //        await transaction.RollbackAsync();
        //        errores.Add(ex.ToString());
        //        respuesta.IsSuccess = false;
        //        respuesta.StatusCode = HttpStatusCode.InternalServerError;
        //    }

        //    respuesta.lstrErrorMessages = errores;
        //    return respuesta;
        //}

        private async Task<(bool IsSuccess, List<string> lstrErrorMessages, int IdFacturaEnc)> ProcesarGrupoFacturacion(IGrouping<string, dynamic> grupo, Ordenes orden) {
            var errores = new List<string>();
            var ejemplo = grupo.First();
            bool esServicioGestion = grupo.Any(item => item.Servicio.IdTipoServicio is 3 or 4);

            var integraReferencia = await ObtenerReferenciaFacturacion(grupo, orden, esServicioGestion);
            if (!integraReferencia.IsSuccess)
                return (false, integraReferencia.lstrErrorMessages, 0);

            var facturaEnc = CrearEncabezadoFactura(integraReferencia.Referencia, orden, ejemplo);
            var respuestaFacturaEnc = await _dbSPFuncionesRepositorio.GuardarIntegracionFacturaEncAsync(facturaEnc);

            if (!respuestaFacturaEnc.IsSuccess)
                return (false, respuestaFacturaEnc.lstrErrorMessages, 0);

            var facturaEncGuardada = (IntegraFacturaEnc)respuestaFacturaEnc.Entidad;

            // Guardar detalles
            foreach (var item in grupo) {
                var detalle = CrearDetalleFactura(facturaEncGuardada.IdIntFacturaEnc, item);
                var respuestaDetalle = await _dbSPFuncionesRepositorio.GuardarIntegracionFacturaDetAsync(detalle);

                if (!respuestaDetalle.IsSuccess) {
                    errores.AddRange(respuestaDetalle.lstrErrorMessages);
                    return (false, errores, 0);
                }
            }

            return (true, new List<string>(), facturaEncGuardada.IdIntFacturaEnc);
        }

        private async Task<(bool IsSuccess, string Referencia, List<string> lstrErrorMessages)> ObtenerReferenciaFacturacion(IGrouping<string, dynamic> grupo, Ordenes orden, bool esServicioGestion) {
            if (esServicioGestion) {
                var respuesta = await _dbSPFuncionesRepositorio.GetReferenciaAloFacturacionPorReferenciaCliente(grupo.Key);
                if (!respuesta.IsSuccess)
                    return (false, null, respuesta.lstrErrorMessages);

                var referencia = ((IntegraReferencia)respuesta.Entidad).ReferenciaALO;
                return (true, referencia, new List<string>());
            } else {
                var ejemplo = grupo.First();
                var referenciaAlo = await _dbSPFuncionesRepositorio.ObtenerReferenciaALO(orden, 0, 0, (int)ejemplo.Servicio.IdClienteFacturarA);
                var nuevaReferencia = new IntegraReferencia {
                    IdOrden = orden.IdOrden,
                    IdCompaniaExterna = "3",
                    ReferenciaALO = referenciaAlo,
                    ReferenciaClienteExterno = grupo.Key,
                    ClaveClienteExterno = ejemplo.Servicio.IdClienteFacturarA.ToString(),
                    Aduana = orden.catAduana?.Aduana.ToString() ?? "00",
                    Enviado = false
                    //FechaEnvio = DateTime.Now
                };

                var respuesta = await _dbSPFuncionesRepositorio.GuardarIntegracionReferenciaAsync(nuevaReferencia);
                if (!respuesta.IsSuccess)
                    return (false, null, respuesta.lstrErrorMessages);

                return (true, ((IntegraReferencia)respuesta.Entidad).ReferenciaALO, new List<string>());
            }
        }

        private IntegraFacturaEnc CrearEncabezadoFactura(string referenciaALO, Ordenes orden, dynamic ejemplo) {
            return new IntegraFacturaEnc {
                IdOrden = orden.IdOrden,
                IdPeticionesReferencia = ejemplo.Contenedor.IdReferencia,
                IdSolicitudFacturacion = orden.IdOrden.ToString(),
                IdCompaniaExterna = "3",
                ClaveClienteExterno = ejemplo.Servicio.IdClienteFacturarA.ToString(),
                Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                ConceptoFacturacion = "3",
                Comentario = "",
                Nota = "",
                Referencia = referenciaALO,
                ClaveSATMoneda = "MXN",
                FacturacionAutomatica = false,
                CierreReferencia = false,
                Activo = true,
                FechaRegistro = DateTime.Now,
                FechaEnvio = DateTime.Now,
                Enviado = false,
                IdIntReferencia = 0
            };
        }

        private IntegraFacturaDet CrearDetalleFactura(int idIntFacturaEnc, dynamic item) {
            return new IntegraFacturaDet {
                IddIntFacturaEnc = idIntFacturaEnc,
                ClaveServicio = item.Servicio.IdTipoServicio.ToString(),
                Cantidad = "1",
                Precio = item.Servicio.IdTipoServicio == 3 ? "500" : "5670",
                IdPeticionesReferencia = item.Contenedor.IdReferencia,
                IdPeticionesContenedor = item.Contenedor.IdContenedor,
                Contenedor = item.Contenedor.Contenedor,
                EIR = "",
                Comentario = "",
                Nota = "",
                IdCatServicio = item.Servicio.IdTipoServicio
            };
        }


    }
}
