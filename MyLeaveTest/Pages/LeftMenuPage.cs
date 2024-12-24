using Automation.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V129.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeaveTest.Pages
{
    public class LeftMenuPage : BasePage
    {
        public LeftMenuPage(IWebDriver _driver) : base(_driver)
        {
        }

        //Web elements
        private IWebElement viewLeaveModuleOption => driver.FindElementByXPath("//a[@href = '/web/index.php/leave/viewLeaveModule']");

        //Methods
        public void ClickLeaveOption()
        {
            viewLeaveModuleOption.Click();
        }
    }
}
