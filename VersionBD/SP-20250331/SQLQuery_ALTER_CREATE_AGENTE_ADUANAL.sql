


CREATE TABLE [WMS].[WMS_033_AGENTE_ADUANAL](
	[nIdAgenteAduanal033] [int] IDENTITY(1,1) NOT NULL,
	[sPatente] [nvarchar](4) NULL,
	[sNombre] [nvarchar](100) NULL,
	[sRFC] [nvarchar](100) NULL,
	[sAlias] [nvarchar](100) NULL,
	[sCURP] [nvarchar](100) NULL,
	[bEsAgenteDeCarga] [bit] NULL,
	[bActivo] [bit] NOT NULL,
	[dFechaAlta] [datetime2](7) NULL,
 CONSTRAINT [PK_WMS_033_AGENTE_ADUANAL] PRIMARY KEY CLUSTERED 
(
	[nIdAgenteAduanal033] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


