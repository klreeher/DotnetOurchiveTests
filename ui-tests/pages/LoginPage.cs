using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ui_tests.pages;

public class LoginPage : BasePage
{

    // LOCATORS
    private By contentHeading = By.Id("index-content-heading");
    private By contentMessage = By.Id("index-content-message");
    private By searchForm = By.Id("index-content-search-form");
    private By navLeftParent = By.XPath("//*[@id='uk-nav-left']/ul");
    private By navRightParent = By.XPath("//*[@id='uk-nav-right']/ul");
    private By footer = By.Id("sign_in");


    override protected string url_segment => "login";

    public LoginPage(WebDriver _driver, string _instance_url) : base(_driver, _instance_url)
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