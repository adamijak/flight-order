using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using OpenQA.Selenium.Extensions;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace WebAppTest.PageObject
{
    internal class NewOrderPage
    {
        // constants
        private readonly string pageUrl = "http://localhost/Orders/Create";
        private readonly string pageLoadedText = "New order";
 
        // private vars
        private Dictionary<string, string> data;
        private IWebDriver driver;
        private int timeout;
        
        // constructor
        public NewOrderPage(IWebDriver driver, Dictionary<string, string> data, int timeout)
        {
            this.driver = driver;
            this.data = data;
            this.timeout = timeout;
            PageFactory.InitElements(driver, this);
        }

        public NewOrderPage(IWebDriver driver, Dictionary<string, string> data)
            : this(driver, data, 10)
        {
        }

        public NewOrderPage(IWebDriver driver)
            : this(driver, new Dictionary<string, string>(), 10)
        {
        }
        
        // page links
        [FindsBy(How = How.CssSelector, Using = "a.navbar-brand")]
        [CacheLookup]
        private IWebElement flightorder;

        [FindsBy(How = How.CssSelector, Using = "a[href='/Orders/Create']")]
        [CacheLookup]
        private IWebElement newOrder;

        [FindsBy(How = How.CssSelector, Using = "a[href='/Orders']")]
        [CacheLookup]
        private IWebElement orders;

        // error messages
        [FindsBy(How = How.XPath, Using = ".validation-summary-errors li")]
        private IList<IWebElement> errorList;

        // basic information fields
        [FindsBy(How = How.CssSelector, Using = "#headingOne button.accordion-button")]
        [CacheLookup]
        private IWebElement basicInformation1;

        [FindsBy(How = How.Id, Using = "firstName")]
        [CacheLookup]
        private IWebElement firstName;

        [FindsBy(How = How.Id, Using = "lastName")]
        [CacheLookup]
        private IWebElement lastName;

        [FindsBy(How = How.Id, Using = "email")]
        [CacheLookup]
        private IWebElement email;

        [FindsBy(How = How.Id, Using = "birthDate")]
        private IWebElement birthDate;

        // flight selection fields
        [FindsBy(How = How.CssSelector, Using = "#headingTwo button.accordion-button.collapsed")]
        [CacheLookup]
        private IWebElement flightSelection2;

        [FindsBy(How = How.Id, Using = "from")]
        [CacheLookup]
        private IWebElement from;

        [FindsBy(How = How.Id, Using = "to")]
        [CacheLookup]
        private IWebElement to;

        [FindsBy(How = How.Id, Using = "flightDate")]
        [CacheLookup]
        private IWebElement flightDate;

        // flights table
        [FindsBy(How = How.XPath, Using = "//table[@class='table text-nowrap table-hover']/tbody/tr")] //  Using = "//input[@name='Order.FlightId']")]
        private IList<IWebElement> flightRows;

        // discount and payment fields
        [FindsBy(How = How.CssSelector, Using = "#headingThree button.accordion-button.collapsed")]
        [CacheLookup]
        private IWebElement discountAndPayment3;

        [FindsBy(How = How.Id, Using = "coupon")]
        [CacheLookup]
        private IWebElement coupon;

        [FindsBy(How = How.Id, Using = "discount")]
        [CacheLookup]
        private IWebElement discount;

        // submit button
        [FindsBy(How = How.CssSelector, Using = "input.btn.btn-primary.ms-auto")]
        [CacheLookup]
        private IWebElement submit;

        // general functions

        /// <summary>
        /// Verify that current page URL matches the expected URL.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage VerifyPageUrl()
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
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage VerifyPageLoaded()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until<bool>((d) =>
            {
                return d.PageSource.Contains(pageLoadedText);
            });
            return this;
        }
 
        // menu link clicks

        /// <summary>
        /// Click on Flightorder Link.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage ClickFlightorderLink()
        {
            flightorder.Click();
            return this;
        }

        /// <summary>
        /// Click on New Order Link.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage ClickNewOrderLink()
        {
            newOrder.Click();
            return this;
        }

        /// <summary>
        /// Click on Orders Link.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage ClickOrdersLink()
        {
            orders.Click();
            return this;
        }

        // section clicks

        /// <summary>
        /// Click on 1 Basic Information Button.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage ClickBasicInformationButton()
        {
            basicInformation1.Click();
            return this;
        }

        /// <summary>
        /// Click on 2 Flight Selection Button.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage ClickFlightSection()
        {
            flightSelection2.Click();
            return this;
        }

        /// <summary>
        /// Click on 3 Discount And Payment Button.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage ClickDiscountSection()
        {
            discountAndPayment3.Click();
            return this;
        }

        /// <summary>
        /// Click on Submit Button.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage ClickSubmitButton()
        {
            submit.Click();
            return this;
        }

        // function to fill basic info at New Order page
        public NewOrderPage FillBasicInformation(string firstName, string lastName, string email, string birthDate)
        {
            SetFirstNameTextField(firstName);
            SetLastNameTextField(lastName);
            SetEmailTextField(email);
            SetBirthDateDateField(birthDate);
            return this;
        }

        // function to fill flight information
        public NewOrderPage FillFlightInformation(string from, string to, string flightDateTime)
        {
            SetFromDropDownListField(from);
            SetToDropDownListField(to);
            SetFlightDateField(flightDateTime);
            return this;
        }

        public NewOrderPage FillDiscountInformation(string coupon, string discount)
        {
            SetCouponTextField(coupon);
            SetDiscountDropDownListField(discount);
            return this;
        }

        public NewOrderPage VerifyFirstFlightId(string expectedId) {
            string firstFlightId = GetFirstFlightId();
            //Console.WriteLine(firstFlightId);
            Assert.AreEqual(expectedId, firstFlightId);
            return this;
        }

        /// <summary>
        /// Fill every fields in the page.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage Fill(string firstName, string lastName, string email, string birthDate, string id, string from, string to, string flightDateTime, string coupon, string discount)
        {
            FillBasicInformation(firstName, lastName, email, birthDate);
            ClickFlightSection();
            FillFlightInformation(from, to, flightDateTime);
            WaitForFlightsToLoad();
            VerifyFirstFlightId(id);
            SelectFirstFlight();
            ClickDiscountSection();
            FillDiscountInformation(coupon, discount);
            return this;
        }

        /// <summary>
        /// Fill every fields in the page and submit it to target page.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage FillAndSubmit(string firstName, string lastName, string email, string birthDate, string id, string from, string to, string flightDateTime, string coupon, string discount)
        {
            Fill(firstName, lastName, email, birthDate, id, from, to, flightDateTime, coupon, discount);
            return Submit();
        }

        public int GetErrorMessagesCount()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var table = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".validation-summary-errors li:first-child")));
            errorList = driver.FindElements(By.CssSelector(".validation-summary-errors li"));
            return errorList.Count;
        }


        /// <summary>
        /// Set value to Coupon Text field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetCouponTextField(string couponValue)
        {
            coupon.Clear();
            coupon.SendKeys(couponValue);
            return this;
        }

        /// <summary>
        /// Set value to Discount Drop Down List field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetDiscountDropDownListField(string discountValue)
        {
            if (!String.IsNullOrEmpty(discountValue))
            {
                new SelectElement(discount).SelectByText(discountValue);
            }
            return this;
        }

        /// <summary>
        /// Set Birth Date Date field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetBirthDateDateField(string birthDateValue)
        {
            // UGH a nightmare was this, let this be here so not to forget the pain searching
            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //birthDate = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("birthDate")));
            //((JavascriptExecutor)driver).executeScript("document.getElementById('dateofbirth').removeAttribute('readonly',0);");
            //birthDate.Click();
            //Console.WriteLine(birthDateValue);
            //driver.ExecuteJavaScript("arguments[0].value=arguments[1]", birthDate, birthDateValue);  
            //string output = driver.ExecuteJavaScript<string>("return arguments[0].placeholder", driver.FindElement(By.Id("birthDate")));
            //Console.WriteLine("Tady je outouteutoeutouetoteoeutoetuoetuoeto:");
            //Console.WriteLine(output);
            if (!String.IsNullOrEmpty(birthDateValue))
            {
                DateTime birthDateDt = DateTime.Parse(birthDateValue);
                birthDateValue = birthDateDt.ToString("yyyy-MM-dd");
                birthDate.Clear();
                birthDate.SendKeys(birthDateValue); // IMPORTANT: this works only with well formated dates on input eg 1985-01-01
            }
            return this;
        }

        /// <summary>
        /// Set value to Email Text field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetEmailTextField(string emailValue)
        {   
            email.Clear();
            email.SendKeys(emailValue);
            return this;
        }

        /// <summary>
        /// Set value to First Name Text field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetFirstNameTextField(string firstNameValue)
        {
            firstName.Clear();  
            firstName.SendKeys(firstNameValue);
            return this;
        }

        /// <summary>
        /// Set value to Last Name Text field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetLastNameTextField(string lastNameValue)
        {
            lastName.Clear();
            lastName.SendKeys(lastNameValue);
            return this;
        }

        /// <summary>
        /// Set Flight Date Date field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetFlightDateField(string flightDateTimeValue)
        {
            //driver.ExecuteJavaScript("arguments[0].value=arguments[1]", flightDate, flightDateValue);
            if (!String.IsNullOrEmpty(flightDateTimeValue))
            {
                // convert flight date 
                DateTime dt = DateTime.Parse(flightDateTimeValue);
                //DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                string flightDateValue = dt.ToString("yyyy-MM-dd");
                flightDate.Click();
                flightDate.SendKeys(flightDateValue);
            }
            return this;
        }

        /// <summary>
        /// Set value to From Drop Down List field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetFromDropDownListField(string fromValue)
        {
            new SelectElement(from).SelectByText(fromValue);
            return this;
        }

        /// <summary>
        /// Set value to To Drop Down List field.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage SetToDropDownListField(string toValue)
        {
            new SelectElement(to).SelectByText(toValue);
            return this;
        }

        public NewOrderPage WaitForFlightsToLoad()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("td[1]/div/input")));
            flightRows = wait.Until<IList<IWebElement>>((d) =>
            {
                var elements = d.FindElements(By.XPath("//table[@class='table text-nowrap table-hover']/tbody/tr"));
                if (elements.Count == 0)
                {
                    return null;
                }

                return elements;
            });
            return this;
        }

        public string GetFirstFlightId()
        {
            return flightRows[0].FindElement(By.XPath("td[1]/div/input")).GetAttribute("value");
        }

        public string GetFirstFlightFrom()
        {
            return flightRows[0].FindElement(By.XPath("td[2]")).Text;
        }

        public string GetFirstFlightTo()
        {
            return flightRows[0].FindElement(By.XPath("td[3]")).Text; 
        }

        public string GetFirstFlightDateTime()
        {
            return flightRows[0].FindElement(By.XPath("td[4]")).Text;
        }

        public string GetFirstFlightPrice()
        {
            return flightRows[0].FindElement(By.XPath("td[5]")).Text;
        }

        public NewOrderPage SelectFirstFlight()
        {
            flightRows[0].FindElement(By.XPath("td[1]/div/input")).Click();
            return this;
        }

        /// <summary>
        /// Submit the form to target page.
        /// </summary>
        /// <returns>The NewOrderPage class instance.</returns>
        public NewOrderPage Submit()
        {
            ClickSubmitButton();
            return this;
        }
    }
}
