using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using WebAppTest.PageObject;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAppTest.E2E;

[TestClass]
public class ProcessTest
{
    private IWebDriver driver;

    [TestInitialize]
    public void Setup()
    {
        driver = new FirefoxDriver();
    }

    [TestMethod]
    public void PageNavigationTest()
    {

        driver.Navigate().GoToUrl("http://localhost");

        var welcomePage = new WelcomePage(driver, 10);
        welcomePage.VerifyPageUrl();
        welcomePage.VerifyPageLoaded();
        welcomePage.ClickNewOrderLink();

        var newOrderPage = new NewOrderPage(driver, null, 10);
        newOrderPage.VerifyPageUrl();
        newOrderPage.VerifyPageLoaded();
        newOrderPage.ClickOrdersLink(); 

        var ordersPage = new OrdersPage(driver, 10); 
        ordersPage.VerifyPageUrl();
        ordersPage.VerifyPageLoaded();
        
    }

    [TestMethod]
    [DataRow("pepa", "dvorak", "pepa.dvorak@email.cz", "1999-08-20", "caed89cf-b332-4634-95e0-7eddb8606170", "Prague", "Krakow", "2023-01-01T19:19:00Z", "460", "", "Student" )]
    public void WholeProcessTest(string firstName, string lastName, string email, string birthDate, string id, string from, string to, string flightDateTime, string price, string coupon, string discount)
    {
        // create new order
        driver.Navigate().GoToUrl("http://localhost/Orders/Create");
        var newOrderPage = new NewOrderPage(driver, null, 10);
        newOrderPage.VerifyPageUrl();
        newOrderPage.VerifyPageLoaded();
        // fill basic information
        newOrderPage.SetFirstNameTextField(firstName);
        newOrderPage.SetLastNameTextField(lastName);
        newOrderPage.SetEmailTextField(email);
        newOrderPage.SetBirthDateDateField(birthDate);
        // fill flight selection
        newOrderPage.ClickFlightSelectionButton();
        newOrderPage.SetFromDropDownListField(from);
        newOrderPage.SetToDropDownListField(to);
        // convert flight date 
        DateTime dt = DateTime.Parse(flightDateTime);
        //DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        string flightDate = dt.ToString("yyyy-MM-dd");
        // set flight date
        newOrderPage.SetFlightDateDateField(flightDate);
        // wait for flights to load, then check and select the first one
        newOrderPage.WaitForFlightsToLoad();
        string firstFlightId = newOrderPage.GetFirstFlightId();
        //Console.WriteLine(firstFlightId);
        Assert.AreEqual(id, firstFlightId);
        newOrderPage.SelectFirstFlight();
        // fill discount and payment section
        newOrderPage.ClickDiscountAndPaymentButton();
        newOrderPage.SetCouponTextField(coupon);
        newOrderPage.SetDiscountDropDownListField(discount);
        // submit the order
        newOrderPage.ClickSubmitButton();
        // verify the order was created
        var welcomePage = new WelcomePage(driver, 10);
        welcomePage.VerifyPageUrl();
        welcomePage.VerifyPageLoaded();
        welcomePage.ClickOrdersLink();
        var ordersPage = new OrdersPage(driver, 10);
        ordersPage.VerifyPageUrl();
        ordersPage.VerifyPageLoaded();
        ordersPage.Search(firstName);
        Assert.AreEqual(firstName, ordersPage.GetFirstFlightFirstName());
        Assert.AreEqual(lastName, ordersPage.GetFirstFlightLastName());
        Assert.AreEqual(email, ordersPage.GetFirstFlightEmail());
        Assert.AreEqual(birthDate, ordersPage.GetFirstFlightBirthDate());
        Assert.AreEqual(from, ordersPage.GetFirstFlightFrom());
        Assert.AreEqual(to, ordersPage.GetFirstFlightTo());
        Assert.AreEqual(flightDateTime, ordersPage.GetFirstFlightDateTime());
        Assert.AreEqual(price, ordersPage.GetFirstFlightPrice());
        Assert.AreEqual(coupon, ordersPage.GetFirstFlightCoupon());
        Assert.AreEqual(discount, ordersPage.GetFirstFlightDiscount());
        
        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //IWebElement firstResult = wait.Until(e => e.FindElement(By.XPath("//asja")));

    }

    //[TestMethod]
    //public void FlightSearchTest 

    [TestCleanup]
    public void Cleanup()
    {
        driver.Quit();
    }
}
