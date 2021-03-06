USE [MRP]
GO
/****** Object:  Table [dbo].[Process]    Script Date: 2021-07-01 오후 2:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Process](
	[PrcIdx] [int] IDENTITY(1,1) NOT NULL,
	[SchIdx] [int] NOT NULL,
	[PrcCD] [char](14) NOT NULL,
	[PrcDate] [date] NOT NULL,
	[PrcLoadTime] [int] NULL,
	[PrcStartTime] [time](7) NULL,
	[PrcEndTime] [time](7) NULL,
	[PrcFacilityID] [char](8) NULL,
	[PrcResult] [bit] NOT NULL,
	[RegDate] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Process] PRIMARY KEY CLUSTERED 
(
	[PrcIdx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 2021-07-01 오후 2:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[SchIdx] [int] IDENTITY(1,1) NOT NULL,
	[PlantCode] [char](8) NULL,
	[SchDate] [date] NOT NULL,
	[SchLoadTime] [int] NOT NULL,
	[SchStartTime] [time](7) NULL,
	[SchEndTime] [time](7) NULL,
	[SchFacilityID] [char](8) NULL,
	[SchAmount] [int] NULL,
	[RegDate] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[SchIdx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 2021-07-01 오후 2:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[BasicCode] [char](8) NOT NULL,
	[CodeName] [nvarchar](100) NOT NULL,
	[CodeDesc] [nvarchar](max) NULL,
	[RegDate] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[BasicCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Process] ON 

INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (1, 8, N'PRC20210629001', CAST(N'2021-06-29' AS Date), 5, CAST(N'07:00:00' AS Time), CAST(N'20:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T10:25:14.193' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (2, 8, N'PRC20210629002', CAST(N'2021-06-29' AS Date), 5, CAST(N'07:00:00' AS Time), CAST(N'20:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-29T10:25:57.500' AS DateTime), N'MRP', CAST(N'2021-06-29T11:15:30.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (3, 8, N'PRC20210629003', CAST(N'2021-06-29' AS Date), 5, CAST(N'07:00:00' AS Time), CAST(N'20:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T12:35:10.207' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (4, 9, N'PRC20210630001', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T09:21:20.053' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (5, 9, N'PRC20210630002', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T09:43:05.950' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (6, 9, N'PRC20210630003', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T09:47:15.837' AS DateTime), N'MRP', CAST(N'2021-06-30T09:47:34.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (7, 9, N'PRC20210630004', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T09:48:07.690' AS DateTime), N'MRP', CAST(N'2021-06-30T09:48:44.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (8, 9, N'PRC20210630005', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T09:52:05.133' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (9, 9, N'PRC20210630006', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T09:53:11.837' AS DateTime), N'MRP', CAST(N'2021-06-30T09:53:24.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (10, 9, N'PRC20210630007', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T09:54:10.253' AS DateTime), N'MRP', CAST(N'2021-06-30T09:55:09.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (11, 9, N'PRC20210630008', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T09:56:17.573' AS DateTime), N'MRP', CAST(N'2021-06-30T09:56:29.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (12, 9, N'PRC20210630009', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T10:06:19.797' AS DateTime), N'MRP', CAST(N'2021-06-30T10:08:28.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (13, 9, N'PRC20210630010', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T10:10:21.793' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (14, 9, N'PRC20210630011', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T10:35:26.063' AS DateTime), N'MRP', CAST(N'2021-06-30T10:36:26.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (15, 9, N'PRC20210630012', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T10:38:25.140' AS DateTime), N'MRP', CAST(N'2021-06-30T10:38:47.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (16, 9, N'PRC20210630013', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T10:48:01.400' AS DateTime), N'MRP', CAST(N'2021-06-30T10:48:46.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (17, 9, N'PRC20210630014', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T10:49:10.333' AS DateTime), N'MRP', CAST(N'2021-06-30T10:50:14.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (18, 9, N'PRC20210630015', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T11:01:08.813' AS DateTime), N'MRP', CAST(N'2021-06-30T11:01:32.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [SchIdx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegDate], [RegID], [ModDate], [ModID]) VALUES (19, 9, N'PRC20210630016', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T11:01:34.670' AS DateTime), N'MRP', CAST(N'2021-06-30T11:01:53.000' AS DateTime), N'SYS')
SET IDENTITY_INSERT [dbo].[Process] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (1, N'PC010001', CAST(N'2021-06-25' AS Date), 10, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 20, CAST(N'2021-06-24T18:00:00.000' AS DateTime), N'SYS', CAST(N'2021-06-25T12:09:48.813' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (2, N'PC010001', CAST(N'2021-06-26' AS Date), 10, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10002', 28, NULL, NULL, CAST(N'2021-06-25T14:00:55.257' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (7, N'PC010002', CAST(N'2021-06-28' AS Date), 20, CAST(N'08:00:00' AS Time), CAST(N'17:00:00' AS Time), N'FAC10001', 40, NULL, NULL, CAST(N'2021-06-28T11:52:16.063' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (8, N'PC010002', CAST(N'2021-06-29' AS Date), 5, CAST(N'07:00:00' AS Time), CAST(N'20:00:00' AS Time), N'FAC10002', 20, NULL, NULL, CAST(N'2021-06-29T09:14:32.747' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegDate], [RegID], [ModDate], [ModID]) VALUES (9, N'PC010002', CAST(N'2021-06-30' AS Date), 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), N'FAC10002', 10, NULL, NULL, CAST(N'2021-06-30T09:21:13.760' AS DateTime), N'MRP')
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RegID], [ModDate], [ModID]) VALUES (N'FAC10001', N'설비1', N'생산설비1', CAST(N'2021-06-24T14:07:05.290' AS DateTime), N'MRP', CAST(N'2021-06-24T14:08:49.820' AS DateTime), N'MRP')
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RegID], [ModDate], [ModID]) VALUES (N'FAC10002', N'설비2', N'생산설비2', CAST(N'2021-06-24T14:08:45.130' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RegID], [ModDate], [ModID]) VALUES (N'PC010001', N'수원공장', N'수원공장(코드)-12', CAST(N'2021-06-24T11:22:10.000' AS DateTime), N'SYS', CAST(N'2021-06-28T10:44:52.230' AS DateTime), N'MRP')
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegDate], [RegID], [ModDate], [ModID]) VALUES (N'PC010002', N'부산공장', N'부산공장(코드)2', CAST(N'2021-06-24T13:58:24.433' AS DateTime), N'MRP', CAST(N'2021-06-28T10:44:57.027' AS DateTime), N'MRP')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Process_PrcCD]    Script Date: 2021-07-01 오후 2:02:02 ******/
ALTER TABLE [dbo].[Process] ADD  CONSTRAINT [UK_Process_PrcCD] UNIQUE NONCLUSTERED 
(
	[PrcCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Process]  WITH NOCHECK ADD  CONSTRAINT [FK_Process_Schedules] FOREIGN KEY([SchIdx])
REFERENCES [dbo].[Schedules] ([SchIdx])
GO
ALTER TABLE [dbo].[Process] NOCHECK CONSTRAINT [FK_Process_Schedules]
GO
ALTER TABLE [dbo].[Process]  WITH NOCHECK ADD  CONSTRAINT [FK_Process_Settings] FOREIGN KEY([PrcFacilityID])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Process] NOCHECK CONSTRAINT [FK_Process_Settings]
GO
ALTER TABLE [dbo].[Schedules]  WITH NOCHECK ADD  CONSTRAINT [FK_Schedules_Settings] FOREIGN KEY([PlantCode])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Schedules] NOCHECK CONSTRAINT [FK_Schedules_Settings]
GO
ALTER TABLE [dbo].[Schedules]  WITH NOCHECK ADD  CONSTRAINT [FK_Schedules_Settings1] FOREIGN KEY([SchFacilityID])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Schedules] NOCHECK CONSTRAINT [FK_Schedules_Settings1]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Process', @level2type=N'COLUMN',@level2name=N'RegDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Process', @level2type=N'COLUMN',@level2name=N'RegID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Process', @level2type=N'COLUMN',@level2name=N'ModDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Process', @level2type=N'COLUMN',@level2name=N'ModID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공정계획 순번' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchIdx'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공정코드' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'PlantCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'공정계획일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'로드타임(초)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchLoadTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'시작시간(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchStartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'종료시간(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchEndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'생산설비ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchFacilityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'목표수량(계획)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'SchAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'RegDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작성자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'RegID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'ModDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Schedules', @level2type=N'COLUMN',@level2name=N'ModID'
GO
