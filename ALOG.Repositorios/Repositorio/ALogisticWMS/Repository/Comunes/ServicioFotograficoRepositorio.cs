using ALOG.Enums;
using ALOG.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Data;

namespace ALOG.Repositorios;

public class ServicioFotograficoRepositorio : GenericoRepositorio<ServicioFotografia>, IServicioFotograficoRepositorio
{

    private readonly ApplicationDbContext _db;

    protected ArchivoBase archivoBase;
    protected TipoProcesoArchivoControlDocumento tipoProcesoArchivoControlDocumento;

    public ServicioFotograficoRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public ResultBase Guardar(CargaArchivoFotografico cargaArchivoFotografico)
    {

        var resultBase = new ResultBase();

        string rutaBase = cargaArchivoFotografico.ArchivoBase.RutaBase;

        int tipoFoto = (int)cargaArchivoFotografico.TipoFoto;

        string _descTipoFoto = tipoFoto.Equals(1) ? "recepcion" : "reportedannios";

        string rutaRelativa = $"fotos/{cargaArchivoFotografico.FolioTarja}/{cargaArchivoFotografico.IdMercancia}/{_descTipoFoto}/";

        string extension = cargaArchivoFotografico.ArchivoBase.Extension;

        int consecutivo = 0;

        var _listServicioFotografico = _db.ServicioFotografias
                                        .Where(p => p.IdInventario.Equals(cargaArchivoFotografico.IdInventario) && p.TipoFoto.Equals(cargaArchivoFotografico.TipoFoto)).ToList();

        consecutivo = _listServicioFotografico.Count + 1;

        string folioTarjaIdMercancia = cargaArchivoFotografico.FolioTarja.ToString() + '-' + _descTipoFoto + '-' + cargaArchivoFotografico.IdMercancia;

        var _resultConsecutivo = ValidaCantidadFotos(cargaArchivoFotografico.IdInventario, consecutivo, cargaArchivoFotografico.TipoFoto);

        if (_resultConsecutivo.Success)
        {

            cargaArchivoFotografico.ArchivoBase.Archivo = $"{folioTarjaIdMercancia}-{consecutivo}{extension}";

            var documentoUtiliidad = new DocumentoUtilidad();

            (ResultBase resultBase, string rutaCompleta, string rutaSinRutaBase) resultBaseArchivo = documentoUtiliidad.CrearArchivo(nombreArchivo: cargaArchivoFotografico.ArchivoBase.Archivo,
                content: cargaArchivoFotografico.ArchivoBase.Content,
                rutaBase: rutaBase,
                rutaRelativa: rutaRelativa,
                tipoSubDirectorioGeneral: TipoSubDirectorioGeneral.None, sobreEscribirSiExiste: true);

            if (resultBaseArchivo.resultBase?.Success == true && !string.IsNullOrWhiteSpace(resultBaseArchivo.rutaSinRutaBase))
            {
                cargaArchivoFotografico.ArchivoBase.RutaNombreArchivo = resultBaseArchivo.rutaSinRutaBase;
                cargaArchivoFotografico.ArchivoBase.RutaBase = rutaBase;

                ServicioFotografia servicioFotografia = new ServicioFotografia();
                servicioFotografia.IdInventario = cargaArchivoFotografico.IdInventario;
                servicioFotografia.IdFolioServicio = cargaArchivoFotografico.IdFolioServicio;
                servicioFotografia.TipoFoto = cargaArchivoFotografico.TipoFoto;
                servicioFotografia.RutaBase = rutaBase;
                servicioFotografia.RutaNombreArchivo = resultBaseArchivo.rutaSinRutaBase;

                _db.ServicioFotografias.Add(servicioFotografia);
                _db.SaveChanges();

            }
            else
            {
                resultBase.MensajeRespuesta = resultBase.MensajeRespuesta;
            }

        }
        else
        {

            resultBase.MensajeRespuesta = _resultConsecutivo.MensajeRespuesta;

        }

        return resultBase;
    }

    public ResultBase Eliminar(int idServicioFotografico)
    {

        var resultBase = new ResultBase();

        var _servicioFotografico = _db.ServicioFotografias.Where(x => x.Id == idServicioFotografico).SingleOrDefault();

        if (_servicioFotografico != null)
        {

            _db.ServicioFotografias.Remove(_servicioFotografico);
            _db.SaveChanges();

        }
        else
        {
            resultBase.MensajeRespuesta = "No se encontró información del registro solicitado.";
        }

        return resultBase;
    }

