using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanItJupiterTestAutomation.Utilities
{
    public static class WaitExtention
    {

        public static void waitForElementToLoad(this IWebDriver driver, By elementLocator, int time)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
        }

        public static void WaitForPageLoad(this IWebDriver driver, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.Until(driver =>
            {
                bool isPageLoaded = (driver as IJavaScriptExecutor)?.ExecuteScript("return document.readyState").Equals("complete") ?? false;
                return isPageLoaded;
            });
        }
    }
}
