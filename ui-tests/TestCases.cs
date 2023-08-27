using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


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

        public bool runHeadless { get; set; } = false;


        [SetUp]
        public void SetUp() // Non-virtual
        {
            baseUrl = TestContext.Parameters["webAppUrl"];
            runHeadless = bool.Parse(TestContext.Parameters["runHeadless"]);
            _webDriver = this.GetDriver(runHeadless);
            TestContext.WriteLine($"Base Url: {baseUrl}");
        }

        protected abstract WebDriver GetDriver(bool runHeadless);

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void LoadLandingPage()
        {
            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            pages.LandingPage home = new(_webDriver, baseUrl);
        }
        [Test]
        public void LoadLoginPage()
        {

            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            pages.LoginPage login = new(_webDriver, baseUrl);
        }

        [Test]
        /// <summary>
        /// tests that landing page and loading pages load, and user can log in with a valid username/pw
        /// </summary>
        public void CanLoginPage()
        {
            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            string username = TestContext.Parameters["webAppUserName"];
            string password = TestContext.Parameters["webAppPassword"];
            pages.LandingPage home = new(_webDriver, baseUrl);
            pages.LoginPage login = new(_webDriver, baseUrl);

            login.DoFillLoginForm(username, password, true);
            home.validateLoggedInUser(username);
        }

        [Test]
        /// <summary>
        /// tests that landing page and loading pages load, and user can log in with a valid username/pw
        /// </summary>
        public void CanLoginAndLogout()
        {
            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            string username = TestContext.Parameters["webAppUserName"];
            string password = TestContext.Parameters["webAppPassword"];
            pages.LandingPage home = new(_webDriver, baseUrl);
            pages.LoginPage login = new(_webDriver, baseUrl);

            login.DoFillLoginForm(username, password, true);
            home.validateLoggedInUser("kate");
            Assert.IsTrue(home.openRightNav());

        }
        //nav-signout-link

        [Test]
        public void CanToggleThemeModeLoggedOut()
        {
            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            pages.LandingPage home = new(_webDriver, baseUrl);
            var darkMode = home.IsThemeModeDark();
            Console.WriteLine($"Found Current Theme To Be Dark Mode?: {darkMode}");
            home.ChangeTheme();
            Console.WriteLine($"Found Current Theme To Be Dark Mode?: {home.IsThemeModeDark()}");
            Assert.That(home.IsThemeModeDark(), Is.Not.EqualTo(darkMode), "Expected the theme to have changed");

        }

        [Test]
        public void CanToggleThemeModeLoggedIn()
        {
            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            string username = TestContext.Parameters["webAppUserName"];
            string password = TestContext.Parameters["webAppPassword"];
            pages.LandingPage home = new(_webDriver, baseUrl);
            pages.LoginPage login = new(_webDriver, baseUrl);

            login.DoFillLoginForm(username, password, true);
            home.validateLoggedInUser("kate");

            var darkMode = home.IsThemeModeDark();
            Console.WriteLine($"Found Current Theme To Be Dark Mode?: {darkMode}");
            home.ChangeTheme();
            Console.WriteLine($"Found Current Theme To Be Dark Mode?: {home.IsThemeModeDark()}");
            Assert.That(home.IsThemeModeDark(), Is.Not.EqualTo(darkMode), "Expected the theme to have changed");

        }

    }

}