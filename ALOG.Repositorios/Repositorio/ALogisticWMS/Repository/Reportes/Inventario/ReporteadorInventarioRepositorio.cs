using System.Data;
using System.Text;
using System.Transactions;
using ALOG.Enums;
using ALOG.InfraestructuraReportes;
using ALOG.Modelos;
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

public class ReporteadorInventarioRepositorio : GenericoRepositorio<Inventario>, IReporteadorInventarioRepositorio
{

    private readonly ApplicationDbContext _db;

    public ReporteadorInventarioRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase<DocumentoBase> ObtieneReporteDescargaInventario(ConsultaMonitorInventario datosReporteador)
    {

        var resultDocumentoBase = new ResultBase<DocumentoBase>();

        try
        {

            int _totalRegistros = 0;

            // List<MonitorInventarioAlmacen> _listMonitorInventario = (from p in _db.InventarioALOs
            //                       join part in _db.Partidas on p.IdInventario equals part.IdInventario
            //                       join invt in _db.Inventarios on p.IdInventario equals invt.Id
            //                       join refncia in _db.Referencias on p.IdReferencia equals refncia.Id
            //                       join catClie in _db.catClientes on refncia.IdCliente equals catClie.IdCatCliente
            //                       join refBookingAux in _db.ReferenciaBookingBls on p.IdReferencia equals refBookingAux.IdReferencia into refBooking
            //                       from xRefBooking in refBooking.DefaultIfEmpty()
            //                       join refViajeAux in _db.Viajes on refncia.IdViaje equals refViajeAux.Id into refViaje
            //                       from xRefViaje in refViaje.DefaultIfEmpty()
            //                       group xRefBooking by new
            //                       {
            //                           part.IdInventario,
            //                           IdReferencia = refncia.Id,
            //                           refncia.IdCliente,
            //                           refncia.IdOrdenServicio,
            //                           part.Numeros,
            //                           part.Marcas,
            //                           part.Modelo,
            //                           xRefBooking.BookingBl,
            //                           FolioViaje = xRefViaje.Folio,
            //                           refncia.TipoOperacion,
            //                           RazonSocialCliente = catClie.RazonSocial,
            //                           invt.FechaRecoleccion,
            //                           invt.FechaIngreso,
            //                       } into grInventario
            //                       where
            //                             //entidad.Entidad.ListaIdCatCliente.Contains((int)refncia.IdCliente)
            //                             //&& 
            //                             grInventario.Key.BookingBl.Contains(datosReporteador.BuscarPorConsultaInventario.Equals(BuscarPorConsultaInventario.BookingBL) ? datosReporteador.ValorBusqueda : grInventario.Key.BookingBl)
            //                             && grInventario.Key.FolioViaje.Contains(datosReporteador.BuscarPorConsultaInventario.Equals(BuscarPorConsultaInventario.Viaje) ? datosReporteador.ValorBusqueda : grInventario.Key.FolioViaje)
            //                             && (datosReporteador.TipoFecha.Equals(1) ? grInventario.Key.FechaRecoleccion : grInventario.Key.FechaIngreso) >= datosReporteador.FechaInicio.Date.Add(DateTime.MinValue.TimeOfDay) &&
            //                                 (datosReporteador.TipoFecha.Equals(1) ? grInventario.Key.FechaRecoleccion : grInventario.Key.FechaIngreso) <= datosReporteador.FechaFin.Date.Add(DateTime.MaxValue.TimeOfDay)
            //                       select new 
            //                       {
            //                           IdInventario = (int)(grInventario.Key.IdInventario != null ? grInventario.Key.IdInventario : 0),
            //                           grInventario.Key.IdReferencia,
            //                           IdOrdenServicio = (int)(grInventario.Key.IdOrdenServicio != null ? grInventario.Key.IdOrdenServicio : 0),
            //                           TipoOperacionAduanera = grInventario.Key.TipoOperacion,
            //                           IdCatCliente = (int)(grInventario.Key.IdCliente != null ? grInventario.Key.IdCliente : 0),
            //                           grInventario.Key.RazonSocialCliente,
            //                           FechaIngreso = (DateTime)(grInventario.Key.FechaIngreso != null ? grInventario.Key.FechaIngreso : DateTime.MinValue),
            //                           FechaRecoleccion = (DateTime)(grInventario.Key.FechaRecoleccion != null ? grInventario.Key.FechaRecoleccion : DateTime.MinValue),
            //                           grInventario.Key.Marcas,
            //                           grInventario.Key.Modelo,
            //                           grInventario.Key.Numeros,
            //                           BookingBl = grInventario.Select(st=>st.BookingBl).Distinct(),

            //                       }).ToList().Select(x => new MonitorInventarioAlmacen
            //                       {
            //                           IdInventario = x.IdInventario,
            //                           IdReferencia = x.IdReferencia,
            //                           IdOrdenServicio = x.IdOrdenServicio,
            //                           TipoOperacionAduanera = x.TipoOperacionAduanera,
            //                           IdCatCliente = x.IdCatCliente,
            //                           RazonSocialCliente = x.RazonSocialCliente,
            //                           FechaIngreso = x.FechaIngreso,
            //                           FechaRecoleccion = x.FechaRecoleccion,
            //                           Marcas = x.Marcas,
            //                           Modelo = x.Modelo,
            //                           Numeros = x.Numeros,
            //                           BookingBl = string.Join(", ", x.BookingBl.ToArray()),
            //                       }).ToList();

            var _listMonitorInventario = (from p in _db.InventarioALOs
                                          join part in _db.Partidas on p.IdInventario equals part.IdInventario
                                          join invt in _db.Inventarios on p.IdInventario equals invt.Id
                                          join refncia in _db.Referencias on p.IdReferencia equals refncia.Id
                                          join catClie in _db.catClientes on refncia.IdCliente equals catClie.IdCatCliente
                                          join refBookingAux in _db.ReferenciaBookingBls on p.IdReferencia equals refBookingAux.IdReferencia into refBooking
                                          from xRefBooking in refBooking.DefaultIfEmpty()
                                          join refViajeAux in _db.Viajes on refncia.IdViaje equals refViajeAux.Id into refViaje
                                          from xRefViaje in refViaje.DefaultIfEmpty()
                                          join tBarcoAux in _db.Barcos on xRefViaje.IdBarco equals tBarcoAux.Id into tBarcoTemp
                                          from tBarco in tBarcoTemp.DefaultIfEmpty()
                                          where
                                              //entidad.Entidad.ListaIdCatCliente.Contains((int)refncia.IdCliente)
                                              //&& 
                                              xRefBooking.BookingBl.Contains(datosReporteador.BuscarPorConsultaInventario.Equals(BuscarPorConsultaInventario.BookingBL) ? datosReporteador.ValorBusqueda : xRefBooking.BookingBl)
                                              && xRefViaje.Folio.Contains(datosReporteador.BuscarPorConsultaInventario.Equals(BuscarPorConsultaInventario.Viaje) ? datosReporteador.ValorBusqueda : xRefViaje.Folio)
                                              && (datosReporteador.TipoFecha.Equals(1) ? invt.FechaRecoleccion : invt.FechaIngreso) >= datosReporteador.FechaInicio.Date.Add(DateTime.MinValue.TimeOfDay) &&
                                                  (datosReporteador.TipoFecha.Equals(1) ? invt.FechaRecoleccion : invt.FechaIngreso) <= datosReporteador.FechaFin.Date.Add(DateTime.MaxValue.TimeOfDay)
                                          select new MonitorInventarioAlmacen
                                          {
                                              IdInventario = (int)(p.IdInventario != null ? p.IdInventario : 0),
                                              IdReferencia = refncia.Id,
                                              IdOrdenServicio = (int)(refncia.IdOrdenServicio != null ? refncia.IdOrdenServicio : 0),
                                              TipoOperacionAduanera = refncia.TipoOperacion,
                                              IdCatCliente = (int)(refncia.IdCliente != null ? refncia.IdCliente : 0),
                                              RazonSocialCliente = catClie.RazonSocial,
                                              FolioViaje = xRefViaje.Folio,
                                              NombreBuque = tBarco.Nombre,
                                              FechaIngreso = (DateTime)(invt.FechaIngreso != null ? invt.FechaIngreso : DateTime.MinValue),
                                              FechaRecoleccion = (DateTime)(invt.FechaRecoleccion != null ? invt.FechaRecoleccion : DateTime.MinValue),
                                              Marcas = part.Marcas,
                                              Numeros = part.Numeros,
                                          }).Distinct().ToList();

            _totalRegistros = _listMonitorInventario.Count;

            _listMonitorInventario.Select(p => { p.BookingBl = ObtenerBookingBlByIdReferencia(p.IdReferencia); return p; }).ToList();

            var npoiExcelExportarDatos = new NpoiExcelExportarDatos();

            string pathSource = datosReporteador.ArchivoBase.RutaBase;

            npoiExcelExportarDatos.AbrirLibroPlantilla(pathSource);

            npoiExcelExportarDatos.SelecionaHoja(0);

            var fontEncabezado = npoiExcelExportarDatos.CrearFuenteBold();
            var fontClaveViaje = npoiExcelExportarDatos.CrearFuenteBold();
            var fontDetalle = npoiExcelExportarDatos.CrearFuenteBold();
            fontEncabezado.FontHeightInPoints = 11;
            fontClaveViaje.FontHeightInPoints = 18;
            fontDetalle.FontHeightInPoints = 11;

            int _pivoteColumna = 0;
            int _pivoteFila = 1;

            foreach (var _itemDetalleReporte in _listMonitorInventario)
            {

                string _tipoOperacionAduanera = _itemDetalleReporte.TipoOperacionAduanera.Equals(TipoOperacionAduanera.Importacion) ? "I" : "E";

                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna, TipoDatoExcel.Number, _pivoteFila, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 1, TipoDatoExcel.String, _itemDetalleReporte.Numeros, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 2, TipoDatoExcel.String, _itemDetalleReporte.Modelo, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 3, TipoDatoExcel.String, _itemDetalleReporte.Marcas, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 4, TipoDatoExcel.String, _itemDetalleReporte.RazonSocialCliente, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 5, TipoDatoExcel.Decimal, _itemDetalleReporte.Peso, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 6, TipoDatoExcel.String, _tipoOperacionAduanera, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 11, TipoDatoExcel.String, _itemDetalleReporte.NombreBuque, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 12, TipoDatoExcel.String, _itemDetalleReporte.FolioViaje, fontDetalle, false, true);
                npoiExcelExportarDatos.AsignaValorEnCelda(_pivoteFila, _pivoteColumna + 14, TipoDatoExcel.String, _itemDetalleReporte.BookingBl, fontDetalle, false, true);
                _pivoteFila++;
            }

            resultDocumentoBase.Data = npoiExcelExportarDatos.LibroExcelToDocumentoBase();

            resultDocumentoBase.Data.Archivo = $"REPORTE_INVENTARIO_TERMINAL_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

        }
        catch (Exception ex)
        {

            resultDocumentoBase.MensajeRespuesta = ex.Message;
        }

        return resultDocumentoBase;
    }

    private string ObtenerBookingBlByIdReferencia(int IdReferencia)
    {

        string _bookingBL = string.Empty;

        var _bookingBLAux = (from tRefBL in _db.ReferenciaBookingBls
                      group tRefBL by new
                      {
                          tRefBL.IdReferencia
                      } into grRefBL
                      where grRefBL.Key.IdReferencia.Equals(IdReferencia)
                      select new
                      {
                          BookingBl = grRefBL.Select(st => st.BookingBl).Distinct(),
                      }
                        ).ToList().Select(x => new
                        {
                            BookingBl = string.Join(", ", x.BookingBl.ToArray()),
                        }).ToList();

        if (_bookingBLAux.Count() > 0)
        {
            _bookingBL = _bookingBLAux[0].BookingBl;
        }

        return _bookingBL;

    }
}
