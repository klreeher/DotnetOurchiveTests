using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace ui_tests
{
    [TestFixture]
    public class FirefoxTests : TestCases
    {


        protected override WebDriver GetDriver(bool runHeadless)
        {
            FirefoxOptions options = new();
            options.EnableDevToolsProtocol = true;
            options.LogLevel = FirefoxDriverLogLevel.Error;
            if (runHeadless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--window-position=-32000,-32000");
                //options.AddArgument("window-size=1920x1080");
                //options.AddArgument("disable-gpu");
            }
            TestContext.WriteLine("STARTING FIREFOX BROWSER:");
            TestContext.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented));

            if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")))
            {
                string isCi = Environment.GetEnvironmentVariable("CI");
                TestContext.WriteLine($"IsCI? {isCi}");
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
                }
            }


            new DriverManager().SetUpDriver(new FirefoxConfig());
            _webDriver = new FirefoxDriver(options);
            return _webDriver;
        }
    }



}