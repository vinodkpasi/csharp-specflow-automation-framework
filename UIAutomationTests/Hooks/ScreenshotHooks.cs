using BoDi;
using UIAutomationTests.Utils;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace UIAutomationTests.Hooks
{
    [Binding]
    public class ScreenshotHooks
    {
        private readonly IObjectContainer container;
        ScenarioContext scenarioContext;
        FeatureContext featureContext;
        private readonly ISpecFlowOutputHelper specFlowOutputHelper;

        public ScreenshotHooks(IObjectContainer container, ScenarioContext scenarioContext, FeatureContext featureContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this.container = container;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            this.specFlowOutputHelper = specFlowOutputHelper;
        }

        [AfterScenario(Order = 1)]
        public void ScreenshotWebDriver()
        {
            try
            {
                Util.Log.Info("AfterScenario- chekcing error for screenshot process");

                var driver = container.Resolve<IWebDriver>();
                if (scenarioContext.TestError != null)
                {
                    Util.Log.Info("AfterScenario- Screenshot capture process has started");
                    TakeScreenshot(driver);
                    Util.Log.Info("AfterScenario- Screenshot capture process has been completed");
                }
            }
            catch (Exception ex)
            {
                Util.Log.Error(ex.StackTrace);
            }
        }

        private void TakeScreenshot(IWebDriver driver)
        {
            try
            {
                string screenshotFolderName = "Screenshots";
                string reportsFolderName = "Reports";
                string fileNameBase = string.Format("Error_{0}_{1}",
                featureContext.FeatureInfo.Title,
                scenarioContext.ScenarioInfo.Title + ".png");
                string projectRootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                string reportsAbsolutePath = Path.Combine(projectRootPath, reportsFolderName);
                string screenshotAbsolutePath = Path.Combine(reportsAbsolutePath, screenshotFolderName);
                if (!Directory.Exists(screenshotAbsolutePath))
                    Directory.CreateDirectory(screenshotAbsolutePath);

                ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;
                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();
                    string screenshotAbsoluteFilePath = Path.Combine(screenshotAbsolutePath, fileNameBase);
                    string screenshotRelativeFilePath = screenshotFolderName + Path.DirectorySeparatorChar + fileNameBase;

                    screenshot.SaveAsFile(screenshotAbsoluteFilePath, ScreenshotImageFormat.Png);
                    specFlowOutputHelper.AddAttachment(screenshotRelativeFilePath);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error while taking screenshot: {0}", ex);
                Util.Log.Error(ex.StackTrace);
            }
        }
    }
}

