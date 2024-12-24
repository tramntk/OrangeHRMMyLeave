using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLeaveTest.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class ViewLeaveListTest: BaseTest
    {
        private NavigationPage navigationPage;
        private ViewLeaveListPage viewLeaveListPage;
        private LoginTest loginTest;
        //private LoginPage loginPage;
        //private DashboardPage dashboardPage;

        [TestInitialize]
        public void InitViewLeaveListPage()
        {
            //Init View Leave List page
            navigationPage = new NavigationPage(driver);
            viewLeaveListPage = new ViewLeaveListPage(driver);
            loginTest = new LoginTest();

            //loginPage = new LoginPage(driver);
            //dashboardPage = new DashboardPage(driver);
            loginTest.InitLoginPage();

            loginTest.Verify_Positive_LoginTest();

        }

        [TestMethod("TC: Verify that can navigate to ViewLeaveListPage")]
        public void VerifyNavigateToViewLeaveListPage()
        {


            //Click Leave option
            navigationPage.ClickLeaveOption();

            //Verify View Leave List page URL contains "leave/viewLeaveList"
            string expectURL = driver.Url;
            StringAssert.Contains(expectURL, "leave/viewLeaveList");

            //Verify View Leave List page has header content is ('Leave List')
            StringAssert.Equals(viewLeaveListPage.GetLeaveListHeader, "Leave List");
        }
    }
}
