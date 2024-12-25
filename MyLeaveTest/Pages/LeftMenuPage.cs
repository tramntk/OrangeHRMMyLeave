﻿using Automation.WebDriver;
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
        private IWebElement adminOption => driver.FindElementByXPath(driver.LeftMenuXPath("Admin"));
        private IWebElement pimOption => driver.FindElementByXPath(driver.LeftMenuXPath("PIM"));
        private IWebElement leaveOption => driver.FindElementByXPath(driver.LeftMenuXPath("Leave"));

        //Methods
        public void ClickAdminOption()
        {
            adminOption.Click();
        }

        public void ClickPIMOption()
        {
            pimOption.Click();
        }

        public void ClickLeaveOption()
        {
            leaveOption.Click();
        }
    }
}
