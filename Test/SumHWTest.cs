using Automatinis1.Page;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Automatinis1.Test
{
    class SumHWTest
    {
        private static SumHWPage page;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            IWebDriver _driver = new ChromeDriver();
            page = new SumHWPage(_driver);
            _driver.Url = "http://www.seleniumeasy.com/test/basic-first-form-demo.html";
            _driver.Manage().Window.Maximize();
            page.CloseAdvertisementButton();
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            page.CloseBrowser();
        }

        [TestCase("2", "2", "4", TestName = "2 plus 2 = 4")]
        [TestCase("-5", "3", "-2", TestName = "-5 plus 3 = -2")]
        [TestCase("a", "b", "NaN", TestName = "a plus b = NaN")]
        public static void TestSumCalculation(string firstInput, string secondInput, string result)
        {
            page.InsertFirstInput(firstInput);
            page.InsertSecondInput(secondInput);
            page.GetTotalSum();
            page.ValuateResult(result);
        }

    }
}
