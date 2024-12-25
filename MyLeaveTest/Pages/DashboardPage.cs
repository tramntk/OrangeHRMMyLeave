using Automation.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeaveTest.Pages
{
    public class DashboardPage : BasePage
    {
        public DashboardPage(IWebDriver _driver) : base(_driver)
        {
        }

        //Web elements
        private IWebElement chartTimeAtWork => driver.FindElementByXPath("//div[@class='emp-attendance-chart']");
        private IWebElement dashboardHeader => driver.FindElementByXPath("//h6[text()='Dashboard']");

        //Methods interact
        public IWebElement ChartTimeAtWorkDisplay()
        {
            return chartTimeAtWork;
        }

        public string GetContentDashboardHeader()
        {
            return dashboardHeader.Text;
        }
    }
}
