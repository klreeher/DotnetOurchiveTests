
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

public class WorksPage : BasePage
{

    private static By controlsTop = By.Id("work-form-top-controls-parent");
    private static By titleDisplay = By.Id("work-form-title-parent");

    private static By workFormCancel = By.Id("work-form-cancel-top");
    private static By workFormSubmit = By.Id("work-form-submit-top");


    private List<By> onLoadElements = new List<By>(){
        titleDisplay,
        workFormCancel,
        workFormSubmit,
    };


    override protected string url_segment => "works/new";

    /// <summary>
    /// this should eventually be replaced with api calls to get the instance's configured data. 
    /// this is currently hardcoded to the ourchive-dev site values
    /// </summary>
    Dictionary<string, List<string>> adminAttribs = new Dictionary<string, List<string>>
    {
        { "workTypes",  new List<string>{"Podfic", "Fic", "Art"} },
    };

    /// <summary>
    /// this should eventually be replaced with api calls to get the instance's configured data. 
    /// this is currently hardcoded to the ourchive-dev site values
    /// </summary>
    Dictionary<string, List<string>> systemTags = new Dictionary<string, List<string>>
    {
        { "genre",  new List<string>{"Adventure", "isekai", "Romance"} },
        { "pairingType",  new List<string>{"F/F", "F/M", "M/M"} },
    };


    /// <summary>
    /// this should eventually be replaced with api calls to get the instance's configured data. 
    /// this is currently hardcoded to the ourchive-dev site values
    /// </summary>
    Dictionary<string, List<string>> dynamicTags = new Dictionary<string, List<string>>
    {
        { "workTypes",  new List<string>{"Podfic", "Fic", "Art"} },
    };
    public WorksPage(WebDriver _driver, string _instance_url = "") : base(_driver, _instance_url)
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

}