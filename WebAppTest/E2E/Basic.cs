using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace WebAppTest.E2E;

[TestClass]
public class Basic
{
    [TestMethod]
    public void WebDriverTest()
    {
        IWebDriver driver = new FirefoxDriver();
        driver.Navigate().GoToUrl("http://localhost/Orders/Create");
        // find id firstName
        driver.FindElement(By.Id("firstName")).SendKeys("John");
        System.Threading.Thread.Sleep(5000);
        driver.Quit();
    }
}
