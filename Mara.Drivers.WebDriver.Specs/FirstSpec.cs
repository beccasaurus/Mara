using System;
using NUnit.Framework;
using Mara;

namespace Mara.WebDriver.Specs {

    [TestFixture]
    public class FirstSpec : Mara {

        // Move into an NUnit-specific baseclass ...
        [SetUp]
        public void Setup() {
            // if this could lazily initialize, that would be nice
            Console.WriteLine("SERVER: {0}", Mara.Server);
            Mara.Server.Start();
        }
        [TearDown]
        public void Teardown() { Page.Close(); }

        [Test]
        public void CanOpenAPageOrWhatever() {
            Visit("/");
            Assert.That(Page.Body, Is.StringContaining("Mara test application"));
            Assert.That(Page.Body, Is.Not.StringContaining("About this site"));
            
            Visit("/About.aspx");
            Assert.That(Page.Body, Is.StringContaining("About this site"));
            Assert.That(Page.Body, Is.Not.StringContaining("Mara test application"));
        }
    }
}
