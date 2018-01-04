using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using OpenQA.Selenium;


namespace Onliner.Framework
{
    class CsvWriter
    {
        private IWebDriver driver;
        public CsvWriter()
        {
            Browser.GetInstance().GetBrowser();
        }

        public static void PickOpinionsInCsv(string strPattern)
        {
            var pattern = new Regex(strPattern);
            string pageSource = Browser.PageSource();
            Match match = pattern.Match(pageSource);
            FileStream file = new FileStream(OnlinerTest.appDir + Configuration.GetPathCsv(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            StreamWriter writer = new StreamWriter(file, Encoding.UTF8);
            
            while (match.Success)
            {
                writer.WriteLine("\"" + match.Groups[1].Value + "\"");
                match = match.NextMatch();
            }
            
            writer.Flush();
        }
    }
}
