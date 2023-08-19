using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;


namespace ui_tests
{
    [TestFixture]
    public class FirefoxTests : TestCases
    {

        protected override WebDriver GetDriver()
        {
            FirefoxOptions options = new();
            //options.AddArgument("--headless");
            //options.AddArgument("window-size=1920x1080");
            //options.AddArgument("disable-gpu");
            new DriverManager().SetUpDriver(new FirefoxConfig());
            _webDriver = new FirefoxDriver(options);
            return _webDriver;
        }
    }



}