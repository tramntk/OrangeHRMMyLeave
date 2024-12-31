using Automation.Core.Helpers;
using Automation.WebDriver;
using OpenQA.Selenium;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace MyLeaveTest.Test
{
    [TestClass]
    public class BaseTest
    {
        protected IWebDriver driver;
        protected static ReportHelpers reportHelpers;
        public TestContext TestContext { get; set; }
        
        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void SetupExtentReport(TestContext context)
        {
            // Init report
            reportHelpers = new ReportHelpers();
        }     

        [TestInitialize]
        public void SetUpAndOpenBrowser()
        {
            // Init driver 
            string browserType = ConfigurationHelpers.GetValue<string>("browser");
            int timeout = ConfigurationHelpers.GetValue<int>("timeout");

            driver = DriverFactory.InitBrowser(browserType, timeout);

            var testMethod = TestContext?.TestName;

            var method = GetType().GetMethods()
                .FirstOrDefault(m => m.GetCustomAttributes(typeof(TestMethodAttribute), false)
                .Any() && m.Name == testMethod);

            var testMethodAttribute = method.GetCustomAttributes(typeof(TestMethodAttribute), false)
                .Cast<TestMethodAttribute>().FirstOrDefault();

            string testDescription = testMethodAttribute?.DisplayName ?? method.Name;

            reportHelpers.CreateTestCase(TestContext.TestName, testDescription);
        }

        [TestCleanup]
        public void BrowserCleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string base64 = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
                reportHelpers.LogMessage("Fail", "Test case failed", base64);
            }
            else
            {
                reportHelpers.LogMessage("Pass", "Test case passed");
            }
            reportHelpers.ExportReport();
            driver.Quit();
        }

    }
}
