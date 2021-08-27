using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Automatinis1.Page
{
    public class SumHWPage : BasePage
    {
        private IWebElement advertisementWindow => Driver.FindElement(By.Id("at-cv-lightbox-close"));
        private IWebElement firstInputField => Driver.FindElement(By.Id("sum1"));
        private IWebElement secondInputField => Driver.FindElement(By.Id("sum2"));
        private IWebElement getTotalButton => Driver.FindElement(By.CssSelector("#gettotal > button"));
        private IWebElement resultFromPage => Driver.FindElement(By.Id("displayvalue"));

        public SumHWPage(IWebDriver webdriver) : base(webdriver) { }
        public void CloseAdvertisementButton()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => advertisementWindow.Displayed);
            advertisementWindow.Click();
        }

        public void InsertFirstInput(string firstInput)
        {
            firstInputField.Clear();
            firstInputField.SendKeys(firstInput);
        }
        public void InsertSecondInput(string secondInput)
        {
            secondInputField.Clear();
            secondInputField.SendKeys(secondInput);
        }

        public void GetTotalSum()
        { getTotalButton.Click(); }

        public void ValuateResult(string result)
        { Assert.AreEqual(result, resultFromPage.Text, $"Actual result differs from expected {result}"); }

    }
}

