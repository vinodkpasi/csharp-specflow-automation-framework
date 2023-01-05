using UIAutomationTests.Pages;
using UIAutomationTests.Utils;
using OpenQA.Selenium;
using SpecFlow.Selenium.Models;
using TechTalk.SpecFlow;

namespace UIAutomationTests.StepDefinitions
{
    [Binding]
public sealed class LoginSteps: BaseSteps
    {
        LoginPage loginPage;
        Login login;

        public LoginSteps(IWebDriver driver, TestData testData)
        {
            loginPage = new LoginPage(driver);
            this.login = testData.Login;
        }

        [Given(@"the user navigates to the login page")]
        public void GivenTheUserNavigatedToLoginPage()
        {
            loginPage.Navigate(login.BaseUrl+"/login");
            Util.Log.Info("login page has opened");
        }

        [When(@"the user enters the correct credentials")]
        public void WhenTheUserEntersCorrectCredentials()
        {
            loginPage.EnterUserName(login.Username);
            Util.Log.Info("User has entered the username");
            loginPage.EnterPassword(login.Password);
            Util.Log.Info("User has entered the password");
        }

        [When(@"the user clicks on the login button")]
        public void WhenTheUserClickOnLoginButton()
        {
            loginPage.ClickOnLoginButton();
            Util.Log.Info("User clicked on the login button");
        }

        [Then(@"the user has successfully logged in")]
        public void ThenTheUserHasSuccessfullyLoggedIn()
        {
            //TODO: Assert
            Util.Log.Info("User has successfully logged-In");
        }

        [Given(@"the user has logged in to the application")]
        public void GivenTheUserHasLoggedInToTheApplication()
        {
            GivenTheUserNavigatedToLoginPage();
            WhenTheUserEntersCorrectCredentials();
            WhenTheUserClickOnLoginButton();
            ThenTheUserHasSuccessfullyLoggedIn();
        }

    }
}
