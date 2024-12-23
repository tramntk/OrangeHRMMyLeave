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

        [TestInitialize]
        public override void SetUpPageObject()
        {
            //Init View Leave List page
            loginTest = new LoginTest();
            navigationPage = new NavigationPage(driver);
            viewLeaveListPage = new ViewLeaveListPage(driver);

            loginTest.SetUpPageObject();

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
