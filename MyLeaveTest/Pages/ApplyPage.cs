using Automation.WebDriver;
using OpenQA.Selenium;

namespace MyLeaveTest.Pages
{
    public class ApplyPage : BasePage
    {
        public ApplyPage(IWebDriver _driver) : base(_driver)
        {

        }

        //Web Elements
        private IWebElement fromDate => driver.FindElementByXPath("//label[contains(text(),'From Date')]/parent::div/following-sibling::div//input");
        private IWebElement toDate => driver.FindElementByXPath("//label[contains(text(),'To Date')]/parent::div/following-sibling::div//input");
        private IWebElement toDateLabel => driver.FindElementByXPath("//label[text()='To Date']");
        private IWebElement leaveType => driver.FindElementByXPath("//label[text()='Leave Type']/parent::div/following-sibling::div//div[@class='oxd-select-text-input']");
        private IWebElement leaveTypeOption => driver.FindElementByXPath("//label[text()='Leave Type']//..//following-sibling::div//span[text()='CAN - FMLA']");
        private IWebElement comment => driver.FindElementByXPath("//label[contains(text(),'Comments')]/parent::div/following-sibling::div/textarea");
        private IWebElement applyButton => driver.FindElementByXPath("//button[@type = 'submit']");

        public string SelectDate(int days)
        {
            DateTime today = DateTime.Now;
            DateTime newDate = today.AddDays(days);

            string selectedDate = String.Concat((newDate.Year).ToString(), '-' ,(newDate.Day).ToString(), '-' , (newDate.Month).ToString());

            return selectedDate;
        }

        public void InputFromDateToDate(string fdate, string tdate)
        {
            //input fromdate
            fromDate.Click();

            fromDate.SendKeys(Keys.Control + 'a');

            fromDate.SendKeys(Keys.Delete);

            fromDate.SendKeys(fdate);

            fromDate.SendKeys(Keys.Enter);

            //input todate

            toDate.Click();

            toDate.SendKeys(Keys.Control + 'a');

            toDate.SendKeys(Keys.Delete);

            toDate.SendKeys(tdate);

            toDate.SendKeys(Keys.Enter);

            driver.DoClickAction(toDateLabel);

        }

        public void SelectLeaveTypeValue()
        {
            driver.WaitToDisplay(leaveType);
            driver.DoClickAction(leaveType);
            leaveTypeOption.Click();                         
        }

        public void CreateApply()
        {
            SelectLeaveTypeValue();
            string fdate = SelectDate(1);
            string tdate = SelectDate(1);
            InputFromDateToDate(fdate, tdate);
            comment.SendKeys("tramntk");
            ClickApplyButton();
        }

        public void ClickApplyButton()
        {
            applyButton.Click();
        }

    }
}
