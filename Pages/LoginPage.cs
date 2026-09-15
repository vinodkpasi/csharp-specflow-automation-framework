using OpenQA.Selenium;

namespace UIAutomationTests.Pages
{
    public class LoginPage: BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        By inputUserName = By.Id("username");
        By inputPassword = By.Id("password");
        By btnLogin = By.XPath("//button[@type='submit']");

        public IWebElement InputUserName => driver.FindElement(inputUserName);
        public IWebElement InputPassword => driver.FindElement(inputPassword);
        public IWebElement BtnLogin => driver.FindElement(btnLogin);


        public void EnterUserName(string username)
        {
            InputUserName.SendKeys(username);
        }
        public void EnterPassword(string password)
        {
            InputPassword.SendKeys(password);
        }
        public void ClickOnLoginButton()
        {
            BtnLogin.Click();
        }

        public bool IsLoggedOut()
        {
            VerifyPageLoaded();
            return BtnLogin.Displayed && InputUserName.Displayed;
        }
    }

}

