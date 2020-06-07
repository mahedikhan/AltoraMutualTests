using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Tests.SeleniumHelpers;

namespace Structura.GuiTests.PageObjects
{
    class ContactFeedbackPage
    {
        String FeedbackURL = "http://demo.testfire.net/feedback.jsp";
        private readonly IWebDriver _driver;

        public ContactFeedbackPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement ContactFeedbackPageLink => _driver.FindElement(By.XPath("//a[@id='HyperLink4']"));
        public IWebElement YourNameField => _driver.FindElement(By.XPath("//input[@name='name']"));
        public IWebElement EmailField => _driver.FindElement(By.XPath("//input[@name='email_addr']"));
        public IWebElement SubjectField => _driver.FindElement(By.XPath("//input[@name='subject']"));
        public IWebElement QuestionCommentField => _driver.FindElement(By.XPath("//textarea[@name='comments']"));
        public IWebElement FeedbackSubmitButton => _driver.FindElement(By.XPath("//input[@name='submit']"));
        public IWebElement FeedBackFormConfirmation => _driver.FindElement(By.XPath("//h1[contains(text(),'Thank You')]"));

        public void NavigateToFeedbackPage()
        {
            
            _driver.Navigate().GoToUrl(FeedbackURL);
        }

        public void InputName()
        {
            YourNameField.Clear();
            YourNameField.SendKeys("John Dow");
        }

        public void InputEmail()
        {
            EmailField.Clear();
            EmailField.SendKeys("Johndow1989@gmail.com");
        }

        public void InputSubject()
        {
            SubjectField.Clear();
            SubjectField.SendKeys("Banking Feedback");
        }

        public void InputQuestionComment()
        {
            QuestionCommentField.Clear();
            QuestionCommentField.SendKeys("How does online Banking work?");
        }

        public void ClickOnSubmit()
        {

            FeedbackSubmitButton.Click();
        }
        public String getFeedBackText()
        {
            return FeedBackFormConfirmation.Text;
        }
    }
}
