using ALOG.Modelos.Modelos.Catalogos;
using System.Text.Json.Serialization;

namespace ALOG.Modelos.Modelos.Vacios
{
    public class PeticionesDocumentos
    {

        [Key]
        public int IdDocumento { get; set; }
        public string DocumentoUUID { get; set; } = Guid.NewGuid().ToString();
        public string Ubicacion { get; set; }

        #region CAMPO IdTipoDocumento
        [ForeignKey("CatDocumento")]
        public int IdTipoDocumento { get; set; }
        public CatDocumentos CatDocumento { get; set; }
        #endregion CAMPO IdTipoDocumento

        public string MimeType { get; set; }
        public string NombreDocumento { get; set; }

        // Esta propiedad no se mapeará a la base de datos
        [NotMapped]
        public string TipoDocumentoNombre { get; set; }

        #region CAMPO IdContenedor
        [ForeignKey("peticionesContenedores")]
        public int IdContenedor { get; set; }
        [JsonIgnore]
        public PeticionesContenedores peticionesContenedores { get; set; }
        #endregion CAMPO IdContenedor

        #region CAMPO IdServicio
        [ForeignKey("peticionesServicios")]
        public int IdServicio { get; set; }
        [JsonIgnore]
        public PeticionesServicios peticionesServicios { get; set; }
        #endregion CAMPO IdServicio
        public int IdUsuarioRegistro { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

    }
}
