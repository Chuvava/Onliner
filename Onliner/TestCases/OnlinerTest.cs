using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit;
using NUnit.Framework;
using Onliner.Framework;
using OpenQA.Selenium.Support.UI;

namespace Onliner
{
    [TestFixture]
    public class OnlinerTest
    {
        private IWebDriver driver;
        private MainPage objMainPage;
        private LoginPage objLoginPage;
        private SomeCategoryPage objCategoryPage;
        public static string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [SetUp]
        public void Setup()
        {
            Browser.InitBrowser(GetConfig.GetBrowser());
            driver = Browser.Driver;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(GetConfig.GetImplicitWait());            
        }

        [Test]
        public void TestOnliner()
        {
            objMainPage = new MainPage(driver);
            objLoginPage = objMainPage.ClickEnterButton();
            objLoginPage.Authorization();
            objMainPage.AssertAuthorization();
                        
            string nameCategory = objMainPage.PickRandomOfCategories();
            objCategoryPage = new SomeCategoryPage(driver);
            objCategoryPage.AssertCategory(nameCategory);
            objMainPage.PickOpinionsInCsv();
            objMainPage.LogOut();
            objMainPage.AssertLogOut();
        }

        [TearDown]
        public void CloseBrowser()
        {
            Browser.CloseBrowser();
        }
    }
}
