using ALOG.Modelos.Modelos.DTO;
using Newtonsoft.Json;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatUsuarios : IActivable
    {
        [Key]
        public int IdCatUsuarios { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string ApellidoMaterno { get; set; }
        [MaxLength(100)]
        [Required]
        public string ApellidoPaterno { get; set; }

        [MaxLength(20)]
        public string RFC { get; set; }
        [StringLength(25)]
        public string CURP { get; set; } = null;
        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Correo { get; set; }
        [Required]
        [MaxLength(100)]
        public string Puesto { get; set; }
        [Required]
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(10)]
        public string Extension { get; set; }
        [MaxLength(20)]
        public string Celular { get; set; }
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "El formato del usuario de tipo correo electrónico")]
        public string Usuario { get; set; }

        [Required]
        public bool Activo { get; set; } = true;


        [MaxLength(500)]
        [JsonProperty]
        [JsonIgnore]
        public string passSistema { get; set; } // Se deserializa en la petición


        [JsonIgnore]
        [MaxLength(500)]
        public string salt { get; set; }

        //[ForeignKey("catUsuarios")]
        //public int IdUsuarioRegistro { get; set; }
        //public CatUsuarios catUsuarios { get; set; }

        [ForeignKey("catTipoPuesto")]
        public int? IdCatTipoPuesto { get; set; }
        public CatTipoPuesto catTipoPuesto { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<CatUsuarioRoles> catUsuarioRoles { get; set; }
        public ICollection<CatUsuariosAduanas> catUsuariosAduanas { get; set; }
        public ICollection<CatUsuariosEmpresa> catUsuariosEmpresas { get; set; }
        public ICollection<CatUsuariosPermisos> catUsuariosPermisos { get; set; }
        public ICollection<CatUsuariosClientes> catUsuariosClientes { get; set; }
        //public CatUsuariosClientes catUsuariosClientes { get; set; }



    }
}
