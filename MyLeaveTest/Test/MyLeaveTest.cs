using MyLeaveTest.Pages;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class MyLeaveTest : BaseTest
    {
        private ViewLeaveListPage viewLeaveListPage;
        private MyLeavePage myLeavePage;
        private LeftMenuPage leftMenuPage;
        private LoginPage loginPage;
        private ApplyPage applyPage;

        [TestInitialize]      
        public void InitMyLeavePage()
        {
            // Init My Leave page
            viewLeaveListPage = new ViewLeaveListPage(driver);
            myLeavePage = new MyLeavePage(driver);
            loginPage = new LoginPage(driver);
            leftMenuPage = new LeftMenuPage(driver);
            applyPage = new ApplyPage(driver);

            // Precondion execution
            Precondtion();
        }
        
        [TestMethod("TC001: Verify default value of filters")]
        [TestCategory("smoketest")]
        public void VerifyDefaultValuesOfFilters()
        {
            // Verify [My Leave List] page URL contains "leave/viewMyLeaveList"
            string expectURL = driver.Url;
            StringAssert.Contains(expectURL, "leave/viewMyLeaveList");

            // Verify [My Leave List] page has header content is ('My Leave List')
            StringAssert.Equals(myLeavePage.GetMyLeaveListHeader(), "My Leave List");

            // Verify default value of Leave Status dropdown:
            // Includes 5 items (Rejected, Cancelled, Pending Approval, Scheduled, Taken)
            Assert.IsTrue(myLeavePage.IsDefaultValueOfLeaveStatus());

            // Verify default value of Leave Type dropdown: "--Select--"
            Assert.IsTrue(myLeavePage.IsDefaultValueOfLeaveType());

        }

        [TestMethod("TC002: Verify search with no records")]
        public void VerifyNoRecordsFound()
        {
            // Step 1: Input From date, To Date: a date in future and To Date >= From Date 
            string fromDate = myLeavePage.SelectDate(50);
            string toDate = myLeavePage.SelectDate(100);
            myLeavePage.InputFromDateToDate(fromDate, toDate);

            // Step 2: Click Search button
            myLeavePage.ClickSearchButton();

            // Verify content of Toast Message
            string toastMss = myLeavePage.ToastMessageContent();
            StringAssert.Contains(toastMss, messagesData.NoRecords);

            // Verify Table Result header return text "No Records Found"
            string headerResult = myLeavePage.GetTableHeader();
            Assert.AreEqual(messagesData.NoRecords, headerResult);
        }

        [TestMethod("TC003: Verify error message when input From Date > To Date")]
        public void VerifyFromDateGreaterThanToDate()
        {
            // Step 1: Input From date > To Date
            string fromDate = myLeavePage.SelectDate(1);
            string toDate = myLeavePage.SelectDate(0);            
            myLeavePage.InputFromDateToDate(fromDate, toDate);

            // Log Info
            reportHelpers.LogMessage("Info", "From Date: " + fromDate);
            reportHelpers.LogMessage("Info", "To Date: " + toDate);

            // Verify that error message should be shown below To Date field with content "To date should be after from date"
            string errMss = myLeavePage.GetContentErrorMessageOfToDate();
            Assert.AreEqual(messagesData.FromDateToDateErr, errMss);
        }

        [TestMethod("TC004: Verify that error will be shown when user does not input at mandatory fields")]
        public void VerifyNotInputMandatoryFields()
        {
            // Step 1: Not select any value of [Status] (Click [x] icon of all values)
            myLeavePage.ClearAllValueForLeaveStatus();

            // Verify that error "Required" will be shown in red color at the bottom of the field[Status]
            string requiredMess = myLeavePage.GetRequireMessage();
            Assert.AreEqual(messagesData.RequiredErr, requiredMess);
        }

        [TestMethod("TC005: Verify that error will be shown when user input invalid value for From Date and To Date")]
        [DataRow("2025-15-13", "2025-15-13")]
        [DataRow("2025-32-01", "2025-32-01")]
        [DataRow("0000-15-01", "0000-15-01")]
        [DataRow("2025-15-mm", "2025-15-mm")]
        [DataRow("20251501", "20251501")]
        [DataRow("2025/15/01", "2025/15/01")]
        [DataRow("yyyy-dd-mm", "yyyy-dd-mm")]
        [DataRow("15-01-2025", "15-01-2025")]
        public void VerifyInvalidDateFormat(string fdate, string tdate)
        {
            // Step 1: Input invalid value for From Date, To Date
            myLeavePage.InputFromDateToDate(fdate, tdate);

            // Log Info
            reportHelpers.LogMessage("Info", "From Date: " + fdate);
            reportHelpers.LogMessage("Info", "To Date: " + tdate);

            // Verify that error will be shown in red color at the bottom of the field [From Date]
            // Content of the error is: "Should be a valid date in yyyy-dd-mm format"
            string errMessFromDate = myLeavePage.GetContentErrorMessageOfFromDate();
            Assert.AreEqual(messagesData.DateFormatErr, errMessFromDate);

            // Verify that error will be shown in red color at the bottom of the field [To Date]
            // Content of the error is: "Should be a valid date in yyyy-dd-mm format"
            string errMessToDate = myLeavePage.GetContentErrorMessageOfToDate();
            Assert.AreEqual(messagesData.DateFormatErr, errMessToDate);
        }

        [TestMethod("TC006: Verify Click Reset button")]
        public void VerifyClickResetButton()
        {
            // Step 1: Not select any value of [Leave Status] (Click [x] icon of all values)
            myLeavePage.ClearAllValueForLeaveStatus();
           
            // Step 2: Select a value in [Leave Type]
            myLeavePage.SelectLeaveTypeValue();

            // Click Reset button
            myLeavePage.ClickResetButton();

            // Verify Leave Type dropdown return default value: "--Select--"
            Assert.IsTrue(myLeavePage.IsDefaultValueOfLeaveType());

            // Verify Leave Status dropdown return default value:
            // Includes 5 items (Rejected, Cancelled, Pending Approval, Scheduled, Taken)
            Assert.IsTrue(myLeavePage.IsDefaultValueOfLeaveStatus());
        }

        [TestMethod("TC007: Verify Search Result")]
        public void VerifySearchResult()
        {
            // Create Search data
            PrecondtionForSearch();

            // Step 1: Navigate to MyLeave Page
            viewLeaveListPage.ClickMyLeaveButton();

            // Step 2: Click Search button
            myLeavePage.ClickSearchButton();

            // Verify Table Result header does not return text "No Records Found"
            string headerResult = myLeavePage.GetTableHeader();
            Assert.AreNotEqual(messagesData.NoRecords, headerResult);

            // Verify data count > 0
            int resultCount = myLeavePage.GetMyLeaveList().Count();
            Assert.AreNotEqual(0, resultCount);                      
        }

        public void Precondtion()
        {
            // Go to login page and login successfully
            loginPage.IsLoginSuccessfully();

            // Click [Leave] item on [Left-Menu] (NavigationPage)
            leftMenuPage.ClickLeaveOption();

            // Click [My Leave] button on [Top Menu] (ViewLeaveListPage)
            viewLeaveListPage.ClickMyLeaveButton();
        }

        public void PrecondtionForSearch()
        {
            // Click [Apply] button on [Top Menu] (ViewLeaveListPage)
            viewLeaveListPage.ClickApplyButton();

            // Create test data
            applyPage.CreateApply();
        }
    }
}
