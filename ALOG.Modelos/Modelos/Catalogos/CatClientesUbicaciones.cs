using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO;

namespace ALOG.Modelos.Modelos.Catalogos
{
    [Table("catClientesUbicaciones", Schema = "dbo")]
    public class CatClientesUbicaciones : IActivable
    {
        [Key]
        public int IdClienteUbicacion { get; set; }

        [ForeignKey("Cliente")]
        public int IdCatCliente { get; set; }

        [StringLength(250)]
        public string Calle { get; set; }

        [StringLength(250)]
        public string Colonia { get; set; }

        [StringLength(50)]
        public string NoExterior { get; set; }

        [StringLength(50)]
        public string NoInterior { get; set; }

        [StringLength(10)]
        public string CP { get; set; }

        [StringLength(500)]
        public string URLMaps { get; set; }

        [ForeignKey("catPaises")]
        public int IdCatPaises { get; set; }
        public virtual CatPaises catPaises { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdCatUsuario { get; set; }

        [ForeignKey("catPaisEstados")]
        public int IdCatPaisEstados { get; set; }
        public virtual CatPaisEstados catPaisEstados { get; set; }
        [ForeignKey("catPaisMunicipios")]
        public int IdCatMunicipios { get; set; }
        public virtual CatPaisMunicipios catPaisMunicipios { get; set; }
        public string NombreLugar { get; set; }
        public string Contacto { get; set; }

        [StringLength(250)]
        public string Referencia { get; set; }

        // Propiedad de navegación (si tienes CatClientes)
        public virtual CatClientes Cliente { get; set; }
        

        // Puedes agregar más propiedades de navegación si lo deseas
    }
}
