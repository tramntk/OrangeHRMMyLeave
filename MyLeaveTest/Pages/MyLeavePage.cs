using Automation.WebDriver;
using OpenQA.Selenium;

namespace MyLeaveTest.Pages
{
    public class MyLeavePage : BasePage
    {
        public MyLeavePage(IWebDriver _driver) : base(_driver)
        {
        }

        private Dictionary<(int, int), string> tableDictionary;

        // Web elements

        // Header
        private IWebElement myLeaveHeader => driver.FindElementByXPath("//h5[text()='My Leave List']");            
        
        // From Date
        private IWebElement fromDate => driver.FindElementByXPath("//label[contains(text(),'From Date')]/parent::div/following-sibling::div//input");
        
        // To Date
        private IWebElement toDate => driver.FindElementByXPath("//label[contains(text(),'To Date')]/parent::div/following-sibling::div//input");
        
        // Leave Status
        private IList<IWebElement> leaveStatusList => driver.FindElementsByXPath("//span[@class='oxd-chip oxd-chip--default oxd-multiselect-chips-selected']");
        private IList<IWebElement> clearButtonOfLeaveStatusList => driver.FindElementsByXPath("//i[@class='oxd-icon bi-x --clear']");
        
        // Leave Type
        private IWebElement leaveType => driver.FindElementByXPath("//label[text()='Leave Type']/parent::div/following-sibling::div//div[@class='oxd-select-text-input']");
        private IList<IWebElement> leaveTypeOption => driver.FindElementsByXPath("//div[@class = 'oxd-select-option']");
        
        // Buttons
        private IWebElement searchButton => driver.FindElementByXPath("//button[@type='submit']");
        private IWebElement resetButton => driver.FindElementByXPath("//button[@type='reset']");
        
        // Labels
        private IWebElement errMessForLeaveStatus => driver.FindElementByXPath("//label[text()='Show Leave with Status']/../following-sibling::span");
        private IWebElement errMessForToDate => driver.FindElementByXPath("//label[text() = 'To Date']/../following-sibling::span");
        private IWebElement errMessForFromDate => driver.FindElementByXPath("//label[text() = 'From Date']/../following-sibling::span");
        private IWebElement toDateLabel => driver.FindElementByXPath("//label[text()='To Date']");

        // Result table
        private IWebElement resultTableHeader => driver.FindElementByXPath("//span[@class = 'oxd-text oxd-text--span']");
        
        // row list
        private IList<IWebElement> rows => driver.FindElementsByXPath("//div[@role = 'row']");
        

        // Methods
        // Get search result:
        public Dictionary<(int,int), string> GetMyLeaveList()
        {
            // Convert table to Dictionary
            tableDictionary = new Dictionary<(int, int), string>();

            for (int i = 0; i < rows.Count; i++)
            {
                var cells = rows[i].FindElements(By.XPath(".//div[@role = 'cell']"));

                for (int j = 0; j < cells.Count; j++)
                {
                    tableDictionary[(i, j + 1)] = cells[j].Text;
                }
            }

            return tableDictionary;
        }

        public string GetMyLeaveListHeader()
        {
            return myLeaveHeader.Text;
        }

        public bool IsDefaultValueOfLeaveStatus()
        {
            List<string> expectStatusList = new List<string> 
                { "Rejected", "Cancelled", "Pending Approval", "Scheduled", "Taken" };

            List<string> actualStatusList = new List<string>();

            if (leaveStatusList.Count == 5)
            {
                for (int i = 0; i < leaveStatusList.Count; i++)
                {
                    actualStatusList.Add(leaveStatusList[i].Text);
                }

                return expectStatusList.ToHashSet().SetEquals(actualStatusList);
            }
            else
            {
                return false;
            }
            
        }

        public bool IsDefaultValueOfLeaveType()
        {            
            // Verify Leave Type: there is no value selected
            if(leaveType.Text == "-- Select --")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Select a date
        public string SelectDate(int days)
        {
            DateTime today = DateTime.Now;
            DateTime newDate = today.AddDays(days);

            string selectedDate = String.Concat((newDate.Year).ToString(), '-', (newDate.Day).ToString(), '-', (newDate.Month).ToString());

            return selectedDate;
        }

        public void InputFromDateToDate(string fdate, string tdate)
        {
            // Input fromdate
            fromDate.Click();

            fromDate.SendKeys(Keys.Control + 'a');

            fromDate.SendKeys(Keys.Delete);

            fromDate.SendKeys(fdate);

            fromDate.SendKeys(Keys.Enter);

            // Input todate

            toDate.Click();

            toDate.SendKeys(Keys.Control + 'a');

            toDate.SendKeys(Keys.Delete);

            toDate.SendKeys(tdate);

            toDate.SendKeys(Keys.Enter);
            
            driver.DoClickAction(toDateLabel);

        }

        public void SelectLeaveTypeValue()
        {
            driver.DoClickAction(leaveType);
            leaveTypeOption.First().Click();
        }

        public string GetTableHeader()
        {
            return resultTableHeader.Text;
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

        public string GetContentErrorMessageOfFromDate()
        {
            return errMessForFromDate.Text;
        }

        public string GetContentErrorMessageOfToDate()
        {
            return errMessForToDate.Text;
        }

        public void ClickSearchButton()
        {
            searchButton.Click();
        }

        public void ClickResetButton()
        {
            resetButton.Click(); 
        }

        public string ToastMessageContent()
        {            
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                IWebElement toastElement = driver.WaitElementIsVisible("//div[@id = 'oxd-toaster_1']");
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
