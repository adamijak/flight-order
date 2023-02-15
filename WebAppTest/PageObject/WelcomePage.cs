using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace WebAppTest.PageObject
{
    internal class WelcomePage
    {
        // constants
        private readonly string pageUrl = "http://localhost";
        private readonly string pageLoadedText = "Welcome";

        // private variables
        private IWebDriver driver;
        private int timeout = 10;

        // constructor

        public WelcomePage(IWebDriver driver, int timeout)
        {
            this.driver = driver;
            this.timeout = timeout;
            PageFactory.InitElements(driver, this);
        }

        public WelcomePage(IWebDriver driver)
            : this(driver, 10)
        {
        }

        // input fields

        [FindsBy(How = How.CssSelector, Using = "a.nav-link.link-primary")]
        [CacheLookup]
        private IWebElement adamijak;

        [FindsBy(How = How.CssSelector, Using = "a.navbar-brand")]
        [CacheLookup]
        private IWebElement flightorder;

        [FindsBy(How = How.CssSelector, Using = "a[href='/Orders/Create']")]
        [CacheLookup]
        private IWebElement newOrder;

        [FindsBy(How = How.CssSelector, Using = "a[href='/Orders']")]
        [CacheLookup]
        private IWebElement orders;

        /// <summary>
        /// Verify that current page URL matches the expected URL.
        /// </summary>
        /// <returns>The WelcomePage class instance.</returns>
        public WelcomePage VerifyPageUrl()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until<bool>((d) =>
            {
                return d.Url.Contains(pageUrl);
            });
            return this;
        }

        /// <summary>
        /// Verify that the page loaded completely.
        /// </summary>
        /// <returns>The WelcomePage class instance.</returns>
        public WelcomePage VerifyPageLoaded()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until<bool>((d) =>
            {
                return d.PageSource.Contains(pageLoadedText);
            });
            return this;
        }

        /// <summary>
        /// Click on Adamijak Link.
        /// </summary>
        /// <returns>The WelcomePage class instance.</returns>
        public WelcomePage ClickAdamijakLink()
        {
            adamijak.Click();
            return this;
        }

        /// <summary>
        /// Click on Flightorder Link.
        /// </summary>
        /// <returns>The WelcomePage class instance.</returns>
        public WelcomePage ClickFlightorderLink()
        {
            flightorder.Click();
            return this;
        }

        /// <summary>
        /// Click on New Order Link.
        /// </summary>
        /// <returns>The WelcomePage class instance.</returns>
        public WelcomePage ClickNewOrderLink()
        {
            newOrder.Click();
            return this;
        }

        /// <summary>
        /// Click on Orders Link.
        /// </summary>
        /// <returns>The WelcomePage class instance.</returns>
        public WelcomePage ClickOrdersLink()
        {
            orders.Click();
            return this;
        }

    }
}
