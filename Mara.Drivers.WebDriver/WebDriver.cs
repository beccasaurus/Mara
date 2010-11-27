using System;
using System.Collections.Generic;

using Mara;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
// using OpenQA.Selenium.IE; // <--- only on Windows ... can we get/load these dynamically?
using OpenQA.Selenium.Remote;

namespace Mara.Drivers {

    /*
     * Mara IDriver implementation for Selenium WebDriver
     */
    public class WebDriver : IDriver {

        public static string DefaultBrowser            = "firefox";
        public static int    DefaultSeleniumServerPort = 4444;
        public static string DefaultSeleniumServerJar  = "selenium-server-standalone.jar";

        public WebDriver() {
            SetBrowserAndRemoteFromEnvironmentVariables();
        }

        void SetBrowserAndRemoteFromEnvironmentVariables() {
            // TODO test this
            // Handle environment variables like CHROME_PATH or FIREFOX_PATH
            WebDriver.OverrideBrowserPathsFromEnvironmentVariables();

			var browserName = Environment.GetEnvironmentVariable("BROWSER");
			var remoteUri   = Environment.GetEnvironmentVariable("REMOTE");
			var htmlUnitUri = Environment.GetEnvironmentVariable("HTMLUNIT");

			// HTMLUNIT is a shortcut for setting BROWSER=HtmlUnit and REMOTE=[the remote uri]
			if (htmlUnitUri != null) {
				browserName = "HtmlUnit";
				remoteUri   = htmlUnitUri;
			}

            // REMOTE=true is a shortcut for telling us to run the default, local selenium server
            if (remoteUri != null && remoteUri.ToLower() == "true")
                remoteUri = DefaultRemote;

            if (browserName != null) Browser = browserName;
            if (remoteUri   != null) Remote  = remoteUri;
        }

        static void OverrideBrowserPathsFromEnvironmentVariables() {
            // TODO is there any harm in simply looking for all environment variables ending in _PATH?
            foreach (var browser in new List<String>(){ "chrome", "firefox", "ie" }){
                var path = Environment.GetEnvironmentVariable(browser.ToUpper() + "_PATH");
                if (path != null) {
                    // Override the webdriver.[browser].bin variable
                    Environment.SetEnvironmentVariable("webdriver." + browser + ".bin", path);

                    // Set the BROWSER environment variable to this browser, if it's not already set
                    if (Environment.GetEnvironmentVariable("BROWSER") == null)
                        Environment.SetEnvironmentVariable("BROWSER", browser);
                }
            }
        }

        public bool? RunSeleniumStandalone { get; set; }

        string _seleniumServerJar;
        public string SeleniumServerJar {
            get {
                if (_seleniumServerJar == null) return WebDriver.DefaultSeleniumServerJar;
                return _seleniumServerJar;
            }
            set { _seleniumServerJar = value; }
        }

        int _seleniumServerPort = -1;
        public int SeleniumServerPort {
            get {
                if (_seleniumServerPort == -1) return WebDriver.DefaultSeleniumServerPort;
                return _seleniumServerPort;
            }
            set { _seleniumServerPort = value; }
        }

        string DefaultRemote {
            get { return String.Format("http://localhost:{0}/wd/hub", SeleniumServerPort); }
        }

        string _browser;
        public string Browser {
            get {
                if (_browser == null) return WebDriver.DefaultBrowser;
                return _browser.ToLower();
            }
            set {
                if (value != null && value.ToLower() == "htmlunit" && Remote == null)
                    Remote = DefaultRemote;
                _browser = value;
            }
        }

        public string SeleniumServerUrl {
            get { return string.Format("http://localhost:{0}/wd/hub", SeleniumServerPort); }
        }

        string _remote;
        public string Remote {
            get { return _remote; }
            set {
                // If you set Remote to localhost using the SeleniumServerPort, set RunSeleniumStandalone to true (unless already set)
                if (RunSeleniumStandalone == null)
                    if (value != null && value == SeleniumServerUrl)
                        RunSeleniumStandalone = true;

                // If you set Remote to something besides localhost, there's no way we can run it!
                if (RunSeleniumStandalone == true)
                    if (value != null && value != SeleniumServerUrl)
                        RunSeleniumStandalone = false;

                _remote = value;
            }
        }

        IWebDriver _webdriver;
        IWebDriver webdriver {
            get {
                if (_webdriver == null) _webdriver = InstantiateWebDriver();
                return _webdriver;
            }
        }

        IWebDriver InstantiateWebDriver() {
            // force to be a certain driver ... just for testing ... this will be configurable ...
            //return new ChromeDriver();
            //return new InternetExplorerDriver();
            return new FirefoxDriver();
        }

        public void Close() {
            webdriver.Close();
        }

        public void ResetSession() {
            throw new NotImplementedException();
        }

        public void Visit(string path) {
            Console.WriteLine("WebDriver.Visit navigating to {0}{1}", Mara.AppHost, path);

            webdriver.Navigate().GoToUrl(Mara.AppHost + path);
            //Console.WriteLine("... waiting for chrome ...");
            //System.Threading.Thread.Sleep(2000);
            
            // Making Chrome work ..
            //for (var i = 0; i < 10; i ++) {
            //    if (PageHasLoaded)
            //        break;
            //    else {
            //        Console.WriteLine("Waiting for page to fully load ...");
            //        System.Threading.Thread.Sleep(200);
            //    }   
            //}
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

        // private methods

        bool PageHasLoaded {
            get {
                if (webdriver == null) return false;
                try {
                    if (webdriver.PageSource == null) return false;
                    return true;
                } catch (NullReferenceException) {
                    Console.WriteLine("Page has not loaded ...");
                    return false; // calling driver.PageSource probably blew up (happens in the ChromeDriver)
                }   
            }   
        } 
    }
}
