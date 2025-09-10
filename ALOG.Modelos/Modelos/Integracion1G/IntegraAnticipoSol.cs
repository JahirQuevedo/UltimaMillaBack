using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
namespace ALOG.Modelos.Modelos.Integracion1G
{
    public class IntegraAnticipoSol
    {
        [Key]
        public int IdIntAnticipoSol { get; set; }
        [Required]
        [ForeignKey("ordenes")]
        public int IdOrden { get; set; }
        public Ordenes ordenes { get; set; }
        [Required]
        [ForeignKey("integracionReferencia")]
        public int IdPeticionesReferencia { get; set; }
        public IntegraReferencia integracionReferencia { get; set; }
        [Required]
        [ForeignKey("peticionesContenedor")]
        public int IdPeticionesContenedor { get; set; }
        public PeticionesContenedores peticionesContenedor { get; set; }
        [Required]
        public string IdCompaniaExterna { get; set; }
        [Required]
        public string IdSolicitudAnticipoProveedor { get; set; }
        [Required]
        public string ClaveProveedorExterno { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Referencia { get; set; }
        [Required]
        public string Contenedor { get; set; }
        [Required]
        public double MontoTotal { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaEnvio { get; set; }
        [Required]
        public bool Enviado { get; set; } = false;
        public string Estado1G { get; set; }
        public string RespuestaWS1G { get; set; }
        public string Nota1G { get; set; }
    }
}
