namespace ALOG.Enums;

public enum EstadoCotizacion
{
    None,
    Simulada,
    Cotizado,
    Asignada,
    Cancelada,
    Facturada
}

public enum EstadoTraslado
{
    None,
    Programado,
    Asignado,
    EnTransito,
    PendienteIngreso,
    Completado,
    Cancelado
}

public enum EstadoTurno
{
    None,
    Creado,
    Iniciado,
    Completado,
    Cancelado,
    EsperaDeCarga,
    EnProcesoDeCarga,
    CargadoYAutorizado
}

public enum EstadoPreTurno
{
    None,
    Pendiente,
    Asignado,
    Cancelado
}

public enum EstadoTurnoSalida
{
    None,
    Creado,
    Completado = 3,
    Cancelado,
    EnEsperaCarga,
    EnProcesoCarga,
    CargadoYAutorizado
}

public enum EstadoTransferenciaInterna
{
    None,
    Pendiente,
    Aceptado,
    Cancelado
}
public enum EstadoManifiestoConsolidacion
{
    None,
    Preliminar,
    Consolidado,
    Cancelado
}

/// <summary>
/// PendientePorIngresar - PI
/// Ingresado            - 01
/// EnEjecucion          - 02
/// liberado             - 03
/// </summary>
public enum EstadoInventario
{
    [StringValue("None")]
    None,
    [StringValue("PI - Pendiente Por Ingresar")]
    PendienteIngresar,
    [StringValue("01 - Ingresado")]
    Ingresado = 2,
    [StringValue("02 - En Ejecucion")]
    EnEjecucionServicio,
    [StringValue("03 - Liberado")]
    Liberado
}

/// <summary>
/// AbandonoNoNotificado - 2M
/// AbandonoNotificado   - 3M
/// Resguardo            - 99
/// </summary>
public enum EstadoRetenidoInventario
{
    None,
    AbandonoNoNotificado,
    AbandonoNotificado,
    Resguardo
}

public enum EstadoFolioServicio
{
    None,
    Cancelado,
    Pendiente,
    Iniciado,
    Terminado
}

public enum EstadoFactura
{
    None,
    Pendiente,
    Pagada,
    Aceptada,
    Cobrada,
    Cancelada,
    NoProcesado

}

/// <summary>
/// Enumeraciones para estado de facturacion del folio de servicio
/// </summary>
public enum EstadoFacturacion
{
    None,
    Facturado,
    Pendiente,
    EnProceso,
    Cancelado,
    Revisado
}

public enum EstadoBitacoraProcesoAbandono
{
    None,
    Finalizado,
    Cancelado,
    Omitido
}

public enum EstadoSolicitudIngreso
{
    None,
    NoIngresado,
    Ingresado,
    Cancelado,
    PendienteSicrefis
}

public enum EstadoFit
{
    None,
    Pendiente,
    Confirmado,
    Cancelado,
    Cerrado
}

public enum EstadoFitEnCalt
{
    None,
    Directo,
    EnEspera,
    Remediacion,
    Cancelado
}

public enum EstadoFitEnRecinto
{
    None,
    Directo,
    EnEspera,
    Cancelado
}

public enum EstadoTarja
{
    /// <summary>
    /// Ninguno
    /// </summary>
    None,
    /// <summary>
    /// Preliminar o Pretarja
    /// </summary>
    Preliminar,
    /// <summary>
    /// Confirmada
    /// </summary>
    Confirmado,
    /// <summary>
    /// Cancelada
    /// </summary>
    Cancelado
}

public enum EstadoSuministroBitacoraTemperatura
{
    None,
    Conectado,
    Desconectado
}

public enum EstadoReferencia
{
    None,
    Preliminar,
    Programado,
    Cancelado,
    Cerrado
}

public enum EstadoDesconsolidacion
{
    None,
    Iniciado,
    Finalizado,
    Cancelado
}

public enum EstadoLiberacion
{
    None,
    Pendiente,
    Autorizada,
    EnProcesoSalida,
    Cerrada,
    Cancelada
}

public enum EstadoSalida
{
    None,
    Pendiente,
    EnProceso,
    Cerrado,
    Cancelado
}
