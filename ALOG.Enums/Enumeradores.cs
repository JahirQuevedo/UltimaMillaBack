namespace ALOG.Enums;

public enum TipoProcesoArchivoControlDocumento
{
    None,
    Archivo,
    EIR,
    EIRFirmaDigital,
    LiberacionControlDocumentos,
    CotizadorControlDocumentos,
    SalidaControlDocumentos,
    AbandonoControlDocumentos,
    ExpedienteRecepcion,
}

public enum TipoSubDirectorioGeneral
{
    /// <summary>No agregar ninguna carpeta</summary>
    None,
    /// <summary>Agregar subcarpeta por anio, mes y dia</summary>
    IncluirAnioMesDia,
    /// <summary>Agregar subcarpeta por anio y mes</summary>
    IncluirAnioMes,
    /// <summary>Agregar subcarpeta por anio</summary>
    IncluirAnio,
}

public enum TipoDatoExcel
{
    String,
    Number,
    Decimal,
    DateTime,
    Boolean
}

public enum TipoPersona
{
    None,
    Moral,
    Fisica
}
public enum EjeNegocio
{
    None,
    LCL,
    FCL,
    FrioPuerto
}

public enum TipoDireccion
{
    None,
    Empresa,
    Persona,
    Cliente,
    Consignatario,
    FacturarA,
    ImpoExpoProvDest,
    Maniobrista,
    AgenteAduanal,
    Transportista
}

public enum TipoMedioCatalogo
{
    None,
    Empresa,
    Persona,
    PersonaEmpresa,
    Cliente,
    Consignatario,
    FacturarA,
    ImpoExpoProvDest,
    Maniobrista,
    AgenteAduanal,
    Transportista

}

public enum ClaveTipoEmpresa
{
    None,
    Almacen,
    AgenteCarga,
    AgenciaAduanal,
    Forwarder
}

public enum EstadoInicialContenedor
{
    None,
    Vacio,
    Lleno,
}

public enum TipoAgente
{
    None,
    Aduanal,
    Carga
}

public enum EstadoFinalContenedor
{
    None,
    Vacio,
    Lleno,
    Consolidado,
    Desconsolidado
}

public enum TipoTipoContenedor
{
    None,
    Estandar,
    Termico,
    Refrigerado,
    CargaSeca,
    CargaNombrados,
    Plataforma,
    Tanque
}

public enum TipoNaviera
{
    None,
    Naviera,
    AgenteNaviero,
    LineaAerea
}

public enum TipoDonacionDestruccion
{
    None,
    Donacion,
    Destruccion
}

public enum TipoImportadorExportadorProveedorDestinatario
{
    None,
    ImportadorExportador,
    ProveedorDestinatario
}

public enum ClasePersonal
{
    None,
    Eventual,
    Fijo,
    Externo,
    Confianza,
    Nombramiento,
    SINDEVENT
}

/// <summary>
/// Tipo de personal para la pestania de Personal en maniobras solicitadas relacionado a un servicio.
/// </summary>
public enum TipoPersonal
{
    None,
    Ordinario,
    Adicional
}

/// <summary>
/// Aplicar Costos [0 – Vacío “ “, 1 -  Catálogo de Personal y 2 – Catálogo Categoría de Empleados]
/// </summary>
public enum AplicarCostosPor
{
    None,
    CatalogoPersonal,
    CatalogoCategoriaEmpleados
}

/// <summary>
/// Contenedor  - CN
/// CargaSuelta - CS
/// </summary>
public enum TipoMercanciaInventario
{
    None,
    Contenedor,
    CargaSuelta
}

public enum AutoridadFolioServicio
{
    None,
    SAGARPA,
    PROFEPA,
    PGR,
    ADUANA,
    MARINA,
    SEDERNA,
    SEMARNAT,
    SSA,
    COFEPRIS
}

public enum TipoOperacionTurno
{
    None,
    Entrada,
    Salida
}

public enum TipoTurno
{
    None,
    Turno1,
    Turno2,
    Turno3

}

public enum TipoRevision
{
    None,
    Fitosanitaria,
    Zoosanitaria,
    Acuicola
}

public enum TipoOperacionAduaneraOficial
{
    None,
    Importacion,
    Exportacion
}

public enum TipoOperacionAduanera
{
    None,
    Importacion,
    Exportacion,
    Ambas
}

