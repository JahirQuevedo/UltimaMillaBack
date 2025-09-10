using ALOG.Modelos.Modelos.Catalogos;
namespace ALOG.Modelos.Modelos.Facturacion
{
    public class AnticiposEnc
    {
        [Key]
        public int IdAnticiposEnc { get; set; }

        [ForeignKey("catUsuarios")]
        public int IdUsuarioSolicita { get; set; }
        public CatUsuarios catUsuarios { get; set; }

        [ForeignKey("catUsuariosAutoriza")]
        public int IdUsuarioAutoriza { get; set; }
        public CatUsuarios catUsuariosAutoriza { get; set; }

        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
        public DateTime FechaAutorizacion { get; set; } = DateTime.Now;
        public DateTime FechaEnvio { get; set; } = DateTime.Now;
        public bool Enviado { get; set; } = false;
        public bool Autorizado { get; set; } = false;

        public int IdTipoAnticipo { get; set; }
        public int EstadoAnticipo { get; set; }
        public int IdTipoMoneda { get; set; }
        public double TipoCambio { get; set; }

        public int Clave1G { get; set; }
        public string Estado1G { get; set; }




        public virtual ICollection<AnticiposDet> AnticiposDet { get; set; }
    }
}
