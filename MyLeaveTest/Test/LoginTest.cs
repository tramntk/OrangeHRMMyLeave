using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MyLeaveTest.Pages;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Automation.Core.Helpers;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class LoginTest: BaseTest
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;

        /* -- Use override
        public override void SetUpPageObject()
        {
            //Init Login page
            loginPage = new LoginPage(driver);

            dashboardPage = new DashboardPage(driver);
        }
        */
        [TestInitialize]        
        public void InitLoginPage()
        {
            //Init Login page
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
        }

        [TestMethod("TC: Verify login successfully")]
        public void Verify_Positive_LoginTest()
        {
            //Navigate to Login Page
            driver.Navigate().GoToUrl(ConfigurationHelpers.GetValue<string>("url"));

            //Type username "Admin" into Username field
            //Type password "admin123" into Password field
            string username = ConfigurationHelpers.GetValue<string>("username");
            string password = ConfigurationHelpers.GetValue<string>("password");
            loginPage.EnterUserNameAndPassword(username,password);

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