public enum TipoPuerto
{
    None,
    Aereo,
    Terrestre
}

public enum TipoMedioTransporte
{
    None,
    Carretero,
    Ferroviario
}
public enum TipoMedioTransporteCatalogo
{
    None,
    Carretero,
    Ferroviario,
    Ambos
}

/// <summary>
/// CER - CambioEstadoAbandono
/// ENT - Entrada
/// SEP - Separacion
/// SBD - Subdivision
/// CDD	- Cesión de Derechos
/// CNE - Cancela Entrada
/// SAL - Salidas
/// CNS - Cancela Salida
/// REV - Revalidación
/// RDS - Retroceso de Salida
/// CTO - Cambio de Tipo de Operación
/// </summary>
public enum TipoMovimientoKardex
{
    None,
    CambioEstadoAbandono,
    Entrada,
    Separacion,
    Subdivision,
    CesiónDerechos,
    CancelaEntrada,
    Salida,
    CancelaSalida,
    Revalidacion,
    RetrocesoSalida,
    Consolidacion,
    AjusteMas,
    AjusteMenos,
    CambioDeTipoDeOperacion,
    SalidaPorRevalidacion
}

public enum TipoAutoridadServicioWeb
{
    None,
    Interno,
    API,
    Sicrefis,
    Proceda,
    Vucem,
    Demo,
    Calt,
    Nombramientos
}

/// <summary>
/// Enum relacionado con la tabla [SORF].[SORF_052_TIPO_SERVICIO_ALMACEN]
/// </summary>
public enum TipoServicioAlmacenDescripcion
{
    [StringValue("None")]
    None,
    [StringValue("Consolidación de Contenedor")]
    ConsolidacionContenedor,
    [StringValue("Desconsolidación de Contenedor")]
    DesconsolidacionContenedor,
    [StringValue("Traslados de Mercancías")]
    TrasladoMercancias,
    [StringValue("Etiquetados")]
    Etiquetados,
    [StringValue("Separaciones de Mercancías")]
    SeparacionMercancias,
    [StringValue("Entregas de Carga a S.P.F.")]
    EntregasCargaSPF,
    [StringValue("Recepciones de Exportación")]
    RecepcionesExportacion,
    [StringValue("Revisión de Mercancías")]
    RevisionMercancias
}


/// <summary>
/// El tipo de consulta que se ara para tener el folio de servicio dependiendo la opcion
/// </summary>
public enum TipoConsultaFolioServicio
{
    None,
    ServicioRealizado,
    ServicioAdicional,
    Personal,
    Insumos,
    MaquinariaEqupo,
    ArchivoFotografico
}

/// <summary>
/// Tipo de Relacion de Folio de Servicio
/// </summary>
public enum TipoRelacionFolioServicio
{
    None,
    Contenedor,
    Tarja,
    PartidaRevalidada,
    Partida
}

public enum MedioEntrada
{
    None,
    Camion,
    Ferrocarril
}

#region AsignacionFolioServicioPersonalOperativo
public enum TipoFechaConsultaAsignacionFolioServicioPersonalOperativo
{
    None,
    Programacion,
    Terminacion
}

public enum BuscarPorConsultaAsignacionFolioServicioPersonalOperativo
{
    None,
    FolioServicio,
    Contenedor,
    TarjaEntrada,
    Controlador,
    AgenteAduanal
}
#endregion

#region Traslados
public enum Prioridad
{
    None,
    Alta,
    Media,
    Baja
}

public enum BuscarPorConsultaTraslado
{
    None,
    Cliente,
    Referencia,
    NumeroContenedor,
    TipoMercancia,
    Prioridad,
    NumeroDeViaje,
    Patente,
    TipoContenedor,
    Booking
}

public enum BuscarPorConsultaIngreso
{
    None,
    Cliente,
    Referencia,
    NumeroContenedor,
    FolioServicio
}

public enum BuscarPorConsultaViajeTraslado
{
    None,
    Folio,
    Transportista,
    Placas,
    TipoEntrada
}

public enum BuscarPorConsultaReporteViajeTraslado
{
    None,
    Transportista,
    Placas
}

public enum TipoFechaEdicion
{
    None,
    Inicio,
    Fin
}

public enum TipoEntrada
{
    None,
    Carretero,
    Ferroviario
}

public enum TipoViaje
{
    None,
    Entrada,
    Salida
}

