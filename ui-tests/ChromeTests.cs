using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;


namespace ui_tests
{
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