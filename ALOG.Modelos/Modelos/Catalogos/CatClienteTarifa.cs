using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatClienteTarifa : IActivable
    {
        [Key]
        public int IdCatClienteTarifa { get; set; }


        [Required]
        [ForeignKey("catClientesServicioAduana")]
        public int IdCatCteServAduana { get; set; }
        public virtual CatClientesServicioAduana catClientesServicioAduana { get; set; }

        [Required]
        public double Precio { get; set; }
        [Required]
        public double Impuesto { get; set; }

        [ForeignKey("IdCatContenedor")]
        public int? IdCatContenedor { get; set; }
        public virtual CatContenedor catContenedor { get; set; }


        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuario { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;




    }
}
