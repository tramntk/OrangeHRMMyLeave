using OpenQA.Selenium;

namespace MyLeaveTest.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        public BasePage(IWebDriver _driver)
        {
            driver = _driver;
        }

    }
}
