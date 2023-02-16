using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using WebAppTest.PageObject;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.RegularExpressions;
using System.Drawing;

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

    // helper methods
    private WelcomePage VerifiedWelcomePage()
    {
        var welcomePage = new WelcomePage(driver, 5);
        welcomePage.VerifyPageUrl();
        welcomePage.VerifyPageLoaded();
        return welcomePage;
    }

    private NewOrderPage VerifiedNewOrderPage()
    {
        var newOrderPage = new NewOrderPage(driver, null, 5);
        newOrderPage.VerifyPageUrl();
        newOrderPage.VerifyPageLoaded();
        return newOrderPage;
    }

    private OrdersPage VerifiedOrdersPage()
    {
        var ordersPage = new OrdersPage(driver, 5);
        ordersPage.VerifyPageUrl();
        ordersPage.VerifyPageLoaded();
        return ordersPage;
    }

    [TestMethod]
    public void PageNavigationTest()
    {
        // PRECONDITION
        driver.Navigate().GoToUrl("http://localhost");
        var welcomePage = VerifiedWelcomePage();

        // STEPS 
        welcomePage.ClickNewOrderLink();
        var newOrderPage = VerifiedNewOrderPage();

        newOrderPage.ClickOrdersLink(); 
        var ordersPage = VerifiedOrdersPage();

        ordersPage.ClickFlightorderLink();
        VerifiedWelcomePage();
    }

    [TestMethod]
    [DataRow("caed89cf-b332-4634-95e0-7eddb8606170", "Prague", "Krakow", "2023-01-01T19:19:00", "460")]
    public void FlightSearchTest(string id, string from, string to, string flightDateTime, string price) {
        // PRECONDITIONS
        driver.Navigate().GoToUrl("http://localhost/Orders/Create");
        var newOrderPage = VerifiedNewOrderPage();
        newOrderPage.ClickFlightSection();
        // STEPS
        newOrderPage.FillFlightInformation(from, to, flightDateTime);
        newOrderPage.WaitForFlightsToLoad();
        newOrderPage.VerifyFirstFlightId(id);
        string actualFrom = newOrderPage.GetFirstFlightFrom();
        string actualTo = newOrderPage.GetFirstFlightTo();
        string actualDateTime = newOrderPage.GetFirstFlightDateTime();
        string actualPrice = newOrderPage.GetFirstFlightPrice();
        // EXPECTED RESULT
        Assert.AreEqual(from, actualFrom);
        Assert.AreEqual(to, actualTo);
        Assert.AreEqual(flightDateTime, actualDateTime);
        Assert.AreEqual(price, actualPrice);
    }

    // DataSource helper (workaround for .NET Core)
    private static string[] SplitCsv(string input)
    {
        var csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
        var list = new List<string>();
        foreach (Match match in csvSplit.Matches(input))
        {
            string value = match.Value;
            if (value.Length == 0)
            {
                list.Add(string.Empty);
            }

            list.Add(value.TrimStart(','));
        }
        return list.ToArray();
    }
    // DataSource helper (workaround for .NET Core)
    private static IEnumerable<string[]> GetValidData()
    {
        IEnumerable<string> rows = System.IO.File.ReadAllLines("Data/SuccessfulNewOrderSubmit.csv").Skip(1); // @"Resources\ValidFlights.csv"
        foreach (string row in rows)
        {
            yield return SplitCsv(row);
        }
    }

    private string AgeToBirthDate(int age)
    {
        var now = DateTime.Now;
        var birthDate = now.AddYears(-age);
        return birthDate.ToString("yyyy-MM-dd");

    }

    [TestMethod]
    //[DataRow("pepa", "dvorak", "pepa.dvorak@email.cz", "1999-08-20", "caed89cf-b332-4634-95e0-7eddb8606170", "Prague", "Krakow", "2023-01-01T19:19:00Z", "460", "", "Student" )]
    [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
    //    [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @".\Data\TestData.csv", "TestData#csv", DataAccessMethod.Sequential)]  // NOT INCLUDED IN .NET CORE
    public void SuccesfulNewOrderSubmitTest(string firstName, string lastName, string email, string age, string id, string coupon, string discount)
    {
        // INIT
        firstName = firstName == "#" ? "" : firstName;
        lastName = lastName == "#" ? "" : lastName;
        email = email == "#" ? "" : email;
        age = age == "#" ? "" : age;
        var birthDate = "";
        if (!String.IsNullOrEmpty(age))
        {
            birthDate = AgeToBirthDate(Int32.Parse(age));
            // print
            Console.WriteLine(birthDate);
        }
        id = id == "#" ? "" : id;
        coupon = coupon == "#" ? "" : coupon;
        discount = discount == "#" ? "" : discount;
        // PRECONDITIONS
        // create new order
        driver.Navigate().GoToUrl("http://localhost/Orders/Create");
        var newOrderPage = VerifiedNewOrderPage();
        // STEPS
        newOrderPage.FillAndSubmit(firstName, lastName, email, birthDate, id, "Prague", "Krakow", "2023-01-01", coupon, discount);
        // EXPECTED RESULT
        var welcomePage = VerifiedWelcomePage();


        // verify the order was created
        //var welcomePage = new WelcomePage(driver, 10);
        //welcomePage.VerifyPageUrl();
        //welcomePage.VerifyPageLoaded();
        //welcomePage.ClickOrdersLink();
        //var ordersPage = new OrdersPage(driver, 10);
        //ordersPage.VerifyPageUrl();
        //ordersPage.VerifyPageLoaded();
        //ordersPage.Search(firstName);
        //Assert.AreEqual(firstName, ordersPage.GetFirstFlightFirstName());  // td element is stale
        //Assert.AreEqual(lastName, ordersPage.GetFirstFlightLastName());
        //Assert.AreEqual(email, ordersPage.GetFirstFlightEmail());
        //DateTime birthDateDt = DateTime.Parse(birthDate);
        //var expectedBirthDate = birthDateDt.ToString("yyyy-MM-dd");
        //Assert.AreEqual(expectedBirthDate, ordersPage.GetFirstFlightBirthDate());
        //Assert.AreEqual(from, ordersPage.GetFirstFlightFrom());
        //Assert.AreEqual(to, ordersPage.GetFirstFlightTo());
        //Assert.AreEqual(flightDateTime, ordersPage.GetFirstFlightDateTime());
        //Assert.AreEqual(price, ordersPage.GetFirstFlightPrice());
        //Assert.AreEqual(coupon, ordersPage.GetFirstFlightCoupon());
        //Assert.AreEqual(discount, ordersPage.GetFirstFlightDiscount());
        
        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //IWebElement firstResult = wait.Until(e => e.FindElement(By.XPath("//asja")));

    }

    // DataSource helper (workaround for .NET Core)
    private static IEnumerable<string[]> GetFailData()
    {
        IEnumerable<string> rows = System.IO.File.ReadAllLines("Data/FailNewOrderSubmit.csv").Skip(1); // @"Resources\ValidFlights.csv"
        foreach (string row in rows)
        {
            yield return SplitCsv(row);
        }
    }

    [TestMethod]
    [DynamicData(nameof(GetFailData), DynamicDataSourceType.Method)]
    public void FailNewOrderSubmitTest(string firstName, string lastName, string email, string age, string id, string coupon, string discount)
    {
        // INIT
        firstName = firstName == "#" ? "" : firstName;
        lastName = lastName == "#" ? "" : lastName;
        email = email == "#" ? "" : email;
        age = age == "#" ? "" : age;
        var birthDate = "";
        if (!String.IsNullOrEmpty(age))
        {
            birthDate = AgeToBirthDate(Int32.Parse(age));
            // print
            Console.WriteLine(birthDate);
        }
        id = id == "#" ? "" : id;
        coupon = coupon == "#" ? "" : coupon;
        discount = discount == "#" ? "" : discount;
        // PRECONDITIONS
        // create new order
        driver.Navigate().GoToUrl("http://localhost/Orders/Create");
        var newOrderPage = VerifiedNewOrderPage();
        // STEPS
        //newOrderPage.FillAndSubmit(firstName, lastName, email, birthDate, id, "Prague", "Krakow", "2023-01-01", coupon, discount);
        // EXPECTED RESULT
        //var welcomePage = VerifiedWelcomePage();
    }

    [TestCleanup]
    public void Cleanup()
    {
        driver.Quit();
    }
}
