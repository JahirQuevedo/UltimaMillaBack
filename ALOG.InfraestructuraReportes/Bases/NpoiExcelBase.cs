using ALOG.Enums;
using ALOG.Modelos;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace ALOG.InfraestructuraReportes;

public abstract class NpoiExcelBase
{

    protected string nombreHoja;
    protected IList encabezados;
    protected IList tipos;
    private const string DefaultNombreHoja = "Hoja1";
    private bool esLibroCreadoOrAbierto;
    protected IWorkbook LibroExcel { get; set; }
    protected HSSFWorkbook WorkBookExcel { get; set; }

    protected ISheet HojaActiva { get; set; }

    public virtual bool AbrirLibroPlantilla(string plantilla)
    {
        esLibroCreadoOrAbierto = false;

        try
        {
            FileStream file = new FileStream(plantilla, FileMode.Open, FileAccess.ReadWrite);

            LibroExcel = new XSSFWorkbook(file);

            return esLibroCreadoOrAbierto = true;

        }
        catch
        {
            throw;
        }
    }

    public virtual bool SelecionaHoja(int numeroDeHoja)
    {
        try
        {
            if (esLibroCreadoOrAbierto)
            {
                HojaActiva = LibroExcel?.GetSheetAt(numeroDeHoja);

                if (HojaActiva != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        catch
        {
            throw;
        }

    }

    public virtual bool CrearLibro()
    {
        try
        {
            LibroExcel = new XSSFWorkbook();

            return esLibroCreadoOrAbierto = true;
        }
        catch
        {
            throw;
        }
    }

    public virtual bool CrearHoja(string nombreHoja)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(nombreHoja))
            {
                throw new ArgumentNullException(nameof(nombreHoja), "Nombre de hoja de excel no asignado.");
            }

            if (esLibroCreadoOrAbierto)
            {
                HojaActiva = LibroExcel?.CreateSheet(nombreHoja);

                this.nombreHoja = nombreHoja;

                if (HojaActiva != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                CrearLibro();

                if (esLibroCreadoOrAbierto)
                {
                    CrearHoja(nombreHoja);
                }
            }

            return false;
        }
        catch
        {
            throw;
        }
    }

    public virtual IFont CrearFuente()
    {
        try
        {
            ValidarLibroHojaActiva();

            return LibroExcel.CreateFont();
        }
        catch
        {
            throw;
        }
    }

    public virtual XSSFFont CrearFuenteBold()
    {
        try
        {
            ValidarLibroHojaActiva();

            var fontDefault = (XSSFFont)LibroExcel.CreateFont();

            fontDefault.FontHeightInPoints = 10;
            fontDefault.FontName = "Arial";
            fontDefault.Color = IndexedColors.Black.Index;
            fontDefault.IsBold = true;
            fontDefault.IsItalic = false;


            return fontDefault;
        }
        catch
        {
            throw;
        }
    }

    public virtual XSSFFont CrearFuenteNormal()
    {
        try
        {
            ValidarLibroHojaActiva();

            var fontDefault = (XSSFFont)LibroExcel.CreateFont();

            fontDefault.FontHeightInPoints = 10;
            fontDefault.FontName = "Arial";
            fontDefault.Color = IndexedColors.Black.Index;
            fontDefault.IsBold = false;
            fontDefault.IsItalic = false;


            return fontDefault;
        }
        catch
        {
            throw;
        }
    }

    public virtual void CombinarCelda(int firstRow, int lastRow, int firstCol, int lastCol)
    {
        try
        {
            ValidarLibroHojaActiva();

            HojaActiva.AddMergedRegion(new CellRangeAddress(firstRow, lastRow, firstCol, lastCol));
        }
        catch
        {
            throw;
        }

    }

    // ------------

    public virtual DocumentoBase ListToDocumentoBase<T>(IList<T> listaDatos, string nombreHoja = DefaultNombreHoja, int fila = 0, int columna = 0)
    {
        try
        {
            CrearHoja(nombreHoja);

            AsignarDatos(fila, columna, listaDatos, agregarEncabezados: true);

            return LibroExcelToDocumentoBase();

        }
        catch
        {
            throw;
        }
    }

    public virtual DocumentoBase DataSetToDocumentoBase(DataSet dataSet)
    {
        try
        {

            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                string nombreHoja = string.IsNullOrWhiteSpace(dataSet.Tables?[i].TableName) ? DefaultNombreHoja : dataSet.Tables?[i].TableName.Trim();

                CrearHoja(nombreHoja);

                AsignarDatos(fila: 0, columna: 0, dataTable: dataSet.Tables[i], agregarEncabezados: true);

            }

            return LibroExcelToDocumentoBase();
        }
        catch
        {
            throw;
        }
    }

