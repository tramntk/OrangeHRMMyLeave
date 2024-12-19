using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class BaseTest
    {
        protected static IWebDriver driver;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void SetUpAndOpenBrowser(TestContext testContext)
        {
            //Init driver for Google Chrome
            driver = new ChromeDriver();

            //Set Implicit timeout
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void BrowserCleanup()
        {
            driver.Quit();
        }

    }
}
