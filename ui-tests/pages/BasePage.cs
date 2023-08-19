using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;



namespace ui_tests.pages;

public abstract class BasePage
{
    protected WebDriver? driver = null;
    protected abstract string url_segment { get; }
    protected string? instance_url;

    public abstract bool validatePage();

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


        var page_url = new Uri(this.instance_url);
        page_url = new Uri(page_url, url_segment);
        driver.Url = page_url.AbsoluteUri;

        Assert.IsTrue(validatePage(), $"Expected {GetType().Name} to pass page validation.");

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
}
