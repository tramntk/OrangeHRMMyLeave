using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebDriver
{
    public static class BrowserExtension
    {
        private static WebDriverWait wait;

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

        public static string LeftMenuXPath(this IWebDriver driver, string elementName)
        {
            return $"//span[text() = '{elementName}']";
        }
        
        public static bool WaitToDisplay(this IWebDriver driver, IWebElement element)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            return wait.Until(d => element.Displayed);
        }

        public static IWebElement WaitToClick(this IWebDriver driver, IWebElement element)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            return wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static IWebElement WaitElementIsVisible(this IWebDriver driver, string xpath)
        {
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            return element;
        }

        public static void DoClickAction(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.Click(element).Perform();
        }

        public static void DoHoverAction(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
        }

    }
}
