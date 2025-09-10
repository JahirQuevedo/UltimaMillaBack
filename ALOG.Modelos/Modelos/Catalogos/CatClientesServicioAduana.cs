using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatClientesServicioAduana : IActivable
    {
        [Key]
        public int IdCatCteServAduana { get; set; }

        [ForeignKey("CatClientes")]
        public int IdCatClientes { get; set; }
        public virtual CatClientes CatClientes { get; set; }
        [Required]
        [ForeignKey("catAduana")]
        public int IdCatAduana { get; set; }
        public virtual CatAduana catAduana { get; set; }
        [Required]
        [ForeignKey("catServicios")]
        public int IdCatServicio { get; set; }
        public virtual CatServicios catServicios { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public virtual CatUsuarios catUsuario { get; set; }

        public virtual ICollection<CatClienteTarifa> GetCatClienteTarifas { get; set; }



    }
}
