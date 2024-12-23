using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class BaseTest
    {
        protected static IWebDriver driver;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void Initialize(TestContext test) 
        {
            //Init driver for Google Chrome
            driver = new ChromeDriver();

            //Set Implicit timeout
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

        }


        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void BrowserCleanup()
        {
            driver.Quit();
        }

    }
}
