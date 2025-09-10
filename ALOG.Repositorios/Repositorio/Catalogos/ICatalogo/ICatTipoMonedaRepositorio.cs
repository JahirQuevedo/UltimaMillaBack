using ALOG.Modelos.Modelos.DTO.Respuestas;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatTipoMonedaRepositorio
    {

        Task<RespuestaGenericaDTO> ListarMonedas();

    }
}
