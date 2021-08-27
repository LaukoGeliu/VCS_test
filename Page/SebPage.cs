using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatinis1.Page
{
    public class SebPage : BasePage
    {
        private IWebElement cookiesWindow => Driver.FindElement(By.ClassName("content-cookie-message"));
        private IWebElement closeCookiesWindow => Driver.FindElement(By.ClassName("main accept-selected"));
        private IWebElement bustoSkaiciuokleWindow => Driver.FindElement(By.ClassName("rtecenter cta-bottom cta-margin-100 mobile-hide"));

        private SelectElement dropdownDistanceType => new SelectElement(Driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(3) > div > select")));
        public SebPage(IWebDriver webdriver) : base(webdriver) { }

        public void closeAdvertisement()
        {
        }


    }
}
