using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLeaveTest.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Runtime.CompilerServices;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class MyLeaveTest : BaseTest
    {
        private ViewLeaveListPage viewLeaveListPage;
        private MyLeavePage myLeavePage;
        private LeftMenuPage leftMenu;
        private LoginPage loginPage;

        [TestInitialize]      
        public void InitMyLeavePage()
        {
            //Init My Leave page
            viewLeaveListPage = new ViewLeaveListPage(driver);
            myLeavePage = new MyLeavePage(driver);
            loginPage = new LoginPage(driver);
            leftMenu = new LeftMenuPage(driver);

            //Go to login page
            loginPage.IsLoginSuccess();            

            //Click [Leave] item on [Left-Menu] (NavigationPage)
            leftMenu.ClickLeaveOption();

            //Click[My Leave] button on [Top Menu] (ViewLeaveListPage)
            viewLeaveListPage.ClickMyLeaveButton();
        }

        [TestMethod("TC001: Verify default value of filters")]
        public void VerifyDefaultValuesOfFilters()
        {
            //Verify [My Leave List] page URL contains "leave/viewMyLeaveList"
            string expectURL = driver.Url;
            StringAssert.Contains(expectURL, "leave/viewMyLeaveList");

            //Verify [My Leave List] page has header content is ('My Leave List')
            StringAssert.Equals(myLeavePage.GetMyLeaveListHeader(), "My Leave List");

            //Check default value of all filters on [My Leave List] page
            myLeavePage.IsAllFiltersHaveDefaultValue();
        }

        [TestMethod("TC002: Verify search with no records")]
        public void VerifyNoRecordsFound()
        {
            //Step 1: Input From date, To Date: a date in future and To Date >= From Date 
            string fromDate = myLeavePage.SelectDate(2, 0, 0);
            string toDate = myLeavePage.SelectDate(2, 1, 0);
            myLeavePage.InputFromDateToDate(fromDate, toDate);

            //Step 2: Click Search button
            myLeavePage.ClickSearchButton();

            //Verify Table Result header return text "No Records Found"
            myLeavePage.IsNoRecordsFoundHeader();

            //Verify content of Toast Message
            string toastMss = myLeavePage.ToastMessageContent();
            StringAssert.Contains(toastMss, "No Records Found");
        }

        [TestMethod("TC003: Verify error message when input From Date > To Date")]
        public void VerifyFromDateGreaterThanToDate()
        {
            //Step 1: Input From date > To Date
            string fromDate = myLeavePage.SelectDate(0, 1, 0);
            string toDate = myLeavePage.SelectDate(0, 0, 0);            
            myLeavePage.InputFromDateToDate(fromDate, toDate);

            //Verify that error message should be shown below To Date field with content "To date should be after from date"
            string errMss = myLeavePage.GetContentErrorMessageWhenFromDateGreaterThanToDate();
            Assert.AreEqual("To date should be after from date", errMss);
        }

        [TestMethod("TC004: Verify that error will be shown when user does not input at mandatory fields")]
        public void VerifyNotInputMandatoryFields()
        {
            //Step 1: Not select any value of [Status] (Click [x] icon of all values)
            myLeavePage.ClearAllValueForLeaveStatus();

            //Verify that error "Required" will be shown in red color at the bottom of the field[Status]
            string requiredMess = myLeavePage.GetRequireMessage();
            Assert.AreEqual("Required", requiredMess);
        }

        [TestCleanup]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
