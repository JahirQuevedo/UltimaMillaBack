using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO.Utilerias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALOG.Repositorios.Utilerias.IUtileria
{
    public interface IUtileriaExportacionExcel
    {
        byte[] GenerarExcelDinamico([FromBody] List<Dictionary<string, object>> data);
        Task<List<FilaExcelDTO>> ProcesarExcel(IFormFile archivo);
    }
}
