USE [ALOgistic]
GO

/****** Object:  Table [dbo].[catTipoOperacionesSLO]    Script Date: 24/09/2025 01:44:31 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[catTipoOperacionesSLO](
	[IdCatTipoOperacionesSLO] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[IdCatUsuarios] [int] NOT NULL,
 CONSTRAINT [PK_catTipoOperacionesSLO] PRIMARY KEY CLUSTERED 
(
	[IdCatTipoOperacionesSLO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

