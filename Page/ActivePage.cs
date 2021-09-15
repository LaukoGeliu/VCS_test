using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershopTest.Page
{
    public class ActivePage : BasePage
    {
        private string distanceType;
        private string paceType;
        private const string PageAddress = "https://www.active.com/fitness/calculators/pace";
        private IWebElement popUpAdvertisement => Driver.FindElement(By.ClassName("virtual-events-modal__inner"));
        private IWebElement popUpAdvertisementCloseButton => Driver.FindElement(By.ClassName("virtual-events-modal__close"));
        //private IWebElement imageAdvertisement => Driver.FindElement(By.CssSelector("#img_anch_CL6kveG0yvICFchV4Aodq5cIjQ > img"));
        // private IWebElement imageAdvertisementCLoseButton => Driver.FindElement(By.CssSelector("#cbb > svg"));
        private IWebElement runningTimeHours => Driver.FindElement(By.Name("time_hours"));
        private IWebElement runningTimeMinutes => Driver.FindElement(By.Name("time_minutes"));
        private IWebElement runningDistance => Driver.FindElement(By.Name("distance"));
        private IWebElement dropdownDistanceTypeElement => Driver.FindElement(By.XPath("//span//*[@class='selectboxit ignore_selectbox selectboxit-enabled selectboxit-btn'][@name='distance_type']"));
        private IWebElement dropdownDistanceTypeKm => Driver.FindElement(By.XPath($"//ul//li[@data-val='km'][contains(.,'{distanceType}')]"));
        private IWebElement dropdownSpeedPerHourTypeField => Driver.FindElement(By.XPath("//span//*[@class='selectboxit ignore_selectbox selectboxit-enabled selectboxit-btn'][@name='pace_type']"));
        private IWebElement dropdownSpeedPerHourTypePerKm => Driver.FindElement(By.XPath($"//span//*[@data-val='km'][contains(.,'{paceType}')]"));
        private IWebElement calculateButton => Driver.FindElement(By.CssSelector(".btn.btn-medium-yellow.calculate-btn"));
        private IWebElement paceResultMin => Driver.FindElement(By.XPath("//input[@name='pace_minutes']"));
        private IWebElement paceResultHours => Driver.FindElement(By.XPath("//input[@name='pace_hours']"));
        private IWebElement paceResultSeconds => Driver.FindElement(By.XPath("//input[@name='pace_seconds']"));
        private IWebElement resetButton => Driver.FindElement(By.XPath("//*[@class='reset']"));
        public ActivePage(IWebDriver webdriver) : base(webdriver) { }

        public void NavigateToPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
        }

        public void CloseAdvertisement()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => popUpAdvertisement.Displayed);
            popUpAdvertisementCloseButton.Click();
        }
        public void AcceptCookies()
        {
            Cookie myCookie = new Cookie("OptanonAlertBoxClosed",
                "2021-09-05T16:39:14.308Z",
                ".active.com",
                "/",
                DateTime.Now.AddDays(5));
            Driver.Manage().Cookies.AddCookie(myCookie);
            Driver.Navigate().Refresh();
        }

        public void InsertRunningHours(string hours)
        { runningTimeHours.SendKeys(hours); }

        public void InsertRunningMinutes(string hours)
        { runningTimeMinutes.SendKeys(hours); }

        public void InsertRunningDistance(string distance)
        { runningDistance.SendKeys(distance); }

        public void SelectRunningDistance(string distanceType)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", dropdownDistanceTypeElement);
            GetWait(10);
            dropdownDistanceTypeElement.Click();
            dropdownDistanceTypeKm.Click();
        }

        public void SelectRunningPacePerHourType(string paceType)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", dropdownSpeedPerHourTypeField);
            GetWait(10);
            dropdownSpeedPerHourTypeField.Click();
            dropdownSpeedPerHourTypePerKm.Click();
        }

        public void CountPacePerHour()
        {
            calculateButton.Click();
        }

        public void ValidatePaceResult(string resultMin)
        {
            Assert.AreEqual(resultMin, paceResultMin.GetAttribute("value"), $"Speed result min is {paceResultMin.Text}, should be {resultMin}");
        }
        public void Reset()
        {
            resetButton.Click();
        }


    }
}
