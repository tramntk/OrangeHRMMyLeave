using MyLeaveTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class LeftMenuTest: BaseTest
    {
        private LoginPage loginPage;
        private LeftMenuPage leftMenuPage;

        [TestInitialize]
        //Init leftmenu
        public void InitLeftMenu()
        {
            loginPage = new LoginPage(driver);
            leftMenuPage = new LeftMenuPage(driver);

            loginPage.IsLoginSuccessfully();
        }

        [TestMethod("TC001: Navigate to Admin page")]
        public void NavigateToAdminPage()
        {
            // Step 1: click Admin option on LeftMenu
            leftMenuPage.ClickAdminOption();

            // Verify URL: contains "admin/viewSystemUsers"
            Assert.IsTrue(leftMenuPage.IsAdminPageURL());
        }

        [TestMethod("TC002: Navigate to PIM page")]
        public void NavigateToPIMPage()
        {
            // Step 1: click PIM option on LeftMenu
            leftMenuPage.ClickPIMOption();

            // Verify URL: contains "pim/viewEmployeeList"
            Assert.IsTrue(leftMenuPage.IsPIMURL());
        }

        [TestMethod("TC003: Navigate to Leave page")]
        public void NavigateToLeavePage()
        {
            // Step 1: click Leave option on LeftMenu
            leftMenuPage.ClickLeaveOption();

            // Verify URL: contains "leave/viewLeaveList"
            Assert.IsTrue(leftMenuPage.IsLeaveListURL()); 
        }
    }
}
