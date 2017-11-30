using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using Onliner.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;

namespace Onliner
{
    class LoginPage
    {
        private IWebDriver driver;
        private By username = By.XPath("//input[@placeholder='Ник или e-mail']");
        private By password = By.XPath("//input[@placeholder='Пароль']");
        private By submit = By.ClassName("auth-box__auth-submit");
        private readonly string strUsername = GetConfig.GetUsername();
        private readonly string strPassword = GetConfig.GetPassword();
        
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SetUsername()
        {
            driver.FindElement(username).SendKeys(strUsername);
        }

        public void SetPassword()
        {
            driver.FindElement(password).SendKeys(strPassword);
        }

        public void SubmitLoginForm()
        {
            IWebElement submitButton = driver.FindElement(submit);
            Actions action = new Actions(driver);
            action.DoubleClick(submitButton).Perform();
        }

        public void Authorization()
        {
            SetUsername();
            SetPassword();
            SubmitLoginForm();
        }
    }
}
