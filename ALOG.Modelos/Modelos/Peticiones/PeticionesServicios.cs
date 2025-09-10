using ALOG.Modelos.Modelos.Catalogos;
using System.Text.Json.Serialization;

namespace ALOG.Modelos.Modelos.Vacios
{
    public class PeticionesServicios
    {
        [Key]
        public int IdServicio { get; set; }
        [ForeignKey("catServicios")]
        [Column("IdCatServicio")]
        public int IdTipoServicio { get; set; }
        public CatServicios catServicios { get; set; }
        public string DescServicio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }
        public ICollection<PeticionesDocumentos> Documentos { get; set; }
        [ForeignKey("PeticionesContenedores")]
        public int IdContenedor { get; set; }
        [JsonIgnore]
        public PeticionesContenedores PeticionesContenedores { get; set; }
        public string EstadoServicio { get; set; }
        [ForeignKey("catReferenciaEstado")]
        public int IdEstadoServicio { get; set; }
        public CatReferenciaEstado catReferenciaEstado { get; set; }
        public string ReferenciaClienteFacturar { get; set; }
        public bool Activo { get; set; }
        //Servicios Facturados a otro cliente.
        [ForeignKey("catClientesFacturarA")]
        public int? IdClienteFacturarA { get; set; }
        public CatClientes catClientesFacturarA { get; set; }
    }
}