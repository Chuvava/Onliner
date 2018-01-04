using OpenQA.Selenium;
using NUnit.Framework;
using Onliner.Framework;


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

        public void AssertCategory(string nameCategory)
        {
            string H1 = driver.FindElement(h1).GetAttribute("innerText");
            Assert.AreEqual(H1, nameCategory, "True category");
        }

        public void NavigateToMainPage()
        {
            driver.Url = Configuration.GetUrl();
        }
    }
}
