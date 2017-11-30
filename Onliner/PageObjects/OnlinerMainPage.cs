using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using System.Text.RegularExpressions;
using NUnit.Framework.Internal;
using Onliner.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace Onliner
{

    class MainPage
    {
        private IWebDriver driver;
        private By enterButton = By.XPath("//div[@id='userbar']//div[contains(@class, 'item--text')]");
        private By categories = By.CssSelector(".project-navigation__flex a");
        private By profileButton = By.ClassName("b-top-profile__image");
        private By exitButton = By.LinkText("Выйти");
        private By people = By.XPath("//h2/a[contains(text(), 'Люди')]");
        private string url = GetConfig.GetUrl();
        private WebDriverWait wait;

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            this.driver.Url = url;
            IWebElement peopleElement = driver.FindElement(people);
            Assert.True(peopleElement.Displayed, "Onliner.by main page opened");
        }

        public LoginPage ClickEnterButton()
        {
            driver.FindElement(enterButton).Click();
            return new LoginPage(driver);
        }

        public void AssertAuthorization()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(GetConfig.GetExpectedWait()));
            IWebElement profile = wait.Until(ExpectedConditions.ElementExists(profileButton));
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

        public void PickOpinionsInCsv()
        {
            var pattern = new Regex("main-2__text\">(.+?)<");
            string pageSource = driver.PageSource;
            Match match = pattern.Match(pageSource);
            FileStream file = new FileStream(OnlinerTest.appDir + GetConfig.GetPathCsv(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            StreamWriter writer = new StreamWriter(file);
            
            while (match.Success)
            {
                writer.WriteLine("\"" + match.Groups[1].Value + "\"");
                match = match.NextMatch();
            }
            writer.Flush();
        }

        public void LogOut()
        {
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(profileButton)).Click();
            driver.FindElement(profileButton).Click();
            driver.FindElement(exitButton).Click();
        }

        public void AssertLogOut()
        {
            Assert.True(driver.FindElement(enterButton).Displayed, "The exit is complete");
        }
    }
}
