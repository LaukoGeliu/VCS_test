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
    class W3SchoolTest
    {
        private static W3SchoolPage page;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            IWebDriver chrome = new ChromeDriver();
            page = new W3SchoolPage(chrome);
            chrome.Manage().Window.Maximize();
            chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            page.NavigateToPage();
            page.CloseCookies();
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            page.CloseBrowser();
        }

        [TestCase("Volvo", TestName = "Test dropdown with one - Volvo -  value")]
        [TestCase("Saab", "Opel", TestName = "Test dropdown with two - Saab, Opel - values")]
        [TestCase("Volvo", "Saab", "Opel", "Audi", TestName = "Test dropdown with all - Volvo, Saab, Opel, Audi - values")]
        public static void TestCarsDropdown(params string[] cars)
        {

            List<string> carsList = cars.ToList();
            page.SelectCarsByText(carsList);
            page.ClickSubmitButton();
            page.VerifySelectedCarsResult(carsList);
        }
    }

}