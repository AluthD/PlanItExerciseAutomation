using OpenQA.Selenium;
using PlanItJupiterTestAutomation.Utilities;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanItJupiterTestAutomation.Pages
{
    internal class ContactPage
    {
        IWebDriver driver;
        public ContactPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
                

        [FindsBy(How = How.CssSelector, Using = ".btn-contact.btn.btn-primary")]
        private IWebElement submitButton;

        [FindsBy(How = How.CssSelector, Using = "#header-message")]
        private IWebElement alertErrorMessageElement;

        private By alertErrorMessage = By.CssSelector(".alert.alert-error.ng-scope");

        [FindsBy(How = How.CssSelector, Using = "div[ng-class*='form.f']")]
        private IWebElement foreNameBlockElement;

        private By foreNameErrorMessage = By.CssSelector("#forename-err");

        [FindsBy(How = How.CssSelector, Using = "#email-group")]  
        private IWebElement emailBlockElement;

        private By emailErrorMessage = By.CssSelector("#email-err");

        [FindsBy(How = How.CssSelector, Using = "#message-group")] 
        private IWebElement messageBlockElement;

        private By messageErrorMessage = By.CssSelector("#message-err");

        [FindsBy(How = How.CssSelector, Using = "#forename")]
        private IWebElement foreName;

        [FindsBy(How = How.CssSelector, Using = "#surname")]
        private IWebElement surName;

        [FindsBy(How = How.CssSelector, Using = "#email")]
        private IWebElement email;

        [FindsBy(How = How.CssSelector, Using = "#telephone")]
        private IWebElement telephone;

        [FindsBy(How = How.CssSelector, Using = "#message")]
        private IWebElement message;

        private By thanksSuccessMessage = By.CssSelector(".alert.alert-success");



        public IWebElement getForeName()
        {
            return foreName;
        }

        public IWebElement getSurname()
        {
            return surName;
        }
        public IWebElement getEmail()
        {
            return email;
        }

        public IWebElement getTelephone()
        {
            return telephone;
        }
        public IWebElement getMessage()
        {
            return message;
        }

        public void clickSubmitButton()
        { 
            submitButton.Click(); 
        }

        public string verifyMandatoryFieldErrorMessage(string fieldName)
        {
            string messageText = string.Empty;
            switch(fieldName)
            {
                case "Alert":
                    messageText = getErrorMessage(alertErrorMessage); 
                    break;

                case "ForeName":
                    messageText = getErrorMessage(foreNameErrorMessage); 
                    break;

                case "Email":
                    messageText = getErrorMessage(emailErrorMessage); 
                    break;

                case "Message":
                    messageText = getErrorMessage(messageErrorMessage);
                    break;
            }
            return messageText;

        }

        public string getErrorMessage(By locator)
        {
            return driver.FindElement(locator).Text;

        }


        public bool verifyErrorMessageDisplayed(string messageField)
        {
            bool isErrorMessageDisplayed = false;
            switch (messageField)
            {
                case "Alert":
                    isErrorMessageDisplayed = isMessageDisplayed(alertErrorMessageElement,alertErrorMessage);
                    break;

                case "ForeName":
                    isErrorMessageDisplayed = isMessageDisplayed(foreNameBlockElement, foreNameErrorMessage); 
                    break;

                case "Email":
                    isErrorMessageDisplayed = isMessageDisplayed(emailBlockElement, emailErrorMessage);
                    break;

                case "Message":
                    isErrorMessageDisplayed = isMessageDisplayed(messageBlockElement, messageErrorMessage);
                    break;
            }
            return isErrorMessageDisplayed;
        }

        public bool isMessageDisplayed(IWebElement element, By locator)
        {
            return element.FindElements(locator).Any();
        }

        public string getThanksSuccessMessage()
        {
            WaitExtention.waitForElementToLoad(driver,thanksSuccessMessage, 30);
            return driver.FindElement(thanksSuccessMessage).Text;
        }



    }
}
