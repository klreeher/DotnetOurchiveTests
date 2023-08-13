using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Microsoft.Extensions.Logging;


namespace ui_tests.pages;

public abstract class BasePage
{
    protected WebDriver? driver = null;
    protected abstract string url_segment { get; }
    protected string instance_url;

    public abstract bool validatePage();

    public BasePage(WebDriver _driver, string _instance_url = "")
    {
        this.instance_url = _instance_url;
        if (string.IsNullOrEmpty(_instance_url))
        {
            try
            {
                this.instance_url = Environment.GetEnvironmentVariable("BaseUrl");
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
            this.instance_url = _instance_url;
        }

        if (this.driver is null)
        {
            Console.Write("Overriding the null driver with pre-existing driver instance:");
            this.driver = _driver;
        }

        if (this.url_segment == string.Empty && this.instance_url is not null)
        {
            var page_url = new Uri(this.instance_url);
            driver.Url = page_url.AbsoluteUri;
            Console.Write($"\nPage Url: {page_url} from {this.instance_url}");
        }
        else if (this.instance_url != null)
        {
            var page_url = new Uri(this.instance_url);
            page_url = new Uri(page_url, url_segment);
            driver.Url = page_url.AbsoluteUri;
            Console.Write($"\nPage Url: {page_url} from {this.instance_url} and {this.url_segment}");
        }




        Assert.IsTrue(this.validatePage());

    }

}

public class LandingPage : BasePage
{
    // LOCATORS
    private By contentHeading = By.Id("index-content-heading");
    private By contentMessage = By.Id("index-content-message");
    private By searchForm = By.Id("index-content-search-form");
    private By navLeftParent = By.XPath("//*[@id='uk-nav-left']/ul");
    private By navRightParent = By.XPath("//*[@id='uk-nav-right']/ul");
    private By footer = By.Id("sign_in");


    override protected string url_segment => "";

    public LandingPage(WebDriver _driver, string _instance_url = "") : base(_driver, _instance_url)
    {
    }

    public override bool validatePage()
    {
        if (this.driver.Title == ("Ourchive"))
        {
            return true;
        }

        return false;
    }

}