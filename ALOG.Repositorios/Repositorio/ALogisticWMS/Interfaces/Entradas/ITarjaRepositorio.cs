using ALOG.Modelos;

namespace ALOG.Repositorios;

public interface ITarjaRepositorio
{

    ResultBase<MonitorTarja> Guardar(MonitorTarja monitorTarja);

    ResultBase Eliminar(int idTarja);

    ResultBase GuardarCargaMasiva(CargaMasiva cargaMasiva);

    ResultBase<MonitorTarja> Confirmar(MonitorTarja monitorTarja);

    ResultBase<MonitorTarja> ObtenerPorId(int idTarja);

    PaginadoResult<MonitorTarja> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaTarja> entidad);

    PaginadoResult<MonitorTarja> ObtenerListaPaginadaPorIdReferencia(ConsultaCatalogoBase<ConsultaTarja> entidad);

    PaginadoResult<MonitorTarjaInventario> ObtenerListaPaginadaTarjaInventarioPorIdReferencia(ConsultaCatalogoBase<ConsultaTarja> entidad);

}
