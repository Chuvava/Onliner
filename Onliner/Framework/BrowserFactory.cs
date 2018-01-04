using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;


namespace Onliner.Framework
{
    class BrowserFactory
    {
        private static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            switch (Configuration.GetBrowser())
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    return driver;
                case "Firefox":
                    driver = new FirefoxDriver();
                    return driver;
                default:
                    throw new DriveNotFoundException();
            }
        }
    }
}
