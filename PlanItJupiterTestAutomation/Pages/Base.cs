using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using LivingDoc.Dtos;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V115.Page;
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
        public ExtentReports extent;
        public ExtentTest reportingTest;

        // Report File
        [OneTimeSetUp] 
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports(); 
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("UserName", "Chinthaka Samarasinghe");

        }

        [SetUp]
        public void StartBrowser()
        {
            // Get the test name when executing
            reportingTest = extent.CreateTest(TestContext.CurrentContext.Test.Name);

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
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

       
            if (status == TestStatus.Failed)
            {

                reportingTest.Fail("Test Failed");
                reportingTest.Log(Status.Fail, "test failed with log trace: " + stackTrace);
            }
            else if(status == TestStatus.Passed)
            {
                reportingTest.Pass("Test Passed");
            }

            extent.Flush();
            driver.Close();
            //driver.Quit();
        }

        //public MediaEntityModelProvider CaptureScreenShot(String screenShotName)
        //{
        //    ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
        //    var screenShot = takesScreenshot.GetScreenshot().AsBase64EncodedString;
        
        //    return  MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, screenShotName).Build;

        //}

    }
}
