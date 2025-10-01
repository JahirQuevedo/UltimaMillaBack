USE [ALOgistic]
GO

/****** Object:  Table [dbo].[catDocumentosLNegocio]    Script Date: 30/09/2025 06:08:42 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[catDocumentosLNegocio](
	[IdCatDocumentosLNegocio] [int] IDENTITY(1,1) NOT NULL,
	[IdCatLineaNegocio] [int] NOT NULL,
	[IdCatDocumento] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
	[IdCatUsuarios] [int] NOT NULL,
 CONSTRAINT [PK_catDocumentosLNegocio] PRIMARY KEY CLUSTERED 
(
	[IdCatDocumentosLNegocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[catDocumentosLNegocio]  WITH CHECK ADD  CONSTRAINT [FK_catDocumentosLNegocio_catDocumento] FOREIGN KEY([IdCatDocumento])
REFERENCES [dbo].[catDocumento] ([IdCatDocumento])
GO

ALTER TABLE [dbo].[catDocumentosLNegocio] CHECK CONSTRAINT [FK_catDocumentosLNegocio_catDocumento]
GO

ALTER TABLE [dbo].[catDocumentosLNegocio]  WITH CHECK ADD  CONSTRAINT [FK_catDocumentosLNegocio_catLineaNegocio] FOREIGN KEY([IdCatLineaNegocio])
REFERENCES [dbo].[catLineaNegocio] ([IdCatLineaNegocio])
GO

ALTER TABLE [dbo].[catDocumentosLNegocio] CHECK CONSTRAINT [FK_catDocumentosLNegocio_catLineaNegocio]
GO

ALTER TABLE [dbo].[catDocumentosLNegocio]  WITH CHECK ADD  CONSTRAINT [FK_catDocumentosLNegocio_catUsuarios1] FOREIGN KEY([IdCatUsuarios])
REFERENCES [dbo].[catUsuarios] ([IdCatUsuarios])
GO

ALTER TABLE [dbo].[catDocumentosLNegocio] CHECK CONSTRAINT [FK_catDocumentosLNegocio_catUsuarios1]
GO

