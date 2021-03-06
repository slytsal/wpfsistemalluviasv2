USE [db_SeguimientoProtocolo_r2_v2]
GO
/****** Object:  Table [dbo].[TMP_CI_REGISTRO_RECURRENTE]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TMP_CI_REGISTRO_RECURRENTE](
	[IdRegistro] [bigint] NOT NULL,
	[IdPuntoMedicion] [bigint] NOT NULL,
	[FechaCaptura] [datetime] NOT NULL,
	[HoraRegistro] [int] NOT NULL,
	[DiaRegistro] [int] NOT NULL,
	[Valor] [float] NOT NULL,
	[AccionActual] [nvarchar](1000) NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[IdCondicion] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
	[FechaNumerica] [bigint] NULL,
 CONSTRAINT [TMP_CI_REGISTRO_RECURRENTE_PK] PRIMARY KEY CLUSTERED 
(
	[IdRegistro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TMP_CI_REGISTRO_ONDEMAND]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TMP_CI_REGISTRO_ONDEMAND](
	[IdRegistro] [bigint] NOT NULL,
	[IdPuntoMedicion] [bigint] NOT NULL,
	[FechaCaptura] [datetime] NOT NULL,
	[HoraRegistro] [int] NOT NULL,
	[DiaRegistro] [int] NOT NULL,
	[Valor] [float] NOT NULL,
	[AccionActual] [nvarchar](1000) NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[IdCondicion] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
	[FechaNumerica] [bigint] NULL,
 CONSTRAINT [TMP_CI_REGISTRO_ONDEMAND_PK] PRIMARY KEY CLUSTERED 
(
	[IdRegistro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TMP_CI_REGISTRO_CONFIRMATION]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TMP_CI_REGISTRO_CONFIRMATION](
	[IdRegistro] [bigint] NOT NULL,
	[IdPuntoMedicion] [bigint] NOT NULL,
	[FechaNumerica] [bigint] NOT NULL,
	[LastModifiedDate] [bigint] NULL,
	[ServerLastModifiedDate] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRegistro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SYNCTABLE]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYNCTABLE](
	[IdSyncTable] [bigint] NOT NULL,
	[SyncTableName] [nvarchar](max) NULL,
	[OrderTable] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSyncTable] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_SERVER_LASTDATA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_SERVER_LASTDATA](
	[IdServerLastData] [bigint] NOT NULL,
	[ActualDate] [bigint] NOT NULL,
 CONSTRAINT [CAT_SERVER_LASTDATA_PK] PRIMARY KEY CLUSTERED 
(
	[IdServerLastData] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CI_TRACKING]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CI_TRACKING](
	[IdTracking] [bigint] NOT NULL,
	[Accion] [nvarchar](500) NULL,
	[Valor] [nvarchar](max) NULL,
	[Ip] [nvarchar](20) NULL,
	[Equipo] [nvarchar](255) NULL,
	[Ubicacion] [nvarchar](1000) NULL,
	[IdUsuario] [bigint] NULL,
	[ServerLastModifiedDate] [bigint] NULL,
	[LastModifiedDate] [bigint] NULL,
	[IsModified] [bit] NOT NULL,
	[IdPuntoMedicion] [bigint] NOT NULL,
	[FechaNumerica] [bigint] NOT NULL,
 CONSTRAINT [PK__CI_TRACK__FDC1E65C5FB337D6] PRIMARY KEY CLUSTERED 
(
	[IdTracking] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_LINKS]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_LINKS](
	[IdLink] [bigint] NOT NULL,
	[LinkUrl] [nvarchar](500) NULL,
	[LinkName] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLink] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_UNIDAD_MEDIDA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_UNIDAD_MEDIDA](
	[IdUnidadMedida] [bigint] NOT NULL,
	[UnidadMedidaName] [nvarchar](250) NOT NULL,
	[UnidadMedidaShort] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [CAT_UNIDAD_MEDIDA_PK] PRIMARY KEY CLUSTERED 
(
	[IdUnidadMedida] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_TIPO_PUNTO_MEDICION]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_TIPO_PUNTO_MEDICION](
	[IdTipoPuntoMedicion] [bigint] NOT NULL,
	[TipoPuntoMedicionName] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [CAT_TIPO_PUNTO_MEDICION_PK] PRIMARY KEY CLUSTERED 
(
	[IdTipoPuntoMedicion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_SYNC_LOG]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_SYNC_LOG](
	[IdSyncLog] [bigint] NOT NULL,
	[FechaSinc] [datetime] NULL,
	[Hora] [nvarchar](20) NULL,
	[Menssage] [nvarchar](500) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_CAT_SYNC_LOG] PRIMARY KEY CLUSTERED 
(
	[IdSyncLog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_SYNC]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_SYNC](
	[IdSycn] [bigint] NOT NULL,
	[ActualDate] [bigint] NOT NULL,
 CONSTRAINT [CAT_SYNC_PK] PRIMARY KEY CLUSTERED 
(
	[IdSycn] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_SISTEMA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_SISTEMA](
	[IdSistema] [bigint] NOT NULL,
	[SistemaName] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [CAT_SISTEMA_PK] PRIMARY KEY CLUSTERED 
(
	[IdSistema] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APP_USUARIO]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APP_USUARIO](
	[IdUsuario] [bigint] NOT NULL,
	[UsuarioCorreo] [nvarchar](250) NULL,
	[UsuarioPwd] [varbinary](500) NULL,
	[Nombre] [nvarchar](250) NOT NULL,
	[Apellido] [nvarchar](250) NOT NULL,
	[Area] [nvarchar](250) NULL,
	[Puesto] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[IsNewUser] [bit] NOT NULL,
	[IsVerified] [bit] NOT NULL,
	[IsMailSent] [bit] NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [APP_USUARIO_PK] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[APP_SETTINGS]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APP_SETTINGS](
	[IdSettings] [bigint] NOT NULL,
	[SettingName] [nvarchar](250) NULL,
	[Value] [nvarchar](250) NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSettings] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APP_ROL]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APP_ROL](
	[IdRol] [bigint] NOT NULL,
	[RolName] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [APP_ROL_PK] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APP_BITACORA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APP_BITACORA](
	[IdBitacora] [bigint] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Metodo] [nvarchar](1000) NULL,
	[Mensaje] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBitacora] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_DEPENDENCIA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_DEPENDENCIA](
	[IdDependencia] [bigint] NOT NULL,
	[DependenciaName] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [CAT_DEPENDENCIA_PK] PRIMARY KEY CLUSTERED 
(
	[IdDependencia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_CONDPRO]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_CONDPRO](
	[IdCondicion] [bigint] NOT NULL,
	[CondicionName] [nvarchar](250) NOT NULL,
	[PathCodicion] [nvarchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [CAT_CONDPRO_PK] PRIMARY KEY CLUSTERED 
(
	[IdCondicion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_AGRUPADOR_ISOYETA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_AGRUPADOR_ISOYETA](
	[IdAgrupadorIsoyeta] [bigint] NOT NULL,
	[AgrupadorIsoyetaName] [nvarchar](15) NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAgrupadorIsoyeta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_ACCION_ACTUAL]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_ACCION_ACTUAL](
	[IdAccionActual] [bigint] IDENTITY(1,1) NOT NULL,
	[AccionAcualName] [nvarchar](255) NOT NULL,
	[IsModified] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAccionActual] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APP_USUARIO_ROL]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APP_USUARIO_ROL](
	[IdUsuario] [bigint] NOT NULL,
	[IdRol] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [APP_USUARIO_ROL_PK] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdRol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_SESION]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_SESION](
	[IdSesion] [bigint] NOT NULL,
	[IdUsuario] [bigint] NULL,
	[IsSaveSesion] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSesion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_PUNTO_MEDICION]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_PUNTO_MEDICION](
	[IdPuntoMedicion] [bigint] NOT NULL,
	[PuntoMedicionName] [nvarchar](250) NOT NULL,
	[IdUnidadMedida] [bigint] NOT NULL,
	[IdTipoPuntoMedicion] [bigint] NOT NULL,
	[ValorReferencia] [float] NULL,
	[ParametroReferencia] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[Visibility] [bit] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
	[vAccion] [bit] NULL,
	[vCondicion] [bit] NULL,
	[latiitud] [float] NULL,
	[longitud] [float] NULL,
	[IdAccionActual] [bigint] NULL,
 CONSTRAINT [CAT_PUNTO_MEDICION_PK] PRIMARY KEY CLUSTERED 
(
	[IdPuntoMedicion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_ESTRUCTURA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_ESTRUCTURA](
	[IdEstructura] [bigint] NOT NULL,
	[EstructuraName] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[IdSistema] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [CAT_ESTRUCTURA_PK] PRIMARY KEY CLUSTERED 
(
	[IdEstructura] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MODIFIEDDATA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MODIFIEDDATA](
	[IdModifiedData] [bigint] NOT NULL,
	[IdSyncTable] [bigint] NULL,
	[IsModified] [bit] NULL,
	[ServerModifiedDate] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdModifiedData] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_UPLOAD_LOG]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_UPLOAD_LOG](
	[IdUploadLog] [bigint] NOT NULL,
	[Msg] [nvarchar](512) NOT NULL,
	[IpDir] [nvarchar](512) NOT NULL,
	[PcName] [nvarchar](512) NOT NULL,
	[IdUsuario] [bigint] NULL,
 CONSTRAINT [CAT_UPLOAD_LOG_PK] PRIMARY KEY CLUSTERED 
(
	[IdUploadLog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REL_ESTRUCTURA_DEPENDENCIA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REL_ESTRUCTURA_DEPENDENCIA](
	[IdEstructuraDependencia] [bigint] NOT NULL,
	[IdDependencia] [bigint] NOT NULL,
	[IdEstructura] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [REL_ESTRUCTURA_DEPENDENCIA_PK] PRIMARY KEY CLUSTERED 
(
	[IdEstructuraDependencia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REL_EST_PUNTOMED]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REL_EST_PUNTOMED](
	[IdEstPuntoMed] [bigint] NOT NULL,
	[IdEstructura] [bigint] NOT NULL,
	[IdPuntoMedicion] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
 CONSTRAINT [REL_EST_PUNTOMED_PK] PRIMARY KEY CLUSTERED 
(
	[IdEstPuntoMed] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CI_REGISTRO]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CI_REGISTRO](
	[IdRegistro] [bigint] NOT NULL,
	[IdPuntoMedicion] [bigint] NOT NULL,
	[FechaCaptura] [datetime] NOT NULL,
	[HoraRegistro] [int] NOT NULL,
	[DiaRegistro] [int] NOT NULL,
	[Valor] [float] NOT NULL,
	[AccionActual] [nvarchar](1000) NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[IdCondicion] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
	[FechaNumerica] [bigint] NULL,
 CONSTRAINT [CI_REGISTRO_PK] PRIMARY KEY CLUSTERED 
(
	[IdRegistro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_PUNTOS_MEDICION_SHORTNAME]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CAT_PUNTOS_MEDICION_SHORTNAME](
	[IdShortName] [bigint] IDENTITY(1,1) NOT NULL,
	[IdPuntoMedicion] [bigint] NULL,
	[ShortName] [varchar](1000) NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdShortName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CAT_PUNTO_MEDICION_MAX_MIN]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_PUNTO_MEDICION_MAX_MIN](
	[IdPuntoMedicionMaxMin] [bigint] NOT NULL,
	[IdPuntoMedicion] [bigint] NOT NULL,
	[Max] [float] NOT NULL,
	[Min] [float] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
	[LastModifiedDate] [bigint] NULL,
	[IsModified] [bit] NULL,
 CONSTRAINT [PK_CAT_PUNTO_MEDICION_MAX_MIN] PRIMARY KEY CLUSTERED 
(
	[IdPuntoMedicionMaxMin] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_PROTOCOLO]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_PROTOCOLO](
	[IdProtocolo] [bigint] IDENTITY(1,1) NOT NULL,
	[IdPuntoMedicion] [bigint] NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProtocolo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_OPERACION_ESTRUCTURA]    Script Date: 04/29/2014 14:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_OPERACION_ESTRUCTURA](
	[IdOperacionEstructura] [bigint] NOT NULL,
	[IdCondicion] [bigint] NOT NULL,
	[IdEstructura] [bigint] NOT NULL,
	[OperacionEstrucuturaName] [nvarchar](1500) NULL,
	[IsActive] [bit] NOT NULL,
	[IsModified] [bit] NOT NULL,
	[LastModifiedDate] [bigint] NOT NULL,
	[ServerLastModifiedDate] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOperacionEstructura] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_CAT_PUNTO_MEDICION_Visibility]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_PUNTO_MEDICION] ADD  CONSTRAINT [DF_CAT_PUNTO_MEDICION_Visibility]  DEFAULT ((1)) FOR [Visibility]
GO
/****** Object:  ForeignKey [APP_ROL_APP_USUARIO_ROL_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[APP_USUARIO_ROL]  WITH CHECK ADD  CONSTRAINT [APP_ROL_APP_USUARIO_ROL_FK1] FOREIGN KEY([IdRol])
REFERENCES [dbo].[APP_ROL] ([IdRol])
GO
ALTER TABLE [dbo].[APP_USUARIO_ROL] CHECK CONSTRAINT [APP_ROL_APP_USUARIO_ROL_FK1]
GO
/****** Object:  ForeignKey [APP_USUARIO_APP_USUARIO_ROL_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[APP_USUARIO_ROL]  WITH CHECK ADD  CONSTRAINT [APP_USUARIO_APP_USUARIO_ROL_FK1] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[APP_USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[APP_USUARIO_ROL] CHECK CONSTRAINT [APP_USUARIO_APP_USUARIO_ROL_FK1]
GO
/****** Object:  ForeignKey [CAT_SISTEMA_CAT_ESTRUCTURA_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_ESTRUCTURA]  WITH CHECK ADD  CONSTRAINT [CAT_SISTEMA_CAT_ESTRUCTURA_FK1] FOREIGN KEY([IdSistema])
REFERENCES [dbo].[CAT_SISTEMA] ([IdSistema])
GO
ALTER TABLE [dbo].[CAT_ESTRUCTURA] CHECK CONSTRAINT [CAT_SISTEMA_CAT_ESTRUCTURA_FK1]
GO
/****** Object:  ForeignKey [FK_CAT_OPERACION_ESTRUCTURA_CAT_CONDPRO]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_OPERACION_ESTRUCTURA]  WITH CHECK ADD  CONSTRAINT [FK_CAT_OPERACION_ESTRUCTURA_CAT_CONDPRO] FOREIGN KEY([IdCondicion])
REFERENCES [dbo].[CAT_CONDPRO] ([IdCondicion])
GO
ALTER TABLE [dbo].[CAT_OPERACION_ESTRUCTURA] CHECK CONSTRAINT [FK_CAT_OPERACION_ESTRUCTURA_CAT_CONDPRO]
GO
/****** Object:  ForeignKey [FK_CAT_OPERACION_ESTRUCTURA_CAT_ESTRUCTURA]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_OPERACION_ESTRUCTURA]  WITH CHECK ADD  CONSTRAINT [FK_CAT_OPERACION_ESTRUCTURA_CAT_ESTRUCTURA] FOREIGN KEY([IdEstructura])
REFERENCES [dbo].[CAT_ESTRUCTURA] ([IdEstructura])
GO
ALTER TABLE [dbo].[CAT_OPERACION_ESTRUCTURA] CHECK CONSTRAINT [FK_CAT_OPERACION_ESTRUCTURA_CAT_ESTRUCTURA]
GO
/****** Object:  ForeignKey [FK__CAT_PROTO__IdPun__339FAB6E]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_PROTOCOLO]  WITH CHECK ADD FOREIGN KEY([IdPuntoMedicion])
REFERENCES [dbo].[CAT_PUNTO_MEDICION] ([IdPuntoMedicion])
GO
/****** Object:  ForeignKey [CAT_TIPO_PUNTO_MEDICION_CAT_PUNTO_MEDICION_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_PUNTO_MEDICION]  WITH CHECK ADD  CONSTRAINT [CAT_TIPO_PUNTO_MEDICION_CAT_PUNTO_MEDICION_FK1] FOREIGN KEY([IdTipoPuntoMedicion])
REFERENCES [dbo].[CAT_TIPO_PUNTO_MEDICION] ([IdTipoPuntoMedicion])
GO
ALTER TABLE [dbo].[CAT_PUNTO_MEDICION] CHECK CONSTRAINT [CAT_TIPO_PUNTO_MEDICION_CAT_PUNTO_MEDICION_FK1]
GO
/****** Object:  ForeignKey [CAT_UNIDAD_MEDIDA_CAT_PUNTO_MEDICION_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_PUNTO_MEDICION]  WITH CHECK ADD  CONSTRAINT [CAT_UNIDAD_MEDIDA_CAT_PUNTO_MEDICION_FK1] FOREIGN KEY([IdUnidadMedida])
REFERENCES [dbo].[CAT_UNIDAD_MEDIDA] ([IdUnidadMedida])
GO
ALTER TABLE [dbo].[CAT_PUNTO_MEDICION] CHECK CONSTRAINT [CAT_UNIDAD_MEDIDA_CAT_PUNTO_MEDICION_FK1]
GO
/****** Object:  ForeignKey [FK__CAT_PUNTO__IdAcc__1EA48E88]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_PUNTO_MEDICION]  WITH CHECK ADD FOREIGN KEY([IdAccionActual])
REFERENCES [dbo].[CAT_ACCION_ACTUAL] ([IdAccionActual])
GO
/****** Object:  ForeignKey [FK_CAT_PUNTO_MEDICION_MAX_MIN_CAT_PUNTO_MEDICION]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_PUNTO_MEDICION_MAX_MIN]  WITH CHECK ADD  CONSTRAINT [FK_CAT_PUNTO_MEDICION_MAX_MIN_CAT_PUNTO_MEDICION] FOREIGN KEY([IdPuntoMedicion])
REFERENCES [dbo].[CAT_PUNTO_MEDICION] ([IdPuntoMedicion])
GO
ALTER TABLE [dbo].[CAT_PUNTO_MEDICION_MAX_MIN] CHECK CONSTRAINT [FK_CAT_PUNTO_MEDICION_MAX_MIN_CAT_PUNTO_MEDICION]
GO
/****** Object:  ForeignKey [FK__CAT_PUNTO__IdPun__540C7B00]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_PUNTOS_MEDICION_SHORTNAME]  WITH CHECK ADD FOREIGN KEY([IdPuntoMedicion])
REFERENCES [dbo].[CAT_PUNTO_MEDICION] ([IdPuntoMedicion])
GO
/****** Object:  ForeignKey [FK__CAT_SESIO__IdUsu__4D94879B]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_SESION]  WITH CHECK ADD  CONSTRAINT [FK__CAT_SESIO__IdUsu__4D94879B] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[APP_USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[CAT_SESION] CHECK CONSTRAINT [FK__CAT_SESIO__IdUsu__4D94879B]
GO
/****** Object:  ForeignKey [APP_USUARIO_CAT_UPLOAD_LOG_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CAT_UPLOAD_LOG]  WITH CHECK ADD  CONSTRAINT [APP_USUARIO_CAT_UPLOAD_LOG_FK1] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[APP_USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[CAT_UPLOAD_LOG] CHECK CONSTRAINT [APP_USUARIO_CAT_UPLOAD_LOG_FK1]
GO
/****** Object:  ForeignKey [CAT_CONDPRO_CI_REGISTRO_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CI_REGISTRO]  WITH CHECK ADD  CONSTRAINT [CAT_CONDPRO_CI_REGISTRO_FK1] FOREIGN KEY([IdCondicion])
REFERENCES [dbo].[CAT_CONDPRO] ([IdCondicion])
GO
ALTER TABLE [dbo].[CI_REGISTRO] CHECK CONSTRAINT [CAT_CONDPRO_CI_REGISTRO_FK1]
GO
/****** Object:  ForeignKey [CAT_PUNTO_MEDICION_CI_REGISTRO_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[CI_REGISTRO]  WITH CHECK ADD  CONSTRAINT [CAT_PUNTO_MEDICION_CI_REGISTRO_FK1] FOREIGN KEY([IdPuntoMedicion])
REFERENCES [dbo].[CAT_PUNTO_MEDICION] ([IdPuntoMedicion])
GO
ALTER TABLE [dbo].[CI_REGISTRO] CHECK CONSTRAINT [CAT_PUNTO_MEDICION_CI_REGISTRO_FK1]
GO
/****** Object:  ForeignKey [FK__MODIFIEDD__IdSyn__6B24EA82]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[MODIFIEDDATA]  WITH CHECK ADD FOREIGN KEY([IdSyncTable])
REFERENCES [dbo].[SYNCTABLE] ([IdSyncTable])
GO
/****** Object:  ForeignKey [CAT_ESTRUCTURA_REL_EST_PUNTOMED_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[REL_EST_PUNTOMED]  WITH CHECK ADD  CONSTRAINT [CAT_ESTRUCTURA_REL_EST_PUNTOMED_FK1] FOREIGN KEY([IdEstructura])
REFERENCES [dbo].[CAT_ESTRUCTURA] ([IdEstructura])
GO
ALTER TABLE [dbo].[REL_EST_PUNTOMED] CHECK CONSTRAINT [CAT_ESTRUCTURA_REL_EST_PUNTOMED_FK1]
GO
/****** Object:  ForeignKey [CAT_PUNTO_MEDICION_REL_EST_PUNTOMED_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[REL_EST_PUNTOMED]  WITH CHECK ADD  CONSTRAINT [CAT_PUNTO_MEDICION_REL_EST_PUNTOMED_FK1] FOREIGN KEY([IdPuntoMedicion])
REFERENCES [dbo].[CAT_PUNTO_MEDICION] ([IdPuntoMedicion])
GO
ALTER TABLE [dbo].[REL_EST_PUNTOMED] CHECK CONSTRAINT [CAT_PUNTO_MEDICION_REL_EST_PUNTOMED_FK1]
GO
/****** Object:  ForeignKey [CAT_DEPENDENCIA_REL_ESTRUCTURA_DEPENDENCIA_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[REL_ESTRUCTURA_DEPENDENCIA]  WITH CHECK ADD  CONSTRAINT [CAT_DEPENDENCIA_REL_ESTRUCTURA_DEPENDENCIA_FK1] FOREIGN KEY([IdDependencia])
REFERENCES [dbo].[CAT_DEPENDENCIA] ([IdDependencia])
GO
ALTER TABLE [dbo].[REL_ESTRUCTURA_DEPENDENCIA] CHECK CONSTRAINT [CAT_DEPENDENCIA_REL_ESTRUCTURA_DEPENDENCIA_FK1]
GO
/****** Object:  ForeignKey [CAT_ESTRUCTURA_REL_ESTRUCTURA_DEPENDENCIA_FK1]    Script Date: 04/29/2014 14:03:23 ******/
ALTER TABLE [dbo].[REL_ESTRUCTURA_DEPENDENCIA]  WITH CHECK ADD  CONSTRAINT [CAT_ESTRUCTURA_REL_ESTRUCTURA_DEPENDENCIA_FK1] FOREIGN KEY([IdEstructura])
REFERENCES [dbo].[CAT_ESTRUCTURA] ([IdEstructura])
GO
ALTER TABLE [dbo].[REL_ESTRUCTURA_DEPENDENCIA] CHECK CONSTRAINT [CAT_ESTRUCTURA_REL_ESTRUCTURA_DEPENDENCIA_FK1]
GO
