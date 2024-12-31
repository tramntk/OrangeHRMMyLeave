﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Automation.WebDriver
{
    public static class DriverFactory
    {
        public static IWebDriver InitBrowser(string browserType, int timeout)
        {
            IWebDriver driver;

            switch (browserType)
            {
                case "CHROME":
                    driver = new ChromeDriver();
                    break;
                case "FIREFOX":
                    driver = new FirefoxDriver();
                    break;
                case "EGDE":
                    driver = new EdgeDriver();
                    break;
                default:
                    throw new Exception("Cannot support this driver.");
            }

            //Set Implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);

            driver.Manage().Window.Maximize();

            return driver;
        }
    }
}
