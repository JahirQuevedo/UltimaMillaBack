using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Peticiones;
using ALOG.Modelos.Modelos.DTO.Utilerias;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Utilerias;
using ALOG.Repositorios.Utilerias.IUtileria;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace ALOG.Repositorios.Utilerias
{
    public class UtileriaExportacionExcel : IUtileriaExportacionExcel
    {
        private readonly ApplicationDbContext _context;
        private IGenericoRepositorio<PeticionesContenedoresCron> _ctgenericoRepositorio;

        public byte[] GenerarExcelDinamico([FromBody] List<Dictionary<string, object>> data)
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Reporte");

            if (data == null || !data.Any())
                return null;

            var headers = data.First().Keys.ToList();

            for (int i = 0; i < headers.Count; i++)
            { 
                worksheet.Cells[1, i + 1].Value = headers[i];
                worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Crimson);
                worksheet.Cells[1, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[1, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            for (int i = 0; i < data.Count; i++)
            {
                var row = data[i];
                for (int j = 0; j < headers.Count; j++)
                {
                    var cell = worksheet.Cells[i + 2, j + 1];
                    cell.Value = row[headers[j]];
                    cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    //cell.Style.WrapText = true;

                }
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }

        public async Task<List<FilaExcelDTO>> ProcesarExcel(IFormFile archivo)
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var resultado = new List<FilaExcelDTO>();

            if (archivo == null || archivo.Length == 0)
                return resultado;

            using var stream = new MemoryStream();
            await archivo.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);
            var hoja = package.Workbook.Worksheets[0];

            var encabezados = new List<string>();
            for (int col = 1; col <= hoja.Dimension.End.Column; col++)
            {
                var encabezado = hoja.Cells[1, col].Text;
                encabezados.Add(encabezado);
            }

            for (int row = 2; row <= hoja.Dimension.End.Row; row++)
            {
                var fila = new FilaExcelDTO();
                for (int col = 1; col <= hoja.Dimension.End.Column; col++)
                {
                    var key = encabezados[col - 1];
                    var value = hoja.Cells[row, col].Text;
                    fila.Columnas[key] = value;
                }
                resultado.Add(fila);
            }

            return resultado;
        }
    }
}
