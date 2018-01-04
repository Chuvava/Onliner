using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using NUnit.Framework;
using Onliner.Framework;


namespace Onliner
{
    [TestFixture]
    public class OnlinerTest
    {
        private IWebDriver driver;
        public static string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [SetUp]
        public void Setup()
        {
            driver = Browser.GetInstance().GetBrowser();
        }

        [Test]
        public void TestOnliner()
        {
            MainPage mainPage = new MainPage(driver);
            mainPage.ClickEnterButton();
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Authorization(Configuration.GetUsername(), Configuration.GetPassword());
            mainPage.AssertAuthorization();
                        
            string nameCategory = mainPage.PickRandomOfCategories();
            SomeCategoryPage  categoryPage = new SomeCategoryPage(driver);
            categoryPage.AssertCategory(nameCategory);
            categoryPage.NavigateToMainPage();
            mainPage.WriteOpinionsInCsv();
            mainPage.LogOut();
            mainPage.AssertLogOut();
        }

        [TearDown]
        public void AfterTest()
        {
            Browser.CloseBrowser();
        }
    }
}
