using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MyLeaveTest.Pages.LoginPage
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        //Page Elements
        private IWebElement inputUsername => driver.FindElement(By.XPath("//input[@name='username']"));

        private IWebElement inputPassword => driver.FindElement(By.XPath("//input[@name='password']"));

        private IWebElement loginBtn => driver.FindElement(By.XPath("//button[@type='submit']"));
        
        //Methods interact
        public void EnterUserName(string userName)
        {
            inputUsername.SendKeys(userName);
        }

        public void EnterPassword(string passWord)
        {
            inputPassword.SendKeys(passWord);
        }

        public void ClickLoginButton()
        {
            loginBtn.Click();
        }
    }
}
