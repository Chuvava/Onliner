using System;
using OpenQA.Selenium;


namespace Onliner.Framework
{
    public class Browser
    {
        private readonly IWebDriver driver = BrowserFactory.GetDriver();
        private static Browser browser;
        private Browser()
        {
        }

        public static Browser GetInstance()
        {
            if (browser == null)
            {
                browser = new Browser();
                browser.SetImplicitWait(Configuration.GetImplicitWait());
                browser.WindowMaximize();
                browser.NavigateToUrl(Configuration.GetUrl());
            }
            return browser;
        }

        public IWebDriver GetBrowser()
        {
            return driver;
        }

        public void SetImplicitWait(int timeout)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
        }

        public void WindowMaximize()
        {
            driver.Manage().Window.Maximize();
        }

        public void NavigateToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void Quit()
        {
            driver.Quit();
        }

        public static void CloseBrowser()
        {
            browser.Quit();
        }

        public string GetPageSource()
        {
            return driver.PageSource;
        }

        public static string PageSource()
        {
            return browser.GetPageSource();
        }
    }
}
