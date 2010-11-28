using System;
using NUnit.Framework;
using Mara;

namespace Mara.DriverSpecs {

    /*
     * The spec covers the basics of finding and interacting with elements:
     *
     *   Find()
     *   All()
     *   Find("...").Click()
     *   Find("...").Text
     *   FillIn("...", "...")
     */
    [TestFixture]
    public class ElementsSpec : MaraTest {

        [SetUp] public void Setup(){ Visit("/"); }

        [Test]
        public void CanFindElementUsingXPath_ReturnsNullIfNotFound() {
             Assert.That(Find("//h2").Text, Is.EqualTo("Why is this styled?"));
             Assert.Null(Find("//h5"));
        }

        [Test]
        public void CanFindElementUsingXPath_BlowsUpIfNotFound() {
             Assert.That(Find("//h2").Text, Is.EqualTo("Why is this styled?"));

             //this.AssertThrows<ElementNotFoundException>("Could not find element with XPath: //h5", () => { Find("//h5") });
             this.AssertThrows<ElementNotFoundException>("Could not find element with XPath: //h5", () => {
                     Find("//h5", true);
             });
        }

        [Test]
        public void CanFindElementsUsingXPath_ReturnsEmptyIfNotFound() {
            Assert.That(All("//p").Count, Is.EqualTo(5));
            Assert.That(All("//p")[0].Text, Is.EqualTo("If you see this text, you're running our test suite!"));

            Assert.IsEmpty(All("//strong"));
            Assert.That(All("//strong").Count, Is.EqualTo(0));
        }

        [Test][Ignore]
        public void CanGetTheValueOfAnElement() {}

        [Test][Ignore]
        public void CanGetTheTextOfAnElement() {}

        [Test][Ignore]
        public void CanGetTheValueOfAnElementsAttribute() {}

        [Test][Ignore]
        public void CanFindLinkByText_ReturnsNullIfNotFound() {
        }

        [Test][Ignore]
        public void CanClickLink_BlowsUpIfNotFound() {
        }
    }
}
