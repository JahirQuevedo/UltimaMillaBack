using ALOG.Modelos.Modelos.DTO.Catalogos;

namespace ALOG.Modelos.Modelos.DTO.Vacios
{
    public class PeticionesDocumentosDTO
    {

        public int IdDocumento { get; set; }
        //public byte[] Documento { get; set; }
        public string DocumentoUUID { get; set; }

        //public string Ubicacion { get; set; }
        public string NombreDocumento { get; set; }
        public string MimeType { get; set; }
        //public string TipoDocumento { get; set; }
        public int IdServicio { get; set; }
        [ForeignKey("CatDocumentos")]
        public int IdTipoDocumento { get; set; }
        public CatDocumentosDTO CatDocumentos { get; set; }
        // Esta propiedad no se mapeará a la base de datos
        public string TipoDocumentoNombre { get; set; }
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
