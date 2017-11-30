using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Onliner.Framework
{
    class GetConfig
    {
        private static string browser;
        private static string url;
        private static int implicitwait;
        private static int expectedwait;
        private static string pathcsv;
        private static string username;
        private static string password;

        private static XmlReader reader = XmlReader.Create(OnlinerTest.appDir + @"Onliner\..\..\config.xml");
        private static XmlNodeType node;

        public static void ReadConfig()
        {
            while (reader.Read())
            {
                node = reader.NodeType;

                if (node == XmlNodeType.Element)
                    switch (reader.Name)
                    {
                        case "browser":
                            reader.Read();
                            browser = reader.Value;
                            break;
                        case "url":
                            reader.Read();
                            url = reader.Value;
                            break;
                        case "implicitwait":
                            reader.Read();
                            implicitwait = Convert.ToInt32(reader.Value);
                            break;
                        case "expectedwait":
                            reader.Read();
                            expectedwait = Convert.ToInt32(reader.Value);
                            break;
                        case "pathcsv":
                            reader.Read();
                            pathcsv = reader.Value;
                            break;
                        case "username":
                            reader.Read();
                            username = reader.Value;
                            break;
                        case "password":
                            reader.Read();
                            password = reader.Value;
                            break;
                    }                
            }
            reader.Close();            
        }
        
                       
        public static string GetBrowser()
        {
            ReadConfig();
            return browser;
        }

        public static string GetUrl()
        {
            return url;
        }

        public static int GetImplicitWait()
        {
            return implicitwait;
        }

        public static int GetExpectedWait()
        {
            return expectedwait;
        }

        public static string GetPathCsv()
        {
            return pathcsv;
        }

        public static string GetUsername()
        {
            return username;
        }

        public static string GetPassword()
        {
            return password;
        }
    }
}
