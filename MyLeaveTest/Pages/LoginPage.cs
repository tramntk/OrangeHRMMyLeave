using OpenQA.Selenium;

namespace MyLeaveTest.Pages
{
    public class LoginPage: BasePage
    {
        public LoginPage(IWebDriver _driver) : base(_driver)
        {
        }

        //Page Elements
        private IWebElement inputUsername => driver.FindElement(By.XPath("//input[@name='username']"));

        private IWebElement inputPassword => driver.FindElement(By.XPath("//input[@name='password']"));

        private IWebElement loginBtn => driver.FindElement(By.XPath("//button[@type='submit']"));

        //Methods interact

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

        public void ClickLoginButton()
        {
            loginBtn.Click();
        }
    }
}
