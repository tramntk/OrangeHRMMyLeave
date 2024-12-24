using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.WebDriver
{
    public static class DriverExtension
    {
        public static bool Wait(this IWebDriver driver, IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            return wait.Until(d => element.Displayed);
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
