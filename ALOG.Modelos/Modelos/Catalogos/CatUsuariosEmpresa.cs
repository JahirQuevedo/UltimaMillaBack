using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatUsuariosEmpresa : IActivable
    {
        [Key]
        public int IdCatUsuariosEmpresa { get; set; }

        [ForeignKey("CatUsuarios")]
        public int IdCatUsuarios { get; set; }
        public virtual CatUsuarios CatUsuarios { get; set; }

        [ForeignKey("catClientes")]
        public int? IdCatCliente { get; set; }
        public virtual CatClientes catClientes { get; set; }

        [ForeignKey("catEmpresas")]
        public int? idCatEmpresa { get; set; }
        public CatEmpresas catEmpresas { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [ForeignKey("catProveedores")]
        public int? IdCatProveedor { get; set; }
        public virtual CatProveedores catProveedores { get; set; }


        //[ForeignKey("catUsuariosReg")]
        //public int IdUsuarioRegistro { get; set; }
        //public CatUsuarios catUsuariosReg { get; set; }
    }
}
