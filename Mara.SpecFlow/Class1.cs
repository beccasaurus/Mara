using System;
using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Mara;
using Mara.Drivers;

namespace Mara {

    // [Given(@"")][When(@"")][Then(@"")]

    /// <summary>SpecFlow step definitions for Mara</summary>
    [Binding]
    public class StepDefinitions {

        public StepDefinitions() {
            ContextPrefix = "Mara.";
        }

        static IDriver _globalDriver;
        Dictionary<string,object> _context;

        /// <summary>A static IDriver that all StepDefinitions will use by default (falls back to Mara.Driver if not set)</summary>
        public static IDriver GlobalDriver {
            get { return _globalDriver ?? (_globalDriver = Mara.Driver); }
            set { _globalDriver = value; }
        }

        /// <summary>Helper for getting the current Dictionary representing the SpecFlow context</summary>
        public virtual Dictionary<string,object> Context {
            get { return _context ?? ScenarioContext.Current; }
            set { _context = value; }
        }

        /// <summary>Prefix used on all keys for our variables stored in the current SpecFlow context</summary>
        public virtual string ContextPrefix { get; set; }

        /// <summary>Helper for getting/setting objects in the current SpecFlow context (prefixes keys with ContextPrefix)</summary>
        public virtual object this[string key] {
            get { return Context.ContainsKey(ContextPrefix + key) ? Context[ContextPrefix + key] : null; }
            set { Context[ContextPrefix + key] = value; }
        }

        /// <summary>The Mara IDriver to use for these StepDefinitions (falls back to GlobalDriver if not set)</summary>
        public virtual IDriver Driver {
            get {
                if (this["Driver"] == null) this["Driver"] = GlobalDriver;
                return this["Driver"] as IDriver;
            }
            set { this["Driver"] = value; }
        }

        [BeforeScenario]
        public virtual void BeforeScenario() {
            if (Driver != null) Driver.ResetSession();
        }

        [Given(@"the root URL is (.+)")][When(@"the root URL is (.+)")][Then(@"the root URL is (.+)")]
        public virtual void SetRootUrl(string url) {
            Driver.Root = url;
        }

        [Given(@"I visit (.+)")][When(@"I visit (.+)")][Then(@"I visit (.+)")]
        public virtual void Visit(string pathOrUrl) {
            Driver.Visit(pathOrUrl);
        }

        [Given(@"I fill in ""([^""]+)"" with ""([^""]+)""")][When(@"I fill in ""([^""]+)"" with ""([^""]+)""")][Then(@"I fill in ""([^""]+)"" with ""([^""]+)""")]
        public virtual void FillIn(string field, string value) {
            Driver.FillIn(field, value);
        }

        [Given(@"I click ""([^""]+)""")][When(@"I click ""([^""]+)""")][Then(@"I click ""([^""]+)""")]
        public virtual void Click(string linkOrButton) {
            Driver.Click(linkOrButton);
        }

        [Given(@"I click button ""([^""]+)""")][When(@"I click button ""([^""]+)""")][Then(@"I click button ""([^""]+)""")]
        public virtual void ClickButton(string buttonValue) {
            Driver.ClickButton(buttonValue);
        }

        [Given(@"I click link ""([^""]+)""")][When(@"I click link ""([^""]+)""")][Then(@"I click link ""([^""]+)""")]
        public virtual void ClickLink(string linkText) {
            Driver.ClickLink(linkText);
        }

        [Given(@"I should see ""([^""]+)""")][When(@"I should see ""([^""]+)""")][Then(@"I should see ""([^""]+)""")]
        public virtual void ShouldSee(string content) {
            var bodyElement = Driver.Find("//body");
            if (bodyElement != null)
                Assert.That(bodyElement.Text, Is.StringContaining(content));
            else
                Assert.That(Driver.Body, Is.StringContaining(content)); // if there's no <body>, check the raw HTML
        }

        [Given(@"I should not see ""([^""]+)""")][When(@"I should not see ""([^""]+)""")][Then(@"I should not see ""([^""]+)""")]
        public virtual void ShouldNotSee(string content) {
            var bodyElement = Driver.Find("//body");
            if (bodyElement != null)
                Assert.That(bodyElement.Text, Is.Not.StringContaining(content));
            else
                Assert.That(Driver.Body, Is.Not.StringContaining(content)); // if there's no <body>, check the raw HTML
        }

        // When I fill in fields for "User" with:
	    // Field        | Value      |
	    // FirstName    | Bob        |
        // LastName     | Smith      |
        [Given(@"I fill in fields for ""([^""]+)"" with:")][When(@"I fill in fields for ""([^""]+)"" with:")][Then(@"I fill in fields for ""([^""]+)"" with:")]
        public void FillInFieldsFor(string fieldPrefix, Table table) {
            fieldPrefix = fieldPrefix.Replace(" ", ".") + "."; // "User Address" becomes "User.Address."
            foreach (var row in table.Rows)
                Driver.FillIn(fieldPrefix + row["Field"], row["Value"]);
        }

        [Given(@"I select ""([^""]+)"" from ""([^""]+)""")][When(@"I select ""([^""]+)"" from ""([^""]+)""")][Then(@"I select ""([^""]+)"" from ""([^""]+)""")]
        public void Select(string value, string select) {
            Driver.Select(select, value);
        }

        // Specifically setup for MVC checkboxes ... only supports selection by name (currently) ...
        [Given(@"I check ""([^""]+)""")][When(@"I check ""([^""]+)""")][Then(@"I check ""([^""]+)""")]
        public void CheckCheckbox(string name) {
            if (Driver.GetType() == typeof(WebDriver))
                Driver.Find("//input[@type='checkbox'][@name='" + name + "']").Click();
            else
                Driver.All("//input[@name='" + name + "']").ForEach(element => element.Value = "true");   
        }

        [Given(@"I set hidden field ""([^""]+)"" to ""([^""]+)""")][When(@"I set hidden field ""([^""]+)"" to ""([^""]+)""")][Then(@"I set hidden field ""([^""]+)"" to ""([^""]+)""")]
        public void SetHiddenField(string field, string value) {
            var idXpath   = "//input[@type='hidden'][@id='"   + field + "']";
            var nameXpath = "//input[@type='hidden'][@name='" + field + "']";

            var element = Driver.Find(idXpath);
            if (element == null)
                element = Driver.Find(nameXpath);
            if (element == null)
                throw new ElementNotFoundException(idXpath + " OR " + nameXpath);

            element.Value = value;
        }

        [Given(@"I save and open page")][When(@"I save and open page")][Then(@"I save and open page")]
        public void SaveAndOpenPage() {
            Driver.SaveAndOpenPage();
        }
    }
}