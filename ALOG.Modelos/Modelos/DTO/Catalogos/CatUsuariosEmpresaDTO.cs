using ALOG.Modelos.Modelos.Catalogos;
namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatUsuariosEmpresaDTO
    {
        public int IdCatUsuariosEmpresa { get; set; }


        public int IdCatUsuarios { get; set; }
        public virtual CatUsuarios CatUsuarios { get; set; }


        public int IdCliente { get; set; }
        public virtual CatClientes catClientes { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        public int IdUsuarioRegistro { get; set; }
        public CatUsuarios catUsuarios { get; set; }
    }
}
