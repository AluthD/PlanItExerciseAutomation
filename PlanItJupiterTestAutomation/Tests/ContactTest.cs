using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using PlanItJupiterTestAutomation.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace PlanItJupiterTestAutomation.Tests
{
    public class ContactTest : Base
    {

        //1.	From the home page go to contact page
        //2.	Click submit button
        //3.	Verify error messages
        //4.	Populate mandatory fields
        //5.	Validate errors are gone

        [Test]
        [TestCase("Chin", "chin@gmail.com","Test message")]
        public void TestContactPageValidationMessages(String foreName, String email, String message)
        {
            // Mandatory field validation error message text
            string alertErrMsg = "We welcome your feedback - but we won't get it unless you complete the form correctly.";
            string foreNameErrMsg = "Forename is required";
            string emailErrMsg = "Email is required";
            string messageErrMsg = "Message is required";


            HomePage homePage = new HomePage(getDriver());
            homePage.getContactMenu().Click();

            ContactPage contactPage = new ContactPage(getDriver());
            contactPage.clickSubmitButton();

            // Validate mandatory field error message displayed
            Assert.That(contactPage.verifyMandatoryFieldErrorMessage("Alert"), Is.EqualTo(alertErrMsg), message: "Alert Error Message is incorrect");
            Assert.That(contactPage.verifyMandatoryFieldErrorMessage("ForeName"), Is.EqualTo(foreNameErrMsg), message: "ForeName Mandatory Field Error Message is incorrect");
            Assert.That(contactPage.verifyMandatoryFieldErrorMessage("Email"), Is.EqualTo(emailErrMsg), message: "Email Mandatory Field Error Message is incorrect");
            Assert.That(contactPage.verifyMandatoryFieldErrorMessage("Message"), Is.EqualTo(messageErrMsg), message: "Message Error Message is incorrect");

            contactPage.getForeName().SendKeys(foreName);
            contactPage.getEmail().SendKeys(email);
            contactPage.getMessage().SendKeys(message);

            // Validate mandatory field error message disappeared 
            Assert.That(contactPage.verifyErrorMessageDisplayed("Alert"), Is.False, message: "Alert Error Message still displayed");
            Assert.That(contactPage.verifyErrorMessageDisplayed("ForeName"), Is.False, message: "ForeName Mandatory Field Error Message still displayed");
            Assert.That(contactPage.verifyErrorMessageDisplayed("Email"), Is.False, message: "Email Mandatory Field Error Message still displayed");
            Assert.That(contactPage.verifyErrorMessageDisplayed("Message"), Is.False, message: "Message Mandatory Field Error Message still displayed");

        }


        [Test, TestCaseSource(nameof(AddTestDataConfig))]
        public void TestContactPageSubmission(String foreName, String email, String message)
        {
            HomePage homePage = new HomePage(getDriver());
            homePage.getContactMenu().Click();

            ContactPage contactPage = new ContactPage(getDriver());

            contactPage.getForeName().SendKeys(foreName);
            contactPage.getEmail().SendKeys(email);
            contactPage.getMessage().SendKeys(message);

            contactPage.clickSubmitButton();
            Assert.That(contactPage.getThanksSuccessMessage(), Is.EqualTo($"Thanks {foreName}, we appreciate your feedback."), message: "Thanks Success message not displayed");

        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("foreName"), getDataParser().extractData("email"), getDataParser().extractData("message"));
            yield return new TestCaseData(getDataParser().extractData("foreName"), getDataParser().extractData("email"), getDataParser().extractData("message"));
            yield return new TestCaseData(getDataParser().extractData("foreName"), getDataParser().extractData("email"), getDataParser().extractData("message"));
            yield return new TestCaseData(getDataParser().extractData("foreName"), getDataParser().extractData("email"), getDataParser().extractData("message"));
            yield return new TestCaseData(getDataParser().extractData("foreName"), getDataParser().extractData("email"), getDataParser().extractData("message"));
        }


    }



}
