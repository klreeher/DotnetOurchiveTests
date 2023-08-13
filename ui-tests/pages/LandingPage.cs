using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Microsoft.Extensions.Logging;


namespace ui_tests.pages;

public class LandingPage
{
    protected WebDriver? driver = null;
    protected string url_segment = "";

    // LOCATORS
    private By contentHeading = By.Id("index-content-heading");
    private By contentMessage = By.Id("index-content-message");
    private By searchForm = By.Id("index-content-search-form");
    private By navLeftParent = By.XPath("//*[@id='uk-nav-left']/ul");
    private By navRightParent = By.XPath("//*[@id='uk-nav-right']/ul");
    private By footer = By.Id("sign_in");

    public LandingPage(WebDriver _driver, string _instance_url = "")
    {
        string? base_url;
        if (string.IsNullOrEmpty(_instance_url))
        {
            try
            {
                base_url = Environment.GetEnvironmentVariable("BaseUrl");
            }
            catch (Exception e)
            {
                new Exception("No instance url provided, and no environment variable found named `BaseUrl`." + e);
                throw;
            }
        }
        else
        {
            Console.Write($"found instance url: {_instance_url}\n");
            base_url = _instance_url;
        }

        if (this.driver is null)
        {
            Console.Write("Overriding the null driver with pre-existing driver instance:");
            this.driver = _driver;
        }

        var page_url = new Uri(new Uri(base_url), url_segment);
        driver.Url = page_url.AbsoluteUri;


        Console.Write($"\nPage Url: {page_url} from {base_url} and {this.url_segment}");

        if (!(this.driver.Title == ("Ourchive")))
        {
            throw new OpenQA.Selenium.InvalidElementStateException($"Expected the LandingPage to be at: {page_url}, however found: {driver.Url}");
        }

    }

    public LandingPage manageProfile()
    {
        // Page encapsulation to manage profile functionality
        return new LandingPage(driver);
    }
}