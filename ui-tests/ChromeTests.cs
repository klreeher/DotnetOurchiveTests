using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

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

            string isCi = Environment.GetEnvironmentVariable("CI");

            if (isCi.Equals("CI"))
            {
                options.AddArguments(new string[]{
                     "--disable-gpu",
                     "--window-size=1920,1200",
                     "--ignore-certificate-errors",
                     "--disable-extensions",
                     "--no-sandbox",
                     "--disable-dev-shm-usage"
                });
                options.BinaryLocation = "/usr/bin/google-chrome";
            }
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _webDriver = new ChromeDriver(options);
            return _webDriver;

        }
    }

}