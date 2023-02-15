using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace WebAppTest.PageObject
{
    internal class OrdersPage
    {
        // constants
        private readonly string pageUrl = "http://localhost/Orders";
        private readonly string pageLoadedText = "Orders";

        // private variables
        private Dictionary<string, string> data;
        private IWebDriver driver;
        private int timeout;

        // constructor

        public OrdersPage(IWebDriver driver, int timeout)
        {
            this.driver = driver;
            this.timeout = timeout;
            PageFactory.InitElements(driver, this);
        }

        public OrdersPage(IWebDriver driver)
            : this(driver, 10)
        {
        }


        // links
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

        // search box
        [FindsBy(How = How.Id, Using = "search")]
        [CacheLookup]
        private IWebElement search;

        // flight rows list
        [FindsBy(How = How.XPath, Using = "//table/tbody/tr")]
        [CacheLookup]
        private IList<IWebElement> flightRows;

        // information about first flight in the list
        [FindsBy(How = How.XPath, Using = "//table/tbody/tr[1]")]
        [CacheLookup]
        private IWebElement firstFlight;

        // first flight's Delete button
        [FindsBy(How = How.XPath, Using = "//table/tbody/tr[1]/td[12]/button")]
        [CacheLookup]
        private IWebElement deleteFirstFlightButton;

        // general

        /// <summary>
        /// Verify that the page loaded completely.
        /// </summary>
        /// <returns>The OrdersPage class instance.</returns>
        public OrdersPage VerifyPageLoaded()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            var table = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table")));
            return this;
        }

        /// <summary>
        /// Verify that current page URL matches the expected URL.
        /// </summary>
        /// <returns>The OrdersPage class instance.</returns>
        public OrdersPage VerifyPageUrl()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until<bool>((d) =>
            {
                return d.Url.Contains(pageUrl);
            });
            return this;
        }

        // link clicks

        /// <summary>
        /// Click on Adamijak Link.
        /// </summary>
        /// <returns>The OrdersPage class instance.</returns>
        public OrdersPage ClickAdamijakLink()
        {
            adamijak.Click();
            return this;
        }
        /// <summary>
        /// Click on Flightorder Link.
        /// </summary>
        /// <returns>The OrdersPage class instance.</returns>
        public OrdersPage ClickFlightorderLink()
        {
            flightorder.Click();
            return this;
        }

        /// <summary>
        /// Click on New Order Link.
        /// </summary>
        /// <returns>The OrdersPage class instance.</returns>
        public OrdersPage ClickNewOrderLink()
        {
            newOrder.Click();
            return this;
        }

        /// <summary>
        /// Click on Orders Link.
        /// </summary>
        /// <returns>The OrdersPage class instance.</returns>
        public OrdersPage ClickOrdersLink()
        {
            orders.Click();
            return this;
        }

        // search

        /// <summary>
        /// set search field and press enter 
        /// </summary>
        /// <returns>The OrdersPage class instance.</returns>
        public OrdersPage Search(string text)
        {
            search.SendKeys(text);
            search.SendKeys(Keys.Enter);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var table = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table")));
            firstFlight = driver.FindElement(By.XPath("//table/tbody/tr[1]"));
            return this;
        }

        // interact with flights

        /// <summary>
        /// Check if there are any flights
        /// </summary>
        /// <returns>True if there are any flights, false otherwise</returns>
        public bool AnyFlights()
        {
            return firstFlight != null;
        }

        /// <summary>
        /// Deletes the first flight displayed in the table
        /// </summary>
        /// <returns>The OrdersPage class instance.</returns>
        public OrdersPage DeleteFirstFlight()
        {
            deleteFirstFlightButton.Click();
            return this;
        }

        /// <summary>
        /// Returns the first flight's first name displayed in the table
        /// </summary>
        /// <returns>A string containing the first name of the first flight.</returns>
        public string GetFirstFlightFirstName()
        {
            return firstFlight.FindElement(By.XPath(".//td[1]")).Text;
        }

        /// <summary>
        /// Returns the first flight's last name displayed in the table
        /// </summary>
        /// <returns>A string containing the last name of the first flight.</returns>
        public string GetFirstFlightLastName()
        {
            return firstFlight.FindElement(By.XPath(".//td[2]")).Text;
        }

        /// <summary>
        /// Returns the first flight's email displayed in the table
        /// </summary>
        /// <returns>A string containing the email of the first flight.</returns>
        public string GetFirstFlightEmail()
        {
            return firstFlight.FindElement(By.XPath(".//td[3]")).Text;
        }

        /// <summary>
        /// Returns the first flight's birth date displayed in the table
        /// </summary>
        /// <returns>A string containing the birth date of the first flight.</returns>
        public string GetFirstFlightBirthDate()
        {
            return firstFlight.FindElement(By.XPath(".//td[4]")).Text;
        }

        /// <summary>
        /// Returns the first flight's departure location displayed in the table
        /// </summary>
        /// <returns>A string containing the departure location of the first flight.</returns>
        public string GetFirstFlightFrom()
        {
            return firstFlight.FindElement(By.XPath(".//td[5]")).Text;
        }

        /// <summary>
        /// Returns the first flight's destination location displayed in the table
        /// </summary>
        /// <returns>A string containing the destination location of the first flight.</returns>
        public string GetFirstFlightTo()
        {
            return firstFlight.FindElement(By.XPath(".//td[6]")).Text;
        }

        /// <summary>
        /// Returns the first flight's departure date and time displayed in the table
        /// </summary>
        /// <returns>A string containing the departure date and time of the first flight.</returns>
        public string GetFirstFlightDateTime()
        {
            return firstFlight.FindElement(By.XPath(".//td[7]")).Text;
        }

        /// <summary>
        /// Returns the first flight's coupon displayed in the table
        /// </summary>
        /// <returns>A string containing the coupon code of the first flight.</returns>
        public string GetFirstFlightCoupon()
        {
            return firstFlight.FindElement(By.XPath(".//td[8]")).Text;
        }

        /// <summary>
        /// Returns the first flight's discount displayed in the table
        /// </summary>
        /// <returns>A string containing the discount applied to the first flight.</returns>
        public string GetFirstFlightDiscount()
        {
            return firstFlight.FindElement(By.XPath(".//td[9]")).Text;
        }

        /// <summary>
        /// Returns the first flight's price displayed in the table
        /// </summary>
        /// <returns>A string containing the price of the first flight.</returns>
        public string GetFirstFlightPrice()
        {
            return firstFlight.FindElement(By.XPath(".//td[10]")).Text;
        }

        /// <summary>
        /// Returns the total price of the first flight displayed in the table
        /// </summary>  
        /// <returns>A string containing the total price of the first flight.</returns>
        public string GetFirstFlightTotalPrice()
        {
            return firstFlight.FindElement(By.XPath(".//td[11]")).Text;
        }

    }
}
