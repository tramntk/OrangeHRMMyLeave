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
        private IWebElement chartTimeAtWork => driver.FindElement(By.XPath("//div[@class='emp-attendance-chart']"));

        private IWebElement dashboardHeader => driver.FindElement(By.XPath("//h6[text()='Dashboard']"));

        //Methods interact

        public bool IsChartTimeAtWorkDisplay()
        {
            return chartTimeAtWork.Displayed;
        }

        public string GetContentDashboardHeader()
        {
            return dashboardHeader.Text;
        }
    }
}
