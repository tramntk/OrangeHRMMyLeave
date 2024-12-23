using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace MyLeaveTest.Pages
{
    public class MyLeavePage : BasePage
    {
        public MyLeavePage(IWebDriver _driver) : base(_driver)
        {

        }

        WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(50));

        //Web elements

        private IWebElement myLeaveHeader => driver.FindElement(By.XPath("//h5[text()='My Leave List']"));            
        private IWebElement fromDate => driver.FindElement(By.XPath("//label[contains(text(),'From Date')]/parent::div/following-sibling::div//input"));
        private IWebElement toDate => driver.FindElement(By.XPath("//label[text()='To Date']/parent::div/parent::div//input"));
        private IWebElement leaveStatusDropdown => driver.FindElement(By.XPath("//label[text()='Show Leave with Status']/parent::div/parent::div//div[@class='oxd-select-text--after']"));
        private IList<IWebElement> leaveStatusList => driver.FindElements(By.XPath("//span[@class='oxd-chip oxd-chip--default oxd-multiselect-chips-selected']"));
        private IList<IWebElement> clearButtonOfLeaveStatusList => driver.FindElements(By.XPath("//i[@class='oxd-icon bi-x --clear']"));
        private IWebElement leaveType => driver.FindElement(By.XPath("//label[text()='Leave Type']/parent::div/parent::div//div[@class='oxd-select-text--after']"));               
        private IWebElement searchButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        private IWebElement resetButton => driver.FindElement(By.XPath("//button[@type='reset']"));
        private IWebElement cancelButtonOnTopOfTable => driver.FindElement(By.XPath("//div[@class='actions']/button"));
        private IWebElement resultTableHeader => driver.FindElement(By.XPath("//span[@class = 'oxd-text oxd-text--span']"));        
        private IWebElement errMessForLeaveStatus => driver.FindElement(By.XPath("//label[text()='Show Leave with Status']/../following-sibling::span"));
        private IWebElement errMessForToDate => driver.FindElement(By.XPath("//label[text() = 'To Date']/../following-sibling::span"));


        //Methods
        public string GetMyLeaveListHeader()
        {
            return myLeaveHeader.Text;
        }

        public bool IsAllFiltersHaveDefaultValue()
        {
            bool result = false;

            wait.Until(d => fromDate.Displayed);          
            
            //Verify default value of to Date (end date of the current year)


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

        //Select a date
        public string SelectDate(int year, int day, int month)
        {


            int currentYear = ((int)DateTime.Now.Year);
            int currentMonth = DateTime.Now.Month;
            int currentDate = DateTime.Now.Date.Day;

            string selectedDate = String.Concat((currentYear + year).ToString(), '-', (currentDate+ day).ToString(), '-', (currentMonth + month).ToString());

            return selectedDate;
        }

        public void InputFromDateToDate(string fdate, string tdate)
        {
            //input from date
            fromDate.Click();

            fromDate.SendKeys(Keys.Control + 'a');

            fromDate.SendKeys(Keys.Delete);

            fromDate.SendKeys(fdate);

            fromDate.SendKeys(Keys.Enter);

            //input to date
            toDate.Click();

            toDate.SendKeys(Keys.Control + 'a');

            toDate.SendKeys(Keys.Delete);

            toDate.SendKeys(tdate);

            toDate.SendKeys(Keys.Enter);

            Actions actions = new Actions(driver);

            actions.Click(driver.FindElement(By.XPath("//label[text()='To Date']"))).Perform();

        }

        public bool IsNoRecordsFoundHeader()
        {
            if (resultTableHeader.Text == "No Records Found")
            {
                return true;
            }
            else { return false; }
        }

        public void ClearAllValueForLeaveStatus()
        {
            Actions actions = new Actions(driver);

            for(int i = 4; i >= 0; i--)
            {
                actions.Click(clearButtonOfLeaveStatusList[i]).Perform();
            }
        }

        public bool IsRequireMessage()
        {
            Assert.AreEqual("Required", errMessForLeaveStatus.Text);
            return true;
        }

        public string GetContentErrorMessageWhenFromDateGreaterThanToDate()
        {
            return errMessForToDate.Text;
        }

        public void ClickSearchButton()
        {
            searchButton.Click();
        }

        public string ToastMessageContent()
        {    
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                IWebElement toastElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@id = 'oxd-toaster_1']")));
                string toastText = (string)js.ExecuteScript("return arguments[0].innerText;", toastElement);
                Console.WriteLine("Toast Message: " + toastText);
                return toastText;
            }
            catch (NoSuchElementException)
            {
                return "Toast message not found.";
            }
        }
    }
}
