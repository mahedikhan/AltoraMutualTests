using System;
using System.Globalization;
using System.Text;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Structura.GuiTests.PageObjects;
using Structura.GuiTests.SeleniumHelpers;
using Structura.GuiTests.Utilities;
using Tests.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace Structura.GuiTests
{
    [TestFixture]
    public class AltoroMutualTests
    {
        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private string _baseUrl;

        [SetUp]
        public void SetupTest()
        {
            _driver = new DriverFactory().Create();
            _baseUrl = ConfigurationHelper.Get<string>("TargetUrl");
            _verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                _driver.Quit();
                _driver.Close();
            }
            catch (Exception)
            {
                // Ignore errors if we are unable to close the browser
            }
            _verificationErrors.ToString().Should().BeEmpty("No verification errors are expected.");
        }

        [Test]
        public void LoginWithValidCredentialsShouldSucceed()
        {
            // Arrange
            var expected = true; //Login Success 

            // Act
            var page = new LoginPage(_driver);
            page.LoginAsAdmin(_baseUrl);

            // Assert
            var actual = new MainPage(_driver).GetAccountButton.Displayed;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoginWithInvalidCredentialsShouldFail()
        {
            // Arrange
            // Act
            var page = new LoginPage(_driver);
            page.LoginAsNobody(_baseUrl);

            // Assert
            //Action a = () =>
            //{
               // var displayed = new MainPage(_driver).GetAccountButton.Displayed; // throws exception if not found
           // };
           // a.ShouldThrow<NoSuchElementException>();
            // New Assert

            string actualvalue = _driver.FindElement(By.Id("_ctl0__ctl0_Content_Main_message")).Text;
            Assert.IsTrue(actualvalue.Contains("Login Failed: We're sorry, but this username or password was not found in our system. Please try again."), actualvalue + " doesn't contains 'Correct message.'");

        }

        [Test]
        public void RequestGoldenVisaShouldBeAccepted()
        {
            // Arrange
            new LoginPage(_driver).LoginAsAdmin(_baseUrl);
            var page = new RequestGoldVisaPage(_driver);
            new MainPage(_driver).NavigateToTransferFunds();

            // Act
            page.PerformRequest();

            // Assert

            // Need to wait until the results are displayed on the web page
            //Thread.Sleep(5000); //Hard sleeps are bad! Don't use them.


            IWebElement WaitForMessage = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists((By.XPath("//span[@id='_ctl0__ctl0_Content_Main_lblMessage']"))));




            page.SuccessMessage.Text.StartsWith(
                "Your new Altoro Mutual Gold VISA with a $10000 and 7.9% APR will be sent in the mail."
                , true, CultureInfo.InvariantCulture).Should().BeTrue();
        }



            [Test]
        public void ContactFeedbackFormValidation()
        {
            // Perform login
            var page = new LoginPage(_driver);
            page.LoginAsAdmin(_baseUrl);

            var feedbackPage = new ContactFeedbackPage(_driver);
            //Navigates directly to the Feedback URL
            feedbackPage.NavigateToFeedbackPage();
            // Input name in name field
            feedbackPage.InputName();
            //Input email in email field
            feedbackPage.InputEmail();
            // Type in subject in subject field
            feedbackPage.InputSubject();
            //Type in Questions/Comment field
            feedbackPage.InputQuestionComment();
            // CLick on Submit button
            feedbackPage.ClickOnSubmit();

            String actualvalue = feedbackPage.getFeedBackText();
            // Verifying The Feedback Form was submitted by asserting Thank you message.
            Assert.IsTrue(actualvalue.Contains("Thank You"), actualvalue + "Feedback Form Not Submitted ");


        }
    }
    
}


