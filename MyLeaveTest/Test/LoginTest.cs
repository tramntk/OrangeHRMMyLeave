using MyLeaveTest.Pages;
using Automation.Core.Helpers;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class LoginTest: BaseTest
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;

        [TestInitialize]        
        public void InitLoginPage()
        {
            // Init Login page
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
        }
        
        [TestMethod("TC001: Verify login successfully")]
        [TestCategory("smoketest")]
        public void VerifyPositiveLoginTest()
        {
            // Step 1 : Navigate to Login Page            
            loginPage.NavigateToLoginPage();

            // Step 2:
            // Type username "Admin" into Username field
            // Type password "admin123" into Password field            
            string username = ConfigurationHelpers.GetValue<string>("username");
            string password = ConfigurationHelpers.GetValue<string>("password");
            loginPage.EnterUserNameAndPassword(username, password);

            // Log Info
            reportHelpers.LogMessage("Info", "Login with user: " + username);
            reportHelpers.LogMessage("Info", "Login with password: " + password);            

            // Step 3: Click Login button
            loginPage.ClickLoginButton();

            // Verify URL: contains "dashboard/index"
            Assert.IsTrue(loginPage.IsHomePageURL());

            // Verify new page contains expected text('Dashboard')
            StringAssert.Contains(dashboardPage.GetContentDashboardHeader(), "Dashboard");

            // Verify time at work chart display
            dashboardPage.IsChartTimeAtWorkDisplay();

            // Log Pass
            reportHelpers.LogMessage("Pass", "Login with valid user: Pass");
        }

        [TestMethod("TC002: Verify login fail with invalid username")]
        public void VerifyNegativeLoginTestInvalidUserName()
        {
            // Step 1 : Navigate to Login Page            
            loginPage.NavigateToLoginPage();

            // Step 2:
            // Input invalid username
            // Input valid password
            string username = "Admin" + new Random().Next(100, 1000);
            string password = ConfigurationHelpers.GetValue<string>("password");
            loginPage.EnterUserNameAndPassword(username, password);

            // Log Info
            reportHelpers.LogMessage("Info", "Login with user: " + username);         
                        
            // Step 3: Click Login button
            loginPage.ClickLoginButton();

            // Verify URL: contains "auth/login"
            Assert.IsTrue(loginPage.IsLoginPageURL());

            // Verify error message: "Invalid credentials" is displayed
            Assert.AreEqual("Invalid credentials", loginPage.GetErrorMessageContent());

            // Verify has error icon
            Assert.IsTrue(loginPage.IsErrorIconDisplay());

            //Log Pass
            reportHelpers.LogMessage("Pass", "Login with invalid username: Pass");
        }
    }
}
