
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tEmployeeDailySchedule_History](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_EmployeeDailySchedule] [int] NULL,
	[Date] [datetime] NULL,
	[ID_DailySchedule] [int] NULL,
	[ID_Employee] [int] NULL,
	[ID_ImportedSchedule] [int] NULL,
	[ID_Attribute] [int] NULL,
	[REG] [decimal](20, 12) NULL,
	[EXT] [decimal](20, 12) NULL,
	[OT] [decimal](20, 12) NULL,
	[ND] [decimal](20, 12) NULL,
	[NDOT] [decimal](20, 12) NULL,
	[TARDY] [int] NULL,
	[UT] [int] NULL,
	[IsRD] [bit] NULL,
	[TimeIn] [datetime] NULL,
	[TimeOut] [datetime] NULL,
	[IsForComputation] [bit] NULL,
	[ID_LeavePayrollItem] [int] NULL,
	[ID_FirstHalfLeavePayrollItem] [int] NULL,
	[ID_SecondHalfLeavePayrollItem] [int] NULL,
	[Comment] [varchar](4000) NULL,
	[IsAbsent] [bit] NULL,
	[ActualTardy] [int] NULL,
	[IsActualAbsent] [bit] NULL,
	[Absences] [decimal](18, 2) NULL,
	[LeaveWithPay] [bit] NULL,
	[FirstHalfLeaveWithPay] [bit] NULL,
	[SecondHalfLeaveWithPay] [bit] NULL,
	[OffsetREG] [decimal](20, 12) NULL,
	[OffsetOT] [decimal](20, 12) NULL,
	[OffsetND] [decimal](20, 12) NULL,
	[OffsetNDOT] [decimal](20, 12) NULL,
	[ComputedREG] [decimal](20, 12) NULL,
	[ComputedOT] [decimal](20, 12) NULL,
	[ComputedND] [decimal](20, 12) NULL,
	[ComputedNDOT] [decimal](20, 12) NULL,
	[RatedREG] [decimal](20, 12) NULL,
	[RatedOT] [decimal](20, 12) NULL,
	[RatedND] [decimal](20, 12) NULL,
	[RatedNDOT] [decimal](20, 12) NULL,
	[OffsetRate] [decimal](20, 12) NULL,
	[ActualREG] [decimal](18, 2) NULL,
	[ActualOT] [decimal](18, 2) NULL,
	[ActualND] [decimal](18, 2) NULL,
	[ActualNDOT] [decimal](18, 2) NULL,
	[ForPerfectAttendance] [bit] NULL,
	[StraightDuty] [bit] NULL,
	[IsHDAbsent] [bit] NULL,
	[MealAllowance] [decimal](18, 2) NULL,
	[IsNoAttendance] [decimal](18, 2) NULL,
	[ID_CostCenter] [int] NULL,
	[IsTentativeAbsent] [bit] NULL,
	[HasStopEmail] [bit] NULL,
	[HasSchedule] [int] NULL,
	[ActualUT] [int] NULL,
	[Posted] [bit] NULL,
	[TardyAsLeavePayrollItem] [int] NULL,
	[UTAsLeavePayrollItem] [int] NULL,
	[TardyAsLeave] [bit] NULL,
	[UTAsLeave] [bit] NULL,
	[ID_TempDailySchedule] [int] NULL,
	[ID_DayType] [int] NULL,
	[DateTimeCreated] [datetime] NULL,
	[DateTimeModified] [datetime] NULL,
	[DateTimeProcessed] [datetime] NULL,
 CONSTRAINT [PK_tEmployeeDailySchedule_History] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [FK_tEmployeeDailySchedule_Un] UNIQUE NONCLUSTERED 
(
	[ID_EmployeeDailySchedule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'For Computation' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailySchedule_History', @level2type=N'COLUMN',@level2name=N'IsForComputation'
GO


