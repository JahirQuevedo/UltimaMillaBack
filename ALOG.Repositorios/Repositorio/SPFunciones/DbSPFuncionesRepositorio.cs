using ALOG.Modelos;
using ALOG.Modelos.Modelos;
using ALOG.Modelos.Modelos.DTO.Catalogos;
using ALOG.Modelos.Modelos.DTO.Logistica;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Facturacion;
using ALOG.Modelos.Modelos.Integracion1G;
using ALOG.Modelos.Modelos.Logisticos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OfficeOpenXml.Style;
using System.Data;
using System.Net;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ALOG.Repositorios.Repositorio.SPFunciones
{
    public class DbSPFuncionesRepositorio
    {
        private readonly ApplicationDbContext _db;

        public DbSPFuncionesRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<string> ObtenerReferenciaALO(Ordenes pOrden, int pIdCatMercancia, int pIdCatTipoOperacion, int pIdCatCliente)
        {
            try
            {
                // Parámetros de entrada
                var paramEmpresa = new SqlParameter("@pIdCatEmpresa", (object)pOrden.IdCatEmpresa ?? DBNull.Value);
                var paramLineaNegocio = new SqlParameter("@pIdCatLineaNegocio", (object)pOrden.IdCatLineaNegocio ?? DBNull.Value);
                var paramAduana = new SqlParameter("@pIdCatAduana", (object)pOrden.IdCatAduana ?? DBNull.Value);
                var paramProyecto = new SqlParameter("@pIdCatProyecto", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0
                };
                var paramMercancia = new SqlParameter("@pIdCatMercancia", pIdCatMercancia);
                var paramTipoOperacion = new SqlParameter("@pIdCatTipoOperacion", pIdCatTipoOperacion);
                var paramCliente = new SqlParameter("@pIdCatCliente", pIdCatCliente);
                var paramEsEdicion = new SqlParameter("@pEsEdicion", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // o true / false según necesites
                };
                var paramEditar = new SqlParameter("@pReferenciaAloEditar", "");

                // Parámetro de salida
                var paramRefAlo = new SqlParameter("@pReferenciaAlo", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC Comun.GenerarFormatoReferenciaAlo 
                    @pIdCatEmpresa, 
                    @pIdCatLineaNegocio, 
                    @pIdCatAduana, 
                    @pIdCatProyecto,
                    @pIdCatMercancia, 
                    @pIdCatTipoOperacion, 
                    @pIdCatCliente, 
                    @pEsEdicion,
                    @pReferenciaAloEditar,
                    @pReferenciaAlo OUTPUT",
                        paramEmpresa,
                        paramLineaNegocio,
                        paramAduana,
                        paramProyecto,
                        paramMercancia,
                        paramTipoOperacion,
                        paramCliente,
                        paramEsEdicion,
                        paramEditar,
                        paramRefAlo
                );

                // Tomamos el valor del parámetro de salida
                return paramRefAlo.Value?.ToString() ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<string> ObtenerPathExpedienteAsync(int idOrden, int idContenedor, int idServicio, int idCatDocumento, int idLineaNegocio)
        {
            try
            {
                // Parámetros de entrada
                var pIdOrden = new SqlParameter("@pIdOrden", (object)idOrden ?? DBNull.Value);
                var pIdContenedor = new SqlParameter("@pIdContenedor", (object)idContenedor ?? DBNull.Value);
                var pIdServicio = new SqlParameter("@pIdServicio", (object)idServicio ?? DBNull.Value);
                var pIdCatDocumento = new SqlParameter("@pIdCatDocumento", (object)idCatDocumento ?? DBNull.Value);
                var pIdLineaNegocio = new SqlParameter("@pIdLineaNegocio", (object)idLineaNegocio ?? DBNull.Value);

                // Parámetro de salida
                var paramPathDocumento = new SqlParameter("@PathDocumento ", SqlDbType.NVarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC dbo.ObtenerPathExpediente 
                    @pIdOrden,
                    @pIdContenedor,
                    @pIdServicio,
                    @pIdCatDocumento,
                    @pIdLineaNegocio,
                    @PathDocumento OUTPUT",
                        pIdOrden,
                        pIdContenedor,
                        pIdServicio,
                        pIdCatDocumento,
                        pIdLineaNegocio,
                        paramPathDocumento
                );

                // Tomamos el valor del parámetro de salida
                return paramPathDocumento.Value?.ToString() ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #region Métodos para la generación de anticipos

        public async Task<RespuestaGenericaDTO> ContenedorServicioExisteAnticipoAsync(int idPeticionContenedor, int idCatServicio)
        {
            var respuesta = new RespuestaGenericaDTO();

            try
            {
                var idPeticionContenedorParam = new SqlParameter("@pIdPeticionContenedor", idPeticionContenedor);
                var idCatServicioParam = new SqlParameter("@pIdCatServicio", idCatServicio);

                var tieneAnticipoParam = new SqlParameter("@pTieneAnticipo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };

                var mensajeErrorParam = new SqlParameter("@pMensajeError", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };

                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC Anticipos.ContenedorServicioTieneAnticipo 
                            @pIdPeticionContenedor, 
                            @pIdCatServicio, 
                            @pTieneAnticipo OUTPUT, 
                            @pMensajeError OUTPUT",
                                idPeticionContenedorParam,
                                idCatServicioParam,
                                tieneAnticipoParam,
                                mensajeErrorParam
                );

                var tieneAnticipo = Convert.ToInt32(tieneAnticipoParam.Value);
                var mensajeError = mensajeErrorParam.Value?.ToString() ?? "";

                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.IsSuccess = true;
                respuesta.Entidad = tieneAnticipo == 1;

                if (!string.IsNullOrWhiteSpace(mensajeError))
                {
                    respuesta.strMensaje = mensajeError;
                }

            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = "Error inesperado al consultar si existe anticipo.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }
            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> GetReferenciaAloPorReferenciaClienteAsync(string referenciaCliente)
        {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();

            try
            {
                // Parámetros de entrada
                var referenciaClienteParam = new SqlParameter("@pReferenciaCliente", (object)referenciaCliente ?? DBNull.Value);

                // Parámetro de salida
                var referenciaAloParam = new SqlParameter("@pReferenciaAlo", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                var mensajeErrorParam = new SqlParameter("@vMensageError", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC Anticipos.ObtenerReferenciaAlo 
                    @pReferenciaCliente, 
                    @pReferenciaAlo OUTPUT,
                    @vMensageError OUTPUT",
                        referenciaClienteParam,
                        referenciaAloParam,
                        mensajeErrorParam
                );

                var resultado = referenciaAloParam.Value?.ToString();
                var mensajeError = mensajeErrorParam.Value?.ToString() ?? "";

                var referenciaAlo = !string.IsNullOrWhiteSpace(resultado) ? resultado : string.Empty;

                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.IsSuccess = true;
                respuesta.Entidad = referenciaAlo;

                if (!string.IsNullOrWhiteSpace(mensajeError))
                {
                    respuesta.strMensaje = mensajeError;
                }

            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = "Error inesperado al consultar la referencia alo.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }
            return respuesta;
        }

        public async Task<bool> ProveedorRequiereAnticipoAsync(int idCatAduana, int idCatPatio, int idCatServicio)
        {

            try
            {
                // Parámetros de entrada
                var idCatAdauanaParam = new SqlParameter("@IdCatAduana", (object)idCatAduana ?? DBNull.Value);
                var idCatPatioParam = new SqlParameter("@IdCatPatio", (object)idCatPatio ?? DBNull.Value);
                var idCatServicioParam = new SqlParameter("@IdCatServicio", (object)idCatServicio ?? DBNull.Value);

                // Parámetro de salida
                var requiereAnticipoParam = new SqlParameter("@RequiereAnticipo", SqlDbType.Bit, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC Anticipos.ProveedorRequiereAnticipo 
                    @IdCatAduana, 
                    @IdCatPatio, 
                    @IdCatServicio, 
                    @RequiereAnticipo OUTPUT",
                        idCatAdauanaParam,
                        idCatPatioParam,
                        idCatServicioParam,
                        requiereAnticipoParam
                );

                return bool.Parse(requiereAnticipoParam.Value.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepcion en ProveedorRequiereAnticipoAsync => {ex.Message}");
                return false;
            }
        }

        public async Task<RespuestaGenericaDTO> GetTarifaProveedorAsync(int idCatAduana, int idCatPatio, int idCatServicio)
        {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();

            try
            {
                // Parámetros de entrada
                var idCatAdauanaParam = new SqlParameter("@IdCatAduana", (object)idCatAduana ?? DBNull.Value);
                var idCatPatioParam = new SqlParameter("@IdCatPatio", (object)idCatPatio ?? DBNull.Value);
                var idCatServicioParam = new SqlParameter("@IdCatServicio", (object)idCatServicio ?? DBNull.Value);

                // Parámetro de salida
                var tarifaProveedorParam = new SqlParameter("@TarifaProveedor", SqlDbType.Float, 100)
                {
                    Direction = ParameterDirection.Output
                };

                var mensajeErrorParam = new SqlParameter("@MensajeError", SqlDbType.Float, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC Anticipos.ObtenerTarifaProveedor 
                    @IdCatAduana, 
                    @IdCatPatio, 
                    @IdCatServicio, 
                    @TarifaProveedor OUTPUT,
                    @MensajeError OUTPUT",
                        idCatAdauanaParam,
                        idCatPatioParam,
                        idCatServicioParam,
                        tarifaProveedorParam,
                        mensajeErrorParam
                );

                var tarifaProveedor = double.Parse(tarifaProveedorParam.Value?.ToString());
                var mensajeError = mensajeErrorParam.Value?.ToString() ?? "";

                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.IsSuccess = true;
                respuesta.Entidad = tarifaProveedor;

                if (!string.IsNullOrWhiteSpace(mensajeError))
                {
                    respuesta.strMensaje = mensajeError;
                }


            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = "Error inesperado al consultar de proveedor.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }
            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> GetDatosBancariosProveedorAsync(int idCatPatio, int idCatAduana)
        {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();

            try
            {
                var idCatPatioParam = new SqlParameter("@IdCatPatio", (object)idCatPatio ?? DBNull.Value);
                var idCatAduanaParam = new SqlParameter("@IdCatAduana", (object)idCatAduana ?? DBNull.Value);

                var resultado = await _db
                    .CuentaBancariaProveedorDTO
                    .FromSqlRaw(
                        @"EXEC Anticipos.ObtenerDatosBancariosProveedor 
                                @IdCatPatio, 
                                @IdCatAduana",
                                    idCatPatioParam,
                                    idCatAduanaParam
                    )
                    .AsNoTracking()
                    .ToListAsync();


                var cuentaBancaria = resultado.FirstOrDefault();

                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.IsSuccess = true;
                respuesta.Entidad = cuentaBancaria;

                if (cuentaBancaria.IdCatProveedor == 0)
                    respuesta.lstrErrorMessages.Add("No se encontró el proveedor proporcionado");
                if (string.IsNullOrWhiteSpace(cuentaBancaria.Clabe))
                    respuesta.lstrErrorMessages.Add($"No se encontró la Clabe interbancaria para el proveedor {cuentaBancaria.ProveedorRazonSocial}");
                if (string.IsNullOrWhiteSpace(cuentaBancaria.Cuenta))
                    respuesta.lstrErrorMessages.Add($"No se encontró la Cuenta para el proveedor {cuentaBancaria.ProveedorRazonSocial}");

            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = "Error inesperado al consultar datos bancarios del proveedor.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }
            return respuesta;
        }

        public async Task<AnticiposEnc> GuardarAnticipoEncabezadoAsync(AnticiposEnc anticiposEnc)
        {

            try
            {
                // Parámetros de entrada
                var idCatEmpresa = new SqlParameter("@IdUsuarioSolicita", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 2
                };
                var idUsuarioSolicitaParam = new SqlParameter("@IdUsuarioSolicita", (object)anticiposEnc.IdUsuarioSolicita ?? DBNull.Value);
                var idUsuarioAutorizaParam = new SqlParameter("@IdUsuarioAutoriza", (object)anticiposEnc.IdUsuarioAutoriza ?? DBNull.Value);
                var fechaAutorizacionParam = new SqlParameter("@FechaAutorizacion", (object)anticiposEnc.FechaAutorizacion ?? DBNull.Value);
                var fechaEnvioParam = new SqlParameter("@FechaEnvio", (object)anticiposEnc.FechaEnvio ?? DBNull.Value);
                var enviadoParam = new SqlParameter("@Enviado", (object)anticiposEnc.Enviado ?? DBNull.Value);
                var autorizadoParam = new SqlParameter("@Autorizado", (object)anticiposEnc.Autorizado ?? DBNull.Value);
                var idTipoAnticipoParam = new SqlParameter("@IdTipoAnticipo", (object)anticiposEnc.IdTipoAnticipo ?? DBNull.Value);
                var estadoAnticipoParam = new SqlParameter("@EstadoAnticipo", (object)anticiposEnc.EstadoAnticipo ?? DBNull.Value);
                var idTipoMonedaParam = new SqlParameter("@IdTipoMoneda", (object)anticiposEnc.IdTipoMoneda ?? DBNull.Value);
                var tipoCambioParam = new SqlParameter("@TipoCambio", (object)anticiposEnc.TipoCambio ?? DBNull.Value);
                var clave1GParam = new SqlParameter("@Clave1G", (object)anticiposEnc.Clave1G ?? DBNull.Value);
                var estado1GParam = new SqlParameter("@Estado1G", (object)anticiposEnc.Estado1G ?? DBNull.Value);

                // Parámetro de salida
                var idAnticipoParam = new SqlParameter("@IdAnticipo", SqlDbType.Int, 100)
                {
                    Direction = ParameterDirection.Output
                };

                var mensjaeParam = new SqlParameter("@Mensage", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC [Anticipos].[InsertarEncabezado]
                            @IdCatEmpresa,
                            @IdUsuarioSolicita,
                            @IdUsuarioAutoriza,
                            @FechaAutorizacion,
                            @FechaEnvio,
                            @Enviado,
                            @Autorizado,
                            @IdTipoAnticipo,
                            @EstadoAnticipo,
                            @IdTipoMoneda,
                            @TipoCambio,
                            @Clave1G,
                            @Estado1G,
                            @IdAnticipo OUTPUT,
                            @Mensage OUTPUT",
                        idCatEmpresa,
                        idUsuarioSolicitaParam,
                        idUsuarioAutorizaParam,
                        fechaAutorizacionParam,
                        fechaEnvioParam,
                        enviadoParam,
                        autorizadoParam,
                        idTipoAnticipoParam,
                        estadoAnticipoParam,
                        idTipoMonedaParam,
                        tipoCambioParam,
                        clave1GParam,
                        estado1GParam,
                        idAnticipoParam,
                        mensjaeParam
                );

                if (idAnticipoParam.Value != DBNull.Value && int.TryParse(idAnticipoParam.Value.ToString(), out var idAnticipo))
                {
                    anticiposEnc.IdAnticiposEnc = idAnticipo;
                }
                else
                {
                    anticiposEnc.IdAnticiposEnc = 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepcion => {ex.Message}");
                return anticiposEnc;
            }

            return anticiposEnc;


        }

        public async Task<AnticiposDet> GuardarAnticipoDetalleAsync(AnticiposDet anticiposDet)
        {

            try
            {
                // Parámetros de entrada
                var idCatEmpresa = new SqlParameter("@IdAnticiposEnc", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 2
                };
                var idAnticipoEncParam = new SqlParameter("@IdAnticiposEnc", (object)anticiposDet.IdAnticiposEnc ?? DBNull.Value);
                var idLineaNegocioParam = new SqlParameter("@IdLineaNegocio", (object)anticiposDet.IdLineaNegocio ?? DBNull.Value);
                var idOrdenParam = new SqlParameter("@IdOrden", (object)anticiposDet.IdOrden ?? DBNull.Value);
                var importeParam = new SqlParameter("@Importe", (object)anticiposDet.Importe ?? DBNull.Value);
                var refTransferenciaParam = new SqlParameter("@RefTransferencia", (object)anticiposDet.RefTransferencia ?? DBNull.Value);
                var idProveedorParam = new SqlParameter("@IdProveedor", (object)anticiposDet.IdProveedor ?? DBNull.Value);
                var rfcParam = new SqlParameter("@RFC", (object)anticiposDet.RFC ?? DBNull.Value);
                var cuentaClabeParam = new SqlParameter("@CuentaClabe", (object)anticiposDet.CuentaClabe ?? DBNull.Value);
                var refProveedorParam = new SqlParameter("@RefProveedor", (object)anticiposDet.RefProveedor ?? DBNull.Value);
                var idCatServiciosParam = new SqlParameter("@IdCatServicios", (object)anticiposDet.IdCatServicios ?? DBNull.Value);
                var cveServicioParam = new SqlParameter("@CveServicio", (object)anticiposDet.CveServicio ?? DBNull.Value);
                var idCargaParam = new SqlParameter("@IdCarga", (object)anticiposDet.IdCarga ?? DBNull.Value);
                var fechaAplicacionParam = new SqlParameter("@FechaAplicacion", (object)anticiposDet.FechaAplicacion ?? DBNull.Value);
                var tipoProvParam = new SqlParameter("@TipoProv", (object)anticiposDet.TipoProv ?? DBNull.Value);
                var refNumeroParam = new SqlParameter("@refNumero", (object)anticiposDet.refNumero ?? DBNull.Value);
                var cuentaBancariaParam = new SqlParameter("@CuentaBancaria", (object)anticiposDet.CuentaBancaria ?? DBNull.Value);
                var saldoAplicadoParam = new SqlParameter("@SaldoAplicado", (object)anticiposDet.SaldoAplicado ?? DBNull.Value);
                var txtMonexParam = new SqlParameter("@txt_app_monex_descargado", (object)anticiposDet.txt_app_monex_descargado ?? DBNull.Value);
                var clave1GParam = new SqlParameter("@Clave1G", (object)anticiposDet.Clave1G ?? DBNull.Value);
                var estado1GParam = new SqlParameter("@Estado1G", (object)anticiposDet.Estado1G ?? DBNull.Value);


                // Parámetro de salida
                var idAnticipoDetalleParam = new SqlParameter("@IdAnticipoDetalle", SqlDbType.Int, 100)
                {
                    Direction = ParameterDirection.Output
                };

                var mensjaeParam = new SqlParameter("@Mensage", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC [Anticipos].[InsertarDetalle]
                            @IdCatEmpresa,
                            @IdAnticiposEnc,
                            @IdLineaNegocio,
                            @IdOrden,
                            @Importe,
                            @RefTransferencia,
                            @IdProveedor,
                            @RFC,
                            @CuentaClabe,
                            @RefProveedor,
                            @IdCatServicios,
                            @CveServicio,
                            @IdCarga,
                            @FechaAplicacion,
                            @TipoProv,
                            @refNumero,
                            @CuentaBancaria,
                            @SaldoAplicado,
                            @txt_app_monex_descargado,
                            @Clave1G,
                            @Estado1G,
                            @IdAnticipoDetalle,
                            @Mensage",
                    idCatEmpresa,
                    idAnticipoEncParam,
                    idLineaNegocioParam,
                    idOrdenParam,
                    importeParam,
                    refTransferenciaParam,
                    idProveedorParam,
                    rfcParam,
                    cuentaClabeParam,
                    refProveedorParam,
                    idCatServiciosParam,
                    cveServicioParam,
                    idCargaParam,
                    fechaAplicacionParam,
                    tipoProvParam,
                    refNumeroParam,
                    cuentaBancariaParam,
                    saldoAplicadoParam,
                    txtMonexParam,
                    clave1GParam,
                    estado1GParam,
                    idAnticipoDetalleParam,
                    mensjaeParam
                );


                if (idAnticipoDetalleParam.Value != DBNull.Value && int.TryParse(idAnticipoDetalleParam.Value.ToString(), out var idAnticipoDetalle))
                {
                    anticiposDet.IdAnticiposDet = idAnticipoDetalle;
                }
                else
                {
                    anticiposDet.IdAnticiposDet = 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepcion => {ex.Message}");
                return anticiposDet;
            }

            return anticiposDet;
        }

        #endregion Métodos para la generación de anticipos

        #region Métodos para la integración con 1G

        public async Task<RespuestaGenericaDTO> GuardarIntegracionReferenciaAsync(IntegraReferencia integraReferencia)
        {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();

            try
            {
                // Parámetros de entrada
                var idOrdenParam = new SqlParameter("@IdOrden", (object)integraReferencia.IdOrden ?? DBNull.Value);
                var referenciaAloParam = new SqlParameter("@ReferenciaALO", (object)integraReferencia.ReferenciaALO ?? DBNull.Value);
                var referenciaClienteExternoParam = new SqlParameter("@ReferenciaClienteExterno", (object)integraReferencia.ReferenciaClienteExterno ?? DBNull.Value);
                var claveClienteExternoParam = new SqlParameter("@ClaveClienteExterno", (object)integraReferencia.ClaveClienteExterno ?? DBNull.Value);
                var aduanaParam = new SqlParameter("@Aduana", (object)integraReferencia.Aduana ?? DBNull.Value);

                // Parámetro de salida
                var idIntegracionReferenciaParam = new SqlParameter("@IdIntegracionReferencia", SqlDbType.Int, 100)
                {
                    Direction = ParameterDirection.Output
                };

                var mensajeErrorParam = new SqlParameter("@MensajeError", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC [dbo].[InsertaIntegracionReferencia]
                        @IdOrden,
                        @ReferenciaALO,
                        @ReferenciaClienteExterno,
                        @ClaveClienteExterno,
                        @Aduana,
                        @IdIntegracionReferencia OUTPUT,
                        @MensajeError OUTPUT",
                    idOrdenParam,
                    referenciaAloParam,
                    referenciaClienteExternoParam,
                    claveClienteExternoParam,
                    aduanaParam,
                    idIntegracionReferenciaParam,
                    mensajeErrorParam
                );

                if (idIntegracionReferenciaParam.Value != DBNull.Value && int.TryParse(idIntegracionReferenciaParam.Value.ToString(), out var idIntReferencia))
                {
                    integraReferencia.IdIntReferencia = idIntReferencia;
                    respuesta.StatusCode = HttpStatusCode.OK;
                    respuesta.IsSuccess = true;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuesta.IsSuccess = false;
                    integraReferencia.IdIntReferencia = 0;
                    var mensajeError = mensajeErrorParam.Value?.ToString();
                    respuesta.lstrErrorMessages.AddRange(mensajeError.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
                }

                respuesta.Entidad = integraReferencia;

            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = $"Error inesperado al guardar la referencias de facturación {integraReferencia.ReferenciaALO}.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }

            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> GuardarIntegracionFacturaEncAsync(IntegraFacturaEnc integraFacturaEnc)
        {

            var respuesta = new RespuestaGenericaDTO();

            try
            {
                // Parámetros
                var idOrdenParam = new SqlParameter("@pIdOrden", (object)integraFacturaEnc.IdOrden ?? DBNull.Value);
                var idIntFacturaEncParam = new SqlParameter("@pIdIntFacturaEnc", (object)integraFacturaEnc.IdIntFacturaEnc ?? DBNull.Value);
                var claveMonedaSatParam = new SqlParameter("@pClaveSATMoneda", (object)integraFacturaEnc.ClaveSATMoneda ?? DBNull.Value);
                var referenciaAloParam = new SqlParameter("@pReferenciaAlo", (object)integraFacturaEnc.Referencia ?? DBNull.Value);
                var comentarioParam = new SqlParameter("@pComentarioEnc", (object)integraFacturaEnc.Comentario ?? DBNull.Value);

                var idIntegracionFacturaEncParam = new SqlParameter("@pIdIntFacturaGenerado", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                var mensajesErrorParam = new SqlParameter("@pMensajesError", SqlDbType.VarChar, 2000)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar SP
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC [Vacios].[InsertaIntegracionFacturaEnc]
                @pIdOrden,
                @pIdIntFacturaEnc,
                @pClaveSATMoneda,
                @pReferenciaAlo,
                @pComentarioEnc,
                @pIdIntFacturaGenerado OUTPUT,
                @pMensajesError OUTPUT",
                    idOrdenParam,
                    idIntFacturaEncParam,
                    claveMonedaSatParam,
                    referenciaAloParam,
                    comentarioParam,
                    idIntegracionFacturaEncParam,
                    mensajesErrorParam
                );

                var mensajesError = mensajesErrorParam.Value?.ToString();

                if (!string.IsNullOrWhiteSpace(mensajesError))
                {
                    respuesta.StatusCode = HttpStatusCode.BadRequest;
                    respuesta.IsSuccess = false;
                    respuesta.strMensaje = "Validación fallida al insertar la factura.";
                    /*
                     StringSplitOptions.RemoveEmptyEntries. Sirve para omitir elementos vacíos en la cadena.
                     Por ejemplo, si se encuentra algo como ;;, este elemento se omitirá
                     */
                    respuesta.lstrErrorMessages.AddRange(mensajesError.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
                    integraFacturaEnc.IdIntFacturaEnc = 0;
                }
                else
                {
                    respuesta.StatusCode = HttpStatusCode.OK;
                    respuesta.IsSuccess = true;
                    respuesta.strMensaje = "Factura generada correctamente.";
                    respuesta.Entidad = integraFacturaEnc;
                    integraFacturaEnc.IdIntFacturaEnc = Convert.ToInt32(idIntegracionFacturaEncParam.Value);
                }

            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = "Ocurrió un error inesperado.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }

            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> GuardarIntegracionFacturaDetAsync(IntegraFacturaDet integraFacturaDet)
        {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();

            try
            {
                // Parámetros de entrada
                var idContenedorParam = new SqlParameter("@pIdContenedor", (object)integraFacturaDet.IdPeticionesContenedor ?? DBNull.Value);
                var idIntFacturaEncParam = new SqlParameter("@pIdIntFacturaEnc", (object)integraFacturaDet.IddIntFacturaEnc ?? DBNull.Value);
                var idIntFacturaDetParam = new SqlParameter("@pIdIntFacturaDet", (object)integraFacturaDet.IdIntFacturaDet ?? DBNull.Value);
                var idCatServicioParam = new SqlParameter("@pIdCatServicio", (object)integraFacturaDet.IdCatServicio ?? DBNull.Value);
                var cantidadParam = new SqlParameter("@pCantidad", (object)integraFacturaDet.Cantidad ?? DBNull.Value);
                var precioParam = new SqlParameter("@pPrecio", (object)integraFacturaDet.Precio ?? DBNull.Value);
                var comentarioParam = new SqlParameter("@pComentarioDet", (object)integraFacturaDet.Comentario ?? DBNull.Value);


                // Parámetro de salida
                var idIntFacturaDetGeneradoParam = new SqlParameter("@pIdIntFacturaDetGenerado", SqlDbType.Int, 100)
                {
                    Direction = ParameterDirection.Output
                };

                var mensajesErrorParam = new SqlParameter("@pMensajesError", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC [Vacios].[InsertaIntegracionFacturaDet]
                        @pIdContenedor,
                        @pIdIntFacturaEnc,
                        @pIdIntFacturaDet,
                        @pIdCatServicio,
                        @pCantidad,
                        @pPrecio,
                        @pComentarioDet,
                        @pIdIntFacturaDetGenerado OUTPUT,
                        @pMensajesError OUTPUT",
                    idContenedorParam,
                    idIntFacturaEncParam,
                    idIntFacturaDetParam,
                    idCatServicioParam,
                    cantidadParam,
                    precioParam,
                    comentarioParam,
                    idIntFacturaDetGeneradoParam,
                    mensajesErrorParam
                );

                var mensajesError = mensajesErrorParam.Value?.ToString();

                if (idIntFacturaDetGeneradoParam.Value != DBNull.Value && int.TryParse(idIntFacturaDetGeneradoParam.Value.ToString(), out var idIntFcaturaDet))
                {
                    integraFacturaDet.IdIntFacturaDet = idIntFcaturaDet;
                    respuesta.StatusCode = HttpStatusCode.OK;
                    respuesta.IsSuccess = true;
                }
                else
                {
                    integraFacturaDet.IdIntFacturaDet = 0;
                    respuesta.lstrErrorMessages.AddRange(mensajesError.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
                }

            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = "Ocurrió un error inesperado.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }

            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> MarcarSolicitudFacturaListaAsync(int idIntFacturaEnc, int idCatEmpresa)
        {

            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();

            try
            {
                // Parámetros de entrada
                var idCatEmpresaParam = new SqlParameter("@IdCatEmpresa", (object)idCatEmpresa ?? DBNull.Value);
                var idIntFacturaEncParam = new SqlParameter("@pIdIntFacturaEnc", (object)idIntFacturaEnc ?? DBNull.Value);


                // Parámetro de salida
                var mensajeParam = new SqlParameter("@pMensaje", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC [Comun].[MarcarSolicitudesFacturasListas]
                        @IdCatEmpresa,
                        @pIdIntFacturaEnc,
                        @pMensaje OUTPUT",
                    idCatEmpresaParam,
                    idIntFacturaEncParam,
                    mensajeParam
                );

                var mensajesError = mensajeParam.Value?.ToString();

                if (string.IsNullOrEmpty(mensajesError))
                {
                    respuesta.StatusCode = HttpStatusCode.OK;
                    respuesta.IsSuccess = true;
                }
                else
                {
                    respuesta.lstrErrorMessages.AddRange(mensajesError.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
                }

            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = "Ocurrió un error inesperado.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }

            return respuesta;
        }

        public async Task<RespuestaGenericaDTO> GetReferenciaAloFacturacionPorReferenciaCliente(string referenciaCliente) {
            RespuestaGenericaDTO respuesta = new RespuestaGenericaDTO();
            IntegraReferencia referencia = new IntegraReferencia();

            try
            {

                respuesta.StatusCode = HttpStatusCode.OK;
                respuesta.IsSuccess = true;
                // Parámetros de entrada
                var referenciaClienteParam = new SqlParameter("@pReferenciaCliente", (object)referenciaCliente ?? DBNull.Value);
                // Ejecutamos el SP con ExecuteSqlRawAsync
                var resultado = await _db.Set<IntegraReferencia>().FromSqlRaw(
                    @"EXEC [Vacios].[ReferenciaClienteTieneReferenciaAloFacturacion] @pReferenciaCliente", referenciaClienteParam).AsNoTracking().ToListAsync();

                referencia = resultado.FirstOrDefault();

            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.IsSuccess = false;
                respuesta.strMensaje = "Ocurrió un error inesperado.";
                respuesta.lstrErrorMessages.Add(ex.Message);
            }

            respuesta.Entidad = referencia;

            return respuesta;
        }
        
        public async Task<Dictionary<string, string>> GetReferenciaALOFacturacion(int idCatLineaNegocio, int idOrden, string referenciaClienteExterno)
        {
            var data = new Dictionary<string, string>();

            try
            {
                // Parámetros de entrada
                var pIdCatLineaNegocio = new SqlParameter("@pIdCatLineaNegocio", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idCatLineaNegocio
                };
                var pIdOrden = new SqlParameter("@pIdOrden", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idOrden
                };
                var pReferenciaClienteExterno = new SqlParameter("@pReferenciaClienteExterno", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = referenciaClienteExterno
                };

                // Parámetro de salida
                var referenciaAloFacturarOutput = new SqlParameter("@ReferenciaAloFacturar", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                var mensajeOutput = new SqlParameter("@Mensaje", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutamos el SP con ExecuteSqlRawAsync
                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC Comun.ObtenerReferenciaALOFacturacion 
                    @pIdCatLineaNegocio, 
                    @pIdOrden, 
                    @pReferenciaClienteExterno, 
                    @ReferenciaAloFacturar OUTPUT,
                    @Mensaje OUTPUT",
                        pIdCatLineaNegocio,
                        pIdOrden,
                        pReferenciaClienteExterno,
                        referenciaAloFacturarOutput,
                        mensajeOutput
                );

                // Tomamos el valor del parámetro de salida
                var referenciaAloFacturar = referenciaAloFacturarOutput.Value?.ToString() ?? string.Empty;
                var mensaje = mensajeOutput.Value?.ToString() ?? string.Empty;

                data.Add("referenciaAloFacturar", referenciaAloFacturar);
                data.Add("mensaje", mensaje);

                return data;
            }
            catch (Exception)
            {
                return data;
            }
        }
        #endregion Métodos para la integración con 1G
        public async Task<RespuestaGenericaDTO> crearReferenciaALO(SLOReferenciasDTO datos)
        {
            try
            {
                #region crear referencia alo
                var paramEmpresa = new SqlParameter("@pIdCatEmpresa", 1);
                var paramLineaNegocio = new SqlParameter("@pIdCatLineaNegocio", datos.IdCatLineaNegocio);
                var paramAduana = new SqlParameter("@pIdCatAduana", datos.IdCatAduana);
                var paramTipoOperacion = new SqlParameter("@pIdCatTipoOperacion", datos.TipoServicio);
                var paramCliente = new SqlParameter("@pIdCatCliente", datos.IdCliente);
                var paramEsEdicion = new SqlParameter("@pEsEdicion", false);
                var paramEditar = new SqlParameter("@pReferenciaAloEditar", "");

                var paramReferenciaAlo = new SqlParameter("@pReferenciaAlo", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                var paramProyecto = new SqlParameter("@pIdCatProyecto", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0
                };
                var paramMercancia = new SqlParameter("@pIdCatMercancia", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0
                };

                var Existencia = await _db.SLOintegracionReferencia
                    .AnyAsync(x => x.ReferenciaClienteExterno == datos.ReferenciaCliente);

                if (Existencia)
                {
                    return new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = "Esa referencia ya está ocupada, favor de colocar otra."
                    };
                }


                await _db.Database.ExecuteSqlRawAsync(
                    @"EXEC Comun.GenerarFormatoReferenciaAlo 
                    @pIdCatEmpresa, 
                    @pIdCatLineaNegocio, 
                    @pIdCatAduana, 
                    @pIdCatProyecto,
                    @pIdCatMercancia, 
                    @pIdCatTipoOperacion, 
                    @pIdCatCliente, 
                    @pEsEdicion,
                    @pReferenciaAloEditar,
                    @pReferenciaAlo OUTPUT",
                        paramEmpresa,
                        paramLineaNegocio,
                        paramAduana,
                        paramProyecto,
                        paramMercancia,
                        paramTipoOperacion,
                        paramCliente,
                        paramEsEdicion,
                        paramEditar,
                        paramReferenciaAlo
                );

                //return paramReferenciaAlo.Value?.ToString() ?? string.Empty;
                string referenciaGenerada = paramReferenciaAlo.Value?.ToString();

                if (string.IsNullOrWhiteSpace(referenciaGenerada))
                {
                    return new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = "No se generó una referencia válida."
                    };
                }

                #endregion crear referencia alo

                // Segundo SP: InsertarOrdenes                
                using var command = _db.Database.GetDbConnection().CreateCommand();
                command.CommandText = "Comun.InsertarOrdenes";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@IdCatCliente", datos.IdCliente));
                //command.Parameters.Add(new SqlParameter("@IdCatClienteFacturar", datos.IdClienteFacturar));
                command.Parameters.Add(new SqlParameter("@IdCatAduana", datos.IdCatAduana));
                command.Parameters.Add(new SqlParameter("@IdCatEmpresa", 1));
                command.Parameters.Add(new SqlParameter("@IdCatLineaNegocio", datos.IdCatLineaNegocio));
                command.Parameters.Add(new SqlParameter("@IdCatProyecto", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@IdUsuario", datos.IdCatUsuario));
                command.Parameters.Add(new SqlParameter("@ReferenciaAlo", referenciaGenerada));
                //command.Parameters.Add(new SqlParameter("@ReferenciaCliente", datos.ReferenciaCliente ?? ""));

                var pIdOrdenGenerada = new SqlParameter("@IdOrdenGenerada", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pIdOrdenGenerada);

                var pMensajesErrore = new SqlParameter("@MensajesErrore", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pMensajesErrore);

                // Abrir conexión si no está abierta
                if (command.Connection.State != ConnectionState.Open)
                    await command.Connection.OpenAsync();

                // Ejecutar el comando
                await command.ExecuteNonQueryAsync();

                // Leer valores
                int idOrden = pIdOrdenGenerada.Value != DBNull.Value ? (int)pIdOrdenGenerada.Value : 0;
                string respuestaFinal = pMensajesErrore.Value?.ToString() ?? "Sin mensaje";

                
                // Tercer SP: Crear entrada en peticionesReferencias                
                using var CRUD_peticionesReferencias = _db.Database.GetDbConnection().CreateCommand();
                CRUD_peticionesReferencias.CommandText = "SLO.CRUD_peticionesReferencias";
                CRUD_peticionesReferencias.CommandType = CommandType.StoredProcedure;

                // Abrir conexión si es necesario
                if (CRUD_peticionesReferencias.Connection.State != ConnectionState.Open)
                    await CRUD_peticionesReferencias.Connection.OpenAsync();

                // Parámetros de entrada
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@Opcion", 1));
                var pIdReferencia = new SqlParameter("@IdReferencia", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0
                };                
                CRUD_peticionesReferencias.Parameters.Add(pIdReferencia);
                var pTicket = new SqlParameter("@Ticket", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0
                };
                CRUD_peticionesReferencias.Parameters.Add(pTicket);
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@Transporte_RFC", DBNull.Value));
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@Transporte_RazonSocial", DBNull.Value));
                var pTrasporteId = new SqlParameter("@TrasporteId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0
                };
                CRUD_peticionesReferencias.Parameters.Add(pTrasporteId);
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@Transporte_Usuario", DBNull.Value));
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@Transporte_UsuarioEmail", DBNull.Value));
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@Comentarios", DBNull.Value));
                var pTipoReferencia = new SqlParameter("@TipoReferencia", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0
                };
                CRUD_peticionesReferencias.Parameters.Add(pTipoReferencia);
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@Procesado", DBNull.Value));
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@EstadoReferencia", "Abierto"));
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@IdCatReferenciaEstado", 1));
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@Activo", true));
                CRUD_peticionesReferencias.Parameters.Add(new SqlParameter("@IdOrden", idOrden));

                // Parámetros de salida
                var pIdReferenciaGenerada = new SqlParameter("@IdReferenciaGenerada", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                CRUD_peticionesReferencias.Parameters.Add(pIdReferenciaGenerada);

                var pMensage = new SqlParameter("@Mensage", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                CRUD_peticionesReferencias.Parameters.Add(pMensage);

                // Ejecutar el procedimiento almacenado
                await CRUD_peticionesReferencias.ExecuteNonQueryAsync();

                // Ahora puedes leer los valores de salida
                var resultadoMensaje = pMensage.Value?.ToString();
                var idReferenciaGenerada = pIdReferenciaGenerada.Value != DBNull.Value
                    ? Convert.ToInt32(pIdReferenciaGenerada.Value)
                    : 0;

                if (!string.IsNullOrWhiteSpace(resultadoMensaje))
                {
                    return new RespuestaGenericaDTO
                    {
                        IsSuccess = false,
                        strMensaje = $"Error al crear la petición de referencia: {resultadoMensaje}"
                    };
                }

                // Si todo fue bien
                return new RespuestaGenericaDTO
                {
                    IsSuccess = true,
                    strMensaje = $"Petición de referencia creada con éxito. ID generado: {idReferenciaGenerada}"
                };
            }
            catch (Exception ex)
            {
                return new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    strMensaje = $"Error: {ex.Message}"
                };
            }
        }        

        public async Task<RespuestaGenericaDTO> crearServicios(List<SLOPeticionesContenedores> contenedores)
        {
            using var connection = _db.Database.GetDbConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction(); // Inicia la transacción

            try
            {
                foreach (var item in contenedores)
                {
                    if (item.SLOpeticionesReferencias?.ordenes?.ReferenciaALO == null)
                    {
                        return new RespuestaGenericaDTO
                        {
                            IsSuccess = false,
                            strMensaje = "Faltan datos obligatorios: ReferenciaALO no fue proporcionada."
                        };
                    }

                    var referencia = item.SLOpeticionesReferencias?.ordenes?.ReferenciaALO ?? string.Empty;
                    foreach (var servicios in item.SLOpeticionesServicios)
                    {
                        using var command = connection.CreateCommand();
                        command.Transaction = transaction;

                        command.CommandText = "SLO.GuardarServiciosFacturacion";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@Opcion", 1));
                        command.Parameters.Add(new SqlParameter("@ReferenciaAlo", referencia));
                        var pIdContenedor = new SqlParameter("@IdContenedor", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Input,
                            Value = 0
                        };
                        command.Parameters.Add(pIdContenedor); // este se llenará dentro del SP
                                                               //command.Parameters.Add(new SqlParameter("@Contenedor", item.Contenedor ?? ""));
                                                               //command.Parameters.Add(new SqlParameter("@BL", item.BL ?? ""));
                        command.Parameters.Add(new SqlParameter("@Contenedor", item.Contenedor?.Trim() == "N/A" ? "" : item.Contenedor ?? ""));
                        command.Parameters.Add(new SqlParameter("@BL", item.BL?.Trim() == "N/A" ? "" : item.BL ?? ""));
                        command.Parameters.Add(new SqlParameter("@ClaveTipoContenedor", string.IsNullOrWhiteSpace(item.ClaveTipoContenedor) ? DBNull.Value : item.ClaveTipoContenedor));
                        var pPatioId = new SqlParameter("@PatioId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Input,
                            Value = 0
                        };
                        command.Parameters.Add(pPatioId);
                        //command.Parameters.Add(new SqlParameter("@PatioId", item.PatioId));
                        command.Parameters.Add(new SqlParameter("@Moneda", servicios.Moneda));
                        var pIdCatNaviera = new SqlParameter("@IdCatNaviera", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Input,
                            Value = 0
                        };
                        command.Parameters.Add(pIdCatNaviera);
                        //command.Parameters.Add(new SqlParameter("@IdCatNaviera", item.Naviera_Id == 0 ? DBNull.Value : item.Naviera_Id));
                        command.Parameters.Add(new SqlParameter("@IdEstadoContenedor", 1));
                        command.Parameters.Add(new SqlParameter("@Activo", true));
                        command.Parameters.Add(new SqlParameter("@IdCatServicio", servicios.IdCatServicio));
                        command.Parameters.Add(new SqlParameter("@IdEstadoServicio", 1));
                        command.Parameters.Add(new SqlParameter("@Monto", servicios.Monto));
                        command.Parameters.Add(new SqlParameter("@Cantidad", servicios.Cantidad));
                        command.Parameters.Add(new SqlParameter("@IdCatClienteFacturar", servicios.IdClienteFacturarA));
                        command.Parameters.Add(new SqlParameter("@ReferenciaFacturacion", servicios.ReferenciaClienteFactura));

                        var pIdPeticionContenedor = new SqlParameter("@IdPeticionContenedor", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(pIdPeticionContenedor);

                        var pIdPeticionServicio = new SqlParameter("@IdPeticionServicio", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(pIdPeticionServicio);

                        var pRespuesta = new SqlParameter("@Respuesta", SqlDbType.VarChar, 1000) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(pRespuesta);

                        await command.ExecuteNonQueryAsync();

                        var mensaje = pRespuesta.Value?.ToString();
                        if (!string.IsNullOrWhiteSpace(mensaje))
                        {
                            return new RespuestaGenericaDTO
                            {
                                IsSuccess = false,
                                strMensaje = $"Error al guardar facturación para referencia {referencia}: {mensaje}"
                            };
                        }

                        // Puedes guardar los Id generados si los necesitas
                        var idContenedor = Convert.ToInt32(pIdPeticionContenedor.Value);
                        var idServicio = Convert.ToInt32(pIdPeticionServicio.Value);
                    }
                }

                await transaction.CommitAsync();
                return new RespuestaGenericaDTO
                {
                    IsSuccess = true,
                    strMensaje = "Servicios guardados correctamente"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Revierte todo si hay error
                return new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    strMensaje = $"Error: {ex.Message}"
                };
            }
        }

        //public async Task<RespuestaGenericaDTO> EnvioaFacturacion(Ordenes servicios)
        //{
        //    try
        //    {
        //        if (servicios == null || servicios.Count == 0)
        //            return new RespuestaGenericaDTO { IsSuccess = false, strMensaje = "No hay servicios para facturar." };

        //        var objOrden = servicios.FirstOrDefault().SLOpeticionesContenedores.SLOpeticionesReferencias.ordenes;

        //        if (objOrden == null)
        //            return new RespuestaGenericaDTO { IsSuccess = false, strMensaje = "Falta información de la orden." };

        //        foreach (var servicio in servicios)
        //        {

        //            string referenciaAloFacturar = null;
        //            string mensajeSP = null;

        //            using (var command = _db.Database.GetDbConnection().CreateCommand())
        //            {
        //                command.CommandText = "[Comun].[ObtenerReferenciaALOFacturacion]";
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.Add(new SqlParameter("@pIdCatLineaNegocio", objOrden.IdCatLineaNegocio));
        //                command.Parameters.Add(new SqlParameter("@pIdOrden", objOrden.IdOrden));
        //                command.Parameters.Add(new SqlParameter("@pReferenciaClienteExterno", servicio.ReferenciaClienteFactura?? ""));
        //                command.Parameters.Add(new SqlParameter("@pIdCatClienteFacturar", servicio.IdClienteFacturarA));

        //                var paramReferenciaOut = new SqlParameter("@ReferenciaAloFacturar", SqlDbType.NVarChar, 100)
        //                {
        //                    Direction = ParameterDirection.Output
        //                };
        //                var paramMensajeOut = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 100)
        //                {
        //                    Direction = ParameterDirection.Output
        //                };

        //                command.Parameters.Add(paramReferenciaOut);
        //                command.Parameters.Add(paramMensajeOut);

        //                if (command.Connection.State != ConnectionState.Open)
        //                    await command.Connection.OpenAsync();

        //                await command.ExecuteNonQueryAsync();

        //                referenciaAloFacturar = paramReferenciaOut.Value?.ToString();
        //                mensajeSP = paramMensajeOut.Value?.ToString();
        //            }

        //            var referenciaFinal = string.IsNullOrWhiteSpace(referenciaAloFacturar)
        //                ? objOrden.ReferenciaALO
        //                : referenciaAloFacturar;


        //                // Agregar a la tabla SLOIntegracionReferencia
        //            var cont = servicio.SLOpeticionesContenedores;
        //            var referenciaClienteExterno = servicio.ReferenciaClienteFactura;
        //            var claveClienteExterno = servicio.IdClienteFacturarA.ToString();

        //            var Clientedatos = await _db.catClientes
        //                   .FirstOrDefaultAsync(x =>
        //                       x.IdCatCliente == servicio.IdClienteFacturarA);

        //            // Verifica si ya existe una referencia con los mismos valores
        //            var yaExiste = await _db.SLOintegracionReferencia.AnyAsync(x =>
        //                x.ReferenciaALO == referenciaFinal &&
        //                x.ReferenciaClienteExterno == referenciaClienteExterno &&
        //                x.ClaveClienteExterno == claveClienteExterno);

        //            if (yaExiste)
        //            {
        //                // Obtener el objeto de referencia existente
        //                var referenciaExistente = await _db.SLOintegracionReferencia
        //                    .FirstOrDefaultAsync(x =>
        //                        x.ReferenciaALO == referenciaFinal &&
        //                        x.ReferenciaClienteExterno == referenciaClienteExterno &&
        //                        x.ClaveClienteExterno == claveClienteExterno);

        //                // Verificar si ya existe encabezado de factura
        //                var factEncExistente = await _db.SLOintegracionFacturaEnc
        //                    .FirstOrDefaultAsync(x =>
        //                        x.Referencia == referenciaFinal &&
        //                        x.ClaveSATMoneda == servicio.Moneda &&
        //                        x.ClaveClienteExterno == claveClienteExterno);

        //                if (factEncExistente != null)
        //                {
        //                    var AgregarFactEnc = new SLOIntegraFacturaEnc
        //                    {
        //                        IdOrden = objOrden.IdOrden,
        //                        IdPeticionesReferencia = servicio.SLOpeticionesContenedores.SLOpeticionesReferencias.IdReferencia,
        //                        IdSolicitudFacturacion = "",
        //                        IdCompaniaExterna = "2",
        //                        ClaveClienteExterno = servicio.IdClienteFacturarA.ToString(),
        //                        Fecha = DateTime.Now.ToString(),
        //                        ConceptoFacturacion = "3",
        //                        Comentario = servicio.SLOpeticionesContenedores.SLOpeticionesReferencias.ordenes.SLOintegracionFacturaEnc.FirstOrDefault()?.Comentario ?? "",
        //                        Nota = "",
        //                        Referencia = referenciaFinal,
        //                        ClaveSATMoneda = servicio.Moneda,
        //                        ClaveSATUsoCFDI = "G03",
        //                        RFC = Clientedatos.RFC,
        //                        FacturacionAutomatica = false,
        //                        CierreReferencia = false,
        //                        Activo = true,
        //                        FechaRegistro = DateTime.Now,
        //                        Enviado = false,
        //                        IdSucursalExterna = 4,
        //                        CodigoPostal = Clientedatos.CodigoPostal,
        //                        Serie = "SLO",
        //                        IdIntReferencia = referenciaExistente.IdIntReferencia
        //                    };
        //                    _db.SLOintegracionFacturaEnc.Add(AgregarFactEnc);
        //                    await _db.SaveChangesAsync();
        //                }

        //                // Validar si ya existe detalle
        //                var existeDet = await _db.SLOintegracionFacturaDet
        //                    .AnyAsync(x => x.Idpservicios == servicio.IdServicio);

        //                if (!existeDet)
        //                {
        //                    var factEnc = await _db.SLOintegracionFacturaEnc
        //                    .FirstOrDefaultAsync(x =>
        //                        x.Referencia == referenciaFinal &&
        //                        x.ClaveSATMoneda == servicio.Moneda &&
        //                        x.ClaveClienteExterno == claveClienteExterno);

        //                    var AgregarFactDet = new SLOIntegraFacturaDet
        //                    {
        //                        IddIntFacturaEnc = factEnc.IdIntFacturaEnc,
        //                        ClaveServicio = servicio.IdCatServicio.ToString(),
        //                        Cantidad = servicio.Cantidad.ToString(),
        //                        Precio = servicio.Monto.ToString(),
        //                        IdPeticionesReferencia = servicio.SLOpeticionesContenedores.SLOpeticionesReferencias.IdReferencia,
        //                        IdPeticionesContenedor = servicio.SLOpeticionesContenedores.IdContenedor,
        //                        Contenedor = servicio.SLOpeticionesContenedores.Contenedor ?? "",
        //                        Bl = servicio.SLOpeticionesContenedores.BL ?? "",
        //                        CentroCostos = "4",
        //                        EIR = "",
        //                        Comentario = servicio.SLOpeticionesContenedores.SLOpeticionesReferencias.ordenes.SLOintegracionFacturaEnc.FirstOrDefault()?.SLOintegracionFacturaDet.FirstOrDefault()?.Comentario ?? "",
        //                        Nota = "",
        //                        IdCatServicio = servicio.IdCatServicio ?? 0,
        //                        Idpservicios = servicio.IdServicio
        //                    };

        //                    _db.SLOintegracionFacturaDet.Add(AgregarFactDet);
        //                    await _db.SaveChangesAsync();
        //                }
        //                continue;
        //            }                

        //            var nuevaReferencia = new SLOIntegracionReferencia
        //            {
        //                IdOrden = objOrden.IdOrden,
        //                IdCompaniaExterna = "2",
        //                ReferenciaALO = referenciaFinal,
        //                ReferenciaClienteExterno = servicio.ReferenciaClienteFactura,
        //                ClaveClienteExterno = servicio.IdClienteFacturarA.ToString(),
        //                Aduana = servicio.SLOpeticionesContenedores.Aduana,
        //                Activo = true,
        //                FechaRegistro = DateTime.Now,
        //                FechaEnvio = DateTime.Now,
        //                Enviado = false
        //            };
        //            _db.SLOintegracionReferencia.Add(nuevaReferencia);
        //            await _db.SaveChangesAsync();

        //            var nuevaFactEnc = new SLOIntegraFacturaEnc
        //            {
        //                IdOrden = objOrden.IdOrden,
        //                IdPeticionesReferencia = servicio.SLOpeticionesContenedores.SLOpeticionesReferencias.IdReferencia,
        //                IdSolicitudFacturacion = "",
        //                IdCompaniaExterna = "2",
        //                ClaveClienteExterno = servicio.IdClienteFacturarA.ToString(),
        //                Fecha = DateTime.Now.ToString(),
        //                ConceptoFacturacion = "3",
        //                Comentario = servicio.SLOpeticionesContenedores.SLOpeticionesReferencias.ordenes.SLOintegracionFacturaEnc.FirstOrDefault()?.Comentario ?? "",
        //                Nota = "",
        //                Referencia = referenciaFinal,
        //                ClaveSATMoneda = servicio.Moneda,
        //                ClaveSATUsoCFDI = "G03",
        //                RFC = Clientedatos.RFC,
        //                FacturacionAutomatica = false,
        //                CierreReferencia = false,
        //                Activo = true,
        //                FechaRegistro = DateTime.Now,
        //                Enviado = false,
        //                IdSucursalExterna = 4,
        //                CodigoPostal = Clientedatos.CodigoPostal,
        //                Serie = "SLO",
        //                IdIntReferencia = nuevaReferencia.IdIntReferencia
        //            };
        //            _db.SLOintegracionFacturaEnc.Add(nuevaFactEnc);
        //            await _db.SaveChangesAsync();

        //            var nuevaFactDet = new SLOIntegraFacturaDet
        //            {
        //                IddIntFacturaEnc = nuevaFactEnc.IdIntFacturaEnc,
        //                ClaveServicio = servicio.IdCatServicio.ToString(),
        //                Cantidad = servicio.Cantidad.ToString(),
        //                Precio = servicio.Monto.ToString(),
        //                IdPeticionesReferencia = servicio.SLOpeticionesContenedores.SLOpeticionesReferencias.IdReferencia,
        //                IdPeticionesContenedor = servicio.SLOpeticionesContenedores.IdContenedor,
        //                Contenedor = servicio.SLOpeticionesContenedores.Contenedor ?? "",
        //                Bl = servicio.SLOpeticionesContenedores.BL ?? "",
        //                CentroCostos = "4",
        //                EIR = "",
        //                Comentario = servicio.SLOpeticionesContenedores.SLOpeticionesReferencias.ordenes.SLOintegracionFacturaEnc.FirstOrDefault()?.SLOintegracionFacturaDet.FirstOrDefault()?.Comentario ?? "",
        //                Nota = "",
        //                IdCatServicio = servicio.IdCatServicio ?? 0,
        //                Idpservicios = servicio.IdServicio
        //            };
        //            _db.SLOintegracionFacturaDet.Add(nuevaFactDet);
        //            await _db.SaveChangesAsync();
        //        }

        //        return new RespuestaGenericaDTO
        //        {
        //            IsSuccess = true,
        //            strMensaje = $"Se generaron referencias ligadas a ALO correctamente"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new RespuestaGenericaDTO
        //        {
        //            IsSuccess = false,
        //            strMensaje = "Hubo errores"
        //        };
        //    }
        //}

        public async Task<RespuestaGenericaDTO> EnvioaFacturacion(Ordenes orden)
        {
            try
            {
                if (orden == null || orden.SLOpeticionesReferencias == null)
                    return new RespuestaGenericaDTO { IsSuccess = false, strMensaje = "No hay servicios para facturar." };

                var servicios = orden.SLOpeticionesReferencias
                    .SelectMany(r => r.SLOpeticionesContenedores ?? new List<SLOPeticionesContenedores>())
                    .SelectMany(c => c.SLOpeticionesServicios ?? new List<SLOPeticionesServicios>())
                    .Where(s => s != null)
                    .ToList();

                if (servicios.Count == 0)
                    return new RespuestaGenericaDTO { IsSuccess = false, strMensaje = "No hay servicios disponibles para facturar." };

                var objOrden = orden;

                // Agrupar por combinación de referencia y cliente a facturar
                var grupos = servicios
                    .GroupBy(s => new { s.ReferenciaClienteFactura, s.IdClienteFacturarA, s.Moneda});

                foreach (var grupo in grupos)
                {
                    var primerServicio = grupo.First();

                    string referenciaAloFacturar = null;
                    string mensajeSP = null;

                    using (var command = _db.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "[Comun].[ObtenerReferenciaALOFacturacion]";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@pIdCatLineaNegocio", objOrden.IdCatLineaNegocio));
                        command.Parameters.Add(new SqlParameter("@pIdOrden", objOrden.IdOrden));
                        command.Parameters.Add(new SqlParameter("@pReferenciaClienteExterno", grupo.Key.ReferenciaClienteFactura ?? ""));
                        command.Parameters.Add(new SqlParameter("@pIdCatClienteFacturar", grupo.Key.IdClienteFacturarA));

                        var paramReferenciaOut = new SqlParameter("@ReferenciaAloFacturar", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var paramMensajeOut = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(paramReferenciaOut);
                        command.Parameters.Add(paramMensajeOut);

                        if (command.Connection.State != ConnectionState.Open)
                            await command.Connection.OpenAsync();

                        await command.ExecuteNonQueryAsync();

                        referenciaAloFacturar = paramReferenciaOut.Value?.ToString();
                        mensajeSP = paramMensajeOut.Value?.ToString();
                    }

                    var referenciaFinal = string.IsNullOrWhiteSpace(referenciaAloFacturar)
                        ? objOrden.ReferenciaALO
                        : referenciaAloFacturar;

                    var clienteDatos = await _db.catClientes
                        .FirstOrDefaultAsync(x => x.IdCatCliente == grupo.Key.IdClienteFacturarA);

                    var yaExiste = await _db.SLOintegracionReferencia.AnyAsync(x =>
                        x.ReferenciaALO == referenciaFinal &&
                        x.ReferenciaClienteExterno == grupo.Key.ReferenciaClienteFactura &&
                        x.ClaveClienteExterno == grupo.Key.IdClienteFacturarA.ToString());

                    if (!yaExiste)
                    {
                        var nuevaReferencia = new SLOIntegracionReferencia
                        {
                            IdOrden = objOrden.IdOrden,
                            IdCompaniaExterna = "2",
                            ReferenciaALO = referenciaFinal,
                            ReferenciaClienteExterno = grupo.Key.ReferenciaClienteFactura,
                            ClaveClienteExterno = grupo.Key.IdClienteFacturarA.ToString(),
                            Aduana = primerServicio.SLOpeticionesContenedores?.Aduana ?? "",
                            Activo = true,
                            FechaRegistro = DateTime.Now,
                            FechaEnvio = DateTime.Now,
                            Enviado = false
                        };
                        _db.SLOintegracionReferencia.Add(nuevaReferencia);
                        await _db.SaveChangesAsync();
                    }

                    var referenciaExistente = await _db.SLOintegracionReferencia
                        .FirstOrDefaultAsync(x =>
                            x.ReferenciaALO == referenciaFinal &&
                            x.ReferenciaClienteExterno == grupo.Key.ReferenciaClienteFactura &&
                            x.ClaveClienteExterno == grupo.Key.IdClienteFacturarA.ToString());

                    //var encabezadoYaExiste = await _db.SLOintegracionFacturaEnc
                    //    .AnyAsync(x =>
                    //        x.Referencia == referenciaFinal &&
                    //        x.ClaveSATMoneda == primerServicio.Moneda &&
                    //        x.ClaveClienteExterno == grupo.Key.IdClienteFacturarA.ToString());

                    var idsServiciosGrupo = grupo.Select(g => g.IdServicio).ToList();

                    var yaExisteDet = await _db.SLOintegracionFacturaDet
                        .AnyAsync(x => idsServiciosGrupo.Contains(x.Idpservicios));

                    if (!yaExisteDet)
                    {
                        var listaDetalles = grupo.Select(servicio => new SLOIntegraFacturaDet
                        {
                            ClaveServicio = servicio.IdCatServicio.ToString(),
                            Cantidad = servicio.Cantidad.ToString(),
                            Precio = servicio.Monto.ToString(),
                            IdPeticionesReferencia = servicio.SLOpeticionesContenedores?.SLOpeticionesReferencias?.IdReferencia ?? 0,
                            IdPeticionesContenedor = servicio.SLOpeticionesContenedores?.IdContenedor ?? 0,
                            Contenedor = servicio.SLOpeticionesContenedores?.Contenedor ?? "",
                            Bl = servicio.SLOpeticionesContenedores?.BL ?? "",
                            CentroCostos = "4",
                            EIR = "",
                            Comentario = orden.SLOintegracionFacturaEnc?.FirstOrDefault()?.SLOintegracionFacturaDet?.FirstOrDefault()?.Comentario ?? "",
                            Nota = "",
                            IdCatServicio = servicio.IdCatServicio ?? 0,
                            Idpservicios = servicio.IdServicio
                        }).ToList();

                        var nuevoEncabezado = new SLOIntegraFacturaEnc
                        {
                            IdOrden = objOrden.IdOrden,
                            IdPeticionesReferencia = primerServicio.SLOpeticionesContenedores?.SLOpeticionesReferencias?.IdReferencia ?? 0,
                            IdSolicitudFacturacion = "",
                            IdCompaniaExterna = "2",
                            ClaveClienteExterno = grupo.Key.IdClienteFacturarA.ToString(),
                            Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                            ConceptoFacturacion = "3",
                            Comentario = orden.SLOintegracionFacturaEnc?.FirstOrDefault()?.Comentario ?? "",
                            Nota = "",
                            Referencia = referenciaFinal,
                            ClaveSATMoneda = primerServicio.Moneda,
                            ClaveSATUsoCFDI = "G03",
                            RFC = clienteDatos?.RFC ?? "",
                            FacturacionAutomatica = false,
                            CierreReferencia = false,
                            Activo = true,
                            FechaRegistro = DateTime.Now,
                            Enviado = false,
                            IdSucursalExterna = 4,
                            CodigoPostal = clienteDatos?.CodigoPostal ?? "",
                            Serie = "SLO",
                            IdIntReferencia = referenciaExistente.IdIntReferencia,
                            EstaListo = false,
                            SLOintegracionFacturaDet = listaDetalles
                        };

                        //nuevoEncabezado.EstaListo = true;
                        _db.SLOintegracionFacturaEnc.Add(nuevoEncabezado);
                        await _db.SaveChangesAsync();

                        nuevoEncabezado.EstaListo = true;
                        _db.SLOintegracionFacturaEnc.Update(nuevoEncabezado);
                        await _db.SaveChangesAsync();
                    }

                }


                return new RespuestaGenericaDTO
                {
                    IsSuccess = true,
                    strMensaje = $"Se ha facturado de forma correctamente"
                };
            }
            catch (Exception ex)
            {
                return new RespuestaGenericaDTO
                {
                    IsSuccess = false,
                    strMensaje = $"Hubo errores: {ex.Message}"
                };
            }
        }
    }
}