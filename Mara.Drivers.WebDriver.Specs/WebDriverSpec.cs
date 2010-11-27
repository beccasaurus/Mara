using System;
using NUnit.Framework;
using Mara;
using Mara.Drivers;

namespace Mara.Drivers.WebDriverSpecs {

    [TestFixture]
    public class WebDriverSpec {

        [SetUp]
        public void Setup() {
            Environment.SetEnvironmentVariable("BROWSER",  null);
            Environment.SetEnvironmentVariable("REMOTE",   null);
            Environment.SetEnvironmentVariable("HTMLUNIT", null);
        }

        [Test]
        public void BrowserDefaultsToFirefox() {
            var driver = new WebDriver();
            Assert.That( driver.Browser, Is.EqualTo("firefox") );
        }

        [Test]
        public void CanSetBrowser() {
            var driver = new WebDriver();
            Assert.That( driver.Browser, Is.EqualTo("firefox") );

            driver.Browser = "ie";
            Assert.That( driver.Browser, Is.EqualTo("ie") );

            driver.Browser = "arbitrary";
            Assert.That( driver.Browser, Is.EqualTo("arbitrary") );

            driver.Browser = null;
            Assert.That( driver.Browser, Is.EqualTo("firefox") ); // default if null
        }

        [Test]
        public void EnvironmentVariableCanBeUsedToSetBrowser() {
            Environment.SetEnvironmentVariable("BROWSER", "Chrome");
            Assert.That(new WebDriver().Browser, Is.EqualTo("chrome"));

            Environment.SetEnvironmentVariable("BROWSER", null);
            Assert.That(new WebDriver().Browser, Is.EqualTo(WebDriver.DefaultBrowser));
        }

        [Test]
        public void CanSetRemoteUrlUsedForRunningAgainstSeleniumStandalone() {
            var driver = new WebDriver();
            Assert.Null(driver.Remote);

            driver.Remote = "http://localhost:4444/wd/hub";
            Assert.That(driver.Remote, Is.EqualTo("http://localhost:4444/wd/hub"));
        }

        [Test]
        public void EnvironmentVariableCanBeUsedToSetRemoteUrl() {
            Environment.SetEnvironmentVariable("REMOTE", "http://some-remote-server:1234");
            Assert.That(new WebDriver().Remote, Is.EqualTo("http://some-remote-server:1234"));

            Environment.SetEnvironmentVariable("REMOTE", null); // defaults to null
            Assert.Null(new WebDriver().Remote);
        }

        [Test]
        public void CanSetHtmlUnitWhichMustRunViaRemote() {
            var driver = new WebDriver();
            Assert.Null(driver.Remote);
            Assert.That(driver.Browser, Is.EqualTo(WebDriver.DefaultBrowser));
            
            driver.Browser = "HtmlUnit";
            Assert.That(driver.Browser, Is.EqualTo("htmlunit"));
            Assert.NotNull(driver.Remote);
            Assert.That(driver.Remote, Is.EqualTo("http://localhost:" + driver.SeleniumServerPort.ToString() + "/wd/hub"));
        }

        [Test]
        public void EnvironmentVariableCanBeUsedToSpecifyHtmlUnit() {
            Environment.SetEnvironmentVariable("BROWSER", "HtmlUnit");
            var driver = new WebDriver();
            Assert.That(driver.Remote, Is.EqualTo("http://localhost:" + driver.SeleniumServerPort.ToString() + "/wd/hub"));
            Assert.That(driver.Browser, Is.EqualTo("htmlunit"));

            Environment.SetEnvironmentVariable("BROWSER", null);
            driver = new WebDriver();
            Assert.That(driver.Browser, Is.EqualTo(WebDriver.DefaultBrowser));
            Assert.Null(driver.Remote);
        }

        [Test]
        public void EnvironmentVariableCanBeUsedToSpecifyHtmlUnitAndRemote() {
            Environment.SetEnvironmentVariable("HTMLUNIT", "http://remote-server:1234/wd/hub");
            var driver = new WebDriver();
            Assert.That(driver.Remote, Is.EqualTo("http://remote-server:1234/wd/hub"));
            Assert.That(driver.Browser, Is.EqualTo("htmlunit"));
        }

