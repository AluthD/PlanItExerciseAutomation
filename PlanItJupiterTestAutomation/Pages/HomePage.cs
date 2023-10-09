using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanItJupiterTestAutomation.Pages
{
    internal class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // Pageobject factory 

        [FindsBy(How = How.CssSelector, Using = "#nav-contact")]
        private IWebElement contactMenu;

        [FindsBy(How = How.CssSelector, Using = "#nav-shop")]
        private IWebElement shopMenu;

        public IWebElement getContactMenu()
        {
            return contactMenu;
        }

        public IWebElement getShopMenu()
        {
            return shopMenu;
        }



    }
}
