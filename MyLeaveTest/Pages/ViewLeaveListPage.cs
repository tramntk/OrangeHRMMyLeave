using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeaveTest.Pages
{
    public class ViewLeaveListPage : BasePage
    {
        public ViewLeaveListPage(IWebDriver _driver) : base(_driver)
        {
        }

        //Web Elements
        private IWebElement myLeaveBtn => driver.FindElement(By.XPath("//a[text() = 'My Leave']"));

        private IWebElement leaveListHeader => driver.FindElement(By.XPath("//h5[text()='Leave List']"));

        //Methods
        public void ClickMyLeaveButton()
        {
            myLeaveBtn.Click();
        }

        public string GetLeaveListHeader()
        {
            return leaveListHeader.Text;
        }
    }
}
