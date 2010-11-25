using System;

using Mara;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Mara.Drivers {

    /*
     * Mara IDriver implementation for Selenium WebDriver
     */
    public class WebDriver : IDriver {

        IWebDriver _webdriver;
        IWebDriver webdriver {
            get {
                if (_webdriver == null) _webdriver = InstantiateWebDriver();
                return _webdriver;
            }
        }

        IWebDriver InstantiateWebDriver() {
            // force to be firefox ... for now ... to get it up and running quickly ... we'll spec customization ASAP
            return new FirefoxDriver();
        }

        public void Close() {
            webdriver.Close();
        }

        public void ResetSession() {
            throw new NotImplementedException();
        }

        public void Visit(string path) {
            Console.WriteLine("WebDriver.Visit navigating to {0} {1}", Mara.AppHost, path);
            webdriver.Navigate().GoToUrl(Mara.AppHost + path);
        }

        public string Body {
            get { return webdriver.PageSource; }
        }

        public string CurrentUrl {
            get { throw new NotImplementedException(); }
        }

        public string CurrentPath {
            get { throw new NotImplementedException(); }
        }
    }
}