    public PaginadoResult<MonitorServicioFotografico> ObtenerListaPaginada(ConsultaCatalogoBase<ConsultaServicioFotografico> entidad)
    {
        var resultBase = new ResultBase<MonitorServicioFotografico>();
        List<MonitorServicioFotografico> listadoServicioFotografico = new List<MonitorServicioFotografico>();

        int _totalRegistros = 0;

        listadoServicioFotografico = (from p in _db.ServicioFotografias
                                      join e in _db.Inventarios on p.IdInventario equals e.Id
                                      where p.IdInventario.Equals(entidad.Entidad.IdInventario)
                                      && p.TipoFoto.Equals(entidad.Entidad.TipoFoto.Equals(TipoFoto.None) ? p.TipoFoto : entidad.Entidad.TipoFoto)
                                      select new MonitorServicioFotografico
                                      {
                                          idServicioFotografico = p.Id,
                                          IdInventario = (int)p.IdInventario,
                                          TipoFoto = p.TipoFoto,
                                          ArchivoBase = new ArchivoBase
                                          {
                                              RutaNombreArchivo = p.RutaNombreArchivo,
                                              RutaBase = p.RutaBase,
                                              EsImagen = true,
                                          }
                                      }).ToList();

        _totalRegistros = listadoServicioFotografico.Count;

        listadoServicioFotografico = listadoServicioFotografico
            .Skip(entidad.RegistrosPorPagina * (entidad.NumeroDePagina - 1))
            .Take(entidad.RegistrosPorPagina).ToList();

        if (!listadoServicioFotografico.Count.Equals(0))
        {
            listadoServicioFotografico.Select(p => { p.ArchivoBase.ContenidoBase64 = ObtenerArchivoFoto(p.ArchivoBase); return p; }).ToList();
        }

        var paginadoInfo = new PaginadoInfo()
        {
            RegistrosPorPagina = entidad.RegistrosPorPagina,
            NumeroDePagina = entidad.NumeroDePagina,
            TotalRegistros = _totalRegistros
        };

        var _paginadoResult = new PaginadoResult<MonitorServicioFotografico>(listadoServicioFotografico, paginadoInfo);

        return _paginadoResult;
    }

    public ResultBase<MonitorServicioFotografico> ObtenerPorId(int idServicioFotografico)
    {
        var resultBase = new ResultBase<MonitorServicioFotografico>();

        MonitorServicioFotografico _servicioFotografia = new MonitorServicioFotografico();

        _servicioFotografia = (from p in _db.ServicioFotografias
                               where p.Id.Equals(idServicioFotografico)
                               select new MonitorServicioFotografico
                               {
                                   IdInventario = (int)p.IdInventario,
                                   TipoFoto = p.TipoFoto,
                                   ArchivoBase = new ArchivoBase
                                   {
                                       RutaNombreArchivo = p.RutaNombreArchivo,
                                       RutaBase = p.RutaBase,
                                       //ContenidoBase64 = ObtenerArchivoFoto(p.RutaBase + p.RutaNombreArchivo),
                                       EsImagen = true,
                                   }
                               }).FirstOrDefault();

        if (_servicioFotografia != null)
        {


            _servicioFotografia.ArchivoBase.ContenidoBase64 = ObtenerArchivoFoto(_servicioFotografia.ArchivoBase);

        }

        resultBase.Data = _servicioFotografia;

        return resultBase;
    }

    private ResultBase ValidaCantidadFotos(int idInventario, int consecutivo, TipoFoto tipoFoto)
    {

        var _result = new ResultBase();

        if (tipoFoto.Equals(TipoFoto.Recepcion))
        {

            if (consecutivo > 3)
            {
                _result.MensajeRespuesta = "El Evidencia Fotográfica de Recepción se encuentra completada.";
            }

        }

        return _result;

    }

    private string ObtenerArchivoFoto(ArchivoBase archivoBase)
    {

        //ResultBase<string> resultBaseLeerFoto = new DocumentoUtilidad().LeerArchivoToBase64(rutaArchivoFoto);

        string frameUri;
        string rutaArchivoFoto = archivoBase.RutaBase + archivoBase.RutaNombreArchivo;

        using (var image = Image.Load(rutaArchivoFoto))
        {
            image.Mutate(x => x.Resize(new Size(320, 240)));
            frameUri = image.ToBase64String(JpegFormat.Instance);
        }

        return frameUri;

    }


