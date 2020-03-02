SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tEmployeeAttendanceLog_History](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_EmployeeAttendanceLog] [int] NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[Source] [varchar](50) NULL,
	[AccessNo] [varchar](500) NULL,
	[DateTime] [datetime] NULL,
	[ID_Employee] [int] NULL,
	[ID_AttendanceLogType] [int] NULL,
	[ID_EmployeeAttendanceLogFile] [int] NULL,
	[WorkDate] [datetime] NULL,
	[Date] [datetime] NULL,
	[Minute] [int] NULL,
	[SeqNo] [int] NULL,
	[IsActive] [bit] NULL,
	[Comment] [varchar](4000) NULL,
	[ID_DailySchedule] [int] NULL,
	[ID_EditedByUser] [int] NULL,
	[DateTimeCreated] [datetime] NULL,
	[DateTimeModified] [datetime] NULL,
	[ID_EmployeeAttendanceLogCreditDate] [int] NULL,
	[ID_EmployeeMissedLog] [int] NULL,
	[ID_EmployeeMissedLogFile_Detail] [int] NULL,
	[ID_ManualAttendanceInput_Detail] [int] NULL,
	[ID_TimekeepingFile] [int] NULL,
 CONSTRAINT [PK_tEmployeeAttendanceLog_History] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeAttendanceLog_History', @level2type=N'COLUMN',@level2name=N'ID_Employee'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Attendance Log Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeAttendanceLog_History', @level2type=N'COLUMN',@level2name=N'ID_AttendanceLogType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee Attendance Log File' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeAttendanceLog_History', @level2type=N'COLUMN',@level2name=N'ID_EmployeeAttendanceLogFile'
GO