    public virtual DocumentoBase DataTableToDocumentoBase(DataTable dataTable, string nombreHoja = null, int fila = 0, int columna = 0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(nombreHoja))
            {
                nombreHoja = string.IsNullOrWhiteSpace(dataTable?.TableName) ? DefaultNombreHoja : dataTable.TableName.Trim();
            }

            CrearHoja(nombreHoja);

            AsignarDatos(fila, columna, dataTable, agregarEncabezados: true);


            return LibroExcelToDocumentoBase();


        }
        catch
        {
            throw;
        }
    }



    public virtual DocumentoBase LibroExcelToDocumentoBase()
    {
        try
        {
            ValidarLibroHojaActiva();

            var documento = new DocumentoBase
            {
                Content = LibroExcelToArray(),

                ContentType = "application/vnd.ms-excel"
            };

            return documento;
        }
        catch
        {
            throw;
        }

    }

    // ------------


    private byte[] LibroExcelToArray(IWorkbook wb)
    {
        try
        {
            if (wb == null)
            {
                throw new ArgumentNullException(nameof(wb), "El libro de excel no contiene información");
            }

            byte[] byteArchivo = null;

            using (var memoryStream = new MemoryStream())
            {
                wb.Write(memoryStream);

                byteArchivo = memoryStream.ToArray();
            }

            return byteArchivo;
        }
        catch
        {
            throw;
        }
    }

    public virtual void AsignaValorEnCelda(int fila, int columna, TipoDatoExcel tipoDatoExcel, object value)
    {
        AsignaValorEnCelda(fila, columna, tipoDatoExcel, value, null);
    }

    public virtual void AsignaValorEnCelda(int fila, int columna, TipoDatoExcel tipoDatoExcel, object value, IFont font = null, bool autoSize = false, bool Center = false, bool Color = false, bool Bordes = false)
    {
        try
        {
            ValidarLibroHojaActiva();

            IRow row = HojaActiva.GetRow(fila);

            if (row == null)
            {
                row = HojaActiva.CreateRow(fila);
            }


            ICell cell = row.CreateCell(columna);

            #region Formato de celda

            if (font != null)
            {
                ICellStyle estilo = LibroExcel.CreateCellStyle();
                estilo.SetFont(font);
                if (Center)
                {
                    estilo.Alignment = HorizontalAlignment.Center;
                }
                if (Color)
                {
                    estilo.FillBackgroundColor = IndexedColors.Blue.Index;
                    estilo.FillForegroundColor = IndexedColors.Blue.Index;
                    estilo.FillPattern = FillPattern.SolidForeground;
                }
                if (Bordes)
                {
                    estilo.BorderTop = BorderStyle.Medium;
                    estilo.BorderLeft = BorderStyle.Medium;
                    estilo.BorderRight = BorderStyle.Medium;
                    estilo.BorderBottom = BorderStyle.Medium;
                    estilo.TopBorderColor = IndexedColors.Black.Index;
                    estilo.LeftBorderColor = IndexedColors.Black.Index;
                    estilo.RightBorderColor = IndexedColors.Black.Index;
                    estilo.BottomBorderColor = IndexedColors.Black.Index;

                }
                cell.CellStyle = estilo;
            }

            #endregion Formato de celda

            switch (tipoDatoExcel)
            {
                case TipoDatoExcel.String:
                    cell.SetCellType(CellType.String);

                    if (value != null && value != DBNull.Value)
                    {
                        cell.SetCellValue((string)value);
                    }
                    break;
                case TipoDatoExcel.Decimal:

                    ICellStyle cellStyleDouble = LibroExcel.CreateCellStyle();
                    cellStyleDouble.DataFormat = LibroExcel.CreateDataFormat().GetFormat("0.000");

                    cell.SetCellType(CellType.Numeric);
                    if (value != null && value != DBNull.Value)
                    {
                        cell.SetCellValue(Convert.ToDouble(value));
                        cell.CellStyle = cellStyleDouble;
                    }
                    break;
                case TipoDatoExcel.Number:

                    cell.SetCellType(CellType.Numeric);
                    if (value != null && value != DBNull.Value)
                    {
                        cell.SetCellValue(Convert.ToDouble(value));
                    }
                    break;
                case TipoDatoExcel.DateTime:

                    if (value != null && value != DBNull.Value)
                    {
                        var newDataFormat = LibroExcel.CreateDataFormat();
                        var style = LibroExcel.CreateCellStyle();
                        style.DataFormat = newDataFormat.GetFormat("dd/MM/yyyy");

                        cell.SetCellValue(Convert.ToDateTime(value));
                        cell.CellStyle = style;
                    }
                    break;
                case TipoDatoExcel.Boolean:
                    cell.SetCellType(CellType.Boolean);

                    if (value != null && value != DBNull.Value)
                    {
                        cell.SetCellValue(Convert.ToBoolean(value));
                    }
                    break;
            }

            if (autoSize)
            {
                HojaActiva.AutoSizeColumn(columna);
            }


        }
        catch
        {
            throw;
        }
    }

    public virtual void AsignarDatos(int fila, int columna, DataTable dataTable, bool agregarEncabezados = false)
    {
        try
        {


            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable), "DataTable no se permiten null");
            }

            if (dataTable.Rows == null)
            {
                throw new ArgumentNullException(nameof(dataTable.Rows), "Los datos no se permiten null");
            }

            ValidarLibroHojaActiva();


            if (encabezados == null)
            {
                encabezados = new List<string>();
            }

            int totalDeFilas = dataTable.Rows.Count;
            int totalDeColumnas = dataTable.Columns.Count;

            int filas = fila;
            int columnas = 0;

            if (agregarEncabezados)
            {

                var fontEncabezado = CrearFuenteBold();

                for (int i = 0; i < totalDeColumnas; i++)
                {
                    string nombre = dataTable.Columns[i].ColumnName;

                    AsignaValorEnCelda(0, i, TipoDatoExcel.String, nombre, fontEncabezado);

                    encabezados.Add(nombre);
                }

                if (filas == 0)
                {
                    filas++;
                }
            }

            for (int f = 0; f < totalDeFilas; f++)
            {
                columnas = columna;

                for (int c = 0; c < totalDeColumnas; c++)
                {
                    TipoDatoExcel tipoDatoExcel = GetDataType(dataTable.Rows[f][c].GetType());

                    var value = dataTable.Rows[f][c] ?? DBNull.Value;

                    AsignaValorEnCelda(filas, columnas, tipoDatoExcel, value);
                    columnas++;
                }

                filas++;
            }

            for (var i = 0; i < encabezados.Count; i++)
            {
                HojaActiva.AutoSizeColumn(i);
            }
        }
        catch
        {
            throw;
        }
    }

    public virtual void AutoSizeColumn(int nCountColumn)
    {
        for (var i = 0; i < nCountColumn; i++)
        {
            HojaActiva.AutoSizeColumn(i);
        }
    }

    public virtual void AsignarDatos<T>(int fila, int columna, IList<T> listaDatos, bool agregarEncabezados = false, bool separarCamelCaseEncabezados = true)
    {
        try
        {
            ValidarLibroHojaActiva();

            if (listaDatos == null)
            {
                throw new ArgumentNullException(nameof(listaDatos), "Sin registros para exportar");
            }

            int totalDeFilas = listaDatos.Count;

            int filas = fila;
            int columnas = 0;

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            if (agregarEncabezados)
            {
                var fontEncabezado = CrearFuenteBold();

                foreach (PropertyDescriptor prop in properties)
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    tipos.Add(type.Name);

                    string name = separarCamelCaseEncabezados ? Regex.Replace(prop.Name, "([A-Z])", " $1").Trim() : prop.Name;

                    encabezados.Add(name);
                }

                if (filas == 0)
                {
                    filas++;
                }

            }

            for (int f = 0; f < totalDeFilas; f++)
            {
                foreach (PropertyDescriptor prop in properties)
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    TipoDatoExcel tipoDatoExcel = GetDataType(type);

                    AsignaValorEnCelda(fila: f, columna: columnas, tipoDatoExcel: tipoDatoExcel, value: prop.GetValue(listaDatos[f]) ?? DBNull.Value);

                    columnas++;
                }
            }

            for (var i = 0; i < encabezados.Count; i++)
            {
                HojaActiva.AutoSizeColumn(i);
            }
        }
        catch
        {
            throw;
        }
    }

    public virtual void AgregarImagen(byte[] data, int fila, int columna, PictureType pictureType)
    {

        try
        {


            ValidarLibroHojaActiva();

            IRow row = HojaActiva.GetRow(fila);

            if (row == null)
            {
                row = HojaActiva.CreateRow(fila);
            }


            ICell cell = row.CreateCell(columna);

            int pictureIndex = LibroExcel.AddPicture(data, PictureType.JPEG);
            ICreationHelper helper = LibroExcel.GetCreationHelper();
            IDrawing drawing = HojaActiva.CreateDrawingPatriarch();
            IClientAnchor anchor = helper.CreateClientAnchor();
            anchor.Col1 = columna;
            anchor.Row1 = fila;
            IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
            picture.Resize();

        }
        catch
        {

            throw;

        }


    }

    public virtual HttpResponseMessage ListToHttpResponseMessage<T>(IList<T> listaDatos, string nombreArchivo, string nombreHoja = DefaultNombreHoja, bool separarCamelCaseEncabezados = true, int fila = 0, int columna = 0)
    {

        try
        {
            CrearHoja(nombreHoja);

            AsignarDatos(fila, columna, listaDatos, agregarEncabezados: true);

            return LibroExcelToHttpResponseMessage(nombreArchivo);

        }
        catch
        {
            throw;
        }
    }

    public virtual HttpResponseMessage DataSetToHttpResponseMessage(DataSet dataSet, string nombreArchivo)
    {
        if (dataSet == null)
        {
            throw new ArgumentNullException(nameof(dataSet), "No se permite enviar el dataSet en null");
        }

        if (string.IsNullOrWhiteSpace(nombreArchivo))
        {
            throw new ArgumentException("Falta el nombre del archivo a generar.", nameof(nombreArchivo));
        }

        if (dataSet.Tables == null || dataSet.Tables.Count <= 0)
        {
            throw new ArgumentNullException(nameof(dataSet.Tables), "No se permite enviar el dataSet sin tablas");
        }

        try
        {

            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                string nombreHoja = string.IsNullOrWhiteSpace(dataSet.Tables?[i].TableName) ? DefaultNombreHoja : dataSet.Tables?[i].TableName.Trim();

                CrearHoja(nombreHoja);

                AsignarDatos(fila: 0, columna: 0, dataTable: dataSet.Tables[i], agregarEncabezados: true);

            }

            return LibroExcelToHttpResponseMessage(nombreArchivo);
        }
        catch
        {
            throw;
        }
    }

    public virtual HttpResponseMessage DataTableToHttpResponseMessage(DataTable dataTable, string nombreArchivo, string nombreHoja = DefaultNombreHoja, int fila = 0, int columna = 0)
    {
        try
        {

            if (string.IsNullOrWhiteSpace(nombreHoja))
            {
                nombreHoja = string.IsNullOrWhiteSpace(dataTable?.TableName) ? DefaultNombreHoja : dataTable.TableName.Trim();
            }

            CrearHoja(nombreHoja);

            AsignarDatos(fila, columna, dataTable, agregarEncabezados: true);

            return LibroExcelToHttpResponseMessage(nombreArchivo);

        }
        catch
        {
            throw;
        }
    }

    public virtual HttpResponseMessage LibroExcelToHttpResponseMessage(string nombreArchivo)
    {
        if (string.IsNullOrWhiteSpace(nombreArchivo))
        {
            throw new ArgumentException("Falta el nombre del archivo", nameof(nombreArchivo));
        }

        ValidarLibroHojaActiva();

        try
        {

            using (var memoryStream = new MemoryStream())
            {
                LibroExcel.Write(memoryStream);
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };

                nombreArchivo = nombreArchivo.Replace(".xlsx", "");
                nombreArchivo = nombreArchivo.Replace(".xls", "");

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"{nombreArchivo}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx"
                };

                return response;
            }
        }
        catch
        {
            throw;
        }

    }

    public virtual DataTable GetDataTableFromExcel(string plantilla)
    {
        try
        {
            using (var archivo = new FileStream(plantilla, FileMode.Open, FileAccess.Read))
            {
                LibroExcel = new XSSFWorkbook(archivo);
            }

            HojaActiva = LibroExcel.GetSheetAt(0);

            DataTable dtPrincipal = new DataTable(HojaActiva.SheetName);

            IRow filaEncabezado = HojaActiva.GetRow(0);

            foreach (ICell f in filaEncabezado)
            {
                dtPrincipal.Columns.Add(f.ToString());
            }

            int countDataTable = dtPrincipal.Columns.Count;
            int i = 0;
            int countCell = HojaActiva.GetRow(0).Cells.Count;

            while (HojaActiva.GetRow(i) != null)
            {
                dtPrincipal.Rows.Add();

                for (int j = 0; j < countCell; j++)
                {
                    var celda = HojaActiva.GetRow(i).GetCell(j);

                    if (celda != null)
                    {
                        switch (celda.CellType)
                        {
                            case CellType.Numeric:
                                dtPrincipal.Rows[i][j] = HojaActiva.GetRow(i).GetCell(j).NumericCellValue;
                                break;

                            case CellType.String:
                                dtPrincipal.Rows[i][j] = HojaActiva.GetRow(i).GetCell(j).StringCellValue;
                                break;
                        }
                    }
                }

                i++;
            }

            return dtPrincipal;
        }
        catch
        {
            throw;
        }
    }

    private byte[] LibroExcelToArray()
    {
        try
        {
            if (LibroExcel == null)
            {
                throw new ArgumentNullException(nameof(LibroExcel), "El libro de excel no contiene información");
            }

            byte[] byteArchivo = null;

            using (var memoryStream = new MemoryStream())
            {
                LibroExcel.Write(memoryStream);

                byteArchivo = memoryStream.ToArray();
            }

            return byteArchivo;
        }
        catch
        {
            throw;
        }
    }

    public static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

    private byte[] NuevoLibroExcelToArray(string pathSource)
    {
        try
        {
            if (LibroExcel == null)
            {
                throw new ArgumentNullException(nameof(LibroExcel), "El libro de excel no contiene información");
            }

            byte[] byteArchivo = null;

            using (FileStream fs = new FileStream(pathSource, FileMode.Create, FileAccess.Write))
            {
                LibroExcel.Write(fs);
            }

            byteArchivo = LibroExcelToArray();

            return byteArchivo;
        }
        catch
        {
            throw;
        }
    }

    private TipoDatoExcel GetDataType(Type type)
    {
        try
        {


            string typeName = type.Name;

            TipoDatoExcel resultado = TipoDatoExcel.String;

            switch (typeName.ToUpper())
            {
                case "DECIMAL":
                    resultado = TipoDatoExcel.Decimal;
                    break;
                case "INT32":
                case "INT16":
                case "INT64":
                case "DOUBLE":
                case "BYTE":
                case "REAL":
                case "FLOAT":
                case "SMALLINT":
                case "NUMERIC":

                    resultado = TipoDatoExcel.Number;
                    break;
                case "DATETIME":
                    resultado = TipoDatoExcel.DateTime;
                    break;
                case "STRING":
                case "CHAR":
                case "VARCHAR":
                case "NCHAR":
                case "NVARCHAR":
                case "TEXT":
                case "NTEXT":
                    resultado = TipoDatoExcel.String;
                    break;
                case "BIT":
                case "BOOL":
                case "BOOLEAN":
                    resultado = TipoDatoExcel.Boolean;
                    break;
                default:
                    resultado = TipoDatoExcel.String;
                    break;
            }

            return resultado;
        }
        catch
        {
            throw;
        }
    }

    private void ValidarLibroHojaActiva()
    {
        try
        {
            if (LibroExcel == null)
            {
                throw new ArgumentNullException(nameof(LibroExcel), "Primero debe crear el libro de excel");
            }

            if (HojaActiva == null)
            {
                throw new ArgumentNullException(nameof(HojaActiva), "Primero debe crear la hoja de excel");
            }

        }
        catch
        {
            throw;
        }
    }


}
