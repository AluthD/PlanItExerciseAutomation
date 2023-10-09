using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using PlanItJupiterTestAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace PlanItJupiterTestAutomation.Pages
{
    public class Base
    {
        protected IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            BrowserExtention browserExtention = new BrowserExtention();

            string browserName = ConfigurationManager.AppSettings["browser"];
            InitBrowser(browserName);

            driver.Manage().Window.Maximize();
            driver.Url = getDataParser().extractData("url");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            TestContext.Progress.WriteLine(driver.Title);
        }

        public IWebDriver getDriver()
        {
            return driver;
        }


        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":

                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;

                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;

            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void TearDownBrowser()
        {
            driver.Close();
            //driver.Quit();
        }

    }
}
