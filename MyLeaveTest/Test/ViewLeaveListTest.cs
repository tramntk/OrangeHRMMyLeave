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
        private LeftMenuPage navigationPage;
        private ViewLeaveListPage viewLeaveListPage;
        private LoginPage loginPage;
        
        [TestInitialize]
        public void InitViewLeaveListPage()
        {
            //Init View Leave List page
            navigationPage = new LeftMenuPage(driver);
            viewLeaveListPage = new ViewLeaveListPage(driver);
            loginPage = new LoginPage(driver);

            //Login successfully
            loginPage.IsLoginSuccess();
        }        

        [TestMethod("TC: Verify that can navigate to ViewLeaveListPage")]
        public void VerifyNavigateToViewLeaveListPage()
        {
            //Step 1: Click Leave option
            navigationPage.ClickLeaveOption();

            //Verify View Leave List page URL contains "leave/viewLeaveList"
            string expectURL = driver.Url;
            StringAssert.Contains(expectURL, "leave/viewLeaveList");

            //Verify View Leave List page has header content is ('Leave List')
            StringAssert.Equals(viewLeaveListPage.GetLeaveListHeader, "Leave List");
        }
    }
}
