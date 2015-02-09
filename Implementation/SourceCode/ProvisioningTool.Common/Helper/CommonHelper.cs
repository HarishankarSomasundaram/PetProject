using System.Configuration;
using Library;
using System;

namespace ProvisioningTool.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommonHelper
    {
        #region[GetConnectionString]
        /// <summary>
        /// Gets applicaton configuration value for given AppKey from configuration file
        /// </summary>
        /// <param name="AppKey">AppKey</param>
        /// <returns>Value for given AppKey</returns>
        public static string GetConnectionString(string AppKey)
        {
            string KeyValue = ConfigurationManager.ConnectionStrings[AppKey].ConnectionString;
            if (ConvertHelper.ConvertToString(KeyValue) != null)
                return KeyValue;
            return null;
        }
        #endregion[GetConnectionString]

        #region[GetAppConfiguration]
        /// <summary>
        /// Gets applicaton configuration value for given AppKey from configuration file
        /// </summary>
        /// <param name="AppKey">AppKey</param>
        /// <returns>Value for given AppKey</returns>
        public static string GetAppConfiguration(string AppKey)
        {
            string KeyValue = ConfigurationManager.AppSettings[AppKey];
            if (ConvertHelper.ConvertToString(KeyValue) != null)
                return KeyValue;
            return null;
        }

        /// <summary>
        /// Gets applicaton configuration value for given AppKey from configuration file
        /// </summary>
        /// <param name="AppKey">AppKey</param>
        /// <returns>Value for given AppKey</returns>
        public static string GetAppConfiguration(string AppKey, string defaultValue)
        {
            string KeyValue = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings[AppKey], defaultValue);
            return KeyValue;
        }

        #endregion[GetAppConfiguration]

        public static string GetServiceBaseURL()
        {
            string serverAddress = string.Empty;
            string servicesAlias = string.Empty;
            string serviceURL = string.Empty;

            serverAddress = GetAppConfiguration("ServerAddress", "localhost");
            servicesAlias = GetAppConfiguration("ServicesAlias", "Services/ProvisioningToolServices.svc");
            serviceURL = ConvertHelper.ConvertToString(string.Format("http://{0}/{1}/", serverAddress, servicesAlias), "http://provisioningtool.techaffinity.com/Services/ProvisioningToolServices.svc/");
            return serviceURL;
        }
    }
}
