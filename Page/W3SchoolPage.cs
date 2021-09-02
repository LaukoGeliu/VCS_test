using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatinis1.Page
{
    class W3SchoolPage : BasePage
    {
        private const string AddressUrl = "https://www.w3schools.com/tags/tryit.asp?filename=tryhtml_select_multiple";

        private IWebElement cookiesWindow => Driver.FindElement(By.ClassName("sn-logo"));
        private IWebElement closeCookiesButton => Driver.FindElement(By.Id("accept-choices"));
        private SelectElement carsNamesDropdown => new SelectElement(Driver.FindElement(By.Id("cars")));
        private IWebElement submitButton => Driver.FindElement(By.CssSelector("body>form>input[type=submit]"));
        private IWebElement resultField => Driver.FindElement(By.CssSelector(".w3-container.w3-large.w3-border"));
        private IWebElement runButton => Driver.FindElement(By.ClassName("w3-button.w3-bar-item.w3-hover-white.w3-hover-text-green"));

        public W3SchoolPage(IWebDriver webdriver) : base(webdriver) { }

        public void NavigateToPage()
        {
            if (Driver.Url != AddressUrl)
                Driver.Url = AddressUrl;
        }

        public void CloseCookies()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => cookiesWindow.Displayed);
            closeCookiesButton.Click();
        }

        public void SelectCarsByText(List<string> cars)
        {
            Driver.SwitchTo().Frame("iframeResult");
            carsNamesDropdown.DeselectAll();
            Actions action = new Actions(Driver);
            action.KeyDown(Keys.Control);
            IList<IWebElement> allOptions = carsNamesDropdown.Options;
            foreach (string car in cars)
            {
                foreach (IWebElement option in allOptions)
                    if (option.Text.Equals(car) && !option.Selected)
                    {
                        carsNamesDropdown.SelectByText(car);
                        //Driver.FindElement(By.Id("cars")).FindElement(By.XPath($"//option[. = '{car}']")).Click();
                    }
            }
            action.KeyUp(Keys.Control);
            action.Build().Perform();
        }


        public void ClickSubmitButton()
        { submitButton.Click(); }

        public void VerifySelectedCarsResult(List<string> cars)
        {
            foreach (string car in cars)
            {
                Assert.IsTrue(resultField.Text.Contains(car.ToLower()), $"{car} car is not present, {resultField.Text}");
            }
        }

        public void RestartPage ()
        {
            Driver.SwitchTo().DefaultContent();
            runButton.Click();
        }
    }
}