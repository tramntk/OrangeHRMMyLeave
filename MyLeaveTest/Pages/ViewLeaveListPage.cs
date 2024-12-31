using Automation.WebDriver;
using OpenQA.Selenium;

namespace MyLeaveTest.Pages
{
    public class ViewLeaveListPage : BasePage
    {
        public ViewLeaveListPage(IWebDriver _driver) : base(_driver)
        {
        }

        // Web Elements
        private IWebElement myLeaveBtn => driver.FindElementByXPath("//a[text() = 'My Leave']");
        private IWebElement applyBtn => driver.FindElementByXPath("//a[text() = 'Apply']");
        private IWebElement leaveListHeader => driver.FindElementByXPath("//h5[text()='Leave List']");
        
        // Methods
        public void ClickMyLeaveButton()
        {
            myLeaveBtn.Click();
        }

        public void ClickApplyButton()
        {
            applyBtn.Click();
        }

        public string GetLeaveListHeader()
        {
            return leaveListHeader.Text;
        }
    }
}
