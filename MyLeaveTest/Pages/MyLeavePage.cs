using Automation.WebDriver;
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

        private IWebElement myLeaveHeader => driver.FindElementByXPath("//h5[text()='My Leave List']");            
        private IWebElement fromDate => driver.FindElementByXPath("//label[contains(text(),'From Date')]/parent::div/following-sibling::div//input");
        private IWebElement toDate => driver.FindElementByXPath("//label[text()='To Date']/parent::div/parent::div//input");
        private IWebElement leaveStatusDropdown => driver.FindElementByXPath("//label[text()='Show Leave with Status']/parent::div/parent::div//div[@class='oxd-select-text--after']");
        private IList<IWebElement> leaveStatusList => driver.FindElementsByXPath("//span[@class='oxd-chip oxd-chip--default oxd-multiselect-chips-selected']");
        private IList<IWebElement> clearButtonOfLeaveStatusList => driver.FindElementsByXPath("//i[@class='oxd-icon bi-x --clear']");
        private IWebElement leaveType => driver.FindElementByXPath("//label[text()='Leave Type']/parent::div/parent::div//div[@class='oxd-select-text--after']");               
        private IWebElement searchButton => driver.FindElementByXPath("//button[@type='submit']");
        private IWebElement resetButton => driver.FindElementByXPath("//button[@type='reset']");
        private IWebElement cancelButtonOnTopOfTable => driver.FindElementByXPath("//div[@class='actions']/button");
        private IWebElement resultTableHeader => driver.FindElementByXPath("//span[@class = 'oxd-text oxd-text--span']");        
        private IWebElement errMessForLeaveStatus => driver.FindElementByXPath("//label[text()='Show Leave with Status']/../following-sibling::span");
        private IWebElement errMessForToDate => driver.FindElementByXPath("//label[text() = 'To Date']/../following-sibling::span");
        private IWebElement toDateLabel => driver.FindElementByXPath("//label[text()='To Date']");

        //Methods
        public string GetMyLeaveListHeader()
        {
            return myLeaveHeader.Text;
        }

        public bool IsAllFiltersHaveDefaultValue()
        {
            bool result = false;       
            
            //Verify default value of to Date (end date of the current year)
            //TBD

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

            driver.DoClickAction(toDateLabel);

        }

        public bool IsNoRecordsFoundHeader()
        {
            if (resultTableHeader.Text == "No Records Found")
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
       
        public void ClearAllValueForLeaveStatus()
        {
            for (int i = 4; i >= 0; i--)
            {
                driver.DoClickAction(clearButtonOfLeaveStatusList[i]);
            }
        }

        public string GetRequireMessage()
        {
           return errMessForLeaveStatus.Text;            
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
                return toastText;
            }
            catch (NoSuchElementException)
            {
                return "Toast message not found.";
            }
        }
    }
}
