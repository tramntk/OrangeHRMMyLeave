using Automation.WebDriver;
using OpenQA.Selenium;

namespace MyLeaveTest.Pages
{
    public class DashboardPage : BasePage
    {
        public DashboardPage(IWebDriver _driver) : base(_driver)
        {
        }

        // Web elements
        private IWebElement chartTimeAtWork => driver.FindElementByXPath("//div[@class='emp-attendance-chart']");
        private IWebElement dashboardHeader => driver.FindElementByXPath("//h6[text()='Dashboard']");

        // Methods interact
        public bool IsChartTimeAtWorkDisplay()
        {
            return driver.WaitToDisplay(chartTimeAtWork);
        }

        public string GetContentDashboardHeader()
        {
            return dashboardHeader.Text;
        }
    }
}
