using ALOG.Modelos;


namespace ALOG.Repositorios;

public interface IServicioFotograficoRepositorio
{

    ResultBase Guardar(CargaArchivoFotografico cargaArchivoFotografico);

    ResultBase Eliminar(int idServicioFotografico);

    ResultBase<MonitorServicioFotografico> ObtenerPorId(int idServicioFotografico);

    PaginadoResult<MonitorServicioFotografico> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaServicioFotografico> entidad);

    ResultBase<ArchivoBase> ObtenerZipArchivos(MultipleSeleccionArchivoBase multipleSeleccionArchivoBase);

}