        [Test]
        public void CanSetWhetherOrNotSeleniumStandaloneShouldBeRun() {
            // by default, RunSeleniumStandalone should == null
            var driver = new WebDriver();
            Assert.Null(driver.RunSeleniumStandalone);

            // if Remote gets set to localhost and this is not set to false, it gets set to true
            driver = new WebDriver();
            Assert.Null(driver.RunSeleniumStandalone);
            driver.Remote = string.Format("http://localhost:{0}/wd/hub", driver.SeleniumServerPort);
            Assert.That(driver.RunSeleniumStandalone, Is.EqualTo(true));

            // can set to false
            driver.RunSeleniumStandalone = false;
            Assert.That(driver.RunSeleniumStandalone, Is.EqualTo(false));
            
            // if false, setting Remote won't override this and set it to true
            driver.Remote = string.Format("http://localhost:{0}/wd/hub", driver.SeleniumServerPort);
            Assert.That(driver.RunSeleniumStandalone, Is.EqualTo(false));
        }

        [Test]
        public void CanSetPathToSeleniumStandaloneJar() {
            var driver = new WebDriver();
            Assert.That(driver.SeleniumServerJar, Is.EqualTo("selenium-server-standalone.jar")); // default

            driver.SeleniumServerJar = "foo.jar";
            Assert.That(driver.SeleniumServerJar, Is.EqualTo("foo.jar")); // default
        }

        [Test][Ignore("Pending")]
        public void CanManuallyGiveWebDriverAnInstantiatedSeleniumIWebDriverInstance() {

        }

        [Test]
        public void Example_LocalFirefoxViaEnvironmentVariables() {
            Environment.SetEnvironmentVariable("BROWSER", "FireFox");
            
            var driver = new WebDriver();
            Assert.That(driver.Browser, Is.EqualTo("firefox"));
            Assert.Null(driver.Remote);
        }

        [Test]
        public void Example_RemoteFirefoxViaEnvironmentVariables() {
            Environment.SetEnvironmentVariable("BROWSER", "FireFox");
            Environment.SetEnvironmentVariable("REMOTE",  "true"); // shortcut for telling it to boot the local selenium server
            
            var driver = new WebDriver();
            Assert.That(driver.Browser, Is.EqualTo("firefox"));
            Assert.That(driver.Remote,  Is.EqualTo(string.Format("http://localhost:{0}/wd/hub", driver.SeleniumServerPort)));
            Assert.That(driver.RunSeleniumStandalone, Is.EqualTo(true));
        }

        [Test]
        public void Example_LocalHtmlUnitViaEnvironmentVariables() {
            Environment.SetEnvironmentVariable("BROWSER", "HtmlUnit");
            
            var driver = new WebDriver();
            Assert.That(driver.Browser, Is.EqualTo("htmlunit"));
            Assert.That(driver.Remote,  Is.EqualTo(string.Format("http://localhost:{0}/wd/hub", driver.SeleniumServerPort)));
            Assert.That(driver.RunSeleniumStandalone, Is.EqualTo(true));
        }

        [Test]
        public void Example_RemoteHtmlUnitViaEnvironmentVariables() {
            Environment.SetEnvironmentVariable("BROWSER", "HtmlUnit");
            Environment.SetEnvironmentVariable("REMOTE",  "http://some-remote-server.com:1234/foo/bar");
            
            var driver = new WebDriver();
            Assert.That(driver.Browser, Is.EqualTo("htmlunit"));
            Assert.That(driver.Remote,  Is.EqualTo("http://some-remote-server.com:1234/foo/bar"));
            Assert.That(driver.RunSeleniumStandalone, Is.EqualTo(false));
        }

        [Test][Ignore]
        public void ShouldBeAbleToSetPathToSeleniumStandaloneServerJarViaEnvironmentVariable() {

        }
    }
}
