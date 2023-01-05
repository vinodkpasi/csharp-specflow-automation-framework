using BoDi;
using UIAutomationTests.Models;
using UIAutomationTests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlow.Selenium.Models;
using TechTalk.SpecFlow;

namespace UIAutomationTests.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        readonly IObjectContainer container;
        static TestData testData;
        public WebDriverHooks(IObjectContainer container)
        {
            this.container = container;
        }

        [BeforeScenario]
        public void CreateWebDriver()
        {
            ChromeOptions option = new ChromeOptions();
            //option.AddArgument("--headless");
            ChromeDriver driver = new ChromeDriver(option);
            container.RegisterInstanceAs<IWebDriver>(driver);
            Util.Log.Info("Before Scenario--Driver has registered");
        }

        [BeforeTestRun]
        public static void BeforeTestRun(TestContext testContext)
        {
            EnvVar.GetEnvironmentVariables(testContext);
            testData = new TestData();
            Util.Log.Info("Before TestRun--TestData has intialized");
        }

        [AfterScenario(Order = 2)]
        public void DestroyWebDriver()
        {
            var driver = container.Resolve<IWebDriver>();
            if (driver != null)
            {
                driver.Quit();
                Util.Log.Info("AfterScenario--driver has been closed/quited");
            }
        }
    }
}
