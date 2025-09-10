IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'sGrupoViaje'
          AND Object_ID = Object_ID(N'WMS.WMS_015_TARJA'))
BEGIN

    ALTER TABLE [WMS].[WMS_015_TARJA] ADD sGrupoViaje VARCHAR(50) NULL;
    
END


IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'nConsecutivo'
          AND Object_ID = Object_ID(N'WMS.WMS_015_TARJA'))
BEGIN

    ALTER TABLE [WMS].[WMS_015_TARJA] ADD nConsecutivo INT NULL;
    
END


IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'sClaveReferencia'
          AND Object_ID = Object_ID(N'WMS.WMS_015_TARJA'))
BEGIN

    ALTER TABLE [WMS].[WMS_015_TARJA] ADD sClaveReferencia VARCHAR(150) NULL;
    
END
