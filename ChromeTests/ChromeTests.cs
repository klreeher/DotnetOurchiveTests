using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace ChromeTests
{
    [TestFixture]
    public class ChromeTests : ui_tests.TestCases
    {
        protected override WebDriver GetDriver(bool runHeadless)
        {
            ChromeOptions options = new();
            if (runHeadless)
            {
                options.AddArgument("--headless");
                //options.AddArgument("window-size=1920x1080");
                //options.AddArgument("disable-gpu");
            }

            if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")))
            {
                string isCi = Environment.GetEnvironmentVariable("CI");

                if (isCi.Equals("CI"))
                {
                    options.AddArguments(new string[]{
                         "--remote-debugging-port=9222",
                         "--start-maximized",
                         "--window-size=1920,1200",
                         "--ignore-certificate-errors",
                         "--disable-extensions",
                         "--no-sandbox",
                         "--disable-dev-shm-usage"
                    });
                }
            }



            TestContext.WriteLine("STARTING CHROME BROWSER:");
            TestContext.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented));

            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver(options);
            return _webDriver;

        }
    }

}