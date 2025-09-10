using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Control;


namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatUsuariosRepositorio
    {
        CatUsuarios obtenerUsuario(int IdCatUsuario);

        ICollection<CatUsuarios> obtenerUsuarios(FiltroGenericoDTO pFiltro);

        Task<SistemaLoginRespuestaDTO> LoginUser(SistemaLoginDTO usuarioLoginDto);

        Task<CatUsuarios> CrearUsuario(CatUsuarios pCatUsuario);
        Task<bool> BajaUsuario(CatUsuarios pCatUsuario);
        Task<bool> ActivarUsuario(CatUsuarios pCatUsuario);

        Task<CatUsuarios> ObtenerUsuario(int pIdCatUsuario);
        Task<List<CatUsuarios>> ObtenerUsuarios(FiltroGenericoDTO pfiltroGenericoDTO);



    }
}
