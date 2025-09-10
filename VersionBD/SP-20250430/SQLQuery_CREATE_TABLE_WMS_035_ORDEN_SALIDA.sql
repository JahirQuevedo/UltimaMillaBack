
GO

/****** Object:  Table [WMS].[WMS_035_ORDEN_SALIDA]    Script Date: 06/04/2025 15:57:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [WMS].[WMS_035_ORDEN_SALIDA](
	[nIdOrdenSalida035] [int] IDENTITY(1,1) NOT NULL,
	[nFolio] [int] NOT NULL,
	[nTipo] [int] NOT NULL,
	[nEstado] [int] NOT NULL,
	[sObservaciones] [nvarchar](4000) NULL,
	[dFechaSalidaProgramada] [datetime2](7) NULL,
	[dFechaAutorizacion] [datetime2](7) NULL,
	[dFechaLiberacion] [datetime2](7) NULL,
	[sMotivoCancelacion] [nvarchar](4000) NULL,
	[nIdCatEmpresa] [int] NOT NULL,
	[dFechaAlta] [datetime2](7) NULL,
 CONSTRAINT [PK_WMS_035_ORDEN_SALIDA] PRIMARY KEY CLUSTERED 
(
	[nIdOrdenSalida035] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [WMS].[WMS_035_ORDEN_SALIDA]  WITH CHECK ADD  CONSTRAINT [FK_WMS_035_ORDEN_SALIDA_CatEmpresas_nIdCatEmpresa] FOREIGN KEY([nIdCatEmpresa])
REFERENCES [dbo].[catEmpresas] ([IdCatEmpresa])
ON DELETE CASCADE
GO

ALTER TABLE [WMS].[WMS_035_ORDEN_SALIDA] CHECK CONSTRAINT [FK_WMS_035_ORDEN_SALIDA_CatEmpresas_nIdCatEmpresa]
GO


