using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatAgentesAduanales : IActivable
    {
        [Key]
        public int IdCatAgenteAduanal { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string RFC { get; set; }

        public string Acronimo { get; set; }
        [Required]
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        [ForeignKey("catUsuariosRegistro")]
        public int IdUsuarioRegistro { get; set; }
        public CatUsuarios catUsuariosRegistro { get; set; }




    }
}
