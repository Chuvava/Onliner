using System;
using OpenQA.Selenium;
using NUnit.Framework;
using Onliner.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace Onliner
{
    class MainPage
    {
        private IWebDriver driver;
        private IWebElement profile;
        private IWebElement enter;
        private IWebElement exit;
        private By enterButton = By.XPath("//div[contains(text(), 'Вход')]");
        private By categories = By.CssSelector(".project-navigation__flex a");
        private By profileButton = By.XPath("//a[@href='https://profile.onliner.by']");
        private By exitButton = By.LinkText("Выйти");
        private By people = By.XPath("//h2/a[contains(text(), 'Люди')]");
        private string strPattern = "main-2__text\">(.+?)<";
        private string url = Configuration.GetUrl();
        private WebDriverWait wait;

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            IWebElement peopleElement = driver.FindElement(people);
            Assert.True(peopleElement.Displayed, "Onliner.by main page opened");
        }

        public void ClickEnterButton()
        {
            driver.FindElement(enterButton).Click();
        }

        public void AssertAuthorization()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Configuration.GetExpectedWait()));
            profile = wait.Until(ExpectedConditions.ElementExists(profileButton));
            Assert.True(profile.Displayed, "Authorization completed");
        }

        public string PickRandomOfCategories()
        {
            var elements = driver.FindElements(categories);
            Random rand = new Random();
            checkVisible:
            int item = rand.Next(0, elements.Count);
            if (elements[item].Displayed)
            {
                string categoryName = elements[item].Text;
                elements[item].Click();
                return categoryName;
            }
            goto checkVisible;
        }

        public void WriteOpinionsInCsv()
        {
            CsvWriter.PickOpinionsInCsv(strPattern);
        }

        public void LogOut()
        {
            Actions action = new Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Configuration.GetExpectedWait()));
            profile = wait.Until(x => x.FindElement(profileButton));
            action.MoveToElement(profile).Click();
            driver.FindElement(profileButton).Click();
            exit = wait.Until(x => x.FindElement(exitButton));
            exit.Click();
        }

        public void AssertLogOut()
        {
            Assert.True(driver.FindElement(enterButton).Displayed, "The exit is complete");
        }
    }
}
