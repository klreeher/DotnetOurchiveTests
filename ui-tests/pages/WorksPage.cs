
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