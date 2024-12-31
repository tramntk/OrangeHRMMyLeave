using MyLeaveTest.Pages;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class ViewLeaveListTest: BaseTest
    {
        private LeftMenuPage leftMenuPage;
        private ViewLeaveListPage viewLeaveListPage;
        private LoginPage loginPage;

        [TestInitialize]
        public void InitViewLeaveListPage()
        {
            // Init View Leave List page
            leftMenuPage = new LeftMenuPage(driver);
            viewLeaveListPage = new ViewLeaveListPage(driver);
            loginPage = new LoginPage(driver);

            // Login successfully
            loginPage.IsLoginSuccessfully();
        }       

        [TestMethod("TC: Verify that can navigate to ViewLeaveListPage")]
        public void VerifyNavigateToViewLeaveListPage()
        {
            // Step 1: Click Leave option
            leftMenuPage.ClickLeaveOption();

            // Verify View Leave List page URL contains "leave/viewLeaveList"
            leftMenuPage.IsLeaveListURL();

            // Verify View Leave List page has header content is ('Leave List')
            StringAssert.Equals(viewLeaveListPage.GetLeaveListHeader, "Leave List");
        }
    }
}
