using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using z.Data;

namespace InSys.Helpers
{
    public class HangFireDashboardAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            /*
                Only system can access the dashboard, login on the system then access the dashboard
             */
            return HttpContext.Current != null && HttpContext.Current?.Session["ID_User"]?.ToInt32() == 1 || Debugger.IsAttached;
        }
    }
}