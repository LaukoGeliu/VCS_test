using MembershopTest.Page;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershopTest.Test
{
    public class MembershopTest : BaseTest
    {
        [TestCase("user", "password", false, "Neteisingas el. paštas arba slaptažodis!", TestName = "Test wrong user login")] //reikia paziureti, kaip pakeisti, ka tikrinti - teisinga logina arba ne
        [TestCase("gdz.testing@gmail.com", "KLAjoklis357", true, "Sveiki, Monika", TestName = "Test correct user login")]

        public static void TestLogin(string login, string password, bool loginSuccess, string resultLoginMessage)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.SelectLogIn();
            _membershopHomePage.InsertUserLoginData(login, password);
            _membershopHomePage.ClickLoginButton();
            _membershopHomePage.VerifyLoginResult(loginSuccess, resultLoginMessage);
        }

        [TestCase("PUDRA", 40, TestName = "Test search field and price slide")]
        public static void TestSearchAndSlide(string searchValue, int maxPrice)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.InsertSearchValue(searchValue);
            _membershopSearchResultPage.VerifySearchResult(searchValue);
           // _membershopSearchResultPage.SetMaxPriceBySlider(maxPrice);
            _membershopSearchResultPage.SetMaxPriceBySlider_another(maxPrice);
        }

        [TestCase("Papuošalai su natūraliais akmenimis", "Papuošalai", "Papuošalai su natūraliais akmenimis", "Moterims", TestName = "Test navigation to cart per discount category")]
        [TestCase("Babaria", "Moterims", "Babaria", "Moterims", TestName = "naujas")]

        public static void TestNavigationToCartPerDiscountCategory(string saleCategoryTitle, string targetGroup, string saleCategoryTitleValidation, string targetGroupValidation)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.NavigateToAndOpenSaleCategory(saleCategoryTitle); // ties šituo lūžta, nors reikiamą linką atidaro
            _membershopHomePage.SelectTargetGroup(targetGroup); //kitas puslapis?
            _membershopHomePage.ValidateTargetGroupAndSaleCategory(saleCategoryTitleValidation, targetGroupValidation);
        }

        [TestCase("lūpų dažai", TestName = "naujas koks")]
        public static void TestBrandSelectionAndExpressDeliveryOption(string searchValue)//, params string[] brands)
        {
            _membershopHomePage.NavigateToPage();
            _membershopHomePage.AcceptCookies();
            _membershopHomePage.InsertSearchValue(searchValue);
            _membershopSearchResultPage.VerifySearchResult(searchValue);
            _membershopHomePage.SelectBrandsCheckboxes();//brands);
        }

        //public void SelectFromDropDownByValue(List<string> cars)
        //{
        //    Driver.SwitchTo().Frame("iframeResult");
        //    carsDropdown.DeselectAll();
        //    Actions action = new Actions(Driver);
        //    action.KeyDown(Keys.Control);
        //    foreach (IWebElement option in carsDropdown.Options)
        //    {
        //        if (cars.Contains(option.Text) && !option.Selected)
        //        {
        //            option.Click();
        //        }
        //    }
        //    action.KeyUp(Keys.Control);
        //    action.Build().Perform();
        //}
    }
} /// gali atsidaryti reklama "class="inner no-fix not-logged not-logged modal-open", u=daryti : //*[@class='close']
