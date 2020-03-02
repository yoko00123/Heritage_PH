SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tEmployeeDailyScheduleView_History](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_EmployeeDailyScheduleView] [int] NULL,
	[Code] [varchar](8000) NULL,
	[Name] [varchar](8000) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ID_Company] [int] NULL,
	[ID_Branch] [int] NULL,
	[ID_PayrollFrequency] [int] NULL,
	[ID_Department] [int] NULL,
	[ID_Designation] [int] NULL,
	[ID_EmployeeStatus] [int] NULL,
	[ID_Gender] [int] NULL,
	[ID_Employee] [int] NULL,
	[ID_Month] [int] NULL,
	[Year] [int] NULL,
	[SeqNo] [int] NULL,
	[IsActive] [bit] NULL,
	[Comment] [varchar](4000) NULL,
	[ID_Transaction_Created] [int] NULL,
	[ID_Transaction_Modified] [int] NULL,
	[DateTimeCreated] [datetime] NULL,
	[DateTimeModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[IsFinalized] [bit] NULL,
	[SalesProjection] [decimal](18, 2) NULL,
	[ManPowerBudgetPercentage] [decimal](18, 2) NULL,
	[ManPowerBudgetAmt] [decimal](18, 2) NULL,
	[ManPowerComputedAmt] [decimal](18, 2) NULL,
	[ManPowerDifferenceAmt] [decimal](18, 2) NULL,
	[ManPowerDifferencePercentage] [decimal](18, 2) NULL,
	[ID_CostCenter] [int] NULL,
	[ID_PayrollClassifi] [int] NULL,
 CONSTRAINT [PK_tEmployeeDailyScheduleView_History] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tBranch] FOREIGN KEY([ID_Branch])
REFERENCES [dbo].[tBranch] ([ID])
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History] CHECK CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tBranch]
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tCompany] FOREIGN KEY([ID_Company])
REFERENCES [dbo].[tCompany] ([ID])
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History] CHECK CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tCompany]
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tDepartment] FOREIGN KEY([ID_Department])
REFERENCES [dbo].[tDepartment] ([ID])
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History] CHECK CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tDepartment]
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tDesignation] FOREIGN KEY([ID_Designation])
REFERENCES [dbo].[tDesignation] ([ID])
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History] CHECK CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tDesignation]
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tEmployee] FOREIGN KEY([ID_Employee])
REFERENCES [dbo].[tEmployee] ([ID])
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History] CHECK CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tEmployee]
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tEmployeeStatus] FOREIGN KEY([ID_EmployeeStatus])
REFERENCES [dbo].[tEmployeeStatus] ([ID])
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History] CHECK CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tEmployeeStatus]
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tGender] FOREIGN KEY([ID_Gender])
REFERENCES [dbo].[tGender] ([ID])
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History] CHECK CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tGender]
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History]  WITH CHECK ADD  CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tPayrollFrequency] FOREIGN KEY([ID_PayrollFrequency])
REFERENCES [dbo].[tPayrollFrequency] ([ID])
GO

ALTER TABLE [dbo].[tEmployeeDailyScheduleView_History] CHECK CONSTRAINT [FK_tEmployeeDailyScheduleView_History_tPayrollFrequency]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Company' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_Company'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Branch' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_Branch'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Payroll Frequency' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_PayrollFrequency'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Department' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_Department'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Designation' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_Designation'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee Status' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_EmployeeStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gender' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_Gender'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_Employee'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Month' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tEmployeeDailyScheduleView_History', @level2type=N'COLUMN',@level2name=N'ID_Month'
GO


