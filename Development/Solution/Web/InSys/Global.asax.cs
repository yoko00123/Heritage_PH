using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using InSys.App_Start;
using System.Web.Optimization;
using z.Controller;
//using z.Controller.Log;
using InSys.Helpers;
using System.Configuration;
using z.Data;
using Autofac;
using Serilog;
using z.SQL;
using Hangfire.Common;
using Hangfire;
using Hangfire.Console;
using InSys.Storage;
using Hangfire.SqlServer;

namespace InSys
{
    public class Global : HttpApplication
    {
        private ContainerBuilder _container;


        protected void Application_Start()
        {
            Config.Init(true);

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            InSysBundles.Register(BundleTable.Bundles);

            var db = Config.Get("SQLConnection").ToString();

            _container = new ContainerBuilder();
            _container.Register(c => Log.Logger).SingleInstance();
            _container.Register<IQueryArgs>(c => new Query.QueryArgs(db)).InstancePerBackgroundJob();
            _container.Register<IJobFilterProvider>(_ => JobFilterProviders.Providers); 
            _container.Register<IStorage>(_ => new Storage.Storage()).InstancePerBackgroundJob();
            _container.RegisterType<JobQueue>().InstancePerBackgroundJob();

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(db, new SqlServerStorageOptions { SchemaName = "web_jobs" })
               // .UseSqlServerStorage(db, new SqlServerStorageOptions { SchemaName = "automated_jobs" })
                .UseAutofacActivator(_container.Build())
                .UseConsole();
              
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        protected void Application_EndRequest(object sender, EventArgs ev)
        {
            Response.Headers.Remove("Server");
            var Secure = HttpContext.Current.Request.IsSecureConnection;
            if (Response.Cookies.Count > 0)
                foreach (var s in Response.Cookies.AllKeys)
                {
                    Response.Cookies[s].HttpOnly = true;
                    Response.Cookies[s].Secure = Secure;
                }
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs ev)
        {
            HttpApplication app = sender as HttpApplication;
            if (app != null && app.Context != null)
            {
                app.Context.Response.Headers.Remove("Server");
            }

        }

    }
}