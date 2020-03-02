SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tAttendance_History](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Attendance] [int] NULL,
	[ID_Employee] [int] NULL,
	[ID_DailySchedule] [int] NULL,
	[ID_Leave] [int] NULL,
	[ID_ImportedAttendance_Detail] [int] NULL,
	[ID_EmployeeDailySchedule] [int] NULL,
	[Date] [datetime] NULL,
	[TimeIn] [datetime] NULL,
	[TimeOut] [datetime] NULL,
	[MinuteIn] [int] NULL,
	[MinuteOut] [int] NULL,
	[Days] [decimal](18, 9) NULL,
	[Hours] [decimal](18, 9) NULL,
	[Tardy] [decimal](18, 9) NULL,
	[OT] [decimal](18, 9) NULL,
	[ND] [decimal](18, 0) NULL,
	[IsComplete] [bit] NULL,
	[SeqNo] [int] NULL,
	[IsActive] [bit] NULL,
	[Comment] [varchar](50) NULL,
	[DateTimeCreated] [datetime] NULL,
	[DateTimeModified] [datetime] NULL,
	[ID_EmployeeAttendanceLog] [int] NULL,
	[ComputedTimeIn] [datetime] NULL,
	[ComputedTimeOut] [datetime] NULL,
	[TempMinuteIn] [int] NULL,
	[TempMinuteOut] [int] NULL,
	[FromOB] [bit] NULL,
	[ID_AttendanceFile_Detail] [int] NULL,
	[OBIN] [bit] NULL,
	[OBOUT] [bit] NULL,
	[WorkDate] [datetime] NULL,
	[ID_AttendanceLogType] [int] NULL,
 CONSTRAINT [PK_tAttendance_History] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tAttendance_History]  WITH CHECK ADD  CONSTRAINT [FK_tAttendance_History_tEmployee] FOREIGN KEY([ID_Employee])
REFERENCES [dbo].[tEmployee] ([ID])
GO

ALTER TABLE [dbo].[tAttendance_History] CHECK CONSTRAINT [FK_tAttendance_History_tEmployee]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Daily Schedule' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tAttendance_History', @level2type=N'COLUMN',@level2name=N'ID_DailySchedule'
GO


