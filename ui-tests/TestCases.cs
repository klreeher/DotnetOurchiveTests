using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;


namespace ui_tests
{

    /// <summary>
    /// TestCases class contains the test cases regardless of driver
    /// template method pattern
    /// </summary>
    public abstract class TestCases
    {
        protected WebDriver _webDriver;


        [SetUp]
        public void SetUp() // Non-virtual
        {
            // Do required work

            // Call virtual method now...
            _webDriver = this.GetDriver();
            _webDriver.Navigate().GoToUrl("https://ourchive-dev.stopthatimp.net/");
        }

        protected abstract WebDriver GetDriver();

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void testLanding()
        {
            pages.LandingPage home = new(_webDriver);

        }
    }

    [TestFixture]
    public class FirefoxTests : TestCases
    {

        protected override WebDriver GetDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            _webDriver = new FirefoxDriver();
            return _webDriver;
        }
    }

    [TestFixture]
    public class ChromeTests : TestCases
    {
        protected override WebDriver GetDriver()
        {

            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
            return _webDriver;

        }
    }



}