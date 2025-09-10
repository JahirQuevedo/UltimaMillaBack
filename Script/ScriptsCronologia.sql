USE [ALOgistic]
GO
/****** Object:  Table [dbo].[catTipoEventosCron]    Script Date: 08/05/2025 11:10:35 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[catTipoEventosCron](
	[IdCatTipoEventoCron] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[IdRegistroUsuario] [int] NOT NULL,
 CONSTRAINT [PK_CatTipoEventosCron] PRIMARY KEY CLUSTERED 
(
	[IdCatTipoEventoCron] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[catTipoIncidenciasCron]    Script Date: 08/05/2025 11:10:35 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[catTipoIncidenciasCron](
	[IdCatTipoIncidenciaCron] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
	[IdRegistroUsuario] [int] NOT NULL,
 CONSTRAINT [CatTipoIncidenciasCron_PK] PRIMARY KEY CLUSTERED 
(
	[IdCatTipoIncidenciaCron] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[catTipoIncidenciaEvento]    Script Date: 08/05/2025 11:10:35 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[catTipoIncidenciaEvento](
	[IdCatTipoIncidenciaEvento] [int] IDENTITY(1,1) NOT NULL,
	[IdCatTipoEventoCron] [int] NOT NULL,
	[IdCatTipoIncidenciaCron] [int] NOT NULL,
	[IdUsuarioRegistro] [int] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_catTipoIncidenciaEvento_1] PRIMARY KEY CLUSTERED 
(
	[IdCatTipoIncidenciaEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Vacios].[peticionesContenedorCron]    Script Date: 08/05/2025 11:10:35 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Vacios].[peticionesContenedorCron](
	[IdContenedorCron] [int] IDENTITY(1,1) NOT NULL,
	[IdCatTipoIncidenciaEvento] [int] NOT NULL,
	[IdRegistroUsuario] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
	[Comentarios] [varchar](500) NOT NULL,
	[FechaEvento] [datetime2](7) NOT NULL,
	[IdContenedor] [int] NOT NULL,
	[IdServicio] [int] NULL,
 CONSTRAINT [PeticionesContenedorCron_PK] PRIMARY KEY CLUSTERED 
(
	[IdContenedorCron] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[catTipoEventosCron] ADD  CONSTRAINT [DF__catTipoEv__Activ__096A45D7]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[catTipoEventosCron] ADD  CONSTRAINT [DF__catTipoEv__Fecha__0A5E6A10]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[catTipoEventosCron] ADD  CONSTRAINT [DF_catTipoEventosCron_IdRegistroUsuario]  DEFAULT ((1)) FOR [IdRegistroUsuario]
GO
ALTER TABLE [dbo].[catTipoIncidenciaEvento] ADD  CONSTRAINT [DF_catTipoIncidenciaEvento_IdUsuarioRegistro]  DEFAULT ((1)) FOR [IdUsuarioRegistro]
GO
ALTER TABLE [dbo].[catTipoIncidenciaEvento] ADD  CONSTRAINT [DF_catTipoIncidenciaEvento_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[catTipoIncidenciasCron] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[catTipoIncidenciasCron] ADD  CONSTRAINT [DF_catTipoIncidenciasCron_IdRegistroUsuario]  DEFAULT ((1)) FOR [IdRegistroUsuario]
GO
ALTER TABLE [Vacios].[peticionesContenedorCron] ADD  CONSTRAINT [DF__peticione__Activ__42A2C333]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Vacios].[peticionesContenedorCron] ADD  CONSTRAINT [DF__peticione__Fecha__4396E76C]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[catTipoEventosCron]  WITH CHECK ADD  CONSTRAINT [FK_catTipoEventosCron.IdRegistroUsuario_catUsuarios_IdUsuarios] FOREIGN KEY([IdRegistroUsuario])
REFERENCES [dbo].[catUsuarios] ([IdCatUsuarios])
GO
ALTER TABLE [dbo].[catTipoEventosCron] CHECK CONSTRAINT [FK_catTipoEventosCron.IdRegistroUsuario_catUsuarios_IdUsuarios]
GO
ALTER TABLE [dbo].[catTipoIncidenciaEvento]  WITH CHECK ADD  CONSTRAINT [FK_catTipoIncidenciaEvento_catTipoEventoCron_IdCatTipoEventoCron] FOREIGN KEY([IdCatTipoEventoCron])
REFERENCES [dbo].[catTipoEventosCron] ([IdCatTipoEventoCron])
GO
ALTER TABLE [dbo].[catTipoIncidenciaEvento] CHECK CONSTRAINT [FK_catTipoIncidenciaEvento_catTipoEventoCron_IdCatTipoEventoCron]
GO
ALTER TABLE [dbo].[catTipoIncidenciaEvento]  WITH CHECK ADD  CONSTRAINT [FK_catTipoIncidenciaEvento_catTipoIncidenciaCron.IdCatTipoIncidenciaCron] FOREIGN KEY([IdCatTipoIncidenciaCron])
REFERENCES [dbo].[catTipoIncidenciasCron] ([IdCatTipoIncidenciaCron])
GO
ALTER TABLE [dbo].[catTipoIncidenciaEvento] CHECK CONSTRAINT [FK_catTipoIncidenciaEvento_catTipoIncidenciaCron.IdCatTipoIncidenciaCron]
GO
ALTER TABLE [dbo].[catTipoIncidenciasCron]  WITH CHECK ADD  CONSTRAINT [FK_catTipoIncidenciasCron.IdRegistroUsuario_catUsuarios_IdcatUsuarios] FOREIGN KEY([IdRegistroUsuario])
REFERENCES [dbo].[catUsuarios] ([IdCatUsuarios])
GO
ALTER TABLE [dbo].[catTipoIncidenciasCron] CHECK CONSTRAINT [FK_catTipoIncidenciasCron.IdRegistroUsuario_catUsuarios_IdcatUsuarios]
GO
ALTER TABLE [Vacios].[peticionesContenedorCron]  WITH CHECK ADD  CONSTRAINT [FK_peticionesContenedorCron.catTipoIncidenciaEvento_IdCatTipoIncidenciaEvento] FOREIGN KEY([IdCatTipoIncidenciaEvento])
REFERENCES [dbo].[catTipoIncidenciaEvento] ([IdCatTipoIncidenciaEvento])
GO
ALTER TABLE [Vacios].[peticionesContenedorCron] CHECK CONSTRAINT [FK_peticionesContenedorCron.catTipoIncidenciaEvento_IdCatTipoIncidenciaEvento]
GO
ALTER TABLE [Vacios].[peticionesContenedorCron]  WITH CHECK ADD  CONSTRAINT [FK_peticionesContenedorCron.IdRegistroUsuario_catUsuaros_IdcatUsuarios] FOREIGN KEY([IdRegistroUsuario])
REFERENCES [dbo].[catUsuarios] ([IdCatUsuarios])
GO
ALTER TABLE [Vacios].[peticionesContenedorCron] CHECK CONSTRAINT [FK_peticionesContenedorCron.IdRegistroUsuario_catUsuaros_IdcatUsuarios]
GO
ALTER TABLE [Vacios].[peticionesContenedorCron]  WITH CHECK ADD  CONSTRAINT [FK_peticionesContenedorCron_peticionesContenedorCron] FOREIGN KEY([IdContenedorCron])
REFERENCES [Vacios].[peticionesContenedorCron] ([IdContenedorCron])
GO
ALTER TABLE [Vacios].[peticionesContenedorCron] CHECK CONSTRAINT [FK_peticionesContenedorCron_peticionesContenedorCron]
GO
ALTER TABLE [Vacios].[peticionesContenedorCron]  WITH CHECK ADD  CONSTRAINT [PeticionesContenedorCron_peticionesContenedores_FK] FOREIGN KEY([IdContenedor])
REFERENCES [dbo].[peticionesContenedores] ([IdContenedor])
GO
ALTER TABLE [Vacios].[peticionesContenedorCron] CHECK CONSTRAINT [PeticionesContenedorCron_peticionesContenedores_FK]
GO
ALTER TABLE [Vacios].[peticionesContenedorCron]  WITH CHECK ADD  CONSTRAINT [peticionesContenedorCron_peticionesServicios_FK] FOREIGN KEY([IdServicio])
REFERENCES [dbo].[peticionesServicios] ([IdServicio])
GO
ALTER TABLE [Vacios].[peticionesContenedorCron] CHECK CONSTRAINT [peticionesContenedorCron_peticionesServicios_FK]
GO
