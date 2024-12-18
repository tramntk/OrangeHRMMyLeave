using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using MyLeaveTest.Pages.LoginPage;
using OpenQA.Selenium.Chrome;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class LoginTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [TestInitialize]
        public void SetUpAndOpenBrowser()
        {
            //Init driver for Google Chrome
            driver = new ChromeDriver();

            //Set Implicit timeout
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Init page
            loginPage = new LoginPage(driver);

        }

        [TestMethod("TC: Verify login successfully")]
        public void Verify_Positive_LoginTest()
        {
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

            //Type username "Admin" into Username field
            loginPage.EnterUserName("Admin");

            //Type password "admin123" into Password field
            loginPage.EnterPassword("admin123");

            //Push Login button
            loginPage.ClickLoginButton();

            //Verify new page URL contains "dashboard/index"
            Assert.IsTrue(driver.Url.Contains("dashboard/index"));

            //Verify new page contains expected text('Dashboard')
            string loggedText = driver.FindElement(By.XPath("//h6[text()='Dashboard']")).Text;
            StringAssert.Contains(loggedText, "Dashboard");

            //verify time at work chart display
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            wait.Until(d => driver.FindElement(By.XPath("//div[@class='emp-attendance-chart']")).Displayed);

        }

        [TestCleanup]
        public void BrowserCleanup()
        {
            driver.Quit();
        }
    }
}
