using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;


namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatUsuariosEmpresaRepositorio : IGenericoRepositorio<CatUsuariosEmpresa>
    {
        CatUsuariosEmpresa exiteUsuarioEmpresa(int IdEmpresa, int IdCatUsuario);
        CatUsuariosEmpresa exiteUsuarioCliente(int IdCliente, int IdCatUsuario);
        ICollection<CatUsuariosEmpresa> ObtenerEmpresasClientesPorUsuario(int IdCatUsuario);
        ICollection<CatUsuariosEmpresa> ObtenerUsuariosEmpresa(int IdCatEmpresa);
        ICollection<CatUsuariosEmpresa> ObtenerUsuariosCliente(int IdCatCliente);
    }
}
