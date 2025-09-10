USE [ALOgistic]
GO
/****** Object:  Schema [Solicitudes]    Script Date: 09/05/2025 08:13:13 p. m. ******/
CREATE SCHEMA [Solicitudes]
GO
/****** Object:  Table [dbo].[catTipoContenedor]   Script Date: 09/05/2025 08:13:13 p. m. ******/
--DROP TABLE [dbo].[catTipoContenedor]
GO
/****** Object:  Table [dbo].[catTiposOperaciones]    Script Date: 09/05/2025 08:13:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[catTiposOperaciones](
	[IdCatTipoOperacion] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](200) NOT NULL,
	[Acronimo] [nvarchar](200) NOT NULL,
	[Descripcion] [nvarchar](1000) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
	[IdCatEmpresa] [int] NOT NULL,
	[IdCatLineaNegocio] [int] NOT NULL,
	[IdUsuarioRegistro] [int] NOT NULL,
 CONSTRAINT [PK_catTiposOperaciones] PRIMARY KEY CLUSTERED 
(
	[IdCatTipoOperacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[catTiposContenedor]    Script Date: 09/05/2025 08:19:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[catTiposContenedor](
	[IdCatTipoContenedor] [int] IDENTITY(1,1) NOT NULL,
	[Nomenclatura] [nvarchar](10) NOT NULL,
	[Descripcion] [nvarchar](200) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
	[IdUsuarioRegistro] [int] NOT NULL,
 CONSTRAINT [PK_catContenedor] PRIMARY KEY CLUSTERED 
(
	[IdCatTipoContenedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[catTiposContenedor] ON 
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (1, N'20DC', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (2, N'40HC', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (3, N'40RH', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (4, N'DC20', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (5, N'DC40', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (6, N'FL20', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (7, N'FL40', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (8, N'HC40', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (9, N'HR40', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (10, N'OT20', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (11, N'OT40', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (12, N'OT60', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (13, N'PC40', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (14, N'RF20', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (15, N'RF40', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
INSERT [dbo].[catTiposContenedor] ([IdCatTipoContenedor], [Nomenclatura], [Descripcion], [Activo], [FechaRegistro], [IdUsuarioRegistro]) VALUES (16, N'RH40', N'', 1, CAST(N'2025-05-03T18:31:53.7333333' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[catTiposContenedor] OFF
GO
ALTER TABLE [dbo].[catTiposContenedor]  WITH CHECK ADD  CONSTRAINT [FK_catTiposContenedor_catUsuarios_IdUsuarioRegistro] FOREIGN KEY([IdUsuarioRegistro])
REFERENCES [dbo].[catUsuarios] ([IdCatUsuarios])
GO
ALTER TABLE [dbo].[catTiposContenedor] CHECK CONSTRAINT [FK_catTiposContenedor_catUsuarios_IdUsuarioRegistro]
GO
ALTER TABLE peticionesContenedores
ADD IdCatTipoContenedor INT NULL;
GO
ALTER TABLE peticionesContenedores
ADD CONSTRAINT FK_peticionesContenedores_catTiposContenedor_IdCatTipoContenedor
FOREIGN KEY (IdCatTipoContenedor)
REFERENCES catTiposContenedor (IdCatTipoContenedor);
GO
ALTER TABLE peticionesServicios
ADD ReferenciaClienteFacturar nvarchar(50) NULL;
GO
UPDATE catSistemas
SET rol = 'SISTEMA'
GO
UPDATE catSistemas
SET IdCatCliente = 189
WHERE IdCatSistema = 2