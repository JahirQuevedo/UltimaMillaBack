using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ALOG.Modelos.Modelos.DTO.Utilerias
{
    public class UploadFileDTO
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
