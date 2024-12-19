using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V129.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeaveTest.Pages
{
    public class NavigationPage : BasePage
    {
        public NavigationPage(IWebDriver _driver) : base(_driver)
        {
        }

        //Web elements
        private IWebElement viewLeaveModuleOption => driver.FindElement(By.XPath("//a[@href = '/web/index.php/leave/viewLeaveModule']"));

        //Methods
        public void ClickLeaveOption()
        {
            viewLeaveModuleOption.Click();
        }
    }
}
