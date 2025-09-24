USE [ALOgistic]
GO

/****** Object:  Table [SLO].[transporteCronDocumentos]    Script Date: 24/09/2025 01:59:10 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [SLO].[transporteCronDocumentos](
	[IdTransporteCronDocumentos] [int] IDENTITY(1,1) NOT NULL,
	[IdSLOTransporteCron] [int] NOT NULL,
	[IdSLOSolicitudDocumentos] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
	[IdUsuarioRegistro] [int] NOT NULL,
 CONSTRAINT [PK_transporteCronDocumentos] PRIMARY KEY CLUSTERED 
(
	[IdTransporteCronDocumentos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [SLO].[transporteCronDocumentos]  WITH CHECK ADD  CONSTRAINT [FK_transporteCronDocumentos_catUsuarios_IdUsuarioRegistro] FOREIGN KEY([IdUsuarioRegistro])
REFERENCES [dbo].[catUsuarios] ([IdCatUsuarios])
GO

ALTER TABLE [SLO].[transporteCronDocumentos] CHECK CONSTRAINT [FK_transporteCronDocumentos_catUsuarios_IdUsuarioRegistro]
GO

ALTER TABLE [SLO].[transporteCronDocumentos]  WITH CHECK ADD  CONSTRAINT [FK_transporteCronDocumentos_solicitudesDocumento_IdSLOSolicitudDocumentos] FOREIGN KEY([IdSLOSolicitudDocumentos])
REFERENCES [SLO].[solicitudesDocumentos] ([IdSLOSolicitudDocumentos])
GO

ALTER TABLE [SLO].[transporteCronDocumentos] CHECK CONSTRAINT [FK_transporteCronDocumentos_solicitudesDocumento_IdSLOSolicitudDocumentos]
GO

ALTER TABLE [SLO].[transporteCronDocumentos]  WITH CHECK ADD  CONSTRAINT [FK_transporteCronDocumentos_transporteCron_IdSLOTransporteCron] FOREIGN KEY([IdSLOTransporteCron])
REFERENCES [SLO].[transporteCron] ([IdSLOTransporteCron])
GO

ALTER TABLE [SLO].[transporteCronDocumentos] CHECK CONSTRAINT [FK_transporteCronDocumentos_transporteCron_IdSLOTransporteCron]
GO

