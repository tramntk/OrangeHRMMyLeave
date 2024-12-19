using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Log;
using System.Numerics;

namespace MyLeaveTest.Pages
{
    public class MyLeavePage : BasePage
    {
        public MyLeavePage(IWebDriver _driver) : base(_driver)
        {
        }

        //Web elements

        private IWebElement myLeaveHeader => driver.FindElement(By.XPath("//h5[text()='My Leave List']"));

        private IWebElement fromDate => driver.FindElement(By.XPath("//label[text()='From Date']/parent::div/parent::div//input"));
        private IWebElement toDate => driver.FindElement(By.XPath("//label[text()='To Date']/parent::div/parent::div//input"));

        private IWebElement leaveStatusDropdown => driver.FindElement(By.XPath("//label[text()='Show Leave with Status']/parent::div/parent::div//div[@class='oxd-select-text--after']"));

        private IList<IWebElement> leaveStatusList => driver.FindElements(By.XPath("//span[@class='oxd-chip oxd-chip--default oxd-multiselect-chips-selected']"));

        private IList<IWebElement> clearButtonOfLeaveStatusList => driver.FindElements(By.XPath("//i[@class='oxd-icon bi-x --clear']"));


        private IWebElement leaveType => driver.FindElement(By.XPath("//label[text()='Leave Type']/parent::div/parent::div//div[@class='oxd-select-text--after']"));

        
        private IWebElement searchButton => driver.FindElement(By.XPath("//button[@type='submit']"));

        private IWebElement resetButton => driver.FindElement(By.XPath("//button[@type='reset']"));

        private IWebElement cancelButtonOnTopOfTable => driver.FindElement(By.XPath("//div[@class='actions']/button"));

        private IWebElement resultTableHeader => driver.FindElement(By.XPath("//div[@class='actions']/span"));
        private IWebElement errMessForLeaveStatus => driver.FindElement(By.XPath("//div[@class = 'oxd-multiselect-wrapper']/../../span"));



        //Methods
        public string GetMyLeaveListHeader()
        {
            return myLeaveHeader.Text;
        }

        public bool IsAllFiltersHaveDefaultValue()
        {
            bool result = false;

            //Verify default value of from Date (start date of the current year)
            /*
            if (fromDate.Text == DateTime.Now.ToString())
            {
               result = true;
            }
            */

            //Verify default value of to Date (end date of the current year)
            /*
            if (toDate.Text == DateTime.Now.ToString())
            {
               result = true;
            }
            */

            // Verify Leave Status: all values (5) are selected("Rejected, "Cancelled", "Pending Approval", "Scheduled","Taken")
            if (leaveStatusList.Count == 5)
            {                
                result = true;
            }
            else
            {
                result = false;
            }

            // Verify Leave Type: there is no value selected
            if(leaveType.Text == "-- Select --")
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public void ClearAllValueForLeaveStatus()
        {
            foreach (IWebElement item in clearButtonOfLeaveStatusList)
            {
                try
                {
                    if (item.Displayed)
                    {
                        item.Click();
                    }
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("Phần tử không còn tồn tại trong DOM.");
                }
            }
        }

        public bool IsRequireMessage()
        {
            Assert.AreEqual("Required", errMessForLeaveStatus.Text);
            return true;
        }

        public void ClickSearchButton()
        {
            searchButton.Click();
        }
    }
}
