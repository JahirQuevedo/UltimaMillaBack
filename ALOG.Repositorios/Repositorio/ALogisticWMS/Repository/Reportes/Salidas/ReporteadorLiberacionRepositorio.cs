using System.Data;
using System.Text;
using System.Transactions;
using ALOG.Enums;
using ALOG.InfraestructuraReportes;
using ALOG.Modelos;
using ALOG.Modelos.ModelosWMS.Reportes.Salidas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Aggregates.Chart;
using NPOI.HSSF.UserModel;
using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXmlFormats;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.Formula.Functions;


namespace ALOG.Repositorios;

public class ReporteadorLiberacionRepositorio : GenericoRepositorio<OrdenSalida>, IReporteadorLiberacionRepositorio
{

    private readonly ApplicationDbContext _db;

    public ReporteadorLiberacionRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<DocumentoBase> ObtieneReporteTarjaSalidaLiberacion(ConsultaLiberacionReporteador datosReporteador)
    {
        var resultDocumentoBase = new ResultBase<DocumentoBase>();

        try
        {

            var npoiExcelExportarDatos = new NpoiExcelExportarDatos();

            string pathSource = datosReporteador.ArchivoBase.RutaBase;

            npoiExcelExportarDatos.AbrirLibroPlantilla(pathSource);

            npoiExcelExportarDatos.SelecionaHoja(1);

            var _dataReporte = (from tOS in _db.OrdenSalidas
                                    join tLib in _db.Liberaciones on tOS.Id equals tLib.IdOrdenSalida
                                    join tClieAux in _db.catClientes on tLib.IdCliente equals tClieAux.IdCatCliente into tClieTemp
                                    from tClie in tClieTemp.DefaultIfEmpty()
                                    join tOSIAux in _db.OrdenSalidaInventarios on tOS.Id equals tOSIAux.IdOrdenSalida into tOSITemp
                                    from tOSI in tOSITemp.DefaultIfEmpty()
                                    join tLibInvAux in _db.LiberacionInventarios on tOSI.Id equals tLibInvAux.IdOrdenSalidaInventario into tLibInvTemp
                                    from tLibInv in tLibInvTemp.DefaultIfEmpty()
                                    join tTSAux in _db.Tarjas on tLibInv.IdTarja equals tTSAux.Id into tTSTemp 
                                    from tTS in tTSTemp.DefaultIfEmpty()
                                    join tSalidaCtrlTransAux in _db.SalidaControlTransportes on tOS.Id equals tSalidaCtrlTransAux.IdOrdenSalida into tSalidaCtrlTransTemp
                                    from tSalidaCtrlTrans in tSalidaCtrlTransTemp.DefaultIfEmpty()
                                    join tCtrlTransAux in _db.ControlTransportes on tSalidaCtrlTrans.IdControlTransporte equals tCtrlTransAux.Id into tCtrlTransTemp
                                    from tCtrlTrans in tCtrlTransTemp.DefaultIfEmpty()
                                    join tCatTrasnporteAux in _db.catTransportistas on tCtrlTrans.IdCatTransportista equals tCatTrasnporteAux.IdCatTransportista into tCatTrasnporteTemp
                                    from tCatTransporte in tCatTrasnporteTemp.DefaultIfEmpty()
                                    join tPartida in _db.Partidas on tOSI.IdInventario equals tPartida.IdInventario
                                    group tPartida by new
                                    {
                                        tOS.Id,
                                        tOS.FechaAlta,
                                        tOS.Folio,
                                        tOS.Estado,
                                        FechaLiberacion = tOS.FechaLiberacion != null ? tOS.FechaLiberacion : DateTime.MinValue,
                                        FechaSalidaProgramada = tOS.FechaSalidaProgramada != null ? tOS.FechaSalidaProgramada : DateTime.MinValue,
                                        IdCliente = tClie.IdCatCliente,
                                        Cliente = tClie.RazonSocial,
                                        IdTarjaSalida = tTS.Id != null ? tTS.Id : 0,
                                        FolioTarja = tTS.Folio != null ? tTS.Folio : 0,
                                        FolioReferenciaTarja = tTS.ClaveReferencia != null ? tTS.ClaveReferencia : String.Empty,
                                        FolioViaje = tCtrlTrans.Folio != null ? tCtrlTrans.Folio : 0,
                                        ClaveViaje = tCtrlTrans.Viajes != null ? tCtrlTrans.Viajes : String.Empty,
                                        Operador = String.IsNullOrEmpty(tCtrlTrans.NombreOperador) ? String.Empty : tCtrlTrans.NombreOperador,
                                        Placas = String.IsNullOrEmpty(tCtrlTrans.Placas) ? String.Empty : tCtrlTrans.Placas,
                                        NumEco = String.IsNullOrEmpty(tCtrlTrans.NumeroEconomico) ? String.Empty : tCtrlTrans.NumeroEconomico,
                                        Trasnporte = String.IsNullOrEmpty(tCatTransporte.RazonSocial) ? String.Empty : tCatTransporte.RazonSocial
                                     } into grLiberacion
                where
                    grLiberacion.Key.Id.Equals(datosReporteador.IdOrdenSalida)
                select new 
                {
                    IdLiberacion = grLiberacion.Key.Id,
                    FolioLiberacion = grLiberacion.Key.Folio,
                    EstadoLiberacion = grLiberacion.Key.Estado,
                    IdCatCliente = grLiberacion.Key.IdCliente,
                    Cliente = grLiberacion.Key.Cliente,
                    IdTarja = grLiberacion.Key.IdTarjaSalida,
                    FolioTarja = grLiberacion.Key.Folio,
                    ClaveReferenciaTarja = grLiberacion.Key.FolioReferenciaTarja,
                    FolioViaje = grLiberacion.Key.FolioViaje,
                    ClaveViaje = grLiberacion.Key.ClaveViaje,
                    grLiberacion.Key.Operador,
                    grLiberacion.Key.Placas,
                    grLiberacion.Key.NumEco,
                    grLiberacion.Key.Trasnporte,
                    FechaAlta = (DateTime)grLiberacion.Key.FechaAlta,
                    FechaLiberacion = (DateTime)grLiberacion.Key.FechaLiberacion,
                    FechaSalidaProgramada = (DateTime)grLiberacion.Key.FechaSalidaProgramada,
                    ModeloPartida = grLiberacion.Select(st=>st.Modelo).Distinct(),
                    MarcasPartida = grLiberacion.Select(st=>st.Marcas).Distinct(),
                    Cantidad = grLiberacion.Key.Estado.Equals(EstadoLiberacion.Cancelada) ? 0 : grLiberacion.Count(),
                }).ToList().Select(x => new MonitorLiberacionInventario
                {
                    IdLiberacion = x.IdLiberacion,
                    FolioLiberacion = x.FolioLiberacion,
                    EstadoLiberacion = x.EstadoLiberacion,
                    IdCatCliente = x.IdCatCliente,
                    Cliente = x.Cliente,
                    IdTarja = x.IdTarja,
                    FolioTarja = x.FolioTarja,
                    ClaveReferenciaTarja = x.ClaveReferenciaTarja,
                    FolioViaje = x.FolioViaje,
                    ClaveViaje = x.ClaveViaje,
                    OperadorTransporte = x.Operador,
                    Placas = x.Placas,
                    NumeroEconomico = x.NumEco,
                    LineaTransportista = x.Trasnporte,
                    FechaAlta = x.FechaAlta,
                    FechaLiberacion = x.FechaLiberacion,
                    FechaSalidaProgramada = x.FechaSalidaProgramada,
                    Modelo = string.Join(", ", x.ModeloPartida.ToArray()),
                    Marcas = string.Join(", ", x.MarcasPartida.ToArray()),
                    Cantidad = x.Cantidad,
                }).FirstOrDefault();

            var _dataReporteDetalle = (from p in _db.Partidas
                join e in _db.Inventarios on p.IdInventario equals e.Id 
                join d in _db.Ubicaciones on e.IdUbicacion equals d.Id into f
                from x in f.DefaultIfEmpty()
                join tOSI in _db.OrdenSalidaInventarios on p.IdInventario equals tOSI.IdInventario
                group p by new {
                    p.IdInventario,
                    tOSI.IdOrdenSalida,
                    p.Numeros
                } into grPartidas
                where  grPartidas.Key.IdOrdenSalida.Equals(datosReporteador.IdOrdenSalida)
                select new ReporteDetalleTarjaLiberacion {
                    NumeroRegistro = 0,
                    Numeros = grPartidas.Key.Numeros,
                }).ToList();

            _dataReporteDetalle.Select((p, index) => { p.NumeroRegistro = index + 1; return p; }).ToList();

            var fontEncabezado = npoiExcelExportarDatos.CrearFuenteBold();
            var fontClaveViaje = npoiExcelExportarDatos.CrearFuenteBold();
            var fontDetalle = npoiExcelExportarDatos.CrearFuenteBold();
            fontEncabezado.FontHeightInPoints = 11;
            fontClaveViaje.FontHeightInPoints = 18;
            fontDetalle.FontHeightInPoints = 11;
            //fontDetalle.IsBold = false;

            //TODO: Se registran los datos del encabezado del reporte.

            string _vafechaSalidaProgramada = _dataReporte.FechaSalidaProgramada.Equals(DateTime.MinValue) ? String.Empty : _dataReporte.FechaSalidaProgramada.ToString();
            string _valModelo = "MODELO:" + (String.IsNullOrEmpty(_dataReporte.Modelo) ? String.Empty : " " + _dataReporte.Modelo);
            string _valMarcas = "MARCA:" + (String.IsNullOrEmpty(_dataReporte.Marcas) ? String.Empty : " " + _dataReporte.Marcas);
            string _valOperador = "OPERADOR:" + (String.IsNullOrEmpty(_dataReporte.OperadorTransporte) ? String.Empty : " " + _dataReporte.OperadorTransporte);
            string _valNumEco = "ECO:" + (String.IsNullOrEmpty(_dataReporte.NumeroEconomico) ? String.Empty : " " + _dataReporte.NumeroEconomico);
            string _valPlaca = "PLACA:" + (String.IsNullOrEmpty(_dataReporte.Placas) ? String.Empty : " " + _dataReporte.Placas);
            string _valTransporte = "TRANSPORTE:" + (String.IsNullOrEmpty(_dataReporte.LineaTransportista) ? String.Empty : " " + _dataReporte.LineaTransportista);

            npoiExcelExportarDatos.AsignaValorEnCelda(3, 0, TipoDatoExcel.String, _dataReporte.ClaveReferenciaTarja, fontEncabezado, false, true, false, true);
            npoiExcelExportarDatos.AsignaValorEnCelda(7, 0, TipoDatoExcel.String, _dataReporte.ClaveViaje, fontClaveViaje, false, true);
            npoiExcelExportarDatos.AsignaValorEnCelda(4, 3, TipoDatoExcel.String, _vafechaSalidaProgramada, fontEncabezado, false, true);
            npoiExcelExportarDatos.AsignaValorEnCelda(18, 3, TipoDatoExcel.String, _dataReporte.FechaAlta.ToString(), fontEncabezado, false, true);
            npoiExcelExportarDatos.AsignaValorEnCelda(8, 0, TipoDatoExcel.String, _valMarcas, fontEncabezado, true, false);
            npoiExcelExportarDatos.AsignaValorEnCelda(9, 0, TipoDatoExcel.String, _valModelo, fontEncabezado, true, false);
            npoiExcelExportarDatos.AsignaValorEnCelda(12, 0, TipoDatoExcel.String, _valOperador, fontEncabezado, true, false);
            npoiExcelExportarDatos.AsignaValorEnCelda(14, 0, TipoDatoExcel.String, _valNumEco, fontEncabezado, true, false);
            npoiExcelExportarDatos.AsignaValorEnCelda(15, 0, TipoDatoExcel.String, _valPlaca, fontEncabezado, true, false);
            npoiExcelExportarDatos.AsignaValorEnCelda(16, 0, TipoDatoExcel.String, _valTransporte, fontEncabezado, true, false);

            // npoiExcelExportarDatos.AsignBorderEnCelda(8, 0);
            // npoiExcelExportarDatos.AsignBorderEnCelda(9, 0);
            // npoiExcelExportarDatos.AsignBorderEnCelda(12, 0);
            // npoiExcelExportarDatos.AsignBorderEnCelda(14, 0);
            // npoiExcelExportarDatos.AsignBorderEnCelda(15, 0);
            // npoiExcelExportarDatos.AsignBorderEnCelda(16, 0);

            int _pivoteColumna = 2;
            int _pivoteFila = 8;
            
            foreach(var _itemDetalleReporte in _dataReporteDetalle) {
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna, TipoDatoExcel.Number, _itemDetalleReporte.NumeroRegistro, fontDetalle, false,  true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 1, TipoDatoExcel.String, _itemDetalleReporte.Numeros, fontDetalle, false,  true);
                _pivoteFila++;
            }

            string imagenBase64 = new GeneraCodigoBarra().CodigoQRImageBase64(contenidoCodigo: _dataReporte.ClaveReferenciaTarja, pixeles: 240);

            byte[] imagenByte = System.Convert.FromBase64String(imagenBase64);

            npoiExcelExportarDatos.AgregarImagen(imagenByte, 0, 8, NPOI.SS.UserModel.PictureType.PNG);


            resultDocumentoBase.Data = npoiExcelExportarDatos.LibroExcelToDocumentoBase();

            resultDocumentoBase.Data.Archivo = $"REPORTE_TARJASALIDA_LIBERACION_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

        }
        catch (Exception ex)
        {

            resultDocumentoBase.MensajeRespuesta = ex.Message;
        }

        return resultDocumentoBase;
    }
}
