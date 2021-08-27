using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatinis1.Page
{
    public class ActivePage : BasePage
    {
        private IWebElement popUpAdvertisement => Driver.FindElement(By.ClassName("virtual-events-modal__inner"));
        private IWebElement popUpAdvertisementCloseButton => Driver.FindElement(By.ClassName("virtual-events-modal__close"));
        //private IWebElement imageAdvertisement => Driver.FindElement(By.CssSelector("#img_anch_CL6kveG0yvICFchV4Aodq5cIjQ > img"));
        // private IWebElement imageAdvertisementCLoseButton => Driver.FindElement(By.CssSelector("#cbb > svg"));

        private IWebElement runningTimeHours => Driver.FindElement(By.Name("time_hours"));

        internal void Quit()
        {
            throw new NotImplementedException();
        }

        private IWebElement runningTimeMinutes => Driver.FindElement(By.Name("time_minutes"));

        private IWebElement runningDistance => Driver.FindElement(By.Name("distance"));
        private SelectElement dropdownDistanceType => new SelectElement(Driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(3) > div > select")));

        private SelectElement dropdownSpeedPerHourType => new SelectElement(Driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(4) > div > span > span > span.selectboxit-text")));

        private IWebElement calculateButton => Driver.FindElement(By.ClassName("btn btn-medium-yellow calculate-btn"));
        public ActivePage(IWebDriver webdriver) : base(webdriver) { }

        public void CloseAdvertisement()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => popUpAdvertisement.Displayed);
            popUpAdvertisementCloseButton.Click();
        }

        public void InsertRunningHours(string hours)
        { runningTimeHours.SendKeys(hours); }

        public void InsertRunningMinutes(string hours)
        { runningTimeMinutes.SendKeys(hours); }

        public void InsertRunningDistance(string distance)
        { runningDistance.SendKeys(distance); }

        public void SelectRunningDistanceType(string distanceType)
        {
            dropdownDistanceType.SelectByValue(distanceType);
        }

        public void SelectRunningSpeedPerHourType(string type)
        {
            dropdownSpeedPerHourType.SelectByText(type);
        }

        public void CountSpeedPerHour()
        {
            calculateButton.Click();
        }


    }
}
