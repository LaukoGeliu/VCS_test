using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MembershopTest.Page
{
    public class MembershopSearchResultPage : BasePage
    {
        private IWebElement _searchResultField => Driver.FindElement(By.CssSelector(".sale-title"));
        private IWebElement maxSlider => Driver.FindElement(By.XPath("//span[@id='sn-slider-max']"));
        private IWebElement maxPriceDefault => Driver.FindElement(By.XPath("//input[@class='priceTo currency-width-1']"));
        private IWebElement minSlider => Driver.FindElement(By.XPath("//span[@id='sn-slider-min']"));
        private IReadOnlyCollection<IWebElement> itemPriceFields => Driver.FindElements(By.XPath("//div[@class='text-center']/span//strong"));    //(By.XPath("//*[@id='docs-container']//span//strong"));
        private IWebElement priceToField => Driver.FindElement(By.XPath("//*[@class='priceTo currency-width-1']"));
        private IWebElement _sliderScale => Driver.FindElement(By.Id("sn-slider-scale"));

        public MembershopSearchResultPage(IWebDriver webdriver) : base(webdriver)
        { }

        public void VerifySearchResult(string searchValue)
        {
            GetWait().Until(driver => _searchResultField.Displayed);
            Assert.IsTrue(_searchResultField.Text.ToLower().Contains(searchValue.ToLower()), $"Search result should be {searchValue}, but is {_searchResultField}");
        }

        public void SetMaxPriceBySlider(double maxPrice)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", maxSlider);
            double sliderWidth = maxSlider.Size.Width;
            double sliderScaleWidth = _sliderScale.Size.Width;
            double maxPriceDefult = Int32.Parse(maxPriceDefault.GetAttribute("value"));
            int pixelsMoveToLeft = Convert.ToInt32(Math.Round((-(sliderScaleWidth - (maxPrice * (sliderScaleWidth) / maxPriceDefult))+sliderWidth)));
            Actions action = new Actions(Driver);
            Actions moveSlider = new Actions(Driver);
            action.ClickAndHold(maxSlider).MoveByOffset(pixelsMoveToLeft, 0).Release().Build();
            action.Perform();
        }


        public void ValidateHighestSetPrice(double maxPrice)
        {
            //bool staleElement = true;
            //while (staleElement)
            //{
            //    try
            //    {
            //        driver.FindElement(By.XPath(link_click), 10).Click();
            //        staleElement = false;
            //    }
            //    catch (StaleElementReferenceException e)
            //    {
            //        staleElement = true;
            //    }
            //}

            double[] allPrices = new double[itemPriceFields.Count];
            int i = 0;
            Thread.Sleep(5000);
            foreach (IWebElement price in itemPriceFields)
            {
                allPrices[i++] = Convert.ToDouble(price.Text.Split(' ')[0]);
            }
            Assert.IsTrue(allPrices.Max() < maxPrice, $"Max price {allPrices.Max()} of the item is more than {maxPrice}");

        }



    }
}
