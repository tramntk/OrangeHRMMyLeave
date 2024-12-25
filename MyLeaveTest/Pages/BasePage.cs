using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
