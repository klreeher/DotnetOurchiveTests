using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;


namespace ui_tests
{
    [TestFixture]
    public class ChromeTests : TestCases
    {
        protected override WebDriver GetDriver(bool runHeadless)
        {
            ChromeOptions options = new();

            if (runHeadless)
            {
                options.AddArgument("--headless");
                options.AddArgument("window-size=1920x1080");
                options.AddArgument("disable-gpu");
            }

            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver(options);
            return _webDriver;

        }
    }

}