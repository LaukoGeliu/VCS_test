using OpenQA.Selenium;
using System.Collections.Generic;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Chrome;
using System;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using System.Linq;

namespace MembershopTest.Page
{
    public class MembershopHomePage : BasePage
    {
        private const string PageAddress = "https://membershop.lt/#";
        private IWebElement _searchResultField => Driver.FindElement(By.CssSelector(".sale-title"));

        private IWebElement _loginDirection => Driver.FindElement(By.ClassName("not-logged-login"));
        private IWebElement _loginPopUp => Driver.FindElement(By.ClassName("modal-form"));
        private IWebElement _emailInsertField => Driver.FindElement(By.Id("LoginEmail"));
        private IWebElement _passwordInsertField => Driver.FindElement(By.Id("LoginPwd"));
        private IWebElement _loginButton => Driver.FindElement(By.Id("Login"));
        private IWebElement _wrongLoginMessage => Driver.FindElement(By.ClassName("errorLine"));
        private IWebElement _userAccountWindow => Driver.FindElement(By.XPath("//div[@id='dropdownMenu1']/a"));
        private IWebElement _correctLoginMessage => Driver.FindElement(By.XPath("//div[@id='dropdownMenu1']/a")); // perkelti į LoginPage
        private IWebElement _logoutButton => Driver.FindElement(By.LinkText("Atsijungti"));// perkelti į LoginPage
        private IWebElement _loginPopUpFieldCloseButton => Driver.FindElement(By.CssSelector("#loginModal>div>div>button"));
        private IWebElement _searchField => Driver.FindElement(By.ClassName("sn-suggest-input"));

        //private IWebElement _discountCategoryFieldCosmetics => Driver.FindElement(By.XPath("//*[@class='item col-md-4 col-sm-6 col-xs-12']//*[@data-sale_title='Babaria, Chi ir kt.: TOP kosmetikos prekiniai ženklai ']"));
        private IReadOnlyCollection<IWebElement> _discountCategoryFields => Driver.FindElements(By.XPath("//*[@class='item col-md-4 col-sm-6 col-xs-12']//*[@data-sale_title]"));
        private IReadOnlyCollection<IWebElement> _targetGroupFields => Driver.FindElements(By.XPath("//*[@class='sale-block hidden-xs hidden-sm']//a[@class='behat-category']")); //("//*[@class='mobile-sale-targets clearfix']//*[@class='behat-category']"));; // nuolaidos kategorijos grup4s - puslapis
        private IWebElement _saleCategoryTitle => Driver.FindElement(By.ClassName("sale-title"));
        private IWebElement _targetGroup => Driver.FindElement(By.XPath("//div[@class='sales-side-target']"));
        private IReadOnlyCollection<IWebElement> _brandsCheckboxes => Driver.FindElements(By.XPath("//div[@data-sn-container='s_title']//div[@class='searchnode ']"));  //("//*[@id='mCSB_36_container']//div[@class='searchnode ']"));
        private IWebElement _deselectBrandsAllCheckboxesOption => Driver.FindElement(By.XPath("//*[@class='clear-filter'][@data-clear-filter='s_title']"));

        public MembershopHomePage(IWebDriver webdriver) : base(webdriver)
        { }

        public void VerifySearchResult(string searchResult)
        {
            GetWait().Until(driver => _searchResultField.Displayed);
            Assert.IsTrue(_searchResultField.Text.ToUpper().Contains(searchResult), $"Search result should be {searchResult}, but is {_searchResultField}");
        }
        public void NavigateToPage()
        {
            if (Driver.Url != PageAddress)
                Driver.Url = PageAddress;
        }


        public void AcceptCookies()
        {
            Cookie myCookie = new Cookie("iCookiePermissionLevel",
                "0", ".membershop.lt",
                "/",
                DateTime.Now.AddDays(5));
            Driver.Manage().Cookies.AddCookie(myCookie);
            Driver.Navigate().Refresh();
        }
        public void SelectLogIn()
        {
            _loginDirection.Click();
        }

        public void InsertUserLoginData(string login, string password)
        {
            GetWait().Until(d => _loginPopUp.Displayed);
            _emailInsertField.SendKeys(login);
            _passwordInsertField.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            _loginButton.Click();
        }

