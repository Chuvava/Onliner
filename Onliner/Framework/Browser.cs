using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Onliner.Framework
{
    class Browser
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = 
            new Dictionary<string, IWebDriver>();

        private static IWebDriver driver;
        private static object valueBlock = new object();
      
        public static IWebDriver Driver
        {
            get
            {
                return driver;
            }
            private set
            { driver = value; }
        }

        public static void InitBrowser(string browserName)
        {
            lock (valueBlock)
            {
                switch (browserName)
                {
                    case "Chrome":
                    {
                        if (Driver == null)
                        {
                            driver = new ChromeDriver();
                            Drivers.Add("Chrome", Driver);
                        }
                        break;
                    }

                    case "Firefox":
                        if (Driver == null)
                        {
                            driver = new FirefoxDriver();
                            Drivers.Add("Firefox", Driver);
                        }
                        break;
                }
            }
        }

        public static void LoadApplication(string url)
        {
            Driver.Url = url;
        }

        public static void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
