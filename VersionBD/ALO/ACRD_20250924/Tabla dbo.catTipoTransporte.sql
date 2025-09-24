USE [ALOgistic]
GO

/****** Object:  Table [dbo].[catTipoTransporte]    Script Date: 24/09/2025 01:50:57 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[catTipoTransporte](
	[IdCatTipoTransporte] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[IdCatUsuarios] [int] NOT NULL,
 CONSTRAINT [PK_catTipoTransporte] PRIMARY KEY CLUSTERED 
(
	[IdCatTipoTransporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[catTipoTransporte]  WITH CHECK ADD  CONSTRAINT [catTipoTransporte_catUsuarios_FK] FOREIGN KEY([IdCatUsuarios])
REFERENCES [dbo].[catUsuarios] ([IdCatUsuarios])
GO

ALTER TABLE [dbo].[catTipoTransporte] CHECK CONSTRAINT [catTipoTransporte_catUsuarios_FK]
GO

