namespace ALOG.Enums;

/// <summary>
/// Opciones para generar reporte
/// </summary>
public enum TipoConvertReporte
{
    None,
    Base64,
    Bytes,
    ReportDocument,
    FileTemp,
    ReportDocumentTotalPaginas,
    Print
}
/// <summary>
/// Tipo de consulta para el reporteador
/// </summary>
public enum TipoConsultaReporteador
{
    None,
    Previsualizar,
    Exportar
}

/// <summary>
/// Tipo de dato para los parametros del eporteador
/// </summary>
public enum TipoDatoParametro
{
    [StringValue("None")]
    None,
    [StringValue("varchar")]
    Varchar,
    [StringValue("nvarchar")]
    NVarchar,
    [StringValue("int")]
    Int,
    [StringValue("decimal")]
    Decimal,
    [StringValue("bit")]
    Bit,
    [StringValue("date")]
    Date,
    [StringValue("time")]
    Time,
    [StringValue("datetime")]
    DateTime,
    [StringValue("timestamp")]
    TimeStamp,
    /// <summary>
    /// Para controles select para un listado de años
    /// </summary>
    [StringValue("selectanio")]
    SelectAnio,
    /// <summary>
    /// Para controles select para un listado de meses
    /// </summary>
    [StringValue("selectmes")]
    SelectMes,
    /// <summary>
    /// Para controles select para un listado de dias
    /// </summary>
    [StringValue("selectdias")]
    SelectDias,
    /// <summary>
    /// Indica que se lanzara una catalogo dinamico
    /// </summary>
    [StringValue("launchseek")]
    LaunchSeek,
    /// <summary>
    /// Para controles select
    /// </summary>
    [StringValue("select")]
    Select,
    [StringValue("tinyint")]
    Tinyint,
    [StringValue("smallint")]
    Smallint,
    [StringValue("bigint")]
    Bigint,
    [StringValue("numeric")]
    Numeric,
    [StringValue("float")]
    Float,
    [StringValue("real")]
    Real,
    [StringValue("selectjson")]
    SelectJSON,
}
