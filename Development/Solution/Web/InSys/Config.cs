using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace z.Controller
{
    /// <summary>
    /// Web Configuration
    /// </summary>
    public sealed class Config
    {

        /// <summary>
        /// Manual Config
        /// </summary>
        /// <param name="isCloudApp"></param>
        /// <param name="useAppConfig"></param>
        public static void Init(bool useAppConfig)
        {

            UseAppConfig = useAppConfig;
        }

        /// <summary>
        /// AutoConfig
        /// </summary>
        public static void Init()
        {
            var b = false;
            try
            {
                b = RoleEnvironment.IsAvailable;
            }
            catch { }

            Init(  b);
        }


        /// <summary>
        /// Get Config
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static object Get(string Name)
        {
            if (UseAppConfig)
                return ConfigurationManager.AppSettings.Get(Name);
            else
                return RoleEnvironment.GetConfigurationSettingValue(Name);
        }

        public static bool UseAppConfig { get; set; }

    }
}
