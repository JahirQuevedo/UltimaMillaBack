USE [ALOgistic]
GO

/****** Object:  Table [dbo].[catTipoOperacionesTransportes]    Script Date: 24/09/2025 01:45:54 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[catTipoOperacionesTransportes](
	[IdCatTipoOperTransportes] [int] IDENTITY(1,1) NOT NULL,
	[IdCatTipoOperacionesSLO] [int] NOT NULL,
	[IdCatTipoTransporte] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[IdCatUsuarios] [int] NOT NULL,
 CONSTRAINT [PK_catTipoOperacionesTransportes] PRIMARY KEY CLUSTERED 
(
	[IdCatTipoOperTransportes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[catTipoOperacionesTransportes]  WITH CHECK ADD  CONSTRAINT [FK_catTipoOperacionesTransportes_catTipoOperacionesSLO_IdCatTipoOperacionesSLO] FOREIGN KEY([IdCatTipoOperacionesSLO])
REFERENCES [dbo].[catTipoOperacionesSLO] ([IdCatTipoOperacionesSLO])
GO

ALTER TABLE [dbo].[catTipoOperacionesTransportes] CHECK CONSTRAINT [FK_catTipoOperacionesTransportes_catTipoOperacionesSLO_IdCatTipoOperacionesSLO]
GO

ALTER TABLE [dbo].[catTipoOperacionesTransportes]  WITH CHECK ADD  CONSTRAINT [FK_catTipoOperacionesTransportes_catTipoTransporte_IdCatTipoTransporte] FOREIGN KEY([IdCatTipoTransporte])
REFERENCES [dbo].[catTipoTransporte] ([IdCatTipoTransporte])
GO

ALTER TABLE [dbo].[catTipoOperacionesTransportes] CHECK CONSTRAINT [FK_catTipoOperacionesTransportes_catTipoTransporte_IdCatTipoTransporte]
GO

ALTER TABLE [dbo].[catTipoOperacionesTransportes]  WITH CHECK ADD  CONSTRAINT [FK_catTipoOperacionesTransportes_catUsuarios_IdCatUsuarios] FOREIGN KEY([IdCatUsuarios])
REFERENCES [dbo].[catUsuarios] ([IdCatUsuarios])
GO

ALTER TABLE [dbo].[catTipoOperacionesTransportes] CHECK CONSTRAINT [FK_catTipoOperacionesTransportes_catUsuarios_IdCatUsuarios]
GO

