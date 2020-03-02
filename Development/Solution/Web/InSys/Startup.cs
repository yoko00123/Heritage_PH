using Microsoft.Owin;
using Owin;
using z.Controller;
using InSys.Helpers;
using z.Data;
using InSys.Storage;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using Hangfire;
using Autofac;
using z.SQL;
using Hangfire.Common;
using Hangfire.Console;
using System.Collections.Generic;
using Hangfire.Dashboard;
using Serilog;

[assembly: OwinStartup(typeof(InSys.Startup))]
namespace InSys
{
    /// <summary>
    /// Must add Under web.config 
    /// <handlers>  
    /// <add name = "Owin" verb="" path="InSysStorage/*" type="Microsoft.Owin.Host.SystemWeb.OwinHttpHandler, Microsoft.Owin.Host.SystemWeb" />
    /// </handlers> 
    /// </summary>
    public class Startup
    {
        public bool EnableDirectoryBrowsing { get; set; } = false;
        
        public void Configuration(IAppBuilder app)
        {
            

            app.UseWebApi(WebApiConfig.Register());

            LogManager.Initiate();

            //base.Configuration(app);
            var g = Config.Get("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString").ToString();
            var sg = Config.Get("RootContainer")?.ToString();
           
            EnableDirectoryBrowsing = !Config.Get("OptimizedBundles").ToBool();

            

            StorageSetting.Init(g, sg);

            if (!StorageSetting.IsCloudPath)
            {
                var fileSystem = new PhysicalFileSystem(g);
                var options = new FileServerOptions
                {
                    FileSystem = fileSystem,
                    EnableDirectoryBrowsing = EnableDirectoryBrowsing,
                    RequestPath = PathString.FromUriComponent(StorageSetting.RequestPath)
                };

                app.UseFileServer(options);
            }
            EmailAnnouncement SendAnnouncement = new EmailAnnouncement(Config.Get("SQLConnection").ToString());
            SendAnnouncement.Start();

            JSReport.Initiate();

            var bj = new BackgroundJobServerOptions
            {
                WorkerCount = Config.Get("BackgroundJobWorkerCount")?.ToInt32() ?? 5,
                Queues = new string[] { "web_jobs", "automated_jobs" }
            };

            app.UseHangfireServer(bj);

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new List<IDashboardAuthorizationFilter>() {
                    new HangFireDashboardAuthFilter()
                }
            }); 
        }
    }
}
