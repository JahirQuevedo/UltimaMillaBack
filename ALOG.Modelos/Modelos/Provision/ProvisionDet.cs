using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Modelos.Modelos.Integracion1G;
using Newtonsoft.Json;

namespace ALOG.Modelos.Modelos.Provision
{
    public class ProvisionDet : IActivable
    {
        [Key]
        public int IdProvisionesDet { get; set; }

        [Required, ForeignKey("provisionesEnc")]
        public int IdProvisionesEnc { get; set; }
        public ProvisionEnc provisionesEnc { get; set; }

        [Required, ForeignKey("peticionesReferencia")]
        public int IdReferencia { get; set; }
        public PeticionesReferencias peticionesReferencia { get; set; }

        [Required, ForeignKey("peticionesContenedor")]
        public int IdContenedor { get; set; }
        public PeticionesContenedores peticionesContenedor { get; set; }

        [Required, ForeignKey("catusuarios")]
        public int IdCatUsuarios { get; set; }
        public CatUsuarios catusuarios { get; set; }

        [Required, MaxLength(10)]
        public string Estatus { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        public int Partida { get; set; }
        public string Servicio { get; set; }

        public int Cantidad { get; set; }
        public double Costo { get; set; }
        public double Impuesto { get; set; }
        public double TotalPartida { get; set; }
        public string Observaciones { get; set; }
        [JsonIgnore]
        [NotMapped]
        public List<IntegraFacturaDet> IntegracionesFacturaDet { get; set; }


    }
}
