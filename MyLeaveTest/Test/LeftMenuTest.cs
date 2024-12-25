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

            loginPage.IsLoginSuccess();
        }

        [TestMethod("TC001: Navigate to Admin page")]
        public void NavigateToAdminPage()
        {
            leftMenuPage.ClickAdminOption();
        }

        [TestMethod("TC002: Navigate to PIM page")]
        public void NavigateToPIMPage()
        {
            leftMenuPage.ClickPIMOption();
        }

        [TestMethod("TC003: Navigate to Leave page")]
        public void NavigateToLeavePage()
        {
            leftMenuPage.ClickLeaveOption();
        }
    }
}
