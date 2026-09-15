using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UIAutomationTests.Models
{
    public static class EnvVar

    {
        public static string ENV { get; set; }
        public static string UID { get; set; }
        public static string PWD { get; set; }


        public static void GetEnvironmentVariables(TestContext testContext)
        {
            UID = (string)testContext.Properties["UID"];
            PWD = (string)testContext.Properties["PWD"];
            ENV = (string)testContext.Properties["ENV"];
        }
    }
}
