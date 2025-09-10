using ALOG.Enums;
using ALOG.InfraestructuraReportes;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using System.Data;


namespace ALOG.Repositorios;

public class ReporteadorTarjaRepositorio : GenericoRepositorio<Tarja>, IReporteadorTarjaRepositorio
{

    private readonly ApplicationDbContext _db;

    public ReporteadorTarjaRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<DocumentoBase> ObtieneReporteTarjaPatioExterno(ConsultaTarjaReporteador datosReporteador)
    {
        var resultDocumentoBase = new ResultBase<DocumentoBase>();

        try
        {

            var npoiExcelExportarDatos = new NpoiExcelExportarDatos();

            string pathSource = datosReporteador.ArchivoBase.RutaBase;

            npoiExcelExportarDatos.AbrirLibroPlantilla(pathSource);

            npoiExcelExportarDatos.SelecionaHoja(1);

            var _dataReporte = (from p in _db.Tarjas
                join e in _db.Referencias on p.IdReferencia equals e.Id 
                join d in _db.catClientes on e.IdCliente equals d.IdCatCliente into _xcliente
                from _cliente in _xcliente.DefaultIfEmpty()
                join f in _db.Viajes on e.IdViaje equals f.Id into _xviaje
                from _viaje in _xviaje.DefaultIfEmpty()
                join g in _db.Barcos on _viaje.IdBarco equals g.Id into _xbarco
                from _barco in _xbarco.DefaultIfEmpty()
                join _folioservAux in _db.FolioServicios on p.Id equals _folioservAux.IdTarja into _folioservTemp
                from _folioserv in _folioservTemp.DefaultIfEmpty()
                join _soltransAux in _db.SolicitudTraslados on _folioserv.Id equals _soltransAux.IdFolioServicio into _soltransTemp
                from _soltrans in _soltransTemp.DefaultIfEmpty()
                join _ctrltransAux in _db.ControlTransportes on _soltrans.IdControlTransporte equals _ctrltransAux.Id into _ctrltransTemp
                from _ctrltrans in _ctrltransTemp.DefaultIfEmpty()
                join _lineatransAux in _db.LineaTransportes on _ctrltrans.IdLineaTransTransporte equals _lineatransAux.Id into _lineatransTemp
                from _lineatrans in _lineatransTemp.DefaultIfEmpty()
                join _cattransAux in _db.catTransportistas on _lineatrans.IdCatTransportista equals _cattransAux.IdCatTransportista into _cattransTemp
                from _cattrans in _cattransTemp.DefaultIfEmpty()
                join _operatransAux in _db.LineaOperadores on _ctrltrans.IdLineaTransOperador equals _operatransAux.Id into _operatransTemp
                from _operatrans in _operatransTemp.DefaultIfEmpty()
                join _manidestinoAux in _db.Maniobristas on _soltrans.IdManiobristaDestino equals _manidestinoAux.Id into _manidestinoTemp
                from _manidestino in _manidestinoTemp.DefaultIfEmpty() 
                where p.Id == datosReporteador.IdTarja
                select new ReporteTarjaRecepcion {
                    FolioTarja = p.ClaveReferencia,
                    FolioViaje = String.IsNullOrEmpty(_viaje.Folio) ? String.Empty : _viaje.Folio,
                    Buque = String.IsNullOrEmpty(_barco.Nombre) ? String.Empty : _barco.Nombre,
                    NombreCliente = String.IsNullOrEmpty(_cliente.RazonSocial) ? String.Empty : _cliente.RazonSocial,
                    Fecha = DateTime.Now.ToShortDateString(),
                    PatioRecepcion = _manidestino.NombreCorto,
                    ListaReporteDetalleTarjaRecepcion = new List<ReporteDetalleTarjaRecepcion>(),
                }).FirstOrDefault();

            var _dataReporteDetalle = (from p in _db.Partidas
                                       join e in _db.Inventarios on p.IdInventario equals e.Id
                                       join d in _db.Ubicaciones on e.IdUbicacion equals d.Id into f
                                       from x in f.DefaultIfEmpty()
                                       where p.IdTarja == datosReporteador.IdTarja
                                       select new ReporteDetalleTarjaRecepcion
                                       {
                                           NumeroRegistro = 0,
                                           VIN = p.Numeros,
                                           Modelo = p.Modelo
                                       }).ToList();

            _dataReporteDetalle.Select((p, index) => { p.NumeroRegistro = index + 1; return p; }).ToList();

            var fontEncabezado = npoiExcelExportarDatos.CrearFuenteBold();
            var fontDetalle = npoiExcelExportarDatos.CrearFuenteBold();
            fontEncabezado.FontHeightInPoints = 11;
            fontDetalle.FontHeightInPoints = 11;
            fontDetalle.IsBold = false;

            //TODO: Se registran los datos del encabezado del reporte.

            npoiExcelExportarDatos.AsignaValorEnCelda(3, 2, TipoDatoExcel.String, _dataReporte.FolioTarja, fontDetalle);
            npoiExcelExportarDatos.AsignaValorEnCelda(4, 2, TipoDatoExcel.String, _dataReporte.NombreCliente, fontDetalle);
            npoiExcelExportarDatos.AsignaValorEnCelda(5, 2, TipoDatoExcel.String, _dataReporte.Buque, fontDetalle);
            npoiExcelExportarDatos.AsignaValorEnCelda(0, 8, TipoDatoExcel.String, "PATIO RECEPCION: " + _dataReporte.PatioRecepcion, fontDetalle);
            npoiExcelExportarDatos.AsignaValorEnCelda(2, 8, TipoDatoExcel.String, "FECHA: " + _dataReporte.Fecha, fontDetalle, false, true);

            int _pivoteColumna = 0;
            int _pivoteFila = 12;

            foreach (var _itemDetalleReporte in _dataReporteDetalle)
            {
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna, TipoDatoExcel.Number, _itemDetalleReporte.NumeroRegistro, fontDetalle);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 1, TipoDatoExcel.String, _dataReporte.FolioViaje, fontDetalle);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 2, TipoDatoExcel.String, _itemDetalleReporte.VIN, fontDetalle);

                _pivoteFila++;
            }

            string imagenBase64 = new GeneraCodigoBarra().CodigoQRImageBase64(contenidoCodigo: _dataReporte.FolioTarja, pixeles: 310);

            byte[] imagenByte = System.Convert.FromBase64String(imagenBase64);

            npoiExcelExportarDatos.AgregarImagen(imagenByte, 1, 10, NPOI.SS.UserModel.PictureType.PNG);

            resultDocumentoBase.Data = npoiExcelExportarDatos.LibroExcelToDocumentoBase();

            resultDocumentoBase.Data.Archivo = $"REPORTE_RECEPCION_TARJA_A_PATIO_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

        }
        catch (Exception ex)
        {

            resultDocumentoBase.MensajeRespuesta = ex.Message;
        }

        return resultDocumentoBase;
    }

}
