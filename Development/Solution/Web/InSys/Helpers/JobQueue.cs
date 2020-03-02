using Hangfire;
using Serilog;
using System;
using z.SQL;
using z.Data;
using Hangfire.Server;
using Hangfire.Console;
using Hangfire.Console.Progress;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;

namespace InSys.Helpers
{
    [Queue("web_jobs")]
    public class JobQueue : JobActivator
    {
        private ILogger logger;
        private IQueryArgs Sql;
        const string pattern = @"\d{1,3}\%";

        public JobQueue(ILogger logger, IQueryArgs sql)
        {
            this.logger = logger;
            this.Sql = sql;
        }

        [DisplayName("{1}: {0}"), AutomaticRetry(Attempts = 3)]
        public void Enqueue(int ID_Record, string Name, string UID, string CommandText, string Token, string Controller, string Action, int ID_User, int ID_Menu, PerformContext context)
        {
            var filingType = GetFilingType(ID_Menu);
            try
            {
                logger = Log.Logger
                    .ForContext("Token", Token)
                    .ForContext("Controller", Controller)
                    .ForContext("Action", Action)
                    .ForContext("ID_User", ID_User);

                logger.Information($"{UID}-Job Started");
                context.WriteLine("Started");
                SetJobStatus(UID, 2);
                NotifyJob(ID_User, filingType, Name, $"Processing started. Ref ID: { ID_Record }", DBNull.Value);
                if (ID_Menu == 1020)
                {
                    var ID_Company = Sql.ExecScalar($"SELECT ID_Company FROM dbo.vEmployeeDailyScheduleView AS EDSV WHERE EDSV.ID=@ID_Record", ID_Record);
                    var payrollUsers = Sql.ExecQuery($"SELECT ID FROM fGetPayrollUsers(@ID_Company)", ID_Company).Tables[0];
                    if (payrollUsers.Rows.Count > 0)
                    {
                        foreach (DataRow row in payrollUsers.Rows)
                        {
                            var ID_PayrollUser = row.Field<int>("ID");
                            var TKUser = Sql.ExecScalar($"SELECT Name FROM vUser where ID = @ID_User",ID_User);
                            NotifyJob(ID_PayrollUser, filingType, Name, $"{TKUser} started processing. Ref. ID {ID_Record}.", ID_Record);
                        }
                    }
                }

                using (var h = new QueryFire(Sql))
                {
                    var bar = context.WriteProgressBar();
                    h.Message += (m, i) =>
                    {
                        logger.Information($"{UID}-{m}");
                        ParseCompletion(bar, m);
                    };
                    h.Execute(CommandText);
                }

                SetJobStatus(UID, 3);
                logger.Information($"{UID}-Job Finish");
                context.WriteLine("Completed");
                //FOR TK to Payroll Notification
                if (ID_Menu == 1020)
                {
                    var ID_Company = Sql.ExecScalar($"SELECT ID_Company FROM dbo.vEmployeeDailyScheduleView AS EDSV WHERE EDSV.ID=@ID_Record",ID_Record);
                    var payrollUsers = Sql.ExecQuery($"SELECT ID FROM fGetPayrollUsers(@ID_Company)",ID_Company).Tables[0];
                    if (payrollUsers.Rows.Count > 0)
                    {
                        foreach (DataRow row in payrollUsers.Rows)
                        {
                            var ID_PayrollUser = row.Field<int>("ID");
                            NotifyJob(ID_PayrollUser,filingType,Name,$"Processing completed successfully. Ref ID: { ID_Record }",ID_Record);
                        }
                    }  
                }
                NotifyJob(ID_User, filingType, Name, $"Processing completed successfully. Ref ID: { ID_Record }", ID_Record);
            }
            catch (Exception ex)
            {
                SetJobStatus(UID, 4);
                logger.Error(ex, $"{UID}-{ex.Message}");
                context.WriteLine(ConsoleTextColor.Red, ex);
                NotifyJob(ID_User, filingType, Name, $"Processing completed with error. Ref ID: { ID_Record }, please contact your system administrator for details regarding Process ID: { UID }", ID_Record);
                throw ex;
            }
            finally
            {
                context.WriteLine("Ended");
            }
        }

        protected void SetJobStatus(string Uid, int status)
        {
            Sql.ExecNonQuery("UPDATE tMenuButtonBackgroundQueue SET ID_MenuButtonBackgroundQueueStatus = @Status WHERE UID = @uid", status, Uid);
        }

        protected void NotifyJob(int ID_user, int FilingType, string Title, string Body, object RecordID)
        {
            Sql.ExecNonQuery("INSERT INTO dbo.tWebNotifications (DateTimeCreated, ID_User, ID_Employee, Title, Body, ID_WebNotificationsFilingType, rID) VALUES (dbo.fGetDate(), @ID_User, 0, @Title, @Body, @FilingType, @ID)", ID_user, Title, Body, FilingType, RecordID);
        }

        protected int GetFilingType(int ID_Menu)
        {
            return Sql.ExecScalar("SELECT ID FROM tWebNotificationsFilingType WHERE ID_Menu = @ID_Menu", ID_Menu).IsNull(0).ToInt32();
        }

        protected void ParseCompletion(IProgressBar bar, string log)
        {
            var str = Regex.Match(log, pattern);
            if (str.Success)
            {
                var h = Convert.ToInt32(str.Value.Replace("%", ""));
                if (h >= 0 && h <= 100)
                    bar.SetValue(h);
            }
        }

    }

}