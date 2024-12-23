using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MyLeaveTest.Pages;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class LoginTest: BaseTest
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;

        [TestInitialize]
        public void InitLogin()
        {
            //Init page
            loginPage = new LoginPage(driver);

            dashboardPage = new DashboardPage(driver);
        }

        [TestMethod("TC: Verify login successfully")]
        public void Verify_Positive_LoginTest()
        {
            //Type username "Admin" into Username field
            //Type password "admin123" into Password field
            loginPage.EnterUserNameAndPassword("Admin","admin123");

            //Push Login button
            loginPage.ClickLoginButton();

            //Verify new page URL contains "dashboard/index"
            Assert.IsTrue(driver.Url.Contains("dashboard/index"));

            //Verify new page contains expected text('Dashboard')
            //StringAssert.Contains(dashboardPage.GetContentDashboardHeader(), "Dashboard");

            //verify time at work chart display
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            wait.Until(d => dashboardPage.IsChartTimeAtWorkDisplay());

        }
    }
}
