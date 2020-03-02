using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using z.Controller;

namespace InSys.Helpers
{
    public static class LogManager
    {

        public static void Initiate()
        {
            var BasePath = AppDomain.CurrentDomain.BaseDirectory;

            var columnoption = new ColumnOptions();
            columnoption.Store.Remove(StandardColumn.Properties);
            columnoption.Store.Remove(StandardColumn.MessageTemplate);
            columnoption.AdditionalDataColumns = new Collection<DataColumn> {
                new DataColumn{ ColumnName = "Token", DataType = typeof(string), MaxLength = 100 },
                new DataColumn{ ColumnName = "Controller", DataType = typeof(string), MaxLength = 50 },
                new DataColumn{ ColumnName = "Action", DataType = typeof(string), MaxLength = 50 },
                new DataColumn{ ColumnName = "ID_User", DataType = typeof(int)}
            };

            Log.Logger = new LoggerConfiguration()
                 .WriteTo.File(Path.Combine(BasePath, "Logs", $"InSysLog-.txt"), rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yy-MM-dd HH:mm:ss}|{Token}|{Controller}|{Action}|{Level:u3}|{Message:lj}{NewLine}{Exception}", rollOnFileSizeLimit: true)
                 .WriteTo.MSSqlServer(Config.Get("SQLConnection").ToString(), "tSerilog", autoCreateSqlTable: true, columnOptions: columnoption)
                 .WriteTo.Console(LogEventLevel.Information)
                 .CreateLogger();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            Log.Logger.Information("Web Started ------------------------------->");
        }

    }
}