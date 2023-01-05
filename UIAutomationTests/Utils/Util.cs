using System.Configuration;

namespace UIAutomationTests.Utils
{
    public static class Util
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static log4net.ILog Log { get { return log; } }

        public static string GetKey(string key)
        {
            string keyValue = ConfigurationManager.AppSettings[key];
            return keyValue;
        }

    }
}
