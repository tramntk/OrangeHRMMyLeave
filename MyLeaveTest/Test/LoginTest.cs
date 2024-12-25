using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MyLeaveTest.Pages;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Automation.Core.Helpers;
using Automation.WebDriver;

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
            //Init Login page
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
        }

        [TestMethod("TC001: Verify login successfully")]
        public void VerifyPositiveLoginTest()
        {
            //Step 1 : Navigate to Login Page            
            driver.GoTo(ConfigurationHelpers.GetValue<string>("url"));

            //Step 2:
            //Type username "Admin" into Username field
            //Type password "admin123" into Password field            
            string username = ConfigurationHelpers.GetValue<string>("username");
            string password = ConfigurationHelpers.GetValue<string>("password");

            //Log Info
            reportHelpers.LogMessage("Info", "Login with user: " + username);
            reportHelpers.LogMessage("Info", "Login with password: " + password);

            loginPage.EnterUserNameAndPassword(username, password);

            //Step3: Push Login button
            loginPage.ClickLoginButton();

            //Verify URL: contains "dashboard/index"
            Assert.IsTrue(loginPage.IsHomePageURL());

            //Verify new page contains expected text('Dashboard')
            StringAssert.Contains(dashboardPage.GetContentDashboardHeader(), "Dashboard");

            //verify time at work chart display
            driver.Wait(dashboardPage.ChartTimeAtWorkDisplay());

            //Log Pass
            reportHelpers.LogMessage("Pass", "Login with valid user: Pass");

        }

        [TestMethod("TC002: Verify login fail with invalid username")]
        public void VerifyNegativeLoginTestInvalidUserName()
        {
            //Step 1 : Navigate to Login Page            
            driver.GoTo(ConfigurationHelpers.GetValue<string>("url"));

            string username = "Admin" + new Random().Next(100, 1000);

            //Log Info
            reportHelpers.LogMessage("Info", "Login with user: " + username);
            string password = ConfigurationHelpers.GetValue<string>("password");

            loginPage.EnterUserNameAndPassword("Admin123", "admin123");

            //Step2: Push Login button
            loginPage.ClickLoginButton();

            //Verify URL: contains "auth/login"
            Assert.IsTrue(loginPage.IsLoginPageURL());

            //Verify error message: "Invalid credentials" is displayed
            Assert.AreEqual("Invalid credentials", loginPage.GetErrorMessageContent());

            //Verify has error icon
            Assert.IsTrue(loginPage.IsErrorIconDisplay());

            //Log Pass
            reportHelpers.LogMessage("Pass", "Login with invalid username: Pass");

        }
    }
}
