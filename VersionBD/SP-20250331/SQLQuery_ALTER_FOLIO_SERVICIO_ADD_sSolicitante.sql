IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'sSolicitante'
          AND Object_ID = Object_ID(N'WMS.WMS_018_FOLIO_SERVICIO'))
BEGIN

    ALTER TABLE [WMS].[WMS_018_FOLIO_SERVICIO] ADD sSolicitante VARCHAR(150) NULL;
    
END