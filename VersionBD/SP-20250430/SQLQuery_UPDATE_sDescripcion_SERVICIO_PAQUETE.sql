  UPDATE [WMS].[WMS_004_SERVICIO] 
  SET sDescripcion = 'ENTRADA ALMACEN CARGA GENERAL' WHERE sClaveServicio = 'TI001';

  UPDATE tPaq SET tPaq.sDescripcion = 'ENTRADA ALMACEN CARGA GENERAL' FROM [WMS].[WMS_004_SERVICIO] tServ
    INNER JOIN [WMS].[WMS_005_PAQUETE] tPaq
    ON tServ.nIdServicio004 = tPaq.nIdServicio004
  WHERE tServ.sClaveServicio = 'TI001';