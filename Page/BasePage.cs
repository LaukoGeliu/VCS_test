using OpenQA.Selenium;

namespace Automatinis1.Page
{
    public class BasePage
    {
        protected IWebDriver Driver;
        public BasePage(IWebDriver webdriver)
        {
            Driver = webdriver;
        }

        public void CloseBrowser()
        {
            Driver.Quit();
        }

    }
}