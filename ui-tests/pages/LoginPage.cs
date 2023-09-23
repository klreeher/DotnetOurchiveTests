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
    private static By contentHeading = By.Id("index-content-heading");
    private static By contentMessage = By.Id("index-content-message");
    private static By searchForm = By.Id("index-content-search-form");
    private static By navLeftParent = By.XPath("//*[@id='uk-nav-left']/ul");
    private static By navRightParent = By.XPath("//*[@id='uk-nav-right']/ul");
    private static By footer = By.XPath("//*[contains(@id,'ourchive-footer')]");

    private static By input_username = By.Id("login-username-input");
    private static By input_password = By.Id("login-password-input");
    private static By submit_button = By.Id("login-submit-button");
    private static By password_reset = By.Id("login-pw-reset-link");

    private List<By> onLoadElements = new List<By>(){
        contentHeading,
        contentMessage,
        searchForm,
        navLeftParent,
        navRightParent,
        footer,
        input_username,
        input_password,
        submit_button,
        password_reset
    };



    override protected string url_segment => "login";
    protected override bool requiresLogin => false;

    public LoginPage(WebDriver _driver, string _instance_url = "") : base(_driver, _instance_url)
    {
    }

    public override bool validatePage()
    {
        this.waitForLoad(this.driver, footer);
        Assert.IsTrue(this.driver.Url.Contains(this.url_segment), $"Expected LoginPage Url to Contain `{this.url_segment}` but instead found `{this.driver.Url}`");
        return true;
    }

    public void DoFillLoginForm(string username, string password, bool do_submit)
    {

        this.waitForLoad(this.driver, input_username);
        this.waitForLoad(this.driver, input_password);

        var username_input = this.driver.FindElement(input_username);
        var password_input = this.driver.FindElement(input_password);

        username_input.Click();
        username_input.SendKeys(username);

        password_input.Click();
        password_input.SendKeys(password);

        if (do_submit)
        {
            var submit = this.driver.FindElement(submit_button);
            submit.Click();
        }
    }


}