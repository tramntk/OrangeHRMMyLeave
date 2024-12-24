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
        
        [TestMethod("TC: Verify login successfully")]
        public void VerifyPositiveLoginTest()
        {
            //Step 1 : Navigate to Login Page

            //Step 2:
            //Type username "Admin" into Username field
            //Type password "admin123" into Password field

            //Step3: Push Login button

            //Verify new page URL contains "dashboard/index"
            loginPage.IsLoginSuccess();

            //Verify new page contains expected text('Dashboard')
            StringAssert.Contains(dashboardPage.GetContentDashboardHeader(), "Dashboard");

            //verify time at work chart display
            driver.Wait(dashboardPage.ChartTimeAtWorkDisplay());

        }
    }
}
