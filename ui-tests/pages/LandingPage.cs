using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using System.IO;
using OpenQA.Selenium.Interactions;

namespace ui_tests.pages;

public class LandingPage : BasePage
{
    // LOCATORS


    private static By contentHeading = By.Id("index-content-heading");
    private static By contentMessage = By.Id("index-content-message");
    private static By searchForm = By.Id("index-content-search-form");
    private static By navLeftParent = By.XPath("//*[@id='uk-nav-left']/ul");
    private static By navRightParent = By.XPath("//*[@id='uk-nav-right']/ul");
    private static By navUserDropdown = By.Id("nav-user-dropdown-ul");
    private static By navUserListItems = By.XPath("//ul[@id='nav-user-dropdown-ul']/*");
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

    private List<string> loggedInUserNavItems = new(){
        "New Work",
        "Import Work(s)",
        "Import Status",
        "New Collection",
        "Bookmarks",
        "Collections",
        "Works",
        "Subscriptions",
        "Bookmarks",
        "Collections",
        "Profile",
        "Edit Profile",
        "Edit Account",
        "Blocklist",
        "Notifications",
        "Log Out"

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


    public void validateLoggedInUser(string loggedInUser)
    {
        this.waitForLoad(this.driver, loginSuccess);

        var userProfileMenu = this.driver.FindElement(navUsername);
        Assert.IsTrue(userProfileMenu.Displayed, $"Expected userProfileMenu to display username of logged in user.");
        Assert.IsTrue(userProfileMenu.Text.Contains(loggedInUser.ToUpper()), $"Expected {loggedInUser.ToUpper()} but got {userProfileMenu.Text}");

        // Page encapsulation to manage profile functionality
        //return new LandingPage(this.driver);
    }
    public bool validateUserNavList()
    {

        var userNavList = this.driver.FindElements(navUserListItems);
        foreach (var item in userNavList)
        {
            Console.WriteLine(item.GetType().ToString());
            Console.WriteLine(item.Text);

            Assert.Contains(item.Text, loggedInUserNavItems, $"Expected to find {item.Text} among loggedInUserNavItems");
        }

        return true;


    }

    public bool openRightNav()
    {
        var userProfileMenu = this.driver.FindElement(navUsername);
        Console.WriteLine($"Aria Expanded?: {userProfileMenu.GetAttribute("aria-expanded")}");
        Actions actions = new Actions(this.driver);
        actions.MoveToElement(userProfileMenu).Perform();
        Console.WriteLine($"Aria Expanded?: {userProfileMenu.GetAttribute("aria-expanded")}");
        if (userProfileMenu.GetAttribute("aria-expanded") == "true")
        {
            Console.WriteLine("Nav is Open");
            //return validateUserNavList();  // note: do not currently want to validate nav list as item names are wip
            return true;
        }
        else
        {
            return false;
        }

    }


    public bool IsThemeModeDark()
    {
        var toggleIcon = this.driver.FindElement(themeModeToggle);
        Console.WriteLine($"Toggle Icon: {toggleIcon.Displayed} / {toggleIcon.GetAttribute("id")}");
        if (toggleIcon.Displayed)
        {
            if (driver.FindElements(darkThemeMode).Count > 0)
            {
                if (driver.FindElement(darkThemeMode).Enabled)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void ChangeTheme()
    {
        this.driver.GetScreenshot().SaveAsFile("theme_01.png", ScreenshotImageFormat.Png);
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
        this.driver.GetScreenshot().SaveAsFile("theme_02.png", ScreenshotImageFormat.Png);

    }

}