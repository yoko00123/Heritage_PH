using InSys.Controllers.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using z.Data;
using z.SQL;

namespace InSys.Controllers.Infos
{
    public class TimeAndAttendanceProcessingInfoController : InfoController
    {

        public async Task<Result> EmployeeDailySchedule_CheckUnCheck() => await TaskExec("pEmployeeDailySchedule_CheckUnCheck @ID, @Check, @ID_User", Q["ID"], Q["Check"], Q["ID_User"]);

        public async Task<Result> ComputeHours() => await TaskResult(r =>
        {
            var requestId = Q["RequestID"].ToString();
            ObjectCache cache = MemoryCache.Default;

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
            cache.Set(requestId, "Starting", policy);

            Task.Run(() =>
            {
                requestId = Q["RequestID"].ToString();
                cache = MemoryCache.Default;
                policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now.AddMinutes(10);

                var h = new QueryFire(this.Sql);
                h.Message += (m, i) => cache.Set(requestId, m, policy);
                h.Execute("pComputeHours @ID", Q["ID"]);
                cache.Remove(requestId); //remove when done
            });

            return r;
        });
        public async Task<Result> ComputeHoursStatus() => await TaskResult(r =>
        {
            ObjectCache cache = MemoryCache.Default;
            var Status = cache.Get(Q["RequestID"].ToString());

            if (Status != null)
                r.ResultSet = new { Message = Status.ToString(), Status = 0 };
            else
                r.ResultSet = new { Message = "Completed", Status = 1 };

            return r;
        });

        public async Task<Result> LoadScheduleDetail() => await TaskQuery("EXEC pEmployeeDailySchedule @ID", Q["ID"]);

    }
}