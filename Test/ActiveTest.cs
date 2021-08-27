using Automatinis1.Page;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatinis1.Test
{
    class ActiveTest
    {
        private static ActivePage page;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            IWebDriver chrome = new ChromeDriver();
            page = new ActivePage(chrome);

            chrome.Manage().Window.Maximize();
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            chrome.Url = "https://www.active.com/fitness/calculators/pace";
            page.CloseAdvertisement();
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            page.CloseBrowser();
        }

        [TestCase("1", "5", "13", "km", "per km", TestName = "13 km in 1 hour 5 min is 5 km/hour")]
        public static void Test13KmIn1Hour5Min(string hour, string minutes, string distance, string distanceType, string speedTipe)
        {
            page.InsertRunningHours(hour);
            page.InsertRunningMinutes(minutes);
            page.InsertRunningDistance(distance);
            page.SelectRunningDistanceType(distanceType);
            page.SelectRunningSpeedPerHourType(speedTipe);
            page.CountSpeedPerHour();

        }
    }
}
