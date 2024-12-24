using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebDriver
{
    public static class BrowserExtension
    {
        public static void GoTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static IWebElement FindElementByXPath(this IWebDriver driver, string xpath)
        {
            return driver.FindElement(By.XPath(xpath));
        }

        public static IList<IWebElement> FindElementsByXPath(this IWebDriver driver, string xpath)
        {
            return driver.FindElements(By.XPath(xpath));
        }
    }
}
