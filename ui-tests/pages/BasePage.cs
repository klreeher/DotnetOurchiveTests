using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;



namespace ui_tests.pages;

public abstract class BasePage
{
    protected WebDriver? driver = null;
    protected abstract string url_segment { get; }
    protected string? instance_url;

    public abstract bool validatePage();
    protected abstract bool requiresLogin { get; }

    public BasePage(WebDriver _driver, string _instance_url = "")
    {
        this.instance_url = _instance_url;
        try
        {
            this.instance_url = Environment.GetEnvironmentVariable("BaseUrl");
        }
        catch (Exception e)
        {
            new Exception("No instance url provided, and no environment variable found named `BaseUrl`." + e);
            throw;
        }

        this.instance_url = _instance_url;

        if (this.driver is null)
        {
            this.driver = _driver;
        }

        if (requiresLogin)
        {
            string username = TestContext.Parameters["webAppUserName"];
            string password = TestContext.Parameters["webAppPassword"];

            pages.LoginPage login = new(driver, this.instance_url);
            login.saveScreenshotAsAttachment();
            login.DoFillLoginForm(username, password, true);
        }

        goTo();
        if (isAlertPresent())
        {
            Console.WriteLine("FOUND ALERTS!");
        }

        Assert.IsTrue(validatePage(), $"Expected {GetType().Name} to pass page validation.");


    }

    public void goTo()
    {
        var page_url = new Uri(this.instance_url);
        page_url = new Uri(page_url, url_segment);
        driver.Url = page_url.AbsoluteUri;
    }

    public bool isAlertPresent()
    {
        var _alerts = this.driver.FindElements(By.XPath("//div[contains(@class, 'alert alert-danger no-margin')]"));
        if (_alerts.Count() >= 1)
        {
            Console.WriteLine($"Found an Alert!");
            foreach (var alert in _alerts)
            {
                Console.WriteLine(alert.Text);
            }
            return true;
        }
        return false;
    }

    private static string GenerateUniqueFileName(string baseFileName, string format)
    {
        // Generate a unique timestamp
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        // Combine the base file name with the timestamp
        string uniqueFileName = $"{baseFileName}_{timestamp}.{format}";

        // You can also add a file extension if needed
        // For example: uniqueFileName = $"{uniqueFileName}.txt";

        return uniqueFileName;
    }

    public void saveScreenshotAsAttachment(string _name = "")
    {
        if (string.IsNullOrWhiteSpace(_name))
        {
            _name = TestContext.CurrentContext.Test.Name;//this.GetType().Name;
        }

        string unique_name = GenerateUniqueFileName(_name, "png");
        TestContext.WriteLine($"Generate Screenshot: {unique_name}");
        driver.GetScreenshot().SaveAsFile(unique_name, ScreenshotImageFormat.Png);
        TestContext.AddTestAttachment(unique_name);
    }

    public void waitForLoad(WebDriver _driver, By _locator)
    {
        WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
        var element = wait.Until<IWebElement>(x =>
        {
            Console.WriteLine($"Looking for {_locator}...");
            var e = _driver.FindElement(_locator);
            if (e.Displayed)
            {
                Console.WriteLine($"Found {_locator}!");
                return e;
            }
            else
            {
                throw new NoSuchElementException($"Expected to Find {e}");
            }
        });
    }

    public void waitForClickable(WebDriver _driver, By _locator)
    {
        WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
        var element = wait.Until<IWebElement>(x =>
        {
            Console.WriteLine($"Looking for {_locator}...");
            var e = _driver.FindElement(_locator);
            if (e.Enabled)
            {
                Console.WriteLine($"{_locator} is Enabled!");
                return e;
            }
            else
            {
                throw new NoSuchElementException($"Expected to Find {e} Enabled");
            }
        });
    }
}