public enum TransporteMercancia
{
    None,
    CargaGeneral,
    Intermodal
}

#endregion

#region Configuracion EIR
public enum CatalogoEir
{
    TipoContenedor = 1,
    TipoVehiculo,
    TipoEmbalaje
}
#region ConfiguracionSistema
public enum ClaveConfiguracionSistema
{
    None,
    ABANDONO,
    CONEXIONES,
    SERVICIOS,
    ALMACENAJES,
    VARIOS,
    FACTURACION,
    CONSECUTIVOS,
    REGLA239
}

public enum TipoFechaAbandonosConfiguracionSistema
{
    None,
    FechaDeAtraque,
    FechaDeDescarga
}
#endregion

public enum LadoEir
{
    None,
    /// <summary>
    /// Valor utilizado para tipo de contenedor, tipo de vehiculo y tipo embalaje.
    /// </summary>
    Frente,
    /// <summary>
    /// Valor utilizado para tipo de vehiculo y tipo embalaje.
    /// </summary>
    CostadoDerecho,
    /// <summary>
    /// Valor utilizado para tipo de vehiculo y tipo embalaje.
    /// </summary>
    CostadoIzquierdo,
    /// <summary>
    /// Valor utilizado para tipo de contenedor.
    /// </summary>
    Posterior,
    /// <summary>
    /// Valor utilizado para tipo de contenedor.
    /// </summary>
    PisoInterior
}
#endregion

#region Paquete
public enum TipoEntradaPaquete
{
    None,
    Traslado,
    Ingreso
}

/// <summary>
/// Enum para identificar para realizar filtros de paquete. No se permite opción None.
/// </summary>
public enum TipoMercanciaPaqueteFiltro
{
    Ambas,
    Contenedor,
    CargaSuelta
}

#endregion

#region Inventario
public enum TipoAlmacenInventario
{
    None,
    Seco,
    Frio
}

public enum BuscarPorConsultaInventario
{
    None,
    Consignatario,
    Contenedor,
    TarjaEntrada,
    FolioRevalidado,
    Referencia,
    ReferenciaOrigen,
    Patente,
    Pedimento,
    BookingBL,
    Viaje,
    Cliente,
}
#endregion

#region Fit
public enum TipoFit
{
    None,
    Carga,
    Descarga
}

public enum ColorCantidadAsignacionesHorarioFit
{
    None,
    Gris,
    Amarillo,
    Rojo
}

public enum TipoHorarioFit
{
    None,
    HorarioConfigurado,
    FueraHorarioConAlmacen,
    FueraHorarioSinAlmacen
}

public enum TipoOperacionMonitorFit
{
    None,
    Importacion,
    Exportacion,
    TransferenciaOVacio
}

/// <summary>
/// Tipo de Operacion en Calt
/// </summary>
public enum TipoOperacionCalt
{
    None,
    Arribo,
    Salida,
    CambioEstado
}
#endregion

#region Almacen (transaccional)
public enum TipoTarja
{
    None,
    Entrada,
    Salida
}

public enum TipoServicioTarja
{
    [StringValue("None")]
    None,
    [StringValue("Desconsolidación")]
    Desconsolidacion,
    [StringValue("Desconsolidación SPF")]
    DesconsolidacionSpf,
    [StringValue("Entrada Almacen")]
    EntradaAlmacen,
    [StringValue("Separación")]
    Separacion,
    [StringValue("Subdivicion Mercancía")]
    SubdivisionMercancia,
    [StringValue("Consolidadción")]
    Consolidacion,
    [StringValue("Salida Almacén")]
    SalidaAlmacen,
    [StringValue("Entrada Por Transferencia")]
    EntradaPorTransferencia,
    [StringValue("Entrada a Patio Externo")]
    EntradaPatioExterno

}

public enum BuscarPorConsultaReferenciaAlmacen
{
    None,
    Barco,
    FolioReferencia,
    ReferenciaAgenteAduanal,
    NumeroContenedor,
    NumeroConocimientoBookingBl,
    FolioServicio,
    FolioTarjaEntrada,
    FolioTarjaSalida,
    Marcas,
    Números,
    ClaveUnicaAbandono
}