    public virtual ResultBase<ArchivoBase> ObtenerZipArchivos(MultipleSeleccionArchivoBase multipleSeleccionArchivoBase)
    {
        try
        {
            var resultBaseArchivoZip = new ResultBase<ArchivoBase>();

            if (multipleSeleccionArchivoBase?.Ids?.Any() == true)
            {
                (List<ArchivoBase> archivoBaseList, string mensajeRespuesta) = GetListaPorIds(idsArchivos: multipleSeleccionArchivoBase.Ids, tipoProcesoArchivoControlDocumento: multipleSeleccionArchivoBase.TipoProcesoArchivoControlDocumento);

                resultBaseArchivoZip.MensajeRespuesta = mensajeRespuesta ?? "No se pudo obtener información de archivos";

                if (resultBaseArchivoZip.Success)
                {

                    if (archivoBaseList?.Any() == true)
                    {
                        string rutaBase = multipleSeleccionArchivoBase.ArchivoBase.RutaBase;

                        int countArchivos = archivoBaseList.Count();

                        string[] rutasArchivos = new string[countArchivos];

                        bool existenArchivos = false;

                        for (int j = 0; j < countArchivos; j++)
                        {

                            archivoBase = archivoBaseList[j];

                            tipoProcesoArchivoControlDocumento = archivoBaseList[j].TipoProcesoArchivoControlDocumento;

                            //Ruta del archivo a leer de fotografia
                            string rutaArchivo = ObtenerRutaArchivoPathCombineRutaBase(archivoBaseList[j].RutaNombreArchivo, rutaBaseServidor: rutaBase);

                            if (File.Exists(rutaArchivo))
                            {
                                rutasArchivos.SetValue(rutaArchivo, j);

                                existenArchivos = true;
                            }
                        }

                        if (existenArchivos)
                        {
                            //Nombre de zip NumFolioServicio_yyyyMMdd-HHmmss.zip

                            string nombreZip = $"{multipleSeleccionArchivoBase.TipoProcesoArchivoControlDocumento.ToString()}_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.zip";

                            //Se genera en la ruta base
                            string rutaZip = ObtenerRutaArchivoPathCombineRutaBase(nombreZip, rutaBaseServidor: rutaBase);

                            //Crea ZIP temporal en el servidor
                            ZIPCoderUtility.GenerateZip(rutaZip, rutasArchivos, false, false);

                            var documentoUtility = new DocumentoUtilidad();

                            //El zip se convierte a base64
                            ResultBase<string> resultBaseLeerArchivo = documentoUtility.LeerArchivoToBase64(rutaZip);

                            if (resultBaseLeerArchivo?.Success == true)
                            {
                                resultBaseArchivoZip.Data = new ArchivoBase
                                {
                                    ContenidoBase64 = resultBaseLeerArchivo.Data,
                                    ContentType = "application/zip",
                                    Archivo = nombreZip
                                };

                                //Se manda a eliminar el archivo zip temporal generado en el servidor.
                                documentoUtility.EliminarArchivo(rutaZip);
                            }
                            else
                            {
                                resultBaseArchivoZip.MensajeRespuesta = resultBaseLeerArchivo?.MensajeRespuesta ?? "No se pudo leer el archivo.";
                            }
                        }
                        else
                        {
                            resultBaseArchivoZip.MensajeRespuesta = "No existen archivos de archivos para generar el zip.";
                        }

                    }
                    else
                    {
                        resultBaseArchivoZip.MensajeRespuesta = "No existe información de archivos para generar el zip.";
                    }
                }
            }
            else
            {
                resultBaseArchivoZip.MensajeRespuesta = "No se envia información de archivos para descargar.";
            }

            return resultBaseArchivoZip;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public string ObtenerRutaArchivoPathCombineRutaBase(string rutaNombreArchivo, string rutaBaseServidor = null)
    {

        string fileFullPath = rutaBaseServidor + rutaNombreArchivo;
        DirectoryInfo directoryInfo = new DirectoryInfo(fileFullPath);

        string filePath = directoryInfo.FullName;

        return filePath;
    }

    public virtual (List<ArchivoBase> servicioFotografiasList, string mensajeRespuesta) GetListaPorIds(List<int> idsArchivos, TipoProcesoArchivoControlDocumento tipoProcesoArchivoControlDocumento)
    {
        try
        {

            List<ArchivoBase> archivoBaseList = new List<ArchivoBase>();

            string mensajeRespuesta = String.Empty;

            foreach (var idInventarioArchivo in idsArchivos)
            {
                var _listaArchivosBase = (from p in _db.ServicioFotografias
                                          where p.IdInventario.Equals(idInventarioArchivo)
                                          select new ArchivoBase
                                          {

                                              RutaNombreArchivo = p.RutaNombreArchivo,
                                              RutaBase = p.RutaBase,
                                              EsImagen = true,

                                          }).ToList();
                foreach (var _archivoBaseAux in _listaArchivosBase)
                {

                    archivoBaseList.Add(_archivoBaseAux);

                }

            }

            return (archivoBaseList, mensajeRespuesta);
        }
        catch
        {
            throw;
        }
        finally
        {

        }
    }


}
