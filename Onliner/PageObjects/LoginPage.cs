using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Onliner.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace Onliner
{
    class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private IWebElement webUsername;
        private IWebElement webPassword;
        private IWebElement btnSubmit;
        private By username = By.XPath("//input[@placeholder='Ник или e-mail']");
        private By password = By.XPath("//input[@placeholder='Пароль']");
        private By submit = By.ClassName("auth-box__auth-submit");
        
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            Assert.IsTrue(driver.FindElement(username).Displayed, "Login Page opened");
        }

        public void SetUsername(string strUsername)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Configuration.GetExpectedWait()));
            webUsername = wait.Until(ExpectedConditions.ElementToBeClickable(username)); 
            webUsername.SendKeys(strUsername);
        }

        public void SetPassword(string strPassword)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Configuration.GetExpectedWait()));
            webPassword = wait.Until(ExpectedConditions.ElementToBeClickable(password));
            webPassword.SendKeys(strPassword);
        }

        public void SubmitLoginForm()
        {            
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Configuration.GetExpectedWait()));
            btnSubmit = wait.Until(x => x.FindElement(submit));
            Actions action = new Actions(driver);
            action.DoubleClick(btnSubmit).Perform();
        }

        public void Authorization(string strUsername, string strPassword)
        {
            SetUsername(strUsername);
            SetPassword(strPassword);
            SubmitLoginForm();
        }
    }
}
