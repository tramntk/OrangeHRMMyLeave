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
        private ViewLeaveListTest viewLeaveListTest;
        private ViewLeaveListPage viewLeaveListPage;
        private MyLeavePage myLeavePage;
        private LoginPage loginPage;



        [TestInitialize]      
        public void InitMyLeavePage()
        {
            //Init My Leave page
            viewLeaveListTest = new ViewLeaveListTest();
            viewLeaveListPage = new ViewLeaveListPage(driver);
            myLeavePage = new MyLeavePage(driver);
            

            //Init View Leave List page
            viewLeaveListTest.InitViewLeaveListPage();
            

            //Click [Leave] item on [Left-Menu] (NavigationPage)
            viewLeaveListTest.VerifyNavigateToViewLeaveListPage();

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
            string fromDate = myLeavePage.SelectDate(2, 0, 0);

            string toDate = myLeavePage.SelectDate(2, 1, 0);

            //Input From date, To Date: a date in future and To Date >= From Date
            myLeavePage.InputFromDateToDate(fromDate, toDate);

            //Click Search button
            myLeavePage.ClickSearchButton();

            //Table Result header return text "No Records Found"
            myLeavePage.IsNoRecordsFoundHeader();

            string toastMss = myLeavePage.ToastMessageContent();

            //Verify Toast Message
            StringAssert.Contains(toastMss, "No Records Found");
        }

        [TestMethod("TC003: Verify error message when input From Date > To Date")]
        public void VerifyFromDateGreaterThanToDate()
        {
            string fromDate = myLeavePage.SelectDate(0, 1, 0);

            string toDate = myLeavePage.SelectDate(0, 0, 0);

            //Input From date > To Date
            myLeavePage.InputFromDateToDate(fromDate, toDate);

            //Error message should be shown below To Date field with content "To date should be after from date"
            string errMss = myLeavePage.GetContentErrorMessageWhenFromDateGreaterThanToDate();

            Assert.AreEqual("To date should be after from date", errMss);
        }

        [TestMethod("TC004: Verify that error will be shown when user does not input at mandatory fields")]
        public void VerifyNotInputMandatoryFields()
        {
            //Not select any value of [Status] (Click [x] icon of all values)
            myLeavePage.ClearAllValueForLeaveStatus();

            //Error "Required" will be shown in red color at the bottom of the field[Status]
            myLeavePage.IsRequireMessage();

        }
        [TestCleanup]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