/// <summary>
/// Se tomaron las claves del catálogo de incidentes de SICREFIS considerando que en un futuro se notifiquen
/// </summary>
public enum TipoPartidaIncidencia
{
    None,
    Otros = 1,
    DiscrepanciaSello = 2,
    DiscrepanciaDimension = 3,
    DiscrepanciaTipoContenedor = 4,
    DiscrepanciaCantidad = 5,
    Robo = 6,
    Danio = 7,
    Extravio = 8
    //Localizacion = 9 
    //Violacion = 10
}

public enum TipoRelacionSolicitudTraslado
{
    None,
    Contenedor,
    Partida,
    Tarja
}

public enum TipoUnidadMedidaTemperatura
{
    None,
    Celsius,
    Fahrenheit
}

public enum TipoDesconsolidacion
{
    None,
    Total,
    Parcial
}

public enum TipoEventoSicrefisVucem
{
    None,
    IniciarDesconsolidacion,
    FinalizarDesconsolidacion,
    CancelarDesconsolidacion
}
#endregion

#region Liberaciones
public enum TipoLiberacion
{
    None,
    Normal,
    PorTransferencia,
    PorOficio,
    TransferenciaInterna,
    Transbordo
}

public enum ResultadoSeleccion
{
    None,
    ReconocimientoAduanero,
    DesaduanamientoLibreConcluido,
    Investigacion,
    ErrorSMA,
    PendienteModular
}

public enum BuscarPorLiberacion
{
    None,
    Liberacion,
    Contenedor,
    TarjaEntrada,
    TarjaSalida,
    Conocimiento,
    PatentePedimento,
    Cliente,
    ClaveAbandono,
    Numeros,
    FolioIngreso,
    FolioServicio
}

public enum TipoDocumentoSalidaLiberacion
{
    None,
    Pedimento,
    Oficio,
    CartaAta
}

public enum CondicionPago
{
    None,
    Contado,
    Credito
}

public enum TipoLiberacionExpediente
{
    None,
    ConsultaRemotaDePedimento,
    Pedimento,
    OficioDeAduana,
    BL,
    CartaDeRevalidacion,
    Articulo15,
    Articulo10
}

public enum EstadoLiberacionProceda
{
    Liberado,
    NoSeEncontro,
    EsUnError,
    EnProceso
}

#endregion

#region Monitor de Viaje

public enum TipoFechaConsultaViaje
{
    None,
    FechaArriboSalida,
    FechaFondeo,
    FechaAtraque,
    FechaInicioCargaDescarga,
    FechaFinCargaDescarga,
    FechaDesatraque
}

#endregion

#region Retención

/// <summary>
/// Tipo de Retención, Si es PAMA, debe Validarse el Número de PAMA
/// </summary>
public enum TipoRetencion
{
    None,
    PAMA,
    Decomiso,
    Retencion,
    Verficacion,
    Abandono = 10 //Para WMS
}

/// <summary>
/// Autoridad Solicitada
/// </summary>
public enum AutoridadSolicitada
{
    None,
    Aduana,
    PGR,
    AFI
}
#endregion

#region Monitor EIR

public enum TipoEir
{
    None,
    Servicio,
    Entrada,
    Salida
}
public enum TipoMercanciaEIR
{
    None,
    Contenedor,
    CargaSuelta,
    Vehiculo
}
#endregion

#region Monitor Salida

public enum TipoOperacionSalida
{
    None,
    Importacion,
    Exportacion,
    Ambas
}

public enum TipoAlmacen
{
    None,
    Seco,
    Patio,
    Friopuerto
}

public enum PosicionMercanciaFerrocarril
{
    None,
    A1,
    A2,
    B1,
    B2
}
#endregion

#region Configuracion de Consecutivos

/// <summary>
/// Enumeraciones para consecutivos del sistema
/// </summary>
public enum TipoConsecutivo
{
    None,
    Articulo23,
    AvisoDesconsolidacion,
    ConsolidacionContenedores,
    EIR,
    IngresoMercancia,
    LiberacionMercancia,
    ProgramacionServicios,
    Referencia,
    Revalidacion,
    SalidaMercancia,
    TarjaEntrada,
    TarjaSalida,
    TrasladoOrden,
    TurnoViaje,
    Preturno,
    ReferenciaBuque,
    Cotizacion
}

#endregion

#region EirContenedores

public enum TipoDanio
{
    None,
    Uso,
    Reciente,
    Origen
}

#endregion

#region Servicio

