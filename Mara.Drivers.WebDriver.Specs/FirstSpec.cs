using System;
using NUnit.Framework;
using Mara;

namespace Mara.Drivers.WebDriverSpecs {

    [TestFixture]
    public class FirstSpec : MaraTestFixture {

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
