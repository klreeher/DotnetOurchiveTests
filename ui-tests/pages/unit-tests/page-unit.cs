using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using ui_tests.pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;



public class PageUnitTests
{
    WebDriver _driver;


    [SetUp]
    public void SetUp() // Non-virtual
    {
        // Do required work

        // Call virtual method now...
        new DriverManager().SetUpDriver(new ChromeConfig());
        _driver = new ChromeDriver();

    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }

    [Test]
    public void inheritTest()
    {

        LandingPage instance = new LandingPage(_driver, _instance_url: "https://ourchive-dev.stopthatimp.net/");

        Console.WriteLine(instance.ToString());

    }
}