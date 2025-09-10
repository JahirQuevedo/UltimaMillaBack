using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{

    public class CatClientesClasificacion : IActivable
    {
        [Key]
        public int IdCatClientesClasif { get; set; }

        [Required]
        [ForeignKey("catClientes")]
        public int IdCatclientes { get; set; }
        public CatClientes catClientes { get; set; }

        [Required]
        [ForeignKey("catTipoClasificacion")]
        public int IdCatClasificacion { get; set; }
        public CatTipoClasificacion catTipoClasificacion { get; set; }
        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
