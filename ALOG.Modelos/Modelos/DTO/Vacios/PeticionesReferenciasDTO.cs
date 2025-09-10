using ALOG.Modelos.Modelos.Catalogos;

namespace ALOG.Modelos.Modelos.DTO.Vacios
{
    public class PeticionesReferenciasDTO
    {
        public int IdReferencia { get; set; }
        public int Ticket { get; set; }
        [StringLength(13, MinimumLength = 12, ErrorMessage = "El RFC debe tener entre 12 y 13 caracteres")]
        public string Transporte_RFC { get; set; }
        public string Transporte_RazonSocial { get; set; }
        public int TrasporteId { get; set; }
        public string Transporte_Usuario { get; set; }
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Transporte_UsuarioEmail { get; set; }
        public string Comentarios { get; set; }
        public int TipoReferencia { get; set; } = 1;
        //public CatPatios catPatios { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaSolicitud { get; set; }= DateTime.Now;
        public DateTime FechaProcesado { get; set; }
        public string EstadoReferencia { get; set; } = "PE";
        public int IdCatReferenciaEstado { get; set; } = 7;
        public string Procesado { get; set; } = "N";
        public int IdOrden { get; set; }
        public bool Activo { get; set; } = true;
        public ICollection<PeticionesContenedoresDTO> Contenedores { get; set; }
    }
}
