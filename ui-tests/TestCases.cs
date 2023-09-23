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
    [TestFixture]
    public abstract class TestCases
    {

        private string baseUrl;
        public TestContext TestContext { get; set; }

        protected WebDriver _webDriver;

        public bool runHeadless { get; set; } = false;


        private static string GenerateUniqueFileName(string baseFileName, ScreenshotImageFormat format)
        {
            // Generate a unique timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Combine the base file name with the timestamp
            string uniqueFileName = $"{baseFileName}_{timestamp}.{format.ToString()}";

            // You can also add a file extension if needed
            // For example: uniqueFileName = $"{uniqueFileName}.txt";

            return uniqueFileName;
        }

        public void saveScreenshotAsAttachment()
        {
            string name = this.GetType().Name;
            string unique_name = GenerateUniqueFileName(name, ScreenshotImageFormat.Png);
            _webDriver.GetScreenshot().SaveAsFile(unique_name, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(unique_name);
        }


        [SetUp, Description("Set Up for Test Cases -- creates driver")]
        public void SetUp() // Non-virtual
        {
            baseUrl = TestContext.Parameters["webAppUrl"];
            runHeadless = bool.Parse(TestContext.Parameters["runHeadless"]);

            TestContext.WriteLine($"Base Url: {baseUrl}");

            TestContext.WriteLine($"runHeadless? {runHeadless.ToString()}");
            _webDriver = this.GetDriver(runHeadless);
        }

        protected abstract WebDriver GetDriver(bool runHeadless);

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }


        [Test]
        [Description("The Landing Page should load successfully.")]
        public void LoadLandingPage()
        {
            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            pages.LandingPage home = new(_webDriver, baseUrl);
        }
        [Test]
        [Description("The Login Page Should Load successfully.")]
        public void LoadLoginPage()
        {

            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            pages.LoginPage login = new(_webDriver, baseUrl);
        }

        [Test]
        [Description("tests that landing page and loading pages load, and user can log in with a valid username/pw")]
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
        [Description("tests that a logged in user can log out successfully")]
        public void CanLoginAndLogout()
        {
            Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.Test.Name);
            string username = TestContext.Parameters["webAppUserName"];
            string password = TestContext.Parameters["webAppPassword"];
            pages.LandingPage home = new(_webDriver, baseUrl);
            pages.LoginPage login = new(_webDriver, baseUrl);

            login.DoFillLoginForm(username, password, true);
            home.validateLoggedInUser(username);
            Assert.IsTrue(home.openRightNav());

        }
        //nav-signout-link

        [Test]
        [Description("A logged out user can toggle the theme from dark to light mode and vice versa.")]
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
        [Description("A logged in user can toggle the theme from dark to light mode and vice versa.")]
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

        [Test]
        public void CanLoadWorksCreatePage()
        {
            saveScreenshotAsAttachment();
            pages.WorksPage worksNew = new(_webDriver, baseUrl);
            saveScreenshotAsAttachment();

        }

    }

}