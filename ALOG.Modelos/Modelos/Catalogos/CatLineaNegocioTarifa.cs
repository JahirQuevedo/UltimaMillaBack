using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatLineaNegocioTarifa
    {
        [Key]
        public int IdCatLineaNegocioTarifa { get; set; }
        [Required]
        //Servicio
        [ForeignKey("catServicios")]
        public int IdCatServicio { get; set; }
        public CatServicios catServicios { get; set; }

        //LN
        [Required]
        [ForeignKey("catLineaNegocio")]
        public int IdCatLineaNegocio { get; set; }
        public CatLineaNegocio catLineaNegocio { get; set; }

        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("catUsuario")]
        public int IdUsuarioRegistro { get; set; }
        public CatUsuarios catUsuario { get; set; }

        [JsonIgnore]
        public virtual ICollection<CatLineaNegocioTariPrecio> GetCatLineaNegocioTariPrecios { get; set; }

    }
}
