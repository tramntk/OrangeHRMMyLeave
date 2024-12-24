using Automation.Core.Helpers;
using Automation.WebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class BaseTest
    {
        protected IWebDriver driver;

        [TestInitialize]
        public void SetUpAndOpenBrowser() 
        {
            //Init driver 
            string browserType = ConfigurationHelpers.GetValue<string>("browser");
            int timeout = ConfigurationHelpers.GetValue<int>("timeout");

            driver = DriverFactory.InitBrowser(browserType, timeout);
        }

        [TestCleanup]
        public void BrowserCleanup()
        {
            driver.Quit();
        }

    }
}