        public void VerifyLoginResult(bool shouldBeLogged, string resultLoginMessage) // // perkelti į LoginPage?
        {
            if (shouldBeLogged == false)
            {
                GetWait().Until(d => _loginPopUp.Displayed);
                Assert.AreEqual(resultLoginMessage, _wrongLoginMessage.Text, $"Message for wrong Login data should be {resultLoginMessage}, but is {_wrongLoginMessage}");
                _loginPopUpFieldCloseButton.Click();
            }

            else
            {
                GetWait().Until(d => _userAccountWindow.Displayed);
                Assert.AreEqual(resultLoginMessage, _correctLoginMessage.Text, $"Message for correct Login data should be {resultLoginMessage}, but is {_correctLoginMessage}");
                Actions action = new Actions(Driver);
                action.MoveToElement(_userAccountWindow);
                action.Build().Perform();
                _logoutButton.Click();
            }
        }

        public void InsertSearchValue(string searchValue)
        {
            _searchField.SendKeys(searchValue);
            Actions action = new Actions(Driver);
            action.SendKeys(Keys.Enter);
            action.Build().Perform();
        }
        public void NavigateToAndOpenSaleCategory(string saleCategoryTitle)
        {
            foreach (IWebElement saleCategory in _discountCategoryFields)
            {
                //GetWait().Until(d => saleCategory.Displayed);
                if (saleCategory.GetAttribute("data-sale_title").Contains(saleCategoryTitle)) // bet puslapi atidaro - teisingai pagal xpath
                {
                    //GetWait().Until(d => saleCategory.Displayed);
                    //Actions action = new Actions(Driver);
                    //action.MoveToElement(saleCategory);
                    //action.Build().Perform(); 
                    saleCategory.Click();
                    break;
                }
            }
        }
        public void SelectTargetGroup(string targetGroup) //perkelti į DiscountCategoryPage
        {

            foreach (IWebElement target in _targetGroupFields)
            {
                //GetWait().Until(d => saleCategory.Displayed);
                if (target.Text.ToLower().Contains(targetGroup.ToLower())) // bet puslapi atidaro - teisingai pagal xpath
                {
                    //GetWait().Until(d => saleCategory.Displayed);
                    //Actions action = new Actions(Driver);
                    //action.MoveToElement(saleCategory);
                    //action.Build().Perform(); 
                    target.Click();

                }
            }
        }
        public void ValidateTargetGroupAndSaleCategory(string saleCategoryTitleValidation, string targetGroupValidation) //perkelti į DiscountCategoryPage
        {
            Assert.AreEqual(saleCategoryTitleValidation.ToLower(), _saleCategoryTitle.Text.ToLower(), $"Sale target is {_saleCategoryTitle.Text}, but should be {saleCategoryTitleValidation}");
            Assert.AreEqual(targetGroupValidation.ToLower(), _targetGroup.Text.ToLower(), $"Sale target is {_targetGroup.Text}, but should be {targetGroupValidation}");
        }

        public void SelectBrandsCheckboxes()//params string[] brandNames)  // cia irgi meta klaida
        {               
            //_deselectBrandsAllCheckboxesOption.Click();
Actions action = new Actions(Driver);
            foreach (IWebElement checkbox in _brandsCheckboxes)
            {
                
                GetWait().Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='mobile-tab-section filter-block'][@data-sn-container='s_title']")));
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", checkbox);
                action.MoveToElement(checkbox);
                if (!(checkbox.Selected))
                ///foreach (string brand in brandNames.ToList()) 
                //{
                //    if (checkbox.Text.Contains(brand) && !(checkbox.Selected))
                //    {
                { checkbox.Click(); }
                //    }
                //}
                //for (int i = 0; i < brandNames.Count(); i++)
                //{
                //    GetWait().Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='mobile-tab-section filter-block'][@data-sn-container='s_title']")));
                //    Actions action = new Actions(Driver);
                //    action.MoveToElement(checkbox);
                //    action.Build().Perform(); if (checkbox.Text.Contains(brandNames[i]) && !(checkbox.Selected))
                //    {
                //        checkbox.Click();
                //    }
                //}


               
            }
 action.Build().Perform();
            //express ir prekes zenklas
            //*[@class="col-md-4 col-xs-6"]//div[@class="flag-box-express-new"]
            //isvalyti pasirinkimus prekes zenklo //*[@class='clear-filter'][@data-clear-filter='s_title']
        }
    }
}

//foreach (IWebElement checkbox in _brandsCheckboxes)
//{
//    Actions action = new Actions(Driver);
//    action.MoveToElement(checkbox);
//    action.Build().Perform();
//    for (int i = 0; i < brandNames.Count(); i++)
//    {
//        if (checkbox.Text.Contains(brandNames.ToList()[i]) && !(checkbox.Selected))
//        {
//            checkbox.Click();
//            GetWait().Until(ExpectedConditions.ElementExists(By.CssSelector(".filter-head clearfix")));
//        }
//    }
//}


