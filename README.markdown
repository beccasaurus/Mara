Mara
====

Description
-----------

Mara aims to simplify the process of integration testing .NET web applications, such as ASP.NET, ASP.NET MVC, or WCF. Mara simulates how a real user would interact with a web application.  It is agnostic about the driver running your tests and currently comes bundled with Selenium support built in.  It is also agnostic about servers that can automatically server your application and currently comes bundles with Cassini and XSP support built in.

Mara is inspired by [Capybara][], a similar tool written in Ruby.

HERE BE DRAGONS
---------------

Mara is *insanely* ALPHA right now.  I recommend you give me a few weeks to polish it before you start using it or contributing.  The API may go through some major changes so ... just be patient.  If you're a hacker and you want to check out the code and try to get it working, be my guest!

Install
-------

We will make Mara available as a [NuGet][] package.  For now, you can clone the solution and build it yourself if you *really* want to try it out.

Hello World (the easy way)
--------------------------

If you're using NUnit, we have a Mara.NUnit assembly that will help you get up and running quickly.

    using Mara;

    namespace MyIntegrationTests {

        // This will instantiate a driver/server for you when your test suite starts and will stop them when it stops
        [SetUpFixture] public class MaraSetup : MaraSetUpFixture {}

        // If your test inherits from MaraTest, you get direct access to all of the driver's methods
        // without having to call them on an object.  eg. You can call Visit() instead of Driver.Visit()
        [TestFixture]
        public class TestMyWebSite : MaraTest {

            [Test]
            public void CanRegister() {
                Visit("/");
                ClickLink("Register");
                FillInFields(new {
                    Username             = "bobsmith",
                    Email                = "bob@smith.com",
                    Password             = "secret",
                    PasswordConfirmation = "secret"
                });
                ClickButton("Sign up!");

                Assert.That(Find("id('Message')").Text, Is.EqualTo("Successfully Registered")); // XPath is accepted by Find() and All()
            }
        }
    }

That should run!

So ... how does it find your web application?  When your tests run, if you haven't explicitly told it where your 
web application is, it looks in parent directories for a directory containing a Web.config.  To manually tell Mara 
where your ASP.NET application is:

    Mara.App = @"C:\Users\me\MyAspNetApp";

It also automatically boots up your application via Cassini (or XSP if running via Mono).

    Mara.RunServer = false; // tell Mara not to run your application

    Mara.DefaultServerName = "Mara.Servers.Cassini"; // FullName of the class that will be used as a default server
 
    Mara.Server = new MyServer(); // tell Mara to use your server.  It needs to use the *very simply* IServer interface 

    Mara.Port = 1234; // set the port that servers use by default

There's a lot more ... the API for some of this stuff will VERY likely change though!  You've been warned!

Hello World (doing it all yourself)
-----------------------------------

So, you're not using NUnit.  Or you don't like the magic.  No problem!

    using Mara;

    namespace MyIntegrationTests {

        [SetUpFixture]
        public class MyFixture {

	    static IDriver maraDriver;

            [SetUp]
            public void Setup() {
                // do some configuration (optional)
                Mara.App = @"C:\Path\To\My\App\Folder";

                // Tell the default Mara.Server to boot up your application (or you could do this manually).
                // If you manually start your own server, be sure to set Mara.AppHost to the URL to the root 
                // of your application, so it can be found by drivers, eg. "http://localhost:1234"
	        Mara.Server.Start();

                // Instantiate a driver.  Again, you can do this manually.  We'll use the default driver
                maraDriver = Mara.Driver; // this instantiates a default driver for you
            }

            [TearDown]
            public void Teardown() {
	        // Stop the server
                Mara.Server.Stop();

                // Close the driver
                maraDriver.Close();
            }

        }

        [TestFixture]
        public class TestMyWebSite {

	    // do something to make the driver available to your tests
            IPage Page { get { return MyFixture.maraPage; }}

            [Test]
            public void CanRegister() {
                Page.Visit("/");
                Page.ClickLink("Register");
                Page.FillInFields(new {
                    Username             = "bobsmith",
                    Email                = "bob@smith.com",
                    Password             = "secret",
                    PasswordConfirmation = "secret"
                });
                Page.ClickButton("Sign up!");

                Assert.That(Page.Find("id('Message')").Text, Is.EqualTo("Successfully Registered")); // XPath is accepted by Find() and All()
            }
        }
    }

Assuming I typed that all out correctly, that should get you up and running doing it all yourself.

The DSL
-------

### Navigating

You can use the `Visit` method to navigate to other pages:

    Visit("/About.aspx");

The visit method only takes a single parameter, the request method is **always** GET.

You can get the current path and url of the browsing session for test assertions:

    Assert.True(CurrentPath == "/About.aspx");
    Assert.True(CurrentUrl  == "http://localhost:8080/About.aspx");

### Clicking links and buttons

You can interact with the webapp by following links and buttons. Mara automatically follows any redirects, and submits forms associated with buttons.

    ClickLink("text of link");
    ClickButton("value of submit button");

    Click("FAQ"); // If there's a Link with the text "FAQ", that is clicked, otherwise tries to ClickButton("FAQ");

### Interacting with forms

Forms are everywhere in webapps, there are a number of tools for interacting with the various form elements:

    FillIn("ID or Name of field", "Value");

    // Calls FillIn() for each of the key/value pairs passed in
    FillInFields(new { Username = "bobsmith", Password = "secret" });

**NOTE**: A number of other form helper methods will be implemented shortly ... there are NOT implemented yet:

    Choose("A Radio Button");
    Check("A Checkbox");
    Uncheck("A Checkbox");
    Select("Option", "ID or Name of <select>")
    AttachFile("ID or Name of field", "/path/to/image.jpg");

### Querying

Mara has some *very* simple querying helpers:

    Page.HasContent("text");

    Page.HasXPath("//h1");

We don't yet support things like querying within scopes, etc, etc.  Eventually!

### Finding

You can find elements in order to manipulate them.

    var nameField = Find("//input[@name='DogName']");
    nameField.Value = "Set the value of this field";
    Console.WriteLine("The alt attribute is {0}", nameField["alt"]);
    
    Find("id('MyButton')").Click();

    // Or, to find many elements:
    foreach (var link in All("//a"))
        Console.WriteLine("Link with text {0} goes to {1}", link.Text, link["href"]);

### Scripting

In drivers which support it, you can easily execute JavaScript:

    ExecuteScript("$('body').empty()");

For simple expressions, you can return the result of the script. Note that this may break with more complicated expressions:

    var result = EvaluateScript("4 + 4");

### Debugging

It can be useful to take a snapshot of the page as it currently is and take a look at it:

    SaveAndOpenPage();

More
----

There is much, much more ... some of it developed, some not yet.

More documentation and features coming soon!

License
-------

Mara is released under the MIT license.

[capybara]: https://github.com/jnicklas/capybara
[nuget]:    http://nuget.codeplex.com/
