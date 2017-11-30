using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;

namespace Onliner
{
    class SomeCategoryPage
    {
        private IWebDriver driver;
        private By h1 = By.ClassName("schema-header__title");

        public SomeCategoryPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public MainPage AssertCategory(string nameCategory)
        {
            string H1 = driver.FindElement(h1).GetAttribute("innerText");
            Assert.AreEqual(H1, nameCategory, "True category");
            return new MainPage(driver);
        }
    }
}
