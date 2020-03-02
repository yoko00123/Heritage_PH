SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tEmployeeDailySchedule_Detail_History](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_EmployeeDailySchedule] [int] NULL,
	[ID_EmployeeDailySchedule_Detail] [int] NULL,
	[ID_Hourtype] [int] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Minutes] [int] NULL,
	[ConsideredHours] [decimal](18, 2) NULL,
	[Approved] [bit] NULL,
	[ApprovedMinutes] [int] NULL,
	[Tardy] [int] NULL,
	[ActualTardy] [int] NULL,
	[Comment] [varchar](4000) NULL,
	[ID_VerifierEmployee] [int] NULL,
	[ID_ApproverEmployee] [int] NULL,
	[VerificationDate] [datetime] NULL,
	[ApprovalDate] [datetime] NULL,
	[ForApproval] [bit] NULL,
	[IsBasic] [bit] NULL,
	[NDAMMinuteIn] [int] NULL,
	[NDAMMinuteOut] [int] NULL,
	[NDAMMinutes] [int] NULL,
	[NDPMMinuteIn] [int] NULL,
	[NDPMMinuteOut] [int] NULL,
	[NDPMMinutes] [int] NULL,
	[NDMinutes] [int] NULL,
	[NDHours] [decimal](18, 2) NULL,
	[ID_WorkCredit] [int] NULL,
	[ComputedHours] [decimal](18, 6) NULL,
 CONSTRAINT [PK_tEmployeeDailySchedule_Detail_History] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tEmployeeDailySchedule_Detail_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailySchedule_Detail__History_tEmployeeDailySchedule_History] FOREIGN KEY([ID_EmployeeDailySchedule])
REFERENCES [dbo].[tEmployeeDailySchedule_History] ([ID_EmployeeDailySchedule])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[tEmployeeDailySchedule_Detail_History] CHECK CONSTRAINT [FK_tEmployeeDailySchedule_Detail__History_tEmployeeDailySchedule_History]
GO


