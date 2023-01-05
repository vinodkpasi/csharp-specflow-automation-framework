using UIAutomationTests.Utils;
using SpecFlow.Selenium.Models;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace UIAutomationTests.Hooks
{
    [Binding]
    public class ReportsHooks
    {
        [AfterTestRun]
        public static void GenerateReport()
        {
            Util.Log.Info("AfterTestRun-Living Doc report genration process has started");
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            string title = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "(" + TestData.GetEnvironment() + ")";
            startInfo.Arguments = "/C livingdoc test-assembly UIAutomationTests.dll -t TestExecution.json --title " + title + " --output ../../../Reports/Report.html";
            process.StartInfo = startInfo;
            process.Start();
            Util.Log.Info("AfterTestRun-Living Doc report genration process has been completed");
        }
    }
}
