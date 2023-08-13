using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ui_tests.pages;

public class LoginPage
{
    protected IWebDriver driver;
    protected Uri base_url = new("https://ourchive-dev.stopthatimp.net/");
    protected string url_segment = "";

    // LOCATORS
    private By contentHeading = By.Id("index-content-heading");
    private By contentMessage = By.Id("index-content-message");
    private By searchForm = By.Id("index-content-search-form");
    private By navLeftParent = By.XPath("//*[@id='uk-nav-left']/ul");
    private By navRightParent = By.XPath("//*[@id='uk-nav-right']/ul");
    private By footer = By.Id("sign_in");

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        driver.Url = new Uri(base_url, url_segment).AbsoluteUri;
        if (!(driver.Title == ("Ourchive")))
        {
            throw new OpenQA.Selenium.InvalidElementStateException("This is not the Landing Page," +
                  " current page is: " + driver.Url);
        }
    }

    public LoginPage manageProfile()
    {
        // Page encapsulation to manage profile functionality
        return new LoginPage(driver);
    }
}