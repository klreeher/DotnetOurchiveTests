using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.IO;
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

        private string baseUrl;
        public TestContext TestContext { get; set; }

        protected WebDriver _webDriver;


        [SetUp]
        public void SetUp() // Non-virtual
        {
            // Do required work

            baseUrl = TestContext.Parameters["webAppUrl"];
            // Call virtual method now...
            _webDriver = this.GetDriver();
            TestContext.WriteLine(baseUrl);
        }

        protected abstract WebDriver GetDriver();

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void LoadLandingPage()
        {
            pages.LandingPage home = new(_webDriver, baseUrl);
        }
        [Test]
        public void LoadLoginPage()
        {
            pages.LoginPage login = new(_webDriver, baseUrl);
        }

        [Test]
        public void CanLoginPage()
        {
            string username = TestContext.Parameters["webAppUserName"];
            string password = TestContext.Parameters["webAppPassword"];
            pages.LandingPage home = new(_webDriver, baseUrl);
            pages.LoginPage login = new(_webDriver, baseUrl);
            login.DoFillLoginForm(username, password, true);

            home.loggedInUser("kate");

        }

        [Test]
        public void CanToggleThemeMode()
        {

            pages.LandingPage home = new(_webDriver, baseUrl);
            var darkMode = home.IsThemeModeDark();
            home.ChangeTheme();


        }
    }
    [TestFixture]
    public class FirefoxTests : TestCases
    {

        protected override WebDriver GetDriver()
        {
            FirefoxOptions options = new();
            options.AddArgument("--headless");
            new DriverManager().SetUpDriver(new FirefoxConfig());
            _webDriver = new FirefoxDriver(options);
            return _webDriver;
        }
    }

    [TestFixture]
    public class ChromeTests : TestCases
    {
        protected override WebDriver GetDriver()
        {
            ChromeOptions options = new();
            options.AddArgument("--headless");
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver(options);
            return _webDriver;

        }
    }



}