public enum CobroPorDefecto
{
    None,
    Obligatorio,
    Opcional
}

public enum TipoClasificacionServicio
{
    None,
    Almacenaje,
    Servicio,
    Entrega,
    Resguardo
}

#endregion

#region Proceda

/// <summary>
/// Indica la accion a realizar
/// </summary>
public enum TipoOperacionProceda
{
    None,
    Alta,
    Consulta,
    Cancelacion
}
#endregion

#region Tarificador

public enum TipoVariableTarificador
{
    None,
    Propiedad,
    Metodo
}

#endregion

#region Facturacion
public enum TipoOperacionFacturacion
{
    None,
    EnviadoERP,
    CanceladoERP
}

public enum ClaseItemFacturar
{
    None,
    Servicio,
    Insumo,
    Personal
}
#endregion

#region Error ERP

public enum TipoProceso
{
    None,
    NotificacionReferencia,
    NotificacionERP,
    CancelarERP
}

#endregion


#region Nombramiento
/// <summary>
/// Enumerador de los tipos de turno por nombramiento
/// </summary>
public enum TipoTurnoNombramiento
{
    None,
    Turno1,
    Turno2,
    Turno3,
    Turno4,
}

public enum MotivoAusentismo
{
    None,
    Vacaciones,
    Incapacidad,
    Descanso,
    Castigo
}
#endregion

#region WMS

public enum TipoManiobra
{
    SinIdentificar = 0,
    //Maniobras de Tipos de ingreso al almacén LCL
    Desconsolidacion = 1, //De Contenedor a almacén
    DesconsolidacionSPF = 2, //De Contenedor a piso de transporte
    EntradaDirecta = 4, //Transporte a Almacén.
    SeparacionDesdeAlmacen = 10, //Almacén - Almacén (se pasan ciertos pallets de una partida de tarja a una nueva partida de otra tarja.)
    Subdivision = 11, //Almacén - Almacén
    SeparacionDesdeContenedor = 12, // Contenedor a Transporte (se separa la mercancía del Contenedor al Transporte).

    //Maniobras de Tipos de salida del almacén LCL
    SalidaDirecta = 20, //Almacén a transporte.
    Consolidacion = 21, //Almacén a contenedor.
    ConsolidacionSPF = 22, //Contenedor a transporte.

    //Maniobras de Tipos de salida del almacén FCL
    TransferenciaVacio = 30,
    TransferenciaCargasueltaA15 = 31,

    //Maniobras en general internas
    EtiquetadoMarbeteo = 40,
    Previo = 41,
    //PrevioExpress = 42,
    Fumigacion = 43, //En RICSA, esto ocurre cuando la referencia del contenedor en SORF tiene una segunda maniobra de previo la cual es ocular.
    Ventilacion = 44,
    Traspaleo = 45,
    Almacenaje = 46,
    Reposicionamiento = 47,
    Resguardo = 48,
    Conexion = 49,
    GuardayConservacion = 50,
    Refrigeracion = 51
}

public enum EventoServicio
{
    None,
    Vaciado,
    Llenado,
    Inspeccion,
    Fumigacion
}

public enum TipoTransporteWMS
{
    None,
    Carga,
    Descarga,
    TrasladoEntrada = 10,
    TrasladoSalida = 11
}

public enum TipoMotivoDesconexionContenedor
{
    None,
    Previo,
    Salida,
    Reacomodo,
    Desconsolidado
}

public enum EstadoPlana
{
    None,
    Detenido,
    Iniciado,
    Pausa
}

public enum EstadoOperacionCALT
{
    Ninguno,
    PendienteNotificar,
    Notificado
}

public enum TipoAlmacenWMS
{
    Ninguno,
    Frio,
    LCL,
    FCL
}

public enum TipoMovimientoWMS
{
    None,
    Entrada,
    Salida,
    Servicio
}


public enum ServicioAplicado
{
    None,
    Referencia,
    Tarja,
    Inventario
}

public enum TipoBusquedaRecepcion
{
    None,
    Tarja,
    Partida
}

public enum TipoClasificacionRecepcion
{
    None,
    Todos,
    Ingresados,
    Pendientes
}

public enum TipoBusquedaEmbarque
{
    None,
    Viaje,
    TarjaSalida,
    Liberacion
}

public enum TipoFoto
{
    None,
    Recepcion,
    ReporteDannios
}

#endregion


