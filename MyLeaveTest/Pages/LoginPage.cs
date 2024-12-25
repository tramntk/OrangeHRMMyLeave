using Automation.Core.Helpers;
using Automation.WebDriver;
using OpenQA.Selenium;
using System.Security.AccessControl;

namespace MyLeaveTest.Pages
{
    public class LoginPage: BasePage
    {
        public LoginPage(IWebDriver _driver) : base(_driver)
        {
        }

        //Page Elements
        private IWebElement inputUsername => driver.FindElementByXPath("//input[@name='username']");
        private IWebElement inputPassword => driver.FindElementByXPath("//input[@name='password']");
        private IWebElement loginBtn => driver.FindElementByXPath("//button[@type='submit']");
        private IWebElement errorMess => driver.FindElementByXPath("//p[text() = 'Invalid credentials']");
        private IWebElement errorIcon => driver.FindElementByXPath("//p[text() = 'Invalid credentials']/preceding-sibling::i");

        //Methods interact
        public bool IsLoginSuccess()
        {
            //Step 1 : Navigate to Login Page            
            driver.GoTo(ConfigurationHelpers.GetValue<string>("url"));

            //Step 2:
            //Type username "Admin" into Username field
            //Type password "admin123" into Password field
            string username = ConfigurationHelpers.GetValue<string>("username");
            string password = ConfigurationHelpers.GetValue<string>("password");
            EnterUserNameAndPassword(username, password);

            //Step3: Push Login button
            loginBtn.Click();

            //Verify URL: contains "dashboard/index"
            return IsHomePageURL();
        }
        public void EnterUserName(string username)
        {
            inputUsername.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            inputPassword.SendKeys(password);
        }

        public void EnterUserNameAndPassword(string username, string password)
        {
            inputUsername.SendKeys(username);
            inputPassword.SendKeys(password);
        }

        public bool IsHomePageURL()
        {
            string homepageURL = driver.Url;
            return homepageURL.Contains("dashboard/index");
        }

        public bool IsLoginPageURL()
        {
            string loginURL = driver.Url;
            return loginURL.Contains("auth/login");
        }

        public void ClickLoginButton()
        {
            loginBtn.Click();
        }

        public string GetErrorMessageContent()
        {
            return errorMess.Text;
        }

        public bool IsErrorIconDisplay()
        {
            return driver.Wait(errorIcon);
        }
    }
}
