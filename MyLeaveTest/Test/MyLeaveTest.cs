using MyLeaveTest.Pages;
using OpenQA.Selenium.BiDi.Modules.Log;
using System.Collections.Generic;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class MyLeaveTest: BaseTest
    {
        private ViewLeaveListTest viewLeaveListTest;
        private ViewLeaveListPage viewLeaveListPage;
        private MyLeavePage myLeavePage;


        [TestInitialize]
        public void SetUpMyLeavePage()
        {
            //Init
            viewLeaveListTest = new ViewLeaveListTest();
            viewLeaveListPage = new ViewLeaveListPage(driver);
            myLeavePage = new MyLeavePage(driver);

            viewLeaveListTest.SetUpViewLeaveListTest();

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

        [TestMethod("TC002: Verify that error will be shown when user does not input at mandatory fields")]
        public void VerifyNotInputMandatoryFields()
        {
            //Not select any value of [Status] (Click [x] icon of all values)
            myLeavePage.ClearAllValueForLeaveStatus();

            //Error "Required" will be shown in red color at the bottom of the field[Status]
            myLeavePage.IsRequireMessage();

        }
    }
}
