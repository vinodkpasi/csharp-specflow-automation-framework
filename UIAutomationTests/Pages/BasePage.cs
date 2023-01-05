using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UIAutomationTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        public readonly TimeSpan defaulTimeSpan = TimeSpan.FromSeconds(60);
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            this.driver.Manage().Window.Maximize();
        }

        public void Navigate(string url)
        {
            driver.Url = url;
            VerifyPageLoaded();
        }
        public void VerifyPageLoaded(double second = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string pageLoadStatus = (string)js.ExecuteScript("return document.readyState");
            wait.Until(p => pageLoadStatus == "complete");
        }
        public void WaitforElementToBeDisplay(By id, double second = 5)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(id));
            }
            catch (WebDriverException)
            {
            }
        }
        public void  WaitforElementToBeInVisible(By id, double second = 5)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(id));
            }
            catch (WebDriverException)
            {
            }
        }
        public void ClickedOnElemennt(IWebElement element)
        {
            element.Click();
        }

        public bool IsTextDisplayed(IWebElement element)
        {
            return element.Displayed;
        }
        public bool IsTextDisplayed(IWebElement element, string text)
        {
            return element.Text == text;
        }
        public bool IsTextBoxTextDisplayed(IWebElement element, string text)
        {
            return element.Displayed && element.GetAttribute("value") == text;
        }
        public void ScrollToElement(IWebElement element)
        {
            var js = driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public string GetTextBoxValue(IWebElement element)
        {
            return element.GetAttribute("value");
        }
        public void WaitElementToClickable(By id, double second = 5)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(id));
            }
            catch (WebDriverException)
            {
            }
        }
        public bool WaitforElementToBeInVisible(By id)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(id));
                return true;
            }
            catch (WebDriverException)
            {
                return  false;
            }
        }
    }
}
