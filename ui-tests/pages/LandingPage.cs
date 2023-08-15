using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using System.IO;


namespace ui_tests.pages;

public class LandingPage : BasePage
{
    // LOCATORS


    private static By contentHeading = By.Id("index-content-heading");
    private static By contentMessage = By.Id("index-content-message");
    private static By searchForm = By.Id("index-content-search-form");
    private static By navLeftParent = By.XPath("//*[@id='uk-nav-left']/ul");
    private static By navRightParent = By.XPath("//*[@id='uk-nav-right']/ul");
    private static By footer = By.XPath("//*[contains(@id,'ourchive-footer')]");

    private static By login = By.Id("nav-login-link");
    private static By register = By.Id("nav-register-link");
    private static By searchIcon = By.XPath("//*[contains(@id,'uk-search-icon')]");



    private static By themeModeToggle = By.XPath("//*[contains(@id,'theme-switcher')]");
    private static By lightThemeMode = By.XPath("//span[@id='theme-switcher-light']");
    private static By darkThemeMode = By.XPath("//span[@id='theme-switcher-dark']");

    private List<By> onLoadElements = new List<By>(){
        contentHeading,
        contentMessage,
        searchForm,
        navLeftParent,
        navRightParent,
        footer,
        login,
        register
    };


    private static By loginSuccess = By.Id("login-success");
    private static By navUsername = By.Id("nav-username");

    private List<By> loggedInElements = new List<By>(){
        contentHeading,
        contentMessage,
        searchForm,
        navLeftParent,
        navRightParent,
        footer
    };


    override protected string url_segment => "";

    public LandingPage(WebDriver _driver, string _instance_url = "") : base(_driver, _instance_url)
    {

    }


    public override bool validatePage()
    {
        foreach (var item in onLoadElements)
        {
            this.waitForLoad(this.driver, item);
        }
        Assert.IsTrue(this.driver.Title.Contains("Ourchive"), $"Expected {GetType().Name} Init Page Title to Contain `Ourchive` but instead found `{this.driver.Title}`");
        Assert.IsTrue(this.driver.Url.Contains(this.url_segment), $"Expected {GetType().Name} Init Url to Contain `{this.url_segment}` but instead found `{this.driver.Url}`");
        return true;
    }


    public void loggedInUser(string loggedInUser)
    {
        this.waitForLoad(this.driver, loginSuccess);

        var userProfileMenu = this.driver.FindElement(navUsername);
        Assert.IsTrue(userProfileMenu.Displayed, $"Expected userProfileMenu to display username of logged in user.");
        Assert.IsTrue(userProfileMenu.Text.Contains(loggedInUser.ToUpper()), $"Expected {loggedInUser.ToUpper()} but got {userProfileMenu.Text}");

        // Page encapsulation to manage profile functionality
        //return new LandingPage(this.driver);

    }

    public bool IsThemeModeDark()
    {
        var toggleIcon = this.driver.FindElement(themeModeToggle);
        Console.WriteLine($"Toggle Icon: {toggleIcon.Displayed} / {toggleIcon.GetAttribute("id")}");
        if (toggleIcon.Displayed && (toggleIcon.GetAttribute("id").Contains("light")))
        {
            Console.WriteLine(toggleIcon.GetAttribute("title"));
            return false;
        }

        return true;
    }

    public void ChangeTheme()
    {
        this.driver.GetScreenshot().SaveAsFile("theme_01", ScreenshotImageFormat.Png);
        bool prior = IsThemeModeDark();
        this.driver.ExecuteScript("return switchTheme()");
        if (prior)
        {
            this.waitForLoad(this.driver, lightThemeMode);
        }
        else
        {
            this.waitForLoad(this.driver, darkThemeMode);
        }
        this.driver.GetScreenshot().SaveAsFile("theme_02", ScreenshotImageFormat.Png);

    }

}