using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Automation.WebDriver
{
    public static class DriverExtension
    {
        private static WebDriverWait wait;
        public static bool WaitToDisplay(this IWebDriver driver, IWebElement element)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            return wait.Until(d => element.Displayed);
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
